using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl
{
    public class HelperMessRecord
    {
        private RichTextBox rtbRecord;
        private Form form;

        public HelperMessRecord(Form form,RichTextBox rtb)
        {
            this.form = form;
            this.rtbRecord = rtb;
        }

      public  void Record(string mess)
        {
            form.Invoke((EventHandler)delegate
            {
                this.rtbRecord.AppendText(DateTime.Now.ToString("HH:mm ") + mess + "\n");
                rtbRecord.SelectionStart = rtbRecord.TextLength;
                rtbRecord.ScrollToCaret();
            });
        }

        public void RecordNODate(string mess)
        {
            form.Invoke((EventHandler)delegate
            {
                this.rtbRecord.AppendText( mess + "\n");
                rtbRecord.SelectionStart = rtbRecord.TextLength;
                rtbRecord.ScrollToCaret();
            });
        }

        public  void Clear()
        {
            form.Invoke((EventHandler)delegate
            {
                this.rtbRecord.Text = "";
            });
        }
    }
}
