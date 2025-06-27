using System.ComponentModel.DataAnnotations;

namespace ToyShopWebApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
