using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("LicenseTypes", Schema = "CV2")]
    public class LicenseType
    {
        [Key]
        public int LicenseTypeId { get; set; }

        [Required]
        public string LicenseTypeName { get; set; }
    }
}