using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntityFramework.Triggers;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("SoftwarePolicies", Schema = "CV2")]
    public class SoftwarePolicy : ITriggerable
    {
        public SoftwarePolicy()
        {
            SoftwarePolicyGroupAssociations = new HashSet<SoftwarePolicyGroupAssociation>();
        }

        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public SoftwarePolicyType SoftwarePolicyType { get; set; }

        //public int? SoftwarePolicySupportLevelId { get; set;}

        //[DataMember]
        //[ForeignKey("SoftwarePolicySupportLevelId")]
        //public virtual SoftwarePolicySupportLevel SoftwarePolicySupportLevel{ get; set; }

        [DataMember]
        public virtual ICollection<SoftwarePolicyGroupAssociation> SoftwarePolicyGroupAssociations { get; set; }

        public virtual Product Product { get; set; }
    }
}