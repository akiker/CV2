namespace CodeVault.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("DistributionLocations", Schema = "CV2")]
    public partial class DistributionLocation
    {
        [Key]
        public int DistributionLocationId { get; set; }

        [Required]
        [DataMember]
        public string DistributionPath { get; set; }

        [Required]
        [DataMember]
        public DistributionMethod DistributionMethod { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}