namespace Server.DataBaseCore.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Doctor : Person
    {
        [Key]
        public int DoctorId { get; set; }

        [StringLength(450)]
        public string Text { get; set; }
    }
}
