using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyShopWebApp.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        public int UserID { get; set; }

        public string Role { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
