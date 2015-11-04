using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using CodeVault.Models.BaseTypes;
using Newtonsoft.Json;

namespace CodeVault.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("Products", Schema = "CV2")]
    public sealed class Product : EntityBase
    {
        public Product()
        {

        }

        [Key, ForeignKey("Request")]
        [DataMember]
        public int ProductId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name cannot be blank")]
        [DataMember]
        public string ProductName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Version cannot be blank")]
        [DataMember]
        public string ProductVersion { get; set; }

        [DataMember]
        public string ProductManufacturer { get; set; }

        [DataMember]
        public string ProductDescription { get; set; }

        [DataMember]
        public bool ProductIsDigitallySigned { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductUpgradeCode { get; set; }

        [DataMember]
        public int ProductDslId { get; set; }

        [DataMember]
        public int ProductCosmicId { get; set; }

        [DataMember]
        public string ProductCosmicMsiGuid { get; set; }

        [Required]
        [DataMember]
        public ProductStatus ProductStatus { get; set; }

        [DataMember]
        public DateTime CreatedOnDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        public int? LegacyId { get; set; }

        public int? ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        [DataMember]
        public ProductCategory ProductCategory { get; set; }

        //one to many (e.g. a product category has many products)

        public int? ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        [DataMember]
        public ProductType ProductType { get; set; }

        public int? ProjectTypeId { get; set; }

        [ForeignKey("ProjectTypeId")]
        [DataMember]
        public ProjectType ProjectType { get; set; }

        [DataMember]
        public InstallDetail InstallDetail { get; set; } //One to one

        [DataMember]
        public SccmDeploymentDetail SccmDeploymentDetail { get; set; }

        [DataMember]
        public ICollection<JournalEntry> JournalEntries { get; set; } = new HashSet<JournalEntry>();

        //one to many (e.g. a product has many journal entries)

        [DataMember]
        public ICollection<DistributionLocation> DistributionLocations { get; set; } = new HashSet<DistributionLocation>();

        [DataMember]
        public ICollection<SccmReturnCode> SccmReturnCodes { get; set; } = new HashSet<SccmReturnCode>();

        public int? CosmicConfigRecordId { get; set; }

        [DataMember]
        [ForeignKey("CosmicConfigRecordId")]
        public CosmicConfigRecord CosmicConfigRecord { get; set; }

        [DataMember]
        public OsRequirement OsRequirements { get; set; }

        [DataMember]
        public SoftwarePolicy SoftwarePolicy { get; set; }

        [DataMember]
        public SoftwareRequirement SoftwareRequirements { get; set; }

        [DataMember]
        public SystemRequirement SystemRequirements { get; set; }

        [DataMember]
        public ProductPermission ProductsPermissions { get; set; }

        [DataMember]
        public LaVerification LocalAccountVerification { get; set; }

        [DataMember]
        public ICollection<ProductPermissionDetail> ProductPermissionDetails { get; set; } = new HashSet<ProductPermissionDetail>();

        [DataMember]
        public ICollection<Document> Documents { get; set; } = new HashSet<Document>();

        [DataMember]
        public ICollection<License> Licenses { get; set; } = new HashSet<License>();

        [DataMember]
        public ICollection<ProductContact> ProductContacts { get; set; } = new HashSet<ProductContact>();

        [DataMember]
        public Request Request { get; set; }

        [DataMember]
        public ICollection<SccmRule> SccmRules { get; set; } = new HashSet<SccmRule>();

        [DataMember]
        public ICollection<Dependencies> Dependencies { get; set; } = new HashSet<Dependencies>();

        [DataMember]
        public ICollection<SupersededProducts> SupersededProducts { get; set; } = new HashSet<SupersededProducts>();

        public ICollection<ProductKeyWord> ProductKeyWords { get; set; } = new HashSet<ProductKeyWord>();

        protected override void RegisterValidationMethods()
        {
        }

        protected override void ResetProperties()
        {
        }
    }
}