using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using package_tracking_app.Areas.Identity.Data;
using package_tracking_app.Data;
using package_tracking_app.Models;
using package_tracking_app.ViewModels;
using Shippo;

namespace package_tracking_app.Controllers
{ 
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            context = dbcontext;
        }

        public IActionResult AddForm()
        {
            MainModel mainModel = new MainModel();
            mainModel.AddPackageViewModel = new AddPackageViewModel();
            return View(mainModel);
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            MainModel mainModel = new MainModel();
            var userPackages = context.Packages.Where(p => p.UserId == userId);
            mainModel.PackageList = userPackages.ToList();
            return View(mainModel);
        }

        public IActionResult Detail(int id)
        {   
            MainModel mainModel = new MainModel();
            APIResource resource = new APIResource("");//insert api key

            mainModel.Package = context.Packages.Find(id);

            string trackingNumber = context.Packages.Find(id).TrackingNumber; //grab from package table
            string carrier = context.Packages.Find(id).Carrier;
            Track track = resource.RetrieveTracking(carrier, trackingNumber);
            mainModel.TrackHistory = new TrackingHistoryModel(track.TrackingHistory);
            mainModel.TrackHistory.Checkpoints.Reverse(); //reverse order of package status with Reverse method

            return View(mainModel);
        }

        public IActionResult Delete()
        {
            string userId = _userManager.GetUserId(User);
            MainModel mainModel = new MainModel();
            var userPackages = context.Packages.Where(p => p.UserId == userId);
            mainModel.PackageList = userPackages.ToList();
            return View(mainModel);
        }

        
        public IActionResult EditName(int id)
        {
            MainModel mainModel = new MainModel();
            mainModel.Package = context.Packages.Find(id);
            return View(mainModel);
        }

        
        [HttpPost]
        public IActionResult ProcessAddForm(MainModel mainModel)
        {
            //if status code is 400 -> locate error -> return custom error message
            APIResource resource = new APIResource("");//insert api key

            string userId = _userManager.GetUserId(User);
            var userPackages = context.Packages.Where(p => p.UserId == userId);
            mainModel.PackageList = userPackages.ToList();
            string carrier = mainModel.AddPackageViewModel.Carrier;
            string trackingNumber = mainModel.AddPackageViewModel.TrackingNumber;
            string description = mainModel.AddPackageViewModel.Description;
            bool duplicate = userPackages.Any(t => t.TrackingNumber == trackingNumber);

            //Track track = resource.RetrieveTracking(carrier, trackingNumber);
            //add new package info if input meets validation

            if (ModelState.IsValid && carrier == "shippo" && duplicate == false)
            {
                Package newPackage = new Package
                {
                    TrackingNumber = trackingNumber,
                    Carrier = carrier,
                    Description = description,
                    UserId = userId
                };

                context.Packages.Add(newPackage);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", mainModel);
        }

        [HttpPost] //delete package
        public IActionResult Delete(int[] ids)
        {
            MainModel mainModel = new MainModel();
            foreach (int id in ids)
            {
                mainModel.Package = context.Packages.Find(id);
                context.Packages.Remove(mainModel.Package);
            }
            context.SaveChanges();
            return Redirect("Index");
        }


        [HttpPost] //change existign description to a new name
        public IActionResult EditName(int id, string newName)
        {
            MainModel mainModel = new MainModel();
            mainModel.Package = context.Packages.Find(id);
            mainModel.Package.Description = newName;

            context.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: /<controller>/
        /*public IActionResult Index()
        {
            List<Package> packages = context.Packages.ToList();
            return View(packages);
        }

        public IActionResult Detail(int id)
        {
            Package thePackage = context.Packages.Find(id);
            return View(thePackage);
        }

        public IActionResult Delete() 
        {
            List<Package> packages = context.Packages.ToList();
            return View(packages);
        }


        public IActionResult EditName(int id)
        {
            Package thePackage = context.Packages.Find(id);
            return View(thePackage);
        }


        [HttpPost] //delete package
        public IActionResult Delete(int[] ids)
        {
            
            foreach (int id in ids)
            {
                Package thePackage = context.Packages.Find(id);
                context.Packages.Remove(thePackage);
            }
            context.SaveChanges();
            return Redirect("Index");
        }


        [HttpPost] //change existign description to a new name
        public IActionResult EditName(int id, string newName)
        {
            Package thePackage = context.Packages.Find(id);
            thePackage.Description = newName;
            
            context.SaveChanges();
            return RedirectToAction("Index", "TrackPackages");
        }*/
    }
}
