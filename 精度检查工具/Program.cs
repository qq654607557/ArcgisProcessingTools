﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 精度检查工具
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

            ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            aoInit = new ESRI.ArcGIS.esriSystem.AoInitializeClass();
            ESRI.ArcGIS.esriSystem.esriLicenseStatus licStatus = aoInit.Initialize(ESRI.ArcGIS.esriSystem.esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);

            Application.SetCompatibleTextRenderingDefault(false);
            //ImageProcessing.Form精度检查工具 form = new ImageProcessing.Form精度检查工具();
            ImageProcessing.Form精度检查工具shp form = new ImageProcessing.Form精度检查工具shp();
            form.RunEvent += new System.EventHandler(excel精度检查run.run);
            Application.Run(form);
        }
    }
}
