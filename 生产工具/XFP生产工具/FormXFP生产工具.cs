using HelperClass.LocalFile;
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

namespace XFP生产工具
{
    public partial class FormXFP生产工具 : Form
    {
        HelperMessRecord Mess;
        HelperControlRecord helperControlRecord;
        const string fromname = "生产工具_XFP生产工具";
        const string fromlevel = " v1.0 20201103";

        public FormXFP生产工具()
        {
            InitializeComponent();

            Mess = new HelperMessRecord(this, richTextBox1);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        private void FormXFP生产工具_Load(object sender, EventArgs e)
        {
            helperControlRecord.Load();
        }

        string txtpaht, xfppath, xfpsave, txtgs;
        double xysize, tfk, tfg;

        private void txt保存路径_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = dialog.SelectedPath;
                //MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtXFP文件_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "XFP(*.xfp)|*.xfp|所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = fileDialog.FileName;
                //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTXT文件_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = fileDialog.FileName;
                //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtpaht = this.txtTXT文件.Text.Trim();
                xfppath = this.txtXFP文件.Text.Trim();
                xfpsave = this.txt保存路径.Text.Trim();
                txtgs = this.txt格式.Text.Trim();
                xysize = double.Parse(this.txt像元大小.Text.Trim());
                tfk = double.Parse(this.txt图幅宽.Text.Trim());
                tfg = double.Parse(this.txt图幅高.Text.Trim());

                helperControlRecord.Save();
                Task.Factory.StartNew(run);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }


        void run()
        {
            Mess.Clear();
            Mess.Record("TXT文件:" + txtpaht);
            Mess.Record("XFP文件:" + xfppath);
            Mess.Record("保存路径:" + xfpsave);
            Mess.Record("像元大小:" + xysize.ToString());
            Mess.Record("图幅宽:" + tfk.ToString());
            Mess.Record("图幅高:" + tfg.ToString());
            Mess.Record("=======================");
            Mess.Record("开始");

            HelperTxt txt = new HelperTxt();
            txt.TxtEncoding = new System.Text.UTF8Encoding(false);
            List<string> txtstrings = txt.ReadTxt(txtpaht);
            List<string> txtxfps = txt.ReadTxt(xfppath);
            List<string> savestrings = new List<string>();
            //savestrings.Add("    40000014              6756.57482      1634.81085   0.00  3 { * }");
            string strconfig = "    {0}              {1}      {2}";

            int index = 0;
            for (int i = 0; i < txtxfps.Count; i++)
            {
                string tem = txtxfps[i];
                savestrings.Add(tem);
                switch (tem.Trim())
                {
                    case "$PHOTO":
                        index = 1;
                        break;
                    case "$END":
                        index = 0;
                        break;
                    default:
                        if (index == 1 && tem.Trim().Length > 10 && tem.Trim().Substring(0, 10) == "$PHOTO_NUM")
                        {
                            string[] sp = tem.Trim().Split(':');
                            if (sp.Length != 2) continue;

                            string number = sp[1].Trim();
                            Mess.Record("操作编号：" + number);
                            savestrings.Add("  $PHOTO_POINTS :");

                            List<string> rstrings = readlist(number, txtstrings);
                            for (int j = 0; j < rstrings.Count; j++)
                            {
                                string[] strout;
                                readinfo(rstrings[j], out strout);
                                string str = string.Format(strconfig, strout);
                                savestrings.Add(str);
                            }
                            savestrings.Add("  $END_POINTS");
                        }
                        break;
                }
            }

            if (!System.IO.Directory.Exists(xfpsave)) System.IO.Directory.CreateDirectory(xfpsave);
            string save = xfpsave + "\\成果_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xpf";
            txt.WriteTxt(save, savestrings);
            Mess.Record("保存成果：" + save);

            Mess.Record("结束");
        }

        List<string> readlist(string number, List<string> txtstrings)
        {
            int index = 0;
            List<string> strs = new List<string>();
            for (int i = 0; i < txtstrings.Count; i++)
            {
                string tem = txtstrings[i].Trim();

                if (index == 0 && tem.Length >= number.Length && tem.Substring(0, number.Length) == number) { index = 1; }
                else if (tem == "-99") { index = 0; }
                else if (index == 1)
                {
                    strs.Add(txtstrings[i]);
                }
            }

            return strs;
        }

        bool readinfo(string str, out string[] strout)
        {
            strout = new string[3] { "", "", "" };
            string[] sp = str.Trim().Split(' ');
            int index = 0;
            for (int i = 0; i < sp.Length; i++)
            {
                if (sp[i].Trim().Length > 0)
                {
                    strout[index] = sp[i].Trim();
                    index++;
                }
            }
            try
            {
                // xfp第二列=txt第二列/像元大小(哪里来的？)+图幅宽（哪里来的？）
                strout[1] = (double.Parse(strout[1]) / xysize + tfk).ToString(txtgs);

                // xfp第三列=图幅高（哪里来的？）- txt第三列/像元大小
                strout[2] = (tfg - (double.Parse(strout[2]) / xysize)).ToString(txtgs);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
    }
}
