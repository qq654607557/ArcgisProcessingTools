using HelperWindowsControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 数据处理_TFW批量修改工具
{
    public partial class Form1 : Form
    {
        HelperMessRecord Mess;
        HelperMessRecord Eess;
        HelperControlRecord helperControlRecord;
        const string fromname = "数据处理_TFW批量修改工具";
        const string fromlevel = " v1.0 20201120";

        public Form1()
        {
            InitializeComponent();

            HelperMainWindows.SetICO(this);
            Mess = new HelperMessRecord(this, rtbMess);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
