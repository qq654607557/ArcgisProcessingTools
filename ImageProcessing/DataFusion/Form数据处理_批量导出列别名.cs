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
    public partial class Form数据处理_批量导出列别名 : Form
    {
        HelperMessRecord Mess;
        HelperControlRecord helperControlRecord;
        const string fromname = "数据处理_批量导出列别名";
        const string fromlevel = " v1.0";

        string strDBName = "";
        int strIndex = 0;

        public Form数据处理_批量导出列别名()
        {
            InitializeComponent();

            HelperMainWindows.SetICO(this);
            Mess = new HelperMessRecord(this, rtbMess);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        private void Form数据处理_批量导出列别名_Load(object sender, EventArgs e)
        {
            helperControlRecord.Load();
        }

        string[] exelist = null;
        private void button1_Click(object sender, EventArgs e)
        {
            //exelist = this.richTextBox1.Lines;
            //helperControlRecord.Save();
            //Task.Factory.StartNew(run1);
            //run();
        }

        void run1()
        {
            Mess.Clear();
            Mess.Record("开始");

            string mess = "";
            bool isok = false;

            try
            {
                string featurename_old = "BA000000_XZCQ_PY";
                string datasetname = "XZCH_520100_XZQH";
                string featurename = "XZCH_520100_ZXCQ";
                string aliasname = "行政区划_中心城区_中心城区";

                //Mess.Record("连接数据库..");
                //IWorkspace workspace = ConnectSDE();
                //if (workspace == null) Mess.Record("连接数据库失败！");
                //IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;



                //IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(datasetname);
                //IFeatureClassContainer ipFcContain = (IFeatureClassContainer)featureDataset;
                //IFeatureClass featureClass = ipFcContain.ClassByName[featurename];
                //if (featureClass == null) { Mess.Record($"失败：{aliasname}"); }

                //Mess.Record($"{featurename}");
                //IFields fields = featureClass.Fields;
                //for (int f = 0; f < fields.FieldCount; f++)
                //{
                //    IField field = fields.Field[f];
                //    Mess.Record($"列：{field.Name},{field.AliasName},{field.Type.ToString()}");
                //}

                Mess.Record("连接数据库..");
                IWorkspace workspace_sql = ConnectSDE_SQL();
                if (workspace_sql == null) Mess.Record("连接数据库失败！");
                IFeatureWorkspace featureWorkspace_sql = (IFeatureWorkspace)workspace_sql;
                List<string> datasetNames_sql = datasetNames_List(workspace_sql);
                if (datasetNames_sql == null) { Mess.Record($"不能读取数据集"); return; }
                string datasetname_old = getDatasetName(datasetNames_sql, featurename_old);
                if (datasetname_old == null) { Mess.Record($"没有找到数据集：{featurename_old}"); return; }

                IFeatureDataset featureDataset_old = featureWorkspace_sql.OpenFeatureDataset(datasetname_old);
                IFeatureClassContainer ipFcContain_old = (IFeatureClassContainer)featureDataset_old;
                IFeatureClass featureClass_old = ipFcContain_old.ClassByName[featurename_old];
                if (featureClass_old == null) { Mess.Record($"失败：{featurename_old}"); }
                //featureClass_old.AliasName=
                Mess.Record($"{featurename_old}");
                IFields fields_old = featureClass_old.Fields;
                for (int f = 0; f < fields_old.FieldCount; f++)
                {
                    IField field_old = fields_old.Field[f];
                    Mess.Record($"列：{field_old.Name},{field_old.AliasName},{field_old.Type.ToString()}");
                }


                Mess.Record("完成！！！");
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
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
            IClassSchemaEdit pOcSchemaEdit = objectClass as IClassSchemaEdit;
            pOcSchemaEdit.AlterAliasName(aliasName);
        }


        public IWorkspace ConnectSDE_SQL()
        {
            IWorkspaceFactory2 sdeFac = new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass();
            //定义一个属性
            ESRI.ArcGIS.esriSystem.IPropertySet Propset = new ESRI.ArcGIS.esriSystem.PropertySetClass();
            switch (strIndex)
            {
                case 1:
                    //设置数据库服务器名
                    Propset.SetProperty("SERVER", "10.15.33.12");
                    Propset.SetProperty("INSTANCE", "sde:sqlserver:10.15.33.12");
                    Propset.SetProperty("USER", "sde");//SDE的用户名
                    Propset.SetProperty("PASSWORD", "sde");//密码
                    Propset.SetProperty("Database", "GYSDE2000");
                    //Propset.SetProperty("Database", "GYZGZT2014"); 
                    //SDE的版本,在这为默认版本
                    Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
                    Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");
                    strDBName = "GYSDE2000.SDE.";
                    break;
                case 2:

                    //设置数据库服务器名
                    Propset.SetProperty("SERVER", "10.15.33.25");
                    Propset.SetProperty("INSTANCE", "sde:sqlserver:10.15.33.25");
                    Propset.SetProperty("USER", "sde");//SDE的用户名
                    Propset.SetProperty("PASSWORD", "sde");//密码
                    Propset.SetProperty("Database", "XZGHSDE");
                    //SDE的版本,在这为默认版本
                    Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
                    Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");
                    strDBName = "XZGHSDE.SDE.";
                    break;
                case 3:
                    //设置数据库服务器名
                    Propset.SetProperty("SERVER", "10.15.33.12");
                    Propset.SetProperty("INSTANCE", "sde:sqlserver:10.15.33.12");
                    Propset.SetProperty("USER", "sde");//SDE的用户名
                    Propset.SetProperty("PASSWORD", "sde");//密码
                    Propset.SetProperty("Database", "GYZGZT2014");
                    //SDE的版本,在这为默认版本
                    Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
                    Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");
                    strDBName = "GYZGZT2014.SDE.";
                    break;
                case 4:
                    //设置数据库服务器名
                    Propset.SetProperty("SERVER", "192.168.51.122");
                    Propset.SetProperty("INSTANCE", "sde:postgresql:192.168.51.122"); //sde: oracle11g: 127.0.0.1 / orcl
                    Propset.SetProperty("USER", "sde");//SDE的用户名
                    Propset.SetProperty("PASSWORD", "sde");//密码
                    Propset.SetProperty("Database", "dcgis");
                    //SDE的版本,在这为默认版本
                    Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
                    Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");
                    break;
                default:
                    break;
            }

            //设置数据库服务器名
            //Propset.SetProperty("SERVER", "10.15.33.25");
            //Propset.SetProperty("INSTANCE", "sde:sqlserver:10.15.33.25"); 
            //Propset.SetProperty("USER", "sde");//SDE的用户名
            //Propset.SetProperty("PASSWORD", "sde");//密码
            //Propset.SetProperty("Database", "XZGHSDE");
            ////SDE的版本,在这为默认版本
            //Propset.SetProperty("VERSION", "SDE.DEFAULT");// DBO
            //Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");



            IWorkspace workspace = sdeFac.Open(Propset, 0);
            return workspace;
        }

        List<string> datasetNames_List(IWorkspace workspace)
        {

            try
            {
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                IDatasetName datasetName = null;
                List<string> datasetNames = new List<string>();
                while ((datasetName = enumDatasetName.Next()) != null) datasetNames.Add(datasetName.Name.ToUpper());
                return datasetNames;
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
                return null;
            }

        }

        string getDatasetName(List<string> datasetNames, string featurename)
        {
            string[] fs = featurename.Split('.');
            featurename = fs[fs.Length - 1];
            string datasetname = "";

            string strinput2 = strDBName;
            switch (strIndex)
            {
                case 3:
                    strinput2 = "GYZGZT2014.SDE.";
                    string[] s = featurename.Split('_');
                    string sn = s[0] + "_" + s[1];
                    if (datasetNames.Contains(strinput2 + sn.ToUpper())) { datasetname = strinput2 + sn; }
                    break;
                default:
                    // "GYSDE2000.SDE.";// "GYZGZT2014.SDE.";// "XZGHSDE.SDE.";// "GYSDE2000.SDE.";
                    if (datasetNames.Contains((strinput2 + featurename.Substring(0, 2)).ToUpper())) { datasetname = strinput2 + featurename.Substring(0, 2); }
                    else if (datasetNames.Contains((strinput2 + featurename.Substring(0, 4)).ToUpper())) { datasetname = strinput2 + featurename.Substring(0, 4); }
                    else if (datasetNames.Contains((strinput2 + featurename.Substring(0, 6)).ToUpper())) { datasetname = strinput2 + featurename.Substring(0, 6); }
                    else { datasetname = null; }
                    break;
            }

            return datasetname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strIndex = 1;
            exelist = this.richTextBox1.Lines;
            helperControlRecord.Save();
            Task.Factory.StartNew(run2);
        }

        void run2()
        {
            Mess.Clear();
            Mess.Record("开始");

            string mess = "";
            bool isok = false;

            try
            {
                Mess.Record("连接数据库..");
                IWorkspace workspace_sql = ConnectSDE_SQL();
                if (workspace_sql == null) Mess.Record("连接数据库失败！");
                IFeatureWorkspace featureWorkspace_sql = (IFeatureWorkspace)workspace_sql;
                List<string> datasetNames_sql = datasetNames_List(workspace_sql);
                if (datasetNames_sql == null) { Mess.Record($"不能读取数据集"); return; }

                for (int i = 0; i < exelist.Length; i++)
                {
                    string featurename_old = exelist[i].Trim();
                    if (string.IsNullOrWhiteSpace(featurename_old)) { Mess.RecordNODate($"{featurename_old}"); continue; }
                    try
                    {
                        string datasetname_old = getDatasetName(datasetNames_sql, featurename_old);
                        if (datasetname_old == null) { Mess.Record($"没有找到数据集：{featurename_old}"); continue; }

                        IFeatureDataset featureDataset_old = featureWorkspace_sql.OpenFeatureDataset(datasetname_old);
                        IFeatureClassContainer ipFcContain_old = (IFeatureClassContainer)featureDataset_old;
                        IFeatureClass featureClass_old = ipFcContain_old.ClassByName[featurename_old];
                        if (featureClass_old == null) { Mess.Record($"失败：{featurename_old}"); continue; }
                        Mess.RecordNODate($"{featureClass_old.AliasName}");
                    }
                    catch (Exception e)
                    {
                        Mess.RecordNODate($"{featurename_old}");
                    }
                }
                if (workspace_sql != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace_sql);
                Mess.Record("完成！！！");
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
        }

        string sjj = "", sjname = ""; string[] outinfo;
        void run3()
        {
            Mess.Clear();
            Mess.Record("开始");

            string mess = "";
            bool isok = false;

            try
            {
                Mess.Record("连接数据库..");
                IWorkspace workspace_sql = ConnectSDE_SQL();
                if (workspace_sql == null) Mess.Record("连接数据库失败！");
                IFeatureWorkspace featureWorkspace_sql = (IFeatureWorkspace)workspace_sql;
                List<string> datasetNames_sql = datasetNames_List(workspace_sql);
                if (datasetNames_sql == null) { Mess.Record($"不能读取数据集"); return; }

                IFeatureDataset featureDataset_old = featureWorkspace_sql.OpenFeatureDataset(sjj);
                IFeatureClassContainer ipFcContain_old = (IFeatureClassContainer)featureDataset_old;
                IFeatureClass featureClass_old = ipFcContain_old.ClassByName[sjname];
                if (featureClass_old == null) { Mess.Record($"失败：{sjname}"); return; }
                Mess.RecordNODate($"读取到表数据");

                DataTable dt = new DataTable("featureClass_old.AliasName");
                dt.Columns.Add("序号");
                dt.Columns.Add("列名");
                dt.Columns.Add("现有别名");
                dt.Columns.Add("要修改的别名");
                dt.Columns.Add("列类型");


                for (int i = 0; i < featureClass_old.Fields.FieldCount; i++)
                {
                    IField field = featureClass_old.Fields.Field[i];
                    dt.Rows.Add((i + 1).ToString(), field.Name, field.AliasName, "", field.Type);
                }
                string opname_path = System.Windows.Forms.Application.StartupPath + "\\EXCEL_COL";
                if (!System.IO.Directory.Exists(opname_path)) System.IO.Directory.CreateDirectory(opname_path);
                string path = opname_path + $"\\{sjj}__{sjname}.xls";
                dt.WriteXml(path);

                if (workspace_sql != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace_sql);
                Mess.Record("导出：" + path);
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
        }

        void run4()
        {
            Mess.Clear();
            Mess.Record("开始");

            string mess = "";
            bool isok = false;

            try
            {
                Mess.Record("连接数据库..");
                IWorkspace workspace_sql = ConnectSDE_SQL();
                if (workspace_sql == null) Mess.Record("连接数据库失败！");
                IFeatureWorkspace featureWorkspace_sql = (IFeatureWorkspace)workspace_sql;
                List<string> datasetNames_sql = datasetNames_List(workspace_sql);
                if (datasetNames_sql == null) { Mess.Record($"不能读取数据集"); return; }

                for (int i = 0; i < outinfo.Length; i++)
                {
                    string[] oi = outinfo[i].Split('\t');
                    if (oi.Length < 3) continue;
                    string t1 = oi[0].Trim(), t2 = oi[1].Trim(), t3 = oi[2].Trim();
                    if (string.IsNullOrWhiteSpace(t1) || string.IsNullOrWhiteSpace(t2)) continue;

                    Mess.RecordNODate($"{t1},{t2},{t3}");
                    try
                    {
                        IFeatureDataset featureDataset_old = featureWorkspace_sql.OpenFeatureDataset(t1);
                        IFeatureClassContainer ipFcContain_old = (IFeatureClassContainer)featureDataset_old;
                        IFeatureClass featureClass_old = ipFcContain_old.ClassByName[t2];

                        DataTable dt = new DataTable("featureClass_old.AliasName");
                        dt.Columns.Add("序号");
                        dt.Columns.Add("列名");
                        dt.Columns.Add("现有别名");
                        dt.Columns.Add("要修改的别名");
                        dt.Columns.Add("列类型");

                        for (int n = 0; n < featureClass_old.Fields.FieldCount; n++)
                        {
                            IField field = featureClass_old.Fields.Field[n];
                            dt.Rows.Add((n + 1).ToString(), field.Name, field.AliasName, "", field.Type);
                        }
                        string opname_path = System.Windows.Forms.Application.StartupPath + "\\EXCEL_COL\\" + t1;
                        if (!System.IO.Directory.Exists(opname_path)) System.IO.Directory.CreateDirectory(opname_path);
                        string path = opname_path + $"\\{t1}__{t2}__{t3}.xls";
                        dt.WriteXml(path);

                        Mess.Record("导出：" + path);
                    }
                    catch (Exception ex)
                    {
                        Mess.Record("错误：" + ex.Message);
                        Mess.RecordNODate($"错误：{t1},{t2},{t3}");
                    }
                }
                if (workspace_sql != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace_sql);
            }
            catch (Exception ex)
            {
                Mess.Record("错误：" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strIndex = 2;
            exelist = this.richTextBox1.Lines;
            helperControlRecord.Save();
            Task.Factory.StartNew(run2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            strIndex = 3;
            exelist = this.richTextBox1.Lines;
            helperControlRecord.Save();
            Task.Factory.StartNew(run2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex < 0) MessageBox.Show("选择服务器！");
            strIndex = this.comboBox1.SelectedIndex + 1;
            this.sjj = this.textBox1.Text.Trim();
            this.sjname = this.textBox2.Text.Trim();
            this.outinfo = this.richTextBox1.Lines;
            helperControlRecord.Save();
            Task.Factory.StartNew(run4);
        }
    }
}
