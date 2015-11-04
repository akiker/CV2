using System.ComponentModel;

namespace CodeVault.Models
{
    public enum DocumentType
    {
        [Description("Knowledge Base Article")] Kb = 0,

        [Description("Support Article")] Support
    }
}