using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyShopWebApp.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int ToyID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("ToyID")]
        public Toy Toy { get; set; }
    }
}
