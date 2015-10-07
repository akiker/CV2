namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SoftwarePolicySupportLevels", Schema = "CV2")]
    public partial class SoftwarePolicySupportLevel
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