using System.Collections.Generic;
using System.Linq;

namespace CodeVault.Models.Utilities
{
    public class PropertyMapper
    {
        private readonly Dictionary<string, string> _propertyMapDictionary = new Dictionary<string, string>();

        public PropertyMapper()
        {
            InitializeMappingDictionary();
        }

        public string GetFriendlyTextForProperty(string propertyName)
        {
            var friendlyName =
                _propertyMapDictionary.Where(p => p.Key == propertyName).Select(p => p.Value).FirstOrDefault();
            return friendlyName;
        }

        private void InitializeMappingDictionary()
        {
            _propertyMapDictionary.Add("ProductName", "Name");

            _propertyMapDictionary.Add("ProductManufacturer", "Manufacturer");

            _propertyMapDictionary.Add("ProductVersion", "Version");

            _propertyMapDictionary.Add("ProductDescription", "Description");

            _propertyMapDictionary.Add("ProductIsDigitallySigned", "Digitally Signed");

            _propertyMapDictionary.Add("ProductCode", "Product Code");

            _propertyMapDictionary.Add("ProductUpgradeCode", "Upgrade Code");

            _propertyMapDictionary.Add("ProductDslId", "DSL ID");

            _propertyMapDictionary.Add("ProductCosmicId", "COSMIC ID");

            _propertyMapDictionary.Add("ProductCosmicMsiGuid", "COSMIC MSI GUID");

            _propertyMapDictionary.Add("ProductStatus", "Status");

            _propertyMapDictionary.Add("ProductCategory", "Category");

            _propertyMapDictionary.Add("ProductType", "Type");

            _propertyMapDictionary.Add("ProductTypeId", "Type");

            _propertyMapDictionary.Add("ProductKeyWord", "Key Word");

            _propertyMapDictionary.Add("InstallDetailFileName", "Install File Name");

            _propertyMapDictionary.Add("InstallDetailTransformFileName", "Transform File Name");

            _propertyMapDictionary.Add("InstallDetailFileSize", "Install File Size");

            _propertyMapDictionary.Add("InstallDetailRebootRequired", "Reboot Required");

            _propertyMapDictionary.Add("InstallDetailInstallCommand", "Install Command");

            _propertyMapDictionary.Add("InstallDetailUninstallCommand", "Uninstall Command");

            _propertyMapDictionary.Add("InstallDetailSourceLocation", "Source Location");

            _propertyMapDictionary.Add("SccmContentLocation", "SCCM - Source Location");

            _propertyMapDictionary.Add("SccmInstallCommand", "SCCM - Install Command");

            _propertyMapDictionary.Add("SccmUninstallCommand", "SCCM - Uninstall Command");

            _propertyMapDictionary.Add("ScmmDeploymentNotes", "SCCM - Deployment Notes");

            _propertyMapDictionary.Add("SccmRebootRequired", "SCCM - Reboot Required");

            _propertyMapDictionary.Add("SccmOnDemandGroup", "SCCM - On-Demand AD Group");

            _propertyMapDictionary.Add("SccmReturnCode", "SCCM - Return Code");

            _propertyMapDictionary.Add("SccmRule", "SCCM Rule");

            _propertyMapDictionary.Add("SccmRuleOrder", "SCCM Rule - Order");

            _propertyMapDictionary.Add("SccmRuleNote", "SCCM Rule - Note");

            _propertyMapDictionary.Add("SccmRuleConnector", "SCCM Rule - Connector");

            _propertyMapDictionary.Add("DistributionPath", "Distribution Path");

            _propertyMapDictionary.Add("DistributionLocation", "Distribution Path");

            _propertyMapDictionary.Add("DistributionMethod", "Distribution Method");

            _propertyMapDictionary.Add("ReturnCode", "SCCM - Return Code");

            _propertyMapDictionary.Add("ReturnCodeDescription", "SCCM - Return Code Description");

            _propertyMapDictionary.Add("WindowsXP_32Bit", "Requirement - Windows XP 32-bit");

            _propertyMapDictionary.Add("WindowsVista_32Bit", "Requirement - Windows Vista 32-bit");

            _propertyMapDictionary.Add("Windows7_32Bit", "Requirement - Windows 7 32-bit");

            _propertyMapDictionary.Add("Windows8_32Bit", "Requirement - Windows 8 32-bit");

            _propertyMapDictionary.Add("Windows81_32Bit", "Requirement - Windows 8.1 32-bit");

            _propertyMapDictionary.Add("Windows10_32Bit", "Requirement - Windows 10 32-bit");

            _propertyMapDictionary.Add("WindowsXP_64Bit", "Requirement - Windows XP 64-bit");

            _propertyMapDictionary.Add("WindowsVista_64Bit", "Requirement - Windows Vista 64-bit");

            _propertyMapDictionary.Add("Windows7_64Bit", "Requirement - Windows 7 64-bit");

            _propertyMapDictionary.Add("Windows8_64Bit", "Requirement - Windows 8 64-bit");

            _propertyMapDictionary.Add("Windows81_64Bit", "Requirement - Windows 8.1 64-bit");

            _propertyMapDictionary.Add("Windows10_64Bit", "Requirement - Windows 10 64-bit");

            _propertyMapDictionary.Add("SoftwarePolicySupportLevel", "Software Policy - Support Level");

            _propertyMapDictionary.Add("SoftwarePolicySupportLevelId", "Software Policy - Support Level");

            _propertyMapDictionary.Add("SoftwarePolicyADGroupName", "Software Policy - AD Group");

            _propertyMapDictionary.Add("SoftwarePolicyGroupAssociation", "Software Policy - Group Association");

            _propertyMapDictionary.Add("SoftwarePolicyGroupDisplayName", "Software Policy - AD Group Display Name");

            _propertyMapDictionary.Add("SoftwarePolicyGroupDescription", "Software Policy - AD Group Description");

            _propertyMapDictionary.Add("InternetExplorerVersion", "Requirement - Internet Explorer Version");

            _propertyMapDictionary.Add("IISVersion", "Requirement - IIS Version");

            _propertyMapDictionary.Add("DotNetVersion", "Requirement - .NET Version");

            _propertyMapDictionary.Add("AdobeReaderVersion", "Requirement - Adobe Reader Version");

            _propertyMapDictionary.Add("JavaRuntimeVersion", "Requirement - Java Runtime Version");

            _propertyMapDictionary.Add("JDKVersion", "Requirement - JDK Version");

            _propertyMapDictionary.Add("DirectXVersion", "Requirement - DirectX Version");

            _propertyMapDictionary.Add("InstalledOfficeApplicationName", "Requirement - Office Application");

            _propertyMapDictionary.Add("InstalledOfficeApplicationVersion", "Requirement - Office Application Version");

            _propertyMapDictionary.Add("SQLExpressVersion", "Requirement - SQL Express Version");

            _propertyMapDictionary.Add("SQLCompactVersion", "Requirement - SQL Compact Version");

            _propertyMapDictionary.Add("VSTORuntimeVersion", "Requirement - VSTO Version");

            _propertyMapDictionary.Add("InstalledOffice2003PIA", "Requirement - Office 2003 PIA");

            _propertyMapDictionary.Add("InstalledOffice2007PIA", "Requirement - Office 2007 PIA");

            _propertyMapDictionary.Add("InstalledOffice2010PIA", "Requirement - Office 2010 PIA");

            _propertyMapDictionary.Add("OfficeSharedInteropAssembly", "Requirement - Office Shared Interop Assembly");

            _propertyMapDictionary.Add("PowerShellVersion", "Requirement - PowerShell Version");

            _propertyMapDictionary.Add("MinimumScreenResolution", "Requirement - Minimum Screen Resolution");

            _propertyMapDictionary.Add("MinimumWindowsInstallerVersion",
                "Requirement - Minimum Windows Installer Version");

            _propertyMapDictionary.Add("MinimumPhysicalMemory", "Requirement - Minimum Physical Memory");

            _propertyMapDictionary.Add("MinimumColorQuality", "Requirement - Minimum Color Quality");

            _propertyMapDictionary.Add("ElevatedRightsRequired", "Elevated Rights Required");

            _propertyMapDictionary.Add("RequiresAdminRightsBasic",
                "Administrative Rights - Required for Basic Operation");

            _propertyMapDictionary.Add("RequiresAdminRightsAdvanced",
                "Administrative Rights - Required for Advanced Operation");

            _propertyMapDictionary.Add("RequiresAdminRightsUpdates", "Administrative Rights - Required for Updates");

            _propertyMapDictionary.Add("LocalAdminVerificationComplete",
                "Local Admin. Verification - Verification Complete");

            _propertyMapDictionary.Add("WorksWithLocalAdminAccount",
                "Local Admin. Verification - Works with Local Administrator Account");

            _propertyMapDictionary.Add("ProductPermissionDetailAcl", "Permission Detail - ACL");

            _propertyMapDictionary.Add("ProductPermissionDetailType", "Permission Detail - Type");

            _propertyMapDictionary.Add("ProductPermissionLocation", "Permission Detail - Path");

            _propertyMapDictionary.Add("ProductPermissionGroupOrUserName", "Permission Detail - AD Group or User Name");

            _propertyMapDictionary.Add("ProductPermissionDetail", "Permission - Detail");

            _propertyMapDictionary.Add("ProductPermission", "Permission");

            _propertyMapDictionary.Add("DocumentTitle", "Document - Name");

            _propertyMapDictionary.Add("DocumentType", "Document - Type");

            _propertyMapDictionary.Add("DocumentTypeId", "Document - Type");

            _propertyMapDictionary.Add("DocumentContents", "Document - Size");

            _propertyMapDictionary.Add("AttachmentName", "Attachment - Name");

            _propertyMapDictionary.Add("Word", "Document - Keyword");

            _propertyMapDictionary.Add("LicenseSku", "License - SKU");

            _propertyMapDictionary.Add("LicenseTypeId", "License - Type");

            _propertyMapDictionary.Add("LicenseNotes", "License - Notes");

            _propertyMapDictionary.Add("LicenseOwner", "License - Owner");

            _propertyMapDictionary.Add("LicenseKeyData", "License Key - Data");

            _propertyMapDictionary.Add("LicenseKeyOwnerName", "License Key - Owner");

            _propertyMapDictionary.Add("LicenseKeyOwnerEmail", "License Key - Owner Email");

            _propertyMapDictionary.Add("LicenseKey", "License Key");

            _propertyMapDictionary.Add("LicenseKeyOwnerPhoneNumber", "License Key - Owner Phone Number");

            _propertyMapDictionary.Add("ProductContactLoginId", "Contact - Login ID");

            _propertyMapDictionary.Add("ProductContactEmail", "Contact - Email");

            _propertyMapDictionary.Add("ProductContactName", "Contact - Name");

            _propertyMapDictionary.Add("ProductContactPhoneNumber", "Contact - Phone Number");

            _propertyMapDictionary.Add("ProductContactRole", "Contact - Role");

            _propertyMapDictionary.Add("ProductContactRoleId", "Contact - Role");

            _propertyMapDictionary.Add("FileOrFolderPath", "SCCM Rule - File or Folder Path");

            _propertyMapDictionary.Add("FileOrFolderName", "SCCM Rule - File or Folder Name");

            _propertyMapDictionary.Add("FileOrFolderMustExist", "SCCM Rule - File or Folder Must Exist");

            _propertyMapDictionary.Add("FileOrFolderRuleType", "SCCM Rule - File or Folder Rule Type");

            _propertyMapDictionary.Add("FileOrFolderRuleTypeId", "SCCM Rule - File or Folder Rule Type");

            _propertyMapDictionary.Add("FileOrFolderRuleOperatorType", "SCCM Rule - File or Folder Operator");

            _propertyMapDictionary.Add("FileOrFolderRuleValue", "SCCM Rule - File or Folder Rule Value");

            _propertyMapDictionary.Add("RegistryValue", "SCCM Rule - Registry Value");

            _propertyMapDictionary.Add("UseDefaultValue", "SCCM Rule - Registry Use Default Value");

            _propertyMapDictionary.Add("RegKeyAssociate32On64",
                "SCCM Rule - Registry Key Associated with 32-bit on 64-bit");

            _propertyMapDictionary.Add("RegistryDataType", "SCCM Rule - Registry Data Type");

            _propertyMapDictionary.Add("RegKeyMustExist", "SCCM Rule - Registry Key Must Exist");

            _propertyMapDictionary.Add("RegistryRuleOperatorType", "SCCM Rule - Registry Operator Type");

            _propertyMapDictionary.Add("RegRuleValue", "SCCM Rule - Registry Rule Value");

            _propertyMapDictionary.Add("MsiProductCodeMustExist", "SCCM Rule - MSI Product Code Must Exist");

            _propertyMapDictionary.Add("MsiRuleProperty", "SCCM Rule - MSI Rule Property");

            _propertyMapDictionary.Add("MsiRuleOperator", "SCCM Rule - MSI Rule Operator");

            _propertyMapDictionary.Add("MsiRuleValue", "SCCM Rule - MSI Rule Value");

            _propertyMapDictionary.Add("Dependency", "Dependency");

            _propertyMapDictionary.Add("Dependencies", "Dependency");

            _propertyMapDictionary.Add("InstallOrder", "Install Order");

            _propertyMapDictionary.Add("DependencyType", "Dependency Type");

            _propertyMapDictionary.Add("SupersededProducts", "Supersedence");

            _propertyMapDictionary.Add("DocumentAttachment", "Attachment");

            _propertyMapDictionary.Add("ProductContact", "Contact");
        }
    }
}