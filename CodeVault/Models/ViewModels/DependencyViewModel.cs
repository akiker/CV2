using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CodeVault.Models.ViewModels
{
    public class DependencyViewModel
    {
        private CV2Context db = new CV2Context();

        public DependencyViewModel()
        {

        }

        public DependencyViewModel(Dependencies viewDependency)
        {
            var query = db.Products.Where(p => p.ProductId == viewDependency.DependencyProductId).FirstOrDefault();

            Id = viewDependency.DependencyProductId;
            Name = query.ProductName;
            Version = query.ProductVersion;
            InstallOrder = viewDependency.InstallOrder;
            TypeOfDependency = viewDependency.DependencyType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        [DisplayName("Install Order")]
        public int InstallOrder { get; set; }
        [DisplayName("Type")]
        public DependencyType TypeOfDependency { get; private set; }
    }
}