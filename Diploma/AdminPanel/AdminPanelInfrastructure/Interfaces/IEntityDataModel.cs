namespace AdminPanelInfrastructure.Interfaces
{
    using AdminPanelDataBaseCore.Interfaces;
    public interface IEntityDataModel<T>
        where T : class, IEntityDataType
    {
        T ToEntity();

        void ToModel(T entity);
    }
}
