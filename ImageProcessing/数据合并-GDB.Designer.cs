namespace ImageProcessing
{
    partial class 数据合并_GDB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_savegdb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_GDB文件 = new System.Windows.Forms.TextBox();
            this.btn开始合并 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txt_savegdb);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txt_GDB文件);
            this.panel2.Controls.Add(this.btn开始合并);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(753, 95);
            this.panel2.TabIndex = 50;
            // 
            // txt_savegdb
            // 
            this.txt_savegdb.Location = new System.Drawing.Point(110, 37);
            this.txt_savegdb.Name = "txt_savegdb";
            this.txt_savegdb.Size = new System.Drawing.Size(626, 21);
            this.txt_savegdb.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 57;
            this.label6.Text = "合并后保存gdb：";
            // 
            // txt_GDB文件
            // 
            this.txt_GDB文件.Location = new System.Drawing.Point(110, 8);
            this.txt_GDB文件.Name = "txt_GDB文件";
            this.txt_GDB文件.Size = new System.Drawing.Size(626, 21);
            this.txt_GDB文件.TabIndex = 31;
            this.txt_GDB文件.DoubleClick += new System.EventHandler(this.txt_GDB文件_DoubleClick);
            // 
            // btn开始合并
            // 
            this.btn开始合并.Location = new System.Drawing.Point(14, 64);
            this.btn开始合并.Name = "btn开始合并";
            this.btn开始合并.Size = new System.Drawing.Size(75, 23);
            this.btn开始合并.TabIndex = 35;
            this.btn开始合并.Text = "开始合并";
            this.btn开始合并.UseVisualStyleBackColor = true;
            this.btn开始合并.Click += new System.EventHandler(this.btn开始合并_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "gdb文件：";
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 95);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(753, 450);
            this.rtbRecord.TabIndex = 51;
            this.rtbRecord.Text = "";
            // 
            // 数据合并_GDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 545);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.panel2);
            this.Name = "数据合并_GDB";
            this.Text = "数据合并_GDB";
            this.Load += new System.EventHandler(this.数据合并_GDB_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_savegdb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_GDB文件;
        private System.Windows.Forms.Button btn开始合并;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbRecord;
    }
}