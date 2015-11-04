using System.ComponentModel;

namespace CodeVault.Models
{
    public enum RegistryRuleOperatorType
    {
        [Description("Equals")] Equals = 0,

        [Description("Not equal to")] NotEqualTo,

        [Description("Greater than or equal to")] GreaterThanOrEqualTo,

        [Description("Greater than")] GreaterThan,

        [Description("Less than")] LessThan,

        [Description("Less than or equal to")] LessThanOrEqualTo,

        [Description("One of")] OneOf,

        [Description("None of")] NoneOf
    }
}