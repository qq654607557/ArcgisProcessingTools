using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ImageProcessing;
using ESRI.ArcGIS.esriSystem;

namespace 循环裁图工具
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

            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            //ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            //aoInit = new AoInitializeClass();
            //esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            //esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form循环裁图工具());
        }
    }
}
