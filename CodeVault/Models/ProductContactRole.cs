namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductContactRoles", Schema = "CV2")]
    public partial class ProductContactRole
    {
        public ProductContactRole()
        {
            ProductContacts = new HashSet<ProductContact>();
        }

        public int ProductContactRoleId { get; set; }

        [Required]
        public string ProductContactRoleName { get; set; }

        public virtual ICollection<ProductContact> ProductContacts { get; set; }
    }
}