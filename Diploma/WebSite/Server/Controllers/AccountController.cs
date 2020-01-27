namespace Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Server.DataBaseCore.AppSettings;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;
    using Server.Infrastructure.Models.AccountViewModels;

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUltraRepository ultraRepository;

        public AccountController(IUltraRepository ultraRepository)
        {
            this.ultraRepository = ultraRepository;
            this.AppSettings = this.AppSettings;
        }

        protected AppSettings AppSettings { get; private set; }

        // POST: api/Account/Login
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.ultraRepository.PasswordSignInAsync(model.Email, model.Password, model.RememberMe);
                if (result.Succeeded)
                {
                    return new OkObjectResult("User logged in.");
                }

                if (result.IsLockedOut)
                {
                    return new BadRequestObjectResult("Locked account.");
                }
                else
                {
                    return new BadRequestObjectResult(result);
                }
            }

            return new BadRequestObjectResult("Failed");
        }

        // POST: api/Account/LogOut
        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> LogOutAsync([FromBody]RegisterViewModel model)
        {
            await this.ultraRepository.SignOutAsync();
            return new OkObjectResult("Logged out.");
        }

        // POST: api/Account/RegisterUser
        [AllowAnonymous]
        [HttpPut]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterIdentityUserAsync([FromBody]RegisterViewModel model)
        {
            if (Equals(model, null))
            {
                return new StatusCodeResult(500);
            }

            ApplicationUser user = await this.ultraRepository.FindUserByEmailAsync(model.Email).ConfigureAwait(false);
            if (user != null)
            {
                return this.BadRequest("Ця пошта вже зареєстрована у системі!");
            }

            user = await this.ultraRepository.FindUserByNameAsync(model.Login).ConfigureAwait(false);
            if (user != null)
            {
                return this.BadRequest("Цей логін вже зайнято!");
            }

            try
            {
                IdentityResult result = await this.ultraRepository.RegisterIdentityUserAsync(model.Email, model.Login, model.Password).ConfigureAwait(false);

                user = await this.ultraRepository.FindUserByEmailAsync(model.Email);

                result = await this.ultraRepository.AddUserDefaultRoleAsync(user, "Patient");

                // Remove Lockout and E-Mail confirmation
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;

                // persist the changes into the Database.
                await this.ultraRepository.SaveChangesAsync();
                return this.Json(user);
            }
            catch
            {
                this.ModelState.AddModelError(string.Empty, "Невдала спроба зареєструватися.");
                return this.BadRequest(this.ModelState);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ResetPassword", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email)
        {
            ApplicationUser user = await this.ultraRepository.FindUserByEmailAsync(email).ConfigureAwait(false);

            if (!Equals(user, null))
            {
                var userToken = await this.ultraRepository.GenerateTokenAsync(user.Id).ConfigureAwait(false);

                var url = this.AppSettings.SiteHost + "reset-password?token=" + System.Net.WebUtility.UrlEncode(userToken);

                string message = string.Format(
                    "<p>Your link: <a href='{0}'></a></p>" +
                    "<br />" +
                    "<p>Please do not reply this email.</p>", url);

                return this.Ok();
            }
            else
            {
                return this.BadRequest("User is not registered");
            }
        }
    }
}