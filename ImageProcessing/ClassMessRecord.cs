﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessing
{
    public class ClassMessRecord
    {
        private RichTextBox rtbRecord;
        private Form form;

        public ClassMessRecord(Form form,RichTextBox rtb)
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

       public  void Clear()
        {
            form.Invoke((EventHandler)delegate
            {
                this.rtbRecord.Text = "";
            });
        }
    }
}
