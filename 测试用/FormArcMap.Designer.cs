namespace 测试用
{
    partial class FormArcMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormArcMap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.but循环裁剪 = new System.Windows.Forms.Button();
            this.btn_选取文件夹 = new System.Windows.Forms.Button();
            this.btn_选择文件 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new AxESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl1 = new AxESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new AxESRI.ArcGIS.Controls.AxLicenseControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axLicenseControl1);
            this.panel1.Controls.Add(this.but循环裁剪);
            this.panel1.Controls.Add(this.btn_选取文件夹);
            this.panel1.Controls.Add(this.btn_选择文件);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 73);
            this.panel1.TabIndex = 4;
            // 
            // but循环裁剪
            // 
            this.but循环裁剪.Location = new System.Drawing.Point(12, 44);
            this.but循环裁剪.Name = "but循环裁剪";
            this.but循环裁剪.Size = new System.Drawing.Size(75, 23);
            this.but循环裁剪.TabIndex = 9;
            this.but循环裁剪.Text = "循环裁剪";
            this.but循环裁剪.UseVisualStyleBackColor = true;
            this.but循环裁剪.Click += new System.EventHandler(this.but循环裁剪_Click);
            // 
            // btn_选取文件夹
            // 
            this.btn_选取文件夹.Location = new System.Drawing.Point(93, 12);
            this.btn_选取文件夹.Name = "btn_选取文件夹";
            this.btn_选取文件夹.Size = new System.Drawing.Size(75, 23);
            this.btn_选取文件夹.TabIndex = 8;
            this.btn_选取文件夹.Text = "选取文件夹";
            this.btn_选取文件夹.UseVisualStyleBackColor = true;
            this.btn_选取文件夹.Click += new System.EventHandler(this.btn_选取文件夹_Click);
            // 
            // btn_选择文件
            // 
            this.btn_选择文件.Location = new System.Drawing.Point(12, 12);
            this.btn_选择文件.Name = "btn_选择文件";
            this.btn_选择文件.Size = new System.Drawing.Size(75, 23);
            this.btn_选择文件.TabIndex = 7;
            this.btn_选择文件.Text = "选择文件";
            this.btn_选择文件.UseVisualStyleBackColor = true;
            this.btn_选择文件.Click += new System.EventHandler(this.btn_选择文件_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(727, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbRecord.Location = new System.Drawing.Point(0, 401);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(545, 183);
            this.rtbRecord.TabIndex = 11;
            this.rtbRecord.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 73);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axMapControl1);
            this.splitContainer1.Panel2.Controls.Add(this.rtbRecord);
            this.splitContainer1.Size = new System.Drawing.Size(823, 584);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 12;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(274, 584);
            this.axTOCControl1.TabIndex = 5;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(545, 401);
            this.axMapControl1.TabIndex = 2;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(174, 8);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 10;
            // 
            // FormArcMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 657);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FormArcMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ArcMap";
            this.Load += new System.EventHandler(this.FormArcMap_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private AxESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.Button btn_选取文件夹;
        private System.Windows.Forms.Button btn_选择文件;
        private System.Windows.Forms.Button but循环裁剪;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AxESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}