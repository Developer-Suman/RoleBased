using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleBased.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace RoleBased.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            //var username = HttpContext.User.Identity.Name;
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          

            var username = HttpContext.Session.GetString("Username");
            var password = HttpContext.Session.GetString("Password");


            return View();
        }


        public IActionResult HomePages()
        {
            return View();
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