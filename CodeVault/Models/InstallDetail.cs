using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntityFramework.Triggers;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("InstallDetails", Schema = "CV2")]
    public class InstallDetail : ITriggerable
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        [DataMember]
        public string InstallDetailFileName { get; set; }

        [DataMember]
        public string InstallDetailTransformFileName { get; set; }

        [DataMember]
        public long InstallDetailFileSize { get; set; }

        [DataMember]
        public bool InstallDetailRebootRequired { get; set; }

        [DataMember]
        public string InstallDetailInstallCommand { get; set; }

        [DataMember]
        public string InstallDetailUninstallCommand { get; set; }

        [DataMember]
        public string InstallDetailSourceLocation { get; set; }

        public virtual Product Product { get; set; }
    }
}