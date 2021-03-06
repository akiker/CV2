using System.ComponentModel.DataAnnotations;

namespace CodeVault.Models
{
    public class SccmWindowInstallerDetectionRule : SccmRule
    {
        [Required]
        public string ProductCode { get; set; }

        public bool MsiProductCodeMustExist { get; set; }

        [Required]
        public WindowsInstallerRulePropertyType MsiRuleProperty { get; set; }

        [Required]
        public WindowsInstallerRuleOperatorType MsiRuleOperator { get; set; }

        public string MsiRuleValue { get; set; }

        //public readonly SccmRuleType SccmRuleType = SccmRuleType.WindowsInstaller;
    }
}