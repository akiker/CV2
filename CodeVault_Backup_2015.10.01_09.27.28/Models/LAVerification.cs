﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("LocalAccountVerifications", Schema = "CV2")]
    public class LAVerification
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        [DataMember]
        public bool LocalAdminVerificationComplete { get; set; }

        [DataMember]
        public bool WorksWithLocalAdminAccount { get; set; }

        public virtual Product Product { get; set; }
    }
}