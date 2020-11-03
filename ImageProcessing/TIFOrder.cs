using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
   public class TIFOrder
    {
       
        private string path = "";
        private string[] str;
        public string[] Order
        {

            get
            {
                if(str==null)
                {
                    HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
                    str= txt.ReadTxt(path).ToArray();
                }
                return str;
            }
        }

        public TIFOrder(string path)
        {
            this.path = path;
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            str = txt.ReadTxt(path).ToArray();
        }
    }
}
