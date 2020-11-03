using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HelperArcGIS.SupportFile;
using ESRI.ArcGIS.Geodatabase;
using HelperArcGIS.PGTool;

namespace ImageProcessing
{
    public partial class 数据合并_GDB : Form
    {
        ClassMessRecord MessRecord = null;
        ClassControlRecord ControlRecord = new ClassControlRecord();

        public 数据合并_GDB()
        {
            InitializeComponent();
            MessRecord = new ClassMessRecord(this, this.rtbRecord);
        }


        private void 数据合并_GDB_Load(object sender, EventArgs e)
        {
            ControlRecord.Add(this.txt_GDB文件);
            ControlRecord.Add(this.txt_savegdb);
            ControlRecord.Load();
        }

        private void txt_GDB文件_DoubleClick(object sender, EventArgs e)
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Multiselect = false;
            //dlg.Filter = "gdb文件(*.gdb)|*.gdb";
            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string txt = "";
            //    string[] files = dlg.FileNames;
            //    for (int i = 0; i < files.Length; i++) txt += files[i] + ";";
            //    this.txt_GDB文件.Text = txt;
            //}

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_GDB文件.Text = fbd.SelectedPath;
            }
        }

        string[] open_gdb_path;
        string shp_folder_gdb_path = "";
        string save_gdb_path = "";

        private void btn开始合并_Click(object sender, EventArgs e)
        {
            shp_folder_gdb_path = this.txt_GDB文件.Text.Trim();
            save_gdb_path = this.txt_savegdb.Text.Trim();

            if (string.IsNullOrEmpty(shp_folder_gdb_path)) { MessageBox.Show("请输入GDB文件"); return; }
            if (string.IsNullOrEmpty(save_gdb_path)) { MessageBox.Show("请输入保存GDB文件"); return; }

            //open_gdb_path = shp_folder_gdb_path.Split(';');

            ControlRecord.Save();

            System.Threading.Tasks.Task.Factory.StartNew(run);
        }

        void run()
        {
            MessRecord.Clear();
            MessRecord.Record("=================开始=================");
            try
            {
                MessRecord.Record("-> 检测GDB文件夹");
                open_gdb_path = System.IO.Directory.GetDirectories(shp_folder_gdb_path, "*.gdb", SearchOption.TopDirectoryOnly);

                List<string> gdb_layers = new List<string>();//表名
                for (int i = 0; i < open_gdb_path.Length; i++)
                {
                    MessRecord.Record("[" + (i + 1).ToString("D3") + "] " + open_gdb_path[i]);

                    if (gdb_layers.Count == 0)
                    {
                        MessRecord.Record("-> 读取所有表");
                        IWorkspace iWorkspace = HelperGDB.OpenGDB(open_gdb_path[i]);
                        gdb_layers = GetAlllayers(iWorkspace);
                        MessRecord.Record("-> 读取到表[" + gdb_layers.Count.ToString()+ "]张");
                    }


                    break;
                }
                MessRecord.Record("-> 开始合并");
                string mess = "";
                bool isok;
                for (int i = 0; i < gdb_layers.Count; i++)
                {
                    string inPuts = "";
                    foreach (string path in open_gdb_path)
                    {
                        inPuts += path + "/" + gdb_layers[i]+";";
                    }
                    string outPut = save_gdb_path + "/" + gdb_layers[i];

                    isok = GPDataManagementTools.Merge(ref mess, inPuts, outPut);
                    if (isok)
                    {
                        MessRecord.Record("[" + (i+1).ToString("D3") + "] 合并完成" + gdb_layers[i]);
                    }
                    else
                    {
                        MessRecord.Record("[" + (i + 1).ToString("D3") + "] 合并失败" + gdb_layers[i] + " \r\n 错误：" + mess);
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                MessRecord.Record("=================错误=================\n" + ex.Message);
            }
            MessRecord.Record("=================结束=================");
        }

        List<string> GetAlllayers(IWorkspace pworkspace)
        {
            List<string> gdb_layers = new List<string>();
            IFeatureWorkspace pfeatwk = pworkspace as IFeatureWorkspace;

            IEnumDatasetName pfeatName = pworkspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            if (pfeatName == null) return gdb_layers;
            pfeatName.Reset();
            IDatasetName pdatasetname = pfeatName.Next();
            while (pdatasetname != null)
            {
                string sss = pdatasetname.Name;
                gdb_layers.Add(sss);
                pdatasetname = pfeatName.Next();
            }
            return gdb_layers;
        }


    }
}
