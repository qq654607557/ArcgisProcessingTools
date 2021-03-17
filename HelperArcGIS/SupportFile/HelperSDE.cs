using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS.SupportFile
{
    public class HelperSDE
    {
        /// <summary>
        /// 连接SDE
        /// </summary>
        private void ConnectSDE()
        {
            IWorkspaceFactory2 sdeFac = new SdeWorkspaceFactoryClass();
            IPropertySet propertySet = GetProSet(false);
            IWorkspace workspace = sdeFac.Open(propertySet, 0);
        }

        /// <summary>
        /// 设置SDE连接属性
        /// </summary>
        /// <param name="ChkSdeLinkModle"></param>
        /// <returns></returns>
        public static IPropertySet GetProSet(bool ChkSdeLinkModle)
        {
            //定义一个属性
            IPropertySet Propset = new PropertySetClass();
            if (ChkSdeLinkModle == true) //采用SDE连接
            {
                //设置数据库服务器名
                //Propset.SetProperty("SERVER", "");

                Propset.SetProperty("INSTANCE", "sde:oracle11g:LCSDE"); //sde: oracle11g: 127.0.0.1 / orcl
                Propset.SetProperty("USER", "SDE");//SDE的用户名
                Propset.SetProperty("PASSWORD", "sde");//密码

                //设置数据库的名字,通过直连或者服务连Oracle，连接参数Database都可以为空；通过服务连SQL SEVER时，连接参数Database可以为空，但直连时不能为空,Informi需要设置
                //Propset.SetProperty("DATABASE", "orcl");
                //SDE的版本,在这为默认版本
                //Propset.SetProperty("VERSION", "SDE.DEFAULT");
            }
            else//弹窗连接
            {
                //设置数据库服务器名,如果是本机可以用"sde:oracle:.",会弹出选择数据库对话框，要求填入用户名密码。
                Propset.SetProperty("INSTANCE", "sde:oracle:LCSDE");
            }
            return Propset;
        }

        /// <summary>
        /// 设置SDE连接属性
        /// </summary>
        /// <param name="ChkSdeLinkModle"></param>
        /// <returns></returns>
        public static IWorkspace ConnectSDE(string INSTANCE, string USER, string PASSWORD, string strDatabaseName)
        {
            IWorkspaceFactory2 sdeFac = new SdeWorkspaceFactoryClass();
            //定义一个属性
            IPropertySet Propset = new PropertySetClass();
            //设置数据库服务器名
            Propset.SetProperty("INSTANCE", "10.15.33.12"); //sde: oracle11g: 127.0.0.1 / orcl
            Propset.SetProperty("USER", "sde");//SDE的用户名
            Propset.SetProperty("PASSWORD", "sde");//密码
            Propset.SetProperty("Database", "GYSDE2000");
            //SDE的版本,在这为默认版本
            Propset.SetProperty("VERSION", "SDE.DEFAULT");

            IWorkspace workspace = sdeFac.Open(Propset, 0);
            return workspace;
        }

        /// <summary>
        /// 从workspace中获取连接信息
        /// </summary>
        private void GetProFromWk(IWorkspace workspace)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            object names, values;
            workspace.ConnectionProperties.GetAllProperties(out names, out values);

            foreach (string name in (string[])names)
            {
                dic.Add(name, workspace.ConnectionProperties.GetProperty(name));
            }
        }
    }
}
