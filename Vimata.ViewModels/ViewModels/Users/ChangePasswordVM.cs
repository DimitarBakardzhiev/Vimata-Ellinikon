namespace Vimata.ViewModels.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordVM
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
