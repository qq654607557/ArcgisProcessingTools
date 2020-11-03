using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl.MyControls
{
    public partial class UC_txt : UserControl
    {
        public string Name
        {
            get;
            set;
        }

        public string Text
        {
            get { return this.textBox1.Text.Trim(); }
            set { this.textBox1.Text = value; }
        }

        public string Title
        {
            set { this.label1.Text = value+"："; }
        }

        public UC_txt()
        {
            InitializeComponent();
            //this.Load += new EventHandler(uc_txt_Load);
        }
    }
}
