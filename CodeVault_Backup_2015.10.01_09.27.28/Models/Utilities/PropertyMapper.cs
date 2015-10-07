using System.Collections.Generic;
using System.Linq;

namespace CV2.Models.Utilities
{
    public class PropertyMapper
    {
        private readonly Dictionary<string, string> propertyMapDictionary = new Dictionary<string, string>();

        public PropertyMapper()
        {
            this.InitializeMappingDictionary();
        }

        public string GetFriendlyTextForProperty(string propertyName)
        {
            string friendlyName = this.propertyMapDictionary.Where(p => p.Key == propertyName).Select(p => p.Value).FirstOrDefault();
            return friendlyName;
        }

        private void InitializeMappingDictionary()
        {
            propertyMapDictionary.Add("ProductName", "Name");

            propertyMapDictionary.Add("ProductManufacturer", "Manufacturer");

            propertyMapDictionary.Add("ProductVersion", "Version");

            propertyMapDictionary.Add("ProductDescription", "Description");

            propertyMapDictionary.Add("ProductIsDigitallySigned", "Digitally Signed");

            propertyMapDictionary.Add("ProductCode", "Product Code");

            propertyMapDictionary.Add("ProductUpgradeCode", "Upgrade Code");

            propertyMapDictionary.Add("ProductDslId", "DSL ID");

            propertyMapDictionary.Add("ProductCosmicId", "COSMIC ID");

            propertyMapDictionary.Add("ProductCosmicMsiGuid", "COSMIC MSI GUID");

            propertyMapDictionary.Add("ProductStatus", "Status");

            propertyMapDictionary.Add("ProductCategory", "Category");

            propertyMapDictionary.Add("ProductType", "Type");

            propertyMapDictionary.Add("ProductTypeId", "Type");

            propertyMapDictionary.Add("ProductKeyWord", "Key Word");

            propertyMapDictionary.Add("InstallDetailFileName", "Install File Name");

            propertyMapDictionary.Add("InstallDetailTransformFileName", "Transform File Name");

            propertyMapDictionary.Add("InstallDetailFileSize", "Install File Size");

            propertyMapDictionary.Add("InstallDetailRebootRequired", "Reboot Required");

            propertyMapDictionary.Add("InstallDetailInstallCommand", "Install Command");

            propertyMapDictionary.Add("InstallDetailUninstallCommand", "Uninstall Command");

            propertyMapDictionary.Add("InstallDetailSourceLocation", "Source Location");

            propertyMapDictionary.Add("SccmContentLocation", "SCCM - Source Location");

            propertyMapDictionary.Add("SccmInstallCommand", "SCCM - Install Command");

            propertyMapDictionary.Add("SccmUninstallCommand", "SCCM - Uninstall Command");

            propertyMapDictionary.Add("ScmmDeploymentNotes", "SCCM - Deployment Notes");

            propertyMapDictionary.Add("SccmRebootRequired", "SCCM - Reboot Required");

            propertyMapDictionary.Add("SccmOnDemandGroup", "SCCM - On-Demand AD Group");

            propertyMapDictionary.Add("SccmReturnCode", "SCCM - Return Code");

            propertyMapDictionary.Add("SccmRule", "SCCM Rule");

            propertyMapDictionary.Add("SccmRuleOrder", "SCCM Rule - Order");

            propertyMapDictionary.Add("SccmRuleNote", "SCCM Rule - Note");

            propertyMapDictionary.Add("SccmRuleConnector", "SCCM Rule - Connector");

            propertyMapDictionary.Add("DistributionPath", "Distribution Path");

            propertyMapDictionary.Add("DistributionLocation", "Distribution Path");

            propertyMapDictionary.Add("DistributionMethod", "Distribution Method");

            propertyMapDictionary.Add("ReturnCode", "SCCM - Return Code");

            propertyMapDictionary.Add("ReturnCodeDescription", "SCCM - Return Code Description");

            propertyMapDictionary.Add("WindowsXP_32Bit", "Requirement - Windows XP 32-bit");

            propertyMapDictionary.Add("WindowsVista_32Bit", "Requirement - Windows Vista 32-bit");

            propertyMapDictionary.Add("Windows7_32Bit", "Requirement - Windows 7 32-bit");

            propertyMapDictionary.Add("Windows8_32Bit", "Requirement - Windows 8 32-bit");

            propertyMapDictionary.Add("Windows81_32Bit", "Requirement - Windows 8.1 32-bit");

            propertyMapDictionary.Add("Windows10_32Bit", "Requirement - Windows 10 32-bit");

            propertyMapDictionary.Add("WindowsXP_64Bit", "Requirement - Windows XP 64-bit");

            propertyMapDictionary.Add("WindowsVista_64Bit", "Requirement - Windows Vista 64-bit");

            propertyMapDictionary.Add("Windows7_64Bit", "Requirement - Windows 7 64-bit");

            propertyMapDictionary.Add("Windows8_64Bit", "Requirement - Windows 8 64-bit");

            propertyMapDictionary.Add("Windows81_64Bit", "Requirement - Windows 8.1 64-bit");

            propertyMapDictionary.Add("Windows10_64Bit", "Requirement - Windows 10 64-bit");

            propertyMapDictionary.Add("SoftwarePolicySupportLevel", "Software Policy - Support Level");

            propertyMapDictionary.Add("SoftwarePolicySupportLevelId", "Software Policy - Support Level");

            propertyMapDictionary.Add("SoftwarePolicyADGroupName", "Software Policy - AD Group");

            propertyMapDictionary.Add("SoftwarePolicyGroupAssociation", "Software Policy - Group Association");

            propertyMapDictionary.Add("SoftwarePolicyGroupDisplayName", "Software Policy - AD Group Display Name");

            propertyMapDictionary.Add("SoftwarePolicyGroupDescription", "Software Policy - AD Group Description");

            propertyMapDictionary.Add("InternetExplorerVersion", "Requirement - Internet Explorer Version");

            propertyMapDictionary.Add("IISVersion", "Requirement - IIS Version");

            propertyMapDictionary.Add("DotNetVersion", "Requirement - .NET Version");

            propertyMapDictionary.Add("AdobeReaderVersion", "Requirement - Adobe Reader Version");

            propertyMapDictionary.Add("JavaRuntimeVersion", "Requirement - Java Runtime Version");

            propertyMapDictionary.Add("JDKVersion", "Requirement - JDK Version");

            propertyMapDictionary.Add("DirectXVersion", "Requirement - DirectX Version");

            propertyMapDictionary.Add("InstalledOfficeApplicationName", "Requirement - Office Application");

            propertyMapDictionary.Add("InstalledOfficeApplicationVersion", "Requirement - Office Application Version");

            propertyMapDictionary.Add("SQLExpressVersion", "Requirement - SQL Express Version");

            propertyMapDictionary.Add("SQLCompactVersion", "Requirement - SQL Compact Version");

            propertyMapDictionary.Add("VSTORuntimeVersion", "Requirement - VSTO Version");

            propertyMapDictionary.Add("InstalledOffice2003PIA", "Requirement - Office 2003 PIA");

            propertyMapDictionary.Add("InstalledOffice2007PIA", "Requirement - Office 2007 PIA");

            propertyMapDictionary.Add("InstalledOffice2010PIA", "Requirement - Office 2010 PIA");

            propertyMapDictionary.Add("OfficeSharedInteropAssembly", "Requirement - Office Shared Interop Assembly");

            propertyMapDictionary.Add("PowerShellVersion", "Requirement - PowerShell Version");

            propertyMapDictionary.Add("MinimumScreenResolution", "Requirement - Minimum Screen Resolution");

            propertyMapDictionary.Add("MinimumWindowsInstallerVersion", "Requirement - Minimum Windows Installer Version");

            propertyMapDictionary.Add("MinimumPhysicalMemory", "Requirement - Minimum Physical Memory");

            propertyMapDictionary.Add("MinimumColorQuality", "Requirement - Minimum Color Quality");

            propertyMapDictionary.Add("ElevatedRightsRequired", "Elevated Rights Required");

            propertyMapDictionary.Add("RequiresAdminRightsBasic", "Administrative Rights - Required for Basic Operation");

            propertyMapDictionary.Add("RequiresAdminRightsAdvanced", "Administrative Rights - Required for Advanced Operation");

            propertyMapDictionary.Add("RequiresAdminRightsUpdates", "Administrative Rights - Required for Updates");

            propertyMapDictionary.Add("LocalAdminVerificationComplete", "Local Admin. Verification - Verification Complete");

            propertyMapDictionary.Add("WorksWithLocalAdminAccount", "Local Admin. Verification - Works with Local Administrator Account");

            propertyMapDictionary.Add("ProductPermissionDetailAcl", "Permission Detail - ACL");

            propertyMapDictionary.Add("ProductPermissionDetailType", "Permission Detail - Type");

            propertyMapDictionary.Add("ProductPermissionLocation", "Permission Detail - Path");

            propertyMapDictionary.Add("ProductPermissionGroupOrUserName", "Permission Detail - AD Group or User Name");

            propertyMapDictionary.Add("ProductPermissionDetail", "Permission - Detail");

            propertyMapDictionary.Add("ProductPermission", "Permission");

            propertyMapDictionary.Add("DocumentTitle", "Document - Name");

            propertyMapDictionary.Add("DocumentType", "Document - Type");

            propertyMapDictionary.Add("DocumentTypeId", "Document - Type");

            propertyMapDictionary.Add("DocumentContents", "Document - Size");

            propertyMapDictionary.Add("AttachmentName", "Attachment - Name");

            propertyMapDictionary.Add("Word", "Document - Keyword");

            propertyMapDictionary.Add("LicenseSku", "License - SKU");

            propertyMapDictionary.Add("LicenseTypeId", "License - Type");

            propertyMapDictionary.Add("LicenseNotes", "License - Notes");

            propertyMapDictionary.Add("LicenseOwner", "License - Owner");

            propertyMapDictionary.Add("LicenseKeyData", "License Key - Data");

            propertyMapDictionary.Add("LicenseKeyOwnerName", "License Key - Owner");

            propertyMapDictionary.Add("LicenseKeyOwnerEmail", "License Key - Owner Email");

            propertyMapDictionary.Add("LicenseKey", "License Key");

            propertyMapDictionary.Add("LicenseKeyOwnerPhoneNumber", "License Key - Owner Phone Number");

            propertyMapDictionary.Add("ProductContactLoginId", "Contact - Login ID");

            propertyMapDictionary.Add("ProductContactEmail", "Contact - Email");

            propertyMapDictionary.Add("ProductContactName", "Contact - Name");

            propertyMapDictionary.Add("ProductContactPhoneNumber", "Contact - Phone Number");

            propertyMapDictionary.Add("ProductContactRole", "Contact - Role");

            propertyMapDictionary.Add("ProductContactRoleId", "Contact - Role");

            propertyMapDictionary.Add("FileOrFolderPath", "SCCM Rule - File or Folder Path");

            propertyMapDictionary.Add("FileOrFolderName", "SCCM Rule - File or Folder Name");

            propertyMapDictionary.Add("FileOrFolderMustExist", "SCCM Rule - File or Folder Must Exist");

            propertyMapDictionary.Add("FileOrFolderRuleType", "SCCM Rule - File or Folder Rule Type");

            propertyMapDictionary.Add("FileOrFolderRuleTypeId", "SCCM Rule - File or Folder Rule Type");

            propertyMapDictionary.Add("FileOrFolderRuleOperatorType", "SCCM Rule - File or Folder Operator");

            propertyMapDictionary.Add("FileOrFolderRuleValue", "SCCM Rule - File or Folder Rule Value");

            propertyMapDictionary.Add("RegistryValue", "SCCM Rule - Registry Value");

            propertyMapDictionary.Add("UseDefaultValue", "SCCM Rule - Registry Use Default Value");

            propertyMapDictionary.Add("RegKeyAssociate32On64", "SCCM Rule - Registry Key Associated with 32-bit on 64-bit");

            propertyMapDictionary.Add("RegistryDataType", "SCCM Rule - Registry Data Type");

            propertyMapDictionary.Add("RegKeyMustExist", "SCCM Rule - Registry Key Must Exist");

            propertyMapDictionary.Add("RegistryRuleOperatorType", "SCCM Rule - Registry Operator Type");

            propertyMapDictionary.Add("RegRuleValue", "SCCM Rule - Registry Rule Value");

            propertyMapDictionary.Add("MsiProductCodeMustExist", "SCCM Rule - MSI Product Code Must Exist");

            propertyMapDictionary.Add("MsiRuleProperty", "SCCM Rule - MSI Rule Property");

            propertyMapDictionary.Add("MsiRuleOperator", "SCCM Rule - MSI Rule Operator");

            propertyMapDictionary.Add("MsiRuleValue", "SCCM Rule - MSI Rule Value");

            propertyMapDictionary.Add("Dependency", "Dependency");

            propertyMapDictionary.Add("Dependencies", "Dependency");

            propertyMapDictionary.Add("InstallOrder", "Install Order");

            propertyMapDictionary.Add("DependencyType", "Dependency Type");

            propertyMapDictionary.Add("SupersededProducts", "Supersedence");

            propertyMapDictionary.Add("DocumentAttachment", "Attachment");

            propertyMapDictionary.Add("ProductContact", "Contact");
        }
    }
}