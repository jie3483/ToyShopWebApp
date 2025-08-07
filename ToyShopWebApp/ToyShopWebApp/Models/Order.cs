using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyShopWebApp.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public string UserID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }

        public string OrderNumber { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
