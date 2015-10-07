namespace CodeVault.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApplicationRoles", Schema = "CV2")]
    public partial class ApplicationRole
    {
        public ApplicationRole()
        {
            ApplicationPermissions = new HashSet<ApplicationPermission>();
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int ApplicationRoleId { get; set; }

        [Required]
        public string ApplicationRoleName { get; set; }

        [Required]
        public string ApplicationRoleDescription { get; set; }

        public DateTime CreatedOnDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ApplicationPermission> ApplicationPermissions { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}