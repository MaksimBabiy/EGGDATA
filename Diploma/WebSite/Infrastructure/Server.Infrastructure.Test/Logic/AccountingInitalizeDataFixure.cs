//namespace Server.Infrastructure.Test.Logic
//{
//    using System.Collections.Generic;
//    using System.Threading.Tasks;
//    using Microsoft.AspNetCore.Identity;
//    using Moq;
//    using Server.DataBaseCore.Entities;
//    using Server.DataBaseCore.Interfaces;
//    using Server.DataBaseCore.Repository;
//    using Server.Infrastructure.Logic;
//    using Xunit;

//    public class AccountingInitalizeDataFixure
//    {
//        private readonly Mock<Server.DataBaseCore.Interfaces.IUltraRepository> ultraRepository;
//        private readonly Mock<Server.DataBaseCore.ApplicationDbContext> applicationDbContext;

//        public AccountingInitalizeDataFixure()
//        {
//            this.ultraRepository = new Mock<DataBaseCore.Interfaces.IUltraRepository>();
//            this.applicationDbContext = new Mock<DataBaseCore.ApplicationDbContext>();
//        }

//        [Fact]
//        public async Task InitalizeMetadateAccountingTypeAsync_SuccessInitalize()
//        {
//            var data = new DbSeeder(this.ultraRepository.Object, this.applicationDbContext.Object);
//            //IDbContextTransaction dbContextTransaction = this.ultraRepository.Setup(x => x.BeginTransactionAsync()).ReturnsAsync(IDbContextTransaction); 

//            List<Migration> migrations = new List<Migration>();

//            int updateMigration = 0;
//            this.ultraRepository.Setup(x => x.GetMigrationListAsync()).Returns(Task.FromResult(migrations));

//            var result = await data.InitalizeMetadateAccountingTypeAsync();
//        }

//        //public async Task<IdentityResult> CreateRoles()
//        //{
//        //    List<ApplicationRole> rolesList = new List<ApplicationRole>
//        //    {
//        //        // roles are adding here
//        //        new ApplicationRole { Id = "1", Name = "SystemAdministrator", NormalizedName = "SystemAdministrator" },
//        //        new ApplicationRole { Id = "2", Name = "Doctor", NormalizedName = "Doctor" },
//        //        new ApplicationRole { Id = "3", Name = "Patient", NormalizedName = "Patient" },
//        //        new ApplicationRole { Id = "4", Name = "Guest", NormalizedName = "Guest" }
//        //    };

//        //    IdentityResult identityResult = null;

//        //    foreach (ApplicationRole role in rolesList)
//        //    {
//        //        identityResult = await this.repository.CreateRolesAsync(role);
//        //    }

//        //    return identityResult;
//        //}

//        [Fact]
//        public async Task CreateRoles_SuccessCreating()
//        {
//            var data = new DbSeeder(this.ultraRepository.Object, this.applicationDbContext.Object);

//            IdentityResult identityResult = null;

//            List<ApplicationRole> rolesList = new List<ApplicationRole>
//            {
//                // roles are adding here
//                new ApplicationRole { Id = "1", Name = "SystemAdministrator", NormalizedName = "SystemAdministrator" },
//                new ApplicationRole { Id = "2", Name = "Doctor", NormalizedName = "Doctor" },
//                new ApplicationRole { Id = "3", Name = "Patient", NormalizedName = "Patient" },
//                new ApplicationRole { Id = "4", Name = "Guest", NormalizedName = "Guest" }
//            };

//            foreach (ApplicationRole role in rolesList)
//            {
//                this.ultraRepository.Setup(x => x.CreateRolesAsync(role)).Returns(Task.FromResult(identityResult));
//            }

//           // var result = await data.CreateRoles();

//           // Assert.Equal(result.ToString(), identityResult.ToString());
//        }

//        [Fact]
//        public void CreateDefaultAdminRole_SuccessCreating()
//        {
//            var data = new DbSeeder(this.ultraRepository.Object, this.applicationDbContext.Object);

//            ApplicationUser applicationUser = new ApplicationUser { UserName = "SystemAdministrator", Email = "admin@example.com", NormalizedUserName = "System Administrator" };

//            IdentityResult identityResult = null;

//            this.ultraRepository.Setup(x => x.CreateUserAsync(applicationUser, "PASSWORD")).Returns(Task.FromResult(identityResult));

//            this.ultraRepository.Setup(x => x.AddUserDefaultRoleAsync(applicationUser, applicationUser.UserName));

//            //var result = await data.CreateDefaultAdminRole();

//            //Assert.Equal(result.ToString(), identityResult.ToString());
//        }
//    }
//}
