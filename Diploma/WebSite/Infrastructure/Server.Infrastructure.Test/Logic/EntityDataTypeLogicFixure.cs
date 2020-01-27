namespace Server.Infrastructure.Test.Logic
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using Server.DataBaseCore.Interfaces;
    using Server.Infrastructure.Interfaces;
    using Server.Infrastructure.Logic;
    using Xunit;

    public class EntityDataTypeLogicFixure
    {
        private Mock<IUltraRepository> ultraRepository;

        public EntityDataTypeLogicFixure()
        {
            this.ultraRepository = new Mock<IUltraRepository>();
        }

        [Fact]
        public async Task AddAsync_ReturnSuccessResult()
        {
            EntityDataTypeLogic<MockEntity> entityData = new EntityDataTypeLogic<MockEntity>(this.ultraRepository.Object);

            // Create mock model
            Mock<IEntityDataModel<MockEntity>> mockModel = new Mock<IEntityDataModel<MockEntity>>();

            // Create object MockEntity
            MockEntity mockEntity = new MockEntity();

            // Setup Moq
            mockModel.Setup(x => x.ToEntity())
                .Returns(mockEntity);

            this.ultraRepository.Setup(x => x.AddAsync(mockEntity))
                .Returns(Task.FromResult(mockEntity)).Verifiable();

            // Run Code
            var result = await entityData.AddAsync(mockModel.Object);

            // Validate true result
            Assert.Equal(mockModel.Object, result);
        }

        [Fact]
        public async Task AddAsync_ReturnParametrIsNull()
        {
            EntityDataTypeLogic<MockEntity> entityData = new EntityDataTypeLogic<MockEntity>(this.ultraRepository.Object);

            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await entityData.AddAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_ReturnSuccessResult()
        {
            EntityDataTypeLogic<MockEntity> entityData = new EntityDataTypeLogic<MockEntity>(this.ultraRepository.Object);

            // Create mock model
            Mock<IEntityDataModel<MockEntity>> mockModel = new Mock<IEntityDataModel<MockEntity>>();

            // Create object MockEntity
            MockEntity mockEntity = new MockEntity();

            // Setup Moq
            mockModel.Setup(x => x.ToEntity())
                .Returns(mockEntity);

            this.ultraRepository.Setup(x => x.UpdateAsync(mockEntity))
                .Returns(Task.FromResult(mockEntity)).Verifiable();

            // Run Code
            var result = await entityData.UpdateAsync(mockModel.Object);

            // Validate true result
            Assert.Equal(mockModel.Object, result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnParametrIsNull()
        {
            EntityDataTypeLogic<MockEntity> entityData = new EntityDataTypeLogic<MockEntity>(this.ultraRepository.Object);

            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await entityData.UpdateAsync(null));
        }
    }
}
