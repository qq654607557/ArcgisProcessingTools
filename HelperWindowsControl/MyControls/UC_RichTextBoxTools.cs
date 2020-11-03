using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl.MyControls
{
    public partial class UC_RichTextBoxTools : UserControl
    {
        public UC_RichTextBoxTools()
        {
            InitializeComponent();
            this.richTextBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RichTextBox1_MouseUp);
            this.richTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RichTextBox1_KeyUp);
        }

        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Ranks();
        }

        private void RichTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            this.Ranks();
        }

        /// <summary>自定义方法 -- 
        ///  获取文本中(行和列)--光标--坐标位置的调用方法
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        private void Ranks()
        {
            /*  得到光标行第一个字符的索引，
             *  即从第1个字符开始到光标行的第1个字符索引*/
            int index = richTextBox1.GetFirstCharIndexOfCurrentLine();
            /*得到光标行的行号,第1行从0开始计算，习惯上我们是从1开始计算，所以+1。 */
            int line = richTextBox1.GetLineFromCharIndex(index) + 1;
            /*  SelectionStart得到光标所在位置的索引
             *  再减去
             *  当前行第一个字符的索引
             *  = 光标所在的列数(从0开始)  */
            int column = richTextBox1.SelectionStart - index + 1;

            // 
            int select = richTextBox1.SelectionLength;

            this.lab选择显示.Text = string.Format("第：{0}行 {1}列(已选择{2})", line.ToString(), column.ToString(), select.ToString());

        }
    }
}
