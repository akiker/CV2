using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("ProductKeyWords", Schema = "CV2")]
    public class ProductKeyWord
    {
        public ProductKeyWord()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ProductKeyWordId { get; set; }

        [Required]
        public string Word { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}