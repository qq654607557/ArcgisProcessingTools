using HelperWindowsControl.MyControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl
{
    public class HelperControlRecord
    {
        public string formname = "";
        public HelperControlRecord(string FormName)
        { this.formname = FormName; }

        public HelperControlRecord(System.Windows.Forms.Form from)
        { this.formname = from.Text; }

        Dictionary<string, Control> idc_rec_control = new Dictionary<string, Control>();

        public void Add(string cname, Control con)
        {
            idc_rec_control.Add(cname, con);
        }

        public void Add(Control con)
        {
            idc_rec_control.Add(con.Name, con);
        }

        public void Add(Control.ControlCollection Controls)
        {
            foreach (Control con in Controls)
            {
                if (con is TextBox) idc_rec_control.Add(con.Name, con);
                else if (con is CheckBox) idc_rec_control.Add(con.Name, con);
                else if (con is NumericUpDown) idc_rec_control.Add(con.Name, con);
                else if (con is RadioButton) idc_rec_control.Add(con.Name, con);
                else if (con is UC_txt) idc_rec_control.Add((con as UC_txt).Name, con);
            }
        }

        public void Add_Label(Control.ControlCollection Controls)
        {
            foreach (Control con in Controls)
            {
                if (con is Label) idc_rec_control.Add(con.Name, con);
            }
        }

        public void Clear()
        {
            idc_rec_control.Clear();
        }

        public void Save()
        {
            List<string> opnamelist = new List<string>();
            foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
            {
                if (kvp.Value is NumericUpDown)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as NumericUpDown).Value.ToString());
                }
                else if (kvp.Value is CheckBox)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as CheckBox).Checked.ToString());
                }
                else if (kvp.Value is RadioButton)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as RadioButton).Checked.ToString());
                }
                else if (kvp.Value is UC_txt)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as UC_txt).Text.ToString());
                }
                else
                {
                    opnamelist.Add(kvp.Key + "," + kvp.Value.Text);
                }
            }

            string opname_path = System.Windows.Forms.Application.StartupPath + "\\Recording";
            if (!System.IO.Directory.Exists(opname_path)) System.IO.Directory.CreateDirectory(opname_path);
            opname_path+= @"\" + this.formname + ".ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            txt.WriteTxt(opname_path, opnamelist);
        }

        public void Load()
        {
            string opname_path = System.Windows.Forms.Application.StartupPath + "\\Recording";
            if (!System.IO.Directory.Exists(opname_path)) System.IO.Directory.CreateDirectory(opname_path);
            opname_path += @"\" + this.formname + ".ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();

            List<string> opnamelist = txt.ReadTxt(opname_path);
            for (int i = 0; i < opnamelist.Count; i++)
            {
                string[] tem = opnamelist[i].Split(',');
                if (tem.Length == 2)
                {
                    foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
                    {

                        if (kvp.Key == tem[0])
                        {
                            if (kvp.Value is NumericUpDown)
                            {
                                decimal d = 0;
                                decimal.TryParse(tem[1], out d);
                                (kvp.Value as NumericUpDown).Value = d;
                            }
                            else if (kvp.Value is CheckBox)
                            {
                                bool t = false;
                                bool.TryParse(tem[1], out t);
                                (kvp.Value as CheckBox).Checked = t;
                            }
                            else if (kvp.Value is RadioButton)
                            {
                                bool t = false;
                                bool.TryParse(tem[1], out t);
                                (kvp.Value as RadioButton).Checked = t;
                            }
                            else if (kvp.Value is UC_txt)
                            {
                                (kvp.Value as UC_txt).Text = tem[1];
                            }
                            else
                            {
                                kvp.Value.Text = tem[1];
                            }
                        }
                    }
                }
            }
        }
    }
}
