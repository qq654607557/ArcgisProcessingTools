
namespace ImageProcessing.DataFusion
{
    partial class Form数据处理_批量导出列别名EXCE
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt导出txt2 = new System.Windows.Forms.TextBox();
            this.txt导入txt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt导出txt = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbMess = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt导出txt2);
            this.groupBox1.Controls.Add(this.txt导入txt);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txt导出txt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1058, 96);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件";
            // 
            // txt导出txt2
            // 
            this.txt导出txt2.Location = new System.Drawing.Point(588, 55);
            this.txt导出txt2.Name = "txt导出txt2";
            this.txt导出txt2.Size = new System.Drawing.Size(255, 25);
            this.txt导出txt2.TabIndex = 11;
            // 
            // txt导入txt
            // 
            this.txt导入txt.Location = new System.Drawing.Point(28, 55);
            this.txt导入txt.Name = "txt导入txt";
            this.txt导入txt.Size = new System.Drawing.Size(554, 25);
            this.txt导入txt.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(861, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "执行";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txt导出txt
            // 
            this.txt导出txt.Location = new System.Drawing.Point(28, 24);
            this.txt导出txt.Name = "txt导出txt";
            this.txt导出txt.Size = new System.Drawing.Size(815, 25);
            this.txt导出txt.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 96);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbMess);
            this.splitContainer1.Size = new System.Drawing.Size(1058, 354);
            this.splitContainer1.SplitterDistance = 576;
            this.splitContainer1.TabIndex = 7;
            // 
            // rtbMess
            // 
            this.rtbMess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMess.Location = new System.Drawing.Point(0, 0);
            this.rtbMess.Name = "rtbMess";
            this.rtbMess.Size = new System.Drawing.Size(478, 354);
            this.rtbMess.TabIndex = 6;
            this.rtbMess.Text = "";
            // 
            // Form数据处理_批量导出列别名EXCE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form数据处理_批量导出列别名EXCE";
            this.Text = "Form数据处理_批量导出列别名EXCE";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt导出txt2;
        private System.Windows.Forms.TextBox txt导入txt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt导出txt;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbMess;
    }
}