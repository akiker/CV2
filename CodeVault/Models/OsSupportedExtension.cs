namespace CodeVault.Models
{
    public static class OsSupportedExtension
    {
        public static OsRequirement CreateOsSupportedWithDefaults()
        {
            var osRequirement = new OsRequirement
            {
                WindowsXp32Bit = true,
                WindowsXp64Bit = true,
                WindowsVista32Bit = true,
                WindowsVista64Bit = true,
                Windows732Bit = true,
                Windows764Bit = true,
                Windows832Bit = true,
                Windows864Bit = true,
                Windows8132Bit = true,
                Windows8164Bit = true
            };
            return osRequirement;
        }
    }
}