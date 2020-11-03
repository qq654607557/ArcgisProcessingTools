using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.PGTool
{
    public class GPAnalysisTools
    {
        /// <summary>
        /// 裁剪
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public static bool Clip(ref string mess, string in_features, string clip_features, string out_feature_class)
        {
            ESRI.ArcGIS.AnalysisTools.Clip gp = new ESRI.ArcGIS.AnalysisTools.Clip();

            gp.in_features = in_features;// @"D:\Projecct\ImageProcessing\data\HB001.shp";
            gp.clip_features = clip_features;// @"D:\Projecct\ImageProcessing\data\HB002.shp";
            gp.out_feature_class = out_feature_class;// @"D:\Projecct\ImageProcessing\data\CQ" +DateTime.Now.ToString("yyyyMMddHHmmss")+".shp";

            return GeoprocessorRun.Run(gp, ref mess);
        }

        public static bool Erase(ref string mess, string in_features, string erase_features, string out_feature_class)
        {
            ESRI.ArcGIS.AnalysisTools.Erase gp = new ESRI.ArcGIS.AnalysisTools.Erase();

            gp.in_features = in_features;
            gp.erase_features = erase_features;
            gp.out_feature_class = out_feature_class;

            return GeoprocessorRun.Run(gp, ref mess);
        }
    }
}
