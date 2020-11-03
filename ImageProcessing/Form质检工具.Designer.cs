namespace ImageProcessing
{
    partial class Form质检工具
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
            this.txt_错误日志 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chb_img_检查坐标系 = new System.Windows.Forms.CheckBox();
            this.chb_img_检查数量 = new System.Windows.Forms.CheckBox();
            this.chb_img_检查名称 = new System.Windows.Forms.CheckBox();
            this.btn开始检测 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chb_shp_检查坐标 = new System.Windows.Forms.CheckBox();
            this.txt_坐标值 = new System.Windows.Forms.NumericUpDown();
            this.lab_坐标 = new System.Windows.Forms.RadioButton();
            this.chb_shp检测_数据全为大写 = new System.Windows.Forms.CheckBox();
            this.txt_影像文件夹 = new System.Windows.Forms.TextBox();
            this.txt_shp文件夹 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_坐标值)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_错误日志);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btn开始检测);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txt_影像文件夹);
            this.panel1.Controls.Add(this.txt_shp文件夹);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 221);
            this.panel1.TabIndex = 12;
            // 
            // txt_错误日志
            // 
            this.txt_错误日志.Location = new System.Drawing.Point(84, 66);
            this.txt_错误日志.Name = "txt_错误日志";
            this.txt_错误日志.Size = new System.Drawing.Size(576, 21);
            this.txt_错误日志.TabIndex = 41;
            this.txt_错误日志.DoubleClick += new System.EventHandler(this.txt_错误日志_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "错误日志：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chb_img_检查坐标系);
            this.groupBox2.Controls.Add(this.chb_img_检查数量);
            this.groupBox2.Controls.Add(this.chb_img_检查名称);
            this.groupBox2.Location = new System.Drawing.Point(12, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 42);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "影像检测";
            // 
            // chb_img_检查坐标系
            // 
            this.chb_img_检查坐标系.AutoSize = true;
            this.chb_img_检查坐标系.Checked = true;
            this.chb_img_检查坐标系.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_img_检查坐标系.Location = new System.Drawing.Point(205, 20);
            this.chb_img_检查坐标系.Name = "chb_img_检查坐标系";
            this.chb_img_检查坐标系.Size = new System.Drawing.Size(84, 16);
            this.chb_img_检查坐标系.TabIndex = 38;
            this.chb_img_检查坐标系.Text = "检测坐标系";
            this.chb_img_检查坐标系.UseVisualStyleBackColor = true;
            // 
            // chb_img_检查数量
            // 
            this.chb_img_检查数量.AutoSize = true;
            this.chb_img_检查数量.Checked = true;
            this.chb_img_检查数量.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_img_检查数量.Location = new System.Drawing.Point(108, 20);
            this.chb_img_检查数量.Name = "chb_img_检查数量";
            this.chb_img_检查数量.Size = new System.Drawing.Size(72, 16);
            this.chb_img_检查数量.TabIndex = 37;
            this.chb_img_检查数量.Text = "检测数量";
            this.chb_img_检查数量.UseVisualStyleBackColor = true;
            // 
            // chb_img_检查名称
            // 
            this.chb_img_检查名称.AutoSize = true;
            this.chb_img_检查名称.Checked = true;
            this.chb_img_检查名称.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_img_检查名称.Location = new System.Drawing.Point(6, 20);
            this.chb_img_检查名称.Name = "chb_img_检查名称";
            this.chb_img_检查名称.Size = new System.Drawing.Size(72, 16);
            this.chb_img_检查名称.TabIndex = 36;
            this.chb_img_检查名称.Text = "检测名称";
            this.chb_img_检查名称.UseVisualStyleBackColor = true;
            // 
            // btn开始检测
            // 
            this.btn开始检测.Location = new System.Drawing.Point(12, 192);
            this.btn开始检测.Name = "btn开始检测";
            this.btn开始检测.Size = new System.Drawing.Size(75, 23);
            this.btn开始检测.TabIndex = 35;
            this.btn开始检测.Text = "开始检测";
            this.btn开始检测.UseVisualStyleBackColor = true;
            this.btn开始检测.Click += new System.EventHandler(this.btn开始检测_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.chb_shp检测_数据全为大写);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 45);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "shp检测";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chb_shp_检查坐标);
            this.panel2.Controls.Add(this.txt_坐标值);
            this.panel2.Controls.Add(this.lab_坐标);
            this.panel2.Location = new System.Drawing.Point(108, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(534, 24);
            this.panel2.TabIndex = 38;
            // 
            // chb_shp_检查坐标
            // 
            this.chb_shp_检查坐标.AutoSize = true;
            this.chb_shp_检查坐标.Checked = true;
            this.chb_shp_检查坐标.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_shp_检查坐标.Location = new System.Drawing.Point(3, 3);
            this.chb_shp_检查坐标.Name = "chb_shp_检查坐标";
            this.chb_shp_检查坐标.Size = new System.Drawing.Size(72, 16);
            this.chb_shp_检查坐标.TabIndex = 36;
            this.chb_shp_检查坐标.Text = "检测坐标";
            this.chb_shp_检查坐标.UseVisualStyleBackColor = true;
            // 
            // txt_坐标值
            // 
            this.txt_坐标值.Location = new System.Drawing.Point(81, 1);
            this.txt_坐标值.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txt_坐标值.Name = "txt_坐标值";
            this.txt_坐标值.Size = new System.Drawing.Size(52, 21);
            this.txt_坐标值.TabIndex = 37;
            this.txt_坐标值.Value = new decimal(new int[] {
            4490,
            0,
            0,
            0});
            this.txt_坐标值.ValueChanged += new System.EventHandler(this.txt_坐标值_ValueChanged);
            // 
            // lab_坐标
            // 
            this.lab_坐标.AutoSize = true;
            this.lab_坐标.Location = new System.Drawing.Point(136, 3);
            this.lab_坐标.Name = "lab_坐标";
            this.lab_坐标.Size = new System.Drawing.Size(269, 16);
            this.lab_坐标.TabIndex = 35;
            this.lab_坐标.TabStop = true;
            this.lab_坐标.Text = "GCS_China_Geodetic_Coordinate_System_2000";
            this.lab_坐标.UseVisualStyleBackColor = true;
            // 
            // chb_shp检测_数据全为大写
            // 
            this.chb_shp检测_数据全为大写.AutoSize = true;
            this.chb_shp检测_数据全为大写.Checked = true;
            this.chb_shp检测_数据全为大写.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_shp检测_数据全为大写.Location = new System.Drawing.Point(6, 21);
            this.chb_shp检测_数据全为大写.Name = "chb_shp检测_数据全为大写";
            this.chb_shp检测_数据全为大写.Size = new System.Drawing.Size(96, 16);
            this.chb_shp检测_数据全为大写.TabIndex = 35;
            this.chb_shp检测_数据全为大写.Text = "数据全为大写";
            this.chb_shp检测_数据全为大写.UseVisualStyleBackColor = true;
            // 
            // txt_影像文件夹
            // 
            this.txt_影像文件夹.Location = new System.Drawing.Point(84, 39);
            this.txt_影像文件夹.Name = "txt_影像文件夹";
            this.txt_影像文件夹.Size = new System.Drawing.Size(576, 21);
            this.txt_影像文件夹.TabIndex = 33;
            this.txt_影像文件夹.DoubleClick += new System.EventHandler(this.txt_影像文件夹_DoubleClick);
            // 
            // txt_shp文件夹
            // 
            this.txt_shp文件夹.Location = new System.Drawing.Point(84, 12);
            this.txt_shp文件夹.Name = "txt_shp文件夹";
            this.txt_shp文件夹.Size = new System.Drawing.Size(576, 21);
            this.txt_shp文件夹.TabIndex = 31;
            this.txt_shp文件夹.DoubleClick += new System.EventHandler(this.txt_shp文件夹_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "影像文件夹：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "SHP文件夹：";
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 221);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(671, 427);
            this.rtbRecord.TabIndex = 13;
            this.rtbRecord.Text = "";
            // 
            // Form质检工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 648);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.panel1);
            this.Name = "Form质检工具";
            this.Text = "质检工具";
            this.Load += new System.EventHandler(this.Form质检工具_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_坐标值)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_shp文件夹;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chb_shp检测_数据全为大写;
        private System.Windows.Forms.TextBox txt_影像文件夹;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chb_shp_检查坐标;
        private System.Windows.Forms.NumericUpDown txt_坐标值;
        private System.Windows.Forms.RadioButton lab_坐标;
        private System.Windows.Forms.Button btn开始检测;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chb_img_检查名称;
        private System.Windows.Forms.CheckBox chb_img_检查坐标系;
        private System.Windows.Forms.CheckBox chb_img_检查数量;
        private System.Windows.Forms.TextBox txt_错误日志;
        private System.Windows.Forms.Label label3;
    }
}