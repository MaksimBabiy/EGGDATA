namespace Server.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Server.DataBaseCore.Entities;
    using Server.Infrastructure.Models;

    public interface IDoctorLogic //: IEntityDataTypeLogic<Doctor>
    {
        Task<DoctorViewModel> AddDoctorAsync(DoctorViewModel doctorViewModel);

        Task<DoctorViewModel> UpdateDoctorAsync(DoctorViewModel doctorViewModel);

        Task<bool> DeleteDoctorAsync(int doctorId);

        Task<List<Doctor>> GetDoctorsListAsync();
    }
}
