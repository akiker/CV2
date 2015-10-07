namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OsRequirements", Schema = "CV2")]
    public partial class OsRequirement
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public bool WindowsXP_32Bit { get; set; }

        public bool WindowsVista_32Bit { get; set; }

        public bool Windows7_32Bit { get; set; }

        public bool Windows8_32Bit { get; set; }

        public bool Windows81_32Bit { get; set; }

        public bool Windows10_32Bit { get; set; }

        public bool WindowsXP_64Bit { get; set; }

        public bool WindowsVista_64Bit { get; set; }

        public bool Windows7_64Bit { get; set; }

        public bool Windows8_64Bit { get; set; }

        public bool Windows81_64Bit { get; set; }

        public bool Windows10_64Bit { get; set; }

        public virtual Product Product { get; set; }
    }
}