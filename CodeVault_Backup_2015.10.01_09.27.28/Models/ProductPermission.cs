﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("ProductPermissions", Schema = "CV2")]
    public class ProductPermission
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        [DataMember]
        public bool ElevatedRightsRequired { get; set; }

        [DataMember]
        public bool RequiresAdminRightsBasic { get; set; }

        [DataMember]
        public bool RequiresAdminRightsAdvanced { get; set; }

        [DataMember]
        public bool RequiresAdminRightsUpdates { get; set; }

        public virtual Product Product { get; set; }
    }
}