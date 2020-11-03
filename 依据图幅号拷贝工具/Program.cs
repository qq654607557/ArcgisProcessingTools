using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 依据图幅号拷贝工具
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageProcessing.依据图幅号拷贝工具());
        }
    }
}
