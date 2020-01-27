namespace Server.DataBaseCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Server.DataBaseCore.Entities;
    using Server.DataBaseCore.Interfaces;

    public class EntitiesRepository : IEntitiesRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EntitiesRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.applicationDbContext.BeginTransactionAsync(cancellationToken);
        }

        #region PatientData
        public async Task<List<Patient>> GetPatientsListAsync()
        {
            return await this.applicationDbContext.Patients.ToListAsync();
        }

        public void GetPatientList(int patientId)
        {
            var patient = this.applicationDbContext.Patients;

            var getData = (from data in patient
                           where data.PatientId == patientId
                           orderby data.LastName, data.FirstName
                           select new
                           {
                               LastName = data.LastName,
                               FirstName = data.FirstName
                           }).ToList();

            if (!Equals(getData, null))
            {
                foreach (var itemQuery in getData)
                {
                    itemQuery.ToString();
                }
            }
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                Patient pat = await this.applicationDbContext.Patients.FirstOrDefaultAsync(f => f.Email == patient.Email);
                if (!Equals(pat, null))
                {
                    return null;
                }

                try
                {
                    patient.CreatedDate = DateTime.Now;
                    patient.UpdatedDate = patient.CreatedDate;
                    this.applicationDbContext.Patients.Add(patient);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return patient;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<Patient> UpdatePatientAsync(Patient updatePatient)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                try
                {
                    Patient patient = await this.applicationDbContext.Patients.FirstOrDefaultAsync(f => f.Email == updatePatient.Email);

                    patient.FirstName = updatePatient.FirstName;
                    patient.LastName = updatePatient.LastName;
                    patient.MiddleName = updatePatient.MiddleName;
                    patient.Age = updatePatient.Age;
                    patient.Weight = updatePatient.Weight;
                    patient.Height = updatePatient.Height;
                    patient.Sex = updatePatient.Sex;
                    patient.PhoneNumber = updatePatient.PhoneNumber;
                    patient.HomeNumber = updatePatient.HomeNumber;
                    patient.Condition = updatePatient.Condition;
                    patient.UpdatedDate = DateTime.Now;

                    this.applicationDbContext.Patients.Update(patient);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return patient;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            try
            {
                Patient patient = await this.applicationDbContext.Patients.FirstOrDefaultAsync(f => f.PatientId == patientId /*&& f.IsActive.GetValueOrDefault()*/);

                if (!Equals(patient, null))
                {
                    this.applicationDbContext.Patients.Remove(patient);
                }
                else
                {
                    throw new Exception($"Patient with {patientId} id not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await this.applicationDbContext.SaveChangesAsync() > 0;
        }
        #endregion

        #region DoctorData
        public async Task<List<Doctor>> GetDoctorsListAsync()
        {
            return await this.applicationDbContext.Doctors.ToListAsync();
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                Doctor doc = await this.applicationDbContext.Doctors.FirstOrDefaultAsync(f => f.Email == doctor.Email);
                if (!Equals(doc, null))
                {
                    return null;
                }

                try
                {
                    doctor.CreatedDate = DateTime.Now;
                    doctor.UpdatedDate = doctor.CreatedDate;
                    this.applicationDbContext.Doctors.Add(doctor);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return doctor;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor updateDoctor)
        {
            using (IDbContextTransaction dbContextTransaction = await this.BeginTransactionAsync())
            {
                try
                {
                    Doctor doctor = await this.applicationDbContext.Doctors.FirstOrDefaultAsync(f => f.Email == updateDoctor.Email);

                    doctor.FirstName = updateDoctor.FirstName;
                    doctor.LastName = updateDoctor.LastName;
                    doctor.MiddleName = updateDoctor.MiddleName;
                    doctor.Age = updateDoctor.Age;
                    doctor.Weight = updateDoctor.Weight;
                    doctor.Height = updateDoctor.Height;
                    doctor.Sex = updateDoctor.Sex;
                    doctor.PhoneNumber = updateDoctor.PhoneNumber;
                    doctor.HomeNumber = updateDoctor.HomeNumber;
                    doctor.Condition = updateDoctor.Condition;
                    doctor.UpdatedDate = DateTime.Now;

                    this.applicationDbContext.Doctors.Update(doctor);

                    await this.applicationDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return doctor;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            try
            {
                Doctor doctor = await this.applicationDbContext.Doctors.FirstOrDefaultAsync(f => f.DoctorId == doctorId /*&& f.IsActive.GetValueOrDefault()*/);

                if (!Equals(doctor, null))
                {
                    this.applicationDbContext.Doctors.Remove(doctor);
                }
                else
                {
                    throw new Exception($"Doctor with {doctorId} id not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await this.applicationDbContext.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
