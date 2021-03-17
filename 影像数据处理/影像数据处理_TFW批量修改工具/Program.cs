using DocumentProcessing.影像数据处理;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 影像数据处理_TFW批量修改工具
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form影像数据处理_XML生成工具());
        }
    }
}
