namespace AdminSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AdminPanel.DataBaseCore;
    using AdminPanelDataBaseCore.Entities;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    [Route("api/[controller]")]
    public class AdminTokenController : BaseApiController
    {
        public AdminTokenController(
        AdminDbContext context,
        IAdminRepositoryDb repo,
        IConfiguration configuration)
        : base(context, repo, configuration)
        {
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Jwt([FromBody]TokenRequestViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            switch (model.GrantType)
            {
                case "password":
                    return await this.GetToken(model);
                            case "refresh_token":
                    return await this.RefreshToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestViewModel model)
        {
            try
            {
                // check if the received refreshToken exists for the given clientId
                var rt = this.DbContext.Tokens
                    .FirstOrDefault(t => t.ClientId == model.ClientId
                    && t.Value == model.RefreshToken);
                if (rt == null)
                {
                    // refresh token not found or invalid (or invalid clientId)
                    return new UnauthorizedResult();
                }

                // check if there's an user with the refresh token's userId
                var user = await this.Repository.FindUserByIdAsync(rt.UserId);

                if (user == null)
                {
                    // UserId not found or invalid
                    return new UnauthorizedResult();
                }

                // generate a new refresh token
                var rtNew = this.CreateRefreshToken(rt.ClientId, rt.UserId);

                // invalidate the old refresh token (by deleting it)
                this.DbContext.Tokens.Remove(rt);

                // add the new refresh token
                this.DbContext.Tokens.Add(rtNew);

                // persist changes in the DB
                this.DbContext.SaveChanges();

                // create a new access token...
                var response = this.CreateAccessToken(rtNew.UserId, rtNew.Value);

                // ... and send it to the client
                return this.Json(response);
            }
            catch (Exception)
            {
                return new UnauthorizedResult();
            }
        }

        private Token CreateRefreshToken(string clientId, string userId)
        {
            return new Token()
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow
            };
        }

        private TokenResponseViewModel CreateAccessToken(string userId, string refreshToken)
        {
            DateTime now = DateTime.UtcNow;

            // add the registered claims for JWT (RFC7519).
            // For more info, see https://tools.ietf.org/html/rfc7519#section-4.1
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())

                    // TODO: add additional claims here
            };
            var tokenExpirationMins = this.Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.Configuration["Auth:Jwt:Key"]));
            var token = new JwtSecurityToken(
            issuer: this.Configuration["Auth:Jwt:Issuer"],
            audience: this.Configuration["Auth:Jwt:Audience"],
            claims: claims,
            notBefore: now,
            expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
            signingCredentials: new SigningCredentials(
                issuerSigningKey, SecurityAlgorithms.HmacSha256));
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponseViewModel()
            {
                Token = encodedToken,
                Expiration = tokenExpirationMins,
                RefreshToken = refreshToken
            };
        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel model)
        {
            try
            {
                // check if there's an user with the given username
                var user = await this.Repository.FindUserByNameAsync(model.UserName);

                // fallback to support e-mail address instead of username
                if (user == null && model.UserName.Contains("@"))
                {
                    user = await this.Repository.FindUserByEmailAsync(model.UserName);
                }

                if (user == null || !await this.Repository.CheckPasswordAsync(user, model.Password))
                {
                    // user does not exists or password mismatch
                    return new UnauthorizedResult();
                }

                // username & password matches: create the refresh token
                var rt = this.CreateRefreshToken(model.ClientId, user.Id);

                // add the new refresh token to the DB
                this.DbContext.Tokens.Add(rt);

                this.DbContext.SaveChanges();

                // create & return the access token
                var t = this.CreateAccessToken(user.Id, rt.Value);
                return this.Json(t);
            }
            catch (Exception)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
