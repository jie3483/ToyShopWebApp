using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    public class TopController : Controller
    {
        public IActionResult Index()
        {
            var topToys = new List<Toy>
            {
                new Toy { Id = 2, Name = "Lego Set", Price = 59.99M, ImageUrl = "/images/lego.jpg" },
                new Toy { Id = 1, Name = "Teddy Bear", Price = 29.99M, ImageUrl = "/images/teddy.jpg" }
            };
            return View(topToys);
        }
    }
}
