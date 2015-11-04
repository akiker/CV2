using System.ComponentModel;
using CodeVault.Models;

namespace CodeVault.ViewModels
{
    public class SoftwarePolicyGroupAssociationViewModel
    {
        public SoftwarePolicyGroupAssociationViewModel()
        {
                
        }

        public int? Id { get; set; }
        public int? SoftwarePolicyId { get; set; }

        [DisplayName("Group Name")]
        public string GroupName { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        [DisplayName("Support Level")]
        public string SupportLevel { get; set; }
    }
}