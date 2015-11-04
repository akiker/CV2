using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("SccmDeploymentDetails", Schema = "CV2")]
    public class SccmDeploymentDetail
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public string SccmContentLocation { get; set; }

        public string SccmInstallCommand { get; set; }

        public string SccmUninstallCommand { get; set; }

        public string SccmDeploymentNotes { get; set; }

        public bool? SccmRebootRequired { get; set; }

        public string SccmOnDemandGroup { get; set; }

        public virtual Product Product { get; set; }
    }
}