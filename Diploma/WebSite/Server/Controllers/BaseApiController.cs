namespace Server.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Server.DataBaseCore;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;

    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        public BaseApiController(
        ApplicationDbContext context,
        IUltraRepository repo,
        IConfiguration configuration)
        {
            // Instantiate the required classes through DI
            this.DbContext = context;
            this.Repository = repo;
            this.Configuration = configuration;

            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            this.JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }

        protected ApplicationDbContext DbContext { get; private set; }

        protected IUltraRepository Repository { get; private set; }

        protected RoleManager<ApplicationRole> RoleManager { get; private set; }

        protected UserManager<ApplicationUser> UserManager { get; private set; }

        protected IConfiguration Configuration { get; private set; }

        protected JsonSerializerSettings JsonSettings { get; private set; }
    }
}
