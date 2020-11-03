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
    public partial class Form读写四点坐标_传感器填写 : Form
    {
        string jh = "";
        string wxgdh="";
        string cgqlx="";
        bool isok = false;

        public string WXGDH{get{return wxgdh;}}
        public string CGQLX{get{return cgqlx;}}
        public string JH{set{jh=value;}}
        public bool IsOK { get { return isok; } }

        public Form读写四点坐标_传感器填写()
        {
            InitializeComponent();
        }

        private void Form读写四点坐标_传感器填写_Load(object sender, EventArgs e)
        {
            this.txt_JH.Text = jh;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           cgqlx= this.txt_CGQLX.Text.Trim();
           wxgdh = this.txt_WXGDH.Text.Trim();
           isok = true;
           this.Close();
        }
    }
}
