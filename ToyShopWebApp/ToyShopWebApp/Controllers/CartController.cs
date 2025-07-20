using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        public IActionResult AddToCart(int toyId)
        {
            // ✅ 获取当前登录用户的 ID（string 类型）
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

            // ✅ 查找用户购物车中是否已有此商品
            var existingItem = _context.CartItems
                .FirstOrDefault(c => c.ToyID == toyId && c.UserID == userIdStr);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ToyID = toyId,
                    UserID = userIdStr,
                    Quantity = 1
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Toy");
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

            var cartItems = _context.CartItems
                .Where(c => c.UserID == userIdStr)
                .Include(c => c.Toy)
                .ToList();

            return View(cartItems);
        }

        // POST: /Cart/Remove
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
