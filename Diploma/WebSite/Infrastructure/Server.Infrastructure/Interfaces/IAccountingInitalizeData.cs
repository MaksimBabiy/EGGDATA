namespace Server.Infrastructure.Interfaces
{
    using System.Threading.Tasks;

    public interface IAccountingInitalizeData
    {
        Task<int> InitalizeMetadateAccountingTypeAsync();
    }
}
