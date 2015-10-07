namespace CodeVault.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SccmSupersedences",Schema="CV2")]
    public partial class SccmSupersedence
    {
        [Key]
        public int SccmSupersedenceId { get; set; }

        public int? ParentSccmSupersedenceId { get; set; }

        public int ProductId { get;set;}

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
