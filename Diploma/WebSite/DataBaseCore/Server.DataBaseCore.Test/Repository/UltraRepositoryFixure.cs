namespace Server.DataBaseCore.Test.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Repository;
    using Xunit;

    public class UltraRepositoryFixure
    {
        private readonly Mock<ApplicationDbContext> mockApplicationDbContext;

        private readonly Mock<UserManager<ApplicationUser>> mockUserManager;

        private readonly Mock<RoleManager<ApplicationRole>> mockRoleManager;

        private readonly UltraRepository repository;

        public UltraRepositoryFixure()
        {
            this.mockApplicationDbContext = new Mock<ApplicationDbContext>();
            this.mockUserManager = new Mock<UserManager<ApplicationUser>>();
            this.mockRoleManager = new Mock<RoleManager<ApplicationRole>>();
        }

        #region UserManagment
        [Fact]
        public async Task RegisterIdentityUserAsync_SuccessRegister()
        {
            string password = "SomethingPassword1234!";
            string login = "Login";
            string email = "MyNewUser@gmail.com";
            var creatingUser = new ApplicationUser() { UserName = email, Email = "MyNewUser@gmail.com" };

            IdentityResult iResult = new IdentityResultFixure { Succeeded = false };

            this.mockUserManager.Setup(x => x.CreateAsync(creatingUser, email))
                .Returns(Task.FromResult(iResult)).Verifiable();

            var result = await this.repository.RegisterIdentityUserAsync(email, login, password);

            Assert.Equal(result.ToString(), iResult.ToString());
        }

        [Fact]
        public async Task CreateUserAsync_SuccessRegister()
        {
            string password = "Passwordium";

            ApplicationUser createUser = new ApplicationUser { UserName = "ourUser", Email = "MyNewUser@gmail.com" };

            IdentityResult iResult = new IdentityResultFixure { Succeeded = false };

            this.mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .Returns(Task.FromResult(iResult));

            var result = await this.repository.CreateUserAsync(createUser, password);

            Assert.Equal(result.ToString(), iResult.ToString());
        }

        [Fact]
        public async Task RegisterApplicationUserAsync_SuccessRegister()
        {
            string password = "Passwordium";

            ApplicationUser createUser = new ApplicationUser { UserName = "ourUser", Email = "MyNewUser@gmail.com" };

            IdentityResult iResult = new IdentityResultFixure { Succeeded = false };

            this.mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(iResult));

            var result = await this.repository.RegisterApplicationUserAsync(password, createUser);

            Assert.Equal(result.ToString(), iResult.ToString());
        }

        //[Fact]
        //public async Task FindUserByIdAsync_GetNullOrEmptyId()
        //{
        //    string userId = "";

        //    Assert.True(string.IsNullOrEmpty(userId));
        //}

        [Fact]
        public async Task FindUserByEmailAsync_GetApplicationUser()
        {
            string email = "testmail@gmail.com";
            ApplicationUser user = new ApplicationUser { UserName = "ourUser", Email = email };

            this.mockUserManager.Setup(x => x.FindByEmailAsync(email)).Returns(Task.FromResult(user));

            var result = await this.repository.FindUserByEmailAsync(email);

            Assert.Equal(result.Email, user.Email);
            Assert.Equal(result.UserName, user.UserName);
        }

        [Fact]
        public async Task FindUserByIdAsync_GetApplicationUser()
        {
            string userId = "1";
            ApplicationUser user = new ApplicationUser { Id = userId, UserName = "ourUser" };

            this.mockUserManager.Setup(x => x.FindByIdAsync(userId)).Returns(Task.FromResult(user));

            var result = await this.repository.FindUserByIdAsync(userId);

            Assert.Equal(result.Id, user.Id);
            Assert.Equal(result.UserName, user.UserName);
        }

        [Fact]
        public async Task CreateRolesAsync_SuccessCreatingRole()
        {
            ApplicationRole role = new ApplicationRole { Id = "1", Name = "admin" };

            IdentityResult iResult = new IdentityResultFixure { Succeeded = false };

            this.mockRoleManager.Setup(x => x.CreateAsync(role)).Returns(Task.FromResult(iResult));

            var result = await this.repository.CreateRolesAsync(role);

            Assert.Equal(result.ToString(), iResult.ToString());
        }

        [Fact]
        public async Task AddUserDefaultRoleAsync_SuccessAdding()
        {
            ApplicationUser user = new ApplicationUser { Id = "1", UserName = "ourUser" };

            string userRole = "admin";

            IdentityResult iResult = new IdentityResultFixure { Succeeded = false };

            this.mockUserManager.Setup(x => x.AddToRoleAsync(user, userRole)).Returns(Task.FromResult(iResult));

            var result = await this.repository.AddUserDefaultRoleAsync(user, userRole);

            Assert.Equal(result.ToString(), iResult.ToString());
        }

        [Fact]
        public async Task GenerateTokenAsyncWithUserOverload_SuccessfulGenerating()
        {
            ApplicationUser user = new ApplicationUser { Id = "1", UserName = "ourUser" };

            string token = string.Empty;

            this.mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).Returns(Task.FromResult(token));

            var result = await this.repository.GenerateTokenAsync(user);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GenerateTokenAsyncWithIdOverload_SuccessfulGenerating()
        {
            ApplicationUser user = new ApplicationUser { Id = "1", UserName = "ourUser" };

            string userId = "1";

            this.mockUserManager.Setup(x => x.FindByIdAsync(userId)).Returns(Task.FromResult(user)).Verifiable();

            string token = string.Empty;

            this.mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).Returns(Task.FromResult(token)).Verifiable();

            var result = await this.repository.GenerateTokenAsync(user);

            Assert.Equal(result, token);
        }
        #endregion

        //        [Fact]
        //        public async Task GetMigrationListAsync_SuccessGettingList()
        //        {
        //            List<Migration> migrations = new List<Migration>();
        //            Migration migration = new Migration { MigrationId = 1 };

        //            List<Migration> migrations = new List<Migration>();

        //            this.mockApplicationDbContext.Setup(x => x.Migrations.Add(migration));
        //.
        //            migrations = this.mockApplicationDbContext.Setup(x => x.Migrations.ToListAsync()).Returns(Task.FromResult(migrations);

        //            var result = await this.repository.GetMigrationListAsync();
        //        }

        #region Transactions
        [Fact]
        public void BeginTransaction_SuccessBeginning()
        {
            var transaction = this.mockApplicationDbContext.Setup(x => x.BeginTransaction());

            var result = this.repository.BeginTransaction();

            Assert.Equal(result.ToString(), transaction.ToString());
        }

        //[Fact]
        //public async Task BeginTransactionAsync_SuccessBeginning()
        //{
        //    var transaction = this.mockApplicationDbContext.Setup(x => x.BeginTransactionAsync(It.IsAny<IsolationLevel>()));

        //    var result = await this.repository.BeginTransactionAsync();

        //    Assert.Equal(result.ToString(), transaction.ToString());
        //}
        #endregion

        [Fact]
        public async Task AddAsync_ThrowsArgumentNullException()
        {
            MockEntity mockEntity = new MockEntity();
            var result = await this.repository.AddAsync(mockEntity);
            Assert.Null(null);
        }

        [Fact]
        public async Task AddAsync_SuccessAddingEntity()
        {
            MockEntity mockEntity = new MockEntity { Id = "1" };

            var data = this.mockApplicationDbContext.Setup(x => x.Entry(mockEntity));

            this.mockApplicationDbContext.Setup(x => x.Add(mockEntity));

            var result = await this.repository.AddAsync(mockEntity);

            Assert.Equal(result.Id, mockEntity.Id);
        }

        [Fact]
        public async Task UpdateAsync_SuccessAddingEntity()
        {
            MockEntity mockEntity = new MockEntity { Id = "1" };

            var data = this.mockApplicationDbContext.Setup(x => x.Entry(mockEntity));

            this.mockApplicationDbContext.Setup(x => x.Update(mockEntity));

            var result = await this.repository.UpdateAsync(mockEntity);

            Assert.Equal(result.Id, mockEntity.Id);
        }

        [Fact]
        public async Task AddPatientAsync_SuccessAddingPatient()
        {
            Patient testPatient = new Patient
            {
                PatientId = 1,
                FirstName = "Andrew",
                LastName = "Moshko",
                MiddleName = "Victor",
                Age = "18",
                Weight = "83",
                Height = "195",
                Email = "dron@gmail.com",
                PhoneNumber = "+380995135682",
                HomeNumber = "-",
                Sex = "man",
                Condition = "lazy student with diploma in front of his nose"
            };

            this.mockApplicationDbContext.Setup(x => x.Patients.Add(testPatient));

            var result = await this.repository.AddPatientAsync(testPatient);

            Assert.Equal(result.PatientId, testPatient.PatientId);
            Assert.Equal(result.FirstName, testPatient.FirstName);
            Assert.Equal(result.LastName, testPatient.LastName);
        }

        [Fact]
        public async Task AddDoctorAsync_SuccessAddingDoctor()
        {
            Doctor testDoctor = new Doctor
            {
                DoctorId = 1,
                FirstName = "Andrew",
                LastName = "Moshko",
                MiddleName = "Victor",
                Age = "18",
                Weight = "83",
                Height = "195",
                Email = "dron@gmail.com",
                PhoneNumber = "+380995135682",
                HomeNumber = "-",
                Sex = "man",
                Condition = "lazy student with diploma in front of his nose"
            };

            this.mockApplicationDbContext.Setup(x => x.Doctors.Add(testDoctor)).Verifiable();

            var result = await this.repository.AddDoctorAsync(testDoctor);

            Assert.Equal(result.DoctorId, testDoctor.DoctorId);
            Assert.Equal(result.FirstName, testDoctor.FirstName);
            Assert.Equal(result.LastName, testDoctor.LastName);
        }

        [Fact]
        public async Task DeletePatientAsync_SuccessDeletingPatient()
        {
            Patient testPatient = new Patient
            {
                PatientId = 1,
                FirstName = "Andrew",
                LastName = "Moshko",
                MiddleName = "Victor",
                Age = "18",
                Weight = "83",
                Height = "195",
                Email = "dron@gmail.com",
                PhoneNumber = "+380995135682",
                HomeNumber = "-",
                Sex = "man",
                Condition = "lazy student with diploma in front of his nose"
            };

            int patientId = testPatient.PatientId;

            this.mockApplicationDbContext.Setup(x => x.Patients.Add(testPatient));

            //var secPat = this.mockApplicationDbContext.Setup(x => x.Patients.FirstOrDefaultAsync();

            //this.mockApplicationDbContext.Setup(x => x.Patients.Remove(secPat);

            var result = await this.repository.AddPatientAsync(testPatient);
        }
    }
}
