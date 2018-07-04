using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace LoginWebApp.Controllers
{
    
    public class HomeController : Controller
    {

        ILogger<HomeController> _log;
        private readonly UserManager<ApplicationUser> _userManager; 


        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _log = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = GetCurrentUser().Result;
            string name = user == null ? "No User is Logged In":user.UserName ;
            _log.LogInformation($"User : {name} - Entered Index Action {DateTime.Now}");
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        private Task<ApplicationUser> GetCurrentUser()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
