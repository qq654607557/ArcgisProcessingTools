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
    public partial class Form拷贝文件工具 : Form
    {
        public Form拷贝文件工具()
        {
            InitializeComponent();
        }

        private void btn开始处理_Click(object sender, EventArgs e)
        {
            string[] strs = this.richTextBox1.Lines;
            string fin = this.txt_数据目录.Text.Trim();
            string fout = this.txt_输出目录.Text.Trim();

            System.Threading.Tasks.Task.Factory.StartNew(() => {
                showMess_Record("======start=======");
                for (int i = 0; i < strs.Length; i++)
                {
                    showMess_Record("开始检测名称：" + strs[i]);
                    string filename = fout + "//" + strs[i];
                    System.IO.File.Create(filename);
                   
                    List<FileInfo> listFileInfo = new List<FileInfo>();
                    Director(fin, ref listFileInfo, strs[i]);
                    for (int n = 0; n < listFileInfo.Count; n++)
                    {
                        System.IO.File.Copy(listFileInfo[n].FullName, filename + "//" + listFileInfo[n].Name);
                        showMess_Record("拷贝" + listFileInfo[n].FullName + "到" + filename + "//" + listFileInfo[n].Name);
                    }
                }
                showMess_Record("======end=======");
            });
        }

        void showMess_Record(string mess)
        {
            this.Invoke((EventHandler)delegate
            {
                //if (rtbRecord.TextLength > 50000) rtbRecord.Clear();

                this.rtbRecord.AppendText(mess + "\n");
                rtbRecord.SelectionStart = rtbRecord.TextLength;
                rtbRecord.ScrollToCaret();
            });

        }

        public void Director(string dir, ref List<FileInfo> listFileInfo,string filename)
        {
            //showMess_Record("循环中：" + dir);
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                  if(f.Name==filename)  listFileInfo.Add(f);

            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                Director(dd.FullName, ref listFileInfo, filename);
            }
        }
       
    }
}
