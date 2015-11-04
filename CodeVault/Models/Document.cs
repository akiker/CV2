using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("Documents", Schema = "CV2")]
    public class Document
    {
        public Document()
        {
            KeyWords = new HashSet<KeyWord>();
            //Attachments = new HashSet<DocumentAttachment>();
        }

        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string DocumentTitle { get; set; }

        public DocumentType DocumentType { get; set; }

        [Required]
        public byte[] DocumentContents { get; set; }

        [Required]
        public string DocumentExtension { get; set; }

        [Required]
        public string DocumentCreatedBy { get; set; }

        public DateTime DocumentCreatedOn { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public virtual ICollection<KeyWord> KeyWords { get; set; }

        public virtual ICollection<DocumentAttachment> Attachments { get; set; }
    }
}