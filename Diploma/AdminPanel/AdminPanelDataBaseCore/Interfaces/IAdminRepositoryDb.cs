namespace AdminPanelDataBaseCore.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Storage;

    public interface IAdminRepositoryDb
    {
        Task<bool> RoleExistsAsync(string roleName);

        Task<IdentityResult> CreateRolesAsync(ApplicationRole applicationRole);

        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);

        Task<ApplicationUser> FindUserByIdAsync(string userId);

        Task<ApplicationUser> FindUserByNameAsync(string name);

        Task<ApplicationUser> FindUserByEmailAsync(string email);

        Task<IdentityResult> AddUserDefaultRoleAsync(ApplicationUser applicationUser, string userRole);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);

        Task<FileStorage> GetFileAsync(string fileBlobName);

        Task<List<FileStorage>> GetAllFilesAsync();

        Task<bool> DeleteFileAsync(string fileBlobName, string userId);

        Task<(List<FileStorage>, int)> GetFileStoragesAsync(string userId, int index, int count);

        Task LogOut();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<List<Server.DataBaseCore.Entities.Patient>> GetPatientsListAsync();

        Task<Server.DataBaseCore.Entities.Patient> UpdatePatientAsync(Server.DataBaseCore.Entities.Patient updatePatient);

        //Task<SignInResult> Login(LoginViewModel model);
    }
}
