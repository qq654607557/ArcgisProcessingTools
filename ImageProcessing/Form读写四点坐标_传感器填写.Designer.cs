namespace ImageProcessing
{
    partial class Form读写四点坐标_传感器填写
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
            this.txt_WXGDH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_JH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_CGQLX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_WXGDH
            // 
            this.txt_WXGDH.Location = new System.Drawing.Point(70, 41);
            this.txt_WXGDH.Name = "txt_WXGDH";
            this.txt_WXGDH.Size = new System.Drawing.Size(404, 21);
            this.txt_WXGDH.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 61;
            this.label6.Text = "WXGDH：";
            // 
            // txt_JH
            // 
            this.txt_JH.Location = new System.Drawing.Point(70, 12);
            this.txt_JH.Name = "txt_JH";
            this.txt_JH.Size = new System.Drawing.Size(404, 21);
            this.txt_JH.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 59;
            this.label1.Text = "JH：";
            // 
            // txt_CGQLX
            // 
            this.txt_CGQLX.Location = new System.Drawing.Point(70, 68);
            this.txt_CGQLX.Name = "txt_CGQLX";
            this.txt_CGQLX.Size = new System.Drawing.Size(404, 21);
            this.txt_CGQLX.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 63;
            this.label2.Text = "CGQLX：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(395, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 65;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form读写四点坐标_传感器填写
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 131);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_CGQLX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_WXGDH);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_JH);
            this.Controls.Add(this.label1);
            this.Name = "Form读写四点坐标_传感器填写";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form读写四点坐标_传感器填写";
            this.Load += new System.EventHandler(this.Form读写四点坐标_传感器填写_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_WXGDH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_JH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_CGQLX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}