using System;

namespace CodeVault.Models.Utilities
{
    public static class FileSizeFormatter
    {
        public static FormattedFileSizeStringHolder ToFormattedString(long i)
        {
            var fsh = new FormattedFileSizeStringHolder();
            double readable = (i < 0 ? -i : i);
            if (i >= 0x1000000000000000) // Exabyte
            {
                fsh.Suffix = "EB";
                readable = (i >> 50);
            }
            else if (i >= 0x4000000000000) // Petabyte
            {
                fsh.Suffix = "PB";
                readable = (i >> 40);
            }
            else if (i >= 0x10000000000) // Terabyte
            {
                fsh.Suffix = "TB";
                readable = (i >> 30);
            }
            else if (i >= 0x40000000) // Gigabyte
            {
                fsh.Suffix = "GB";
                readable = (i >> 20);
            }
            else if (i >= 0x100000) // Megabyte
            {
                fsh.Suffix = "MB";
                readable = (i >> 10);
            }
            else if (i >= 0x400) // Kilobyte = 1024 bytes
            {
                fsh.Suffix = "KB";
                readable = i;
            }
            else
            {
                fsh.Suffix = "B";
                fsh.Number = i.ToString();
                return fsh; // Byte
            }
            readable /= 1024;
            fsh.Number = readable.ToString("0.###");
            return fsh;
        }

        public static long FormattedStringAsLong(string formattedString)
        {
            var formattedStringParts = formattedString.Split(' ');
            var numericPortion = Convert.ToInt64(formattedStringParts[0]);
            double readable = (numericPortion < 0 ? -numericPortion : numericPortion);
            if (numericPortion >= 0x1000000000000000) // Exabyte
            {
                readable = (numericPortion << 50);
            }
            else if (numericPortion >= 0x4000000000000) // Petabyte
            {
                readable = (numericPortion << 40);
            }
            else if (numericPortion >= 0x10000000000) // Terabyte
            {
                readable = (numericPortion << 30);
            }
            else if (numericPortion >= 0x40000000) // Gigabyte
            {
                readable = (numericPortion << 20);
            }
            else if (numericPortion >= 0x100000) // Megabyte
            {
                readable = (numericPortion << 10);
            }
            else if (numericPortion >= 0x400) // Kilobyte = 1024 bytes
            {
                readable = numericPortion;
            }
            else
            {
                return numericPortion; // Byte
            }
            readable /= 1024;
            return Convert.ToInt64(readable);
        }

        public static long StringAsBytes(string bytesString)
        {
            if (string.IsNullOrEmpty(bytesString))
            {
                return 0;
            }

            const long oneKb = 1024;
            const long oneMb = oneKb*1024;
            const long oneGb = oneMb*1024;
            const long oneTb = oneGb*1024;
            double returnValue;
            var suffix = string.Empty;

            if (bytesString.IndexOf(" ") > 0)
            {
                returnValue = float.Parse(bytesString.Substring(0, bytesString.IndexOf(" ")));
                suffix = bytesString.Substring(bytesString.IndexOf(" ") + 1).ToUpperInvariant();
            }
            else
            {
                returnValue = float.Parse(bytesString.Substring(0, bytesString.Length - 2));
                suffix = bytesString.ToUpperInvariant().Substring(bytesString.Length - 2);
            }

            switch (suffix)
            {
                case "KB":
                {
                    returnValue *= oneKb;
                    break;
                }

                case "MB":
                {
                    returnValue *= oneMb;
                    break;
                }

                case "GB":
                {
                    returnValue *= oneGb;
                    break;
                }

                case "TB":
                {
                    returnValue *= oneTb;
                    break;
                }

                default:
                {
                    break;
                }
            }

            return Convert.ToInt64(returnValue);
        }

        //Converts long to bytes format
        // The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
        public static string GetBytesReadable(long i)
        {
            var sign = (i < 0 ? "-" : "");
            double readable = (i < 0 ? -i : i);
            string suffix;
            if (i >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (i >> 50);
            }
            else if (i >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (i >> 40);
            }
            else if (i >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (i >> 30);
            }
            else if (i >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (i >> 20);
            }
            else if (i >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (i >> 10);
            }
            else if (i >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = i;
            }
            else
            {
                return i.ToString(sign + "0 B"); // Byte
            }
            readable /= 1024;

            return sign + readable.ToString("0.### ") + suffix;
        }
    }

    public class FormattedFileSizeStringHolder
    {
        public string Number { get; set; }

        public string Suffix { get; set; }
    }
}