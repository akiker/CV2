using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodeVault.Models.ViewModels
{
    public class SoftwarePolicyViewModel : IDisposable
    {
        private readonly Cv2Context _db = new Cv2Context();

        public SoftwarePolicyViewModel()
        {
            GroupAssociations = new List<SoftwarePolicyGroupAssociationViewModel>();
        }

        //public SoftwarePolicyViewModel(Product viewProduct)
        //{
        //    var softwarePolicy =
        //        _db.SoftwarePolicies
        //            .Include("SoftwarePolicyGroupAssociations")
        //            .FirstOrDefault(p => p.ProductId == viewProduct.ProductId);

        //    Id = softwarePolicy.ProductId;
        //    PolicyType = softwarePolicy.SoftwarePolicyType.ToString();
        //    GroupAssociations = new HashSet<SoftwarePolicyGroupAssociationViewModel>();
        //    foreach (var group in softwarePolicy.SoftwarePolicyGroupAssociations)
        //    {
        //        var policyGroup = new SoftwarePolicyGroupAssociationViewModel(group);
        //        policyGroup.SupportLevel =
        //            _db.SoftwarePolicySupportLevels.Where(
        //                s => s.SoftwarePolicySupportLevelId == group.SoftwarePolicySupportLevelId)
        //                .Select(s => s.SoftwarePolicySupportLevelName)
        //                .FirstOrDefault();
        //        GroupAssociations.Add(policyGroup);
        //    }
        //}

        [Key]
        public int Id { get; set; }

        [DisplayName("Group Name")]
        public ICollection<SoftwarePolicyGroupAssociationViewModel> GroupAssociations { get; set; }

        public string PolicyType { get; set; }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}