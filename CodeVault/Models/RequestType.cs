namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestTypes", Schema = "CV2")]
    public partial class RequestType
    {
        [Key]
        public int RequestTypeId { get; set; }

        [Required]
        public string RequestValue { get; set; }

        [Required]
        public string RequestValueHelpText { get; set; }
    }
}