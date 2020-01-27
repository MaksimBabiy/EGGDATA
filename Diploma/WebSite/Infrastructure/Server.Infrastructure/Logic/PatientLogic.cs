namespace Server.Infrastructure.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Server.DataBaseCore;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;
    using Server.Infrastructure.Interfaces;
    using Server.Infrastructure.Models;

    public class PatientLogic : /*EntityDataTypeLogic<Patient>*/ IPatientLogic
    {
        private readonly IEntitiesRepository repository;

        public PatientLogic(IEntitiesRepository repository)
         //   : base(ultraRepository)
        {
            this.repository = repository;
        }

        // public async Task<PatientViewModel> AddPatientAsync(PatientViewModel patientViewModel)
        // {
        //    try
        //    {
        //        Patient patient = await this.UltraRepository.AddPatientAsync(patientViewModel.ToEntity());

        // patientViewModel.ToModel(patient);

        // return patientViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        // }

        public async Task<PatientViewModel> AddPatientAsync(PatientViewModel patientViewModel)
        {
            try
            {
                Patient patient = await this.repository.AddPatientAsync(patientViewModel.ToEntity());

                patientViewModel.ToModel(patient);

                return patientViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PatientViewModel> UpdatePatientAsync(PatientViewModel patientViewModel)
        {
            try
            {
                Patient patient = new Patient();
                Debug.WriteLine($"1\n");
                patient = await this.repository.UpdatePatientAsync(patientViewModel.ToEntity());

                Debug.WriteLine($"{patient.PatientId}\n{patient.Email}\n{patient.FirstName}\n{patient.Condition}\n ");

                patientViewModel.ToModel(patient);

                return patientViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Patient>> GetPatientsListAsync()
        {
            return await this.repository.GetPatientsListAsync();
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            try
            {
                bool isPatientDeleted = await this.repository.DeletePatientAsync(patientId);

                return isPatientDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
