using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace package_tracking_app.Controllers
{
    public class SettingController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Notifications()
        {
            return View();
        }
    }
}
