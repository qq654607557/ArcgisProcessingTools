using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.PGTool
{
   public class GPConversionTools
    {
        public static bool FeatureClassToGeodatabase(ref string mess, string input_Features, string output_Geodatabase)
        {
            ESRI.ArcGIS.ConversionTools.FeatureClassToGeodatabase gp = new ESRI.ArcGIS.ConversionTools.FeatureClassToGeodatabase();

            gp.Input_Features = input_Features;
            gp.Output_Geodatabase = output_Geodatabase;
            

            return GeoprocessorRun.Run(gp, ref mess);
        }
    }
}
