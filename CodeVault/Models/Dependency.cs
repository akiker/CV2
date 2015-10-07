using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("Dependencies", Schema = "CV2")]
    public partial class Dependencies
    {
        [Key, ForeignKey("BaseProduct"), Column(Order = 0)]
        public int BaseProductId { get; set; }

        [Key, ForeignKey("Dependency"), Column(Order = 1)]
        public int DependencyProductId { get; set; }

        public int InstallOrder { get; set; }

        public DependencyType DependencyType { get; set; }

        public virtual Product BaseProduct { get; set; }

        public virtual Product Dependency { get; set; }
    }
}