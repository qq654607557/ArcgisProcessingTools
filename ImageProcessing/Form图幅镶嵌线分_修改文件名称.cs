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
using System.IO;

namespace ImageProcessing
{
    public partial class Form图幅镶嵌线分_修改文件名称 : Form
    {
        string shp_path = "";
        string shp_col_name = "";

        string directory_path = "";
        string xml_path = "";

        public Form图幅镶嵌线分_修改文件名称()
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

        private void btn开始生成_Click(object sender, EventArgs e)
        {
            this.shp_path = this.txt_shp.Text;
            this.shp_col_name = this.txt_shp_col.Text;

            this.directory_path = this.txt_成果输出文件夹.Text;
            this.xml_path = this.txt_xml.Text;

            if (string.IsNullOrEmpty(shp_path.Trim())) { MessageBox.Show("shp" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(shp_col_name.Trim())) { MessageBox.Show("列名" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(directory_path.Trim())) { MessageBox.Show("文件夹" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(xml_path.Trim())) { MessageBox.Show("xml" + "没有输入！"); return; }

            save_recording();

            Task.Factory.StartNew(run);
        }

        #region 判断

        #endregion


        private void run()
        {
            showMess_Record("=============开始==================");
            try
            {
                Dictionary<string, string> list_name = new Dictionary<string, string>();
                List<string> list_file_name = new List<string>();


                showMess_Record("-> 读取shp文件");

                string[] files_shp = System.IO.Directory.GetFiles(shp_path, "*.shp");
                for (int i = 0; i < files_shp.Length; i++)
                {
                    string allfilename = read_shp_name(files_shp[i]);
                    string allfilename10 = (allfilename.Length > 10) ? allfilename.Substring(0, 10) : allfilename;
                    list_name.Add(allfilename10, allfilename);
                }

                showMess_Record("-> 读取文件夹文件");
                string[] files_tfw = System.IO.Directory.GetFiles(directory_path, "*.tfw");

                showMess_Record("-> 去掉tfw小数点后3位,拷贝xml");
                for (int i = 0; i < files_tfw.Length; i++)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(files_tfw[i]);
                    list_file_name.Add(filename);
                    string savecopy = directory_path + "\\" + filename + ".xml";
                    System.IO.File.Copy(xml_path, savecopy, true);
                    //tfw小数点后3位
                    HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
                    List<string> txts = txt.ReadTxt(files_tfw[i]);
                    for (int n = 0; n < txts.Count; n++)
                    {
                        int index = txts[n].IndexOf('.') + 4;
                        if (txts[n].Length > index) txts[n] = txts[n].Substring(0, index);
                    }
                    txt.WriteTxt(files_tfw[i], txts);
                }

                showMess_Record("-> 修改文件名");

                DataTable dt = new DataTable("日志");
                dt.Columns.Add("ID");
                dt.Columns.Add("success");
                dt.Columns.Add("old_name");
                dt.Columns.Add("new_name");
                dt.Columns.Add("directory");

                string[] allfiles = System.IO.Directory.GetFiles(directory_path);
                int jsq = 0;
                for (int i = 0; i < allfiles.Length; i++)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(allfiles[i]);
                    string extension = System.IO.Path.GetExtension(allfiles[i]);
                    if (list_name.ContainsKey(filename))
                    {
                        dt.Rows.Add(i.ToString(), "T", filename + extension, list_name[filename] + extension, directory_path);
                        System.IO.File.Move(directory_path + "\\" + filename + extension, directory_path + "\\" + list_name[filename] + extension);
                        jsq++;
                    }
                    else
                    {
                        dt.Rows.Add(i.ToString(), "F", filename + extension,"", directory_path);
                    }
                }

                showMess_Record("-> 文件修改完成总计：" + jsq + "条");
                string record_path = System.Windows.Forms.Application.StartupPath + @"\data\R_"+DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")+".xml";
                dt.WriteXml(record_path);
                showMess_Record("-> 输出日志：" + record_path);
            }
            catch (Exception ex)
            {
                showMess_Record("==================================\n错误：" + ex.Message);
            }
            showMess_Record("=============结束==================");
        }

        string read_shp_name(string shp_path)
        {
            // 基本数据
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shp_path);

            int idCol_NAME = featureClass.Fields.FindField(shp_col_name);

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

            string valueCol_FBL = "";// 2位

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

            idc_rec_control.Add("txt_shp", this.txt_shp);
            idc_rec_control.Add("txt_shp_col", this.txt_shp_col);
            idc_rec_control.Add("txt_成果输出文件夹", this.txt_成果输出文件夹);
            idc_rec_control.Add("txt_xml", this.txt_xml);

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

        private void txt_shp_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txt_shp.Text = fbd.SelectedPath;
            }
        }

        private void txt_xml_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "XML文件(*.xml)|*.xml";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                shp_path = dlg.FileName;
                this.txt_xml.Text = shp_path;
            }
        }

        private void txt_成果输出文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txt_成果输出文件夹.Text = fbd.SelectedPath;
            }
        }

        private void Form修改数据001_Load(object sender, EventArgs e)
        {
            load_recording();
            //this.txt_xml.Text = System.Windows.Forms.Application.StartupPath + @"\data\modelxml.xml";
        }
    }
}
