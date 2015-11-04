using System.ComponentModel;

namespace CodeVault.Models
{
    public enum FileOrFolderRulePropertyType
    {
        [Description("Version")] Version = 0,

        [Description("Date Modified")] DateModified,

        [Description("Date Created")] DateCreated,

        [Description("Size (Bytes)")] SizeBytes
    }
}