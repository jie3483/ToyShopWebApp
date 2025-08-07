using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager; 

        public AdminController(ApplicationDbContext context, UserManager<User> userManager) // ✅ 注入
        {
            _context = context;
            _userManager = userManager;
        }

        // Backend homepage
        public IActionResult Index()
        {
            return View();
        }

        // View user registration information
        public IActionResult Users()
        {
            var users = _userManager.Users.Cast<User>().ToList();
            return View(users);
        }

        // View order billing information
        public IActionResult Orders()
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }
    }
}
