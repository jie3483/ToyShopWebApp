using System.ComponentModel.DataAnnotations;

namespace ToyShopWebApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
