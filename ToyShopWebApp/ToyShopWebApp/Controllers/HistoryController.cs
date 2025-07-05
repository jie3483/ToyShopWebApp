using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("User");
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return RedirectToAction("Login", "Account");

            var history = _context.BrowsingHistories
                .Where(h => h.UserID == user.UserID)
                .Include(h => h.Toy)
                .OrderByDescending(h => h.ViewedAt)
                .Take(10)
                .Select(h => h.Toy)
                .ToList();

            return View(history);
        }

        public IActionResult AddToHistory(int toyId)
        {
            var username = HttpContext.Session.GetString("User");
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return RedirectToAction("Login", "Account");

            bool exists = _context.BrowsingHistories.Any(h => h.UserID == user.UserID && h.ToyID == toyId);
            if (!exists)
            {
                _context.BrowsingHistories.Add(new BrowsingHistory
                {
                    UserID = user.UserID,
                    ToyID = toyId,
                    ViewedAt = DateTime.Now
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Toy");
        }
    }
}
