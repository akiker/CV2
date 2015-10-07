namespace CodeVault.Models
{
    using System.ComponentModel;

    public enum FileOrFolderRuleType
    {
        [Description("File")]
        File = 0,

        [Description("Folder")]
        Folder
    }
}