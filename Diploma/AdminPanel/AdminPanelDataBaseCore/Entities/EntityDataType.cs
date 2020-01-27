namespace AdminPanelDataBaseCore.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using AdminPanelDataBaseCore.Interfaces;

    public abstract class EntityDataType : IEntityDataType
    {
        [Required]
        public bool? IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedDate { get; set; }

        public void ToModelEntity(EntityDataType entityDataType)
        {
            this.IsActive = entityDataType.IsActive;
            this.CreatedDate = entityDataType.CreatedDate;
            this.UpdatedDate = entityDataType.UpdatedDate;
        }
    }
}
