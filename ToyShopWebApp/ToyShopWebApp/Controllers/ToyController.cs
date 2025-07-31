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

        public IActionResult Detail(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            return View(toy);
        }

        public IActionResult DetailPartial(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            // 记录点击次数
            toy.ClickCount++;
            _context.SaveChanges();

            // 记录浏览记录
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                if (!_context.BrowsingHistories.Any(h => h.UserID == userId && h.ToyID == id))
                {
                    _context.BrowsingHistories.Add(new BrowsingHistory
                    {
                        ToyID = id,
                        UserID = userId,
                        ViewedAt = DateTime.Now
                    });
                    _context.SaveChanges();
                }
            }

            var reviews = _context.Reviews
                .Where(r => r.ToyID == id)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            ViewBag.Toy = toy;
            ViewBag.Reviews = reviews;

            return PartialView("_ToyDetailPartial");
        }

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

        [HttpPost]
        public IActionResult SubmitReview(int toyId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

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

            // 🔁 提交成功后跳转回完整的详情页，显示评论
            return RedirectToAction("ReviewPage", new { id = toyId });
        }

        public IActionResult ReviewPage(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            var reviews = _context.Reviews
                .Where(r => r.ToyID == id)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            ViewBag.Reviews = reviews;

            return View("ReviewPage", toy); // 对应 Views/Toy/ReviewPage.cshtml
        }

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

            return RedirectToAction("ReviewPage", new { id = toyId });
        }
    }
}


