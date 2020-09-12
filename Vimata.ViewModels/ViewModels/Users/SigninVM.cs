using System.ComponentModel.DataAnnotations;

namespace Vimata.ViewModels.ViewModels.Users
{
    public class SigninVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
