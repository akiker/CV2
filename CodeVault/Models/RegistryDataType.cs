using System.ComponentModel;

namespace CodeVault.Models
{
    public enum RegistryDataType
    {
        [Description("String Value")] RegSz = 0,

        [Description("Binary Value")] RegBinary,

        [Description("DWORD (32-bit) Value")] RegDword,

        [Description("DWORD (64-bit) Value")] RegQword,

        [Description("Multi-String Value")] RegMultiSz,

        [Description("Expandable String Value")] RegExpandSz,

        [Description("Zero-Length Binary Value")] RegNone
    }
}