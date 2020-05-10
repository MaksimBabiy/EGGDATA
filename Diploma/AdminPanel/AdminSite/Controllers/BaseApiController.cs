namespace AdminSite.Controllers
{
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure;
    using AspNet.Security.OpenIdConnect.Primitives;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System.Linq;

    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        private readonly IAdminRepositoryDb adminRepositoryDb;

        private readonly IOptions<AppSettings> options;

        public BaseApiController(
        IAdminRepositoryDb repo,
        IOptions<AppSettings> options)
        {
            // Instantiate the required classes through DI
            this.adminRepositoryDb = repo;
            this.options = options;

            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            this.JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }

        protected JsonSerializerSettings JsonSettings { get; private set; }

        protected string UserId
        {
            get
            {
                return this.User.Claims.Where(us => us.Type == OpenIdConnectConstants.Claims.Subject).Select(us => us.Value).SingleOrDefault();
            }
        }
    }
}
