using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.PGTool
{
   public class GeoprocessorRun
    {
        public static bool Run(IGPProcess process, ref string mess)
        {
            mess = "";
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            try
            {
                gp.Execute(process, null);
                return true;
            }

            catch (Exception)
            {

                for (int i = 0; i < gp.MessageCount; i++)
                {
                    mess += gp.GetMessage(i) + "\r\n";
                }
                return false;
            }
        }
    }
}
