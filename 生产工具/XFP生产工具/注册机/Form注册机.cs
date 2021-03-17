using HelperClass.Hardware;
using HelperWindowsControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 注册机
{
    public partial class Form1 : Form
    {
        const string fromname = "小工具注册机";
        const string fromlevel = " v1.0";

        RegisterClass registerClass = new RegisterClass();
        string currentCode = "";

        public Form1()
        {
            InitializeComponent();

            HelperMainWindows.SetICO(this);
            this.Text = fromname + fromlevel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text != "tuzhi2020s") { MessageBox.Show("您的   不正确"); return; }
            try
            {
                string code = this.textBox1.Text.Trim();
                if (code.Length != 35) { MessageBox.Show("您的注册码不正确！正确格式:xxxxxxxx(8位)-xxxxxxxx-xxxxxxxx-xxxxxxxx"); this.textBox2.Text = ""; return; };
                this.textBox2.Text = registerClass.GetCode(code);
            }
            catch (Exception ex)
            { MessageBox.Show("您的注册码不正确！正确格式:xxxxxxxx(8位)-xxxxxxxx-xxxxxxxx-xxxxxxxx"); this.textBox2.Text = ""; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //currentCode = registerClass.CreateCode();
            //this.textBox1.Text = currentCode;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //bool isok = registerClass.RegistIt(registerClass.GetCode(registerClass.CreateCode()), this.textBox2.Text);
            //MessageBox.Show("注册：" + (isok ? "成功!" : "失败!!!"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //bool isok = registerClass.BoolRegist(registerClass.GetCode(registerClass.CreateCode()));
            //MessageBox.Show("验证：" + (isok ? "成功!" : "失败!!!"));
        }
    }
}
