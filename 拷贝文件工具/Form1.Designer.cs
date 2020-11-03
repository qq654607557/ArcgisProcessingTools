namespace 拷贝文件工具
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.btn开始处理 = new System.Windows.Forms.Button();
            this.txt_输出目录 = new System.Windows.Forms.TextBox();
            this.txt_数据目录 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(602, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(285, 441);
            this.richTextBox1.TabIndex = 35;
            this.richTextBox1.Text = "";
            // 
            // rtbRecord
            // 
            this.rtbRecord.Location = new System.Drawing.Point(12, 104);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(584, 349);
            this.rtbRecord.TabIndex = 34;
            this.rtbRecord.Text = "";
            // 
            // btn开始处理
            // 
            this.btn开始处理.Location = new System.Drawing.Point(12, 75);
            this.btn开始处理.Name = "btn开始处理";
            this.btn开始处理.Size = new System.Drawing.Size(75, 23);
            this.btn开始处理.TabIndex = 33;
            this.btn开始处理.Text = "开始处理";
            this.btn开始处理.UseVisualStyleBackColor = true;
            this.btn开始处理.Click += new System.EventHandler(this.btn开始处理_Click);
            // 
            // txt_输出目录
            // 
            this.txt_输出目录.Location = new System.Drawing.Point(65, 48);
            this.txt_输出目录.Name = "txt_输出目录";
            this.txt_输出目录.Size = new System.Drawing.Size(531, 21);
            this.txt_输出目录.TabIndex = 32;
            // 
            // txt_数据目录
            // 
            this.txt_数据目录.Location = new System.Drawing.Point(65, 12);
            this.txt_数据目录.Name = "txt_数据目录";
            this.txt_数据目录.Size = new System.Drawing.Size(531, 21);
            this.txt_数据目录.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "输出目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "数据目录：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 565);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.btn开始处理);
            this.Controls.Add(this.txt_输出目录);
            this.Controls.Add(this.txt_数据目录);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "拷贝文件工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.Button btn开始处理;
        private System.Windows.Forms.TextBox txt_输出目录;
        private System.Windows.Forms.TextBox txt_数据目录;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

