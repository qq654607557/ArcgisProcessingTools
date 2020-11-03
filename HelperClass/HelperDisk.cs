using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace HelperClass
{
    public static class HelperDisk
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="calculationUnit">计算单位（默认为1000）</param>
        /// <returns>SerialNumber(硬盘序列号),diskName(硬盘名称（大小）)</SerialNumber></returns>
        public static Dictionary<string, string> GetDisk(decimal calculationUnit)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (calculationUnit != 1000 && calculationUnit != 1024) calculationUnit = 1000;
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //大小
                decimal size = decimal.Parse(mo.Properties["Size"].Value.ToString()) / calculationUnit / calculationUnit / calculationUnit;
                string sizestr = size > calculationUnit ? Math.Round(size / calculationUnit) + "TB" : Math.Round(size) + "GB";

                string serialNumber = mo.Properties["SerialNumber"].Value.ToString().Trim();
                if(serialNumber.Length>4)
                    dic.Add(serialNumber, serialNumber + "(" + sizestr + ")");
                else
                {//读不到SerialNumber处理
                    serialNumber= mo.Properties["Caption"].Value.ToString().Trim();
                    dic.Add(parseSerialFromDeviceID(mo.Properties["PNPDeviceID"].Value.ToString().Trim()), serialNumber + "(" + sizestr + ")");
                }

            }
            return dic;
        }

        private static string parseSerialFromDeviceID(string deviceId)
        {
            var splitDeviceId = deviceId.Split('\\');
            var arrayLen = splitDeviceId.Length - 1;
            var serialArray = splitDeviceId[arrayLen].Split('&');
            var serial = serialArray[0];
            return serial;
        }
    }
}
