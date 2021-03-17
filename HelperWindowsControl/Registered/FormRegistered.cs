using HelperClass.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl.Registered
{
    public partial class FormRegistered : Form
    {
        RegisterClass registerClass = new RegisterClass();
        string currentCode = "";
        string pid = "";
        public FormRegistered(string softname,string pid)
        {
            InitializeComponent();
            this.pid = pid;
            this.Text = softname+ " 注册";
        }

        private void FormRegistered_Load(object sender, EventArgs e)
        {
            currentCode = registerClass.CreateCode(pid);
            this.textBox1.Text = currentCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isok = registerClass.RegistIt(registerClass.GetCode(registerClass.CreateCode(pid)), this.textBox2.Text.Trim());
            MessageBox.Show("注册：" + (isok ? "成功!" : "失败!!!"));
          if(isok)  this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
