using System;
using System.ComponentModel;
using System.Reflection;

namespace CodeVault.Models
{
    public static class EnumHelper
    {
        public static string ToDescriptionString(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
    }

    public static class Enum<T>
    {
        public static T Parse(string value)
        {
            return Enum<T>.Parse(value, true);
        }

        public static T Parse(string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static bool TryParse(string value, out T returnedValue)
        {
            return Enum<T>.TryParse(value, true, out returnedValue);
        }

        public static bool TryParse(string value, bool ignoreCase, out T returnedValue)
        {
            try
            {
                returnedValue = (T)Enum.Parse(typeof(T), value, ignoreCase);
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