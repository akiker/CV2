using System;
using System.Collections.Generic;
using System.ComponentModel;
using CodeVault.Models;

namespace CodeVault.ViewModels
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
            Category = product.ProductCategory == null ? string.Empty : product.ProductCategory.ProductCategoryName;
            ProductCode = product.ProductCode;
            UpgradeCode = product.ProductUpgradeCode;
            SoftwarePolicyType = product.SoftwarePolicy.SoftwarePolicyType.ToString().ToUpper();
            ElevatedRightsRequired = product.ProductsPermissions != null && product.ProductsPermissions.ElevatedRightsRequired;
            RequiresAdminRightsAdvanced = product.ProductsPermissions != null && product.ProductsPermissions.RequiresAdminRightsAdvanced;
            RequiresAdminRightsBasic = product.ProductsPermissions != null && product.ProductsPermissions.RequiresAdminRightsBasic;
            RequiresAdminRightsUpdate = product.ProductsPermissions != null && product.ProductsPermissions.RequiresAdminRightsUpdates;
            LocalAccountVerificationComplete = product.LocalAccountVerification != null && product.LocalAccountVerification.LocalAdminVerificationComplete;
            WorksWithLocalAccount = product.LocalAccountVerification != null && product.LocalAccountVerification.WorksWithLocalAdminAccount;
            DoesNotWorkWithLocalAccount = WorksWithLocalAccount != true;
        }

        public bool LocalAccountVerificationComplete { get; set; }

        public bool WorksWithLocalAccount { get; set; }

        public bool DoesNotWorkWithLocalAccount { get; set; }

        public bool ElevatedRightsRequired { get; set; }

        public bool RequiresAdminRightsAdvanced { get; set; }

        public bool RequiresAdminRightsBasic { get; set; }

        public bool RequiresAdminRightsUpdate { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        [DisplayName("Created on Date")]
        public DateTime CreatedOnDate { get; set; }

        public string Status { get; set; }

        [DisplayName("Type")]
        public string ProductType { get; set; }

        public string Category { get; set; }

        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        [DisplayName("Upgrade Code")]
        public string UpgradeCode { get; set; }

        public ProductContact PrimaryContact { get; private set; }

        public string SoftwarePolicyType { get; set; }
    }
}