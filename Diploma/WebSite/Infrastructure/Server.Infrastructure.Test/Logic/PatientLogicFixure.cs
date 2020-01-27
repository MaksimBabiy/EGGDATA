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

    public class PatientLogicFixure
    {
        private readonly Mock<IUltraRepository> ultraRepository;
        private readonly Mock<IEntitiesRepository> patRepo;

        public PatientLogicFixure()
        {
            this.ultraRepository = new Mock<IUltraRepository>();
            this.patRepo = new Mock<IEntitiesRepository>();
        }

        [Fact]
        public async Task AddPatient_SuccessResult()
        {
            PatientLogic patientData = new PatientLogic(this.patRepo.Object);

            // Create model
            PatientViewModel model = new PatientViewModel()
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
                Condition = "test Patient"
            };

            // Create object Doctor
            Patient patient = model.ToEntity();
            this.ultraRepository.Setup(x => x.AddPatientAsync(patient))
                .ReturnsAsync(patient).Verifiable();

            // Run Code
            var result = await patientData.AddPatientAsync(model);

            // validate true result
            Assert.Equal(result.PatientId, model.PatientId);
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
        }

        [Fact]
        public async Task AddPatient_ExceptionResult()
        {
            PatientLogic patientLogic = new PatientLogic(this.patRepo.Object);

            PatientViewModel model = new PatientViewModel();

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await patientLogic.AddPatientAsync(model);
            });
        }

        [Fact]
        public async Task DeletePatient_SuccessResult()
        {
            PatientLogic patientData = new PatientLogic(this.patRepo.Object);

            // Create model
            PatientViewModel model = new PatientViewModel()
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
                Condition = "test Patient"
            };

            // Create object Doctor
            Patient patient = model.ToEntity();
            this.ultraRepository.Setup(x => x.AddPatientAsync(patient))
                .ReturnsAsync(patient).Verifiable();

            this.ultraRepository.Setup(x => x.DeletePatientAsync(patient.PatientId))
                .ReturnsAsync(true).Verifiable();

            // Run Code
            var result = await patientData.DeletePatientAsync(model.PatientId); // AddAsync(mockModel.Object);

            Assert.True(result);
        }

        [Fact]
        public async Task DeletePatient_ExceptionResult()
        {
            PatientLogic doctorLogic = new PatientLogic(this.patRepo.Object);

            int? id = null;

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await doctorLogic.DeletePatientAsync((int)id);
            });
        }
    }
}