namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LicenseTypes", Schema = "CV2")]
    public partial class LicenseType
    {
        [Key]
        public int LicenseTypeId { get; set; }

        [Required]
        public string LicenseTypeName { get; set; }
    }
}