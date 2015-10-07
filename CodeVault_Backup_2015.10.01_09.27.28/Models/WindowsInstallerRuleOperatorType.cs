namespace CodeVault.Models
{
    using System.ComponentModel;

    public enum WindowsInstallerRuleOperatorType
    {
        [Description("Equals")]
        Equals = 0,

        [Description("Not equal to")]
        NotEqualTo,

        [Description("Greater than or equal to")]
        GreaterThanOrEqualTo,

        [Description("Greater than")]
        GreaterThan,

        [Description("Less than or equal to")]
        LessThanOrEqualTo,

        [Description("Less than")]
        LessThan
    }
}