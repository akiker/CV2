namespace CodeVault.Models
{
    using BaseTypes;
    using EntityFramework.Triggers;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System;

    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("Licenses", Schema = "CV2")]
    public partial class License : EntityBase, ITriggerable
    {
        public License()
        {
            LicenseKeys = new HashSet<LicenseKey>();
        }

        [DataMember]
        public int LicenseId { get; set; }

        [DataMember]
        public string LicenseSku { get; set; }

        [DataMember]
        public string LicenseNotes { get; set; }

        [DataMember]
        public string LicenseOwner { get; set; }

        [DataMember]
        public virtual ICollection<LicenseKey> LicenseKeys { get; set; }

        public int? LicenseTypeId { get; set; }

        [DataMember]
        [ForeignKey("LicenseTypeId")]
        public virtual LicenseType LicenseType { get; set; }

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