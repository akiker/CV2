namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductTypes", Schema = "CV2")]
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ProductTypeId { get; set; }

        [Required]
        public string ProductTypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}