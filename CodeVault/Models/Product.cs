namespace CodeVault.Models
{
    using CodeVault.Models.BaseTypes;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/CodeVault.Models")]
    [Table("Products", Schema = "CV2")]
    public partial class Product : EntityBase
    {
        public Product()
        {
            Dependencies = new HashSet<Dependencies>();
            DistributionLocations = new HashSet<DistributionLocation>();
            Documents = new HashSet<Document>();
            JournalEntries = new HashSet<JournalEntry>();
            Licenses = new HashSet<License>();
            ProductContacts = new HashSet<ProductContact>();
            SccmReturnCodes = new HashSet<SccmReturnCode>();
            SccmRules = new HashSet<SccmRule>();
            SupersededProducts = new HashSet<SupersededProducts>();
            ProductKeyWords = new HashSet<ProductKeyWord>();
            ProductPermissionDetails = new HashSet<ProductPermissionDetail>();
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
        public virtual ProductCategory ProductCategory { get; set; } //one to many (e.g. a product category has many products)

        public int? ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        [DataMember]
        public virtual ProductType ProductType { get; set; }

        public int? ProjectTypeId { get; set; }

        [ForeignKey("ProjectTypeId")]
        [DataMember]
        public virtual ProjectType ProjectType { get; set; }

        [DataMember]
        public virtual InstallDetail InstallDetail { get; set; } //One to one

        [DataMember]
        public virtual SccmDeploymentDetail SccmDeploymentDetail { get; set; }

        [DataMember]
        public virtual ICollection<JournalEntry> JournalEntries { get; set; } //one to many (e.g. a product has many journal entries)

        [DataMember]
        public virtual ICollection<DistributionLocation> DistributionLocations { get; set; }

        [DataMember]
        public virtual ICollection<SccmReturnCode> SccmReturnCodes { get; set; }

        public int? CosmicConfigRecordId { get; set; }

        [DataMember]
        [ForeignKey("CosmicConfigRecordId")]
        public virtual CosmicConfigRecord CosmicConfigRecord { get; set; }

        [DataMember]
        public virtual OsRequirement OsRequirements { get; set; }

        [DataMember]
        public virtual SoftwarePolicy SoftwarePolicy { get; set; }

        [DataMember]
        public virtual SoftwareRequirement SoftwareRequirements { get; set; }

        [DataMember]
        public virtual SystemRequirement SystemRequirements { get; set; }

        [DataMember]
        public virtual ProductPermission ProductsPermissions { get; set; }

        [DataMember]
        public virtual LAVerification LocalAccountVerification { get; set; }

        [DataMember]
        public virtual ICollection<ProductPermissionDetail> ProductPermissionDetails { get; set; }

        [DataMember]
        public virtual ICollection<Document> Documents { get; set; }

        [DataMember]
        public virtual ICollection<License> Licenses { get; set; }

        [DataMember]
        public virtual ICollection<ProductContact> ProductContacts { get; set; }

        [DataMember]
        public virtual Request Request { get; set; }

        [DataMember]
        public virtual ICollection<SccmRule> SccmRules { get; set; }

        [DataMember]
        public virtual ICollection<Dependencies> Dependencies { get; set; }

        [DataMember]
        public virtual ICollection<SupersededProducts> SupersededProducts { get; set; }

        public virtual ICollection<ProductKeyWord> ProductKeyWords { get; set; }

        protected override void RegisterValidationMethods()
        {
        }

        protected override void ResetProperties()
        {
        }
    }
}