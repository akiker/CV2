namespace CodeVault.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("SoftwarePolicyGroupAssociations", Schema = "CV2")]
    public partial class SoftwarePolicyGroupAssociation
    {
        [DataMember]
        [Key]
        public int SoftwarePolicyGroupAssociationId { get; set; }

        [DataMember]
        public string SoftwarePolicyADGroupName { get; set; }

        [DataMember]
        public string SoftwarePolicyGroupDisplayName { get; set; }

        [DataMember]
        public string SoftwarePolicyGroupDescription { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual SoftwarePolicy SoftwarePolicy { get; set; }

        public int? SoftwarePolicySupportLevelId { get; set; }

        [ForeignKey("SoftwarePolicySupportLevelId")]
        [DataMember]
        public virtual SoftwarePolicySupportLevel SoftwarePolicySupportLevel { get; set; }
    }
}