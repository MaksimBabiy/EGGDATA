namespace Server.Infrastructure.Models
{
    using System;
    using Server.DataBaseCore.Interfaces;

    public abstract class EntityDataTypeViewModel
    {
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; }

        public DateTime UpdatedDate { get; }

        //protected void ToEntity(IEntityDataType entityDataType)
        //{
        //    entityDataType = entityDataType ?? throw new ArgumentNullException(nameof(entityDataType));

        //    entityDataType.IsActive = this.IsActive;
        //}

        //protected void ToModel(IEntityDataType entityDataType)
        //{
        //    entityDataType = entityDataType ?? throw new ArgumentNullException(nameof(entityDataType));

        //    entityDataType.IsActive = this.IsActive.HasValue ? this.IsActive.Value : true;
        //}
    }
}
