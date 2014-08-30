using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Parse;

namespace QuotesApp
{
    public class AppConstants
    {
        public static List<object> pageParameters = new List<object>();

        public static SolidColorBrush appPrimaryColor1 = new SolidColorBrush(Color.FromArgb(255, 255, 193, 7));
        public static SolidColorBrush appPrimaryColor2 = new SolidColorBrush(Color.FromArgb(255, 255, 235, 179));
        public static SolidColorBrush appPrimaryColor3 = new SolidColorBrush(Color.FromArgb(255, 255, 160, 0));

        public static SolidColorBrush appAccentColor1 = new SolidColorBrush(Color.FromArgb(255, 104, 137, 255));
        public static SolidColorBrush appAccentColor2 = new SolidColorBrush(Color.FromArgb(255, 166, 186, 255));
        public static SolidColorBrush appAccentColor3 = new SolidColorBrush(Color.FromArgb(255, 77, 115, 255));

        public static ParseUser user;

        public static SolidColorBrush HexToRGB(string hexValue)
        {
            List<string> splitHex = new List<string>();

            if (hexValue.Length == 3)
            {
                string newHex = hexValue[0].ToString() + hexValue[0].ToString() +
                    hexValue[1].ToString() + hexValue[1].ToString() +
                    hexValue[2].ToString() + hexValue[2].ToString();
                hexValue = newHex;
            }

            if (hexValue.Length == 6)
            {
                splitHex.Add(hexValue.Substring(0, 2));
                splitHex.Add(hexValue.Substring(2, 2));
                splitHex.Add(hexValue.Substring(4, 2));
            }
            else if (hexValue.Length == 8)
            {
                splitHex.Add(hexValue.Substring(0, 2));
                splitHex.Add(hexValue.Substring(2, 2));
                splitHex.Add(hexValue.Substring(4, 2));
                splitHex.Add(hexValue.Substring(6, 2));
            }
            else
            {
                return null;
            }

            List<int> newInts = new List<int>();
            foreach (string hex in splitHex)
            {
                newInts.Add(int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
            }

            if (newInts.Count == 3)
            {
                return new SolidColorBrush(Color.FromArgb(255, (byte)newInts[0], (byte)newInts[1], (byte)newInts[2]));
            }
            else if (newInts.Count == 4)
            {
                return new SolidColorBrush(Color.FromArgb((byte)newInts[0], (byte)newInts[1], (byte)newInts[2], (byte)newInts[3]));
            }
            else
            {
                return null;
            }
        }
    }
}
