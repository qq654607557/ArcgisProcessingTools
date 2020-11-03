
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessor;
using System.Threading.Tasks;
using HelperClass.LocalFile;
using HelperArcGIS.PGTool;
using HelperArcGIS.DataProcessing;

namespace ImageProcessing
{
    public partial class Form络图生产工具 : Form
    {
        string foldersPath = "";
        string foldersSavePath = "";
        int wkid = 4490;
        bool isDetection = true;


        bool run_zhidu = false;

        bool run_xqsjj = false;
        int run_xqsjj_start = 0;
        int run_xqsjj_end = 0;

        bool run_lunkuo = false;
        int run_lunkuo_start = 0;
        int run_lunkuo_end = 0;

        bool run_daochuSHP = false;
        int run_daochuSHP_start = 0;
        int run_daochuSHP_end = 0;

        bool run_hebingSHP = false;
        int run_hebingSHP_start = 0;
        int run_hebingSHP_end = 0;

        bool run_writeSHP = true;

        TIFOrder tifOrder;

        public Form络图生产工具()
        {
            InitializeComponent();
        }

        private void Form络图_Load(object sender, EventArgs e)
        {
            load_recording();
        }

        private void txt_源数据文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_源数据文件夹.Text = fbd.SelectedPath;
            }
        }

        private void txt_成果输出文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = foldersSavePath;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_成果输出文件夹.Text = fbd.SelectedPath;
            }
        }

        private void Btn开始生成_Click(object sender, EventArgs e)
        {
            this.foldersPath = this.txt_源数据文件夹.Text;
            this.foldersSavePath = this.txt_成果输出文件夹.Text;
            this.wkid = (int)this.txtWKID.Value;
            this.isDetection = this.txtc_强制检查.Checked;

            run_zhidu = this.txtc_只读.Checked;

            run_xqsjj = this.txtc_镶嵌数据集.Checked;
            int.TryParse(this.txt_镶嵌数据集_start.Text, out run_xqsjj_start);
            int.TryParse(this.txt_镶嵌数据集_end.Text, out run_xqsjj_end);

            run_lunkuo = this.txtc_轮廓.Checked;
            int.TryParse(this.txt_轮廓开始.Text, out run_lunkuo_start);
            int.TryParse(this.txt_轮廓结束.Text, out run_lunkuo_end);

            run_daochuSHP = this.txtc_导出SHP.Checked;
            int.TryParse(this.txt_导出SHP_开始.Text, out run_daochuSHP_start);
            int.TryParse(this.txt_导出SHP_结束.Text, out run_daochuSHP_end);

            run_hebingSHP = this.txtc_合并修改.Checked;
            int.TryParse(this.txt_合并修改_Start.Text, out run_hebingSHP_start);
            int.TryParse(this.txt_合并修改_end.Text, out run_hebingSHP_end);

            run_writeSHP = this.txtc_写入数据.Checked;

            if (string.IsNullOrEmpty(foldersPath.Trim())) { MessageBox.Show("源数据文件夹" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(foldersSavePath.Trim())) { MessageBox.Show("成果输出文件夹" + "没有输入！"); return; }

            save_recording();

            Task.Factory.StartNew(readFile);
        }

        private void readFile()
        {
            showMess_clear();
            showMess_Record("=============开始==================");
            try
            {
                showMess_Record("-> 准备数据");
                bool isok = false;
                string mess = "";
                List<Routo> nameList = new List<Routo>();

                // 基本数据
                string gdbPath = System.Windows.Forms.Application.StartupPath + @"\data\NewGDB.gdb";// @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";
                string tablenamebase = "TEM" + DateTime.Now.ToString("yyyyMMddHHmmss");//
                tablenamebase = "TEM20190419133508";
                string tablename = tablenamebase;
                string saveSHPPath = System.Windows.Forms.Application.StartupPath + @"\data\";// + tablename +".shp";// @"D:\Projecct\ImageProcessing\data\" + tablename + ".shp";
                string savePath = foldersSavePath + @"\LT" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".shp";// @"D:\Projecct\ImageProcessing\data\LT" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".shp";
                string GDBTablepath = gdbPath + @"\" + tablename;

                tifOrder = new TIFOrder(System.Windows.Forms.Application.StartupPath + @"\TIFOrder.ini");
                SysConfig.GetTestAngle(System.Windows.Forms.Application.StartupPath + @"\TestAngle.ini");

                string[] drop_fields = new string[] { "TypeID", "ItemTS", "UriHash", "GroupName", "ProductNam" };

                ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem(wkid);

                showMess_Record("-> 读取根目录文件夹");
                string foldersPath = this.foldersPath;
                nameList = readRootFolder(ref mess, foldersPath);
                if (nameList.Count <= 0) { showMess_Record("没有读取到文件夹"); return; }

                showMess_Record("-> 解析目录文件XML");
                isok = readFolderXML(ref mess, ref nameList);

                if (!isok) { showMess_Record("错误：" + mess); return; }

                showMess_Record("-> 解析目录文件TIF");
                readFolderTIF(ref mess, ref nameList);

                showMess_Record("==============================数据检查======================================");
                if (this.isDetection)
                {
                    bool isexit = false;
                    for (int i = 0; i < nameList.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(nameList[i].TIFPath))
                        {
                            if (string.IsNullOrEmpty(nameList[i].XML.TestAngle)) { showMess_Record("【侧视角】错误！文件：" + nameList[i].FolderName); isexit = true; }
                            if (string.IsNullOrEmpty(nameList[i].XML.TimePhase)) { showMess_Record("【时相】错误！文件：" + nameList[i].FolderName); isexit = true; }
                        }
                    }
                    if (isexit) return;
                }
                for (int i = 0; i < nameList.Count; i++)
                {
                    mess = "==================================\n";
                    //mess += "[" + i.ToString("D3") + "]文件夹:" + nameList[i].FolderName + "\n";
                    //mess += "[" + i.ToString("D3") + "]SHP:" + nameList[i].SHPPath + "\n";
                    mess += "[" + i.ToString("D3") + "]TIF:" + nameList[i].TIFPath + "";

                    showMess_Record(mess);
                }

                showMess_Record("==================================\n-> 开始读取坐标应为[" + spatialReference.Name + "] 非必须");
                if (run_zhidu) return;


                int fs = 10;


                if (run_xqsjj)
                {
                    showMess_Record("-> 创建镶嵌数据集");
                    if (run_xqsjj_end == 0) run_xqsjj_end = nameList.Count;
                    int run_xqsjj_index = -1;
                    string tem_tablename = "";
                    string tem_gdbPath = "";
                    for (int i = run_xqsjj_start; i < run_xqsjj_end; i++)
                    {
                        if (i % fs == 0)
                        {
                            run_xqsjj_index++;

                            // 10个输出一个文件
                            tem_tablename = tablename + "_" + run_xqsjj_index.ToString("D3");
                            tem_gdbPath = gdbPath + @"\" + tem_tablename;

                            isok = GPDataManagementTools.CreateMosaicDataset(ref mess, gdbPath, tem_tablename, spatialReference);
                            System.Threading.Thread.Sleep(100);
                            if (!isok) { showMess_Record("失败：/r/n" + mess); break; }
                            showMess_Record("-> 加入镶嵌数据集:" + tem_tablename);

                        }

                        showMess_Record("[" + i.ToString("D3") + "]加入镶嵌数据集：" + nameList[i].FolderName + "");
                        isok = GPDataManagementTools.AddRastersToMosaicDataset(ref mess, tem_gdbPath, nameList[i].TIFPath);
                        System.Threading.Thread.Sleep(100);
                        if (!isok) { showMess_Record("[" + i.ToString("D3") + "]==失败：\n" + mess); break; }
                    }
                }

                if (run_lunkuo)
                {
                    showMess_Record("-> 生成轮廓");
                    if (run_lunkuo_end == 0) run_lunkuo_end = nameList.Count;
                    int run_lunkuo_index = -1;
                    string tem_tablename = "";
                    string tem_gdbPath = "";

                    showMess_Record("-> 生成轮廓：" + run_lunkuo_start);
                    showMess_Record("-> 生成轮廓：" + run_lunkuo_end);

                    for (int i = run_lunkuo_start; i < run_lunkuo_end; i++)
                    {
                        showMess_Record("-> 生成轮廓：" + (i % fs));
                        if (i % fs == 0)
                        {
                            run_lunkuo_index++;
                            tem_tablename = tablename + "_" + (run_lunkuo_index).ToString("D3");
                            tem_gdbPath = gdbPath + @"\" + tem_tablename;
                        }

                        showMess_Record("-> 开始生成轮廓:" + tem_tablename);
                        isok = GPDataManagementTools.BuildFootprints(ref mess, tem_gdbPath);
                        System.Threading.Thread.Sleep(100);
                        if (!isok) { showMess_Record("生成轮廓错误:" + tem_tablename + "  =================\n" + mess); break; }
                        
                        showMess_Record("-> 生成轮廓:" + tem_tablename);
                    }
                }

                if (run_daochuSHP)
                {
                    showMess_Record("-> 导出shp文件");
                    if (run_daochuSHP_end == 0) run_daochuSHP_end = nameList.Count;

                    IWorkspace workspace = openGDB(gdbPath);
                    System.Threading.Thread.Sleep(100);

                    for (int i = this.run_daochuSHP_start; i < run_daochuSHP_end; i++)
                    {
                        if (i % fs == 0)
                        {
                            string tem_tablename = tablename + "_" + (i).ToString("D3");
                            string tem_gdbPath = gdbPath + @"\" + tem_tablename;
                            string tem_saveSHPPath = saveSHPPath + tem_tablename + ".shp";

                            IMosaicDataset iMosaicDataset = HelperMosaicDataset.GetMosaicDataset(ref mess, tem_tablename, workspace);
                            System.Threading.Thread.Sleep(100);
                            isok = GPDataManagementTools.CopyFeatures(ref mess, iMosaicDataset.Catalog, tem_saveSHPPath);
                            System.Threading.Thread.Sleep(100);
                            HelperMosaicDataset.DeleteMosaic(ref mess, tem_tablename, workspace);
                            System.Threading.Thread.Sleep(100);

                            if (!isok)
                            {
                                showMess_Record("导出shp文件:" + tem_tablename + "失败：" + mess);
                                saveSHPPath = "";
                            }
                            else
                            {
                                showMess_Record("-> 导出shp文件:" + tem_tablename + ".shp");
                                //list_shp.Add(tem_saveSHPPath);
                            }
                            i = i + fs - 1;
                        }
                        else if (i + fs > nameList.Count)
                        {
                            string tem_tablename = tablename + "_" + (i).ToString("D3");
                            string tem_gdbPath = gdbPath + @"\" + tem_tablename;
                            string tem_saveSHPPath = saveSHPPath + tem_tablename + ".shp";

                            IMosaicDataset iMosaicDataset = HelperMosaicDataset.GetMosaicDataset(ref mess, tem_tablename, workspace);
                            System.Threading.Thread.Sleep(100);
                            isok = GPDataManagementTools.CopyFeatures(ref mess, iMosaicDataset.Catalog, tem_saveSHPPath);
                            System.Threading.Thread.Sleep(100);
                            HelperMosaicDataset.DeleteMosaic(ref mess, tem_tablename, workspace);
                            System.Threading.Thread.Sleep(100);

                            if (!isok)
                            {
                                showMess_Record("导出shp文件:" + tem_tablename + "失败：" + mess);
                                saveSHPPath = "";
                            }
                            else
                            {
                                showMess_Record("-> 导出shp文件:" + tem_tablename + ".shp");
                                //list_shp.Add(tem_saveSHPPath);
                            }
                            break;
                        }
                    }
                }

                if (run_hebingSHP)
                {
                    showMess_Record("合并shp文件数据");
                    if (run_hebingSHP_end == 0) run_hebingSHP_end = nameList.Count;
                    List<string> list_shp = new List<string>();
                    for (int i = run_hebingSHP_start; i < run_hebingSHP_end; i++)
                    {
                        if (i % fs == 0)
                        {
                            string tem_tablename = tablename + "_" + (i).ToString("D3");
                            string tem_gdbPath = gdbPath + @"\" + tem_tablename;
                            string tem_saveSHPPath = saveSHPPath + tem_tablename + ".shp";

                            list_shp.Add(tem_saveSHPPath);
                            i = i + fs - 1;
                        }
                        else if (i + fs > nameList.Count)
                        {
                            string tem_tablename = tablename + "_" + (i).ToString("D3");
                            string tem_gdbPath = gdbPath + @"\" + tem_tablename;
                            string tem_saveSHPPath = saveSHPPath + tem_tablename + ".shp";

                            list_shp.Add(tem_saveSHPPath);
                            break;
                        }
                    }

                    isok = GPDataManagementTools.Merge(ref mess, list_shp.ToArray(), savePath);
                    System.Threading.Thread.Sleep(100);
                    if (!isok) { showMess_Record("合并shp文件数据错误：" + mess); return; }
                }

                if (run_writeSHP)
                {
                    //savePath = @"\\10.152.4.49\d\临时数据\LT20190506173339.shp";
                    showMess_Record("-> 删除不需要列");
                    isok = GPDataManagementTools.DeleteField(ref mess, savePath, drop_fields);
                    if (!isok) { showMess_Record(mess); return; }

                    showMess_Record("-> 修改shp文件数据");
                    isok = writeSHP(ref mess, savePath, nameList);
                    if (!isok) { showMess_Record(mess); return; }

                    showMess_Record("输出络图文件：" + savePath);
                }

            }
            catch (Exception ex)
            {
                showMess_Record("==================================\n错误：" + ex.Message);
            }
            showMess_Record("=============结束==================");
        }

        // 读取文件夹名称用
        private List<Routo> readRootFolder(ref string mess, string foldersPath)
        {
            List<Routo> nameList = new List<Routo>();

            DirectoryInfo d = new DirectoryInfo(foldersPath);
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            for (int i = 0; i < directs.Length; i++)
            {
                string fileType = readFileType(directs[i].Name);
                if (string.IsNullOrEmpty(fileType)) continue;
                nameList.Add(new Routo
                {
                    FolderName = directs[i].Name,
                    FolderName50 = stringTop(directs[i].Name),
                    FolderPath = directs[i].FullName,
                    TypeName = fileType
                });

                showMess_Record("获取文件夹:[" + (i + 1).ToString("D3") + "]" + directs[i].Name);
            }

            return nameList;
        }

        private string stringTop(string str, int top = 50)
        {
            string strpd = str;
            if (str.Substring(str.Length - 4).ToUpper() == ".TIF") { strpd = str.Substring(0, str.Length - 4); }
            if (str.Substring(str.Length - 4).ToUpper() == ".IMG") { strpd = str.Substring(0, str.Length - 4); }
            if (str.Substring(str.Length - 5).ToUpper() == ".TIFF") { strpd = str.Substring(0, str.Length - 5); }
            if (strpd.Length > top) { return strpd.Substring(0, top); }
            else { return strpd; }
        }

        private string readFileType(string foldersName)
        {
            if (foldersName.Length <= 0) return null;
            string[] sp = foldersName.Split('_');
            if (sp.Length <= 0) return null;
            string str = sp[0];
            if (str.Length <= 1) return null;
            if (SysConfig.TestAngle.ContainsKey(str)) return str;
            // 特殊判断 特殊WV（前面为为纯数字）
            if (isNumberic(str) && SysConfig.TestAngle.ContainsKey("WV")) return "WV";

            return null;
        }

        protected bool isNumberic(string message)
        {
            //判断是否为整数字符串
            //是的话则将其转换为数字并将其设为out类型的输出值、返回true, 否则为false
            int result = -1;   //result 定义为out 用来输出值
            try
            {
                //当数字字符串的为是少于4时，以下三种都可以转换，任选一种
                //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()

                //result = int.Parse(message);
                //result = Convert.ToInt16(message);
                result = Convert.ToInt32(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool readFolderXML(ref string mess, ref List<Routo> nameList)
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                if (i % 50 == 0) { showMess_Record("读取xml文件" + i.ToString() + "/" + nameList.Count); }
                List<string> listPath = new List<string>();
                FindFiles(nameList[i].FolderPath, ".XML", ref listPath);
                if (listPath.Count <= 0)
                {
                    showMess_Record("文件夹下并没有检测到xml文件请检查文件的正确性[" + (i + 1).ToString("D3") + "][" + nameList[i].FolderPath + "]");
                    //return true;
                }
                // 读取xml值
                for (int n = 0; n < listPath.Count; n++)
                {
                    if (string.IsNullOrEmpty(nameList[i].XML.TestAngle)
                        || string.IsNullOrEmpty(nameList[i].XML.TimePhase))
                    {
                        try
                        {
                            var document = new System.Xml.XPath.XPathDocument(listPath[n]);
                            var navigator = document.CreateNavigator();
                            // 角度
                            if (string.IsNullOrEmpty(nameList[i].XML.TestAngle))
                            {
                                string title = SysConfig.TestAngle[nameList[i].TypeName].TestAngle;
                                System.Xml.XPath.XPathNavigator node = navigator.SelectSingleNode("//" + title);
                                if (node != null) nameList[i].XML.TestAngle = node.Value.ToString();
                            }
                            // 时像
                            if (string.IsNullOrEmpty(nameList[i].XML.TimePhase))
                            {
                                string title = SysConfig.TestAngle[nameList[i].TypeName].TimePhase;
                                System.Xml.XPath.XPathNavigator node = navigator.SelectSingleNode("//" + title);
                                if (node != null)
                                {
                                    DateTime datetime;
                                    if (DateTime.TryParse(node.Value.ToString(), out datetime))
                                    {
                                        nameList[i].XML.TimePhase = datetime.ToString("yyyyMMdd");
                                    }
                                }
                            }
                        }
                        catch { }
                    }

                    if (!string.IsNullOrEmpty(nameList[i].XML.TestAngle) &&
                        !string.IsNullOrEmpty(nameList[i].XML.TimePhase))
                    {
                        break;
                    }

                }
            }
            return true;
        }

        private string readXMLValue(ref string mess, string xmlpath, string title)
        {
            try
            {
                var document = new System.Xml.XPath.XPathDocument(xmlpath);
                var navigator = document.CreateNavigator();
                System.Xml.XPath.XPathNavigator node = navigator.SelectSingleNode("//" + title);

                if (node == null) return null;
                else return node.Value.ToString();
            }
            catch
            {
                return null;
            }
        }

        private bool readFolderSHP(ref string mess, ref List<Routo> nameList)
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                List<string> listPath = new List<string>();
                FindFiles(nameList[i].FolderPath, ".SHP", ref listPath);
                if (listPath.Count <= 0)
                {
                    //showMess_Record("文件夹下并没有检测到SHP文件请检查文件的正确性[" + (i + 1).ToString("D3") + "][" + nameList[i].FolderPath + "]");
                    //return true;
                }
                else { nameList[i].SHPPath = listPath[0]; }

            }
            return true;
        }





        private bool readFolderTIF(ref string mess, ref List<Routo> nameList)
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                if (i % 50 == 0) { showMess_Record("完成" + i.ToString() + "/" + nameList.Count.ToString() + ""); }
                //showMess_Record("完成" + i.ToString() + "/" + nameList.Count.ToString() + "");
                if (!string.IsNullOrEmpty(nameList[i].SHPPath)) continue;

                List<string> listPath = new List<string>();
                FindFiles(nameList[i].FolderPath, ".tif", ".tiff", ".img", ref listPath);
                if (listPath.Count <= 0)
                {
                    nameList[i].Mess = "文件夹下并没有检测到TIF文件请检查文件的正确性[" + (i + 1).ToString("D3") + "][" + nameList[i].FolderPath + "]";
                    showMess_Record(nameList[i].Mess);
                    //return true;
                }
                else
                {
                    nameList[i].TIFPath = findBestTIF(listPath);
                    if (string.IsNullOrEmpty(nameList[i].TIFPath))
                    {
                        nameList[i].Mess = "文件夹内TIF都不是WGS1984坐标：" + nameList[i].FolderPath + "";
                        showMess_Record(nameList[i].Mess);
                        //nameList.Remove()
                    }
                }

            }

            return true;
        }

        private string findBestTIF(List<string> listPath)
        {
            // 判断需要哪个TIF文件
            int[] scores = new int[listPath.Count];

            for (int i = 0; i < listPath.Count; i++)
            {
                string foldername = System.IO.Path.GetFileNameWithoutExtension(listPath[i]);
                for (int o = 0; o < tifOrder.Order.Length; o++)
                {
                    if (foldername.ToLower().Contains(tifOrder.Order[o].ToLower())) scores[i]++;
                }
            }

            int position = -1;
            int score = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > score)
                {
                    score = scores[i];
                    position = i;
                }
            }
            if (position < 0) position = 0;

            return listPath[position];
        }

        private void detectionSHP(ref List<Routo> nameList)
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                if (string.IsNullOrEmpty(nameList[i].SHPPath)) continue;
                IEnvelope extent = GetTIFEnvelope(nameList[i].SHPPath);
                if (extent.SpatialReference.FactoryCode == (int)esriSRGeoCSType.esriSRGeoCS_WGS1984)
                {
                    //return true;
                }
                else
                {
                    showMess_Record("SHP文件不是WGS1984坐标：" + nameList[i].SHPPath + "");
                    nameList[i].SHPPath = null;
                    //nameList.Remove(nameList[i]);
                    //return false;
                }
            }
        }

        private bool detectionTIF(string TIFPath)
        {
            IEnvelope extent = GetTIFEnvelope(TIFPath);
            return (extent.SpatialReference.FactoryCode == (int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
        }

        public FileInfo FindFile(string path, string extension)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                if (f.Extension.ToUpper() == extension) { return f; }
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                return FindFile(dd.FullName, extension);
            }
            return null;
        }

        public void FindFiles(string path, string extension, ref List<string> listPath)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*" + extension);
            listPath.AddRange(files);
            string[] directs = System.IO.Directory.GetDirectories(path);
            foreach (string str in directs)
            {
                FindFiles(str, extension, ref listPath);
            }
        }

        public void FindFiles(string path, string extension1, string extension2, ref List<string> listPath)
        {
            string[] files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
        .Where(s => s.EndsWith(extension1.ToUpper()) || s.EndsWith(extension2.ToUpper()) ||
            s.EndsWith(extension1.ToLower()) || s.EndsWith(extension2.ToLower())).ToArray();

            listPath.AddRange(files);
            string[] directs = System.IO.Directory.GetDirectories(path);
            foreach (string str in directs)
            {
                FindFiles(str, extension1, extension2, ref listPath);
            }
        }

        public void FindFiles(string path, string extension1, string extension2, string extension3, ref List<string> listPath)
        {
            string[] files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
        .Where(s => s.EndsWith(extension1.ToUpper()) || s.EndsWith(extension2.ToUpper()) || s.EndsWith(extension3.ToUpper()) ||
            s.EndsWith(extension1.ToLower()) || s.EndsWith(extension2.ToLower()) || s.EndsWith(extension3.ToLower())).ToArray();

            listPath.AddRange(files);
            string[] directs = System.IO.Directory.GetDirectories(path);
            foreach (string str in directs)
            {
                FindFiles(str, extension1, extension2, extension3, ref listPath);
            }
        }

        private bool writeSHP(ref string mess, List<Routo> nameList)
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                if (string.IsNullOrEmpty(nameList[i].SHPPath)) continue;

                // 基本数据
                ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
                IFeatureClass featureClass = sr.ReadInfo(nameList[i].SHPPath);

                int idCol_NAME = featureClass.Fields.FindField("NAME");
                if (idCol_NAME < 0) { ArcGISTool.ShapefileRead.AddField(featureClass, "NAME", "NAME", esriFieldType.esriFieldTypeString); idCol_NAME = featureClass.Fields.FindField("NAME"); }

                IQueryFilter queryFilterSave = new QueryFilterClass();
                IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

                IFeature featureUpdata = featureCursor.NextFeature();
                if (featureUpdata != null)
                {
                    featureUpdata.set_Value(idCol_NAME, nameList[i].FolderName50);
                    featureUpdata.Store();
                }

                featureUpdata = null;
                queryFilterSave = null;
                featureCursor = null;
                featureClass = null;
            }
            return true;
        }

        List<string> name50 = new List<string>();
        private bool writeSHP(ref string mess, string shppath, List<Routo> nameList)
        {
            // 基本数据
            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shppath);

            int idCol_NAME = featureClass.Fields.FindField("NAME");
            int idCol_CSJ = featureClass.Fields.FindField("CSJ");
            int idCol_SX = featureClass.Fields.FindField("SX");
            int idCol_NAMEALL = featureClass.Fields.FindField("NAMEALL");


            if (idCol_NAME < 0) { return false; }
            //if (idCol_NAME < 0) { ArcGISTool.ShapefileRead.AddField(featureClass, "NAME", "NAME", esriFieldType.esriFieldTypeString); idCol_NAME = featureClass.Fields.FindField("NAME"); }
            if (idCol_CSJ < 0) { ArcGISTool.ShapefileRead.AddField(featureClass, "CSJ", "CSJ", esriFieldType.esriFieldTypeString); idCol_CSJ = featureClass.Fields.FindField("CSJ"); }
            if (idCol_SX < 0) { ArcGISTool.ShapefileRead.AddField(featureClass, "SX", "SX", esriFieldType.esriFieldTypeString); idCol_SX = featureClass.Fields.FindField("SX"); }
            if (idCol_NAMEALL < 0) { ArcGISTool.ShapefileRead.AddField(featureClass, "NAMEALL", "NAMEALL", esriFieldType.esriFieldTypeString, 250); idCol_NAMEALL = featureClass.Fields.FindField("NAMEALL"); }

            for (int i = 0; i < nameList.Count; i++)
            {
                IQueryFilter queryFilterSave = new QueryFilterClass();
                queryFilterSave.WhereClause = " NAME like '%" + nameList[i].FolderName50 + "%' ";
                IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

                IFeature featureUpdata = featureCursor.NextFeature();
                if (featureUpdata != null)
                {
                    if (idCol_NAME > 0) { featureUpdata.set_Value(idCol_NAME, nameList[i].FolderName); }
                    if (idCol_CSJ > 0) { featureUpdata.set_Value(idCol_CSJ, nameList[i].XML.TestAngle); }
                    if (idCol_SX > 0) { featureUpdata.set_Value(idCol_SX, nameList[i].XML.TimePhase); }
                    if (idCol_NAMEALL > 0) { featureUpdata.set_Value(idCol_NAMEALL, nameList[i].FolderName); }

                    featureUpdata.Store();
                }
                else
                { // 截取名字查询
                    if (name50.Count == 0)
                    {
                        string opname_path = System.Windows.Forms.Application.StartupPath + @"\name50.ini";
                        HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();

                        name50 = txt.ReadTxt(opname_path);
                    }
                    string str = nameList[i].FolderName50;
                    for (int n = 0; n < name50.Count; n++)
                    {
                        str = str.Replace(name50[n], "%");
                    }

                    queryFilterSave.WhereClause = " NAME like '%" + str + "%' ";
                    featureCursor = featureClass.Search(queryFilterSave, false);
                    featureUpdata = featureCursor.NextFeature();
                    if (featureUpdata != null)
                    {
                        if (idCol_NAME > 0) { featureUpdata.set_Value(idCol_NAME, nameList[i].FolderName); }
                        if (idCol_CSJ > 0) { featureUpdata.set_Value(idCol_CSJ, nameList[i].XML.TestAngle); }
                        if (idCol_SX > 0) { featureUpdata.set_Value(idCol_SX, nameList[i].XML.TimePhase); }
                        if (idCol_NAMEALL > 0) { featureUpdata.set_Value(idCol_NAMEALL, nameList[i].FolderName); }

                        featureUpdata.Store();
                    }
                    else
                    {
                        showMess_Record("shp文件中找不到name为" + nameList[i].FolderName50 + "的记录,sql" + " NAME like '%" + nameList[i].FolderName50 + "%' ||  " + str);
                    }
                }
            }

            featureClass = null;
            return true;
        }

        void showMess_Record(string mess)
        {
            this.Invoke((EventHandler)delegate
            {
                this.rtbRecord.AppendText(DateTime.Now.ToString("HH:mm ") + mess + "\n");
                rtbRecord.SelectionStart = rtbRecord.TextLength;
                rtbRecord.ScrollToCaret();
            });
        }

        void showMess_clear()
        {
            this.Invoke((EventHandler)delegate
            {
                this.rtbRecord.Text = "";
            });
        }


        void buildSHP()
        {
            showMess_Record("=============开始==================");
            // 基本数据
            string gdbPath = @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";

            string[] tifPath = new string[2]
            { @"D:\Projecct\ImageProcessing\data\K3_20171029060300_29078_07711197_L1G_Bundle\K3_20171029060300_29078_07711197_L1G_B.tif",
                 @"D:\Projecct\ImageProcessing\data\K3A_20180304060821_16235_00571286_L1O_DZ\K3A_20180304060821_16235_00571286_L1O_DZ\K3A_20180304060821_16235_00571286_L1O_B.tif"};

            // GDB新建镶嵌数据集
            string tablenamebase = "XQ" + DateTime.Now.ToString("yyyyMMddHHmmss");

            for (int i = 0; i < tifPath.Length; i++)
            {
                string tablename = tablenamebase + i.ToString("D3");

                showMess_Record("正在处理：" + tablename + "");

                IEnvelope envelope = GetTIFEnvelope(tifPath[i]);
                MosaicDataset_CreateMosaicDataset(gdbPath, tablename, envelope);
                string GDBTablepath = gdbPath + @"\" + tablename;
                MosaicDataset_AddRastersToMosaicDataset(GDBTablepath, tifPath[i]);
                MosaicDataset_BuildFootprints(GDBTablepath);
                MosaicDataset_CopyFeatures(gdbPath, tablename, @"D:\Projecct\ImageProcessing\data\" + tablename + ".shp");
                //showMess_Record("处理完成：" + tablename + "");
            }

            // 添加栅格数据数据（tfi）

            // 镶嵌数据集-构建轮廓

            // xml数据填写入 镶嵌数据集-轮廓 ？？？

            // 镶嵌数据集-轮廓 生成shp

            // xml数据填写入shp ？？？

            // 完成

            showMess_Record("=============结束==================");
        }

        void buildSHP2()
        {
            showMess_Record("=============开始==================");
            // 基本数据
            string gdbPath = @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";
            string[] tifPath = new string[2]
            { @"D:\Projecct\ImageProcessing\data\TRIPLESAT_1_PMS_L1_20180303032204_00165EVI_031_0120180417001001_003\TRIPLESAT_1_MS_L1_20180303032204_00165EVI_031_0120180417001001_003_browser.tif",
                 @"D:\Projecct\ImageProcessing\data\TRIPLESAT_1_PMS_L1_20170509031545_000ED4VI_010_20170405002001_444\PAN\TRIPLESAT_1_PAN_L1_20170509031545_000ED4VI_010_20170405002001_444_browser.tif"};


            // GDB新建镶嵌数据集
            string tablenamebase = "XQ" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string tablename = tablenamebase;

            showMess_Record("开始读取坐标");
            IEnvelope envelope = GetTIFEnvelope(tifPath[0]);
            MosaicDataset_CreateMosaicDataset(gdbPath, tablename, envelope);
            string GDBTablepath = gdbPath + @"\" + tablename;

            for (int i = 0; i < tifPath.Length; i++)
            {
                showMess_Record("正在处理：" + tifPath[i] + "");
                MosaicDataset_AddRastersToMosaicDataset(GDBTablepath, tifPath[i]);
            }
            showMess_Record("生成轮廓");
            MosaicDataset_BuildFootprints(GDBTablepath);
            showMess_Record("导出文件");
            MosaicDataset_CopyFeatures(gdbPath, tablename, @"D:\Projecct\ImageProcessing\data\" + tablename + ".shp");

            // 添加栅格数据数据（tfi）

            // 镶嵌数据集-构建轮廓

            // xml数据填写入 镶嵌数据集-轮廓 ？？？

            // 镶嵌数据集-轮廓 生成shp

            // xml数据填写入shp ？？？

            // 完成

            showMess_Record("=============结束==================");
        }

        private IRasterLayer ReadFile(string filepath)
        {
            IRasterLayer rasterLayer = (IRasterLayer)new RasterLayerClass();
            rasterLayer.CreateFromFilePath(filepath);
            return rasterLayer;
        }

        private IEnvelope GetTIFEnvelope(string tifPath)
        {
            IRasterLayer rasterLayer = ReadFile(tifPath);
            IRaster raster = rasterLayer.Raster;
            IRasterProps rasterProps = raster as IRasterProps;
            IEnvelope extent = rasterProps.Extent; //当前栅格数据集的范围

            rasterProps = null;
            raster = null;
            rasterLayer = null;

            return extent;
        }

        private IWorkspace openGDB(string gdbPath)
        {
            //string gdbPath = @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";
            IWorkspaceFactory pFactory = new FileGDBWorkspaceFactory();
            IWorkspace pWorkspace = pFactory.OpenFromFile(gdbPath, 0);
            return pWorkspace;
        }

        /// <summary> 
        /// 创建镶嵌数据集 
        /// </summary> 
        /// <param name="pFgdbWorkspace">工作空间</param> 
        /// <param name="pMDame">名称</param> 
        /// <param name="pSrs">空间参考</param>
        /// <returns>镶嵌数据集</returns>
        public static IMosaicDataset CreateMosaicDataset(IWorkspace pFgdbWorkspace, string pMDame, ISpatialReference pSrs)
        {
            try
            {
                ICreateMosaicDatasetParameters pCreationPars = new CreateMosaicDatasetParametersClass();

                pCreationPars.BandCount = 3;
                pCreationPars.PixelType = rstPixelType.PT_UCHAR;
                IMosaicWorkspaceExtensionHelper pMosaicExentionHelper = new MosaicWorkspaceExtensionHelperClass();
                IMosaicWorkspaceExtension pMosaicExtention = pMosaicExentionHelper.FindExtension(pFgdbWorkspace);
                return pMosaicExtention.CreateMosaicDataset(pMDame, pSrs, pCreationPars, "DOM");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 删除镶嵌数据集
        /// </summary>
        /// <param name="rasterName">名称</param>
        /// <param name="workspace">工作空间</param>
        /// <returns>删除成功时返回true,否则返回false</returns>
        public static bool DeleteMosaic(string rasterName, IWorkspace workspace)
        {
            try
            {
                IMosaicWorkspaceExtensionHelper pMosaicWsExHelper = new MosaicWorkspaceExtensionHelperClass();
                IMosaicWorkspaceExtension pMosaicWsExt = pMosaicWsExHelper.FindExtension(workspace);
                pMosaicWsExt.DeleteMosaicDataset(rasterName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MosaicDataset_CreateMosaicDataset(string gdbPath, string tablename, IEnvelope envelope)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.CreateMosaicDataset createMosaicDataset = new ESRI.ArcGIS.DataManagementTools.CreateMosaicDataset();


            createMosaicDataset.in_mosaicdataset_name = tablename;
            createMosaicDataset.in_workspace = gdbPath;// @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";
            createMosaicDataset.coordinate_system = envelope.SpatialReference; //esriSRGeoCSType.esriSRGeoCS_WGS1984;
            try
            {
                gp.Execute(createMosaicDataset, null);
            }

            catch (Exception)
            {
                for (int i = 0; i < gp.MessageCount; i++)
                {
                    showMess_Record(gp.GetMessage(i));
                }
            }
        }

        /// <summary>
        /// 添加栅格数据集 AddRastersToMosaicDataset
        /// </summary>
        public void MosaicDataset_AddRastersToMosaicDataset(string GDBTablepath, string tifPath)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.AddRastersToMosaicDataset addRastersToMosaicDataset = new ESRI.ArcGIS.DataManagementTools.AddRastersToMosaicDataset();
            addRastersToMosaicDataset.in_mosaic_dataset = GDBTablepath;
            addRastersToMosaicDataset.raster_type = "Raster Dataset";
            addRastersToMosaicDataset.input_path = tifPath;
            gp.Execute(addRastersToMosaicDataset, null);
        }

        /// <summary>
        /// 构建轮廓 BuildFootprints
        /// </summary>
        public void MosaicDataset_BuildFootprints(string GDBTablepath)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.BuildFootprints buildFootprints = new ESRI.ArcGIS.DataManagementTools.BuildFootprints();
            buildFootprints.in_mosaic_dataset = GDBTablepath;

            gp.Execute(buildFootprints, null);
        }



        public void MosaicDataset_CopyFeatures(string GDBTablepath, string tablename, string save_shpName)
        {
            Geoprocessor gp = new Geoprocessor();    //初始化Geoprocessor
            gp.OverwriteOutput = true;                     //允许运算结果覆盖现有文件

            ESRI.ArcGIS.DataManagementTools.CopyFeatures copyFeatures = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();

            IWorkspace workspace = openGDB(@"D:\Projecct\ImageProcessing\data\NewGDB.gdb");
            IMosaicDataset iMosaicDataset = GetMosaicDataset(tablename, workspace);// GetMosaicDataset("aaa", workspace);

            copyFeatures.in_features = iMosaicDataset.Catalog;
            copyFeatures.out_feature_class = save_shpName;// @"D:\Projecct\ImageProcessing\data\" +  "SHP" + DateTime.Now.ToString("yyyyMMddHHmmss") +".shp";
            gp.Execute(copyFeatures, null);

            DeleteMosaicDataset(tablename, workspace);
        }

        public void MosaicDataset_Merge()
        {
            string[] instrs = new string[5]{
            @"D:\Projecct\ImageProcessing\data\XQ20190218093738.shp",
            @"D:\Projecct\ImageProcessing\data\XQ20190218094336.shp",
            @"D:\Projecct\ImageProcessing\data\XQ20190218094355.shp",
            @"D:\Projecct\ImageProcessing\data\XQ20190218094406.shp",
            @"D:\Projecct\ImageProcessing\data\XQ20190218094415.shp"
            };

            string instr = "";
            for (int i = 0; i < instrs.Length; i++)
            {
                instr += instrs[i] + (instrs.Length - 1 == i ? "" : ";");
            }
            instr += ";";

            string output = @"D:\Projecct\ImageProcessing\data\HB003.shp";

            bool isok = false;
            string mess = "";
            showMess_Record("开始");

            isok = HelperArcGIS.PGTool.GPDataManagementTools.Merge(ref mess, instr, output);
            if (!isok) { showMess_Record(mess); return; }

            showMess_Record("完成");
        }



        /// <summary>
        /// 获取镶嵌数据集
        /// </summary>
        /// <param name="MosaicName">数据集名称</param>
        /// <param name="workspace">数据集所在工作空间</param>
        /// <returns>镶嵌数据集</returns>
        public static IMosaicDataset GetMosaicDataset(string MosaicName, IWorkspace workspace)
        {
            IMosaicDataset pMosicDataset = null;
            IMosaicWorkspaceExtensionHelper pMosaicWsExHelper = new MosaicWorkspaceExtensionHelperClass();
            IMosaicWorkspaceExtension pMosaicWsExt = pMosaicWsExHelper.FindExtension(workspace);
            if (pMosaicWsExt != null)
            {
                try
                {
                    pMosicDataset = pMosaicWsExt.OpenMosaicDataset(MosaicName);
                }
                catch (Exception)
                {
                    return pMosicDataset;
                }
            }
            return pMosicDataset;
        }



        /// <summary>
        /// 删除镶嵌数据集
        /// </summary>
        /// <param name="rasterName">名称</param>
        /// <param name="workspace">工作空间</param>
        /// <returns>删除成功时返回true,否则返回false</returns>
        public static bool DeleteMosaicDataset(string rasterName, IWorkspace workspace)
        {
            try
            {
                IMosaicWorkspaceExtensionHelper pMosaicWsExHelper = new MosaicWorkspaceExtensionHelperClass();
                IMosaicWorkspaceExtension pMosaicWsExt = pMosaicWsExHelper.FindExtension(workspace);
                pMosaicWsExt.DeleteMosaicDataset(rasterName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void but测试_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(()=>{ buildSHP(); });
            //Task.Factory.StartNew(buildSHP); 
            Task.Factory.StartNew(buildSHP2);
            //Task.Factory.StartNew(MosaicDataset_Merge);   
        }



        private void Btn读取xml_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(readXML);
            string path = @"D:\影像统筹数据处理工具\C#源码\ImageProcessing\络图生产工具\bin\Debug\";
            //string foldername = System.IO.Path.GetFileNameWithoutExtension(path);

            //string[] files = System.IO.Directory.GetFiles(path, "*.txt");

            string[] files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".txt") || s.EndsWith(".jpg")).ToArray();

            string str = "";


        }

        private void readXML()
        {
            showMess_Record("=============开始==================");

            // 基本数据
            string xmlpath = @"C:\TuZhiData\test\TRIPLESAT_1_PMS_L1_20170509031545_000ED4VI_010_20170405002001_444\PAN\TRIPLESAT_1_PAN_L1_20170509031545_000ED4VI_010_20170405002001_444_meta.xml";

            //System.Xml.XmlDocument xml = HelperXML.OpenXML(xmlpath);
            //string str = HelperXML.GetValue(xml, "Auxiliary/General/Projection/Type");
            //showMess_Record(str);
            string mess = "";
            string value = readXMLValue(ref mess, xmlpath, "Roll_Angle");

            showMess_Record("=============结束==================");
        }

        private void Btn编辑shp_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(readshp);
        }

        private void readshp()
        {
            showMess_Record("=============开始==================");

            // 基本数据
            string shppath = @"D:\Projecct\ImageProcessing\data\XQ20190218093738.shp";
            string see = "TRIPLESAT_1_MS_L1_20180303032204_00165EVI_031_0120";

            ArcGISTool.ShapefileRead sr = new ArcGISTool.ShapefileRead();
            IFeatureClass featureClass = sr.ReadInfo(shppath);

            IQueryFilter queryFilterSave = new QueryFilterClass();
            queryFilterSave.WhereClause = " name='" + see + "' ";

            IFeatureCursor featureCursor = featureClass.Search(queryFilterSave, false);

            int idCol_GroupName = featureCursor.Fields.FindField("GroupName");
            int idCol_ProductNam = featureCursor.Fields.FindField("ProductNam");

            IFeature featureUpdata = featureCursor.NextFeature();
            if (featureUpdata != null)
            {
                featureUpdata.set_Value(idCol_GroupName, "GroupName");
                featureUpdata.set_Value(idCol_ProductNam, "ProductNam");

                featureUpdata.Store();
            }

            featureClass = null;

            showMess_Record("================结束==================");
        }

        private void Btn删列shp_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(delColSHP);
        }

        void delColSHP()
        {
            showMess_Record("=============开始==================");

            string shpPath = @"D:\Projecct\ImageProcessing\data\XQ20190218094355.shp";
            string[] drop_fields = new string[3] { "TypeID", "ItemTS", "UriHash" };

            bool isok = false;
            string mess = "";

            isok = HelperArcGIS.PGTool.GPDataManagementTools.DeleteField(ref mess, shpPath, drop_fields);
            if (!isok) showMess_Record(mess);

            showMess_Record("================结束==================");
        }

        private void Btn选择TIF_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string[] strs = fd.FileNames;
                for (int i = 0; i < strs.Length; i++) lstTIF.Items.Add(strs[i]);
            }
        }

        private void Btn生成络图_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(runLT);
        }

        void runLT()
        {
            showMess_Record("=============开始==================");
            bool isok = false;
            string mess = "";

            // 基本数据
            string gdbPath = System.Windows.Forms.Application.StartupPath + @"\data\NewGDB.gdb";// @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";
            string tablenamebase = "TEM" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string tablename = tablenamebase;
            string saveSHPPath = System.Windows.Forms.Application.StartupPath + @"\" + tablename + ".shp";// @"D:\Projecct\ImageProcessing\data\" + tablename + ".shp";
            string savePath = System.Windows.Forms.Application.StartupPath + @"\LT" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".shp";// @"D:\Projecct\ImageProcessing\data\LT" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".shp";
            string[] drop_fields = new string[3] { "TypeID", "ItemTS", "UriHash" };

            string[] tifPath = null;
            this.Invoke((EventHandler)delegate
            {
                tifPath = new string[lstTIF.Items.Count];
                for (int i = 0; i < lstTIF.Items.Count; i++) tifPath[i] = lstTIF.Items[i].ToString();
            });

            string[] shpPath = null;
            this.Invoke((EventHandler)delegate
            {
                shpPath = new string[lstSHP.Items.Count + 1];
                for (int i = 0; i < lstSHP.Items.Count; i++) shpPath[i] = lstSHP.Items[i].ToString();
            });

            // GDB新建镶嵌数据集

            showMess_Record("开始读取坐标");
            IEnvelope envelope = GetTIFEnvelope(tifPath[0]);
            isok = GPDataManagementTools.CreateMosaicDataset(ref mess, gdbPath, tablename, envelope.SpatialReference);
            if (!isok) { showMess_Record(mess); return; }
            envelope = null;

            string GDBTablepath = gdbPath + @"\" + tablename;

            for (int i = 0; i < tifPath.Length; i++)
            {
                showMess_Record("正在处理：" + tifPath[i] + "");
                isok = GPDataManagementTools.AddRastersToMosaicDataset(ref mess, GDBTablepath, tifPath[i]);
                if (!isok) { showMess_Record(mess); return; }
            }
            showMess_Record("生成轮廓");
            isok = GPDataManagementTools.BuildFootprints(ref mess, GDBTablepath);
            if (!isok) { showMess_Record(mess); return; }

            showMess_Record("导出shp文件");
            IWorkspace workspace = openGDB(gdbPath);
            IMosaicDataset iMosaicDataset = HelperMosaicDataset.GetMosaicDataset(ref mess, tablename, workspace);
            isok = GPDataManagementTools.CopyFeatures(ref mess, iMosaicDataset.Catalog, saveSHPPath);
            HelperMosaicDataset.DeleteMosaic(ref mess, tablename, workspace);
            if (!isok) { showMess_Record(mess); return; }
            workspace = null;

            showMess_Record("合并shp文件数据");
            shpPath[shpPath.Length - 1] = saveSHPPath;
            isok = GPDataManagementTools.Merge(ref mess, shpPath, savePath);
            if (!isok) { showMess_Record(mess); return; }

            showMess_Record("修改shp文件数据");
            isok = GPDataManagementTools.DeleteField(ref mess, savePath, drop_fields);
            if (!isok) { showMess_Record(mess); return; }

            showMess_Record("输出络图文件：" + savePath);

            showMess_Record("=============结束==================");
        }

        private void LstTIF_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void LstTIF_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void LstTIF_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String s in files)
                {
                    (sender as ListBox).Items.Add(s);
                }
            }
        }

        private void LstTIF_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
            //lstTIF.Items.Remove(lstTIF.SelectedItem);
        }

        private void LstTIF_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as ListBox).Items.Remove((sender as ListBox).SelectedItem);
        }


        Dictionary<string, Control> idc_rec_control = new Dictionary<string, Control>();
        private void save_recording()
        {
            List<string> opnamelist = new List<string>();
            foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
            {
                if (kvp.Value is NumericUpDown)
                {
                    opnamelist.Add(kvp.Key + "," + (kvp.Value as NumericUpDown).Value.ToString("F0"));
                }
                else
                {
                    opnamelist.Add(kvp.Key + "," + kvp.Value.Text);
                }
            }

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\Recording.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();
            txt.WriteTxt(opname_path, opnamelist);
        }

        private void load_recording()
        {
            idc_rec_control.Clear();

            idc_rec_control.Add("成果输出文件夹", this.txt_成果输出文件夹);
            idc_rec_control.Add("源数据文件夹", this.txt_源数据文件夹);

            idc_rec_control.Add("轮廓开始", this.txt_轮廓开始);
            //idc_rec_control.Add("WKID", this.txtWKID);

            string opname_path = System.Windows.Forms.Application.StartupPath + @"\Recording.ini";
            HelperClass.LocalFile.HelperTxt txt = new HelperClass.LocalFile.HelperTxt();

            List<string> opnamelist = txt.ReadTxt(opname_path);
            for (int i = 0; i < opnamelist.Count; i++)
            {
                string[] tem = opnamelist[i].Split(',');
                if (tem.Length == 2)
                {
                    foreach (KeyValuePair<string, Control> kvp in idc_rec_control)
                    {

                        if (kvp.Key == tem[0])
                        {
                            if (kvp.Value is NumericUpDown)
                            {
                                decimal d = 0;
                                decimal.TryParse(tem[1], out d);
                                (kvp.Value as NumericUpDown).Value = d;
                            }
                            else
                            {
                                kvp.Value.Text = tem[1];
                            }
                        }
                    }
                }
            }
        }

        private void rbtn_CGCS2000_3_Degree_GK_Zone_35_CheckedChanged(object sender, EventArgs e)
        {
            this.txtWKID.Value = 4490;
        }

    }
}
