using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;
using ToyShopWebApp.ViewModels;

namespace ToyShopWebApp.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Load browsing history, including time
            var history = _context.BrowsingHistories
                .Where(h => h.UserID == userId)
                .Include(h => h.Toy)
                .OrderByDescending(h => h.ViewedAt)
                .Take(10)
                .Select(h => new HistoryViewModel
                {
                    Toy = h.Toy,
                    ViewedAt = h.ViewedAt
                })
                .ToList();

            return View(history); // @model List<HistoryViewModel>
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var history = _context.BrowsingHistories.Where(h => h.UserID == userId);
            _context.BrowsingHistories.RemoveRange(history);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
