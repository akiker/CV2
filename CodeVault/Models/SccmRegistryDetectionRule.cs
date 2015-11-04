using System.ComponentModel.DataAnnotations;

namespace CodeVault.Models
{
    public class SccmRegistryDetectionRule : SccmRule
    {
        [Required]
        public RegistryHiveType RegistryHive { get; set; }

        [Required]
        public string RegistryKey { get; set; }

        public string RegistryValue { get; set; }

        public bool UseDefaultValue { get; set; }

        public bool RegKeyAssociate32On64 { get; set; }

        public RegistryDataType RegistryDataType { get; set; }

        public bool RegKeyMustExist { get; set; }

        public RegistryRuleOperatorType RegRuleOperator { get; set; }

        public string RegRuleValue { get; set; }
    }
}