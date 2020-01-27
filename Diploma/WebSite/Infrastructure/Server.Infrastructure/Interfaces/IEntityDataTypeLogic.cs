namespace Server.Infrastructure.Interfaces
{
    using System.Threading.Tasks;
    using Server.DataBaseCore.Interfaces;

    public interface IEntityDataTypeLogic<TData>
        where TData : class, IEntityDataType
    {
        Task<IEntityDataModel<TData>> AddAsync(IEntityDataModel<TData> model);

        Task<IEntityDataModel<TData>> UpdateAsync(IEntityDataModel<TData> model);
    }
}
