namespace AdminPanelInfrastructure.Interfaces
{
    using AdminPanelDataBaseCore.Interfaces;
    using System.Threading.Tasks;

    public interface IEntityDataTypeLogic<TData>
        where TData : class, IEntityDataType
    {
        Task<IEntityDataModel<TData>> AddAsync(IEntityDataModel<TData> model);

        Task<IEntityDataModel<TData>> UpdateAsync(IEntityDataModel<TData> model);
    }
}
