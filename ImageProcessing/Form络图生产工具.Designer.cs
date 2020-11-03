namespace ImageProcessing
{
    partial class Form络图生产工具
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtc_写入数据 = new System.Windows.Forms.CheckBox();
            this.txt_合并修改_end = new System.Windows.Forms.TextBox();
            this.txt_合并修改_Start = new System.Windows.Forms.TextBox();
            this.txtc_合并修改 = new System.Windows.Forms.CheckBox();
            this.btn生成络图 = new System.Windows.Forms.Button();
            this.btn选择TIF = new System.Windows.Forms.Button();
            this.btn读取xml = new System.Windows.Forms.Button();
            this.but测试 = new System.Windows.Forms.Button();
            this.txt_镶嵌数据集_end = new System.Windows.Forms.TextBox();
            this.txt_镶嵌数据集_start = new System.Windows.Forms.TextBox();
            this.txtc_镶嵌数据集 = new System.Windows.Forms.CheckBox();
            this.txt_导出SHP_结束 = new System.Windows.Forms.TextBox();
            this.txt_导出SHP_开始 = new System.Windows.Forms.TextBox();
            this.txtc_导出SHP = new System.Windows.Forms.CheckBox();
            this.txtc_只读 = new System.Windows.Forms.CheckBox();
            this.txt_轮廓结束 = new System.Windows.Forms.TextBox();
            this.txt_轮廓开始 = new System.Windows.Forms.TextBox();
            this.txtc_轮廓 = new System.Windows.Forms.CheckBox();
            this.txtc_强制检查 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWKID = new System.Windows.Forms.NumericUpDown();
            this.txt_成果输出文件夹 = new System.Windows.Forms.TextBox();
            this.txt_源数据文件夹 = new System.Windows.Forms.TextBox();
            this.btn开始生成 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTIF = new System.Windows.Forms.ListBox();
            this.btn删列shp = new System.Windows.Forms.Button();
            this.btn编辑shp = new System.Windows.Forms.Button();
            this.lstSHP = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWKID)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 115);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(1002, 474);
            this.rtbRecord.TabIndex = 7;
            this.rtbRecord.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtc_写入数据);
            this.panel1.Controls.Add(this.txt_合并修改_end);
            this.panel1.Controls.Add(this.txt_合并修改_Start);
            this.panel1.Controls.Add(this.txtc_合并修改);
            this.panel1.Controls.Add(this.btn生成络图);
            this.panel1.Controls.Add(this.btn选择TIF);
            this.panel1.Controls.Add(this.btn读取xml);
            this.panel1.Controls.Add(this.but测试);
            this.panel1.Controls.Add(this.txt_镶嵌数据集_end);
            this.panel1.Controls.Add(this.txt_镶嵌数据集_start);
            this.panel1.Controls.Add(this.txtc_镶嵌数据集);
            this.panel1.Controls.Add(this.txt_导出SHP_结束);
            this.panel1.Controls.Add(this.txt_导出SHP_开始);
            this.panel1.Controls.Add(this.txtc_导出SHP);
            this.panel1.Controls.Add(this.txtc_只读);
            this.panel1.Controls.Add(this.txt_轮廓结束);
            this.panel1.Controls.Add(this.txt_轮廓开始);
            this.panel1.Controls.Add(this.txtc_轮廓);
            this.panel1.Controls.Add(this.txtc_强制检查);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.txt_成果输出文件夹);
            this.panel1.Controls.Add(this.txt_源数据文件夹);
            this.panel1.Controls.Add(this.btn开始生成);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lstTIF);
            this.panel1.Controls.Add(this.btn删列shp);
            this.panel1.Controls.Add(this.btn编辑shp);
            this.panel1.Controls.Add(this.lstSHP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 115);
            this.panel1.TabIndex = 8;
            // 
            // txtc_写入数据
            // 
            this.txtc_写入数据.AutoSize = true;
            this.txtc_写入数据.Checked = true;
            this.txtc_写入数据.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_写入数据.Location = new System.Drawing.Point(908, 86);
            this.txtc_写入数据.Name = "txtc_写入数据";
            this.txtc_写入数据.Size = new System.Drawing.Size(72, 16);
            this.txtc_写入数据.TabIndex = 52;
            this.txtc_写入数据.Text = "写入数据";
            this.txtc_写入数据.UseVisualStyleBackColor = true;
            // 
            // txt_合并修改_end
            // 
            this.txt_合并修改_end.Location = new System.Drawing.Point(870, 84);
            this.txt_合并修改_end.Name = "txt_合并修改_end";
            this.txt_合并修改_end.Size = new System.Drawing.Size(32, 21);
            this.txt_合并修改_end.TabIndex = 51;
            this.txt_合并修改_end.Text = "0";
            // 
            // txt_合并修改_Start
            // 
            this.txt_合并修改_Start.Location = new System.Drawing.Point(832, 84);
            this.txt_合并修改_Start.Name = "txt_合并修改_Start";
            this.txt_合并修改_Start.Size = new System.Drawing.Size(32, 21);
            this.txt_合并修改_Start.TabIndex = 49;
            this.txt_合并修改_Start.Text = "0";
            // 
            // txtc_合并修改
            // 
            this.txtc_合并修改.AutoSize = true;
            this.txtc_合并修改.Checked = true;
            this.txtc_合并修改.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_合并修改.Location = new System.Drawing.Point(757, 86);
            this.txtc_合并修改.Name = "txtc_合并修改";
            this.txtc_合并修改.Size = new System.Drawing.Size(84, 16);
            this.txtc_合并修改.TabIndex = 50;
            this.txtc_合并修改.Text = "合并修改：";
            this.txtc_合并修改.UseVisualStyleBackColor = true;
            // 
            // btn生成络图
            // 
            this.btn生成络图.Location = new System.Drawing.Point(822, 19);
            this.btn生成络图.Name = "btn生成络图";
            this.btn生成络图.Size = new System.Drawing.Size(75, 23);
            this.btn生成络图.TabIndex = 10;
            this.btn生成络图.Text = "生成络图";
            this.btn生成络图.UseVisualStyleBackColor = true;
            this.btn生成络图.Visible = false;
            this.btn生成络图.Click += new System.EventHandler(this.Btn生成络图_Click);
            // 
            // btn选择TIF
            // 
            this.btn选择TIF.Location = new System.Drawing.Point(737, 19);
            this.btn选择TIF.Name = "btn选择TIF";
            this.btn选择TIF.Size = new System.Drawing.Size(75, 23);
            this.btn选择TIF.TabIndex = 7;
            this.btn选择TIF.Text = "选择TIF";
            this.btn选择TIF.UseVisualStyleBackColor = true;
            this.btn选择TIF.Visible = false;
            this.btn选择TIF.Click += new System.EventHandler(this.Btn选择TIF_Click);
            // 
            // btn读取xml
            // 
            this.btn读取xml.Location = new System.Drawing.Point(822, 48);
            this.btn读取xml.Name = "btn读取xml";
            this.btn读取xml.Size = new System.Drawing.Size(75, 23);
            this.btn读取xml.TabIndex = 4;
            this.btn读取xml.Text = "读取xml";
            this.btn读取xml.UseVisualStyleBackColor = true;
            this.btn读取xml.Click += new System.EventHandler(this.Btn读取xml_Click);
            // 
            // but测试
            // 
            this.but测试.Location = new System.Drawing.Point(737, 48);
            this.but测试.Name = "but测试";
            this.but测试.Size = new System.Drawing.Size(75, 23);
            this.but测试.TabIndex = 2;
            this.but测试.Text = "生成shp";
            this.but测试.UseVisualStyleBackColor = true;
            this.but测试.Visible = false;
            this.but测试.Click += new System.EventHandler(this.but测试_Click);
            // 
            // txt_镶嵌数据集_end
            // 
            this.txt_镶嵌数据集_end.Location = new System.Drawing.Point(390, 84);
            this.txt_镶嵌数据集_end.Name = "txt_镶嵌数据集_end";
            this.txt_镶嵌数据集_end.Size = new System.Drawing.Size(32, 21);
            this.txt_镶嵌数据集_end.TabIndex = 48;
            this.txt_镶嵌数据集_end.Text = "0";
            // 
            // txt_镶嵌数据集_start
            // 
            this.txt_镶嵌数据集_start.Location = new System.Drawing.Point(352, 84);
            this.txt_镶嵌数据集_start.Name = "txt_镶嵌数据集_start";
            this.txt_镶嵌数据集_start.Size = new System.Drawing.Size(32, 21);
            this.txt_镶嵌数据集_start.TabIndex = 46;
            this.txt_镶嵌数据集_start.Text = "0";
            // 
            // txtc_镶嵌数据集
            // 
            this.txtc_镶嵌数据集.AutoSize = true;
            this.txtc_镶嵌数据集.Checked = true;
            this.txtc_镶嵌数据集.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_镶嵌数据集.Location = new System.Drawing.Point(277, 86);
            this.txtc_镶嵌数据集.Name = "txtc_镶嵌数据集";
            this.txtc_镶嵌数据集.Size = new System.Drawing.Size(96, 16);
            this.txtc_镶嵌数据集.TabIndex = 47;
            this.txtc_镶嵌数据集.Text = "镶嵌数据集：";
            this.txtc_镶嵌数据集.UseVisualStyleBackColor = true;
            // 
            // txt_导出SHP_结束
            // 
            this.txt_导出SHP_结束.Location = new System.Drawing.Point(706, 84);
            this.txt_导出SHP_结束.Name = "txt_导出SHP_结束";
            this.txt_导出SHP_结束.Size = new System.Drawing.Size(32, 21);
            this.txt_导出SHP_结束.TabIndex = 45;
            this.txt_导出SHP_结束.Text = "0";
            // 
            // txt_导出SHP_开始
            // 
            this.txt_导出SHP_开始.Location = new System.Drawing.Point(668, 84);
            this.txt_导出SHP_开始.Name = "txt_导出SHP_开始";
            this.txt_导出SHP_开始.Size = new System.Drawing.Size(32, 21);
            this.txt_导出SHP_开始.TabIndex = 43;
            this.txt_导出SHP_开始.Text = "0";
            // 
            // txtc_导出SHP
            // 
            this.txtc_导出SHP.AutoSize = true;
            this.txtc_导出SHP.Checked = true;
            this.txtc_导出SHP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_导出SHP.Location = new System.Drawing.Point(593, 86);
            this.txtc_导出SHP.Name = "txtc_导出SHP";
            this.txtc_导出SHP.Size = new System.Drawing.Size(78, 16);
            this.txtc_导出SHP.TabIndex = 44;
            this.txtc_导出SHP.Text = "导出SHP：";
            this.txtc_导出SHP.UseVisualStyleBackColor = true;
            // 
            // txtc_只读
            // 
            this.txtc_只读.AutoSize = true;
            this.txtc_只读.Location = new System.Drawing.Point(223, 86);
            this.txtc_只读.Name = "txtc_只读";
            this.txtc_只读.Size = new System.Drawing.Size(48, 16);
            this.txtc_只读.TabIndex = 42;
            this.txtc_只读.Text = "只读";
            this.txtc_只读.UseVisualStyleBackColor = true;
            // 
            // txt_轮廓结束
            // 
            this.txt_轮廓结束.Location = new System.Drawing.Point(551, 84);
            this.txt_轮廓结束.Name = "txt_轮廓结束";
            this.txt_轮廓结束.Size = new System.Drawing.Size(32, 21);
            this.txt_轮廓结束.TabIndex = 41;
            this.txt_轮廓结束.Text = "0";
            // 
            // txt_轮廓开始
            // 
            this.txt_轮廓开始.Location = new System.Drawing.Point(513, 84);
            this.txt_轮廓开始.Name = "txt_轮廓开始";
            this.txt_轮廓开始.Size = new System.Drawing.Size(32, 21);
            this.txt_轮廓开始.TabIndex = 39;
            this.txt_轮廓开始.Text = "0";
            // 
            // txtc_轮廓
            // 
            this.txtc_轮廓.AutoSize = true;
            this.txtc_轮廓.Checked = true;
            this.txtc_轮廓.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_轮廓.Location = new System.Drawing.Point(438, 86);
            this.txtc_轮廓.Name = "txtc_轮廓";
            this.txtc_轮廓.Size = new System.Drawing.Size(84, 16);
            this.txtc_轮廓.TabIndex = 40;
            this.txtc_轮廓.Text = "生成轮廓：";
            this.txtc_轮廓.UseVisualStyleBackColor = true;
            // 
            // txtc_强制检查
            // 
            this.txtc_强制检查.AutoSize = true;
            this.txtc_强制检查.Checked = true;
            this.txtc_强制检查.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtc_强制检查.Location = new System.Drawing.Point(669, 19);
            this.txtc_强制检查.Name = "txtc_强制检查";
            this.txtc_强制检查.Size = new System.Drawing.Size(72, 16);
            this.txtc_强制检查.TabIndex = 31;
            this.txtc_强制检查.Text = "强制检查";
            this.txtc_强制检查.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(738, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 36);
            this.label3.TabIndex = 30;
            this.label3.Text = "*注意：\r\nxml文件节点区分大小写注意\r\n《TestAngle.ini》配置文件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtWKID);
            this.groupBox2.Location = new System.Drawing.Point(106, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(106, 39);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "坐标值";
            // 
            // txtWKID
            // 
            this.txtWKID.Location = new System.Drawing.Point(6, 12);
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
            // txt_成果输出文件夹
            // 
            this.txt_成果输出文件夹.Location = new System.Drawing.Point(112, 46);
            this.txt_成果输出文件夹.Name = "txt_成果输出文件夹";
            this.txt_成果输出文件夹.Size = new System.Drawing.Size(551, 21);
            this.txt_成果输出文件夹.TabIndex = 28;
            this.txt_成果输出文件夹.DoubleClick += new System.EventHandler(this.txt_成果输出文件夹_DoubleClick);
            // 
            // txt_源数据文件夹
            // 
            this.txt_源数据文件夹.Location = new System.Drawing.Point(112, 17);
            this.txt_源数据文件夹.Name = "txt_源数据文件夹";
            this.txt_源数据文件夹.Size = new System.Drawing.Size(551, 21);
            this.txt_源数据文件夹.TabIndex = 20;
            this.txt_源数据文件夹.DoubleClick += new System.EventHandler(this.txt_源数据文件夹_DoubleClick);
            // 
            // btn开始生成
            // 
            this.btn开始生成.Location = new System.Drawing.Point(12, 83);
            this.btn开始生成.Name = "btn开始生成";
            this.btn开始生成.Size = new System.Drawing.Size(75, 23);
            this.btn开始生成.TabIndex = 14;
            this.btn开始生成.Text = "生成络图";
            this.btn开始生成.UseVisualStyleBackColor = true;
            this.btn开始生成.Click += new System.EventHandler(this.Btn开始生成_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "成果输出文件夹：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据输入文件夹：";
            // 
            // lstTIF
            // 
            this.lstTIF.AllowDrop = true;
            this.lstTIF.FormattingEnabled = true;
            this.lstTIF.ItemHeight = 12;
            this.lstTIF.Location = new System.Drawing.Point(12, 121);
            this.lstTIF.Name = "lstTIF";
            this.lstTIF.Size = new System.Drawing.Size(191, 52);
            this.lstTIF.TabIndex = 8;
            this.lstTIF.Visible = false;
            this.lstTIF.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstTIF_DragDrop);
            this.lstTIF.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstTIF_DragEnter);
            this.lstTIF.DragOver += new System.Windows.Forms.DragEventHandler(this.LstTIF_DragOver);
            this.lstTIF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstTIF_KeyPress);
            this.lstTIF.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LstTIF_KeyUp);
            // 
            // btn删列shp
            // 
            this.btn删列shp.Location = new System.Drawing.Point(818, 12);
            this.btn删列shp.Name = "btn删列shp";
            this.btn删列shp.Size = new System.Drawing.Size(75, 23);
            this.btn删列shp.TabIndex = 6;
            this.btn删列shp.Text = "删列shp";
            this.btn删列shp.UseVisualStyleBackColor = true;
            this.btn删列shp.Visible = false;
            this.btn删列shp.Click += new System.EventHandler(this.Btn删列shp_Click);
            // 
            // btn编辑shp
            // 
            this.btn编辑shp.Location = new System.Drawing.Point(733, 12);
            this.btn编辑shp.Name = "btn编辑shp";
            this.btn编辑shp.Size = new System.Drawing.Size(75, 23);
            this.btn编辑shp.TabIndex = 5;
            this.btn编辑shp.Text = "编辑shp";
            this.btn编辑shp.UseVisualStyleBackColor = true;
            this.btn编辑shp.Visible = false;
            this.btn编辑shp.Click += new System.EventHandler(this.Btn编辑shp_Click);
            // 
            // lstSHP
            // 
            this.lstSHP.AllowDrop = true;
            this.lstSHP.FormattingEnabled = true;
            this.lstSHP.ItemHeight = 12;
            this.lstSHP.Location = new System.Drawing.Point(243, 121);
            this.lstSHP.Name = "lstSHP";
            this.lstSHP.Size = new System.Drawing.Size(180, 52);
            this.lstSHP.TabIndex = 9;
            this.lstSHP.Visible = false;
            this.lstSHP.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstTIF_DragDrop);
            this.lstSHP.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstTIF_DragEnter);
            this.lstSHP.DragOver += new System.Windows.Forms.DragEventHandler(this.LstTIF_DragOver);
            this.lstSHP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstTIF_KeyUp);
            // 
            // Form络图生产工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 589);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.panel1);
            this.Name = "Form络图生产工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "络图生产工具 V3.2";
            this.Load += new System.EventHandler(this.Form络图_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWKID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button but测试;
        private System.Windows.Forms.Button btn读取xml;
        private System.Windows.Forms.Button btn编辑shp;
        private System.Windows.Forms.Button btn删列shp;
        private System.Windows.Forms.ListBox lstSHP;
        private System.Windows.Forms.ListBox lstTIF;
        private System.Windows.Forms.Button btn选择TIF;
        private System.Windows.Forms.Button btn生成络图;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn开始生成;
        private System.Windows.Forms.TextBox txt_源数据文件夹;
        private System.Windows.Forms.TextBox txt_成果输出文件夹;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown txtWKID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox txtc_强制检查;
        private System.Windows.Forms.TextBox txt_轮廓开始;
        private System.Windows.Forms.CheckBox txtc_轮廓;
        private System.Windows.Forms.CheckBox txtc_只读;
        private System.Windows.Forms.TextBox txt_轮廓结束;
        private System.Windows.Forms.TextBox txt_导出SHP_结束;
        private System.Windows.Forms.TextBox txt_导出SHP_开始;
        private System.Windows.Forms.CheckBox txtc_导出SHP;
        private System.Windows.Forms.TextBox txt_镶嵌数据集_end;
        private System.Windows.Forms.TextBox txt_镶嵌数据集_start;
        private System.Windows.Forms.CheckBox txtc_镶嵌数据集;
        private System.Windows.Forms.TextBox txt_合并修改_end;
        private System.Windows.Forms.TextBox txt_合并修改_Start;
        private System.Windows.Forms.CheckBox txtc_合并修改;
        private System.Windows.Forms.CheckBox txtc_写入数据;
    }
}

