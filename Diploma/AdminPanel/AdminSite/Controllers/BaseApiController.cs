namespace AdminSite.Controllers
{
    using AdminPanel.DataBaseCore;
    using AdminPanelDataBaseCore.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        public BaseApiController(
        AdminDbContext context,
        IAdminRepositoryDb repo,
        IConfiguration configuration)
        {
            // Instantiate the required classes through DI
            this.Repository = repo;
            this.Configuration = configuration;
            this.DbContext = context;

            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            this.JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }

        protected AdminDbContext DbContext { get; private set; }

        protected IAdminRepositoryDb Repository { get; private set; }

        protected IConfiguration Configuration { get; private set; }

        protected JsonSerializerSettings JsonSettings { get; private set; }
    }
}
