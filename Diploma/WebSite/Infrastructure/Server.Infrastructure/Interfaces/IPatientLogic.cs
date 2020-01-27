namespace Server.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Server.DataBaseCore.Entities;
    using Server.Infrastructure.Models;

    public interface IPatientLogic // : IEntityDataTypeLogic<Patient>
    {
        Task<PatientViewModel> AddPatientAsync(PatientViewModel patientViewModel);

        Task<PatientViewModel> UpdatePatientAsync(PatientViewModel patientViewModel);

        Task<List<Patient>> GetPatientsListAsync();

        Task<bool> DeletePatientAsync(int patientId);
    }
}
