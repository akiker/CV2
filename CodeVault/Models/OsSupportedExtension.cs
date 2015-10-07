using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeVault.Models;

namespace CodeVault.Models
{
    public static class OsSupportedExtension
    {
        public static OsRequirement CreateOsSupportedWithDefaults()
        {
            var osRequirement = new OsRequirement()
            {
                WindowsXP_32Bit = true,
                WindowsXP_64Bit = true,
                WindowsVista_32Bit = true,
                WindowsVista_64Bit = true,
                Windows7_32Bit = true,
                Windows7_64Bit = true,
                Windows8_32Bit = true,
                Windows8_64Bit = true,
                Windows81_32Bit = true,
                Windows81_64Bit = true
            };
            return osRequirement;
        }
    }
}
