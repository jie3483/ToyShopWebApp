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

        // ✅ 商品详情页面（全页加载）
        public IActionResult Detail(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            // 点击统计
            toy.ClickCount++;
            _context.SaveChanges();

            // 当前用户
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool hasPurchased = false;

            // 判断是否购买
            if (!string.IsNullOrEmpty(userId))
            {
                hasPurchased = _context.Orders
                    .Include(o => o.OrderItems)
                    .Any(o => o.UserID == userId && o.OrderItems.Any(oi => oi.ToyID == id));

                // 浏览记录
                var history = _context.BrowsingHistories
    .FirstOrDefault(h => h.UserID == userId && h.ToyID == id);

                if (history != null)
                {
                    // ✅ 已存在：更新时间
                    history.ViewedAt = DateTime.Now;
                }
                else
                {
                    // ✅ 不存在：新增记录
                    _context.BrowsingHistories.Add(new BrowsingHistory
                    {
                        ToyID = id,
                        UserID = userId,
                        ViewedAt = DateTime.Now
                    });
                }
                _context.SaveChanges();

            }

            // 加载评论
            var reviews = _context.Reviews
                .Where(r => r.ToyID == id)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            ViewBag.Toy = toy;
            ViewBag.Reviews = reviews;
            ViewBag.HasPurchased = hasPurchased;

            return View("Detail");
        }

        // ✅ 模态弹窗详情视图（局部加载）
        public IActionResult DetailPartial(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool hasPurchased = false;

            if (!string.IsNullOrEmpty(userId))
            {
                // ✅ 判断是否购买
                hasPurchased = _context.Orders
                    .Include(o => o.OrderItems)
                    .Any(o => o.UserID == userId && o.OrderItems.Any(oi => oi.ToyID == id));

                // ✅ 浏览记录：若已存在则更新时间，否则新增
                var history = _context.BrowsingHistories
                    .FirstOrDefault(h => h.UserID == userId && h.ToyID == id);

                if (history != null)
                {
                    history.ViewedAt = DateTime.Now;
                }
                else
                {
                    _context.BrowsingHistories.Add(new BrowsingHistory
                    {
                        ToyID = id,
                        UserID = userId,
                        ViewedAt = DateTime.Now
                    });
                }

                // ✅ 点击数+1
                toy.ClickCount++;

                _context.SaveChanges();
            }

            var reviews = _context.Reviews
                .Where(r => r.ToyID == id)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            ViewBag.Toy = toy;
            ViewBag.Reviews = reviews;
            ViewBag.HasPurchased = hasPurchased;

            return PartialView("_ToyDetailPartial");
        }


        // ✅ 加入购物车
        [HttpPost]
        public IActionResult AddToCart(int toyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var existing = _context.CartItems.FirstOrDefault(c => c.UserID == userId && c.ToyID == toyId);
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

        // ✅ 提交评论（已购买用户才能提交）
        [HttpPost]
        public IActionResult SubmitReview(int toyId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // 检查是否购买
            var hasPurchased = _context.Orders
                .Include(o => o.OrderItems)
                .Any(o => o.UserID == userId && o.OrderItems.Any(oi => oi.ToyID == toyId));

            if (!hasPurchased)
            {
                TempData["Error"] = "You must purchase this toy before leaving a review.";
                return RedirectToAction("Detail", new { id = toyId });
            }

            var review = new Review
            {
                ToyID = toyId,
                UserID = userId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Detail", new { id = toyId });
        }

        // ✅ 删除评论（仅限本人）
        [HttpPost]
        public IActionResult DeleteReview(int reviewId, int toyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = _context.Reviews.FirstOrDefault(r => r.Id == reviewId && r.UserID == userId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }

            return RedirectToAction("Detail", new { id = toyId });
        }
    }
}
