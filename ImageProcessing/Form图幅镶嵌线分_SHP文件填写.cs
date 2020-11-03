using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ImageProcessing
{
    public partial class Form图幅镶嵌线分_SHP文件填写 : Form
    {
        public Form图幅镶嵌线分_SHP文件填写()
        {
            InitializeComponent();
        }


        void showMess_Record(string mess)
        {
            this.Invoke((EventHandler)delegate
            {
                this.rtbRecord.AppendText(mess + "\n");
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
            idc_rec_control.Add("txt_rcol_FBL", this.txt_rcol_FBL);
            idc_rec_control.Add("txt_rcol_SJY", this.txt_rcol_SJY);
            idc_rec_control.Add("txt_rcol_SX", this.txt_rcol_SX);
            idc_rec_control.Add("txt_rcol_TFH", this.txt_rcol_TFH);
            idc_rec_control.Add("txt_wcol_TFH", this.txt_wcol_TFH);
            idc_rec_control.Add("txt_wcol_YXMC", this.txt_wcol_YXMC);

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
                            else
                            {
                                kvp.Value.Text = tem[1];
                            }
                        }
                    }
                }
            }
        }

        private void SHP文件填写001_Load(object sender, EventArgs e)
        {
            load_recording();
        }


        string shp_path;

        string wcol_TFH;
        string wcol_YXMC;

        string rcol_FBL;
        string rcol_SJY;
        string rcol_SX;
        string rcol_TFH;


        private void btn开始生成_Click(object sender, EventArgs e)
        {
            shp_path = this.txt_shp文件夹.Text;

            wcol_TFH = this.txt_wcol_TFH.Text;
            wcol_YXMC = this.txt_wcol_YXMC.Text;

            rcol_FBL = this.txt_rcol_FBL.Text;
            rcol_SJY = this.txt_rcol_SJY.Text;
            rcol_SX = this.txt_rcol_SX.Text;
            rcol_TFH = this.txt_rcol_TFH.Text;

            if (string.IsNullOrEmpty(shp_path.Trim())) { MessageBox.Show("shp" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(wcol_TFH.Trim())) { MessageBox.Show("TFH" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(wcol_YXMC.Trim())) { MessageBox.Show("YXMC" + "没有输入！"); return; }

            if (string.IsNullOrEmpty(rcol_FBL.Trim())) { MessageBox.Show("FBL" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(rcol_SJY.Trim())) { MessageBox.Show("SJY" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(rcol_SX.Trim())) { MessageBox.Show("SX" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(rcol_TFH.Trim())) { MessageBox.Show("TFH" + "没有输入！"); return; }

            save_recording();

            Task.Factory.StartNew(run);
        }


        private void run()
        {
            showMess_clear();
            showMess_Record("=============开始==================");
            try
            {
                showMess_Record("-> 循环读取shp文件");
                string[] files_shp = System.IO.Directory.GetFiles(shp_path, "*.shp");

                for (int i = 0; i < files_shp.Length; i++)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(files_shp[i]);
                    showMess_Record("-> 读写shp文件：" + filename);

                    ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
                    IFeatureClass featureClass = sr.ReadInfo(files_shp[i]);

                    int idCol_TFH = featureClass.Fields.FindField(wcol_TFH);
                    int idCol_YXMC = featureClass.Fields.FindField(wcol_YXMC);

                    int idCol_FBL = featureClass.Fields.FindField(rcol_FBL);
                    int idCol_SJY = featureClass.Fields.FindField(rcol_SJY);
                    int idCol_SX = featureClass.Fields.FindField(rcol_SX);   // 判断最新时间

                    int idCol_PAREA = featureClass.Fields.FindField("PAREA");    // 判断最大面积

                    //string valueCol_TFH = "";

                    string valueCol_FBL = "";

                    // 名称
                    string valueCol_TFH = filename.Replace("xq", "");

                    IQueryFilter queryFilterSave = new QueryFilterClass();

                    IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);
                    IFeature featureUpdata;
                    // 生成YXMC

                    Dictionary<string, YXMC_List> ear = new Dictionary<string, YXMC_List>();
                    while ((featureUpdata = featureCursor.NextFeature()) != null)
                    {
                        // 计算面积
                      IGeometry ig = featureUpdata.Shape;
                      IArea darea=  featureUpdata.Shape as IArea;

                        string valueCol_SJY = featureUpdata.get_Value(idCol_SJY).ToString();
                        string valueCol_SX = featureUpdata.get_Value(idCol_SX).ToString();
                        double mj = (double)featureUpdata.get_Value(idCol_PAREA);

                        if (!string.IsNullOrEmpty(valueCol_SX) && valueCol_SX.Length > 6) valueCol_SX = valueCol_SX.Substring(0, 6);

                        if (ear.ContainsKey(valueCol_SJY))
                        {
                            ear[valueCol_SJY].sumAREA = ear[valueCol_SJY].sumAREA + mj;
                            ear[valueCol_SJY].YXMClist.Add(new YXMC_Info() { AREA = mj, TimeStr = valueCol_SX });
                        }
                        else
                        {
                            YXMC_List yl = new YXMC_List() { sumAREA = mj, strName = valueCol_SJY };
                            yl.YXMClist.Add(new YXMC_Info() { AREA = mj, TimeStr = valueCol_SX });
                            ear.Add(valueCol_SJY, yl);
                        }

                        valueCol_FBL = featureUpdata.get_Value(idCol_FBL).ToString();
                        if (valueCol_FBL == "0.5") valueCol_FBL = "05";
                        else if (valueCol_FBL == "2") valueCol_FBL = "20";

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

                    string yxmc = valueCol_TFH + kvp_tem.strName + valueCol_FBL + yi_tem.TimeStr;

                    featureCursor = featureClass.Search(queryFilterSave, false);
                    while ((featureUpdata = featureCursor.NextFeature()) != null)
                    {
                        // 写
                        featureUpdata.set_Value(idCol_TFH, valueCol_TFH);
                        featureUpdata.set_Value(idCol_YXMC, yxmc);

                        // 写
                        // string valueCol_SJY = featureUpdata.get_Value(idCol_SJY).ToString();
                        //string valueCol_SX = featureUpdata.get_Value(idCol_SX).ToString();
                        //if (!string.IsNullOrEmpty(valueCol_SX) && valueCol_SX.Length > 6) valueCol_SX = valueCol_SX.Substring(0, 6);
                        //string valueCol_YXMC = valueCol_TFH + valueCol_SJY + valueCol_FBL + valueCol_SX;
                        //featureUpdata.set_Value(idCol_YXMC, valueCol_YXMC);

                        featureUpdata.Store();
                    }

                    featureUpdata = null;
                    featureCursor = null;
                    queryFilterSave = null;
                    featureClass = null;

                    showMess_Record("-> 完成shp文件：" + filename);
                }

                showMess_Record("-> 文件修改完成总计：条");
            }
            catch (Exception ex)
            {
                showMess_Record("==================================\n错误：" + ex.Message);
            }
            showMess_Record("=============结束==================");
        }

        private void txt_shp_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_shp文件夹.Text = fbd.SelectedPath;
            }
        }
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
    }
}
