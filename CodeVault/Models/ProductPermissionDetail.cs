using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using CodeVault.Models.BaseTypes;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("ProductPermissionDetails", Schema = "CV2")]
    public class ProductPermissionDetail : EntityBase
    {
        [DataMember]
        [Key]
        public int ProductPermissionDetailId { get; set; }

        [DataMember]
        public ProductPermissionDetailAcl ProductPermissionDetailAcl { get; set; }

        [DataMember]
        public ProductPermissionDetailType ProductPermissionDetailType { get; set; }

        [DataMember]
        public string ProductPermissionLocation { get; set; }

        [DataMember]
        public string ProductPermissionGroupOrUserName { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        protected override void RegisterValidationMethods()
        {
        }

        protected override void ResetProperties()
        {
        }
    }
}