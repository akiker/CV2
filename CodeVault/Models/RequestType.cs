using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("RequestTypes", Schema = "CV2")]
    public class RequestType
    {
        [Key]
        public int RequestTypeId { get; set; }

        [Required]
        public string RequestValue { get; set; }

        [Required]
        public string RequestValueHelpText { get; set; }
    }
}