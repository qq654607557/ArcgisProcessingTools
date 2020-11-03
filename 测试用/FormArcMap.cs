using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ImageProcessing;
using System.Threading.Tasks;

namespace 测试用
{
    public partial class FormArcMap : Form
    {
        ClassMessRecord MessRecord = null;

        public FormArcMap()
        {
            InitializeComponent();
            MessRecord = new ClassMessRecord(this, this.rtbRecord);
        }

        private void FormArcMap_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "SHP文件(*.shp)|*.shp";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = dlg.FileName;
            }
        }

        private void btn_选择文件_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "SHP文件(*.shp)|*.shp";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string txt = "";
                string[] files = dlg.FileNames;
                for (int i = 0; i < files.Length; i++) txt += files[i] + ";";

                string path = txt;
                string[] paths = path.Split(';');
                for (int i = 0; i < paths.Length; i++)
                {
                    if (!string.IsNullOrEmpty(paths[i]))
                    {
                        open_shp(paths[i]);
                    }
                }
            }
        }

        private void btn_选取文件夹_Click(object sender, EventArgs e)
        {

        }

        Dictionary<string, string> dic_path = new Dictionary<string, string>();

        void open_shp(string shp_path)
        {
            string shp_filename = System.IO.Path.GetFileNameWithoutExtension(shp_path);
            string shp_directory = System.IO.Path.GetDirectoryName(shp_path);

            if (dic_path.ContainsKey(shp_filename))
            {
                MessageBox.Show("文件" + shp_filename + "已存在！");
            }
            else
            {
                dic_path.Add(shp_filename, shp_path);
                this.axMapControl1.AddShapeFile(shp_directory, shp_filename);
            }
        }

        List<string> read_px()
        {
            List<string> shp_path = new List<string>();

            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer iLayer = axMapControl1.get_Layer(i);
                string name = iLayer.Name;
                if (dic_path.ContainsKey(name)) { shp_path.Add(dic_path[name]); }
            }

            return shp_path;
        }

        string foldersSavePath = "";
        private void but循环裁剪_Click(object sender, EventArgs e)
        {
            foldersSavePath = this.textBox1.Text.Trim();
            if (string.IsNullOrEmpty(foldersSavePath))
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "SHP文件(*.shp)|*.shp";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foldersSavePath = dlg.FileName;
                    this.textBox1.Text = foldersSavePath;
                }
                else
                {
                    return;
                }
            }

            List<string> shp_path = read_px();
            Task.Factory.StartNew(run, shp_path);
        }

        private void run(object obj)
        {
            List<string> shp_path = (List<string>)obj;

            MessRecord.Record("=============开始==================");
            try
            {
                string temPath = System.Windows.Forms.Application.StartupPath + @"\data\tem" + DateTime.Now.ToString("yyyyMMddHHmmss");
                MessRecord.Record("-> 准备数据");

                if (!System.IO.Directory.Exists(temPath)) System.IO.Directory.CreateDirectory(temPath);


                MessRecord.Record("-> 开始循环");
                bool isok = true;
                string out_path = foldersSavePath;

                for (int i = 0; i < shp_path.Count; i++)
                {
                    int start_index = shp_path.Count - i - 1;
                    string start_name = System.IO.Path.GetFileNameWithoutExtension(shp_path[start_index]);
                    string in_features = shp_path[start_index];

                    if (start_index == 0)
                    {
                        string save_directory = System.IO.Path.GetDirectoryName(foldersSavePath);
                        string save_name = System.IO.Path.GetFileNameWithoutExtension(foldersSavePath);
                        MessRecord.Record("开始拷贝[" + (start_index + 1).ToString("D2") + "]:" + save_name);
                        out_path = ArcGISTool.ShapefileRead.CopyFile(in_features, save_directory, save_name);
                        if (string.IsNullOrEmpty(out_path)) { isok = true; }
                    }
                    else
                    {
                        MessRecord.Record("开始裁切[" + (start_index + 1).ToString("D2") + "]:" + start_name);

                        for (int n = start_index; n > 0; n--)
                        {
                            int index = n - 1;
                            string messinfo = "裁剪" + (index + 1).ToString("D2");
                            string mess = "";

                            string erase_features = shp_path[index];
                            string out_feature_class = "";
                            if (index == 0)
                            {
                                out_feature_class = out_path;
                            }
                            else
                            {
                                out_feature_class = temPath + "\\" + start_name  + n.ToString("D3") + ".shp"; ;
                            }

                            isok = HelperArcGIS.PGTool.GPAnalysisTools.Erase(ref mess, in_features, erase_features, out_feature_class);
                            if (!isok)
                            {
                                MessRecord.Record(messinfo + mess);
                                return;
                            }
                            else
                            {
                                in_features = out_feature_class;
                            }
                        }
                    }

                    if (isok)
                    {
                        MessRecord.Record("输出文件：" + out_path);
                    }
                }

                System.IO.Directory.Delete(temPath, true);

                //showMess_Record("输出文件：");
            }
            catch (Exception ex)
            {
                MessRecord.Record("==================================\n错误：" + ex.Message);
            }
            MessRecord.Record("=============结束==================");
        }


    }
}
