using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeVault.Models.ViewModels
{
    public class LicenseViewModel
    {
        CV2Context db = new CV2Context();

        public LicenseViewModel(Product viewProduct)
        {
            var query = db.Licenses.Include("LicenseKeys").Where(l => l.ProductId == viewProduct.ProductId);


        }

        public string LicenseType { get; set; }
        public string SKU { get; set; }
        public string Notes { get; set; }
        public string Owner { get; set; }
    }

    public class LicenseKeyViewModel
    {
        public LicenseKeyViewModel()
        {

        }
    }
}