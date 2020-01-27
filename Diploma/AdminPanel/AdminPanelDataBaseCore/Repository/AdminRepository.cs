namespace AdminPanelDataBaseCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AdminPanel.DataBaseCore;
    using AdminPanelDataBaseCore.Entities;
    using AdminPanelDataBaseCore.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Storage;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;

    public class AdminRepository : IAdminRepositoryDb
    {
        private readonly IUltraRepository ultraRepository;

        private readonly SignInManager<Entities.ApplicationUser> signInManager;

        private readonly UserManager<Entities.ApplicationUser> userManager;

        private readonly RoleManager<Entities.ApplicationRole> roleManager;

        private readonly AdminDbContext applicationDbContext;

        private readonly IUltraRepository repos;

        public AdminRepository(AdminDbContext applicationDbContext, SignInManager<Entities.ApplicationUser> signInManager, UserManager<Entities.ApplicationUser> userManager, RoleManager<Entities.ApplicationRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IdentityResult> AddUserDefaultRoleAsync(Entities.ApplicationUser applicationUser, string userRole)
        {
            return await this.userManager.AddToRoleAsync(applicationUser, userRole);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await this.roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRolesAsync(Entities.ApplicationRole applicationRole)
        {
            return await this.roleManager.CreateAsync(applicationRole);
        }

        public async Task<Entities.ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<Entities.ApplicationUser> FindUserByNameAsync(string name)
        {
            return await this.userManager.FindByNameAsync(name);
        }

        public async Task<IdentityResult> CreateUserAsync(Entities.ApplicationUser applicationUser, string password)
        {
            IdentityResult identityResult = await this.userManager.CreateAsync(applicationUser, password);

            return identityResult;
        }

        public async Task<Entities.ApplicationUser> FindUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            return await this.userManager.FindByIdAsync(userId);
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await this.signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        }

        public async Task<bool> CheckPasswordAsync(Entities.ApplicationUser user, string password)
        {
            return await this.userManager.CheckPasswordAsync(user, password);
        }

        //public async Task<SignInResult> Login(LoginViewModel model)
        //{
        //    return await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //}

        public async Task LogOut()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.applicationDbContext.BeginTransactionAsync(cancellationToken);
        }

        public async Task<List<Server.DataBaseCore.Entities.Patient>> GetPatientsListAsync()
        {
            return await this.repos.GetPatientsListAsync();
        }

        public async Task<Server.DataBaseCore.Entities.Patient> UpdatePatientAsync(Server.DataBaseCore.Entities.Patient updatePatient)
        {
            return await this.repos.UpdatePatientAsync(updatePatient);
        }
    }
}
