namespace CodeVault.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("JournalCategories", Schema = "CV2")]
    public partial class JournalCategory
    {
        public JournalCategory()
        {
            JournalEntries = new HashSet<JournalEntry>();
        }

        [Key]
        public int JournalCategoryId { get; set; }

        [Required]
        public string JournalCategoryNameValue { get; set; }

        public virtual ICollection<JournalEntry> JournalEntries { get; set; }
    }
}