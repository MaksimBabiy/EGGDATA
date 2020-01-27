namespace Server.DataBaseCore.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Migration
    {
        [Key]
        public int MigrationId { get; set; }
         
        [StringLength(450)]
        public string MigrationKey { get; set; }
    }
}
