using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.DataProcessing
{
    public class HelperMosaicDataset
    {
        /// <summary> 
        /// 创建镶嵌数据集 
        /// </summary> 
        /// <param name="pFgdbWorkspace">工作空间</param> 
        /// <param name="pMDame">名称</param> 
        /// <param name="pSrs">空间参考</param>
        /// <returns>镶嵌数据集</returns>
        public static IMosaicDataset CreateMosaicDataset(ref string mess, IWorkspace pFgdbWorkspace, string pMDame, ISpatialReference pSrs)
        {
            try
            {
                IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();
                ICreateMosaicDatasetParameters pCreationPars = new CreateMosaicDatasetParametersClass();

                pCreationPars.BandCount = 3;
                pCreationPars.PixelType = rstPixelType.PT_UCHAR;
                IMosaicWorkspaceExtensionHelper pMosaicExentionHelper = new MosaicWorkspaceExtensionHelperClass();
                IMosaicWorkspaceExtension pMosaicExtention = pMosaicExentionHelper.FindExtension(pFgdbWorkspace);
                return pMosaicExtention.CreateMosaicDataset(pMDame, pSrs, pCreationPars, "DOM");
            }
            catch (Exception ex)
            {
                mess = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取镶嵌数据集
        /// </summary>
        /// <param name="MosaicName">数据集名称</param>
        /// <param name="workspace">数据集所在工作空间</param>
        /// <returns>镶嵌数据集</returns>
        public static IMosaicDataset GetMosaicDataset(ref string mess, string MosaicName, IWorkspace workspace)
        {
            IMosaicDataset pMosicDataset = null;
            try
            {
                IMosaicWorkspaceExtensionHelper pMosaicWsExHelper = new MosaicWorkspaceExtensionHelperClass();
                IMosaicWorkspaceExtension pMosaicWsExt = pMosaicWsExHelper.FindExtension(workspace);
                if (pMosaicWsExt != null)
                {
                    try
                    {
                        pMosicDataset = pMosaicWsExt.OpenMosaicDataset(MosaicName);
                    }
                    catch (Exception ex)
                    {
                        mess = ex.Message;
                        return pMosicDataset;
                    }
                }
            }
            catch (Exception exall)
            {
                mess = exall.Message;
            }
            return pMosicDataset;
        }

        /// <summary>
        /// 获取镶嵌数据表
        /// </summary>
        /// <param name="pMosaicDataset">镶嵌数据集</param>
        /// <returns>镶嵌数据表</returns>
        public static ITable GetMosaicDatasetTable(ref string mess, IMosaicDataset pMosaicDataset)
        {
            ITable pTable = null;
            try
            {
                IEnumName pEnumName = pMosaicDataset.Children;
                pEnumName.Reset();
                ESRI.ArcGIS.esriSystem.IName pName;
                while ((pName = pEnumName.Next()) != null)
                {
                    pTable = pName.Open() as ITable;
                    int i = pTable.Fields.FieldCount;
                    if (i >= 21) break;
                }
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            return pTable;
        }

        /// <summary>
        /// 删除镶嵌数据集
        /// </summary>
        /// <param name="rasterName">名称</param>
        /// <param name="workspace">工作空间</param>
        /// <returns>删除成功时返回true,否则返回false</returns>
        public static bool DeleteMosaic(ref string mess, string rasterName, IWorkspace workspace)
        {
            try
            {
                IMosaicWorkspaceExtensionHelper pMosaicWsExHelper = new MosaicWorkspaceExtensionHelperClass();
                IMosaicWorkspaceExtension pMosaicWsExt = pMosaicWsExHelper.FindExtension(workspace);
                pMosaicWsExt.DeleteMosaicDataset(rasterName);
                return true;
            }
            catch (Exception ex)
            {
                mess = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 导入栅格数据
        /// </summary>
        /// <param name="filePath">导入文件路径</param>
        /// <param name="mosaicDataSet">镶嵌数据集</param>
        public static bool ImportRasterToMosaic(ref string mess, string filePath, IMosaicDataset mosaicDataSet)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
                IRasterWorkspace rasterWorkspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(filePath), 0) as IRasterWorkspace;
                IMosaicDatasetOperation mOp = (IMosaicDatasetOperation)mosaicDataSet;
                IAddRastersParameters addRs = new AddRastersParametersClass();
                IRasterDatasetCrawler rsDsetCrawl = new RasterDatasetCrawlerClass();
                rsDsetCrawl.RasterDataset = rasterWorkspace.OpenRasterDataset(System.IO.Path.GetFileName(filePath));
                IRasterTypeFactory rsFact = new RasterTypeFactoryClass();
                IRasterType rsType = rsFact.CreateRasterType("Raster dataset");
                rsType.FullName = rsDsetCrawl.DatasetName;
                addRs.Crawler = (IDataSourceCrawler)rsDsetCrawl;
                addRs.RasterType = rsType;
                mOp.AddRasters(addRs, null);
                //计算cellSize 和边界
                // Create a calculate cellsize ranges parameters object.
                ICalculateCellSizeRangesParameters computeArgs = new CalculateCellSizeRangesParametersClass();
                // Use the mosaic dataset operation interface to calculate cellsize ranges.
                mOp.CalculateCellSizeRanges(computeArgs, null);
                // Create a build boundary parameters object.
                IBuildBoundaryParameters boundaryArgs = new BuildBoundaryParametersClass();
                // Set flags that control boundary generation.
                boundaryArgs.AppendToExistingBoundary = true;
                // Use the mosaic dataset operation interface to build boundary.
                mOp.BuildBoundary(boundaryArgs, null);
                return true;
            }
            catch (Exception ex)
            {
                mess = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// 导出镶嵌数据集为删格数据
        /// </summary>
        /// <param name="RasterName">数据名称</param>
        /// <param name="workspaceDB">工作空间</param>
        /// <param name="DownLoadLocation">保存路径</param>
        /// <returns>成功返回true，失败返回false</returns>
        //public static bool DownLoadMosaic(ref string mess, string RasterName, IWorkspace workspaceDB, string DownLoadLocation)
        //{
        //    try
        //    {
        //        IWorkspace wsGDB = null;
        //        IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
        //        //判断是GDB文件还是普通文件夹
        //        string locationForm = DownLoadLocation.Substring(DownLoadLocation.Length - 4, 4).ToUpper();
        //        if (locationForm == ".GDB")
        //        {
        //            wsGDB = workspaceFactory.OpenFromFile(@"" + DownLoadLocation, 0);
        //        }
        //        else
        //        {
        //            IRasterWorkspace rasterWorkspace = SetRasterWorkspace(DownLoadLocation);
        //            wsGDB = (IWorkspace)rasterWorkspace;
        //        }
        //        IMosaicWorkspaceExtensionHelper mosaicHelper = new MosaicWorkspaceExtensionHelperClass();
        //        IMosaicWorkspaceExtension mosaicWs = mosaicHelper.FindExtension(workspaceDB);
        //        IMosaicDataset mosaic = mosaicWs.OpenMosaicDataset(RasterName);
        //        IFunctionRasterDataset functionDS = (IFunctionRasterDataset)mosaic;

        //        ISaveAs rasterSaveAs = (ISaveAs)functionDS;
        //        if (locationForm == ".GDB")
        //        {
        //            rasterSaveAs.SaveAs(RasterName, wsGDB, "GDB");
        //        }
        //        else
        //        {
        //            rasterSaveAs.SaveAs(RasterName + ".tif", wsGDB, "TIFF");
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mess = ex.Message;
        //        return false;
        //    }
        //}

    }
}
