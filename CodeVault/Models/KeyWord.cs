using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVault.Models
{
    [Table("KeyWords", Schema = "CV2")]
    public class KeyWord
    {
        public KeyWord()
        {
            Documents = new HashSet<Document>();
        }

        [Key]
        public int KeyWordId { get; set; }

        [Required]
        public string Word { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}