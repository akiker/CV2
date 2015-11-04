using System.ComponentModel;

namespace CodeVault.Models
{
    public enum DependencyType
    {
        [Description("Pre-Install")] PreInstall = 0,

        [Description("Post-Install")] PostInstall = 1
    }
}