using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArcGISTool;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ImageProcessing
{
    public partial class 依据图幅号拷贝工具 : Form
    {
        ShapefileRead shapefileRead = new ShapefileRead();

        string shp_path = "";
        string shp_col_name = "name";
        /// <summary>
        /// 外接shp文件
        /// </summary>
        string external_shp_path = "";

        string out_path = "";
        string in_path = "";

        string all_2_5_path = "";
        string ex_2_5 = "";
        string all_5_path = "";
        string ex_5 = "";
        string op_name = "";
        string op_col_opname = "";

        List<string> lsit_name = new List<string>();

        /// <summary>
        /// 记录
        /// </summary>
        Dictionary<string, string> idc_recording = new Dictionary<string, string>();
        Dictionary<string, Control> idc_rec_control = new Dictionary<string, Control>();


        public 依据图幅号拷贝工具()
        {
            InitializeComponent();
        }

        private void txt_SHP文件_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "shp文件(*.shp)|*.shp|所有文件(*.*)|*.*";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                shp_path = dlg.FileName;
                this.txt_SHP文件.Text = shp_path;
            }
        }

        private void txt_数据目录_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                in_path = fbd.SelectedPath;
                this.txt_数据目录.Text = in_path;
            }

        }

        private void txt_输出目录_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                out_path = fbd.SelectedPath;
                this.txt_输出目录.Text = out_path;
            }
        }

        private void btn开始处理_Click(object sender, EventArgs e)
        {
            shp_path = this.txt_SHP文件.Text;
            shp_col_name = this.txt_SHP文件名称字段.Text;
            external_shp_path = this.txt_外接SHP.Text;

            out_path = this.txt_输出目录.Text;
            in_path = this.txt_数据目录.Text;

            all_2_5_path = this.txt_2_5W总图幅.Text;
            ex_2_5 = this.txt_文件选择_2_5W总图幅.Text;

            all_5_path = this.txt_5W总图幅.Text;
            ex_5 = this.txt_文件选择_5W总图幅.Text;

            op_name = this.txt_分配人员.Text;
            op_col_opname = this.txt_col_opname.Text;

            save_recording();

            if (string.IsNullOrEmpty(shp_path.Trim())) { MessageBox.Show("SHP文件" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(shp_col_name.Trim())) { MessageBox.Show("SHP文件名称字段" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(external_shp_path.Trim())) { MessageBox.Show("外接SHP" + "没有输入！"); return; }

            if (string.IsNullOrEmpty(out_path.Trim())) { MessageBox.Show("输出目录" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(in_path.Trim())) { MessageBox.Show("数据目录" + "没有输入！"); return; }

            if (string.IsNullOrEmpty(all_2_5_path.Trim())) { MessageBox.Show("2.5W总图幅" + "没有输入！"); return; }
            //if (string.IsNullOrEmpty(ex_2_5.Trim())) { MessageBox.Show("SHP文件名称字段" + "没有输入！"); return; }

            if (string.IsNullOrEmpty(all_5_path.Trim())) { MessageBox.Show("5W总图幅" + "没有输入！"); return; }
            //if (string.IsNullOrEmpty(ex_5.Trim())) { MessageBox.Show("数据目录" + "没有输入！"); return; }

            if (string.IsNullOrEmpty(op_name.Trim())) { MessageBox.Show("分配人员" + "没有输入！"); return; }

            Task.Factory.StartNew(run);
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

        private void run()
        {
            showMess_Record("=============开始==================");
            try
            {
                showMess_Record("-> 读取shp");
                lsit_name.Clear();
                readshp(shp_path);

                showMess_Record("-> 拷贝文件");
                List<string> strno = new List<string>();
                List<string> delno = new List<string>();
                for (int i = 0; i < lsit_name.Count; i++)
                {
                    string mess = copyfile(in_path + "\\" + lsit_name[i]);
                    if (!string.IsNullOrEmpty(mess))
                    {
                        showMess_Record("[" + i.ToString("D3") + "][" + lsit_name[i] + "]输出：" + mess);
                        strno.Add(lsit_name[i]);
                    }

                    //delno.Add(lsit_name[i]);
                    //delfile(lsit_name[i]);
                    //showMess_Record("[" + i.ToString("D3") + "][" + lsit_name[i] + "]删除：" + mess);
                }
                if (strno.Count > 0)
                {
                    int len = 4;
                    if (strno[0].Substring(0, len) == ex_2_5)
                    {
                        showMess_Record("-> 写入2.5文件:" + all_2_5_path);
                        writeSHP(strno, all_2_5_path);
                    }
                    else if (strno[0].Substring(0, len) == ex_5)
                    {
                        showMess_Record("-> 写入5W文件:" + all_5_path);
                        writeSHP(strno, all_5_path);
                    }

                }


            }
            catch (Exception ex)
            {
                showMess_Record("==================================\n错误：" + ex.Message);
            }
            showMess_Record("=============结束==================");
        }

        private void readshp(string shppath)
        {
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shppath);

            int idCol_NAME = featureClass.Fields.FindField(shp_col_name);
            //if (idCol_NAME < 0) { return false; }

            IQueryFilter queryFilterSave = new QueryFilterClass();
            queryFilterSave.WhereClause = "";
            IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

            IFeature iFeature = null;
            while ((iFeature = featureCursor.NextFeature()) != null)
            {
                lsit_name.Add(iFeature.get_Value(idCol_NAME).ToString().Trim());
            }
        }

        private string copyfile(string filePath)
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

        private string delfile(string filePath)
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

                System.IO.File.Delete(fileold);
                isrun = true;
            }

            return isrun ? (out_path + "\\" + filename + ".shp") : "";
        }

        private bool writeSHP(List<string> strno, string shppath)
        {
            // 基本数据
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shppath);

            int idCol_NAME = featureClass.Fields.FindField(shp_col_name);
            int idCol_opname = featureClass.Fields.FindField(op_col_opname);

            if (idCol_NAME < 0) { return false; }
            if (idCol_opname < 0) { ArcGISTool.ShapefileRead.AddField(featureClass, op_col_opname, op_col_opname, esriFieldType.esriFieldTypeString); idCol_opname = featureClass.Fields.FindField(op_col_opname); }

            for (int i = 0; i < strno.Count; i++)
            {
                IQueryFilter queryFilterSave = new QueryFilterClass();
                queryFilterSave.WhereClause = " " + shp_col_name + " like '%" + strno[i] + "%' ";
                IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

                IFeature featureUpdata = featureCursor.NextFeature();
                if (featureUpdata != null)
                {
                    featureUpdata.set_Value(idCol_opname, op_name);

                    featureUpdata.Store();
                }
            }

            featureClass = null;
            return true;
        }

        private void 依据图幅号拷贝工具_Load(object sender, EventArgs e)
        {
            loadConfig();
            load_recording();
        }

        private void loadConfig()
        {
            string opname_path = System.Windows.Forms.Application.StartupPath + @"\OPName.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            List<string> opnamelist = txt.ReadTxt(opname_path);

            this.txt_分配人员.Items.Add("");
            for (int i = 0; i < opnamelist.Count; i++)
            {
                this.txt_分配人员.Items.Add(opnamelist[i]);
            }
        }

        private void save_recording()
        {
            List<string> opnamelist = new List<string>();
            foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
            {
                opnamelist.Add(kvp.Key + "," + kvp.Value.Text);
            }

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\Recording.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            txt.WriteTxt(opname_path, opnamelist);
        }

        private void load_recording()
        {
            idc_rec_control.Clear();

            idc_rec_control.Add("SHP文件", this.txt_SHP文件);
            idc_rec_control.Add("SHP文件名称字段", this.txt_SHP文件名称字段);
            idc_rec_control.Add("外接SHP", this.txt_外接SHP);

            idc_rec_control.Add("输出目录文件", this.txt_输出目录);
            idc_rec_control.Add("数据目录", this.txt_数据目录);

            idc_rec_control.Add("2_5W总图幅", this.txt_2_5W总图幅);
            idc_rec_control.Add("文件选择_2_5W总图幅", this.txt_文件选择_2_5W总图幅);

            idc_rec_control.Add("5W总图幅", this.txt_5W总图幅);
            idc_rec_control.Add("文件选择_5W总图幅", this.txt_文件选择_5W总图幅);

            idc_rec_control.Add("分配人员", this.txt_分配人员);
            idc_rec_control.Add("人员字段", this.txt_col_opname);

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
                        if (kvp.Key == tem[0]) kvp.Value.Text = tem[1];
                    }
                }
            }
        }
    }
}
