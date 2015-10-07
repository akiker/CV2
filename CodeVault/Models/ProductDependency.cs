namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductDependencies", Schema = "CV2")]
    public partial class ProductDependency
    {
        [Key]
        public int ProductDependencyId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId"), Column(Order = 0)]
        public virtual Product Product { get; set; }

        public int DependencyId { get; set; }

        [ForeignKey("DependencyId"), Column(Order = 1)]
        public virtual Product Dependency { get; set; }

        public int InstallOrder { get; set; }

        public DependencyType DependencyType { get; set; }
    }
}