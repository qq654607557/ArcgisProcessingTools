using HelperWindowsControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentProcessing.影像数据处理
{
    public partial class Form影像数据处理_XML生成工具 : Form
    {
        HelperMessRecord Mess;
        HelperMessRecord MessE;
        HelperControlRecord helperControlRecord;
        const string fromname = "影像数据处理_XML生成工具";
        const string fromlevel = " v1.0 20201120";

        public Form影像数据处理_XML生成工具()
        {
            InitializeComponent();

            HelperMainWindows.SetICO(this);
            Mess = new HelperMessRecord(this, rtbMess);
            MessE = new HelperMessRecord(this, this.richTextBox1);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        private void Form影像数据处理_XML生成工具_Load(object sender, EventArgs e)
        {
            helperControlRecord.Load();
        }

        string strinput, strinput2;
        string strxml35, strstart35, strl35, strv35;
        string strxml36, strstart36, strl36, strv36;
        string[] strlist;
        private void button1_Click(object sender, EventArgs e)
        {
            strinput = this.txt导出txt.Text.Trim();
            strinput2 = this.txt导出txt2.Text.Trim();

            strxml35 = this.txt35_1.Text.Trim();
            strstart35 = this.txt35_2.Text.Trim();
            strl35 = this.txt35_3.Text.Trim();
            strv35 = this.txt35_4.Text.Trim();

            strxml36 = this.txt36_1.Text.Trim();
            strstart36 = this.txt36_2.Text.Trim();
            strl36 = this.txt36_3.Text.Trim();
            strv36 = this.txt36_4.Text.Trim();

            helperControlRecord.Save();
            Task.Factory.StartNew(run);
        }

        void run()
        {
            Mess.Clear();
            MessE.Clear();
            Mess.Record("开始");
            string mess = "";
            bool isok = false;
            List<string> outtablenames = new List<string>();
            try
            {
                string[] files = System.IO.Directory.GetFiles(strinput, strinput2);
                for (int i = 0; i < files.Length; i++)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(files[i]);
                    try { 
    
                    bool isok1 = false,isrun=false;
                    isok1 = copyxml(filename, strxml35, strstart35, strl35, strv35);
                    if (isok1) isrun = true;
                     isok1 = copyxml(filename, strxml36, strstart36, strl36, strv36);
                    if (isok1) isrun = true;

                    if (!isrun) MessE.Record("没有找到："+ filename);
                    }catch(Exception  ex1)
                    {
                        MessE.Record("错误：" + filename+" "+ex1.Message);
                    }
                } 

                Mess.Record("完成！");
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
        }

        bool copyxml(string filename,string modelxml,string strstart,string strleng, string strvalue)
        {
            int start = int.Parse(strstart),leng= int.Parse(strleng);
            if (filename.Length < start + leng) return false;
            if (filename.Substring(start, leng) != strvalue) return false;

            string savexml = strinput + "\\"+ filename+".xml";
            System.IO.File.Copy( modelxml, savexml);
            Mess.Record("生成："+ filename + ".xml");

            return true;

        }
    }
}
