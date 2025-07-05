using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;
using System.Linq;

namespace ToyShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
            if (user != null)
            {
                HttpContext.Session.SetString("User", username);
                return RedirectToAction("Index", "Toy");
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string email, string address)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            var exists = _context.Users.Any(u => u.Username == username);
            if (exists)
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var user = new User
            {
                Username = username,
                PasswordHash = password,
                Email = email,
                Address = address
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetString("User", username);
            return RedirectToAction("Index", "Toy");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User"); 
            return RedirectToAction("Index", "Home");
        }
    }
}
