using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.DataProcessing
{
    class HelperDataset
    {
        /// <summary>
        /// 创建要素数据集
        /// </summary>
        public static void CreateDataset()
        {

        }

        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="Workspac">工作空间</param>
        /// <param name="featureDatasetName">要素数据集名称</param>
        /// <param name="spatialReference">空间参考，可以为空</param>
        /// <returns></returns>
        public static IFeatureDataset CreateOrOpenFeatureDataset(ref IWorkspace Workspac, string featureDatasetName, ISpatialReference spatialReference)
        {
            IFeatureWorkspace targetWorkspac = Workspac as IFeatureWorkspace;
            IFeatureClass pFeaClass = null;
            pFeaClass = ExitFeatureClass(Workspac, featureDatasetName, esriDatasetType.esriDTFeatureDataset);
            if (pFeaClass != null)
            {
                return targetWorkspac.OpenFeatureDataset(featureDatasetName);
            }

            if (spatialReference == null)
            {
                spatialReference = new UnknownCoordinateSystemClass();
            }
            IControlPrecision2 pCP = spatialReference as IControlPrecision2;
            IFeatureDataset newDataset = null;

            try
            {
                if (!pCP.IsHighPrecision)//判断是否为高精度
                {
                    pCP.IsHighPrecision = true;
                }
                newDataset = targetWorkspac.CreateFeatureDataset(featureDatasetName, spatialReference);
                return newDataset;
            }
            catch (Exception ex)//低精度创建
            {
                pCP.IsHighPrecision = false;
                IGeographicCoordinateSystem pGeo = spatialReference as IGeographicCoordinateSystem;
                bool bGeo = (pGeo == null) ? false : true;
                //要素分辨率
                ISpatialReferenceResolution spatialReferenceResolution = spatialReference as ISpatialReferenceResolution;
                if (bGeo)
                {
                    spatialReferenceResolution.ConstructFromHorizon();// 定义XY的分辨率和范围根据空间参考的水平范围
                    spatialReferenceResolution.SetDefaultXYResolution();// 设置默认分辨率容差
                }
                else
                {
                    spatialReferenceResolution.set_XYResolution(false, 0.04);
                }
                //要素数据集容差
                ISpatialReferenceTolerance spatialReferenceTolerance = spatialReference as ISpatialReferenceTolerance;
                if (bGeo)
                {
                    spatialReferenceTolerance.SetDefaultXYTolerance();
                }
                else
                {
                    spatialReferenceTolerance.SetMinimumXYTolerance();
                }

                try
                {
                    newDataset = targetWorkspac.CreateFeatureDataset(featureDatasetName, spatialReference);
                    return newDataset;
                }
                catch (Exception e)
                {

                }
                return null;
            }
        }

        public static IFeatureClass ExitFeatureClass(IWorkspace workspac, string featureDatasetName, esriDatasetType esridatasetType)
        {


            return null;
        }

        private static IRgbColor GetRGBColor(int yourRed, int yourGreen, int yourBlue, IServerContext pSOC)
        {
            IRgbColor pRGB = (IRgbColor)pSOC.CreateObject("esriDisplay.RgbColor");
            pRGB.Red = yourRed;
            pRGB.Green = yourGreen;
            pRGB.Blue = yourBlue;
            pRGB.UseWindowsDithering = true;
            return pRGB;
        }

        /// <summary>
        /// 颜色设置
        /// </summary>
        /// <param name="red">R</param>
        /// <param name="green">G</param>
        /// <param name="blue">B</param>
        /// <returns>GIS颜色对象</returns>
        private static IColor GetColor(int red, int green, int blue, IServerContext pSOC)
        {
            IRgbColor rgbColor = GetRGBColor(red, green, blue, pSOC);
            IColor color = rgbColor as IColor;
            return color;
        }

    }
}
