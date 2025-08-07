using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    [Authorize]
    public class TopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Read from browsing history first
            var toyList = _context.BrowsingHistories
                .Where(h => h.UserID == userId)
                .Include(h => h.Toy)
                .OrderByDescending(h => h.ViewedAt)
                .Select(h => h.Toy)
                .Distinct()
                .ToList();

            // If the history is empty, the top 5 popular toys with the highest number of clicks will be displayed by default
            if (!toyList.Any())
            {
                toyList = _context.Toys
                    .OrderByDescending(t => t.ClickCount)
                    .Take(5)
                    .ToList();
            }

            return View(toyList); // @model List<Toy>
        }
    }
}
