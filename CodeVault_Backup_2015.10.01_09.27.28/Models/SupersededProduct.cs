using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("SupersededProducts", Schema = "CV2")]
    public partial class SupersededProducts
    {
        [Key, ForeignKey("BaseProduct"), Column(Order = 0)]
        public int BaseProductId { get; set; }

        [Key, ForeignKey("SupersededProduct"), Column(Order = 1)]
        public int SupersededProductId { get; set; }

        public virtual Product BaseProduct { get; set; }

        public virtual Product SupersededProduct { get; set; }
    }
}