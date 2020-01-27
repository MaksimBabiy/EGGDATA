namespace Server.DataBaseCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Server.DataBaseCore;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;

    public class UltraRepository : IUltraRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly RoleManager<ApplicationRole> roleManager;

        public UltraRepository(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        #region Selectors
        /*
        public static IEnumerable<TDataType> Where<TDataType>(this IEnumerable<TDataType> source, Func<TDataType, bool> predicate)
        {
            return Enumerable.Where(source, predicate);
        }

        public static IEnumerable<DataTypeObject> Select<TDataType, DataTypeObject>(this IEnumerable<TDataType> source, Func<TDataType, DataTypeObject> predicate)
        {
            return Enumerable.Select(source, predicate);
        }
        */
        #endregion

        #region UserManagemant
        public async Task<IdentityResult> RegisterIdentityUserAsync(string email, string login, string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = login,
                Email = email,
                EmailConfirmed = false
            };

            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
            }

                return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await this.signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterApplicationUserAsync(string password, ApplicationUser user)
        {
            try
            {
                return await this.userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await this.userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password)
        {
            IdentityResult identityResult = await this.userManager.CreateAsync(applicationUser, password);

            return identityResult;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            return await this.userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> FindUserByNameAsync(string name)
        {
            return await this.userManager.FindByNameAsync(name);
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await this.roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRolesAsync(ApplicationRole applicationRole)
        {
            return await this.roleManager.CreateAsync(applicationRole);
        }

        public async Task<IdentityResult> AddUserDefaultRoleAsync(ApplicationUser applicationUser, string userRole)
        {
            return await this.userManager.AddToRoleAsync(applicationUser, userRole);
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            user = user ?? throw new ArgumentNullException(nameof(user));

            return await this.userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateTokenAsync(string userId)
        {
            ApplicationUser applicationUser = await this.FindUserByIdAsync(userId);

            // return await this.GenerateTokenAsync(userId);
            return await this.GenerateTokenAsync(applicationUser);
        }
            #endregion

        #region Migration

        public async Task<List<Migration>> GetMigrationListAsync()
        {
            return await this.applicationDbContext.Migrations.ToListAsync();
        }

        public async Task<int> SaveMigrationAsync(string migrationId)
        {
            Migration migration = new Migration { MigrationKey = migrationId };
            this.applicationDbContext.Migrations.Add(migration);
            return await this.applicationDbContext.SaveChangesAsync();
        }
        #endregion

        #region Transactions

        public IDbContextTransaction BeginTransaction()
        {
            return this.applicationDbContext.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.applicationDbContext.BeginTransactionAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.applicationDbContext.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await this.applicationDbContext.SaveChangesAsync();
        }

        #region AddAsync

        // TODO: Refactor code
        public async Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntityDataType
        {
            entity = entity ?? throw new ArgumentNullException(nameof(entity));

            var dataEntity = this.applicationDbContext.Entry(entity);

            this.applicationDbContext.Add(entity);

            try
            {
                int dataSave = await this.applicationDbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries[0].Reload();
                throw;
            }
        }
        #endregion

        #region UpdateAsync
        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntityDataType
        {
            entity = entity ?? throw new ArgumentNullException(nameof(entity));

            var dataEntity = this.applicationDbContext.Entry(entity);

            this.applicationDbContext.Update(entity);

            try
            {
                int dataSave = await this.applicationDbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries[0].Reload();
                throw;
            }
        }
        #endregion

        #region PatientData

        public async Task<List<Patient>> GetPatientsListAsync()
        {
            return await this.applicationDbContext.Patients.ToListAsync();
        }

        public void GetPatientList(int patientId)
        {
            var patient = this.applicationDbContext.Patients;

            var getData = (from data in patient
                           where data.PatientId == patientId
                           orderby data.LastName, data.FirstName
                           select new
                           {
                               LastName = data.LastName,
                               FirstName = data.FirstName
                           }).ToList();

            if (!Equals(getData, null))
            {
                foreach (var itemQuery in getData)
                {
                    itemQuery.ToString();
                }
            }
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                try
                {
                    this.applicationDbContext.Patients.Add(patient);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return patient;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<Patient> UpdatePatientAsync(Patient updatePatient)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                try
                {
                    Patient patient = await this.applicationDbContext.Patients.FirstOrDefaultAsync(f => f.PatientId == updatePatient.PatientId);

                    patient.FirstName = updatePatient.FirstName;
                    patient.LastName = updatePatient.LastName;
                    patient.MiddleName = updatePatient.MiddleName;
                    patient.Age = updatePatient.Age;
                    patient.Weight = updatePatient.Weight;
                    patient.Height = updatePatient.Height;
                    patient.Sex = updatePatient.Sex;
                    patient.PhoneNumber = updatePatient.PhoneNumber;
                    patient.HomeNumber = updatePatient.HomeNumber;
                    patient.Email = updatePatient.Email;
                    patient.Condition = updatePatient.Condition;

                    this.applicationDbContext.Patients.Update(patient);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return patient;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            try
            {
                Patient patient = await this.applicationDbContext.Patients.FirstOrDefaultAsync(f => f.PatientId == patientId /*&& f.IsActive.GetValueOrDefault()*/);

                if (!Equals(patient, null))
                {
                    this.applicationDbContext.Patients.Remove(patient);
                }
                else
                {
                    throw new Exception($"Patient with {patientId} id not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await this.applicationDbContext.SaveChangesAsync() > 0;
        }
        #endregion

        #region DoctorData

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                try
                {
                    this.applicationDbContext.Doctors.Add(doctor);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return doctor;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            try
            {
                Doctor doctor = await this.applicationDbContext.Doctors.FirstOrDefaultAsync(f => f.DoctorId == doctorId /*&& f.IsActive.GetValueOrDefault()*/);

                if (!Equals(doctor, null))
                {
                    this.applicationDbContext.Doctors.Remove(doctor);
                }
                else
                {
                    throw new Exception($"Doctor with {doctorId} id not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await this.applicationDbContext.SaveChangesAsync() > 0;
        }
        #endregion

    }
}
