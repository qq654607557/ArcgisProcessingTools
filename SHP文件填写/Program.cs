﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace 图幅镶嵌线分_SHP文件填写
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
            aoInit = new AoInitializeClass();
            esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageProcessing.Form图幅镶嵌线分_SHP文件填写());
        }
    }
}
