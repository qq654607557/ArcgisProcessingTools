using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form循环裁图工具 : Form
    {
        public Form循环裁图工具()
        {
            InitializeComponent();
        }

        string foldersPath = "";
        string foldersSavePath = "";

        private void BtnSelectFolders_Click(object sender, EventArgs e)
        {
            string folderName = "CQ" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string out_feature_class = @"C:\TEMP\" + folderName;

            string mergeshpPath = @"C:\TEMP\ME" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".SHP";

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"D:\Projecct\ImageProcessing\data\TIF_SHP\";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string foldersPath = fbd.SelectedPath;

                Task.Factory.StartNew(() =>
                {
                    showMess_Record("=======================开始==========================");
                    showMess_Record("--> 读取文件");
                    List<FileInfo> listFileInfo = new List<FileInfo>();
                    Director(foldersPath, ref listFileInfo);
                    if (listFileInfo.Count <= 0) { showMess_Record("读取文件失败"); return; }
                    foreach (FileInfo fi in listFileInfo) { showMess_Record("找到文件：" + fi.FullName); }

                    showMess_Record("--> 排序");
                    sort(ref listFileInfo);
                    if (listFileInfo.Count <= 0) { showMess_Record("排序文件失败"); return; }

                    showMess_Record("--> 填写信息");

                    showMess_Record("--> 循环裁剪");
                    out_feature_class = loopTailoring(listFileInfo, out_feature_class);
                    if (string.IsNullOrEmpty(out_feature_class)) { showMess_Record("循环裁剪失败"); return; }

                    showMess_Record("--> 合并");
                    listFileInfo = new List<FileInfo>();
                    Director(out_feature_class, ref listFileInfo);
                    if (listFileInfo.Count <= 0) { showMess_Record("合并读取文件失败"); return; }

                    mergeshpPath = Merge(listFileInfo, mergeshpPath);
                    if (string.IsNullOrEmpty(mergeshpPath)) { showMess_Record("合并失败"); return; }

                    Directory.Delete(out_feature_class, true);
                    showMess_Record("输出合并文件:" + mergeshpPath);

                    showMess_Record("--> 裁切[县级]");

                    showMess_Record("--> 拓扑检测");

                    showMess_Record("--> 输出成果");
                    showMess_Record("输出成果:" + mergeshpPath);

                    showMess_Record("=======================结束==========================");
                });
            }
        }

        public void Director(string dir, ref List<FileInfo> listFileInfo)
        {
            //showMess_Record("循环中：" + dir);
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                if (f.Extension.ToUpper() == ".SHP")
                {
                    //string str = GetFileInfo(f);
                    //showMess_Record(str);
                    listFileInfo.Add(f);
                    //showMess_Record("找到文件：" + f.FullName);
                    //showMess_Record("=================================================");
                }
                //    list.Add(f.Name);//添加文件名到列表中  
                //showMess_Record("得到文件：" + f.FullName);
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                Director(dd.FullName, ref listFileInfo);
            }
        }

        private void sort(ref List<FileInfo> listFileInfo)
        {
            listFileInfo.Sort(new CustomComparer());
            for (int i = 0; i < listFileInfo.Count; i++) { showMess_Record("排序：[" + (i + 1).ToString("D3") + "]" + listFileInfo[i].Name); }
        }


        #region 弃用的排序方法
        public class CustomComparer : IComparer<FileInfo>
        {
            public int Compare(FileInfo obj1, FileInfo obj2)
            {


                int res = 0;
                if ((obj1 == null) && (obj2 == null))
                {
                    return 0;
                }
                else if ((obj1 != null) && (obj2 == null))
                {
                    return 1;
                }
                else if ((obj1 == null) && (obj2 != null))
                {
                    return -1;
                }

                return CompareName(obj1.Name, obj2.Name);
            }

            public int CompareName(string s1, string s2)
            {
                if (s1.Length > s2.Length) return 1;
                if (s1.Length < s2.Length) return -1;
                for (int i = 0; i < s1.Length; i++)
                {
                    if (s1[i] > s2[i]) return 1;
                    if (s1[i] < s2[i]) return -1;
                }
                return 0;
            }
        }
        #endregion

        private string loopTailoring(List<FileInfo> listFileInfo, string out_feature_class)
        {

            if (!Directory.Exists(out_feature_class)) Directory.CreateDirectory(out_feature_class);

            for (int i = 0; i < listFileInfo.Count; i++)
            {
                string mess = " ";
                for (int n = i + 1; n < listFileInfo.Count; n++)
                {
                    //mess += listFileInfo[i] + "->" + listFileInfo[n] + " | ";
                    mess = "[" + (i + 1).ToString("D3") + "]" + listFileInfo[i].Name + "->" + "[" + (n + 1).ToString("D3") + "]" + listFileInfo[n].Name + " | ";
                    showMess_Record(mess);

                    bool isok = false;
                    isok = HelperArcGIS.PGTool.GPAnalysisTools.Clip(ref mess, listFileInfo[i].FullName, listFileInfo[n].FullName, out_feature_class + @"\" + "CQ" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".SHP");
                    if (!isok) { showMess_Record(mess); return null; }
                }
            }
            return out_feature_class;
        }

        private string Merge(List<FileInfo> listFileInfo, string mergeshpPath)
        {
            //if (!Directory.Exists(mergeshpPath)) Directory.CreateDirectory(mergeshpPath);

            string strs = "";
            for (int i = 0; i < listFileInfo.Count; i++) strs += (listFileInfo[i].FullName + ";");
            bool isok = false;
            string mess = "";
            isok = HelperArcGIS.PGTool.GPDataManagementTools.Merge(ref mess, strs, mergeshpPath);
            if (!isok) { showMess_Record(mess); return null; }
            return mergeshpPath;
        }

        private static string GetFileInfo(FileInfo fif)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("文件创建时间：{0}", fif.CreationTime.ToString()));
            sb.AppendLine(string.Format("文件最后一次读取时间：{0}", fif.LastAccessTime.ToString()));
            sb.AppendLine(string.Format("文件最后一次修改时间：{0}", fif.LastWriteTime.ToString()));
            sb.AppendLine(string.Format("文件创建时间(UTC)：{0}", fif.CreationTimeUtc.ToString()));
            sb.AppendLine(string.Format("文件最后一次读取时间(UTC)：{0}", fif.LastAccessTimeUtc.ToString()));
            sb.AppendLine(string.Format("文件最后一次修改时间(UTC)：{0}", fif.LastWriteTimeUtc.ToString()));
            sb.AppendLine(string.Format("文件目录：{0}", fif.Directory));
            sb.AppendLine(string.Format("文件目录名称：{0}", fif.DirectoryName));
            sb.AppendLine(string.Format("文件扩展名：{0}", fif.Extension));
            sb.AppendLine(string.Format("文件完整名称：{0}", fif.FullName));
            sb.AppendLine(string.Format("文件名：{0}", fif.Name));
            sb.AppendLine(string.Format("文件字节长度：{0}", fif.Length));
            return sb.ToString();
        }

        void showMess_Record(string mess)
        {
            this.Invoke((EventHandler)delegate
            {
                //if (rtbRecord.TextLength > 50000) rtbRecord.Clear();

                this.rtbRecord.AppendText(mess + "\n");
                rtbRecord.SelectionStart = rtbRecord.TextLength;
                rtbRecord.ScrollToCaret();
            });

        }

        private void Btn循环_Click(object sender, EventArgs e)
        {
            string[] strs = new string[10];
            for (int i = 0; i < 10; i++) strs[i] = (i + 1).ToString("D2");


            Task.Factory.StartNew(() =>
            {
                showMess_Record("=======================开始==========================");


                for (int i = 0; i < strs.Length; i++)
                {
                    string mess = " ";
                    for (int n = i + 1; n < strs.Length; n++)
                    {
                        mess += strs[i] + "->" + strs[n] + " | ";
                    }

                    showMess_Record(mess);
                    System.Threading.Thread.Sleep(400);
                }

                showMess_Record("=======================结束==========================");
            });

        }

        private void Btn裁剪_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(ClipSHP);
        }

        void ClipSHP()
        {
            showMess_Record("=============开始==================");

            string in_features = @"D:\影像统筹数据处理工具\原始数据\循环裁图\gf1b_pms_e1054_n246_20190321_l1a1227607217_17.shp";
            string erase_features = @"D:\影像统筹数据处理工具\原始数据\循环裁图\gf1b_pms_e1056_n252_20190321_l1a1227607212_12.shp";
            string out_feature_class = @"D:\影像统筹数据处理工具\成果数据\循环裁图\CQ" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".shp";

            bool isok = false;
            string mess = "";

            isok = HelperArcGIS.PGTool.GPAnalysisTools.Erase(ref mess, in_features, erase_features, out_feature_class);
            if (!isok) showMess_Record(mess);

            showMess_Record("================结束==================");
        }

        private void Btn创建要数集_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(AddDataAggregate);
        }

        void AddDataAggregate()
        {
            showMess_Record("=============开始==================");

            bool isok = false;
            string mess = "";
            string gdbPath = @"D:\Projecct\ImageProcessing\data\NewGDB.gdb";

            string dataName = "TP" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string dataPath = gdbPath + @"\" + dataName;
            string dataName_topology = dataName + "_topology";

            string dataTopologyPath = dataPath + @"\" + dataName_topology;

            showMess_Record("创建要数数据集");
            IWorkspace workspace = HelperArcGIS.SupportFile.HelperGDB.OpenGDB(gdbPath);
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
            HelperArcGIS.SupportFile.HelperGDB.CreateDataset(ref mess, workspace, dataName, spatialReference);
            workspace = null;
            System.Threading.Thread.Sleep(1000);

            showMess_Record("复制数据");
            string input_Features = @"D:\Projecct\ImageProcessing\data\HB001.shp;";
            input_Features += @"D:\Projecct\ImageProcessing\data\HB002.shp;";
            isok = HelperArcGIS.PGTool.GPConversionTools.FeatureClassToGeodatabase(ref mess, input_Features, dataPath);
            if (!isok) { showMess_Record(mess); return; }
            System.Threading.Thread.Sleep(1000);

            showMess_Record("创建拓扑");
            isok = HelperArcGIS.PGTool.GPDataManagementTools.CreateTopology(ref mess, dataPath, dataName_topology);
            //isok = HelperArcGIS.PGTool.GPDataManagementTools.CreateTopology(ref mess, @"D:\Projecct\ImageProcessing\data\NewGDB.gdb\TP20190225151054", "TP20190225151054_topology");
            if (!isok) { showMess_Record(mess); return; }
            System.Threading.Thread.Sleep(1000);

            string[] inputFeatures = new string[] { dataPath + @"\HB001", dataPath + @"\HB002" };
            for (int i = 0; i < inputFeatures.Length; i++)
            {
                showMess_Record("向拓扑中添加要素" + i.ToString("D3"));
                isok = HelperArcGIS.PGTool.GPDataManagementTools.AddFeatureClassToTopology(ref mess, dataTopologyPath, inputFeatures[i]);
                if (!isok) { showMess_Record(mess); return; }
                System.Threading.Thread.Sleep(1000);
            }

            showMess_Record("拓扑验证");
            isok = HelperArcGIS.PGTool.GPDataManagementTools.ValidateTopology(ref mess, dataTopologyPath);
            if (!isok) { showMess_Record(mess); return; }
            else showMess_Record(mess);

            showMess_Record("================结束==================");
        }


        private void Form循环裁图工具_Load(object sender, EventArgs e)
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

        private void txt_源数据文件夹_TextChanged(object sender, EventArgs e)
        {
            btn读取文件_Click(btn读取文件, null);
        }

        private void txt_成果输出文件夹_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn开始生成_Click(object sender, EventArgs e)
        {
            this.foldersPath = this.txt_源数据文件夹.Text;
            this.foldersSavePath = this.txt_成果输出文件夹.Text;

            if (string.IsNullOrEmpty(foldersPath.Trim())) { MessageBox.Show("源数据文件夹" + "没有输入！"); return; }
            if (string.IsNullOrEmpty(foldersSavePath.Trim())) { MessageBox.Show("成果输出文件夹" + "没有输入！"); return; }

            save_recording();

            Task.Factory.StartNew(run);
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
            idc_rec_control.Add("WKID", this.txtWKID);

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

        Dictionary<string, ModelShpList> dic_shp = new Dictionary<string, ModelShpList>();
        List<string> px = new List<string>();
        Dictionary<string,string> dic_Eorror_name=new Dictionary<string,string>();

        private void btn读取文件_Click(object sender, EventArgs e)
        {
            dic_shp.Clear();            
            px.Clear();
            lstTIF.Items.Clear();
            dic_Eorror_name.Clear();

            this.foldersPath = this.txt_源数据文件夹.Text.Trim();

            if (string.IsNullOrEmpty(this.foldersPath)) return;
            try
            {


                //同名文件复制，循环处理
                string parentdir = System.IO.Path.GetDirectoryName(foldersPath);
                string[] files = System.IO.Directory.GetFiles(foldersPath, "*.shp");

                foreach (string fileold in files)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(fileold);
                    dic_shp.Add(filename, new ModelShpList { ShpName = filename, ShpPath = fileold });
                    px.Add(filename);
                }

                string temp = "";
                // 排序
                for (int i = 0; i < px.Count - 1; i++)
                {
                    for (int j = 0; j < px.Count - 1 - i; j++)
                    {
                        int px_j;
                        int px_index;
                        if (!int.TryParse(px[j].Substring(px[j].Length - 2), out px_index)) { MessageBox.Show("你的shp文件【" + dic_shp[px[i]].ShpPath + "】后面2位不是数字"); continue; }
                        if (!int.TryParse(px[j + 1].Substring(px[j + 1].Length - 2), out px_j)) { MessageBox.Show("你的shp文件【" + dic_shp[px[j]].ShpPath + "】后面2位不是数字"); continue; }
                        if (px_j < px_index)
                        {
                            temp = px[j];
                            px[j] = px[j + 1];
                            px[j + 1] = temp;
                        }
                    }
                }


                for (int i = 0; i < px.Count; i++)
                {
                    lstTIF.Items.Add(dic_shp[px[i]].ShpName);
                }

                showMess_Record("总计：" + px.Count.ToString() + "条数据");
            }
            catch (Exception ex)
            {
            }
        }


        private void run()
        {
            showMess_Record("=============开始==================");
            try
            {
                string temPath = System.Windows.Forms.Application.StartupPath + @"\data\tem" + DateTime.Now.ToString("yyyyMMddHHmmss");
                showMess_Record("-> 准备数据");

                if (!System.IO.Directory.Exists(temPath)) System.IO.Directory.CreateDirectory(temPath);

                // 数据检测
                for (int i = 0; i < px.Count; i++)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(dic_shp[px[i]].ShpPath);
                    if (filename.IndexOf('.') >= 0)
                    {
                        string filename_tem = filename.Replace('.', '_');
                        string out_path = ArcGISTool.ShapefileRead.CopyFile(dic_shp[px[i]].ShpPath, temPath, filename_tem);
                     
                        dic_shp.Remove(px[i]);
                        dic_shp.Add(filename_tem, new ModelShpList { ShpName = filename_tem, ShpPath = out_path });
                        px[i] = filename_tem;
                        dic_Eorror_name.Add(filename_tem, filename);
                    }
                }

                showMess_Record("-> 开始循环");
                bool isok = true;

                for (int i = 0; i < px.Count; i++)
                {
                    int start_index = px.Count - i - 1;
                    string start_name = dic_shp[px[start_index]].ShpName.Substring(0, dic_shp[px[start_index]].ShpName.Length - 3);



                    string out_path = foldersSavePath + "\\" + start_name + ".shp";

                    string shp_temPath = temPath + "\\" + start_name;
                    string in_features = dic_shp[px[start_index]].ShpPath;

                    if (start_index == 0)
                    {
                        showMess_Record("开始拷贝[" + (start_index + 1).ToString("D2") + "]:" + start_name);
                        out_path = ArcGISTool.ShapefileRead.CopyFile(in_features, foldersSavePath, start_name);
                        if (string.IsNullOrEmpty(out_path)) { isok = true; }
                    }
                    else
                    {
                        showMess_Record("开始裁切[" + (start_index + 1).ToString("D2") + "]:" + start_name);

                        for (int n = start_index; n > 0; n--)
                        {
                            int index = n - 1;
                            //string messinfo = "裁剪[" + px[i] + "]->[" + px[n] + "] ";
                            string messinfo = "裁剪" + (index + 1).ToString("D2");
                            string mess = "";

                            string erase_features = dic_shp[px[index]].ShpPath;
                            string out_feature_class = "";
                            if (index == 0)
                            {
                                out_feature_class = out_path;
                            }
                            else
                            {
                                out_feature_class = shp_temPath + "_" + n.ToString("D3") + ".shp";
                            }

                            isok = HelperArcGIS.PGTool.GPAnalysisTools.Erase(ref mess, in_features, erase_features, out_feature_class);
                            if (!isok)
                            {
                                showMess_Record(messinfo + mess);
                                return;
                            }
                            else
                            {
                                //showMess_Record(messinfo);
                                //showMess_Record(messinfo + "输出文件:" + out_feature_class);
                                in_features = out_feature_class;
                            }
                        }
                    }



                    if (isok)
                    {
                        showMess_Record("输出文件：" + out_path);
                    }
                }

                System.IO.Directory.Delete(temPath, true);

                // 修改错误的命名
                showMess_Record("修正文件【" + dic_Eorror_name.Count.ToString() + "】");
                foreach (KeyValuePair<string, string> kvp in dic_Eorror_name)
                {
                    string name_old = kvp.Key.Substring(0, kvp.Key.Length - 3);
                    string name_new= kvp.Value.Substring(0, kvp.Value.Length - 3);
                    string out_path_old = foldersSavePath + "\\" + name_old + ".shp";
                    //string out_path_new = foldersSavePath + "\\" + kvp.Value + ".shp";
                    string out_path = ArcGISTool.ShapefileRead.CopyFile(out_path_old, foldersSavePath, name_new);
                    ArcGISTool.ShapefileRead.DeleteSHP(foldersSavePath, name_old);
                }

                //showMess_Record("输出文件：");
            }
            catch (Exception ex)
            {
                showMess_Record("==================================\n错误：" + ex.Message);
            }
            showMess_Record("=============结束==================");
        }


        class ModelShpList
        {
            public string ShpName;
            public string ShpPath;
        }

        private void txt_成果输出文件夹_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txt_成果输出文件夹.Text = fbd.SelectedPath;
            }
        }
    }
}
