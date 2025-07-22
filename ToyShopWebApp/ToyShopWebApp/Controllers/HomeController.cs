using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Data;
using ToyShopWebApp.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ToyShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ? 首页加载点击最多的前 3 个玩具
        public IActionResult Index()
        {
            var topToys = _context.Toys
                .OrderByDescending(t => t.ClickCount)
                .Take(3)
                .ToList();

            return View(topToys); // ? 传递给视图：@model List<Toy>
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
