namespace ImageProcessing
{
    partial class Form图幅镶嵌线分_修改文件名称
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
            this.btn开始生成 = new System.Windows.Forms.Button();
            this.txt_成果输出文件夹 = new System.Windows.Forms.TextBox();
            this.txt_shp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.txt_xml = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_shp_col = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_shp_col);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_xml);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btn开始生成);
            this.panel1.Controls.Add(this.txt_成果输出文件夹);
            this.panel1.Controls.Add(this.txt_shp);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1048, 129);
            this.panel1.TabIndex = 10;
            // 
            // btn开始生成
            // 
            this.btn开始生成.Location = new System.Drawing.Point(12, 100);
            this.btn开始生成.Name = "btn开始生成";
            this.btn开始生成.Size = new System.Drawing.Size(75, 23);
            this.btn开始生成.TabIndex = 33;
            this.btn开始生成.Text = "开始运行";
            this.btn开始生成.UseVisualStyleBackColor = true;
            this.btn开始生成.Click += new System.EventHandler(this.btn开始生成_Click);
            // 
            // txt_成果输出文件夹
            // 
            this.txt_成果输出文件夹.Location = new System.Drawing.Point(107, 68);
            this.txt_成果输出文件夹.Name = "txt_成果输出文件夹";
            this.txt_成果输出文件夹.Size = new System.Drawing.Size(551, 21);
            this.txt_成果输出文件夹.TabIndex = 32;
            this.txt_成果输出文件夹.DoubleClick += new System.EventHandler(this.txt_成果输出文件夹_DoubleClick);
            // 
            // txt_shp
            // 
            this.txt_shp.Location = new System.Drawing.Point(107, 12);
            this.txt_shp.Name = "txt_shp";
            this.txt_shp.Size = new System.Drawing.Size(551, 21);
            this.txt_shp.TabIndex = 31;
            this.txt_shp.DoubleClick += new System.EventHandler(this.txt_shp_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "修改名称文件夹：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "SHP文件：";
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 129);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(1048, 506);
            this.rtbRecord.TabIndex = 11;
            this.rtbRecord.Text = "";
            // 
            // txt_xml
            // 
            this.txt_xml.Location = new System.Drawing.Point(107, 41);
            this.txt_xml.Name = "txt_xml";
            this.txt_xml.Size = new System.Drawing.Size(551, 21);
            this.txt_xml.TabIndex = 36;
            this.txt_xml.DoubleClick += new System.EventHandler(this.txt_xml_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "xml文件：";
            // 
            // txt_shp_col
            // 
            this.txt_shp_col.Location = new System.Drawing.Point(720, 12);
            this.txt_shp_col.Name = "txt_shp_col";
            this.txt_shp_col.Size = new System.Drawing.Size(87, 21);
            this.txt_shp_col.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(673, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 37;
            this.label5.Text = "名称列：";
            // 
            // Form修改数据001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 635);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.panel1);
            this.Name = "Form修改数据001";
            this.Text = "修改数据001";
            this.Load += new System.EventHandler(this.Form修改数据001_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn开始生成;
        private System.Windows.Forms.TextBox txt_成果输出文件夹;
        private System.Windows.Forms.TextBox txt_shp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.TextBox txt_shp_col;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_xml;
        private System.Windows.Forms.Label label4;
    }
}