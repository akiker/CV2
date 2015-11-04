using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("SoftwarePolicySupportLevels", Schema = "CV2")]
    public class SoftwarePolicySupportLevel
    {
        public SoftwarePolicySupportLevel()
        {
            SoftwarePolicyGroupAssociations = new HashSet<SoftwarePolicyGroupAssociation>();
        }

        public int SoftwarePolicySupportLevelId { get; set; }

        [Required]
        public string SoftwarePolicySupportLevelName { get; set; }

        public virtual ICollection<SoftwarePolicyGroupAssociation> SoftwarePolicyGroupAssociations { get; set; }
    }
}