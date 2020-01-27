namespace AdminPanelInfrastructure.Logic
{
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class EntityDataTypeLogic<TData> : IEntityDataTypeLogic<TData>
        where TData : class, IEntityDataType
    {
        public EntityDataTypeLogic(IAdminRepositoryDb repo)
        {
            this.Repository = repo;
        }

        protected IAdminRepositoryDb Repository { get; private set; }

        public async Task<IEntityDataModel<TData>> AddAsync(IEntityDataModel<TData> model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            TData data = model.ToEntity();
            try
            {
                //model.ToModel(await this.Repository.AddAsync(data));

                return model;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                model.ToModel(ex.Entries[0].Entity as TData);
                throw ex;
            }
        }

        public async Task<IEntityDataModel<TData>> UpdateAsync(IEntityDataModel<TData> model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            TData data = model.ToEntity();

            try
            {
               // model.ToModel(await this.Repository.UpdateAsync(data));

                return model;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                model.ToModel(ex.Entries[0].Entity as TData);
                throw ex;
            }
        }
    }
}
