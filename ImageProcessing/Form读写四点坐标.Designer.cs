namespace ImageProcessing
{
    partial class Form读写四点坐标
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
            this.panelInput = new System.Windows.Forms.Panel();
            this.txtc_弹出卫星填写框 = new System.Windows.Forms.CheckBox();
            this.txt_输出成果_文件夹 = new System.Windows.Forms.TextBox();
            this.txt_mdb模板 = new System.Windows.Forms.TextBox();
            this.txt_镶嵌线SHP_文件夹 = new System.Windows.Forms.TextBox();
            this.txt_分幅SHP = new System.Windows.Forms.TextBox();
            this.gb_坐标转换 = new System.Windows.Forms.GroupBox();
            this.rb_WKID_35 = new System.Windows.Forms.RadioButton();
            this.rb_WKID_36 = new System.Windows.Forms.RadioButton();
            this.txtn_WKID = new System.Windows.Forms.NumericUpDown();
            this.gb_四点位置顺序 = new System.Windows.Forms.GroupBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btn开始检测 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.gb_填写值 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelInput.SuspendLayout();
            this.gb_坐标转换.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtn_WKID)).BeginInit();
            this.gb_四点位置顺序.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gb_填写值.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.txtc_弹出卫星填写框);
            this.panelInput.Controls.Add(this.txt_输出成果_文件夹);
            this.panelInput.Controls.Add(this.txt_mdb模板);
            this.panelInput.Controls.Add(this.txt_镶嵌线SHP_文件夹);
            this.panelInput.Controls.Add(this.txt_分幅SHP);
            this.panelInput.Controls.Add(this.gb_坐标转换);
            this.panelInput.Controls.Add(this.gb_四点位置顺序);
            this.panelInput.Controls.Add(this.btn开始检测);
            this.panelInput.Controls.Add(this.label10);
            this.panelInput.Controls.Add(this.label9);
            this.panelInput.Controls.Add(this.label8);
            this.panelInput.Controls.Add(this.label7);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 0);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(1209, 126);
            this.panelInput.TabIndex = 49;
            // 
            // txtc_弹出卫星填写框
            // 
            this.txtc_弹出卫星填写框.AutoSize = true;
            this.txtc_弹出卫星填写框.Checked = true;
            this.txtc_弹出卫星填写框.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_弹出卫星填写框.Location = new System.Drawing.Point(742, 59);
            this.txtc_弹出卫星填写框.Name = "txtc_弹出卫星填写框";
            this.txtc_弹出卫星填写框.Size = new System.Drawing.Size(108, 16);
            this.txtc_弹出卫星填写框.TabIndex = 68;
            this.txtc_弹出卫星填写框.Text = "弹出卫星填写框";
            this.txtc_弹出卫星填写框.UseVisualStyleBackColor = true;
            // 
            // txt_输出成果_文件夹
            // 
            this.txt_输出成果_文件夹.Location = new System.Drawing.Point(139, 93);
            this.txt_输出成果_文件夹.Name = "txt_输出成果_文件夹";
            this.txt_输出成果_文件夹.Size = new System.Drawing.Size(588, 21);
            this.txt_输出成果_文件夹.TabIndex = 67;
            this.txt_输出成果_文件夹.DoubleClick += new System.EventHandler(this.txt_输出成果_文件夹_DoubleClick);
            // 
            // txt_mdb模板
            // 
            this.txt_mdb模板.Location = new System.Drawing.Point(139, 66);
            this.txt_mdb模板.Name = "txt_mdb模板";
            this.txt_mdb模板.Size = new System.Drawing.Size(588, 21);
            this.txt_mdb模板.TabIndex = 65;
            this.txt_mdb模板.DoubleClick += new System.EventHandler(this.txt_mdb模板_DoubleClick);
            // 
            // txt_镶嵌线SHP_文件夹
            // 
            this.txt_镶嵌线SHP_文件夹.Location = new System.Drawing.Point(139, 39);
            this.txt_镶嵌线SHP_文件夹.Name = "txt_镶嵌线SHP_文件夹";
            this.txt_镶嵌线SHP_文件夹.Size = new System.Drawing.Size(588, 21);
            this.txt_镶嵌线SHP_文件夹.TabIndex = 63;
            this.txt_镶嵌线SHP_文件夹.DoubleClick += new System.EventHandler(this.txt_镶嵌线SHP_文件夹_DoubleClick);
            // 
            // txt_分幅SHP
            // 
            this.txt_分幅SHP.Location = new System.Drawing.Point(139, 12);
            this.txt_分幅SHP.Name = "txt_分幅SHP";
            this.txt_分幅SHP.Size = new System.Drawing.Size(588, 21);
            this.txt_分幅SHP.TabIndex = 61;
            this.txt_分幅SHP.DoubleClick += new System.EventHandler(this.txt_分幅SHP_DoubleClick);
            // 
            // gb_坐标转换
            // 
            this.gb_坐标转换.Controls.Add(this.rb_WKID_35);
            this.gb_坐标转换.Controls.Add(this.rb_WKID_36);
            this.gb_坐标转换.Controls.Add(this.txtn_WKID);
            this.gb_坐标转换.Location = new System.Drawing.Point(733, 12);
            this.gb_坐标转换.Name = "gb_坐标转换";
            this.gb_坐标转换.Size = new System.Drawing.Size(200, 41);
            this.gb_坐标转换.TabIndex = 59;
            this.gb_坐标转换.TabStop = false;
            this.gb_坐标转换.Text = "坐标转换";
            // 
            // rb_WKID_35
            // 
            this.rb_WKID_35.AutoSize = true;
            this.rb_WKID_35.Checked = true;
            this.rb_WKID_35.Location = new System.Drawing.Point(9, 19);
            this.rb_WKID_35.Name = "rb_WKID_35";
            this.rb_WKID_35.Size = new System.Drawing.Size(35, 16);
            this.rb_WKID_35.TabIndex = 55;
            this.rb_WKID_35.TabStop = true;
            this.rb_WKID_35.Text = "35";
            this.rb_WKID_35.UseVisualStyleBackColor = true;
            this.rb_WKID_35.CheckedChanged += new System.EventHandler(this.rb_WKID_CheckedChanged);
            // 
            // rb_WKID_36
            // 
            this.rb_WKID_36.AutoSize = true;
            this.rb_WKID_36.Location = new System.Drawing.Point(73, 19);
            this.rb_WKID_36.Name = "rb_WKID_36";
            this.rb_WKID_36.Size = new System.Drawing.Size(35, 16);
            this.rb_WKID_36.TabIndex = 56;
            this.rb_WKID_36.Text = "36";
            this.rb_WKID_36.UseVisualStyleBackColor = true;
            this.rb_WKID_36.CheckedChanged += new System.EventHandler(this.rb_WKID_CheckedChanged);
            // 
            // txtn_WKID
            // 
            this.txtn_WKID.Location = new System.Drawing.Point(132, 17);
            this.txtn_WKID.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtn_WKID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtn_WKID.Name = "txtn_WKID";
            this.txtn_WKID.Size = new System.Drawing.Size(59, 21);
            this.txtn_WKID.TabIndex = 37;
            this.txtn_WKID.Value = new decimal(new int[] {
            4523,
            0,
            0,
            0});
            // 
            // gb_四点位置顺序
            // 
            this.gb_四点位置顺序.Controls.Add(this.numericUpDown4);
            this.gb_四点位置顺序.Controls.Add(this.label5);
            this.gb_四点位置顺序.Controls.Add(this.numericUpDown3);
            this.gb_四点位置顺序.Controls.Add(this.label4);
            this.gb_四点位置顺序.Controls.Add(this.numericUpDown2);
            this.gb_四点位置顺序.Controls.Add(this.label3);
            this.gb_四点位置顺序.Controls.Add(this.numericUpDown1);
            this.gb_四点位置顺序.Controls.Add(this.label2);
            this.gb_四点位置顺序.Location = new System.Drawing.Point(939, 19);
            this.gb_四点位置顺序.Name = "gb_四点位置顺序";
            this.gb_四点位置顺序.Size = new System.Drawing.Size(227, 89);
            this.gb_四点位置顺序.TabIndex = 51;
            this.gb_四点位置顺序.TabStop = false;
            this.gb_四点位置顺序.Text = "四点位置顺序";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(47, 25);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(59, 21);
            this.numericUpDown4.TabIndex = 37;
            this.numericUpDown4.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "左上：";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(47, 52);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(59, 21);
            this.numericUpDown3.TabIndex = 35;
            this.numericUpDown3.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "左下：";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(152, 52);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(59, 21);
            this.numericUpDown2.TabIndex = 33;
            this.numericUpDown2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "右下：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(152, 25);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 21);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "右上：";
            // 
            // btn开始检测
            // 
            this.btn开始检测.Location = new System.Drawing.Point(742, 91);
            this.btn开始检测.Name = "btn开始检测";
            this.btn开始检测.Size = new System.Drawing.Size(75, 23);
            this.btn开始检测.TabIndex = 35;
            this.btn开始检测.Text = "开始写入";
            this.btn开始检测.UseVisualStyleBackColor = true;
            this.btn开始检测.Click += new System.EventHandler(this.btn开始检测_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 66;
            this.label10.Text = "输出成果（文件夹）：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(83, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 64;
            this.label9.Text = "mdb模板：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 12);
            this.label8.TabIndex = 62;
            this.label8.Text = "镶嵌线SHP（文件夹）：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(83, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 60;
            this.label7.Text = "分幅SHP：";
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 444);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(1209, 215);
            this.rtbRecord.TabIndex = 14;
            this.rtbRecord.Text = "";
            // 
            // gb_填写值
            // 
            this.gb_填写值.Controls.Add(this.flowLayoutPanel1);
            this.gb_填写值.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_填写值.Location = new System.Drawing.Point(0, 126);
            this.gb_填写值.Name = "gb_填写值";
            this.gb_填写值.Size = new System.Drawing.Size(1209, 318);
            this.gb_填写值.TabIndex = 50;
            this.gb_填写值.TabStop = false;
            this.gb_填写值.Text = "填写值";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1203, 298);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // Form读写四点坐标
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 659);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.gb_填写值);
            this.Controls.Add(this.panelInput);
            this.Name = "Form读写四点坐标";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "读写四点坐标v2";
            this.Load += new System.EventHandler(this.Form读写四点坐标_Load);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.gb_坐标转换.ResumeLayout(false);
            this.gb_坐标转换.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtn_WKID)).EndInit();
            this.gb_四点位置顺序.ResumeLayout(false);
            this.gb_四点位置顺序.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gb_填写值.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn开始检测;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.GroupBox gb_四点位置顺序;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_填写值;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gb_坐标转换;
        private System.Windows.Forms.RadioButton rb_WKID_35;
        private System.Windows.Forms.RadioButton rb_WKID_36;
        private System.Windows.Forms.NumericUpDown txtn_WKID;
        private System.Windows.Forms.TextBox txt_输出成果_文件夹;
        private System.Windows.Forms.TextBox txt_mdb模板;
        private System.Windows.Forms.TextBox txt_镶嵌线SHP_文件夹;
        private System.Windows.Forms.TextBox txt_分幅SHP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox txtc_弹出卫星填写框;
    }
}