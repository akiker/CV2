using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntityFramework.Triggers;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("ApplicationPermissions", Schema = "CV2")]
    public class ApplicationPermission : ITriggerable
    {
        [DataMember]
        [Key]
        public int ApplicationPermissionId { get; set; }

        [DataMember]
        public bool Approve { get; set; }

        [DataMember]
        public bool UserAdmin { get; set; }

        public int ApplicationRoleId { get; set; }

        [DataMember]
        [ForeignKey("ApplicationRoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}