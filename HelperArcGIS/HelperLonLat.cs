using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS
{
   public class HelperLonLat
   {
       /// <summary>
       /// 数字经纬度和度分秒经纬度转换
       /// </summary>
       /// <param name="digitalLati_Longi">数字经纬度</param>
       /// <returns>度分秒经纬度</returns>
       public static string ConvertDigitalToDegrees(string digitalLati_Longi)
       {
           double digitalDegree = Convert.ToDouble(digitalLati_Longi);
           return ComvertDigitalToDegrees(digitalDegree);
       }

       /// <summary>
       /// 数字经纬度和度分秒经纬度转换
       /// </summary>
       /// <param name="digitalDegree">数字经纬度</param>
       /// <returns>度分秒经纬度</returns>
       public static string ComvertDigitalToDegrees(double digitalDegree)
       {
           const double num = 60;
           int degree = (int)digitalDegree;
           double tmp = (digitalDegree - degree) * num;
           int minute = (int)tmp;

           double second_double = (tmp - minute) * num;
           int second = (int)Math.Round(second_double, 0);

           if (second == num) { second = 0; minute += 1; }
           if (minute == num) { minute = 0; degree += 1; }

           string degrees = "" + degree + "°" + minute.ToString("D2") + "′" + second.ToString("D2") + "″";
           return degrees;
       }

       /// <summary>
       /// 度分秒经纬度（必须含有'°'）和数字经纬度转换
       /// </summary>
       /// <param name="degrees">度分秒经纬度</param>
       /// <returns>数字经纬度</returns>
       public static double ConvertDegreesToDigital(string degrees)
       {
           const double num = 60;
           double digitalDegree = 0.0;
           int d = degrees.IndexOf('°');// 度的符号对应unicode代码为00B0[1](十六进制),显示为°
           if (d < 0)
           {
               return digitalDegree;
           }
           string degree = degrees.Substring(0, d);
           digitalDegree += Convert.ToDouble(degree);

           int m = degrees.IndexOf('′');// 分的符号对应unicode代码为2032[1](十六进制)°
           if (m < 0)
           {
               return digitalDegree;
           }
           string minute = degrees.Substring(d + 1, m - d - 1);
           digitalDegree += ((Convert.ToDouble(minute)) / num);

           int s = degrees.IndexOf('″');// 秒的符号对应unicode代码为2032[1](十六进制)
           if (s < 0)
           {
               return digitalDegree;
           }
           string second = degrees.Substring(m + 1, s - m - 1);
           digitalDegree += (Convert.ToDouble(second) / (num * num));

           return digitalDegree;
       }

       /// <summary>
       /// 度分秒经纬度（必须含有'/'）和数字经纬度
       /// </summary>
       /// <param name="degrees">度分秒经纬度</param>
       /// <returns>数字经纬度</returns>
       public static double ConvertDegreesToDigital_default(string degrees)
       {
           char ch = '/';
           return ConvertDegreesToDigital(degrees, ch);
       }

       /// <summary>
       /// 度分秒经纬度和数字经纬度转换
       /// </summary>
       /// <param name="degrees">度分秒经纬度</param>
       /// <param name="cflag">分隔符</param>
       /// <returns>数字经纬度</returns>
       public static double ConvertDegreesToDigital(string degrees, char cflag)
       {
           const double num = 60;
           double digitalDegree = 0.0;
           int d = degrees.IndexOf(cflag);
           if (d < 0)
           {
               return digitalDegree;
           }
           string degree = degrees.Substring(0, d);
           digitalDegree += Convert.ToDouble(degree);

           int m = degrees.IndexOf(cflag, d + 1);
           if (m < 0)
           {
               return digitalDegree;
           }
           string minute = degrees.Substring(d + 1, m - d - 1);
           digitalDegree += ((Convert.ToDouble(minute)) / num);

           int s = degrees.Length;
           if (s < 0)
           {
               return digitalDegree;
           }
           string second = degrees.Substring(m + 1, s - m - 1);
           digitalDegree += (Convert.ToDouble(second) / (num * num));

           return digitalDegree;
       }
   }
}
