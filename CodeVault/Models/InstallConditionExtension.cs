namespace CodeVault.Models
{
    public static class InstallConditionExtension
    {
        public static SystemRequirement CreateNewSystemRequirementWithDefaults()
        {
            var systemRequirement = new SystemRequirement
            {
                MinimumColorQuality = string.Empty,
                MinimumPhysicalMemory = string.Empty,
                MinimumScreenResolution = string.Empty,
                MinimumWindowsInstallerVersion = string.Empty
            };
            return systemRequirement;
        }

        public static SoftwareRequirement CreateNewSoftwareRequirementWithDefaults()
        {
            var softwareRequirement = new SoftwareRequirement
            {
                InternetExplorerVersion = string.Empty,
                IisVersion = string.Empty,
                DotNetVersion = string.Empty,
                AdobeReaderVersion = string.Empty,
                JavaRuntimeVersion = string.Empty,
                JdkVersion = string.Empty,
                DirectXVersion = string.Empty,
                InstalledOfficeApplicationName = string.Empty,
                InstalledOfficeApplicationVersion = string.Empty,
                SqlExpressVersion = string.Empty,
                SqlCompactVersion = string.Empty,
                VstoRuntimeVersion = string.Empty,
                InstalledOffice2003Pia = string.Empty,
                InstalledOffice2007Pia = string.Empty,
                InstalledOffice2010Pia = string.Empty,
                OfficeSharedInteropAssembly = string.Empty,
                PowerShellVersion = string.Empty
            };
            return softwareRequirement;
        }

        public static OsRequirement CreateNewOperatingSystemRequirementWithDefaults()
        {
            var operatingSystemRequirement = new OsRequirement
            {
                WindowsXp32Bit = true,
                WindowsVista32Bit = true,
                Windows732Bit = true,
                Windows832Bit = true,
                Windows8132Bit = true,
                WindowsXp64Bit = true,
                WindowsVista64Bit = true,
                Windows764Bit = true,
                Windows864Bit = true,
                Windows8164Bit = true
            };
            return operatingSystemRequirement;
        }
    }
}