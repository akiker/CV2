using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("PreInstallDependencies", Schema = "CV2")]
    public class PreInstallDependency
    {
        [Key, ForeignKey("BaseProduct")]
        public int BaseProductId { get; set; }

        [Key, ForeignKey("Dependency")]
        public int DependencyProductId { get; set; }

        public int InstallOrder { get; set; }

        public DependencyType DependencyType { get; set; }

        public virtual Product BaseProduct { get; set; }

        public virtual Product Dependency { get; set; }
    }
}