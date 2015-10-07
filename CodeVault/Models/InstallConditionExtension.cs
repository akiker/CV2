
namespace CodeVault.Models
{
    public static class InstallConditionExtension
    {
        public static SystemRequirement CreateNewSystemRequirementWithDefaults()
        {
            var systemRequirement = new SystemRequirement()
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
            var softwareRequirement = new SoftwareRequirement()
            {
                InternetExplorerVersion = string.Empty,
                IISVersion = string.Empty,
                DotNetVersion = string.Empty,
                AdobeReaderVersion = string.Empty,
                JavaRuntimeVersion = string.Empty,
                JDKVersion = string.Empty,
                DirectXVersion = string.Empty,
                InstalledOfficeApplicationName = string.Empty,
                InstalledOfficeApplicationVersion = string.Empty,
                SQLExpressVersion = string.Empty,
                SQLCompactVersion = string.Empty,
                VSTORuntimeVersion = string.Empty,
                InstalledOffice2003PIA = string.Empty,
                InstalledOffice2007PIA = string.Empty,
                InstalledOffice2010PIA = string.Empty,
                OfficeSharedInteropAssembly = string.Empty,
                PowerShellVersion = string.Empty
            };
            return softwareRequirement;
        }

        public static OsRequirement CreateNewOperatingSystemRequirementWithDefaults()
        {
            var operatingSystemRequirement = new OsRequirement()
             {
                 WindowsXP_32Bit = true,
                 WindowsVista_32Bit = true,
                 Windows7_32Bit = true,
                 Windows8_32Bit = true,
                 Windows81_32Bit = true,
                 WindowsXP_64Bit = true,
                 WindowsVista_64Bit = true,
                 Windows7_64Bit = true,
                 Windows8_64Bit = true,
                 Windows81_64Bit = true
             };
            return operatingSystemRequirement;
        }
    }
}
