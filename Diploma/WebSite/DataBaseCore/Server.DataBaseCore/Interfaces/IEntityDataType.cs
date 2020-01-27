namespace Server.DataBaseCore.Interfaces
{
    using System;

    public interface IEntityDataType
    {
        bool IsActive { get; set; }

        DateTime CreatedDate { get; }

        DateTime? UpdatedDate { get; set; }
    }
}
