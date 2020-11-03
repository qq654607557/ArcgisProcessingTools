
namespace ARCGIS小工具
{
    partial class FormMain
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
            this.btn数据融合_图层交集与保留 = new System.Windows.Forms.Button();
            this.gb数据融合 = new System.Windows.Forms.GroupBox();
            this.flp数据融合 = new System.Windows.Forms.FlowLayoutPanel();
            this.uC_RichTextBoxTools1 = new HelperWindowsControl.MyControls.UC_RichTextBoxTools();
            this.gb数据融合.SuspendLayout();
            this.flp数据融合.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn数据融合_图层交集与保留
            // 
            this.btn数据融合_图层交集与保留.Location = new System.Drawing.Point(3, 3);
            this.btn数据融合_图层交集与保留.Name = "btn数据融合_图层交集与保留";
            this.btn数据融合_图层交集与保留.Size = new System.Drawing.Size(145, 88);
            this.btn数据融合_图层交集与保留.TabIndex = 0;
            this.btn数据融合_图层交集与保留.Text = "图层交集与保留";
            this.btn数据融合_图层交集与保留.UseVisualStyleBackColor = true;
            this.btn数据融合_图层交集与保留.Click += new System.EventHandler(this.btn数据融合_图层交集与保留_Click);
            // 
            // gb数据融合
            // 
            this.gb数据融合.Controls.Add(this.flp数据融合);
            this.gb数据融合.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb数据融合.Location = new System.Drawing.Point(0, 0);
            this.gb数据融合.Name = "gb数据融合";
            this.gb数据融合.Size = new System.Drawing.Size(811, 252);
            this.gb数据融合.TabIndex = 1;
            this.gb数据融合.TabStop = false;
            this.gb数据融合.Text = "数据融合";
            // 
            // flp数据融合
            // 
            this.flp数据融合.Controls.Add(this.btn数据融合_图层交集与保留);
            this.flp数据融合.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp数据融合.Location = new System.Drawing.Point(3, 21);
            this.flp数据融合.Name = "flp数据融合";
            this.flp数据融合.Size = new System.Drawing.Size(805, 228);
            this.flp数据融合.TabIndex = 0;
            // 
            // uC_RichTextBoxTools1
            // 
            this.uC_RichTextBoxTools1.Location = new System.Drawing.Point(47, 277);
            this.uC_RichTextBoxTools1.Name = "uC_RichTextBoxTools1";
            this.uC_RichTextBoxTools1.Size = new System.Drawing.Size(687, 322);
            this.uC_RichTextBoxTools1.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 643);
            this.Controls.Add(this.uC_RichTextBoxTools1);
            this.Controls.Add(this.gb数据融合);
            this.Name = "FormMain";
            this.Text = "所有工具";
            this.gb数据融合.ResumeLayout(false);
            this.flp数据融合.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn数据融合_图层交集与保留;
        private System.Windows.Forms.GroupBox gb数据融合;
        private System.Windows.Forms.FlowLayoutPanel flp数据融合;
        private HelperWindowsControl.MyControls.UC_RichTextBoxTools uC_RichTextBoxTools1;
    }
}

