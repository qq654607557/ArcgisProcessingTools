using ImageProcessing.DataFusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 数据融合_图层交集与保留
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

            string mess = "";
            bool isrun = true;
            isrun = HelperArcGIS.LicenseRun.Run(ref mess);
            if (!isrun) { MessageBox.Show(mess); return; }

            Application.Run(new Form数据融合_图层交集与保留());
        }
    }
}
