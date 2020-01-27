namespace Server.Infrastructure.Interfaces
{
    public interface IEntityDataModel<T>
        where T : class, DataBaseCore.Interfaces.IEntityDataType
    {
        T ToEntity();

        void ToModel(T entity);
    }
}
