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
        private readonly UserManager<User> _userManager; // ✅ 新增字段

        public AdminController(ApplicationDbContext context, UserManager<User> userManager) // ✅ 注入
        {
            _context = context;
            _userManager = userManager;
        }

        // 后台主页
        public IActionResult Index()
        {
            return View();
        }

        // ✅ 查看用户注册信息
        public IActionResult Users()
        {
            var users = _userManager.Users.Cast<User>().ToList();
            return View(users);
        }

        // ✅ 查看订单账单信息
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
