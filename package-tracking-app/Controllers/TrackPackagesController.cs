using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using package_tracking_app.Data;
using package_tracking_app.Models;
using package_tracking_app.ViewModels;
using Shippo;

namespace package_tracking_app.Controllers
{
    public class TrackPackagesController : Controller
    {
        private ApplicationDbContext context;

        public TrackPackagesController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        //retrieve stataus info from Shippo and add to local model class
        public IActionResult GetStatus()
        {
            APIResource resource = new APIResource("");
            string TRACKING_NO = "SHIPPO_DELIVERED";
            Track track = resource.RetrieveTracking("shippo", TRACKING_NO);

            TrackingStatusModel status = new TrackingStatusModel(track.TrackingStatus.Status, track.TrackingStatus.Location.City, track.TrackingStatus.Location.State, track.TrackingStatus.StatusDate);

            return View(status);
        }

        //retrieve tracking history from Shippo and add to local model class
        public IActionResult GetTrackingHistory()
        {
            APIResource resource = new APIResource("");
            string TRACKING_NO = "SHIPPO_DELIVERED";
            Track track = resource.RetrieveTracking("shippo", TRACKING_NO);
            TrackingHistoryModel checkpoints = new TrackingHistoryModel(track.TrackingHistory);
            
            return View(checkpoints);
        }

        // GET: /<controller>/
        public IActionResult Index()
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
        }
    }
}
