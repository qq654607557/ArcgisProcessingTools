using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;

namespace ArcGISTool
{
    public class ShapefileRead
    {
        /// <summary>
        /// 打开Shape文件工作空间
        /// </summary>
        /// <param name="shapeFileFolder">文件目录</param>
        /// <returns>工作空间</returns>
        public static IWorkspace OpenShapeWorkspace(string shapeFileFolder)
        {
            IWorkspace pWorkspace = null;
            try
            {
                if (!string.IsNullOrEmpty(shapeFileFolder) && System.IO.Directory.Exists(shapeFileFolder))
                {
                    IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                    pWorkspace = pWorkspaceFactory.OpenFromFile(shapeFileFolder, 0);
                }
            }
            catch { pWorkspace = null; }
            return pWorkspace;
        }

        public static string CopyFile(string filePath, string out_path, bool overwrite=true)
        {
            //同名文件复制，循环处理
            string parentdir = System.IO.Path.GetDirectoryName(filePath);
            string filename = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string[] files = System.IO.Directory.GetFiles(parentdir, filename + ".*");
            string savefilename = filename;

            bool isrun = false;
            foreach (string fileold in files)
            {
                if (fileold.Contains(".lock")) continue;

                string filenamenew = System.IO.Path.GetFileName(fileold);
                string newpath = out_path + "\\" + filenamenew;

                System.IO.File.Copy(fileold, newpath, true);
                isrun = true;
            }

            return isrun ? (out_path + "\\" + filename + ".shp") : "";
        }

        public static string CopyFile(string filePath, string out_path, string out_name, bool overwrite = true)
        {
            //同名文件复制，循环处理
            string parentdir = System.IO.Path.GetDirectoryName(filePath);
            string filename = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string[] files = System.IO.Directory.GetFiles(parentdir, filename + ".*");
            string savefilename = out_name;

            bool isrun = false;
            foreach (string fileold in files)
            {
                if (fileold.Contains(".lock"))
                    continue;

                string sxtension = System.IO.Path.GetExtension(fileold);
                string filenamenew = savefilename + sxtension;// System.IO.Path.GetFileName(fileold);
                if (sxtension.ToUpper() == ".XML")
                {
                    if (System.IO.Path.GetFileName(fileold).ToUpper().IndexOf(".SHP") > 0)
                    {
                        filenamenew = savefilename + ".shp" + sxtension;
                    }
                }

                string newpath = out_path + "\\" + filenamenew;
                System.IO.File.Copy(fileold, newpath, overwrite);
                isrun = true;
            }

            return isrun ? (out_path + "\\" + savefilename + ".shp") : "";
        }

        public IFeatureClass ReadInfo(string strpath)
        {
            try
            {
                IFeatureLayer mCphFeatureLayer = OpenFile_FeatureLayer(strpath);
                IFeatureClass pCphFeatureClass = mCphFeatureLayer.FeatureClass;
                return pCphFeatureClass;
            }
            catch (Exception ex)
            {
                //throw new ArgumentNullException(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 打开Layer文件
        /// </summary>
        /// <param name="aFileName">要打开Shape文件的全路径</param>
        /// <param name="axMapControl"></param>
        public IFeatureLayer OpenFile_FeatureLayer(string aFileName)//打开shapefile文件
        {
            string fullPath;
            string path;//路径
            string fileName;//文件名
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            //IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactoryClass();
            fullPath = aFileName;
            path = System.IO.Path.GetDirectoryName(fullPath);//路径
            //fileName = System.IO.Path.GetFileName(fullPath);//文件名
            fileName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(path, 0);
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(fileName);
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pFeatureClass;
            pFeatureLayer.Name = pFeatureClass.AliasName;

            pWorkspace = null;
            //ILayer pLayer = pFeatureLayer as ILayer;
            return pFeatureLayer;
        }

        /// <summary>
        ///  裁剪图形
        /// </summary>
        /// <param name="pGeo_base"></param>
        /// <param name="pGeo_line"></param>
        /// <returns> 若没有图形 .IsEmpty 为 null</returns>
        public IGeometry Cut_IGeometry(IGeometry pGeo_base, IGeometry pGeo_line)
        {
            IGeometry outputGeometry = null;    //裁剪后的图形
            ITopologicalOperator2 topo = null;
            if (pGeo_base.SpatialReference != pGeo_line.SpatialReference)  //sourceGeometry为被裁剪的图形
            {
                pGeo_line.Project(pGeo_base.SpatialReference);
            }
            //此处应保持裁剪与被裁剪图层的空间参考一致，否则容易发生异常
            switch (pGeo_base.GeometryType)
            {
                case esriGeometryType.esriGeometryPolyline:
                    topo = pGeo_base as ITopologicalOperator2;
                    topo.IsKnownSimple_2 = true;
                    topo.Simplify();
                    outputGeometry = topo.Intersect(topo.Intersect(pGeo_line, esriGeometryDimension.esriGeometry1Dimension), esriGeometryDimension.esriGeometry1Dimension);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    topo = pGeo_base as ITopologicalOperator2;
                    topo.IsKnownSimple_2 = true;
                    topo.Simplify();
                    outputGeometry = topo.Difference(topo.Difference(pGeo_line));
                    break;
                default:
                    outputGeometry = pGeo_base;
                    break;
            }
            return outputGeometry;
        }

        public IGeometry Add_IGeometry(IGeometry pGeo_01, IGeometry pGeo_02)
        {
            if (pGeo_01 == null) return pGeo_02;
            if (pGeo_02 == null) return pGeo_01;

            IGeometry outputGeometry = null;    //裁剪后的图形
            ITopologicalOperator2 topo = null;

            if (pGeo_01.SpatialReference != pGeo_02.SpatialReference)  //sourceGeometry为被裁剪的图形
            {
                pGeo_01.Project(pGeo_02.SpatialReference);
            }

            topo = pGeo_01 as ITopologicalOperator2;
            topo.IsKnownSimple_2 = true;
            topo.Simplify();
            outputGeometry = topo.Union(pGeo_02);


            return outputGeometry;
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="GeometryA">要合并的一个Geometry</param>
        /// <param name="GeometryB">要合并的另一个Geometry</param>
        /// <returns>合并后的Geometry</returns>
        public static IGeometry UnionTwoGeometries(IGeometry GeometryA, IGeometry GeometryB)
        {
            ITopologicalOperator pTopologicalOperator = GeometryA as ITopologicalOperator;
            IGeometry UnionGeometry = pTopologicalOperator.Union(GeometryB);
            return UnionGeometry;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile">shp路径</param>
        /// <param name="folderpath">保存路径</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool CopyFileSHP(string sourceFile, string saveFile)
        {
            string pathSource = System.IO.Path.GetDirectoryName(sourceFile);//路径
            string nameSource = System.IO.Path.GetFileNameWithoutExtension(sourceFile);//文件名

            string pathSave = System.IO.Path.GetDirectoryName(saveFile);//路径
            string nameSave = System.IO.Path.GetFileNameWithoutExtension(saveFile);//文件名

            copyfile(pathSource + @"\" + nameSource + ".CPG", pathSave + @"\" + nameSave + ".CPG");
            copyfile(pathSource + @"\" + nameSource + ".dbf", pathSave + @"\" + nameSave + ".dbf");
            copyfile(pathSource + @"\" + nameSource + ".prj", pathSave + @"\" + nameSave + ".prj");
            copyfile(pathSource + @"\" + nameSource + ".shp", pathSave + @"\" + nameSave + ".shp");
            copyfile(pathSource + @"\" + nameSource + ".sbn", pathSave + @"\" + nameSave + ".sbn");
            copyfile(pathSource + @"\" + nameSource + ".sbx", pathSave + @"\" + nameSave + ".sbx");
            copyfile(pathSource + @"\" + nameSource + ".shp.xml", pathSave + @"\" + nameSave + ".shp.xml");
            copyfile(pathSource + @"\" + nameSource + ".shx", pathSave + @"\" + nameSave + ".shx");
            return System.IO.Path.IsPathRooted(saveFile);
        }

        static void copyfile(string sourceFile, string filepath)
        {
            try
            {
                bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                System.IO.File.Copy(sourceFile, filepath, isrewrite);
            }
            catch { }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="name"></param>
        /// <param name="aliasName"></param>
        /// <param name="FieldType"></param>
        public static void UpdateField(IFeatureClass pFeatureClass, string name, string aliasName, esriFieldType FieldType)
        {
            //若不存在，则不修改
            if (pFeatureClass.Fields.FindField(name) < 0) return;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Name_2 = name;
            pFieldEdit.Type_2 = FieldType;
            pFieldEdit.Length_2 = 255;

            IClass pClass = pFeatureClass as IClass;
            pClass.AddField(pField);
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="name"></param>
        /// <param name="aliasName"></param>
        /// <param name="FieldType"></param>
        public static void AddField(IFeatureClass pFeatureClass, string name, string aliasName, esriFieldType FieldType, int Length = 50)
        {
            //若存在，则不需添加
            if (pFeatureClass.Fields.FindField(name) > -1) return;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Name_2 = name;
            pFieldEdit.Type_2 = FieldType;
            pFieldEdit.Length_2 = Length;

            IClass pClass = pFeatureClass as IClass;
            pClass.AddField(pField);
        }

        /// <summary>
        /// 删除eSHP
        /// </summary>
        /// <param name="filePath">示例：E:\HiOS\TestShp</param>
        /// <param name="fileName">示例：Some</param>
        public static void DeleteSHP(string filePath, string fileName)
        {
            //打开shp工作空间
            //string fileName = "Some";//shp文件名
            //string filePath = @"E:\HiOS\TestShp";//shp文件位置
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();//文件夹
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(filePath, 0);
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;

            IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(fileName);//fileName为文件名(不包含路径)
            IDataset pFeaDataset = pFeatureClass as IDataset;
            pFeaDataset.Delete();

            pFeaDataset = null;
            pFeatureClass = null;
            pFeatureWorkspace = null;
            pWorkspace = null;
            pWorkspaceFactory = null;
        }
    }
}
