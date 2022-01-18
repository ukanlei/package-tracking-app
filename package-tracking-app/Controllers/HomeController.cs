using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using package_tracking_app.Data;
using package_tracking_app.Models;
using package_tracking_app.ViewModels;
using Shippo;

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

        public IActionResult AddForm()
        {
            MainModel mainModel = new MainModel();
            mainModel.AddPackageViewModel = new AddPackageViewModel();
            return View(mainModel);
        }

        public IActionResult Index()
        {
            MainModel mainModel = new MainModel();
            mainModel.PackageList = context.Packages.ToList();
            return View(mainModel);
        }

        public IActionResult Detail(int id)
        {   
     
            MainModel mainModel = new MainModel();
            APIResource resource = new APIResource("");

            mainModel.Package = context.Packages.Find(id);

            //needs work. when id selected, grab from database for carrier and tracking# as arguemnts
            string trackingNumber = context.Packages.Find(id).TrackingNumber; //grab from package table
            string carrier = context.Packages.Find(id).Carrier;
            Track track = resource.RetrieveTracking(carrier, trackingNumber);
            mainModel.TrackHistory = new TrackingHistoryModel(track.TrackingHistory);

            return View(mainModel);
        }

        public IActionResult Delete()
        {
            MainModel mainModel = new MainModel();
            mainModel.PackageList = context.Packages.ToList();
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
            APIResource resource = new APIResource("");
            string carrier = mainModel.AddPackageViewModel.Carrier;
            string trackingNumber = mainModel.AddPackageViewModel.TrackingNumber;
            string description = mainModel.AddPackageViewModel.Description;
            Track track = resource.RetrieveTracking(carrier, trackingNumber);

            //add new package info if input meets validation
            if (ModelState.IsValid)
            {
                Package newPackage = new Package
                {
                    TrackingNumber = trackingNumber,
                    Carrier = carrier,
                    Description = description
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
