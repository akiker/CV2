namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cv2.SccmReturnCodes")]
    public partial class SccmReturnCode
    {
        public int SccmReturnCodeId { get; set; }

        public int ReturnCode { get; set; }

        [Required]
        public string ReturnCodeDescription { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}