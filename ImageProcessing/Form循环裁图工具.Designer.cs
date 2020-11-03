namespace ImageProcessing
{
    partial class Form循环裁图工具
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn读取文件 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWKID = new System.Windows.Forms.NumericUpDown();
            this.btn开始生成 = new System.Windows.Forms.Button();
            this.txt_成果输出文件夹 = new System.Windows.Forms.TextBox();
            this.txt_源数据文件夹 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn创建要数集 = new System.Windows.Forms.Button();
            this.btn裁剪 = new System.Windows.Forms.Button();
            this.btn循环 = new System.Windows.Forms.Button();
            this.btnSelectFolders = new System.Windows.Forms.Button();
            this.lstTIF = new System.Windows.Forms.ListBox();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWKID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn读取文件);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btn开始生成);
            this.panel1.Controls.Add(this.txt_成果输出文件夹);
            this.panel1.Controls.Add(this.txt_源数据文件夹);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn创建要数集);
            this.panel1.Controls.Add(this.btn裁剪);
            this.panel1.Controls.Add(this.btn循环);
            this.panel1.Controls.Add(this.btnSelectFolders);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 121);
            this.panel1.TabIndex = 9;
            // 
            // btn读取文件
            // 
            this.btn读取文件.Location = new System.Drawing.Point(11, 79);
            this.btn读取文件.Name = "btn读取文件";
            this.btn读取文件.Size = new System.Drawing.Size(75, 23);
            this.btn读取文件.TabIndex = 35;
            this.btn读取文件.Text = "读取文件";
            this.btn读取文件.UseVisualStyleBackColor = true;
            this.btn读取文件.Click += new System.EventHandler(this.btn读取文件_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtWKID);
            this.groupBox2.Location = new System.Drawing.Point(226, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 39);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "坐标值";
            this.groupBox2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "成果输出文件夹：";
            // 
            // txtWKID
            // 
            this.txtWKID.Location = new System.Drawing.Point(11, 13);
            this.txtWKID.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.txtWKID.Name = "txtWKID";
            this.txtWKID.Size = new System.Drawing.Size(92, 21);
            this.txtWKID.TabIndex = 0;
            this.txtWKID.Value = new decimal(new int[] {
            4490,
            0,
            0,
            0});
            // 
            // btn开始生成
            // 
            this.btn开始生成.Location = new System.Drawing.Point(107, 79);
            this.btn开始生成.Name = "btn开始生成";
            this.btn开始生成.Size = new System.Drawing.Size(75, 23);
            this.btn开始生成.TabIndex = 33;
            this.btn开始生成.Text = "开始裁剪";
            this.btn开始生成.UseVisualStyleBackColor = true;
            this.btn开始生成.Click += new System.EventHandler(this.btn开始生成_Click);
            // 
            // txt_成果输出文件夹
            // 
            this.txt_成果输出文件夹.Location = new System.Drawing.Point(107, 41);
            this.txt_成果输出文件夹.Name = "txt_成果输出文件夹";
            this.txt_成果输出文件夹.Size = new System.Drawing.Size(551, 21);
            this.txt_成果输出文件夹.TabIndex = 32;
            this.txt_成果输出文件夹.TextChanged += new System.EventHandler(this.txt_成果输出文件夹_TextChanged);
            this.txt_成果输出文件夹.DoubleClick += new System.EventHandler(this.txt_成果输出文件夹_DoubleClick);
            // 
            // txt_源数据文件夹
            // 
            this.txt_源数据文件夹.Location = new System.Drawing.Point(107, 12);
            this.txt_源数据文件夹.Name = "txt_源数据文件夹";
            this.txt_源数据文件夹.Size = new System.Drawing.Size(551, 21);
            this.txt_源数据文件夹.TabIndex = 31;
            this.txt_源数据文件夹.TextChanged += new System.EventHandler(this.txt_源数据文件夹_TextChanged);
            this.txt_源数据文件夹.DoubleClick += new System.EventHandler(this.txt_源数据文件夹_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "成果输出文件夹：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "数据输入文件夹：";
            // 
            // btn创建要数集
            // 
            this.btn创建要数集.Location = new System.Drawing.Point(878, 41);
            this.btn创建要数集.Name = "btn创建要数集";
            this.btn创建要数集.Size = new System.Drawing.Size(75, 23);
            this.btn创建要数集.TabIndex = 11;
            this.btn创建要数集.Text = "创建要数集";
            this.btn创建要数集.UseVisualStyleBackColor = true;
            this.btn创建要数集.Visible = false;
            this.btn创建要数集.Click += new System.EventHandler(this.Btn创建要数集_Click);
            // 
            // btn裁剪
            // 
            this.btn裁剪.Location = new System.Drawing.Point(878, 12);
            this.btn裁剪.Name = "btn裁剪";
            this.btn裁剪.Size = new System.Drawing.Size(75, 23);
            this.btn裁剪.TabIndex = 10;
            this.btn裁剪.Text = "裁剪";
            this.btn裁剪.UseVisualStyleBackColor = true;
            this.btn裁剪.Visible = false;
            this.btn裁剪.Click += new System.EventHandler(this.Btn裁剪_Click);
            // 
            // btn循环
            // 
            this.btn循环.Location = new System.Drawing.Point(797, 41);
            this.btn循环.Name = "btn循环";
            this.btn循环.Size = new System.Drawing.Size(75, 23);
            this.btn循环.TabIndex = 9;
            this.btn循环.Text = "循环";
            this.btn循环.UseVisualStyleBackColor = true;
            this.btn循环.Visible = false;
            this.btn循环.Click += new System.EventHandler(this.Btn循环_Click);
            // 
            // btnSelectFolders
            // 
            this.btnSelectFolders.Location = new System.Drawing.Point(797, 12);
            this.btnSelectFolders.Name = "btnSelectFolders";
            this.btnSelectFolders.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolders.TabIndex = 0;
            this.btnSelectFolders.Text = "选择文件夹";
            this.btnSelectFolders.UseVisualStyleBackColor = true;
            this.btnSelectFolders.Visible = false;
            this.btnSelectFolders.Click += new System.EventHandler(this.BtnSelectFolders_Click);
            // 
            // lstTIF
            // 
            this.lstTIF.AllowDrop = true;
            this.lstTIF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTIF.FormattingEnabled = true;
            this.lstTIF.ItemHeight = 12;
            this.lstTIF.Location = new System.Drawing.Point(0, 0);
            this.lstTIF.Name = "lstTIF";
            this.lstTIF.Size = new System.Drawing.Size(469, 460);
            this.lstTIF.TabIndex = 8;
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 0);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(498, 460);
            this.rtbRecord.TabIndex = 10;
            this.rtbRecord.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 121);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstTIF);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbRecord);
            this.splitContainer1.Size = new System.Drawing.Size(971, 460);
            this.splitContainer1.SplitterDistance = 469;
            this.splitContainer1.TabIndex = 11;
            // 
            // Form循环裁图工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 581);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "Form循环裁图工具";
            this.Text = "循环裁图工具";
            this.Load += new System.EventHandler(this.Form循环裁图工具_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWKID)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstTIF;
        private System.Windows.Forms.Button btnSelectFolders;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.Button btn循环;
        private System.Windows.Forms.Button btn裁剪;
        private System.Windows.Forms.Button btn创建要数集;
        private System.Windows.Forms.TextBox txt_成果输出文件夹;
        private System.Windows.Forms.TextBox txt_源数据文件夹;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn开始生成;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtWKID;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn读取文件;
    }
}