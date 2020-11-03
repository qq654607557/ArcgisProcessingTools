using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
    public class Routo
    {
        /// <summary>
        /// 文件夹名称前50字符
        /// </summary>
        public string FolderName50;
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName;

        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FolderPath;

        /// <summary>
        /// 文件名称前50字符
        /// </summary>
        public string FileName50;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName;

        /// <summary>
        /// XML项目
        /// </summary>
        public RoutoXML XML = new RoutoXML();
        /// <summary>
        /// 类型
        /// </summary>
        public string TypeName;
        /// <summary>
        /// SHP路径
        /// </summary>
        public string SHPPath;
        /// <summary>
        /// TIF路径
        /// </summary>
        public string TIFPath;
        /// <summary>
        /// 此数据是否正确
        /// </summary>
        public bool IsOK;
        /// <summary>
        /// 消息
        /// </summary>
        public string Mess;
    }

    public class RoutoXML
    {
        /// <summary>
        /// 测试角
        /// </summary>
        public string TestAngle { get; set; }
        /// <summary>
        /// 时像
        /// </summary>
        public string TimePhase { get; set; }
    }

    public class SysConfig
    {
        public static string path = "";
        /// <summary>
        /// shp列，xml标记
        /// 特殊WV（前面为为纯数字）
        /// </summary>
        public static Dictionary<string, RoutoXML> TestAngle
        {
            get
            {
                if (testAngle == null)
                {
                    GetTestAngle();
                }
                return testAngle;
            }
        }

        private static Dictionary<string, RoutoXML> testAngle = null;

        public static void GetTestAngle(string pathinfo = "")
        {
            if (!string.IsNullOrEmpty(pathinfo)) path = pathinfo;

            testAngle = new Dictionary<string, RoutoXML>();

            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            string[] sl = txt.ReadTxt(path).ToArray();
            for (int i = 0; i < sl.Length; i++)
            {
                if (!string.IsNullOrEmpty(sl[i]))
                {
                    string[] t = sl[i].Split(',');
                    testAngle.Add(t[0].Trim(), new RoutoXML()
                    {
                        TestAngle = t[1].Trim(),
                        TimePhase = t[2].Trim()
                    });
                }
            }
        }
    }



    public struct TIFZSHP
    {
        public string FilePath;
        public string FileName;
    }
}
