using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form显示表格 : Form
    {
        DataTable dt;
        public DataTable Get_DataTable { set { dt = value; } }

        public Form显示表格()
        {
            InitializeComponent();
        }

        private void Form显示表格_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = dt;
        }
    }
}
