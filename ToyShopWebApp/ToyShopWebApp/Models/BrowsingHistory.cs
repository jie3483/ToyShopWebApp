using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyShopWebApp.Models
{
    public class BrowsingHistory
    {
        [Key]
        public int Id { get; set; }

        public string UserID { get; set; }
        public int ToyID { get; set; }

        public DateTime ViewedAt { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("ToyID")]
        public Toy Toy { get; set; }
    }
}
