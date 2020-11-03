using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using System.IO;

namespace ImageProcessing
{
    public partial class Form批量切片服务 : Form
    {
        ClassMessRecord MessRecord = null;
        ClassControlRecord ControlRecord = new ClassControlRecord();

        public Form批量切片服务()
        {
            InitializeComponent();
            MessRecord = new ClassMessRecord(this, this.rtbRecord);
        }

        private void Form批量切片服务_Load(object sender, EventArgs e)
        {
            ControlRecord.Add(this.panel1.Controls);
            ControlRecord.Load();
        }

        string str_InputService = "", str_SaveFile, str_Shps = "";//, str_TargetCachePath = "", str_AreaOfInterest = "";

        private void btn开始运行_Click(object sender, EventArgs e)
        {
            str_InputService = this.txt_服务路径.Text.Trim();
            str_SaveFile = this.txt_保存文件.Text.Trim();
            str_Shps = this.txt_shp文件夹.Text.Trim();

            if (string.IsNullOrEmpty(str_InputService)) { MessageBox.Show("请填写“服务路径”！"); return; }
            if (string.IsNullOrEmpty(str_SaveFile)) { MessageBox.Show("请填写“保存文件夹”！"); return; }
            if (string.IsNullOrEmpty(str_Shps)) { MessageBox.Show("请填写“shp文件夹”！"); return; }

            ControlRecord.Save();

            System.Threading.Tasks.Task.Factory.StartNew(run);
        }

        void run()
        {
            MessRecord.Clear();
            MessRecord.Record("=================开始=================");
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(str_Shps);
                System.IO.FileInfo[] files = directoryInfo.GetFiles("*.shp");
                MessRecord.Record("读取到[" + files.Length.ToString() + "]个文件");
                for (int i = 0; i < files.Length; i++)
                {
                    string mess = "";
                    string target_cache_path = str_SaveFile + "\\" + files[i].Name.ToUpper().Replace(".SHP", "");
                    System.IO.Directory.CreateDirectory(target_cache_path);

                    MessRecord.Record("开始[" + i.ToString() + "]" + files[i].Name);
                    if (HelperArcGIS.PGTool.GPServerTools.ExportMapServerCache(ref mess, str_InputService, target_cache_path, files[i].FullName))
                        MessRecord.Record("结束[" + i.ToString() + "]" + files[i].Name + " " + mess);
                    else MessRecord.Record("错误[" + i.ToString() + "]" + files[i].Name + " " + mess);
                }
            }
            catch (Exception ex)
            {
                MessRecord.Record("=================错误=================\n" + ex.Message);
            }

        TitleEnd:
            MessRecord.Record("=================结束=================");
        }
    }
}
