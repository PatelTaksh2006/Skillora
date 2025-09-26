using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skillora.Models;
using Skillora.Models.Auth;
using Skillora.Models.ViewModels;

namespace Skillora.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user =await _userManager.GetUserAsync(User);
            if(user!=null)
            {
                if(user.Role=="Student")
                {
                    
                    return RedirectToAction("Index", "Student");
                }
                else if(user.Role=="Company")
                {
                    return RedirectToAction("Index", "Company");
                }
                else if(user.Role=="Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
