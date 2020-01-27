namespace AdminPanelInfrastructure.Logic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AdminPanel.DataBaseCore;
    using AdminPanelDataBaseCore.Entities;
    using AdminPanelDataBaseCore.Interfaces;

    public class DbSeeder
    {
        public DbSeeder()
        {
        }

        public static void Seed(AdminDbContext dbContext, IAdminRepositoryDb repo)
        {
            if (!dbContext.Users.Any())
            {
                CreateAdminRoleAndUser(dbContext, repo)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public static async Task CreateAdminRoleAndUser(AdminDbContext dbContext, IAdminRepositoryDb repo)
        {
            ApplicationRole adminRole = new ApplicationRole { Id = "1", Name = "SystemAdministrator", NormalizedName = "SystemAdministrator" };

            if (!await repo.RoleExistsAsync(adminRole.Name))
            {
                await repo.CreateRolesAsync(adminRole);
            }

            ApplicationUser user_Admin = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "SystemAdministrator",
                Email = "admin@example.com",
                NormalizedUserName = "System Administrator"
            };

            if (await repo.FindUserByEmailAsync(user_Admin.Email) == null)
            {
                await repo.CreateUserAsync(user_Admin, "Diplo1!");
                await repo.AddUserDefaultRoleAsync(user_Admin, adminRole.Name);
                // Remove Lockout and E-Mail confirmation.
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
