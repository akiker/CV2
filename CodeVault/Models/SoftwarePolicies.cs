using System.Collections.Generic;

namespace CodeVault.Models
{
    public static class SoftwarePolicyCategories
    {
        public static Dictionary<int, string> CategoryLookupList()
        {
            var values = new Dictionary<int, string>
            {
                {0, "WFDC Core"},
                {1, "WFA Core"},
                {2, "LOB"}
            };
            return values;
        }

        public static Dictionary<int, string> LobSubCategoryLookupList()
        {
            var values = new Dictionary<int, string>
            {
                {0, "Standard"},
                {1, "Optional"},
                {2, "Non-Standard"},
                {3, "By-Exception"},
                {4, "Restricted"}
            };
            return values;
        }
    }
}