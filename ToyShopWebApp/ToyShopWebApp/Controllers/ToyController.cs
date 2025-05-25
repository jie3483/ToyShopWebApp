using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    public class ToyController : Controller
    {
        public IActionResult Index()
        {
            var toys = new List<Toy>
            {
                new Toy { Id = 1, Name = "Teddy Bear", Description = "Soft and cuddly.", Price = 29.99M, ImageUrl = "/images/teddy.jpg" },
                new Toy { Id = 2, Name = "Lego Set", Description = "Creative building blocks.", Price = 59.99M, ImageUrl = "/images/lego.jpg" }
            };
            return View(toys);
        }
    }
}
