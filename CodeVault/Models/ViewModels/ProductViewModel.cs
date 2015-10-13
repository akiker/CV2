using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeVault.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }

        public ProductViewModel(Product product)
        {
            Id = product.ProductId;
            Name = product.ProductName;
            Manufacturer = product.ProductManufacturer;
            Description = product.ProductDescription;
            Version = product.ProductVersion;
            CreatedOnDate = product.CreatedOnDate;
            Status = product.ProductStatus.ToString();
            PrimaryContact = product.ProductContacts.FirstOrDefault(t => t.ProductContactRoleId == 2);
            Permissions = new PermissionViewModel(product);
            PermissionDetails = Permissions.PermissionDetails;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public string Status { get; set; }

        public PermissionViewModel Permissions { get; set; }
        public ICollection<PermissionDetailViewModel> PermissionDetails { get; private set; }
        public ProductContact PrimaryContact { get; private set; }
    }
}