using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyShopWebApp.Models
{
    public class ToyInventory
    {
        [Key, ForeignKey("Toy")]
        public int ToyID { get; set; }

        public int Quantity { get; set; }

        public Toy Toy { get; set; }
    }
}
