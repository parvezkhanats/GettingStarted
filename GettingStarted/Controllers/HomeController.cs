using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GettingStarted.Models;
using Microsoft.AspNetCore.Identity;
using GettingStarted.ViewModels;

namespace GettingStarted.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("~/Home")]
        //[Route("[action]")]
        [Route("~/")]

        public IActionResult Index()
        {
            return View();
        }
        //[Route("Create")]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task <IActionResult> Create(RoleStore role)
        //{
        //    var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
        //    if (!roleExist)
        //    {
        //       await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
        //    }
        //    return RedirectToAction("Index");
        //}

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
