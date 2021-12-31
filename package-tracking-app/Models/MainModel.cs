using System;
using System.Collections.Generic;
using package_tracking_app.ViewModels;

namespace package_tracking_app.Models
{
    public class MainModel
    {
        public AddPackageViewModel AddPackageViewModel { get; set; }
        public List<Package> PackageList { get; set; }
        public Package Package { get; set; }

        public MainModel()
        {
        }
    }
}
