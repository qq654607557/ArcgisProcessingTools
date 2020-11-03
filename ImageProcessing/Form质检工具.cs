using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;

namespace ImageProcessing
{
    public partial class Form质检工具 : Form
    {


        public Form质检工具()
        {
            InitializeComponent();
        }

        private void Form质检工具_Load(object sender, EventArgs e)
        {
            load_recording();
        }

        private void txt_shp文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_shp文件夹.Text = fbd.SelectedPath;
            }
        }

        private void txt_影像文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_影像文件夹.Text = fbd.SelectedPath;
            }
        }

        private void txt_坐标值_ValueChanged(object sender, EventArgs e)
        {
            int coordinate = (int)this.txt_坐标值.Value;
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem(coordinate);
            this.lab_坐标.Text = spatialReference.Name;
        }

        private void txt_错误日志_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_错误日志.Text = fbd.SelectedPath;
            }
        }

        void showMess_Record(string mess)
        {
            this.Invoke((EventHandler)delegate
            {
                this.rtbRecord.AppendText(DateTime.Now.ToString("HH:mm ") + mess + "\n");
                rtbRecord.SelectionStart = rtbRecord.TextLength;
                rtbRecord.ScrollToCaret();
            });
        }

        void showMess_clear()
        {
            this.Invoke((EventHandler)delegate
            {
                this.rtbRecord.Text = "";
            });
        }

        Dictionary<string, Control> idc_rec_control = new Dictionary<string, Control>();
        private void save_recording()
        {
            List<string> opnamelist = new List<string>();
            foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
            {
                if (kvp.Value is NumericUpDown)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as NumericUpDown).Value.ToString("F0"));
                }
                else if (kvp.Value is CheckBox)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as CheckBox).Checked.ToString());
                }
                else
                {
                    opnamelist.Add(kvp.Key + "," + kvp.Value.Text);
                }
            }

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\Recording.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            txt.WriteTxt(opname_path, opnamelist);
        }

        private void load_recording()
        {
            idc_rec_control.Clear();

            idc_rec_control.Add("txt_shp文件夹", this.txt_shp文件夹);
            idc_rec_control.Add("txt_影像文件夹", this.txt_影像文件夹);

            idc_rec_control.Add("txt_坐标值", this.txt_坐标值);

            idc_rec_control.Add("chb_shp检测_数据全为大写", this.chb_shp检测_数据全为大写);
            idc_rec_control.Add("chb_检查坐标", this.chb_shp_检查坐标);

            //idc_rec_control.Add("", this.txt_shp文件夹);

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\Recording.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();

            List<string> opnamelist = txt.ReadTxt(opname_path);
            for (int i = 0; i < opnamelist.Count; i++)
            {
                string[] tem = opnamelist[i].Split(',');
                if (tem.Length == 2)
                {
                    foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
                    {

                        if (kvp.Key == tem[0])
                        {
                            if (kvp.Value is NumericUpDown)
                            {
                                decimal d = 0;
                                decimal.TryParse(tem[1], out d);
                                (kvp.Value as NumericUpDown).Value = d;
                            }
                            else if (kvp.Value is CheckBox)
                            {
                                bool t = false;
                                bool.TryParse(tem[1], out t);
                                (kvp.Value as CheckBox).Checked = t;
                            }
                            else
                            {
                                kvp.Value.Text = tem[1];
                            }
                        }
                    }
                }
            }
        }


        string shp_folder = "";
        bool shp_upper_is = true;//大写
        bool shp_Coordinate_is = true;//坐标
        int shp_Coordinate_value = 4490;
        string shp_lab = "";

        string img_folder = "";
        string log_path = "";

        private void btn开始检测_Click(object sender, EventArgs e)
        {
            shp_folder = this.txt_shp文件夹.Text.Trim();

            shp_upper_is = this.chb_shp检测_数据全为大写.Checked;
            //
            shp_Coordinate_is = this.chb_shp_检查坐标.Checked;
            shp_Coordinate_value = (int)this.txt_坐标值.Value;
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem(shp_Coordinate_value);
            shp_lab = spatialReference.Name;

            img_folder = this.txt_影像文件夹.Text.Trim();
            log_path = this.txt_错误日志.Text.Trim();
            if (string.IsNullOrEmpty(log_path))
            {
                log_path = System.Windows.Forms.Application.StartupPath + @"\log";
                if (!System.IO.Directory.Exists(log_path)) System.IO.Directory.CreateDirectory(log_path);
            }

            save_recording();

            System.Threading.Tasks.Task.Factory.StartNew(run);

        }

        void run()
        {
            showMess_clear();
            initialize_Error();
            showMess_Record("=============开始==================");
            try
            {
                showMess_Record("-> 检测shp文件");
                Dictionary<string, string> shp_name = new Dictionary<string, string>();
                int count_shp = 0;
                if (shp_upper_is ||
                    shp_Coordinate_is)
                {
                    string[] files_shp = System.IO.Directory.GetFiles(shp_folder, "*.shp");

                    count_shp = files_shp.Length;
                    for (int i = 0; i < count_shp; i++)
                    {
                        if (i % 20 == 0) { showMess_Record("-> 开始检测shp文件" + i.ToString() + "/" + count_shp); }
                        string shpname = run_shp_upper(files_shp[i]);
                        string foldername = System.IO.Path.GetFileNameWithoutExtension(files_shp[i]);

                        shp_name.Add(shpname, foldername);
                    }
                }
                showMess_Record("-> 结束检测shp文件");

                showMess_Record("-> 检测影像文件");
                string[] files_tif = System.IO.Directory.GetFiles(img_folder, "*.tif");
                string[] files_img = System.IO.Directory.GetFiles(img_folder, "*.img");
                string imgname = "";
                //for (int i = 0; i < count_shp; i++)
                // G48E012014GF102201903
                // G48E012014GF120201903
                for (int n = 0; n < files_tif.Length; n++)
                {
                    imgname = System.IO.Path.GetFileNameWithoutExtension(files_tif[n]);
                    if (shp_name.ContainsKey(imgname))
                    {
                        is_file(files_tif[n]);
                        is_img_SpatialReference(files_tif[n]);
                        shp_name.Remove(imgname);
                    }
                    else { add_Error("影像检测", "影像名称", "没有找到名称为【" + imgname.Substring(0,10)+"xq.shp" + "】的镶嵌线（shp）", ""); }
                }
                for (int n = 0; n > files_img.Length; n++)
                {
                    imgname = System.IO.Path.GetFileNameWithoutExtension(files_img[n]);

                    if (shp_name.ContainsKey(imgname))
                    {
                        is_file(files_img[n]);
                        is_img_SpatialReference(files_tif[n]);
                        shp_name.Remove(imgname);
                    }
                    else { add_Error("影像检测", "影像名称", "没有找到名称为【" + imgname.Substring(0, 10) + "xq.shp" + "】的镶嵌线（shp）", ""); }
                }
                foreach (KeyValuePair<string, string> kvp in shp_name)
                {
                    add_Error("影像检测", "影像名称", "没有找到名称为【" + kvp.Key + "】的影像文件（tif/img）", "");
                }
                if (dt.Rows.Count > 0)
                {
                    Form显示表格 f_show = new Form显示表格();
                    f_show.Get_DataTable = dt;
                    f_show.ShowDialog();

                    string logpath = log_path + @"\LOG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                    dt.WriteXml(logpath);
                    showMess_Record("错误条数：" + dt.Rows.Count.ToString() + "条");
                    showMess_Record("错误日志输出：" + logpath);
                }
                else
                {
                    showMess_Record("检测完成没有任何错误！");
                }

            }
            catch (Exception ex)
            {
                showMess_Record("==================================\n错误：" + ex.Message);
            }
            showMess_Record("=============结束==================");
        }

        string run_shp_upper(string shp_path)
        {
            // 基本数据
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shp_path);

            // 检测大写
            if (shp_upper_is)
            {
                IQueryFilter queryFilterSave = new QueryFilterClass();
                IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

                int col_count = featureClass.Fields.FieldCount;

                IFeature featureUpdata;
                while ((featureUpdata = featureCursor.NextFeature()) != null)
                {
                    string id = featureUpdata.get_Value(0).ToString();
                    for (int i = 2; i < col_count; i++)
                    {
                        string tem = featureUpdata.get_Value(i).ToString();
                        if (!is_upper(tem))
                        {
                            add_Error("SHP检测", "数据全为大写", "ID:" + id + "行," + i + "列有小写数据,值（" + tem + "）", shp_path);
                        }
                    }
                }
            }

            // 检测坐标
            if (shp_Coordinate_is)
            {
                IGeoDataset iGeoDataset = featureClass as IGeoDataset;
                ISpatialReference iSpatialReference = iGeoDataset.SpatialReference;
                string shp_sp;
                if (!is_SpatialReference(iSpatialReference, out shp_sp))
                {
                    add_Error("SHP检测", "检测坐标值", "shp文件坐标值为（" + shp_sp + "）应为（" + shp_lab + "）", shp_path);
                }
            }

            // 名字
            string shp_name = read_shp_name(featureClass);

            featureClass = null;

            return shp_name;
        }

        bool is_upper(string str)
        {
            foreach (var c in str)
            {
                if (c >= 'a' && c <= 'z')
                {
                    return false;
                }
            }
            return true;
        }

        bool is_SpatialReference(ISpatialReference iSpatialReference, out  string shp_sp)
        {
            if (shp_Coordinate_value == iSpatialReference.FactoryCode)
            {
                shp_sp = iSpatialReference.Name;
                return true;
            }
            else
            {
                shp_sp = (iSpatialReference.Name == null) ? "" : iSpatialReference.Name;
                return false;
            }
        }

        string is_file(string path)
        {
            bool isok = true;
            string extension = System.IO.Path.GetExtension(path);
            string foldername = System.IO.Path.GetFileNameWithoutExtension(path);

            string title = "影像检测";
            string filetitle = "文件不完整检测";

            string directory = System.IO.Path.GetDirectoryName(path);
            string files_xml = directory + "//" + foldername + ".xml";
            string files_mdb = directory + "//" + foldername + ".mdb";
            if (!System.IO.File.Exists(files_xml)) { add_Error(title, filetitle, "（" + foldername + ".xml" + "）文件不存在", files_xml); isok = false; }
            if (!System.IO.File.Exists(files_mdb)) { add_Error(title, filetitle, "（" + foldername + ".mdb" + "）文件不存在", files_mdb); isok = false; }

            switch (extension.ToUpper())
            {
                case ".TIF":
                    string files_tif = directory + "//" + foldername + ".tif";
                    string files_tfw = directory + "//" + foldername + ".tfw";
                    if (!System.IO.File.Exists(files_tif)) { add_Error(title, filetitle, "（" + foldername + ".tif" + "）文件不存在", files_tif); isok = false; }
                    if (!System.IO.File.Exists(files_tfw)) { add_Error(title, filetitle, "（" + foldername + ".tfw" + "）文件不存在", files_tfw); isok = false; }
                    break;
                case ".IMG":
                    string files_img = directory + "//" + foldername + ".img";
                    string files_ige = directory + "//" + foldername + ".ige";
                    if (!System.IO.File.Exists(files_img)) { add_Error(title, filetitle, "（" + foldername + ".img" + "）文件不存在", files_img); isok = false; }
                    if (!System.IO.File.Exists(files_ige)) { add_Error(title, filetitle, "（" + foldername + ".ige" + "）文件不存在", files_ige); isok = false; }
                    break;
                default:
                    add_Error(title, filetitle, "（" + foldername + "）文件不存在", directory);
                    isok = false;
                    break;
            }

            return foldername;
        }

        bool is_img_SpatialReference(string path)
        {
            string title = "影像检测";
            string filetitle = "检测影像坐标系";

            IEnvelope extent;
            GetTIFEnvelope(path, out extent);

            ISpatialReference isr = extent.SpatialReference;
            string name_isr = isr.Name == null ? "" : isr.Name;
            double xmin = extent.XMin;
            string xmin_str = xmin.ToString();
            if (xmin_str.Length > 2)
            {
                string top = xmin_str.Substring(0, 2);
                if (top == "35")//CGCS2000_3_Degree_GK_Zone_35 WKID: 4523 权限: EPSG
                {
                    if (isr.FactoryCode == 4523)
                    {
                        return true;
                    }
                    else
                    {
                        add_Error(title, filetitle, "坐标应为[CGCS2000_3_Degree_GK_Zone_35]现在为[" + name_isr + "]", path);
                        return false;
                    }
                }
                else if (top == "36")//CGCS2000_3_Degree_GK_Zone_36 WKID: 4524 权限: EPSG
                {
                    if (isr.FactoryCode == 4524)
                    {
                        return true;

                    }
                    else
                    {
                        add_Error(title, filetitle, "坐标应为[CGCS2000_3_Degree_GK_Zone_36]现在为[" + name_isr + "]", path);
                        return false;
                    }
                }
            }
            add_Error(title, filetitle, "值不在范围内不能检测", path);

            return false;
        }

        private IRasterLayer ReadFile(string filepath)
        {
            IRasterLayer rasterLayer = (IRasterLayer)new RasterLayerClass();
            rasterLayer.CreateFromFilePath(filepath);
            return rasterLayer;
        }


        private bool GetTIFEnvelope(string tifPath, out IEnvelope extent)
        {
            IRasterLayer rasterLayer = ReadFile(tifPath);
            IRaster raster = rasterLayer.Raster;
            IRasterProps rasterProps = raster as IRasterProps;
            extent = rasterProps.Extent; //当前栅格数据集的范围

            rasterProps = null;
            raster = null;
            rasterLayer = null;

            return true;
        }

        string read_shp_name(IFeatureClass featureClass)
        {
            // 基本数据
            //ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            //IFeatureClass featureClass = sr.ReadInfo(shp_path);

            //int idCol_NAME = featureClass.Fields.FindField("YXMC");

            IQueryFilter queryFilterSave = new QueryFilterClass();
            IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

            int idCol_TFH = featureClass.Fields.FindField("TFH");
            int idCol_FBL = featureClass.Fields.FindField("FBL");
            int idCol_SJY = featureClass.Fields.FindField("SJY");
            int idCol_SX = featureClass.Fields.FindField("SX");   // 判断最新时间
            int idCol_PAREA = featureClass.Fields.FindField("PAREA");

            string valueCol_TFH = "";

            string valueCol_SJY = "";// 面积最大
            double valueCol_PAREA = 0;

            string valueCol_FBL = "";// 2位(0.5->05 2->20)

            string valueCol_SX = "";// 时间最大（最新）前六位（年月）
            int v_sx = 0;


            IFeature featureUpdata;
            while ((featureUpdata = featureCursor.NextFeature()) != null)
            {
                if (string.IsNullOrEmpty(valueCol_TFH)) valueCol_TFH = featureUpdata.get_Value(idCol_TFH).ToString();

                if (string.IsNullOrEmpty(valueCol_FBL))
                {
                    valueCol_FBL = featureUpdata.get_Value(idCol_FBL).ToString();
                    if (valueCol_FBL == "0.5") valueCol_FBL = "05";
                    else if (valueCol_FBL == "2") valueCol_FBL = "20";
                }

                // 取面积最大
                double tem_PAREA = 0;
                double.TryParse(featureUpdata.get_Value(idCol_PAREA).ToString(), out tem_PAREA);
                if (tem_PAREA > valueCol_PAREA)
                {
                    valueCol_PAREA = tem_PAREA;
                    valueCol_SJY = featureUpdata.get_Value(idCol_SJY).ToString();
                }


                // 取时间最新
                int tem_time = 0;
                int.TryParse(featureUpdata.get_Value(idCol_SX).ToString(), out tem_time);
                if (tem_time > v_sx)
                {
                    v_sx = tem_time;
                }
            }

            valueCol_SX = v_sx.ToString();
            if (valueCol_SX.Length > 6) valueCol_SX = valueCol_SX.Substring(0, 6);

            string valueCol_YXMC = valueCol_TFH + valueCol_SJY + valueCol_FBL + valueCol_SX;

            featureUpdata = null;
            featureCursor = null;
            queryFilterSave = null;
            featureClass = null;

            return valueCol_YXMC;
        }

        DataTable dt = new DataTable();
        void initialize_Error()
        {
            dt = new DataTable("错误日志");
            dt.Columns.Add("ID");
            dt.Columns.Add("项目");
            dt.Columns.Add("错误");
            dt.Columns.Add("内容");
            dt.Columns.Add("文件");
        }

        void add_Error(string xm, string cw, string content, string filepath)
        {
            dt.Rows.Add(dt.Rows.Count + 1, xm, cw, content, filepath);
        }



    }
}
