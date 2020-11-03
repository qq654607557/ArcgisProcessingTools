using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.PGTool
{
    public class GPServerTools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public static bool ExportMapServerCache(ref string mess, string input_service, string target_cache_path, string area_of_interest)
        {
           ESRI.ArcGIS.ServerTools.ExportMapServerCache gp = new ESRI.ArcGIS.ServerTools.ExportMapServerCache();

           gp.input_service = input_service;

           gp.target_cache_path = target_cache_path;
           gp.area_of_interest = area_of_interest;
           //gp.export_cache_type = "TILE_PACKAGE";
           //gp.storage_format_type = "COMPACT";
           //gp.scales = "";

            return GeoprocessorRun.Run(gp, ref mess);
        }
    }
}
