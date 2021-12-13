using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using package_tracking_app.Data;
using package_tracking_app.Models;

namespace package_tracking_app.Controllers
{
    public class TrackPackagesController : Controller
    {

        private ApplicationDbContext context;

        public TrackPackagesController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
 
        // GET: /<controller>/
        public IActionResult Index()
        {
            //display list of existing packages
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
