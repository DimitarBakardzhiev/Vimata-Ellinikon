namespace Vimata.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class SignupVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
