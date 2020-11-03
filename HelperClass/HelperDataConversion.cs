using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperClass
{
    public static class HelperDataConversion
    {
        public static double StringToDouble(string str)
        {
            double d;
            double.TryParse(str, out d);
            return d;
        }

        public static DateTime StringToDateTime(string str)
        {
            DateTime time;
            DateTime.TryParse(str, out time);
            return time;
        }
    }
}
