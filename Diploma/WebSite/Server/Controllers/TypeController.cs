namespace Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Server.DataBaseCore.Interfaces;
    using Server.Infrastructure.Interfaces;
    using Server.Infrastructure.Models;

    public class TypeController<TModel, TData> : Controller
        where TData : class, IEntityDataType
        where TModel : EntityDataTypeViewModel, IEntityDataModel<TData>, new()
    {
        public TypeController(IEntityDataTypeLogic<TData> entityDataType)
        {
            this.EntityDataType = entityDataType;
        }

        protected IEntityDataTypeLogic<TData> EntityDataType { get; private set; }

        public async Task<IActionResult> AddAsync([FromBody]TModel model)
        {
            try
            {
                return this.Ok(await this.EntityDataType.AddAsync(model));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> UpdateAsync([FromBody]TModel model)
        {
            try
            {
                var data = await this.EntityDataType.UpdateAsync(model);

                return this.Ok(data);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries[0].Reload();
                throw ex;
            }
        }
    }
}
