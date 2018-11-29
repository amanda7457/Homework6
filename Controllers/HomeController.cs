using System;
using Microsoft.AspNetCore.Mvc;

namespace Barron_Amanda_HW6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
