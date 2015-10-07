namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductContacts", Schema = "CV2")]
    public partial class ProductContact
    {
        [Key]
        public int ProductContactId { get; set; }

        [Required]
        public string ProductContactLoginId { get; set; }

        [Required]
        public string ProductContactName { get; set; }

        [Required]
        public string ProductContactEmail { get; set; }

        [Required]
        public string ProductContactPhoneNumber { get; set; }

        public int ProductContactRoleId { get; set; }

        [ForeignKey("ProductContactRoleId")]
        public virtual ProductContactRole ProductContactRole { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}