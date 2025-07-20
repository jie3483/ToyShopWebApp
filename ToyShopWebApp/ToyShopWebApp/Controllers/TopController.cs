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
                new Toy { Id = 2, Name = "Basketball Hoop", Price = 34.99M, ImageUrl = "/images/hoop.jpg" },
                new Toy { Id = 1, Name = "Teddy Bear", Price = 24.99M, ImageUrl = "/images/teddy.jpg" }
            };
            return View(topToys);
        }
    }
}
