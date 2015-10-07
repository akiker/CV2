namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SystemRequirements", Schema = "CV2")]
    public partial class SystemRequirement
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public string MinimumWindowsInstallerVersion { get; set; }

        public string MinimumScreenResolution { get; set; }

        public string MinimumPhysicalMemory { get; set; }

        public string MinimumColorQuality { get; set; }

        public virtual Product Product { get; set; }
    }
}