namespace AdminSite
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AdminPanel.DataBaseCore;
    using AdminPanelDataBaseCore.Entities;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelDataBaseCore.Repository;
    using AdminPanelInfrastructure.Logic;
    using AspNet.Security.OpenIdConnect.Primitives;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Server.DataBaseCore;
    using Server.DataBaseCore.Interfaces;
    using Server.DataBaseCore.Repository;
    using Server.Infrastructure.Interfaces;
    using Server.Infrastructure.Logic;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "application/xhtml+xml",
                    "application/atom+xml",
                    "image/svg+xml",
                    "text/plain",
                    "text/json",
                    "ap­pli­ca­tion/json"
                });
            });

            services.AddDbContext<AdminDbContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(this.Configuration["ConnectionStrings:DefaultConnection"], o => o.MigrationsAssembly("AdminSite"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()

                // TODO: Update logical part to store users
                .AddUserManager<UserManager<ApplicationUser>>()
                // .AddSignInManager<ApplicationSignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<AdminDbContext>()
                .AddDefaultTokenProviders();

            // Add Authentication with JWT Tokens
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme =
               JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    // standard configuration
                    ValidIssuer = this.Configuration["Auth:Jwt:Issuer"],
                    ValidAudience = this.Configuration["Auth:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(this.Configuration["Auth:Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero,

                    //// security switches
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddScoped<IAdminRepositoryDb, AdminRepository>();
            services.AddScoped<IPatientLogic, PatientLogic>();
            services.AddScoped<IDoctorLogic, DoctorLogic>();
            services.AddScoped<IEntitiesRepository, EntitiesRepository>();

            services.AddMvc();

            // adding dbContext from server
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(this.Configuration["ConnectionStrings:WebSiteConnection"], o => o.MigrationsAssembly("Server"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            //services.AddIdentity<Server.DataBaseCore.Entities.ApplicationUser, Server.DataBaseCore.Entities.ApplicationRole>()
            //    .AddRoles<Server.DataBaseCore.Entities.ApplicationRole>()
            //    .AddRoleManager<RoleManager<Server.DataBaseCore.Entities.ApplicationRole>>()

            //    // TODO: Update logical part to store users
            //    .AddUserManager<UserManager<Server.DataBaseCore.Entities.ApplicationUser>>()
            //    .AddSignInManager<SignInManager<Server.DataBaseCore.Entities.ApplicationUser>>()
            //    .AddEntityFrameworkStores<AdminPanel.DataBaseCore.ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseResponseCompression();


            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            using (var serviceScopeDataType = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dataContext = serviceScopeDataType.ServiceProvider.GetService<AdminDbContext>();
                var repo = serviceScopeDataType.ServiceProvider.GetService<IAdminRepositoryDb>();
                try
                {
                    dataContext.Database.Migrate();
                    AdminPanelInfrastructure.Logic.DbSeeder.Seed(dataContext, repo);

                    //    int dataResult = accountingInitalizeData.InitalizeMetadateAccountingTypeAsync().Result;
                    //    loggerFactory.CreateLogger("DataBase").LogInformation($"Can`t initalize results with error {dataContext}");

                    //    // TODO: Refactor method
                    //    //int result = dataContext.InitalizeMetadateType().Result;
                    //    //loggerFactory.CreateLogger("DataBase").LogInformation($"Can`t result with error {result}");
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
