using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToyShopWebApp.Models
{
    public class User : IdentityUser
    {
        [MaxLength(200)]
        public string? Address { get; set; }

        public DateTime? RegisteredAt { get; set; }
    }
}
