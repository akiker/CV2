using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("ProjectTypes", Schema = "CV2")]
    public class ProjectType
    {
        public ProjectType()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ProjectTypeId { get; set; }

        [Required]
        public string ProjectTypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}