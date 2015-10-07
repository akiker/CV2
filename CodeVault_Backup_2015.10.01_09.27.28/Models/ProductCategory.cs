namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductCategories", Schema = "CV2")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ProductCategoryId { get; set; }

        [Required]
        public string ProductCategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}