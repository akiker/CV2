namespace CodeVault.Models
{
    using EntityFramework.Triggers;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("SccmRules", Schema = "CV2")]
    public abstract class SccmRule : ITriggerable
    {
        [DataMember]
        [Key]
        public int SccmRuleId { get; set; }

        [DataMember]
        public SccmRuleType SccmRuleType { get; set; }

        [DataMember]
        public SccmRuleConnector SccmRuleConnector { get; set; }

        [DataMember]
        public int SccmRuleOrder { get; set; }

        [DataMember]
        public string SccmRuleNote { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}