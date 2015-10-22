using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CodeVault.Models.ViewModels
{
    public class LicenseViewModel
    {
        CV2Context db = new CV2Context();

        public LicenseViewModel()
        {

        }

        public LicenseViewModel(int productId)
        {
            var licenses = db.Licenses.Include("LicenseKeys").Where(p => p.ProductId == productId);
            Licenses = new HashSet<LicenseDetailViewModel>();
            foreach (var license in licenses)
            {
                LicenseDetailViewModel licenseDetailView = new LicenseDetailViewModel(license);
                Licenses.Add(licenseDetailView);
            }
        }

        public ICollection<LicenseDetailViewModel> Licenses { get; set; }
    }

    public class LicenseDetailViewModel
    {
        public LicenseDetailViewModel(License viewLicense)
        {
            LicenseType = viewLicense.LicenseType.LicenseTypeName;
            SKU = viewLicense.LicenseSku;
            Notes = viewLicense.LicenseNotes;
            Owner = viewLicense.LicenseOwner;
            LicenseKeys = new HashSet<LicenseKey>();
            LicenseKeys = viewLicense.LicenseKeys;
            Id = viewLicense.ProductId;
        }

        public int? Id { get; set; }

        [DisplayName("Type")]
        public string LicenseType { get; set; }
        [DisplayName("SKU#")]
        public string SKU { get; set; }
        public string Notes { get; set; }
        public string Owner { get; set; }
        public ICollection<LicenseKey> LicenseKeys { get; set; }
    }
}