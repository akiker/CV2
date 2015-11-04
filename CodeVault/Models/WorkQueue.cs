using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("WorkQueues", Schema = "CV2")]
    public class WorkQueue
    {
        public WorkQueue()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Requests = new HashSet<Request>();
        }

        [Key]
        public int WorkQueueId { get; set; }

        public DateTime CreatedOnDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}