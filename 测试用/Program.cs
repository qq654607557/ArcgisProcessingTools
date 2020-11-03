using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 测试用
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
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormArcMap());
        }


    }
}
