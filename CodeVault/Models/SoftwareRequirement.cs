using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("SoftwareRequirements", Schema = "CV2")]
    public class SoftwareRequirement
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public string InternetExplorerVersion { get; set; }

        public string IisVersion { get; set; }

        public string DotNetVersion { get; set; }

        public string AdobeReaderVersion { get; set; }

        public string JavaRuntimeVersion { get; set; }

        public string JdkVersion { get; set; }

        public string DirectXVersion { get; set; }

        public string InstalledOfficeApplicationName { get; set; }

        public string InstalledOfficeApplicationVersion { get; set; }

        public string SqlExpressVersion { get; set; }

        public string SqlCompactVersion { get; set; }

        public string VstoRuntimeVersion { get; set; }

        public string InstalledOffice2003Pia { get; set; }

        public string InstalledOffice2007Pia { get; set; }

        public string InstalledOffice2010Pia { get; set; }

        public string OfficeSharedInteropAssembly { get; set; }

        public string PowerShellVersion { get; set; }

        public virtual Product Product { get; set; }
    }
}