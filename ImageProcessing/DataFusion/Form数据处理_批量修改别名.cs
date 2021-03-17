using ESRI.ArcGIS.Geodatabase;
using HelperWindowsControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing.DataFusion
{
    public partial class Form数据处理_批量修改别名 : Form
    {
        HelperMessRecord Mess;
        HelperControlRecord helperControlRecord;
        const string fromname = "数据处理_批量修改别名";
        const string fromlevel = " v1.0";

        public Form数据处理_批量修改别名()
        {
            InitializeComponent();

            HelperMainWindows.SetICO(this);
            Mess = new HelperMessRecord(this, rtbMess);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        private void Form数据处理_批量修改别名_Load(object sender, EventArgs e)
        {
            helperControlRecord.Load();
        }

        string[] exelist = null;
        private void button1_Click(object sender, EventArgs e)
        {
            exelist = this.richTextBox1.Lines;
            helperControlRecord.Save();
            Task.Factory.StartNew(run);
            //run();
        }

       

        void run()
        {
            Mess.Clear();
            Mess.Record("开始");

            string mess = "";
            bool isok = false;

            try
            {
                Dictionary<string, string[]> strleng = new Dictionary<string, string[]>();

                for (int i = 0; i < exelist.Length; i++)
                {
                    string[] s = exelist[i].Split('\t');
                    if (s.Length >= 3)
                    {
                        string datasetname = s[0].Trim();
                        string featurename = s[1].Trim();
                        string aliasname = s[2].Trim();

                        if (string.IsNullOrWhiteSpace(datasetname) || string.IsNullOrWhiteSpace(featurename) || string.IsNullOrWhiteSpace(aliasname))
                        {
                            Mess.Record($"数据错误:{datasetname},{featurename},{aliasname}");
                            continue;
                        }

                        if (!strleng.ContainsKey(featurename))
                        {
                            strleng.Add(featurename, new string[3] { datasetname, featurename, aliasname });
                            Mess.Record($"读取到数据:{datasetname},{featurename},{aliasname}");
                        }
                    }
                }

                Mess.Record("连接数据库..");
                IWorkspace workspace = ConnectSDE();
                if (workspace == null) Mess.Record("连接数据库失败！");
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                foreach (KeyValuePair<string, string[]> kvp in strleng)
                {
                    string datasetname = kvp.Value[0].Trim();
                    string featurename = kvp.Value[1].Trim();
                    string aliasname = kvp.Value[2].Trim();

                    try
                    {
                        IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(datasetname);
                        IFeatureClassContainer ipFcContain = (IFeatureClassContainer)featureDataset;
                        IFeatureClass featureClass = ipFcContain.ClassByName[featurename];
                        if (featureClass == null) { Mess.Record($"失败：{aliasname}"); }
                        else
                        {
                            AlterAliasName(featureClass, aliasname);
                            Mess.Record($"修改别名成功：{aliasname}");
                        }
                        if (featureDataset != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataset);
                        if (featureClass != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    }
                    catch (Exception ex1)
                    {
                        Mess.Record($"{aliasname}错误：" + ex1.Message);
                    }
                }

                if (workspace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace);
                Mess.Record("完成！！！");
                //string datasetname = "XZCH_520100_XZQH";
                //string featurename = "XZCH_520100_ZXCQ";
                //string aliasname = "行政区划_中心城区";

                //IWorkspace workspace = ConnectSDE();
                //IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                //IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(datasetname);
                //IFeatureClassContainer ipFcContain = (IFeatureClassContainer)featureDataset;
                //IFeatureClass featureClass = ipFcContain.ClassByName[featurename];
                //if (featureClass == null) { Mess.Record($"打开失败：{featurename}"); }
                //else {
                //    AlterAliasName(featureClass, aliasname);
                //    Mess.Record($"修改别名成功：{featurename}");
                //}
                //if (workspace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace);
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
        }

        public static IWorkspace ConnectSDE()
        {
            IWorkspaceFactory2 sdeFac = new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass();
            //定义一个属性
            ESRI.ArcGIS.esriSystem.IPropertySet Propset = new ESRI.ArcGIS.esriSystem.PropertySetClass();
            //设置数据库服务器名
            Propset.SetProperty("SERVER", "192.168.51.122");
            Propset.SetProperty("INSTANCE", "sde:postgresql:192.168.51.122"); //sde: oracle11g: 127.0.0.1 / orcl
            Propset.SetProperty("USER", "sde");//SDE的用户名
            Propset.SetProperty("PASSWORD", "sde");//密码
            Propset.SetProperty("Database", "dcgis");
            //SDE的版本,在这为默认版本
            Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
            Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");

            IWorkspace workspace = sdeFac.Open(Propset, 0);
            return workspace;
        }


        /// <summary>
        /// 修改数据集别名
        /// </summary>
        /// <param name="objectClass">对象类</param>
        public void AlterAliasName(IObjectClass objectClass, string aliasName)
        {
            //cast for the IClassSchemaEdit
            IClassSchemaEdit pOcSchemaEdit = objectClass as IClassSchemaEdit;

            //set and exclusive lock on the class 设置并独占锁
            //ISchemaLock schemaLock = (ISchemaLock)objectClass;
            //schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

            //alter the class extension for the class
            pOcSchemaEdit.AlterAliasName(aliasName);

            //release the exclusive lock 释放锁
            //schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.rtbMess.Clear();
            string headstr = this.txt前缀.Text.Trim();
            string[] strs = this.richTextBox1.Lines;
            for (int i = 0; i < strs.Length; i++)
            {
                string str = strs[i].Trim();
                if (string.IsNullOrWhiteSpace(str)) { this.rtbMess.AppendText("\r"); }
                else
                {
                    int index = str.IndexOf('(');
                    if (index >= 0) str = str.Substring(0, index);
                    this.rtbMess.AppendText(headstr + str + "\r");

                }
            }
        }
    }
}
