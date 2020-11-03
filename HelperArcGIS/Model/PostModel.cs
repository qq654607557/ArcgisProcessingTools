using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.Model
{
    public class PostitionModel
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public PostitionModel()
        { }

        public PostitionModel(decimal x, decimal y)
        { this.X = x; this.Y = y; }

        public PostitionModel(string x, string y)
        { this.X = decimal.Parse(x); this.Y = decimal.Parse(y); }
    }
}
