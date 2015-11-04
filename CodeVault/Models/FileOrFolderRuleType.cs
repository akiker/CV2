using System.ComponentModel;

namespace CodeVault.Models
{
    public enum FileOrFolderRuleType
    {
        [Description("File")] File = 0,

        [Description("Folder")] Folder
    }
}