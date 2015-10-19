using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeVault.Models.ViewModels
{
    public class SoftwarePolicyViewModel : IDisposable
    {
        private CV2Context db = new CV2Context();
        public void Dispose()
        {
            db.Dispose();
        }

        public SoftwarePolicyViewModel()
        {

        }

        public SoftwarePolicyViewModel(Product viewProduct)
        {
            var softwarePolicy = db.SoftwarePolicies.Include("SoftwarePolicyGroupAssociations").Where(p => p.ProductId == viewProduct.ProductId).FirstOrDefault();
            
            Id = softwarePolicy.ProductId;
            PolicyType = softwarePolicy.SoftwarePolicyType.ToString();
            GroupAssociations = new HashSet<SoftwarePolicyGroupAssociationViewModel>();
            foreach (var group in softwarePolicy.SoftwarePolicyGroupAssociations)
            {
                SoftwarePolicyGroupAssociationViewModel policyGroup = new SoftwarePolicyGroupAssociationViewModel(group);
                policyGroup.SupportLevel = db.SoftwarePolicySupportLevels.Where(s => s.SoftwarePolicySupportLevelId == group.SoftwarePolicySupportLevelId).Select(s => s.SoftwarePolicySupportLevelName).FirstOrDefault();
                GroupAssociations.Add(policyGroup);
            }
        }

        [Key]
        public int Id { get; set; }
        [DisplayName("Group Name")]
        public ICollection<SoftwarePolicyGroupAssociationViewModel> GroupAssociations { get; set; }

        public string PolicyType { get; set; }
    }

    public class SoftwarePolicyGroupAssociationViewModel
    {
        public SoftwarePolicyGroupAssociationViewModel(SoftwarePolicyGroupAssociation viewGroupAssociation)
        {
            Id = viewGroupAssociation.ProductId;
            SoftwarePolicyId = viewGroupAssociation.SoftwarePolicyGroupAssociationId;
            GroupName = viewGroupAssociation.SoftwarePolicyADGroupName;
            DisplayName = viewGroupAssociation.SoftwarePolicyGroupDisplayName;
            Description = viewGroupAssociation.SoftwarePolicyGroupDescription;
        }

        public int ? Id { get; set; }
        public int ? SoftwarePolicyId { get; set; }
        [DisplayName("Group Name")]
        public string GroupName { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        public string Description { get; set; }
        [DisplayName("Support Level")]
        public string SupportLevel { get; set; }
    }
}