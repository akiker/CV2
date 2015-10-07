namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LicenseKeys", Schema = "CV2")]
    public partial class LicenseKey
    {
        public int LicenseKeyId { get; set; }

        [Required]
        public string LicenseKeyData { get; set; }

        [Required]
        public string LicenseKeyOwnerName { get; set; }

        public string LicenseKeyOwnerEmail { get; set; }

        public string LicenseKeyOwnerPhoneNumber { get; set; }

        public int? LicenseId { get; set; }

        [ForeignKey("LicenseId")]
        public virtual License License { get; set; }
    }
}