using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;

namespace HelperArcGIS
{
    public class LicenseRun
    {
        public static bool Run(ref string mess)
        {
            //ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            //aoInit = new AoInitializeClass();
            //esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            mess = "";
            try
            {
                if (RuntimeManager.Bind(ProductCode.EngineOrDesktop))
                {
                    try
                    {
                        RuntimeManager.BindLicense(ProductCode.EngineOrDesktop, LicenseLevel.GeodatabaseUpdate);

                        AoInitialize aoInit = new AoInitialize();
                        aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
                        return true;
                    }
                    catch (Exception e)
                    {
                        mess = "需要的ArcGIS组件未许可，程序不能运行!";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mess = "需要的ArcGIS组件未安装，程序不能运行!";
                return false;
            }
            mess = "未知错误！";
            return false;
        }
    }
}
