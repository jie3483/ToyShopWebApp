using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;

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

            var history = _context.BrowsingHistories
                .Where(h => h.UserID == userId)
                .Include(h => h.Toy)
                .OrderByDescending(h => h.ViewedAt)
                .Select(h => h.Toy)
                .Take(10)
                .ToList();

            return View(history); // View 要用 @model List<Toy>
        }
    }
}
