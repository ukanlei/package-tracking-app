using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using package_tracking_app.Data;
using package_tracking_app.Models;
using package_tracking_app.ViewModels;

namespace package_tracking_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AddPackageViewModel addPackageViewModel = new AddPackageViewModel();
            return View(addPackageViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddForm(AddPackageViewModel addPackageViewModel)
        {
            //add new package info if input meets validation
            if (ModelState.IsValid)
            {
                Package newPackage = new Package
                {
                    TrackingNumber = addPackageViewModel.TrackingNumber,
                    Carrier = addPackageViewModel.Carrier,
                    Description = addPackageViewModel.Description
                };

                context.Packages.Add(newPackage);
                context.SaveChanges();
                return RedirectToAction("Index","TrackingPackages");
            }

            return View("Index", addPackageViewModel);
        }
    }
}
