using Microsoft.AspNetCore.Mvc;

namespace ToyShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                TempData["User"] = username;
                return RedirectToAction("Index", "Toy");
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }
    }
}
