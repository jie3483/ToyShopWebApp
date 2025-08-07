using System.Diagnostics;
using System.Text.Json;
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

        // ? 首页随机展示 2–4 个玩具，刷新后变更
        public IActionResult Index()
        {
            const string sessionKey = "HomePageToys";
            List<Toy> toysToDisplay;

            if (HttpContext.Session.GetString(sessionKey) == null)
            {
                var allToys = _context.Toys.ToList();
                var rnd = new Random();
                var count = rnd.Next(2, 5); // 随机数量：2~4

                var randomToys = allToys.OrderBy(t => Guid.NewGuid()).Take(count).ToList();
                toysToDisplay = randomToys;

                var json = JsonSerializer.Serialize(toysToDisplay);
                HttpContext.Session.SetString(sessionKey, json);
            }
            else
            {
                var json = HttpContext.Session.GetString(sessionKey);
                toysToDisplay = JsonSerializer.Deserialize<List<Toy>>(json);
            }

            return View(toysToDisplay); // @model List<Toy>
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
