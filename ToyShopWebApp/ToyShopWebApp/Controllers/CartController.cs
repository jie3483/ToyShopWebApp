using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private static List<Toy> cart = new List<Toy>();

        public IActionResult Index()
        {
            return View(cart);
        }

        public IActionResult Add(int id)
        {
            var toy = new Toy { Id = id, Name = "Sample Toy " + id, Price = 19.99M };
            cart.Add(toy);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var toy = cart.FirstOrDefault(t => t.Id == id);
            if (toy != null) cart.Remove(toy);
            return RedirectToAction("Index");
        }
    }
}
