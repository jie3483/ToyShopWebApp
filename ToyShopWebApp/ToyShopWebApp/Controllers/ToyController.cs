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

        // Product details page (full page loading)
        public IActionResult Detail(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            
            toy.ClickCount++;
            _context.SaveChanges();

            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool hasPurchased = false;

            //Determine whether to purchase
            if (!string.IsNullOrEmpty(userId))
            {
                hasPurchased = _context.Orders
                    .Include(o => o.OrderItems)
                    .Any(o => o.UserID == userId && o.OrderItems.Any(oi => oi.ToyID == id));

                //Browse history
                var history = _context.BrowsingHistories
    .FirstOrDefault(h => h.UserID == userId && h.ToyID == id);

                if (history != null)
                {
                    // Already exists: Update time
                    history.ViewedAt = DateTime.Now;
                }
                else
                {
                    // Does not exist: New record added
                    _context.BrowsingHistories.Add(new BrowsingHistory
                    {
                        ToyID = id,
                        UserID = userId,
                        ViewedAt = DateTime.Now
                    });
                }
                _context.SaveChanges();

            }

            // Load comments
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

        // Modal popup detail view (partial loading)
        public IActionResult DetailPartial(int id)
        {
            var toy = _context.Toys.FirstOrDefault(t => t.Id == id);
            if (toy == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool hasPurchased = false;

            if (!string.IsNullOrEmpty(userId))
            {
                hasPurchased = _context.Orders
                    .Include(o => o.OrderItems)
                    .Any(o => o.UserID == userId && o.OrderItems.Any(oi => oi.ToyID == id));

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

                // Click count+1
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


        // Add Cart
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

        // Submit a comment (only users who have already purchased can submit it)
        [HttpPost]
        public IActionResult SubmitReview(int toyId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

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

        // Delete comment (personal only)
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
