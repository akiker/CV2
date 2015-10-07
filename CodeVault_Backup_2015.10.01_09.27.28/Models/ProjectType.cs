namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProjectTypes", Schema = "CV2")]
    public partial class ProjectType
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