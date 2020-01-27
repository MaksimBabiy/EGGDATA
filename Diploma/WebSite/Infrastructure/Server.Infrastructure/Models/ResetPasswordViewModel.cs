namespace Server.Infrastructure.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
