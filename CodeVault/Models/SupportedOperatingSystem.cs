using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("SupportedOperatingSystems", Schema = "CV2")]
    public class SupportedOperatingSystem
    {
        [Key]
        public int SupportedOperatingSystemId { get; set; }

        [Required]
        public string SupportedOperatingSystemName { get; set; }

        [Required]
        public OsBitness OsBitness { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}