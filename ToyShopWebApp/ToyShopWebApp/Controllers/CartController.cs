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
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

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

        // ✅ Checkout 提交订单并生成订单编号
        [HttpPost]
        public IActionResult Checkout(string Name, string Email, string Address, string PaymentMethod)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

            var cartItems = _context.CartItems
                .Where(c => c.UserID == userIdStr)
                .Include(c => c.Toy)
                .ToList();

            if (!cartItems.Any())
                return RedirectToAction("Index");

            decimal totalAmount = cartItems.Sum(item => item.Toy.Price * item.Quantity);

            // ✅ 生成唯一订单编号
            string orderNumber = $"ORD{DateTime.Now:yyyyMMddHHmmssfff}";

            var order = new Order
            {
                CustomerName = Name,
                CustomerEmail = Email,
                ShippingAddress = Address,
                PaymentMethod = PaymentMethod,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                UserID = userIdStr,
                OrderNumber = orderNumber
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // ✅ 清空购物车
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            // ✅ 根据支付方式跳转
            if (PaymentMethod == "QR")
                return RedirectToAction("PayQR", new { orderNumber = orderNumber });
            else
                return RedirectToAction("PaymentSuccess");
        }

        // ✅ 支付成功页面，含 Toast 提示触发器
        public IActionResult PaymentSuccess()
        {
            TempData["ShowToast"] = "true"; // ⬅️ 用于显示成功提示
            return View();
        }

        // ✅ 支付失败页面（备用）
        public IActionResult PaymentFailed()
        {
            return View();
        }

        // ✅ 模拟扫码支付页面，接收订单编号
        public IActionResult PayQR(string orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;
            return View();
        }
    }
}
