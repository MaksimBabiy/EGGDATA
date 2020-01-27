namespace Server.Infrastructure.Logic
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Server.DataBaseCore.Interfaces;
    using Server.DataBaseCore.Repository;
    using Server.Infrastructure.Interfaces;

    public class EntityDataTypeLogic<TData> : IEntityDataTypeLogic<TData>
        where TData : class, IEntityDataType
    {
        public EntityDataTypeLogic(IUltraRepository ultraRepository)
        {
            this.UltraRepository = ultraRepository;
        }

        protected IUltraRepository UltraRepository { get; private set; }

        public async Task<IEntityDataModel<TData>> AddAsync(IEntityDataModel<TData> model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            TData data = model.ToEntity();
            try
            {
                model.ToModel(await this.UltraRepository.AddAsync(data));

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
                model.ToModel(await this.UltraRepository.UpdateAsync(data));

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
