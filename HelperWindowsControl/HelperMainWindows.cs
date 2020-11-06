using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl
{
   public class HelperMainWindows
    {
        public static void SetICO(Form form)
        {
            form.Icon = Properties.Resources.ICO32;
        }
    }
}
