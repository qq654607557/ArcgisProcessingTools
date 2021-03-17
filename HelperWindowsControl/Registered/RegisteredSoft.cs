using HelperClass.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelperWindowsControl.Registered
{
   public static class RegisteredSoft
    {
        static RegisterClass registerClass = new RegisterClass();
        static string code = null;
        public static bool Run(string softname,Form form,string pid)
        {
            if (code is null) code = registerClass.GetCode(registerClass.CreateCode(pid));
            bool isok = registerClass.BoolRegist(code);
            if (isok) { form.Text += " 已注册"; return true; }

            FormRegistered formRegistered = new FormRegistered(softname,pid);
            formRegistered.ShowDialog();
            isok = registerClass.BoolRegist(code);
            if (isok) { form.Text += " 已注册"; return true; }
            form.Text += " 未注册";
            return false;
        }
    }
}
