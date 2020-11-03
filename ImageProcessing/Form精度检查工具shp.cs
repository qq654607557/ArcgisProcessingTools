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
using ArcGISTool;

namespace ImageProcessing
{
    public partial class Form精度检查工具shp : Form
    {
        ClassMessRecord MessRecord = null;
        ClassControlRecord ControlRecord = new ClassControlRecord();

        public EventHandler RunEvent;

        public Form精度检查工具shp()
        {
            InitializeComponent();
            MessRecord = new ClassMessRecord(this, this.rtbRecord);
        }

        private void Form精度检查工具_Load(object sender, EventArgs e)
        {
            ControlRecord.Add(this.txt_标题);
            ControlRecord.Add(this.txt_shp参考点);
            ControlRecord.Add(this.txt_shp检查点);
            ControlRecord.Add(this.txt_检查标准);
            ControlRecord.Add(this.txt_景号);
            ControlRecord.Add(this.txt_检查员);
            ControlRecord.Load();
        }

        private void txt_shp参考点_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "SHP文件(*.shp)|*.shp";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txt_shp参考点.Text = dlg.FileName;
                txt_jh(dlg.FileName);
            }
        }

        private void txt_shp检查点_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "SHP文件(*.shp)|*.shp";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txt_shp检查点.Text = dlg.FileName;
                txt_jh(dlg.FileName);
            }
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

        string str_参考点 = "";
        string str_检查点 = "";

        excel精度检查type etype = new excel精度检查type();

        private void but开始_Click(object sender, EventArgs e)
        {
            str_参考点 = this.txt_shp参考点.Text.Trim();
            str_检查点 = this.txt_shp检查点.Text.Trim();

            etype.title = this.txt_标题.Text.Trim();
            etype.scene = this.txt_景号.Text.Trim();
            etype.checker = this.txt_检查员.Text.Trim();
            etype.date = this.txt_检查时间.Value.ToString("yyyy-MM-dd");

            etype.reference = "参考点";
            etype.check = "检查点";
            etype.standard = this.txt_检查标准.Value;

            ControlRecord.Save();

            System.Threading.Tasks.Task.Factory.StartNew(run);
        }


        void run()
        {
            MessRecord.Clear();
            MessRecord.Record("=================开始=================");
            try
            {
                //MessRecord.Record("-> 打开点");

                ShapefileRead sr = new ShapefileRead();
                etype.reFeatureClass = sr.ReadInfo(str_参考点);
                etype.chFeatureClass = sr.ReadInfo(str_检查点);

                if (etype.reFeatureClass == null) { MessRecord.Record("-> 打开参考点shp错误：" + str_参考点); return; }
                if (etype.chFeatureClass == null) { MessRecord.Record("-> 打开检查点shp错误：" + str_检查点); return; }

                string savename = System.IO.Path.GetFileNameWithoutExtension(str_参考点).Replace("参考点", "") + "成果精度检查";
                string savepath = System.IO.Path.GetDirectoryName(str_参考点);

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

    }
}
