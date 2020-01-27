namespace Server.Infrastructure.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
