namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SupportedOperatingSystems", Schema = "CV2")]
    public partial class SupportedOperatingSystem
    {
        [Key]
        public int SupportedOperatingSystemId { get; set; }

        [Required]
        public string SupportedOperatingSystemName { get; set; }

        [Required]
        public OSBitness OSBitness { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}