using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("JournalEntries", Schema = "CV2")]
    public class JournalEntry
    {
        [Key]
        public int JournalEntryId { get; set; }

        public JournalEntryType JournalEntryType { get; set; }

        [Required]
        public string JournalEntryText { get; set; }

        [Required]
        public string JournalEntryCreatedBy { get; set; }

        public DateTime JournalEntryCreatedOn { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}