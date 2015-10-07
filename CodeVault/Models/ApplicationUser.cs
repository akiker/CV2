namespace CodeVault.Models
{
    using EntityFramework.Triggers;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("ApplicationUsers", Schema = "CV2")]
    public partial class ApplicationUser : ITriggerable
    {
        [DataMember]
        [Key]
        public int ApplicationUserId { get; set; }

        [DataMember]
        [Required]
        public string ApplicationUserLoginId { get; set; }

        [DataMember]
        [Required]
        public string ApplicationUserName { get; set; }

        [DataMember]
        [Required]
        public string ApplicationUserEmail { get; set; }

        [DataMember]
        [Required]
        public string ApplicationUserPhoneNumber { get; set; }

        public int ApplicationRoleId { get; set; }

        [DataMember]
        [ForeignKey("ApplicationRoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        [DataMember]
        public DateTime CreatedOnDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int WorkQueueId { get; set; }

        [DataMember]
        [ForeignKey("WorkQueueId")]
        public virtual WorkQueue WorkQueue { get; set; }
    }
}