namespace Server.Infrastructure.Test.Logic
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;
    using Server.Infrastructure.Logic;
    using Server.Infrastructure.Models;
    using Xunit;

    public class DoctorLogicFixure
    {
        private readonly Mock<IEntitiesRepository> repository;

        public DoctorLogicFixure()
        {
            this.repository = new Mock<IEntitiesRepository>();
        }

        [Fact]
        public async Task AddDoctor_ExceptionResult()
        {
            DoctorLogic doctorLogic = new DoctorLogic(this.repository.Object);

            DoctorViewModel model = new DoctorViewModel();

            await Assert.ThrowsAsync<Exception>(async () =>
             {
                 var result = await doctorLogic.AddDoctorAsync(model);
             });
        }

        [Fact]
        public async Task AddDoctor_SuccessResult()
        {
            DoctorLogic doctorData = new DoctorLogic(this.repository.Object);

            // Create model
            DoctorViewModel model = new DoctorViewModel()
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
                Condition = "test Doctor",
                Text = "default string"
            };

            // Create object Doctor
            Doctor doctor = model.ToEntity();

            // this.ultraRepository.Setup(x => x.AddDoctorAsync(model.ToEntity()))
            //    .Returns(Task.FromResult(doctor)).Verifiable();
            this.repository.Setup(x => x.AddDoctorAsync(doctor))
                .ReturnsAsync(doctor).Verifiable();

            // Run Code
            var result = await doctorData.AddDoctorAsync(model); // AddAsync(mockModel.Object);

            // validate true result
            Assert.Equal(result.DoctorId, model.DoctorId);
            Assert.Equal(result.FirstName, model.FirstName);
            Assert.Equal(result.LastName, model.LastName);
            Assert.Equal(result.MiddleName, model.MiddleName);
            Assert.Equal(result.Age, model.Age);
            Assert.Equal(result.Weight, model.Weight);
            Assert.Equal(result.Height, model.Height);
            Assert.Equal(result.Email, model.Email);
            Assert.Equal(result.PhoneNumber, model.PhoneNumber);
            Assert.Equal(result.HomeNumber, model.HomeNumber);
            Assert.Equal(result.Sex, model.Sex);
            Assert.Equal(result.Condition, model.Condition);
            Assert.Equal(result.Text, model.Text);
        }

        [Fact]
        public async Task DeleteDoctor_ExceptionResult()
        {
            DoctorLogic doctorLogic = new DoctorLogic(this.repository.Object);

            int? id = null;

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await doctorLogic.DeleteDoctorAsync((int)id);
            });
        }

        [Fact]
        public async Task DeleteDoctor_SuccessResult()
        {
            DoctorLogic doctorData = new DoctorLogic(this.repository.Object);

            // Create model
            DoctorViewModel model = new DoctorViewModel()
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
                Condition = "test Doctor",
                Text = "default string"
            };

            // Create object Doctor
            Doctor doctor = model.ToEntity();

            // this.ultraRepository.Setup(x => x.AddDoctorAsync(model.ToEntity()))
            //    .Returns(Task.FromResult(doctor)).Verifiable();
            this.repository.Setup(x => x.AddDoctorAsync(doctor))
                .ReturnsAsync(doctor).Verifiable();

            this.repository.Setup(x => x.DeleteDoctorAsync(doctor.DoctorId))
                .ReturnsAsync(true).Verifiable();


            // Run Code
            var result = await doctorData.DeleteDoctorAsync(model.DoctorId); // AddAsync(mockModel.Object);

            Assert.True(result);
        }
    }
}
