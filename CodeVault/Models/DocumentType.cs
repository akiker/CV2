using System.ComponentModel;

namespace CodeVault.Models
{
    public enum DocumentType
    {
        [Description("Knowledge Base Article")]
        KB = 0,

        [Description("Support Article")]
        Support
    }
}