namespace ImageProcessing
{
    partial class Form拷贝文件工具
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
            this.txt_输出目录 = new System.Windows.Forms.TextBox();
            this.txt_数据目录 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn开始处理 = new System.Windows.Forms.Button();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txt_输出目录
            // 
            this.txt_输出目录.Location = new System.Drawing.Point(65, 55);
            this.txt_输出目录.Name = "txt_输出目录";
            this.txt_输出目录.Size = new System.Drawing.Size(531, 21);
            this.txt_输出目录.TabIndex = 25;
            // 
            // txt_数据目录
            // 
            this.txt_数据目录.Location = new System.Drawing.Point(65, 19);
            this.txt_数据目录.Name = "txt_数据目录";
            this.txt_数据目录.Size = new System.Drawing.Size(531, 21);
            this.txt_数据目录.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "输出目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "数据目录：";
            // 
            // btn开始处理
            // 
            this.btn开始处理.Location = new System.Drawing.Point(12, 82);
            this.btn开始处理.Name = "btn开始处理";
            this.btn开始处理.Size = new System.Drawing.Size(75, 23);
            this.btn开始处理.TabIndex = 26;
            this.btn开始处理.Text = "开始处理";
            this.btn开始处理.UseVisualStyleBackColor = true;
            this.btn开始处理.Click += new System.EventHandler(this.btn开始处理_Click);
            // 
            // rtbRecord
            // 
            this.rtbRecord.Location = new System.Drawing.Point(12, 111);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(584, 349);
            this.rtbRecord.TabIndex = 27;
            this.rtbRecord.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(602, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(285, 441);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // Form拷贝文件工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 472);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.btn开始处理);
            this.Controls.Add(this.txt_输出目录);
            this.Controls.Add(this.txt_数据目录);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Form拷贝文件工具";
            this.Text = "Form拷贝文件工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_输出目录;
        private System.Windows.Forms.TextBox txt_数据目录;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn开始处理;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}