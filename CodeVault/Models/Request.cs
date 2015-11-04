using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using CodeVault.Models.BaseTypes;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("Requests", Schema = "CV2")]
    public class Request : EntityBase
    {
        public Request()
        {
            RequestHistories = new HashSet<RequestHistory>();
        }

        [Key]
        [DataMember]
        public int RequestId { get; set; }

        [DataMember]
        [Required]
        public string RequestorUserId { get; set; }

        [DataMember]
        [Required]
        public string RequestTitle { get; set; }

        [DataMember]
        [Required]
        public string RequestDescription { get; set; }

        [DataMember]
        public DateTime CreatedOnDate { get; set; }

        [DataMember]
        public DateTime DueOnDate { get; set; }

        [DataMember]
        public DateTime? CompletedOnDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int RequestTypeId { get; set; }

        [DataMember]
        [ForeignKey("RequestTypeId")]
        public virtual RequestType RequestType { get; set; }

        [DataMember]
        public virtual ICollection<RequestHistory> RequestHistories { get; set; }

        public int RequestStatusId { get; set; }

        [DataMember]
        [ForeignKey("RequestStatusId")]
        public virtual RequestStatus RequestStatus { get; set; }

        public int? WorkQueueId { get; set; }

        [DataMember]
        [ForeignKey("WorkQueueId")]
        public virtual WorkQueue WorkQueue { get; set; }

        public virtual Product Product { get; set; }

        protected override void RegisterValidationMethods()
        {
        }

        protected override void ResetProperties()
        {
        }
    }
}