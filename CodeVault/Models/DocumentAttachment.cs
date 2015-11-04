using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("Attachments", Schema = "CV2")]
    public class DocumentAttachment
    {
        [Key]
        public int AttachementId { get; set; }

        [Required]
        public string AttachmentName { get; set; }

        [Required]
        public byte[] AttachmentContents { get; set; }

        [Required]
        public string AttachmentExtension { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int? DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }
    }
}