using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    [Authorize]
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var toys = _context.Toys.ToList();
            return View(toys);
        }

        // ✅ 弹窗加载详情 + 记录历史 + 点击次数
        public IActionResult DetailPartial(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            toy.ClickCount++; // ✅ 每次查看详情增加点击数
            _context.SaveChanges();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                if (!_context.BrowsingHistories.Any(h => h.UserID == userId && h.ToyID == id))
                {
                    _context.BrowsingHistories.Add(new BrowsingHistory
                    {
                        UserID = userId,
                        ToyID = id,
                        ViewedAt = DateTime.Now
                    });
                    _context.SaveChanges();
                }
            }

            return PartialView("_ToyDetailPartial", toy);
        }

        [HttpPost]
        public IActionResult AddToCart(int toyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login", "Account");

            var existing = _context.CartItems
                .FirstOrDefault(c => c.UserID == userId && c.ToyID == toyId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ToyID = toyId,
                    UserID = userId,
                    Quantity = 1
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
