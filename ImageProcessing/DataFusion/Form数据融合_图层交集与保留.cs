using ArcGISTool;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using HelperArcGIS.SupportFile;
using HelperClass;
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
    public partial class Form数据融合_图层交集与保留 : System.Windows.Forms.Form
    {
        HelperMessRecord Mess;
        HelperControlRecord helperControlRecord;
       const string fromname = "数据融合_图层交集与保留";
        const string fromlevel = " v1.0 20201103";

        public Form数据融合_图层交集与保留()
        {
            InitializeComponent();
            Mess = new HelperMessRecord(this, richTextBox1);
            helperControlRecord = new HelperControlRecord(fromname);
            helperControlRecord.Add(this.groupBox1.Controls);
            this.Text = fromname + fromlevel;
        }

        string pathgdb, fname1, fname2;
        private void button1_Click(object sender, EventArgs e)
        {
            pathgdb = this.textBox1.Text.Trim();
            fname1 = this.textBox2.Text.Trim();
            fname2 = this.textBox3.Text.Trim();
            helperControlRecord.Save();
            Task.Factory.StartNew(run);
            //run();
        }

        void run()
        {
            Mess.Clear();
            Mess.Record("开始");
            try { 
            bool isok = true;
            string mess = "";
            ShapefileRead shapefileRead = new ShapefileRead();

            //string gdbpath = @"D:\项目\其他项目\公安中心建筑物面融合_花果园.gdb";
            IWorkspace workspace = HelperArcGIS.SupportFile.HelperGDB.OpenGDB(pathgdb);
            if (workspace == null) { Mess.Record("读取错误：" + pathgdb); isok = false; }
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            IFeatureClass featureClassA = featureWorkspace.OpenFeatureClass(fname1);// ("中心_花果园_1");//
            IFeatureClass featureClassB = featureWorkspace.OpenFeatureClass(fname2);// ("公安_花果园_重复");  //公安_花果园_重复
            IFeatureClass featureClassC = null;
            IFeatureClass featureClassD = null;
            string strcg = "成果" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try { featureClassC = featureWorkspace.OpenFeatureClass(strcg); } catch { featureClassC = null; }

            if (featureClassC == null)
            {
                featureClassC = featureWorkspace.CreateFeatureClass(strcg, featureClassA.Fields, featureClassA.CLSID, featureClassA.EXTCLSID,
                   esriFeatureType.esriFTSimple, featureClassA.ShapeFieldName, "");
            }



            if (featureClassA == null) { Mess.Record("读取错误：" + fname1); isok = false; }
            if (featureClassB == null) { Mess.Record("读取错误：" + fname2); isok = false; }
            if (featureClassC == null) { Mess.Record("读取错误：" + strcg); isok = false; }


            if (!isok) return;

            // 读取字段
            Dictionary<string, IField> dicB = new Dictionary<string, IField>();
            Dictionary<string, int> dicC = new Dictionary<string, int>();
            for (int i = 0; i < featureClassB.Fields.FieldCount; i++)
            {
                IField field = featureClassB.Fields.Field[i];
                if (field.Type != esriFieldType.esriFieldTypeGeometry) dicB.Add(field.Name, field);
            }
            for (int i = 0; i < featureClassC.Fields.FieldCount; i++)
            {
                IField field = featureClassC.Fields.Field[i];
                dicC.Add(field.Name, i);
            }

            // 添加B
            ShapefileRead.AddField(featureClassC, featureClassA.OIDFieldName + "_1", featureClassA.OIDFieldName + "_1", esriFieldType.esriFieldTypeString);
            foreach (KeyValuePair<string, IField> kvp in dicB)
            {
                if (kvp.Value.Type == esriFieldType.esriFieldTypeOID) ShapefileRead.AddField(featureClassC, kvp.Key + "_2", kvp.Key + "_2", esriFieldType.esriFieldTypeInteger);
                else
                {
                    if (featureClassA.Fields.FindField(kvp.Key) >= 0)
                    {
                        ShapefileRead.AddField(featureClassC, kvp.Key + "_2", kvp.Key + "_2", kvp.Value.Type);
                    }
                    else
                    {
                        ShapefileRead.AddField(featureClassC, kvp.Key, kvp.Key, kvp.Value.Type);
                    }
                }
            }

            string strcgdx = "成果多选" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try { featureClassD = featureWorkspace.OpenFeatureClass(strcgdx); } catch { featureClassD = null; }

            if (featureClassD == null)
            {
                featureClassD = featureWorkspace.CreateFeatureClass(strcgdx, featureClassC.Fields, featureClassC.CLSID, featureClassC.EXTCLSID,
                   esriFeatureType.esriFTSimple, featureClassC.ShapeFieldName, "");
            }
            if (featureClassD == null) { Mess.Record("读取错误：" + strcgdx); isok = false; }

            IQueryFilter queryFilterSave = new QueryFilterClass();
            IFeatureCursor featureCursorA, featureCursorB;
            IFeature featureUpdataA, featureUpdataB;

            // 循环A数据
            Mess.Record("循环A数据");
            featureCursorA = featureClassA.Search(queryFilterSave, false);
            while ((featureUpdataA = featureCursorA.NextFeature()) != null)
            {
                bool result = false;
                // 循环B数据
                featureCursorB = featureClassB.Search(queryFilterSave, false);
                while ((featureUpdataB = featureCursorB.NextFeature()) != null)
                {
                    // A数据与B数据相交
                    IRelationalOperator relaOper = featureUpdataA.ShapeCopy as IRelationalOperator;
                    result = relaOper.Disjoint(featureUpdataB.ShapeCopy);
                    result = !result;
                    if (result)
                    {
                        Mess.Record("A1:" + featureUpdataA.OID.ToString());
                        AddFeatureToFeatureClass(featureClassC, featureUpdataA, featureUpdataB);
                        break;
                    }
                }
                if (!result)
                {
                    Mess.Record("A2:" + featureUpdataA.OID.ToString());
                    AddFeatureToFeatureClass(featureClassC, featureUpdataA, null);
                }
            }

            // 循环B数据
            Mess.Record("循环B数据");
            featureCursorB = featureClassB.Search(queryFilterSave, false);
            while ((featureUpdataB = featureCursorB.NextFeature()) != null)
            {
                bool result = false;
                featureCursorA = featureClassA.Search(queryFilterSave, false);
                while ((featureUpdataA = featureCursorA.NextFeature()) != null)
                {//循环A数据
                 // A数据与B数据相交
                    IRelationalOperator relaOper = featureUpdataB.ShapeCopy as IRelationalOperator;
                    result = relaOper.Disjoint(featureUpdataA.ShapeCopy);
                    if (result) break;
                }
                if (!result)
                {
                    Mess.Record("B:" + featureUpdataB.OID.ToString());
                    AddFeatureToFeatureClass(featureClassC, null, featureUpdataB);
                    break;
                }
            }

            // 多项
            Mess.Record("多项");
            featureCursorA = featureClassA.Search(queryFilterSave, false);
            while ((featureUpdataA = featureCursorA.NextFeature()) != null)
            {
                bool result = false; int i = 0;
                // 循环B数据
                featureCursorB = featureClassB.Search(queryFilterSave, false);
                while ((featureUpdataB = featureCursorB.NextFeature()) != null)
                {
                    // A数据与B数据相交
                    IRelationalOperator relaOper = featureUpdataA.ShapeCopy as IRelationalOperator;
                    result = relaOper.Disjoint(featureUpdataB.ShapeCopy);
                    if (!result)
                    {
                        Mess.Record("A1:" + featureUpdataA.OID.ToString());
                        AddFeatureToFeatureClass(featureClassD, featureUpdataA, featureUpdataB);
                    }
                }
            }

            Mess.Record("完成！");
            }catch(Exception ex)
            {
                Mess.Record("错误："+ex.Message);
            }
        }


        public void AddFeatureToFeatureClass(IFeatureClass pFeatureClass, IFeature fa, IFeature fb)
        {
            //    try
            //    {
            IFeatureCursor pFeatureCur = pFeatureClass.Insert(true);
            IFeatureBuffer pFeatureBuf = pFeatureClass.CreateFeatureBuffer();
            IFields pFilds = pFeatureClass.Fields;
            for (int i = 1; i < pFilds.FieldCount; i++)
            {
                IField pFild = pFilds.get_Field(i);
                if (pFild.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    if (fa != null) pFeatureBuf.set_Value(i, fa.ShapeCopy);
                    else if (fb != null) pFeatureBuf.set_Value(i, fa.ShapeCopy);
                }

                else
                {
                    string findstr = pFild.Name;
                    if (findstr.Length > 2 && findstr.Substring(findstr.Length - 2) == "_1")
                    {
                        if (fa != null)
                        {
                            int index = fa.Fields.FindField(findstr.Substring(0, findstr.Length - 2));
                            if (index >= 0) pFeatureBuf.set_Value(i, fa.Value[index].ToString());
                        }
                    }
                    else if (findstr.Length > 2 && findstr.Substring(findstr.Length - 2) == "_2")
                    {
                        if (fb != null)
                        {
                            int index = fb.Fields.FindField(findstr.Substring(0, findstr.Length - 2));
                            if (index == 0) pFeatureBuf.set_Value(i, fb.Value[index].ToString());
                            else if (index > 0) pFeatureBuf.set_Value(i, fb.Value[index]);
                        }
                    }
                    else
                    {
                        if (fa != null)
                        {
                            int index = fa.Fields.FindField(findstr);
                            if (index >= 0) try { if(fa.Value[index]!=null && !string.IsNullOrEmpty(fa.Value[index].ToString())) pFeatureBuf.set_Value(i, fa.Value[index]); } catch { }
                        }
                        if (fb != null)
                        {
                            int index = fb.Fields.FindField(findstr);
                            if (index >= 0) try { if (fb.Value[index] != null && !string.IsNullOrEmpty(fb.Value[index].ToString())) pFeatureBuf.set_Value(i, fb.Value[index]); } catch { }
                        }
                    }
                }
            }
            pFeatureCur.InsertFeature(pFeatureBuf);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void Form服务项目_Load(object sender, EventArgs e)
        {
            helperControlRecord.Load();
        }

        public void AddFeatureToFeatureClass2(IFeatureClass pFeatureClass, IGeometry pGeom)
        {
            try
            {
                IFeatureCursor pFeatureCur = pFeatureClass.Insert(true);
                IFeatureBuffer pFeatureBuf = pFeatureClass.CreateFeatureBuffer();
                IPolyline pPolyline = pGeom as IPolyline;
                IFields pFilds = pFeatureClass.Fields;
                for (int i = 1; i < pFilds.FieldCount; i++)
                {
                    IField pFild = pFilds.get_Field(i);
                    if (pFild.Type == esriFieldType.esriFieldTypeGeometry)
                        pFeatureBuf.set_Value(i, pGeom);

                    else
                    {
                        if (pFild.Type == esriFieldType.esriFieldTypeInteger)
                            pFeatureBuf.set_Value(i, 0);
                        if (pFild.Type == esriFieldType.esriFieldTypeDouble)
                            pFeatureBuf.set_Value(i, pPolyline.Length);
                        if (pFild.Type == esriFieldType.esriFieldTypeSmallInteger)
                            pFeatureBuf.set_Value(i, 0);
                        if (pFild.Type == esriFieldType.esriFieldTypeString)
                            pFeatureBuf.set_Value(i, "没名字");
                    }
                }
                pFeatureCur.InsertFeature(pFeatureBuf);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HelperGDB helperGDB = new HelperGDB();
        }
    }
}
