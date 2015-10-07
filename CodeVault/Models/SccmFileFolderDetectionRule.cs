namespace CodeVault.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class SccmFileFolderDetectionRule : SccmRule
    {
        [Required]
        public string FileOrFolderPath { get; set; }

        [Required]
        public string FileOrFolderName { get; set; }

        public bool FileOrFolderAssociate32On64 { get; set; }

        public bool FileOrFolderMustExist { get; set; }

        [EnumDataType(typeof(FileOrFolderRuleType))]
        [Display(Name = "FileOrFolderRuleType", Description = "Indicates if this is a file or folder rule")]
        public FileOrFolderRuleType FileOrFolderRuleType { get; set; }

        [EnumDataType(typeof(FileOrFolderRulePropertyType))]
        [Display(Name = "FileOrFolderRulePropertyType", Description = "The property value the rule is set to (ex: Version)")]
        public FileOrFolderRulePropertyType FileOrFolderRuleProperty { get; set; }

        [EnumDataType(typeof(FileOrFolderRuleOperatorType))]
        [Display(Name = "FileOrFOlderRuleOperatorType", Description = "The operator value the rule is set to (ex: Equals")]
        public FileOrFolderRuleOperatorType FileOrFolderRuleOperator { get; set; }

        [Display(Name = "FileOrFolderRuleValue", Description = "Holds the string value that the Property must match to (ex: 2.0)", Prompt = "Enter the Value that the rules must match to")]
        public string FileOrFolderRuleValue { get; set; }
    }
}