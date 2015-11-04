using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("SoftwarePolicyCategories", Schema = "CV2")]
    public class SoftwarePolicyCategory
    {
        public SoftwarePolicyCategory()
        {
            SoftwarePolicies = new HashSet<SoftwarePolicy>();
        }

        public int SoftwarePolicyCategoryId { get; set; }

        [Required]
        public string SoftwarePolicyCategoryName { get; set; }

        public virtual ICollection<SoftwarePolicy> SoftwarePolicies { get; set; }
    }
}