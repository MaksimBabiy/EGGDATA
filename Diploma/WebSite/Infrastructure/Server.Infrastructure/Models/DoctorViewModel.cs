namespace Server.Infrastructure.Models
{
    using System;
    using Server.DataBaseCore.Entities;

    public class DoctorViewModel : PersonViewModel//, IEntityDataModel<Doctor>
    {
        public int DoctorId { get; set; }

        public string Text { get; set; }

        public Doctor ToEntity()
        {
            var doctor = new Doctor
            {
                DoctorId = this.DoctorId,
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
                Text = this.Text,
                IsActive = true
            };

            //this.ToEntity(doctor);

            return doctor;
        }

        public void ToModel(Doctor entity)
        {
            if (!Equals(entity, null))
            {
                this.DoctorId = entity.DoctorId;
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
                this.Text = entity.Text;
                //this.ToModel((EntityDataType)entity);
                //this.IsActive = entity.IsActive;
            }
        }
    }
}
