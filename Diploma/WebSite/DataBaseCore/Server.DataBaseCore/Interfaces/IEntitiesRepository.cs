namespace Server.DataBaseCore.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Server.DataBaseCore.Entities;

    public interface IEntitiesRepository
    {
        #region PatientData
        void GetPatientList(int patientId);

        Task<List<Patient>> GetPatientsListAsync();

        Task<Patient> AddPatientAsync(Patient patient);

        Task<Patient> UpdatePatientAsync(Patient updatePatient);

        Task<bool> DeletePatientAsync(int patientId);
        #endregion

        #region DoctorData
        Task<List<Doctor>> GetDoctorsListAsync();

        Task<Doctor> AddDoctorAsync(Doctor doctor);

        Task<Doctor> UpdateDoctorAsync(Doctor updateDoctor);

        Task<bool> DeleteDoctorAsync(int doctorId);
        #endregion
    }
}
