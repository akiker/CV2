using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CodeVault.Models.ViewModels
{
    public class PermissionViewModel : IDisposable
    {
        private CV2Context db = new CV2Context();
        public PermissionViewModel()
        {

        }

        public void Dispose()
        {
            db.Dispose();
        }

        public PermissionViewModel(Product viewProduct)
        {
            var permission = db.ProductPermissions.Where(p => p.ProductId == viewProduct.ProductId).FirstOrDefault();
            var permissionDetail = db.ProductPermissionDetails.Where(p => p.ProductId == viewProduct.ProductId);
            var localAccountVerification = db.LocalAdminVerification.Where(p => p.ProductId == viewProduct.ProductId).FirstOrDefault();
            
            this.Id = viewProduct.ProductId;
            this.ElevatedRightsRequired = permission == null ? false:permission.ElevatedRightsRequired;
            this.RequiresAdminRightsBasic = permission == null ? false : permission.RequiresAdminRightsBasic;
            this.RequiresAdminRightsAdvanced = permission == null ? false : permission.RequiresAdminRightsAdvanced;
            this.RequiresAdminRightsUpdate = permission == null ? false : permission.RequiresAdminRightsUpdates;
            this.LaVerified = localAccountVerification == null ? false : localAccountVerification.LocalAdminVerificationComplete;
            this.WorksWithLa = localAccountVerification == null ? false : localAccountVerification.WorksWithLocalAdminAccount;
            this.DoesNotWorkWithLa = this.WorksWithLa ? false:true;
            this.PermissionDetails = new HashSet<PermissionDetailViewModel>();

            foreach (var perm in permissionDetail)
            {
                var permissionDetailViewModel = new PermissionDetailViewModel(perm);
                this.PermissionDetails.Add(permissionDetailViewModel);
            }
        }

        [Key]
        public int Id { get; set; }
        [DisplayName("Elevated Rights Required")]
        public bool ElevatedRightsRequired { get; set; }
        public bool RequiresAdminRightsBasic { get; set; }
        public bool RequiresAdminRightsAdvanced { get; set; }
        public bool RequiresAdminRightsUpdate { get; set; }
        public bool LaVerified { get; set; }
        public bool WorksWithLa { get; set; }
        public bool DoesNotWorkWithLa { get; set; }
        public ICollection<PermissionDetailViewModel> PermissionDetails { get; set; }
    }

    public class PermissionDetailViewModel
    {
        public PermissionDetailViewModel(ProductPermissionDetail permissionDetail)
        {
            this.Id = permissionDetail.ProductId;
            this.Permission = permissionDetail.ProductPermissionDetailAcl.ToString();
            this.Type = permissionDetail.ProductPermissionDetailType.ToString();
            this.GroupUser = permissionDetail.ProductPermissionGroupOrUserName;
            this.Location = permissionDetail.ProductPermissionLocation;
        }

        [Key]
        public int? Id { get; set; }
        public string Permission { get; set; }

        public string Type { get; set; }
        public string GroupUser { get; set; }
        public string Location { get; set; }
    }
}
