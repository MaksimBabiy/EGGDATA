namespace Server.Infrastructure.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Storage;
    using Server.DataBaseCore;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;
    using Server.Infrastructure.Interfaces;

    public class DbSeeder
    {
        public DbSeeder()
        {
        }

        public static void Seed(ApplicationDbContext dbContext, IUltraRepository repo)
        {
            if (!dbContext.Users.Any())
            {
                CreateApplicationRolesAndAdminUser(dbContext, repo)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public static async Task CreateApplicationRolesAndAdminUser(ApplicationDbContext dbContext, IUltraRepository repo)
        {
            List<ApplicationRole> rolesList = new List<ApplicationRole>
            {
                // roles are adding here
                new ApplicationRole { Id = "1", Name = "SystemAdministrator", NormalizedName = "SystemAdministrator" },
                new ApplicationRole { Id = "2", Name = "Doctor", NormalizedName = "Doctor" },
                new ApplicationRole { Id = "3", Name = "Patient", NormalizedName = "Patient" }
            };
            foreach (ApplicationRole role in rolesList)
            {
                if (!await repo.RoleExistsAsync(role.Name))
                {
                    await repo.CreateRolesAsync(role);
                }
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
                await repo.AddUserDefaultRoleAsync(user_Admin, "SystemAdministrator");

                // Remove Lockout and E-Mail confirmation.
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
