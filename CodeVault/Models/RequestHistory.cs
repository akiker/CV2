using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("RequestHistories", Schema = "CV2")]
    public class RequestHistory
    {
        [Key]
        public int RequestHistoryId { get; set; }

        [Required]
        public string RequestHistoryEntry { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public int? RequestId { get; set; }

        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }
    }
}