namespace Server.Infrastructure.Models
{
    using Server.DataBaseCore.Entities;
    using Server.Infrastructure.Interfaces;

    public class PatientViewModel : PersonViewModel // , IEntityDataModel<Patient>
    {
        public int PatientId { get; set; }

        public Patient ToEntity()
        {
            var patient = new Patient
            {
                PatientId = this.PatientId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                Age = this.Age,
                Weight = this.Weight,
                Height = this.Height,
                Sex = this.Sex,
                PhoneNumber = this.PhoneNumber,
                HomeNumber = this.HomeNumber,
                Email = this.Email,
                Condition = this.Condition,
                IsActive = true
            };

            //this.ToEntity(patient);

            return patient;
        }

        public void ToModel(Patient entity)
        {
            if (!Equals(entity, null))
            {
                this.PatientId = entity.PatientId;
                this.FirstName = entity.FirstName;
                this.LastName = entity.LastName;
                this.MiddleName = entity.MiddleName;
                this.Age = entity.Age;
                this.Weight = entity.Weight;
                this.Height = entity.Height;
                this.Sex = entity.Sex;
                this.PhoneNumber = entity.PhoneNumber;
                this.HomeNumber = entity.HomeNumber;
                this.Email = entity.Email;
                this.Condition = entity.Condition;

                //this.ToModel((EntityDataType)entity);
                //this.IsActive = entity.IsActive;
            }
        }
    }
}