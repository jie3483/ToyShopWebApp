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

        // Increase quantity
        [HttpPost]
        public IActionResult Increase(int id)
        {
            var item = _context.CartItems.Include(c => c.Toy).FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                item.Quantity++;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Reduce quantity (no further reduction when it is 1)
        [HttpPost]
        public IActionResult Decrease(int id)
        {
            var item = _context.CartItems.Include(c => c.Toy).FirstOrDefault(c => c.Id == id);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Checkout and write the order+order item
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
            string orderNumber = $"ORD{DateTime.Now:yyyyMMddHHmmssfff}";

            // Create Order
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

            // Add order item record
            foreach (var item in cartItems)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderID = order.OrderID,
                    ToyID = item.ToyID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Toy.Price
                });
            }

            // empty cart
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            // Jump to the payment method page
            if (PaymentMethod == "QR")
                return RedirectToAction("QRCodePayment", new { orderNumber = orderNumber });
            else
                return RedirectToAction("PaymentSuccess");
        }

        public IActionResult PaymentSuccess()
        {
            TempData["ShowToast"] = "true";
            return View();
        }

        public IActionResult PaymentFailed()
        {
            return View();
        }

        public IActionResult QRCodePayment(string orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;
            return View();
        }
    }
}
