using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using HelperArcGIS.SupportFile;

namespace ImageProcessing
{
    public partial class Form读写四点坐标 : Form
    {
        ClassMessRecord MessRecord = null;
        ClassControlRecord ControlRecord = new ClassControlRecord();
        Dictionary<string, Config_JH> config_jh = new Dictionary<string, Config_JH>();

        public Form读写四点坐标()
        {
            InitializeComponent();
            MessRecord = new ClassMessRecord(this, this.rtbRecord);
        }

        private void Form读写四点坐标_Load(object sender, EventArgs e)
        {
            loading_txt();
            loadconfig_JH();

            ControlRecord.Add(this.panelInput.Controls);
            ControlRecord.Add(this.gb_四点位置顺序.Controls);
            ControlRecord.Add(this.gb_坐标转换.Controls);
            ControlRecord.Add(this.flowLayoutPanel1.Controls);
            ControlRecord.Load();
        }


        private void txt_分幅SHP_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "SHP文件(*.shp)|*.shp";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string txt = "";
                string[] files = dlg.FileNames;
                for (int i = 0; i < files.Length; i++) txt += files[i];
                this.txt_分幅SHP.Text = txt;
            }
        }

        private void txt_镶嵌线SHP_文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_镶嵌线SHP_文件夹.Text = fbd.SelectedPath;
            }
        }

        private void txt_mdb模板_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "MDB文件(*.mdb)|*.mdb";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string txt = "";
                string[] files = dlg.FileNames;
                for (int i = 0; i < files.Length; i++) txt += files[i];
                this.txt_mdb模板.Text = txt;
            }
        }

        private void txt_输出成果_文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_输出成果_文件夹.Text = fbd.SelectedPath;
            }
        }

        #region 读取方式

        Dictionary<string, Control> dic_Control = new Dictionary<string, Control>();

        void loading_txt()
        {
            dic_wsc.Clear();
            dic_Control.Clear();

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\setting.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();

            List<string> opnamelist = txt.ReadTxt(opname_path);
            for (int i = 0; i < opnamelist.Count; i++)
            {
                string[] tem = opnamelist[i].Split(',');
                if (tem.Length == 5)
                {
                    WritShpConfig wsc = new WritShpConfig()
                    {
                        SHP_Field_Name = tem[2],
                        Con_Type = tem[0],
                        SHP_Row_Dig = tem[4],
                        Name = tem[1],
                        SHP_Row_Type = tem[3]
                    };
                    dic_wsc.Add(wsc.SHP_Field_Name, wsc);

                    switch (wsc.Con_Type)
                    {
                        case "TXT":
                            UC_txt uctxt = new UC_txt()
                            {
                                Name = wsc.SHP_Field_Name,
                                Title = wsc.Name
                            };
                            this.flowLayoutPanel1.Controls.Add(uctxt);
                            dic_Control.Add(wsc.SHP_Field_Name, uctxt);
                            break;
                        case "AUT":
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        void loadconfig_JH()
        {
            config_jh.Clear();

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\setting_JH.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();


            List<string> opnamelist = txt.ReadTxt(opname_path);
            for (int i = 0; i < opnamelist.Count; i++)
            {
                string[] tem = opnamelist[i].Split(',');
                if (tem.Length == 6)
                {
                    Config_JH cjh = new Config_JH()
                    {
                        WXGDH = tem[1],
                     
                        WXGDH_StartIndex = int.Parse(tem[2]),
                        WXGDH_EndIndex = int.Parse(tem[3]),

                        CGQLX_StartIndex = int.Parse(tem[4]),
                        CGQLX_EndIndex = int.Parse(tem[5])
                    };

                    config_jh.Add(tem[0], cjh);
                }
            }
        }

        #endregion

        #region 全局数据

        Dictionary<string, WritShpConfig> dic_wsc = new Dictionary<string, WritShpConfig>();
        string str_mdb模板 = "";
        string str_分幅SHP = "";
        string str_输出成果_文件夹 = "";
        string str_镶嵌线SHP_文件夹 = "";
        int wkid = 0;
        int point_start = 0;
        bool is_Show填写 = false;

        #endregion

        private void btn开始检测_Click(object sender, EventArgs e)
        {
            str_mdb模板 = this.txt_mdb模板.Text.Trim();
            str_分幅SHP = this.txt_分幅SHP.Text.Trim();
            str_输出成果_文件夹 = this.txt_输出成果_文件夹.Text.Trim();
            str_镶嵌线SHP_文件夹 = this.txt_镶嵌线SHP_文件夹.Text.Trim();
            is_Show填写 = this.txtc_弹出卫星填写框.Checked;
            wkid = (int)this.txtn_WKID.Value;

            if (string.IsNullOrEmpty(str_mdb模板)) { MessageBox.Show("请填写“mdb模板”！"); return; }
            if (string.IsNullOrEmpty(str_分幅SHP)) { MessageBox.Show("请填写“分幅SHP”！"); return; }
            if (string.IsNullOrEmpty(str_输出成果_文件夹)) { MessageBox.Show("请填写“输出成果（文件夹）”！"); return; }
            if (string.IsNullOrEmpty(str_镶嵌线SHP_文件夹)) { MessageBox.Show("请填写“镶嵌线SHP（文件夹）”！"); return; }
            if (wkid == 0) { MessageBox.Show("WKID错误！"); return; }

            point_start = (int)this.numericUpDown1.Value;
            foreach (KeyValuePair<string, Control> kvp in dic_Control)
            {
                if (dic_wsc.ContainsKey(kvp.Key)) dic_wsc[kvp.Key].SHP_Row_value = (kvp.Value as UC_txt).Text;
            }

            ControlRecord.Save();

            System.Threading.Tasks.Task.Factory.StartNew(run);
        }

        void run()
        {
            MessRecord.Clear();
            MessRecord.Record("=================开始=================");
            try
            {
                MessRecord.Record("-> 读取配置文件");
                string[] ps_xy = read_四点坐标列名();

                if (!System.IO.File.Exists(str_分幅SHP)) { MessRecord.Record("-> 没有找到文件：" + str_分幅SHP); goto TitleEnd; }
                if (!System.IO.File.Exists(str_mdb模板)) { MessRecord.Record("-> 没有找到文件：" + str_mdb模板); goto TitleEnd; }
                if (!System.IO.Directory.Exists(str_镶嵌线SHP_文件夹)) { MessRecord.Record("-> 没有找到文件夹：" + str_镶嵌线SHP_文件夹); goto TitleEnd; }
                if (!System.IO.Directory.Exists(str_输出成果_文件夹)) System.IO.Directory.CreateDirectory(str_输出成果_文件夹);

                read_分幅SHP(ps_xy);
            }
            catch (Exception ex)
            {
                MessRecord.Record("=================错误=================\n" + ex.Message);
            }

        TitleEnd:
            MessRecord.Record("=================结束=================");
        }

        string[] read_四点坐标列名()
        {
            string[] ps_xy = new string[8];

            foreach (KeyValuePair<string, WritShpConfig> kvp in dic_wsc)
            {
                if (kvp.Value.Con_Type == "AUT")
                {
                    switch (kvp.Value.Name)
                    {
                        case "东北图廓角点X坐标": ps_xy[0] = kvp.Value.SHP_Field_Name; break;
                        case "东北图廓角点Y坐标": ps_xy[1] = kvp.Value.SHP_Field_Name; break;

                        case "东南图廓角点X坐标": ps_xy[2] = kvp.Value.SHP_Field_Name; break;
                        case "东南图廓角点Y坐标": ps_xy[3] = kvp.Value.SHP_Field_Name; break;

                        case "西南图廓角点X坐标": ps_xy[4] = kvp.Value.SHP_Field_Name; break;
                        case "西南图廓角点Y坐标": ps_xy[5] = kvp.Value.SHP_Field_Name; break;

                        case "西北图廓角点X坐标": ps_xy[6] = kvp.Value.SHP_Field_Name; break;
                        case "西北图廓角点Y坐标": ps_xy[7] = kvp.Value.SHP_Field_Name; break;
                    }
                }
            }

            return ps_xy;
        }

        void read_分幅SHP(string[] ps_xy)
        {
            List<string> errorInfo = new List<string>();
            List<string> completeInfo = new List<string>();

            MessRecord.Record("-> 读取分幅SHP");
            string shp_path = str_分幅SHP;

            // 基本数据
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shp_path);

            int col_Name = featureClass.Fields.FindField("Name");

            IQueryFilter queryFilterSave = new QueryFilterClass();
            IFeatureCursor featureCursor = featureClass.Search(null, false);

            int sum = 0;
            DataTable dt_log = new DataTable("日志");
            dt_log.Columns.Add("ID");
            dt_log.Columns.Add("图幅号");
            dt_log.Columns.Add("状态");
            dt_log.Columns.Add("信息");

            IFeature featureUpdata;
            while ((featureUpdata = featureCursor.NextFeature()) != null)
            {
                sum++;
                string mess = "";
                string str_Name = featureUpdata.get_Value(col_Name).ToString();
                IGeometry shape = featureUpdata.ShapeCopy;
                MessRecord.Record("-> [" + sum.ToString("D3") + "]开始处理【" + str_Name + "】");

                string srt_xq = str_镶嵌线SHP_文件夹 + @"\" + str_Name + "xq.shp";
                if (!System.IO.File.Exists(srt_xq)) { mess = "【" + str_Name + "】错误：没有找到镶嵌线SHP：" + srt_xq; goto titleError; }

                // MessRecord.Record("-> 拷贝MDB:" + copymdb);
                XQ_Info xq_Info = writeIFeatureClass(str_Name, srt_xq);
                if (xq_Info == null) { mess = "【" + str_Name + "】错误：打开镶嵌线失败SHP：" + srt_xq; goto titleError; }

                string copymdb = str_输出成果_文件夹 + @"\" + xq_Info.YSJWJM + ".mdb";
                System.IO.File.Copy(str_mdb模板, copymdb, true);
                //completeInfo.Add("完成：【" + copymdb + "】");

                //// MessRecord.Record("开始写入" + copymdb + "");
                IWorkspace pWorkspace = HelperMDB.Open(copymdb);
                if (pWorkspace == null) { mess = "【" + str_Name + "】错误：不能打开文件【" + copymdb + "】"; goto titleError; }
                IEnumDataset enumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
                IDataset dataset = enumDataset.Next();
                if (dataset == null) { mess = "【" + str_Name + "】错误：文件结构错误【" + copymdb + "】"; goto titleError; }
                IFeatureClass featureClass_mdb = dataset as IFeatureClass;

                foreach (KeyValuePair<string, WritShpConfig> kvp in dic_wsc)
                {
                    kvp.Value.SHP_Field_int = featureClass_mdb.Fields.FindField(kvp.Value.SHP_Field_Name);
                }

                int col_YSJWJM = featureClass_mdb.Fields.FindField("YSJWJM");
                int col_WXMC = featureClass_mdb.Fields.FindField("WXMC");
                int col_WXGDH = featureClass_mdb.Fields.FindField("WXGDH");

                int col_CGQLX = featureClass_mdb.Fields.FindField("CGQLX");
                int col_YXFBL = featureClass_mdb.Fields.FindField("YXFBL");
                int col_YXHQSJ = featureClass_mdb.Fields.FindField("YXHQSJ");

                int col_TH = featureClass_mdb.Fields.FindField("TH");
                int col_MFQK = featureClass_mdb.Fields.FindField("MFQK");
                int col_CSJ = featureClass_mdb.Fields.FindField("CSJ");

                int col_TKJDJDFW = featureClass_mdb.Fields.FindField("TKJDJDFW");
                int col_TKJDWDFW = featureClass_mdb.Fields.FindField("TKJDWDFW");
                int col_YXDMFBL = featureClass_mdb.Fields.FindField("YXDMFBL"); 

                xq_Info = write分幅(xq_Info, featureUpdata.ShapeCopy, ps_xy);

                IFeature featureUpdata_mdb = featureClass_mdb.CreateFeature();

                featureUpdata_mdb.Shape = writeIGeometry(featureUpdata.ShapeCopy);

                featureUpdata_mdb.set_Value(col_YSJWJM, xq_Info.YSJWJM);
                featureUpdata_mdb.set_Value(col_WXMC, xq_Info.WXMC);
                featureUpdata_mdb.set_Value(col_WXGDH, xq_Info.CGQLX); //

                featureUpdata_mdb.set_Value(col_CGQLX, xq_Info.WXGDH); //
                featureUpdata_mdb.set_Value(col_YXFBL, xq_Info.YXFBL);
                featureUpdata_mdb.set_Value(col_YXHQSJ, xq_Info.YXHQSJ);

                featureUpdata_mdb.set_Value(col_TH, xq_Info.TH);
                featureUpdata_mdb.set_Value(col_MFQK, xq_Info.MFQK);
                featureUpdata_mdb.set_Value(col_CSJ, xq_Info.CSJ);

                featureUpdata_mdb.set_Value(col_TKJDJDFW, xq_Info.TKJDJDFW);
                featureUpdata_mdb.set_Value(col_TKJDWDFW, xq_Info.TKJDWDFW);
                //featureUpdata_mdb.set_Value(col_YXDMFBL, xq_Info.YXDMFBL);

                writeIFeatureClass(featureUpdata_mdb);

                featureUpdata_mdb.Store();
                pWorkspace = null;

                mess = "完成：【" + copymdb + "】";
                dt_log.Rows.Add(sum, str_Name, "成功", mess);
                completeInfo.Add(mess);
                continue;

            titleError:
                dt_log.Rows.Add(sum, str_Name, "失败", mess);
                errorInfo.Add(mess);
            }

            sum = 0;

            MessRecord.Record("=============================");
            for (int i = 0; i < completeInfo.Count; i++) { MessRecord.Record(i.ToString("D3") + " " + completeInfo[i]); }
            MessRecord.Record("=============================");
            for (int i = 0; i < errorInfo.Count; i++) { MessRecord.Record(i.ToString("D3") + " " + errorInfo[i]); }
            MessRecord.Record("=============================");
            MessRecord.Record("-> 处理数据条，成功" + completeInfo.Count.ToString("D3") + "条，失败" + errorInfo.Count.ToString("D3") + "条");

            string log_Directory = System.Windows.Forms.Application.StartupPath + @"\syslog";
            if (!System.IO.Directory.Exists(log_Directory)) System.IO.Directory.CreateDirectory(log_Directory);
            string log_file = log_Directory + @"\Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            DataView dv = dt_log.DefaultView;
            dv.Sort = "图幅号";
            dv.ToTable().WriteXml(log_file);
            MessRecord.Record("-> 日志输出：" + log_file);
        }

        IGeometry writeIGeometry(IGeometry geometry)
        {
            IZAware zAware = geometry as IZAware;
            zAware.DropZs();
            zAware.ZAware = false;
            return geometry;
        }

        double getIGeometryArea(IGeometry geometry)
        {
            ISpatialReferenceFactory srf = new SpatialReferenceEnvironment();
            geometry.Project(srf.CreateProjectedCoordinateSystem(wkid));
            IArea area = geometry as IArea;
            double area_double = area.Area * 0.000001;// 平方千米
            area_double = Math.Round(area_double, 2);
            return area_double;
        }

        void getRe(IGeometry geometry, out string log, out string lat)
        {
            double lon_max = 0, lat_max = 0, lon_mix = 0, lat_mix = 0;

            IPolygon iPolygon = geometry as IPolygon;
            IPointCollection iPointCollection = iPolygon as IPointCollection;
            for (int i = 0; i < iPointCollection.PointCount; i++)
            {
                IPoint iPoint = iPointCollection.get_Point(i);

                if (i == 0)
                {
                    lon_max = iPoint.X;
                    lon_mix = iPoint.X;
                    lat_max = iPoint.Y;
                    lat_mix = iPoint.Y;
                }
                else
                {
                    if (lon_max < iPoint.X) lon_max = iPoint.X;
                    if (lon_mix > iPoint.X) lon_mix = iPoint.X;
                    if (lat_max < iPoint.Y) lat_max = iPoint.Y;
                    if (lat_mix > iPoint.Y) lat_max = iPoint.Y;
                }
            }

            log = HelperArcGIS.HelperLonLat.ComvertDigitalToDegrees(lon_mix) + "-" + HelperArcGIS.HelperLonLat.ComvertDigitalToDegrees(lon_max);
            lat = HelperArcGIS.HelperLonLat.ComvertDigitalToDegrees(lat_mix) + "-" + HelperArcGIS.HelperLonLat.ComvertDigitalToDegrees(lat_max);
        }

        XQ_Info write分幅(XQ_Info xq_Info, IGeometry geometry, string[] ps_xy)
        {
            //IGeometry geometry = featureUpdata.ShapeCopy;
            // 读取经纬度范围
            string log_string, lat_string;
            getRe(geometry, out log_string, out  lat_string);

            xq_Info.TKJDJDFW = log_string;// 经度范围
            xq_Info.TKJDWDFW = lat_string;// 纬度范围

            double area = getIGeometryArea(geometry);
            xq_Info.MFQK = Math.Round(xq_Info.Sum_area / area * 100, 0).ToString("F0"); // "";// 满幅情况

            // 填写四点坐标
            IPolygon iPolygon = geometry as IPolygon;
            IPointCollection iPointCollection = iPolygon as IPointCollection;
            for (int i = 0; i < iPointCollection.PointCount; i++)
            {
                if (i == 4) continue;
                IPoint iPoint = iPointCollection.get_Point(i);
                string ip_x = iPoint.X.ToString();
                string ip_y = iPoint.Y.ToString();
                switch (i)
                {
                    case 0:
                        dic_wsc[ps_xy[js_point(0)]].SHP_Row_value = ip_x;
                        dic_wsc[ps_xy[js_point(1)]].SHP_Row_value = ip_y;
                        break;
                    case 1:
                        dic_wsc[ps_xy[js_point(2)]].SHP_Row_value = ip_x;
                        dic_wsc[ps_xy[js_point(3)]].SHP_Row_value = ip_y;
                        break;
                    case 2:
                        dic_wsc[ps_xy[js_point(4)]].SHP_Row_value = ip_x;
                        dic_wsc[ps_xy[js_point(5)]].SHP_Row_value = ip_y;
                        break;
                    case 3:
                        dic_wsc[ps_xy[js_point(6)]].SHP_Row_value = ip_x;
                        dic_wsc[ps_xy[js_point(7)]].SHP_Row_value = ip_y;
                        break;
                }
            }



            return xq_Info;
        }

        XQ_Info writeIFeatureClass(string TFH, string srt_xq)
        {
            XQ_Info xq_info = new XQ_Info();
            // 读取镶嵌
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(srt_xq);
            if (featureClass == null) return null;

            //int col_Name = featureClass.Fields.FindField("Name");
            int col_PAREA = featureClass.Fields.FindField("PAREA");
            int col_TFH = featureClass.Fields.FindField("TFH");
            int col_SJY = featureClass.Fields.FindField("SJY");
            int col_SX = featureClass.Fields.FindField("SX");
            int col_FBL = featureClass.Fields.FindField("FBL");
            int col_JH = featureClass.Fields.FindField("JH");
            int col_YXMC = featureClass.Fields.FindField("YXMC");
            int col_CSJ = featureClass.Fields.FindField("CSJ");
            int col_CYJG = featureClass.Fields.FindField("CYJG");    

            IQueryFilter queryFilterSave = new QueryFilterClass();
            IFeatureCursor featureCursor = featureClass.Search(null, false);

            string value_FBL = "";
            string valueCol_CYJG = "";
            Dictionary<string, YXMC_List> ear = new Dictionary<string, YXMC_List>();

            IFeature featureUpdata;
            double sum_area = 0;
            while ((featureUpdata = featureCursor.NextFeature()) != null)
            {
                IGeometry geometry = featureUpdata.ShapeCopy;

                double area_double = getIGeometryArea(geometry);

                sum_area += area_double;
                featureUpdata.set_Value(col_PAREA, area_double);

                string valueCol_SJY = featureUpdata.get_Value(col_SJY).ToString();
                string valueCol_SX = featureUpdata.get_Value(col_SX).ToString();
                string valueCol_JH = featureUpdata.get_Value(col_JH).ToString();
                string valueCol_CSJ = featureUpdata.get_Value(col_CSJ).ToString();

                if (!string.IsNullOrEmpty(valueCol_SX) && valueCol_SX.Length > 6) valueCol_SX = valueCol_SX.Substring(0, 6);

                YXMC_Info yxmc_info = new YXMC_Info() { AREA = area_double, TimeStr = valueCol_SX, JH = valueCol_JH, CSJ = valueCol_CSJ };
                if (ear.ContainsKey(valueCol_SJY))
                {
                    ear[valueCol_SJY].sumAREA = ear[valueCol_SJY].sumAREA + area_double;
                    ear[valueCol_SJY].YXMClist.Add(yxmc_info);
                }
                else
                {
                    YXMC_List yl = new YXMC_List() { sumAREA = area_double, strName = valueCol_SJY };
                    yl.YXMClist.Add(yxmc_info);
                    ear.Add(valueCol_SJY, yl);
                }

                value_FBL = featureUpdata.get_Value(col_FBL).ToString();
                valueCol_CYJG = featureUpdata.get_Value(col_CYJG).ToString();
                if (valueCol_CYJG == "0.5") valueCol_CYJG = "05";
                else if (valueCol_CYJG == "2") valueCol_CYJG = "20";

                featureUpdata.Store();
            }

            // 找到面积最大
            YXMC_List kvp_tem = null;
            foreach (KeyValuePair<string, YXMC_List> kvp in ear)
            {
                if (kvp_tem == null) { kvp_tem = kvp.Value; continue; }
                else if (kvp_tem.sumAREA < kvp.Value.sumAREA) { kvp_tem = kvp.Value; }
            }
            // 找到时间最新
            YXMC_Info yi_tem = null;
            foreach (YXMC_Info yi in kvp_tem.YXMClist)
            {
                if (yi_tem == null) { yi_tem = yi; continue; }
                else if (int.Parse(yi_tem.TimeStr) < int.Parse(yi.TimeStr)) { yi_tem = yi; }
            }
            xq_info.YXMC = TFH + kvp_tem.strName + valueCol_CYJG + yi_tem.TimeStr;

            // 再找面积最大
            YXMC_Info yi_area_tem = null;
            foreach (YXMC_Info yi in kvp_tem.YXMClist)
            {
                if (yi_area_tem == null) { yi_area_tem = yi; continue; }
                else if (yi_area_tem.AREA < yi.AREA) { yi_area_tem = yi; }
            }
            xq_info.JH = yi_area_tem.JH;

            featureCursor = featureClass.Search(queryFilterSave, false);
            while ((featureUpdata = featureCursor.NextFeature()) != null)
            {
                // 写
                featureUpdata.set_Value(col_TFH, TFH);
                featureUpdata.set_Value(col_YXMC, xq_info.YSJWJM);

                featureUpdata.Store();
            }
            //xq_info.JH="";
            //xq_info.YXMC = ""; 

            xq_info.CGQLX = "";//传感器类型         
            xq_info.WXGDH = "";// 卫星轨道号

            //xq_info.MFQK = Math.Round(sum_area / area_all, 0).ToString("F0"); // "";// 满幅情况
            xq_info.TH = TFH;
            xq_info.Sum_area = sum_area;

            //xq_info.TKJDJDFW = "";// 经度范围
            //xq_info.TKJDWDFW = "";// 纬度范围

            xq_info.WXMC = kvp_tem.strName;
            xq_info.YXFBL = value_FBL;
            xq_info.YXDMFBL = valueCol_CYJG;
            xq_info.YXHQSJ = yi_tem.TimeStr;
            xq_info.CSJ = yi_tem.CSJ;

            //MessRecord.Record("-> JH:" + xq_info.JH);
            //config_jh
           string str_wxmc= xq_info.JH.Split('_')[0];
           if (config_jh.ContainsKey(str_wxmc))
           {
               Config_JH cjh = config_jh[str_wxmc];
               //MessRecord.Record("-> str_wxmc:" + cjh.WXGDH_StartIndex + "," + cjh.WXGDH_EndIndex + "|" + cjh.CGQLX_StartIndex+","+cjh.CGQLX_EndIndex);
               if (string.IsNullOrEmpty(cjh.WXGDH)) xq_info.WXGDH = xq_info.JH.Substring(cjh.WXGDH_StartIndex, cjh.WXGDH_EndIndex);
               else xq_info.WXGDH = cjh.WXGDH;
               xq_info.CGQLX = xq_info.JH.Substring(cjh.CGQLX_StartIndex, cjh.CGQLX_EndIndex);

           }
           else if (config_jh.ContainsKey(xq_info.WXMC))
           {
               //MessRecord.Record("-> WXMC:" + xq_info.JH);
               Config_JH cjh = config_jh[xq_info.WXMC];
               //MessRecord.Record("-> str_wxmc:" + cjh.WXGDH_StartIndex + "," + cjh.WXGDH_EndIndex + "|" + cjh.CGQLX_StartIndex + "," + cjh.CGQLX_EndIndex);
               if (string.IsNullOrEmpty(cjh.WXGDH)) xq_info.WXGDH = xq_info.JH.Substring(cjh.WXGDH_StartIndex, cjh.WXGDH_EndIndex);
               else xq_info.WXGDH = cjh.WXGDH;
               xq_info.CGQLX = xq_info.JH.Substring(cjh.CGQLX_StartIndex, cjh.CGQLX_EndIndex);
           }
           else
           {
               if (is_Show填写)
               {
                   Form读写四点坐标_传感器填写 f = new Form读写四点坐标_传感器填写();
                   f.JH = xq_info.JH;
                   f.ShowDialog();
                   if (f.IsOK)
                   {
                       xq_info.WXGDH = f.WXGDH;//f.CGQLX
                       xq_info.CGQLX = f.CGQLX;
                   }
               }
           }
           //MessRecord.Record("-> WXGDH:" + xq_info.WXGDH);
           //MessRecord.Record("-> CGQLX:" + xq_info.CGQLX);

            featureUpdata = null;
            featureCursor = null;
            queryFilterSave = null;
            featureClass = null;

            return xq_info;
        }

        void writeIFeatureClass(IFeature featureUpdata)
        {
            string id = featureUpdata.get_Value(0).ToString();

            // 填写固定值
            foreach (KeyValuePair<string, WritShpConfig> kvp in dic_wsc)
            {
                if (kvp.Value.SHP_Field_int > 0)
                {
                    if (string.IsNullOrEmpty(kvp.Value.SHP_Row_value)) continue;
                    switch (kvp.Value.SHP_Row_Type)
                    {
                        case "FLOAT":
                            double tim_f;
                            string str = kvp.Value.SHP_Row_value;
                            if (double.TryParse(kvp.Value.SHP_Row_value, out tim_f)) featureUpdata.set_Value(kvp.Value.SHP_Field_int, (string.IsNullOrEmpty(kvp.Value.SHP_Row_Dig) ? tim_f.ToString() : tim_f.ToString(kvp.Value.SHP_Row_Dig)));
                            break;
                        case "INTEGER":
                            int tim_i;
                            if (int.TryParse(kvp.Value.SHP_Row_value, out tim_i)) featureUpdata.set_Value(kvp.Value.SHP_Field_int, (string.IsNullOrEmpty(kvp.Value.SHP_Row_Dig) ? tim_i.ToString() : tim_i.ToString(kvp.Value.SHP_Row_Dig)));
                            break;
                        case "TEXT":
                        default:
                            featureUpdata.set_Value(kvp.Value.SHP_Field_int, kvp.Value.SHP_Row_value);
                            break;
                    }

                }

            }
        }

        int js_point(int xy)
        {

            switch (point_start)
            {
                case 1:
                default:
                    return xy;
                case 2:
                    xy += 6;
                    if (xy >= 8)
                    {
                        xy -= 8;
                    }
                    return xy;
                case 3:
                    xy += 4;
                    if (xy >= 8)
                    {
                        xy -= 8;
                    }
                    return xy;
                case 4:
                    xy += 2;
                    if (xy >= 8)
                    {
                        xy -= 8;
                    }
                    return xy;
            }
        }

        private void rb_WKID_CheckedChanged(object sender, EventArgs e)
        {
            int wkid = 4490;//GCS_China_Geodetic_Coordinate_System_2000
            if (this.rb_WKID_35.Checked) wkid = 4523; // CGCS2000_3_Degree_GK_Zone_35
            else if (this.rb_WKID_36.Checked) wkid = 4524;//CGCS2000_3_Degree_GK_Zone_36
            else { }

            this.txtn_WKID.Value = wkid;
        }

        class YXMC_List
        {
            public double sumAREA = 0;
            public String strName = "";
            public List<YXMC_Info> YXMClist = new List<YXMC_Info>();
        }

        class YXMC_Info
        {
            public double AREA = 0;
            public String TimeStr = "";
            public String JH = "";
            public String CSJ = "";
        }

        class XQ_Info
        {
            string yxmc;
            public string JH = "";
            public string YXMC { set { yxmc = value; } }

            public string YSJWJM { get { return yxmc; } }
            public string WXMC;
            public string WXGDH;

            public string CGQLX;
            /// <summary>
            /// 卫星影像分辨率(FBL)
            /// </summary>
            public string YXFBL;
            /// <summary>
            /// 影像地面分辨率(CYJG)
            /// </summary>
            public string YXDMFBL;
            public string YXHQSJ;
            public string CSJ;

            public string TH;
            public string MFQK;
            public string TKJDJDFW;
            public string TKJDWDFW;

            public double Sum_area;
        }

        class Config_JH
        {
            public string WXGDH;
            public int WXGDH_StartIndex;
            public int WXGDH_EndIndex;
            public int CGQLX_StartIndex;
            public int CGQLX_EndIndex;
        }
    }

    public class WritShpConfig
    {
        public string Con_Type;
        public string Name;
        public string SHP_Field_Name;
        public string SHP_Row_Type;
        /// <summary>
        /// 位数
        /// </summary>
        public string SHP_Row_Dig;

        public string SHP_Row_value;
        public int SHP_Field_int = -1;
    }

    public class shp_镶嵌线
    {
        public string YXMC;
        public string SJY;
        public string SX;
        public string CSJ;
    }

    /// <summary>
    /// 
    /// </summary>
    public class specialFunctions
    {
        /// <summary>
        /// 数字经纬度和度分秒经纬度转换
        /// </summary>
        /// <param name="digitalLati_Longi">数字经纬度</param>
        /// <returns>度分秒经纬度</returns>
        public static string ConvertDigitalToDegrees(string digitalLati_Longi)
        {
            double digitalDegree = Convert.ToDouble(digitalLati_Longi);
            return ComvertDigitalToDegrees(digitalDegree);
        }

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换
        /// </summary>
        /// <param name="digitalDegree">数字经纬度</param>
        /// <returns>度分秒经纬度</returns>
        public static string ComvertDigitalToDegrees(double digitalDegree)
        {
            const double num = 60;
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = (tmp - minute) * num;
            string degrees = "" + degree + "°" + minute + "′" + second + "″";
            return degrees;
        }

        /// <summary>
        /// 度分秒经纬度（必须含有'°'）和数字经纬度转换
        /// </summary>
        /// <param name="degrees">度分秒经纬度</param>
        /// <returns>数字经纬度</returns>
        public static double ConvertDegreesToDigital(string degrees)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf('°');// 度的符号对应unicode代码为00B0[1](十六进制),显示为°
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf('′');// 分的符号对应unicode代码为2032[1](十六进制)°
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.IndexOf('″');// 秒的符号对应unicode代码为2032[1](十六进制)
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }

        /// <summary>
        /// 度分秒经纬度（必须含有'/'）和数字经纬度
        /// </summary>
        /// <param name="degrees">度分秒经纬度</param>
        /// <returns>数字经纬度</returns>
        public static double ConvertDegreesToDigital_default(string degrees)
        {
            char ch = '/';
            return ConvertDegreesToDigital(degrees, ch);
        }

        /// <summary>
        /// 度分秒经纬度和数字经纬度转换
        /// </summary>
        /// <param name="degrees">度分秒经纬度</param>
        /// <param name="cflag">分隔符</param>
        /// <returns>数字经纬度</returns>
        public static double ConvertDegreesToDigital(string degrees, char cflag)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf(cflag);
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf(cflag, d + 1);
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.Length;
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }
    }
}