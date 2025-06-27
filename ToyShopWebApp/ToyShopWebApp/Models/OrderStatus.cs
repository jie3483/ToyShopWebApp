using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyShopWebApp.Models
{
    public class OrderStatus
    {
        [Key]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
