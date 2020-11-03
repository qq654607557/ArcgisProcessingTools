namespace ImageProcessing
{
    partial class Form批量切片服务
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
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn开始运行 = new System.Windows.Forms.Button();
            this.txt_shp文件夹 = new System.Windows.Forms.TextBox();
            this.txt_保存文件 = new System.Windows.Forms.TextBox();
            this.txt_服务路径 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbRecord
            // 
            this.rtbRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRecord.Location = new System.Drawing.Point(0, 122);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(688, 422);
            this.rtbRecord.TabIndex = 73;
            this.rtbRecord.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn开始运行);
            this.panel1.Controls.Add(this.txt_shp文件夹);
            this.panel1.Controls.Add(this.txt_保存文件);
            this.panel1.Controls.Add(this.txt_服务路径);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 122);
            this.panel1.TabIndex = 74;
            // 
            // btn开始运行
            // 
            this.btn开始运行.Location = new System.Drawing.Point(601, 93);
            this.btn开始运行.Name = "btn开始运行";
            this.btn开始运行.Size = new System.Drawing.Size(75, 23);
            this.btn开始运行.TabIndex = 79;
            this.btn开始运行.Text = "开始运行";
            this.btn开始运行.UseVisualStyleBackColor = true;
            this.btn开始运行.Click += new System.EventHandler(this.btn开始运行_Click);
            // 
            // txt_shp文件夹
            // 
            this.txt_shp文件夹.Location = new System.Drawing.Point(88, 67);
            this.txt_shp文件夹.Name = "txt_shp文件夹";
            this.txt_shp文件夹.Size = new System.Drawing.Size(588, 21);
            this.txt_shp文件夹.TabIndex = 78;
            // 
            // txt_保存文件
            // 
            this.txt_保存文件.Location = new System.Drawing.Point(88, 39);
            this.txt_保存文件.Name = "txt_保存文件";
            this.txt_保存文件.Size = new System.Drawing.Size(588, 21);
            this.txt_保存文件.TabIndex = 76;
            // 
            // txt_服务路径
            // 
            this.txt_服务路径.Location = new System.Drawing.Point(88, 12);
            this.txt_服务路径.Name = "txt_服务路径";
            this.txt_服务路径.Size = new System.Drawing.Size(588, 21);
            this.txt_服务路径.TabIndex = 74;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 77;
            this.label9.Text = "shp文件夹：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 75;
            this.label8.Text = "保存文件：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 73;
            this.label7.Text = "服务路径：";
            // 
            // Form批量切片服务
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 544);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.panel1);
            this.Name = "Form批量切片服务";
            this.Text = "批量切片服务";
            this.Load += new System.EventHandler(this.Form批量切片服务_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn开始运行;
        private System.Windows.Forms.TextBox txt_shp文件夹;
        private System.Windows.Forms.TextBox txt_保存文件;
        private System.Windows.Forms.TextBox txt_服务路径;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}