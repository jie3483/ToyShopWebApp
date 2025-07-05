using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Models;
using ToyShopWebApp.Data;

namespace ToyShopWebApp.Controllers
{
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var toys = _context.Toys.ToList(); // use real DB data
            return View(toys);
        }

        // NEW METHOD: Record browsing history then show toy
        public IActionResult ViewToy(int id)
        {
            var username = HttpContext.Session.GetString("User");
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return RedirectToAction("Login", "Account");

            bool exists = _context.BrowsingHistories.Any(h => h.UserID == user.UserID && h.ToyID == id);
            if (!exists)
            {
                _context.BrowsingHistories.Add(new BrowsingHistory
                {
                    UserID = user.UserID,
                    ToyID = id,
                    ViewedAt = DateTime.Now
                });
                _context.SaveChanges();
            }

            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            return View("ToyDetail", toy);
        }
    }
}
