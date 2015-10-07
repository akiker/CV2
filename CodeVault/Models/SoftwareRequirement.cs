namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SoftwareRequirements", Schema = "CV2")]
    public partial class SoftwareRequirement
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public string InternetExplorerVersion { get; set; }

        public string IISVersion { get; set; }

        public string DotNetVersion { get; set; }

        public string AdobeReaderVersion { get; set; }

        public string JavaRuntimeVersion { get; set; }

        public string JDKVersion { get; set; }

        public string DirectXVersion { get; set; }

        public string InstalledOfficeApplicationName { get; set; }

        public string InstalledOfficeApplicationVersion { get; set; }

        public string SQLExpressVersion { get; set; }

        public string SQLCompactVersion { get; set; }

        public string VSTORuntimeVersion { get; set; }

        public string InstalledOffice2003PIA { get; set; }

        public string InstalledOffice2007PIA { get; set; }

        public string InstalledOffice2010PIA { get; set; }

        public string OfficeSharedInteropAssembly { get; set; }

        public string PowerShellVersion { get; set; }

        public virtual Product Product { get; set; }
    }
}