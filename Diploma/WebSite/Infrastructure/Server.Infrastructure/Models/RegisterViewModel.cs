namespace Server.Infrastructure.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "Your {0} must be {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password doesn`t match.")]
        //public string ConfirmPassword { get; set; }
    }
}
