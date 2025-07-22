using Microsoft.AspNetCore.Mvc;
using ToyShopWebApp.Data;
using System.Linq;

namespace ToyShopWebApp.Controllers
{
    public class TopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var topToys = _context.Toys
                .OrderByDescending(t => t.ClickCount)
                .Take(5)
                .ToList();

            return View(topToys);
        }
    }
}
