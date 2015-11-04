using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("OsRequirements", Schema = "CV2")]
    public class OsRequirement
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public bool WindowsXp32Bit { get; set; }

        public bool WindowsVista32Bit { get; set; }

        public bool Windows732Bit { get; set; }

        public bool Windows832Bit { get; set; }

        public bool Windows8132Bit { get; set; }

        public bool Windows1032Bit { get; set; }

        public bool WindowsXp64Bit { get; set; }

        public bool WindowsVista64Bit { get; set; }

        public bool Windows764Bit { get; set; }

        public bool Windows864Bit { get; set; }

        public bool Windows8164Bit { get; set; }

        public bool Windows1064Bit { get; set; }

        public virtual Product Product { get; set; }
    }
}