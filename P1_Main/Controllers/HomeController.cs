using LogicLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using P1_Main.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P1_Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LogicClass _logicClass;

        public HomeController(ILogger<HomeController> logger, LogicClass logicClass)
        {
            _logicClass = logicClass;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserDetailView()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return View("Index");
            }
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var uvm = _logicClass.DisplayUser(claim);
            return View("UserDetailView", uvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
