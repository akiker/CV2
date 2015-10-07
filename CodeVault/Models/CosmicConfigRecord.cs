namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CosmicConfigRecords", Schema = "CV2")]
    public partial class CosmicConfigRecord
    {
        public CosmicConfigRecord()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CosmicConfigRecordId { get; set; }

        [Required]
        public string CosmicConfigRecordName { get; set; }

        [Required]
        public string CosmicConfigRecordCreatedBy { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}