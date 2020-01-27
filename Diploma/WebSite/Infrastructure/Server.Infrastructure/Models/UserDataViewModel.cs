namespace Server.Infrastructure.Models.AccountViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class UserDataViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(12, ErrorMessage = "Insorrect password", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords doesn`t match")]
        public string ConfirmPassword { get; set; }
    }
}
