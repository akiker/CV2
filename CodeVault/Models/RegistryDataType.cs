namespace CodeVault.Models
{
    using System.ComponentModel;

    public enum RegistryDataType
    {
        [Description("String Value")]
        REG_SZ = 0,

        [Description("Binary Value")]
        REG_BINARY,

        [Description("DWORD (32-bit) Value")]
        REG_DWORD,

        [Description("DWORD (64-bit) Value")]
        REG_QWORD,

        [Description("Multi-String Value")]
        REG_MULTI_SZ,

        [Description("Expandable String Value")]
        REG_EXPAND_SZ,

        [Description("Zero-Length Binary Value")]
        REG_NONE
    }
}