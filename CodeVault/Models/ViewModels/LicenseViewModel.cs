using CodeVault.Models.BaseTypes;
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
        IDALFacade facade = new DALFacade();

        public LicenseViewModel()
        {

        }

        public LicenseViewModel(int id)
        {
            Licenses = new HashSet<LicenseDetailViewModel>();
            IUnitOfWork unitOfWork = facade.GetUnitOfWork();
            var query = unitOfWork.LicenseRepo.GetByQuery(p => p.ProductId == id, o => o.OrderBy(n => n.ProductId), "LicenseKeys,LicenseType");
            var result = query.Select(p => p);

            foreach (var item in result)
            {
                LicenseDetailViewModel licenseDetailView = new LicenseDetailViewModel(item);
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
            //LicenseKeys = new HashSet<LicenseKey>();
            //LicenseKeys = viewLicense.LicenseKeys;
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