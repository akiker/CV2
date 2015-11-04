using System.ComponentModel;

namespace CodeVault.ViewModels
{
    public class LicenseViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Type")]
        public string LicenseType { get; set; }

        [DisplayName("SKU#")]
        public string Sku { get; set; }

        public string Notes { get; set; }
        public string Owner { get; set; }
    }
}