using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("PostInstallDependencies", Schema = "CV2")]
    public class PostInstallDependency
    {
        [Key, Column(Order = 0)]
        public int ParentProductId { get; set; }

        [Key, Column(Order = 1)]
        public int ChildProductId { get; set; }

        public int InstallOrder { get; set; }

        public virtual Product ParentProduct { get; set; } // FK_ParentProductId

        public virtual Product ChildProduct { get; set; } // FK_ChildProductId
    }
}