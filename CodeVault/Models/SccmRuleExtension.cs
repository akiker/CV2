using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeVault.Models
{
    public static class SccmRuleExtension
    {
        public static Dictionary<int, string> ClauseConnectorList()
        {
            var values = new Dictionary<int, string>
            {
                {0, ""}, //Allows for empty clauses for first rule
                {1, "Or"},
                {2, "And"}
            };
            return values;
        }

        private static SccmRegistryDetectionRule CreateNewRegistryRuleWithDefaults()
        {
            var registryRule = new SccmRegistryDetectionRule
            {
                UseDefaultValue = true,
                RegKeyMustExist = true,
                RegKeyAssociate32On64 = false,
                RegistryDataType = RegistryDataType.RegSz,
                RegistryHive = RegistryHiveType.HkeyLocalMachine,
                RegistryKey = string.Empty,
                RegistryValue = string.Empty,
                RegRuleOperator = RegistryRuleOperatorType.Equals,
                RegRuleValue = string.Empty
            };
            return registryRule;
        }

        private static SccmFileFolderDetectionRule CreateNewFileFolderRuleWithDefaults()
        {
            var fileFolderRule = new SccmFileFolderDetectionRule
            {
                FileOrFolderPath = string.Empty,
                FileOrFolderAssociate32On64 = false,
                FileOrFolderMustExist = true,
                FileOrFolderName = string.Empty,
                FileOrFolderRuleOperator = FileOrFolderRuleOperatorType.Equals,
                FileOrFolderRuleProperty = FileOrFolderRulePropertyType.Version,
                FileOrFolderRuleValue = string.Empty,
                FileOrFolderRuleType = FileOrFolderRuleType.File
            };
            return fileFolderRule;
        }

        private static SccmWindowInstallerDetectionRule CreateNewWindowsInstallerRuleWithDefaults()
        {
            var windowInstallerRule = new SccmWindowInstallerDetectionRule
            {
                MsiProductCodeMustExist = true,
                MsiRuleOperator = WindowsInstallerRuleOperatorType.Equals,
                MsiRuleProperty = WindowsInstallerRulePropertyType.Version,
                MsiRuleValue = string.Empty,
                ProductCode = string.Empty
            };
            return windowInstallerRule;
        }

        public static SccmRule CreateNewSccmRuleWithDefaults(ref Product product, SccmRuleType sccmRuleType,
            SccmRuleConnector sccmRuleConnector = SccmRuleConnector.Null)
        {
            SccmRule sccmRule;
            if (sccmRuleType.Equals(SccmRuleType.FileFolder))
            {
                sccmRule = CreateNewFileFolderRuleWithDefaults();
            }
            else if (sccmRuleType.Equals(SccmRuleType.Registry))
            {
                sccmRule = CreateNewRegistryRuleWithDefaults();
            }
            else
            {
                sccmRule = CreateNewWindowsInstallerRuleWithDefaults();
            }
            if (product.SccmRules.Count == 0)
            {
                sccmRule.SccmRuleOrder = 0;
            }
            else
            {
                sccmRule.SccmRuleOrder = product.SccmRules.Count + 1;
            }
            sccmRule.SccmRuleConnector = sccmRuleConnector;
            sccmRule.SccmRuleType = sccmRuleType;
            return sccmRule;
        }

        private static string ToFileFolderClause(SccmFileFolderDetectionRule fileFolderRule)
        {
            var clause = new StringBuilder();

            if (fileFolderRule.FileOrFolderRuleType == FileOrFolderRuleType.File &&
                (fileFolderRule.FileOrFolderMustExist))
            {
                clause.AppendFormat("File: {0} Exists", fileFolderRule.FileOrFolderName);
            }
            else if (fileFolderRule.FileOrFolderRuleType == FileOrFolderRuleType.File &&
                     !fileFolderRule.FileOrFolderMustExist)
            {
                clause.AppendFormat("File: {0} {1}: {2} {3}", fileFolderRule.FileOrFolderName,
                    fileFolderRule.FileOrFolderRuleProperty, fileFolderRule.FileOrFolderRuleOperator,
                    fileFolderRule.FileOrFolderRuleValue);
            }
            else if (fileFolderRule.FileOrFolderRuleType == FileOrFolderRuleType.Folder &&
                     fileFolderRule.FileOrFolderMustExist)
            {
                clause.AppendFormat("Folder: {0} exists", fileFolderRule.FileOrFolderPath);
            }
            else if (fileFolderRule.FileOrFolderRuleType == FileOrFolderRuleType.Folder &&
                     !fileFolderRule.FileOrFolderMustExist)
            {
                clause.AppendFormat("Folder: {0} {1}: {2}", fileFolderRule.FileOrFolderRuleProperty,
                    fileFolderRule.FileOrFolderRuleOperator, fileFolderRule.FileOrFolderRuleValue);
            }
            return clause.ToString();
        }

        private static string ToWindowsInstallerClause(SccmWindowInstallerDetectionRule windowsInstallerRule)
        {
            var clause = new StringBuilder();
            if (windowsInstallerRule.MsiProductCodeMustExist)
            {
                clause.AppendFormat("MSI Product Code: {0} Exists", windowsInstallerRule.ProductCode);
            }
            else
            {
                clause.AppendFormat("MSI Product Code: {0} Version: {1} {2}", windowsInstallerRule.ProductCode,
                    windowsInstallerRule.MsiRuleOperator, windowsInstallerRule.MsiRuleValue);
            }
            return clause.ToString();
        }

        private static string ToRegistryClause(SccmRegistryDetectionRule registryRule)
        {
            var clause = new StringBuilder();
            if (registryRule.RegKeyMustExist)
            {
                if (registryRule.UseDefaultValue)
                {
                    clause.AppendFormat("{0}\\{1} Value: (Default) Exists", registryRule.RegistryHive,
                        registryRule.RegistryKey);
                }
                else
                {
                    clause.AppendFormat("{0}\\{1} Value: {2} Exists", registryRule.RegistryHive,
                        registryRule.RegistryKey, registryRule.RegistryValue);
                }
            }
            else
            {
                if (registryRule.UseDefaultValue)
                {
                    clause.AppendFormat("{0}\\{1} Value: (Default) {2} {3}", registryRule.RegistryHive,
                        registryRule.RegistryKey, registryRule.RegRuleOperator, registryRule.RegRuleValue);
                }
                else
                {
                    clause.AppendFormat("{0}\\{1} Value: {2} {3} {4}", registryRule.RegistryHive,
                        registryRule.RegistryKey, registryRule.RegistryValue, registryRule.RegRuleOperator,
                        registryRule.RegRuleValue);
                }
            }
            return clause.ToString();
        }

        public static List<FormattedRuleHolder> CreateFormattedRulesList(
            List<SccmWindowInstallerDetectionRule> windowsInstallerRules, List<SccmRegistryDetectionRule> registryRules,
            List<SccmFileFolderDetectionRule> fileFolderRules)
        {
            var ruleHolderList = new List<FormattedRuleHolder>();
            if (windowsInstallerRules != null)
            {
                foreach (var rule in windowsInstallerRules)
                {
                    var clause = ToWindowsInstallerClause(rule);
                    var formattedRuleHolder = new FormattedRuleHolder(rule.SccmRuleId, "Windows Installer",
                        rule.SccmRuleConnector, clause);
                    ruleHolderList.Add(formattedRuleHolder);
                }
            }

            if (fileFolderRules != null)
            {
                foreach (var rule in fileFolderRules)
                {
                    var clause = ToFileFolderClause(rule);
                    var formattedRuleHolder = new FormattedRuleHolder(rule.SccmRuleId, "File or Folder",
                        rule.SccmRuleConnector, clause);
                    ruleHolderList.Add(formattedRuleHolder);
                }
            }

            if (registryRules != null)
            {
                foreach (var rule in registryRules)
                {
                    var clause = ToRegistryClause(rule);
                    var formattedRuleHolder = new FormattedRuleHolder(rule.SccmRuleId, "Registry",
                        rule.SccmRuleConnector, clause);
                    ruleHolderList.Add(formattedRuleHolder);
                }
            }
            return ruleHolderList.OrderBy(i => i.RuleId).ToList();
        }
    }
}