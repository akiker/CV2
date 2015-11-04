using System;
using System.ComponentModel;

namespace CodeVault.Models
{
    public static class EnumHelper
    {
        public static string ToDescriptionString(Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo.Length <= 0) return en.ToString();
            var attrs = memInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
            return attrs.Length > 0 ? ((DescriptionAttribute) attrs[0]).Description : en.ToString();
        }
    }

    public static class Enum<T>
    {
        public static T Parse(string value)
        {
            return Parse(value, true);
        }

        public static T Parse(string value, bool ignoreCase)
        {
            return (T) Enum.Parse(typeof (T), value, ignoreCase);
        }

        public static bool TryParse(string value, out T returnedValue)
        {
            return TryParse(value, true, out returnedValue);
        }

        public static bool TryParse(string value, bool ignoreCase, out T returnedValue)
        {
            try
            {
                returnedValue = (T) Enum.Parse(typeof (T), value, ignoreCase);
                return true;
            }
            catch
            {
                returnedValue = default(T);
                return false;
            }
        }
    }
}