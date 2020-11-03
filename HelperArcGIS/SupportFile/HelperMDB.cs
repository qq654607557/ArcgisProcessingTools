using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;

namespace HelperArcGIS.SupportFile
{
   public class HelperMDB
    {
        public static IWorkspace Open(string path)
        {
            IPropertySet propset = new PropertySetClass();
            propset.SetProperty("DATABASE",path);

            IWorkspaceFactory fact = new AccessWorkspaceFactoryClass();
            IWorkspace pWorkspace = fact.Open(propset, 0);
            return pWorkspace;
        }
    }
}
