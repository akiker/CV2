namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestStatus", Schema = "CV2")]
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        public int RequestStatusId { get; set; }

        [Required]
        public string RequestStatusName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}