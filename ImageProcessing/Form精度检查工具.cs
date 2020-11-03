using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using HelperArcGIS.SupportFile;

namespace ImageProcessing
{
    public partial class Form精度检查工具 : Form
    {
        ClassMessRecord MessRecord = null;
        ClassControlRecord ControlRecord = new ClassControlRecord();

        public EventHandler RunEvent;

        public Form精度检查工具()
        {
            InitializeComponent();
            MessRecord = new ClassMessRecord(this, this.rtbRecord);
        }

        private void Form精度检查工具_Load(object sender, EventArgs e)
        {
            ControlRecord.Add(this.txt_标题);
            ControlRecord.Add(this.txt_标准点);
            ControlRecord.Add(this.txt_检查标准);
            ControlRecord.Add(this.txt_检查点);
            ControlRecord.Add(this.txt_景号);
            ControlRecord.Add(this.txt_检查员);
            ControlRecord.Add(this.txt_mdb);
            ControlRecord.Load();
        }

        private void txt_文件夹_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "SHP文件(*.mdb)|*.mdb";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txt_mdb.Text = dlg.FileName;
                txt_jh(dlg.FileName);
            }
        }

        private void txt_mdb_TextChanged(object sender, EventArgs e)
        {
            txt_jh(this.txt_mdb.Text.Trim());
        }

        private void txt_jh(string path)
        {
            if (this.txtc_以MDB文件夹为景号.Checked)
            {
                try
                {
                    string dirname = System.IO.Path.GetDirectoryName(path);
                    dirname = System.IO.Path.GetFileName(dirname);
                    this.txt_景号.Text = dirname;
                }
                catch
                {
                    this.txt_景号.Text = "";
                }
            }
        }

        string mdb_path = "";
        string str_标准点 = "";
        string str_检查点 = "";
        int model_mdb = 0;

        excel精度检查type etype = new excel精度检查type();

        private void but开始_Click(object sender, EventArgs e)
        {
            mdb_path = this.txt_mdb.Text.Trim();
            str_标准点 = this.txt_标准点.Text.Trim();
            str_检查点 = this.txt_检查点.Text.Trim();

            etype.title = this.txt_标题.Text.Trim();
            etype.scene = this.txt_景号.Text.Trim();
            etype.checker = this.txt_检查员.Text.Trim();
            etype.date = this.txt_检查时间.Value.ToString("yyyy-MM-dd");

            etype.reference = "参考点";
            etype.check = "检查点";
            etype.standard = this.txt_检查标准.Value;

            if (string.IsNullOrEmpty(mdb_path)) { MessageBox.Show("请填MDB！"); return; }

            ControlRecord.Save();

            System.Threading.Tasks.Task.Factory.StartNew(run);
        }


        void run()
        {
            MessRecord.Clear();
            MessRecord.Record("=================开始=================");
            try
            {
                MessRecord.Record("-> 检测mdb文件");
                IWorkspace pWorkspace = HelperMDB.Open(mdb_path);
                if (pWorkspace == null) { MessRecord.Record("错误：打开【" + mdb_path + "】文件"); return; }
                IEnumDataset enumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
                IDataset dataset;
                while ((dataset = enumDataset.Next()) != null)
                {
                    IFeatureClass featureClass = dataset as IFeatureClass;
                    string classname = featureClass.AliasName;
                    if (classname == str_标准点)
                    {
                        etype.reFeatureClass = featureClass;
                    }
                    else if (classname == str_检查点)
                    {
                        etype.chFeatureClass = featureClass;
                    }
                }

                string savename = System.IO.Path.GetFileNameWithoutExtension(mdb_path) + "成果精度检查";
                string savepath = System.IO.Path.GetDirectoryName(mdb_path);

                etype.excelname = savename;
                etype.savepath = savepath;

                RunEvent(etype, null);

                string excel_savepath = savepath + "\\" + savename + ".xlsx";
                MessRecord.Record("-> 输出文件：" + excel_savepath);
            }
            catch (Exception ex)
            {
                MessRecord.Record("=================错误=================\n" + ex.Message);
            }
            MessRecord.Record("=================结束=================");
        }

        private void txtr_文件夹循环_CheckedChanged(object sender, EventArgs e)
        {
            if (this.txtr_单个检查.Checked)
            {
                model_mdb = 0;
                this.txt_景号.Enabled = true;
                this.txtc_以MDB文件夹为景号.Enabled = true;
            }
            else if (this.txtr_多个检查.Checked)
            {
                model_mdb = 1;
                this.txt_景号.Enabled = false;
                this.txtc_以MDB文件夹为景号.Enabled = false;
            }
            else
            {
                model_mdb = 2;
                this.txt_景号.Enabled = false;
                this.txtc_以MDB文件夹为景号.Enabled = false;
            }
        }
    }
}
