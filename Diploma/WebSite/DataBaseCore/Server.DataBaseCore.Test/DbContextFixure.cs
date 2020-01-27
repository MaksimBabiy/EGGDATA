namespace Server.DataBaseCore.Test
{
    using System.Threading.Tasks;
    using Xunit;

    public class DbContextFixure
    {
        private readonly ApplicationDbContext dbContext;

        [Fact]
        public async Task InitalizeMetadateType_SuccessInitalize()
        {
            int count = 0;
            var dbTransaction = await this.dbContext.BeginTransactionAsync();

            dbTransaction.Commit();

            //var result = await this.dbContext.InitalizeMetadateType();

            //Assert.Equal(result, count);
        }
    }
}
