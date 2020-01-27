namespace Server.DataBaseCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Storage;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;

    public interface IUltraRepository
    {
        Task<IdentityResult> RegisterIdentityUserAsync(string email, string login, string password);

        Task<IdentityResult> RegisterApplicationUserAsync(string password, ApplicationUser user);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<ApplicationUser> FindUserByIdAsync(string userId);

        Task<string> GenerateTokenAsync(string userId);

        Task<ApplicationUser> FindUserByEmailAsync(string email);

        Task<ApplicationUser> FindUserByNameAsync(string name);

        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);

        Task<bool> RoleExistsAsync(string roleName);

        Task<IdentityResult> CreateRolesAsync(ApplicationRole applicationRole);

        Task<IdentityResult> AddUserDefaultRoleAsync(ApplicationUser applicationUser, string userRole);

        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);

        Task SignOutAsync();

        Task<int> SaveChangesAsync();

        Task<string> GenerateTokenAsync(ApplicationUser user);

        Task<List<Migration>> GetMigrationListAsync();

        Task<int> SaveMigrationAsync(string migrationId);

        Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntityDataType;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntityDataType;

        #region Transaction
        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region PatientData

        void GetPatientList(int patientId);

        Task<List<Patient>> GetPatientsListAsync();

        Task<Patient> AddPatientAsync(Patient patient);

        Task<Patient> UpdatePatientAsync(Patient updatePatient);

        Task<bool> DeletePatientAsync(int patientId);
        #endregion

        #region DoctorData

        Task<Doctor> AddDoctorAsync(Doctor doctor);

        Task<bool> DeleteDoctorAsync(int doctorId);
        #endregion
    }
}
