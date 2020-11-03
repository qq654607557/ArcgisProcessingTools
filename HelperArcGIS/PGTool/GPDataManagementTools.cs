using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.PGTool
{
    public class GPDataManagementTools
    {
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="inPuts">放入文件路径用;号分开</param>
        /// <param name="outPut">输出路径</param>
        /// <returns></returns>
        public static bool Merge(ref string mess, string inPuts, string outPut)
        {
            ESRI.ArcGIS.DataManagementTools.Merge merge = new ESRI.ArcGIS.DataManagementTools.Merge();
            merge.output = outPut;
            merge.inputs = inPuts;

            return GeoprocessorRun.Run(merge, ref mess);
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="inPuts"></param>
        /// <param name="outPut"></param>
        /// <returns></returns>
        public static bool Merge(ref string mess, string[] inPuts, string outPut)
        {
            string putstr = "";
            for (int i = 0; i < inPuts.Length; i++)
            {
                putstr += inPuts[i] + ";";//(i == drop_fields.Length - 1?"":";");
            }

            return Merge(ref mess, putstr, outPut);
        }

        public static bool BuildFootprints(ref string mess, string in_mosaic_dataset)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.BuildFootprints buildFootprints = new ESRI.ArcGIS.DataManagementTools.BuildFootprints();
            buildFootprints.in_mosaic_dataset = in_mosaic_dataset;

            return GeoprocessorRun.Run(buildFootprints, ref mess);
        }

        public static bool AddRastersToMosaicDataset(ref string mess, string in_mosaic_dataset, string input_path)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.AddRastersToMosaicDataset addRastersToMosaicDataset = new ESRI.ArcGIS.DataManagementTools.AddRastersToMosaicDataset();
            addRastersToMosaicDataset.in_mosaic_dataset = in_mosaic_dataset;
            addRastersToMosaicDataset.raster_type = "Raster Dataset";
            addRastersToMosaicDataset.input_path = input_path;

            return GeoprocessorRun.Run(addRastersToMosaicDataset, ref mess);
        }

        public static bool CreateMosaicDataset(ref string mess, string in_workspace, string in_mosaicdataset_name, ISpatialReference spatialReference)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.CreateMosaicDataset createMosaicDataset = new ESRI.ArcGIS.DataManagementTools.CreateMosaicDataset();

            createMosaicDataset.in_mosaicdataset_name = in_mosaicdataset_name;
            createMosaicDataset.in_workspace = in_workspace;// @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";
            createMosaicDataset.coordinate_system = spatialReference; //esriSRGeoCSType.esriSRGeoCS_WGS1984;

            return GeoprocessorRun.Run(createMosaicDataset, ref mess);
        }

        public static bool CopyFeatures(ref string mess, IFeatureClass in_features, string out_feature_class)
        {
            Geoprocessor gp = new Geoprocessor();//初始化Geoprocessor
            gp.OverwriteOutput = true; //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.CopyFeatures copyFeatures = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();

            copyFeatures.in_features = in_features;
            copyFeatures.out_feature_class = out_feature_class;// @"D:\Projecct\ImageProcessing\data\" +  "SHP" + DateTime.Now.ToString("yyyyMMddHHmmss") +".shp";

            return GeoprocessorRun.Run(copyFeatures, ref mess);
        }

        public static bool DeleteField(ref string mess, string in_table, string drop_field)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.DeleteField temTools = new ESRI.ArcGIS.DataManagementTools.DeleteField();

            temTools.in_table = in_table; // @"D:\Projecct\ImageProcessing\data\XQ20190218093738.shp";
            temTools.drop_field = drop_field; // "TypeID;ItemTS;UriHash;";

            return GeoprocessorRun.Run(temTools, ref mess);
        }

        public static bool DeleteField(ref string mess, string in_table, string[] drop_fields)
        {
            string drop_field = "";
            for (int i = 0; i < drop_fields.Length; i++)
            {
                drop_field += drop_fields[i] + ";";//(i == drop_fields.Length - 1?"":";");
            }

            return DeleteField(ref mess, in_table, drop_field);
        }

        /// <summary>
        /// 创建拓扑
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="in_dataset"></param>
        /// <param name="out_topology"></param>
        /// <returns></returns>
        public static bool CreateTopology(ref string mess, string in_dataset,string out_name)
        {
            ESRI.ArcGIS.DataManagementTools.CreateTopology gp = new ESRI.ArcGIS.DataManagementTools.CreateTopology();

            gp.in_dataset = in_dataset;
            gp.out_topology = out_name;
            gp.out_name = out_name;

            return GeoprocessorRun.Run(gp, ref mess);
        }

        /// <summary>
        /// 向拓扑中添加要素
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public static bool AddFeatureClassToTopology(ref string mess, string in_topology, string in_featureclass)
        {
            ESRI.ArcGIS.DataManagementTools.AddFeatureClassToTopology gp = new ESRI.ArcGIS.DataManagementTools.AddFeatureClassToTopology();

            gp.in_topology = in_topology;
            gp.in_featureclass = in_featureclass;

            return GeoprocessorRun.Run(gp, ref mess);
        }

        /// <summary>
        /// 拓扑验证
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public static bool ValidateTopology(ref string mess,string in_topology)
        {
            ESRI.ArcGIS.DataManagementTools.ValidateTopology gp = new ESRI.ArcGIS.DataManagementTools.ValidateTopology();

            gp.in_topology = in_topology;

            return GeoprocessorRun.Run(gp, ref mess);
        }
    }
}
