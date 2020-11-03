using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.SupportFile
{
    public class HelperGDB
    {
        public static IWorkspace OpenGDB(string gdbPath)
        {
            IWorkspaceFactory pFactory = new FileGDBWorkspaceFactory();
            IWorkspace pWorkspace = pFactory.OpenFromFile(gdbPath, 0);
            return pWorkspace;
        }

        /// <summary>
        /// 创建要素数据集
        /// </summary>
        public static IFeatureDataset CreateDataset(ref string mess, IWorkspace Workspac, string featureDatasetName, ISpatialReference spatialReference)
        {

            IFeatureWorkspace targetWorkspac = Workspac as IFeatureWorkspace;
            try
            {
                IFeatureDataset newDataset = targetWorkspac.CreateFeatureDataset(featureDatasetName, spatialReference);
                return newDataset;
            }
            catch (Exception ex)
            { mess = ex.Message; return null; }

        }

        public static IFeatureClass GetFeatureClassFormFeatureDataset(IWorkspace iWorkspace, string className)
        {
            IEnumDataset enumDataset= iWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
            IDataset dataset = enumDataset.Next();
            while ((dataset = enumDataset.Next()) != null)
            {
                IFeatureClass featureClass = dataset as IFeatureClass;
                if (featureClass.AliasName == className)
                {
                    return featureClass;
                }
            }

            return null;
        }

    }
}
