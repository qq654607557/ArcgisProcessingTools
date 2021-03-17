using ESRI.ArcGIS.Carto;
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
    public partial class Form数据处理_批量导出数据 : Form
    {
        HelperMessRecord Mess;
        HelperControlRecord helperControlRecord;
        const string fromname = "数据处理_批量导出数据";
        const string fromlevel = " v1.0 20201109 10.15.33.25(XZGHSDE)";

        public Form数据处理_批量导出数据()
        {
            InitializeComponent();

            HelperMainWindows.SetICO(this);
            Mess = new HelperMessRecord(this, rtbMess);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        private void Form数据处理_批量导出数据_Load(object sender, EventArgs e)
        {
            helperControlRecord.Load();
        }

        string strinput, strinput2, stroutput, stroutput2;
        string[] strlist, strlist2;
        private void button1_Click(object sender, EventArgs e)
        {
            strinput = this.txt导出txt.Text.Trim();
            strinput2 = this.txt导出txt2.Text.Trim();
            stroutput = this.txt导入txt.Text.Trim();
            //stroutput2 = this.txt导入txt2.Text.Trim();
            strlist = this.richTextBox1.Lines;
            strlist2 = this.richTextBox2.Lines;
            helperControlRecord.Save();
            Task.Factory.StartNew(run);
            //run();
            //IWorkspace workspace = ConnectSDE();
        }

        public static IWorkspace ConnectSDE()
        {
            IWorkspaceFactory2 sdeFac = new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass();
            //定义一个属性
            ESRI.ArcGIS.esriSystem.IPropertySet Propset = new ESRI.ArcGIS.esriSystem.PropertySetClass();
            //设置数据库服务器名
            Propset.SetProperty("SERVER", "10.15.33.25");
            Propset.SetProperty("INSTANCE", "sde:sqlserver:10.15.33.25"); //sde: oracle11g: 127.0.0.1 / orcl
            Propset.SetProperty("USER", "sde");//SDE的用户名
            Propset.SetProperty("PASSWORD", "sde");//密码
            Propset.SetProperty("Database", "XZGHSDE");
            //SDE的版本,在这为默认版本
            Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
            Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");

            IWorkspace workspace = sdeFac.Open(Propset, 0);
            return workspace;
        }

        void run()
        {
            Mess.Clear();
            Mess.Record("开始");

            string mess = "";
            bool isok = false;

            Dictionary<string, string> outtablenames = new Dictionary<string, string>();
            //List<string> outtablenames = new List<string>();
            if (strlist2.Length == strlist.Length) { Mess.Record("启用改名");
                for (int i = 0; i < strlist.Length; i++)
                {
                    if (strlist[i].Trim().Length <= 0) continue;
                    if (!outtablenames.Keys.Contains(strlist[i].Trim())) outtablenames.Add(strlist[i].Trim(), strlist2[i].Trim());
                }
            }
            else
            {
                for (int i = 0; i < strlist.Length; i++)
                {
                    if (strlist[i].Trim().Length <= 0) continue;
                    if (!outtablenames.Keys.Contains(strlist[i].Trim())) outtablenames.Add(strlist[i].Trim(), strlist[i].Trim());
                }
            }


                try
            {
                IWorkspace workspace = ConnectSDE();
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                IDatasetName datasetName = null;
                List<string> datasetNames = new List<string>();
                while ((datasetName=enumDatasetName.Next())!=null) datasetNames.Add(datasetName.Name.ToUpper());
                Mess.Record($"读取记录集：{datasetNames.Count.ToString()}条");

                foreach(KeyValuePair<string,string> kvp in outtablenames)
                {

                    string featurename = kvp.Key;
                    try
                    {
                        Mess.Record($"执行：{featurename}->{kvp.Value}");
                        string[] fs = featurename.Split('.');
                        featurename = fs[fs.Length - 1];

                        string datasetname = "";
                        if (datasetNames.Contains((strinput2 + featurename.Substring(0, 2)).ToUpper())) { datasetname = strinput2 + featurename.Substring(0, 2); }
                        else if (datasetNames.Contains((strinput2 + featurename.Substring(0, 4)).ToUpper())) { datasetname = strinput2 + featurename.Substring(0, 4); }
                        else if (datasetNames.Contains((strinput2 + featurename.Substring(0, 6)).ToUpper())) { datasetname = strinput2 + featurename.Substring(0, 6); }
                        else { Mess.Record($"没有找到：{featurename}"); continue; }

                        IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(datasetname);
                        IFeatureClassContainer ipFcContain = (IFeatureClassContainer)featureDataset;
                        IFeatureClass featureClass = ipFcContain.ClassByName[featurename];
                        if (featureClass == null) { Mess.Record($"打开失败：{featurename}"); continue; }
                        isok = HelperArcGIS.PGTool.GPConversionTools.FeatureClassToFeatureClass(ref mess, featureClass, stroutput, kvp.Value);

                        if (isok) Mess.Record($"{featurename} 成功！");
                        else Mess.Record($"{featurename} 失败：{mess}");
                    }
                    catch (Exception exc)
                    {
                        Mess.Record($"{featurename} 错误：{exc.Message}");
                    }
                }

                Mess.Record("完成！");
               
                if (workspace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace); 
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
        }

        public void test(IWorkspace pWorkspace)
        {
            IEnumDataset pEnumDataset = pWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTAny);
            pEnumDataset.Reset();
            IDataset pDataset = null;
            while ((pDataset = pEnumDataset.Next()) != null)
            {

                if (pDataset is IFeatureDataset)
                {
                    //IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                    //IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(pDataset.Name);
                    //IEnumDataset pEnumDataset1 = pFeatureDataset.Subsets;
                    //pEnumDataset1.Reset();
                    Mess.Record("D:" + pDataset.Name);
                    //IFeatureDataset pFeatureDataset = pDataset as IFeatureDataset;
                    //IEnumDataset pEnumDataset1 = pFeatureDataset.Subsets;
                    //IDataset pDataset1 = pEnumDataset1.Next();

                    //if (pDataset1 is IFeatureClass)
                    //{
                    //    //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                    //    //pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset1.Name);
                    //    //pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                    //    Mess.Record("L:"+pDataset1.Name);
                    //}
                }
                else
                {
                    //IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                    //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                    //pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                    //pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                    Mess.Record("L:" + pDataset.Name);
                }
            }
        }
    }
}
