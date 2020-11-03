using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace ImageProcessing
{
    public class excel精度检查type
    {
        /// <summary>
        /// excel 标题
        /// </summary>
        public string title;
        /// <summary>
        /// excel 景号
        /// </summary>
        public string scene;
        /// <summary>
        ///  参考点shp路径
        /// </summary>
        public string referencepath;
        /// <summary>
        /// 检查点shp路径
        /// </summary>
        public string checkpath;
        /// <summary>
        /// 填写exe参考序号
        /// </summary>
        public string reference;
        /// <summary>
        /// 填写exe检查序号
        /// </summary>
        public string check;
        /// <summary>
        /// 日期
        /// </summary>
        public string date;
        /// <summary>
        /// 人员
        /// </summary>
        public string checker;
        /// <summary>
        /// 检查标准
        /// </summary>
        public decimal standard;
        /// <summary>
        /// excel 文件名
        /// </summary>
        public string excelname;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string savepath;

        /// <summary>
        /// 
        /// </summary>
        public IFeatureClass reFeatureClass;
        /// <summary>
        /// 
        /// </summary>
        public IFeatureClass chFeatureClass;

        public ClassControlRecord ControlRecord;
    }
}
