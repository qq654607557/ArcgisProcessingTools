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

        public static bool FeatureClassToFeatureClass(ref string mess, object input_Features, string output_Location,string output_FeatureClass)
        {
            ESRI.ArcGIS.ConversionTools.FeatureClassToFeatureClass gp = new ESRI.ArcGIS.ConversionTools.FeatureClassToFeatureClass();

            gp.in_features = input_Features;
            gp.out_path = output_Location;
            gp.out_feature_class = output_FeatureClass;
            gp.out_name = output_FeatureClass;
            return GeoprocessorRun.Run(gp, ref mess);
        }
    }
}
