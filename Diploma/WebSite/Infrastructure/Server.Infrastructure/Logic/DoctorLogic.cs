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

    public class DoctorLogic : IDoctorLogic // EntityDataTypeLogic<Doctor>
    {
        private readonly IEntitiesRepository repo;

        public DoctorLogic(IEntitiesRepository repo)
            //: base(ultraRepository)
        {
            this.repo = repo;
        }

        public async Task<DoctorViewModel> AddDoctorAsync(DoctorViewModel doctorViewModel)
        {
            try
            {
                Doctor doctor = await this.repo.AddDoctorAsync(doctorViewModel.ToEntity());

                doctorViewModel.ToModel(doctor);

                return doctorViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DoctorViewModel> UpdateDoctorAsync(DoctorViewModel doctorViewModel)
        {
            try
            {
                Doctor doctor = new Doctor();
                doctor = await this.repo.UpdateDoctorAsync(doctorViewModel.ToEntity());

                doctorViewModel.ToModel(doctor);

                return doctorViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            try
            {
                bool isDoctorDeleted = await this.repo.DeleteDoctorAsync(doctorId);

                return isDoctorDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Doctor>> GetDoctorsListAsync()
        {
            return await this.repo.GetDoctorsListAsync();
        }
    }
}
