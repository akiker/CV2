using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            ProductType = product.ProductType == null ? string.Empty : product.ProductType.ProductTypeName;
            Category = product.ProductCategory == null ? string.Empty :  product.ProductCategory.ProductCategoryName;
            ProductCode = product.ProductCode;
            UpgradeCode = product.ProductUpgradeCode;
            //PrimaryContact = product.ProductContacts.FirstOrDefault(t => t.ProductContactRoleId == 2);
            Permissions = new PermissionViewModel(product);
            PermissionDetails = Permissions.PermissionDetails;
            PostInstallDependencies = new HashSet<DependencyViewModel>();
            PreInstallDependencies = new HashSet<DependencyViewModel>();
            SoftwarePolicyGroupAssociations = new HashSet<SoftwarePolicyGroupAssociationViewModel>();
            foreach (var dep in product.Dependencies)
            {
                DependencyViewModel depView = new DependencyViewModel(dep);
                if(depView.TypeOfDependency == DependencyType.PostInstall)
                {
                    PostInstallDependencies.Add(depView);
                }
                else
                {
                    PreInstallDependencies.Add(depView);
                }
            }

            SoftwarePolicy = new SoftwarePolicyViewModel(product);
            if(SoftwarePolicy != null)
            {
                foreach (var group in SoftwarePolicy.GroupAssociations)
                {
                    SoftwarePolicyGroupAssociations.Add(group);
                }
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public string Status { get; set; }

        [DisplayName("Type")]
        public string ProductType { get; set; }

        public string Category { get; set; }
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }
        [DisplayName("Upgrade Code")]
        public string UpgradeCode { get; set; }

        public PermissionViewModel Permissions { get; set; }
        public ICollection<PermissionDetailViewModel> PermissionDetails { get; private set; }
        public ProductContact PrimaryContact { get; private set; }

        public ICollection<DependencyViewModel> PostInstallDependencies { get; set; }

        public ICollection<DependencyViewModel> PreInstallDependencies { get; set; }

        public SoftwarePolicyViewModel SoftwarePolicy { get; set; }

        public ICollection<SoftwarePolicyGroupAssociationViewModel> SoftwarePolicyGroupAssociations { get; set; }
    }
}