using ImageProcessing.DataFusion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ARCGIS小工具
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btn数据融合_图层交集与保留_Click(object sender, EventArgs e)
        {
            Form数据融合_图层交集与保留 from = new Form数据融合_图层交集与保留();
            from.Show();
        }
    }
}
