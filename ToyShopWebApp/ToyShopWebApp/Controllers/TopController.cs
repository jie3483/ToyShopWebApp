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

            // 先从浏览记录中读取
            var toyList = _context.BrowsingHistories
                .Where(h => h.UserID == userId)
                .Include(h => h.Toy)
                .OrderByDescending(h => h.ViewedAt)
                .Select(h => h.Toy)
                .Distinct()
                .ToList();

            // 如果历史为空，默认显示点击数前5的热门玩具
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
