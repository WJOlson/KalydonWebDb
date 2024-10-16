using KalydonClientDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KalydonClientDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //GET: Home/Login
        public IActionResult Login()
        {
            return View();
        }

        private int loginAttempts = 0;
        private const int maxLoginAttempts = 3;
        
        public IActionResult ValidateLogin(string usernameInput, string passwordInput) 
        {
            const string username = "Wolson@2000";
            const string password = "HotDog@3";
            
            if ((username == usernameInput && password == passwordInput) && loginAttempts <= maxLoginAttempts)
            {
                return RedirectToAction("Index"); //go to index page if successful
            }
            else
            {
                ++loginAttempts;
                if (loginAttempts >= maxLoginAttempts)
                    return RedirectToAction("Error");
            }

            return View("Login"); //else stay on login page
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}