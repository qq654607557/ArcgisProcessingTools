namespace ImageProcessing
{
    partial class 依据图幅号拷贝工具
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
            this.btn开始处理 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_外接SHP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_col_opname = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_分配人员 = new System.Windows.Forms.ComboBox();
            this.txt_文件选择_5W总图幅 = new System.Windows.Forms.TextBox();
            this.txt_文件选择_2_5W总图幅 = new System.Windows.Forms.TextBox();
            this.txt_5W总图幅 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_2_5W总图幅 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_SHP文件名称字段 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_输出目录 = new System.Windows.Forms.TextBox();
            this.txt_数据目录 = new System.Windows.Forms.TextBox();
            this.txt_SHP文件 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn开始处理
            // 
            this.btn开始处理.Location = new System.Drawing.Point(7, 236);
            this.btn开始处理.Name = "btn开始处理";
            this.btn开始处理.Size = new System.Drawing.Size(75, 23);
            this.btn开始处理.TabIndex = 14;
            this.btn开始处理.Text = "开始处理";
            this.btn开始处理.UseVisualStyleBackColor = true;
            this.btn开始处理.Click += new System.EventHandler(this.btn开始处理_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "数据目录：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "SHP文件：";
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 266);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(803, 376);
            this.rtbRecord.TabIndex = 9;
            this.rtbRecord.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_外接SHP);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txt_SHP文件名称字段);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_输出目录);
            this.panel1.Controls.Add(this.txt_数据目录);
            this.panel1.Controls.Add(this.txt_SHP文件);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btn开始处理);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 266);
            this.panel1.TabIndex = 10;
            // 
            // txt_外接SHP
            // 
            this.txt_外接SHP.Location = new System.Drawing.Point(80, 45);
            this.txt_外接SHP.Name = "txt_外接SHP";
            this.txt_外接SHP.Size = new System.Drawing.Size(531, 21);
            this.txt_外接SHP.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "外接SHP：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_col_opname);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_分配人员);
            this.groupBox1.Controls.Add(this.txt_文件选择_5W总图幅);
            this.groupBox1.Controls.Add(this.txt_文件选择_2_5W总图幅);
            this.groupBox1.Controls.Add(this.txt_5W总图幅);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_2_5W总图幅);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(7, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 82);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图幅处理";
            // 
            // txt_col_opname
            // 
            this.txt_col_opname.Location = new System.Drawing.Point(670, 21);
            this.txt_col_opname.Name = "txt_col_opname";
            this.txt_col_opname.Size = new System.Drawing.Size(103, 21);
            this.txt_col_opname.TabIndex = 31;
            this.txt_col_opname.Text = "OPName";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(610, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "人员字段：";
            // 
            // txt_分配人员
            // 
            this.txt_分配人员.FormattingEnabled = true;
            this.txt_分配人员.Location = new System.Drawing.Point(670, 54);
            this.txt_分配人员.Name = "txt_分配人员";
            this.txt_分配人员.Size = new System.Drawing.Size(103, 20);
            this.txt_分配人员.TabIndex = 29;
            // 
            // txt_文件选择_5W总图幅
            // 
            this.txt_文件选择_5W总图幅.Location = new System.Drawing.Point(507, 53);
            this.txt_文件选择_5W总图幅.Name = "txt_文件选择_5W总图幅";
            this.txt_文件选择_5W总图幅.Size = new System.Drawing.Size(91, 21);
            this.txt_文件选择_5W总图幅.TabIndex = 28;
            this.txt_文件选择_5W总图幅.Text = "G49E";
            // 
            // txt_文件选择_2_5W总图幅
            // 
            this.txt_文件选择_2_5W总图幅.Location = new System.Drawing.Point(507, 21);
            this.txt_文件选择_2_5W总图幅.Name = "txt_文件选择_2_5W总图幅";
            this.txt_文件选择_2_5W总图幅.Size = new System.Drawing.Size(91, 21);
            this.txt_文件选择_2_5W总图幅.TabIndex = 27;
            this.txt_文件选择_2_5W总图幅.Text = "G48F";
            // 
            // txt_5W总图幅
            // 
            this.txt_5W总图幅.Location = new System.Drawing.Point(81, 52);
            this.txt_5W总图幅.Name = "txt_5W总图幅";
            this.txt_5W总图幅.Size = new System.Drawing.Size(420, 21);
            this.txt_5W总图幅.TabIndex = 25;
            this.txt_5W总图幅.Text = "D:\\影像统筹数据处理工具\\原始数据\\五万.shp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(610, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "分配人员：";
            // 
            // txt_2_5W总图幅
            // 
            this.txt_2_5W总图幅.Location = new System.Drawing.Point(81, 21);
            this.txt_2_5W总图幅.Name = "txt_2_5W总图幅";
            this.txt_2_5W总图幅.Size = new System.Drawing.Size(420, 21);
            this.txt_2_5W总图幅.TabIndex = 24;
            this.txt_2_5W总图幅.Text = "D:\\影像统筹数据处理工具\\原始数据\\二点五万.shp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "5W总图幅：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "2.5W总图幅：";
            // 
            // txt_SHP文件名称字段
            // 
            this.txt_SHP文件名称字段.Location = new System.Drawing.Point(724, 12);
            this.txt_SHP文件名称字段.Name = "txt_SHP文件名称字段";
            this.txt_SHP文件名称字段.Size = new System.Drawing.Size(62, 21);
            this.txt_SHP文件名称字段.TabIndex = 24;
            this.txt_SHP文件名称字段.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(624, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "SHP文件名称字段：";
            // 
            // txt_输出目录
            // 
            this.txt_输出目录.Location = new System.Drawing.Point(80, 112);
            this.txt_输出目录.Name = "txt_输出目录";
            this.txt_输出目录.Size = new System.Drawing.Size(531, 21);
            this.txt_输出目录.TabIndex = 21;
            this.txt_输出目录.DoubleClick += new System.EventHandler(this.txt_输出目录_DoubleClick);
            // 
            // txt_数据目录
            // 
            this.txt_数据目录.Location = new System.Drawing.Point(80, 76);
            this.txt_数据目录.Name = "txt_数据目录";
            this.txt_数据目录.Size = new System.Drawing.Size(531, 21);
            this.txt_数据目录.TabIndex = 20;
            this.txt_数据目录.DoubleClick += new System.EventHandler(this.txt_数据目录_DoubleClick);
            // 
            // txt_SHP文件
            // 
            this.txt_SHP文件.Location = new System.Drawing.Point(80, 12);
            this.txt_SHP文件.Name = "txt_SHP文件";
            this.txt_SHP文件.Size = new System.Drawing.Size(531, 21);
            this.txt_SHP文件.TabIndex = 19;
            this.txt_SHP文件.DoubleClick += new System.EventHandler(this.txt_SHP文件_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "输出目录：";
            // 
            // 依据图幅号拷贝工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 642);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.panel1);
            this.Name = "依据图幅号拷贝工具";
            this.Text = "依据图幅号拷贝工具";
            this.Load += new System.EventHandler(this.依据图幅号拷贝工具_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn开始处理;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_输出目录;
        private System.Windows.Forms.TextBox txt_数据目录;
        private System.Windows.Forms.TextBox txt_SHP文件;
        private System.Windows.Forms.TextBox txt_SHP文件名称字段;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_5W总图幅;
        private System.Windows.Forms.TextBox txt_2_5W总图幅;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_文件选择_5W总图幅;
        private System.Windows.Forms.TextBox txt_文件选择_2_5W总图幅;
        private System.Windows.Forms.ComboBox txt_分配人员;
        private System.Windows.Forms.TextBox txt_col_opname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_外接SHP;
        private System.Windows.Forms.Label label9;
    }
}