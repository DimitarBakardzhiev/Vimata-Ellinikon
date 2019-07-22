using System.ComponentModel.DataAnnotations;

namespace Vimata.Data.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
