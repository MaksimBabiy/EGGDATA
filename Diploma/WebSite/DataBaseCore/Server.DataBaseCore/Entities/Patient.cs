namespace Server.DataBaseCore.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Patient : Person
    {
        [Key]
        public int PatientId { get; set; }
    }
}