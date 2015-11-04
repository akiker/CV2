using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("SccmSupersedences", Schema = "CV2")]
    public class SccmSupersedence
    {
        [Key]
        public int SccmSupersedenceId { get; set; }

        public int? ParentSccmSupersedenceId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}