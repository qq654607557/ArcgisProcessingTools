using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using ImageProcessing;

namespace 精度检查工具
{

    public class excel精度检查run
    {
        public static void run(object sender, EventArgs e)
        {
            excel精度检查type etype = sender as excel精度检查type;
            excel精度检查run erun = new excel精度检查run();
            erun.MakeExcelAndExport(etype.title, etype.scene,etype.date,etype.checker,
                etype.reference, etype.check,//两个不知道干嘛用
                 etype.standard,// 检查标准
                 etype.reFeatureClass,etype.chFeatureClass,etype.ControlRecord,
                 etype.excelname ,etype.savepath );
        }

       /// <summary>
       /// 根据不同检查类型（单景、县成果、接边），确定参数值，调取此方法批量精度检查导出excel
       /// </summary>
       /// <param name="para_title">填excel标题名称</param>
       /// <param name="para_scene">填excel景号</param>
       /// <param name="para_reference">填excel参考序号</param>
       /// <param name="para_check">填excel检查序号</param>
       /// <param name="para_date">填excel检查日期</param>
       /// <param name="para_checker">填excel检查人员</param>
       /// <param name="para_standard">检查标准</param>
       /// <param name="para_excelname">excel文件名</param>
       /// <param name="para_savepath">excel保存路径</param>

        public void MakeExcelAndExport(string para_title, string para_scene, string para_date, string para_checker,
           string para_reference, string para_check,
           decimal para_standard,
              IFeatureClass reFeatureClass,   IFeatureClass chFeatureClass, ClassControlRecord ControlRecord,
            string para_excelname, string para_savepath)
       {
           try
           {
               Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();  //Excel的操作类
               Workbook bookDest = excel.Workbooks.Add(Missing.Value);
               Worksheet sheetDest = bookDest.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value) as Worksheet;  //添加一个sheet
               sheetDest.Name = "检查结果";  //表名
               for (int i = bookDest.Worksheets.Count; i > 1; i--)
               {
                   Worksheet wt = (Worksheet)bookDest.Worksheets[i];
                   if (wt.Name != "检查结果")
                   {
                       wt.Delete();  //删除多余表
                   }
               }
               sheetDest.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;  //单元格水平居中
               sheetDest.Cells.Font.Name = "宋体";  //设置字体类型
               sheetDest.Rows.RowHeight = 20;//设置行高

               Range title = (Range)sheetDest.get_Range("A1", "I1");//设置标题，填字，字体大小
               title.Merge(Missing.Value);
               title.Font.Size = 16;
               sheetDest.Cells[1, 1] = para_title;

               sheetDest.Cells[2, 1] = "景号";

               Range jinghao = (Range)sheetDest.get_Range("B2", "H2");//设置景号
               jinghao.Merge(Missing.Value);
               jinghao.Font.Size = 12;
               sheetDest.Cells[2, 2] = para_scene;//Path.GetFileNameWithoutExtension(tb_scene.Text);

               Range xuhao = (Range)sheetDest.get_Range("A3", "A4");
               xuhao.Merge(Missing.Value);
               xuhao.Font.Size = 12;
               sheetDest.Cells[3, 1] = "序号";

               Range cankao = (Range)sheetDest.get_Range("B3", "C3");
               cankao.Merge(Missing.Value);
               cankao.Font.Size = 12;
               sheetDest.Cells[3, 2] = para_reference;//Path.GetFileNameWithoutExtension(tb_reference.Text);

               Range jiuzheng = (Range)sheetDest.get_Range("D3", "E3");
               jiuzheng.Merge(Missing.Value);
               jiuzheng.Font.Size = 12;
               sheetDest.Cells[3, 4] = para_check;//Path.GetFileNameWithoutExtension(tb_change.Text);

               Range L2 = (Range)sheetDest.get_Range("H3", "H4");
               L2.Merge(Missing.Value);
               L2.Font.Size = 12;
               sheetDest.Cells[3, 8] = "⊿L²";

               Range L = (Range)sheetDest.get_Range("I3", "I4");
               L.Merge(Missing.Value);
               L.Font.Size = 12;
               sheetDest.Cells[3, 9] = "⊿L";

               sheetDest.Cells[3, 6] = "⊿X";
               sheetDest.Cells[3, 7] = "⊿Y";
               sheetDest.Cells[4, 2] = "X(m)";
               sheetDest.Cells[4, 3] = "Y(m)";
               sheetDest.Cells[4, 4] = "X(m)";
               sheetDest.Cells[4, 5] = "Y(m)";
               sheetDest.Cells[4, 6] = "(m)";
               sheetDest.Cells[4, 7] = "(m)";

               //输入影像数据
               //IFeatureClass reFeatureClass = readshpfeatureclass(para_referencepath);   //参考影像fc
               //IFeatureClass chFeatureClass = readshpfeatureclass(para_checkpath);  //检查影像fc

               IFeatureCursor rFeatureCursor = reFeatureClass.Search(null, false);
               IFeatureCursor cFeatureCursor = chFeatureClass.Search(null, false);
               IFeature rFeature = rFeatureCursor.NextFeature();
               IFeature cFeature = cFeatureCursor.NextFeature();
               List<double> chafang = new List<double>();
               List<double> cf_sqrl = new List<double>();//记录差方开根值

               int x = reFeatureClass.Fields.FindField("POINT_X");  //x属性索引
               int y = reFeatureClass.Fields.FindField("POINT_Y");  //y属性索引

               int j = 5;
               while (rFeature != null && cFeature != null)
               {
                   double re_X = Convert.ToDouble(rFeature.get_Value(x));  //参考影像X坐标
                   double re_y = Convert.ToDouble(rFeature.get_Value(y));  //参考影像Y坐标
                   double ch_x = Convert.ToDouble(cFeature.get_Value(x));  //检查影像X坐标
                   double ch_y = Convert.ToDouble(cFeature.get_Value(y));  //检查影像Y坐标
                   sheetDest.Cells[j, 1] = (j - 4).ToString();

                   //设置小数点显示位数
                   string k = j.ToString();
                   Range r = (Range)sheetDest.get_Range("B" + k, "I" + k);
                   r.NumberFormat = "0.00000";

                   sheetDest.Cells[j, 2] = re_X.ToString();
                   sheetDest.Cells[j, 3] = re_y.ToString();
                   sheetDest.Cells[j, 4] = ch_x.ToString();
                   sheetDest.Cells[j, 5] = ch_y.ToString();
                   sheetDest.Cells[j, 6] = (re_X - ch_x).ToString();  //X差值
                   sheetDest.Cells[j, 7] = (re_y - ch_y).ToString();  //Y差值
                   sheetDest.Cells[j, 8] = Square(re_X, re_y, ch_x, ch_y).ToString(); //差方和
                   sheetDest.Cells[j, 9] = Math.Sqrt(Square(re_X, re_y, ch_x, ch_y)).ToString();//差方和开根

                   chafang.Add(Square(re_X, re_y, ch_x, ch_y));
                   cf_sqrl.Add(Math.Sqrt(Square(re_X, re_y, ch_x, ch_y)));

                   rFeature = rFeatureCursor.NextFeature();  //遍历参考影像feature
                   cFeature = cFeatureCursor.NextFeature();  //遍历检查影像feature
                   j++;
                   if (rFeature == null && cFeature != null || rFeature != null && cFeature == null)
                   {
                       MessageBox.Show("请确认参考影像与检查影像属性表数目相同！", "警告！", MessageBoxButtons.OK);
                   }
               }
               int num = j - 5;//数据条数
               double sumary = Sum(chafang);//记录差方和总值
               double sum = Sum(cf_sqrl);//记录差方和开根总值
               double zhongwucha = Save2(Math.Sqrt(sumary / num));//计算中误差

               Range heji = (Range)sheetDest.get_Range("A" + j.ToString(), "G" + j.ToString());
               heji.Merge(Missing.Value);
               heji.Font.Size = 12;
               sheetDest.Cells[j, 1] = "合计";
               sheetDest.Cells[j, 8] = Save2(sumary);

               Range result = (Range)sheetDest.get_Range("A" + (j + 1).ToString(), "H" + (j + 1).ToString());
               result.Font.Size = 12;
               Range wucha = (Range)sheetDest.get_Range("A" + (j + 1).ToString(), "C" + (j + 1).ToString());
               wucha.Merge(Missing.Value);
               sheetDest.Cells[j + 1, 1] = "中误差 M=±sqrt(Σ[ΔL²]/N)";
               sheetDest.Cells[j + 1, 4] = "±" + zhongwucha.ToString();
               sheetDest.Cells[j + 1, 5] = "最大值 M=MAX(ΔL)";
               sheetDest.Cells[j + 1, 6] = Save2(Sort(chafang)).ToString(); //最大L方开方
               sheetDest.Cells[j + 1, 7] = "平均值 S=AVERAGE(ΔL)";
               sheetDest.Cells[j + 1, 8] = Save2(sum / num).ToString();

               Range set = (Range)sheetDest.get_Range("A" + (j + 1).ToString(), "H" + (j + 1).ToString());
               set.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightGray);//背景色设置
               set.Font.Bold = true;//字体加粗
               ((Range)sheetDest.Rows[j + 1]).RowHeight = 36;//单独一行设置高度

               Range kongbai = (Range)sheetDest.get_Range("I" + j.ToString(), "I" + (j + 1).ToString());
               kongbai.Merge(Missing.Value);

               Range jielun = (Range)sheetDest.get_Range("A" + (j + 2).ToString(), "C" + (j + 2).ToString());
               Range hege = (Range)sheetDest.get_Range("D" + (j + 2).ToString(), "I" + (j + 2).ToString());
               jielun.Merge(Missing.Value);
               jielun.Font.Size = 12;
               hege.Merge(Missing.Value);
               hege.Font.Size = 12;
               sheetDest.Cells[j + 2, 1] = "结论";
               if (Sort(chafang) <(double)para_standard) //if (Sort(chafang) < Convert.ToDouble(para_standard))
               {
                   sheetDest.Cells[j + 2, 4] = "合格";
               }
               else
               {
                   sheetDest.Cells[j + 2, 4] = "不合格";
               }

               Range check = (Range)sheetDest.get_Range("A" + (j + 3).ToString(), "C" + (j + 3).ToString());
               Range checkdate = (Range)sheetDest.get_Range("D" + (j + 3).ToString(), "I" + (j + 3).ToString());
               check.Merge(Missing.Value);
               check.Font.Size = 12;
               checkdate.Merge(Missing.Value);
               checkdate.Font.Size = 12;
               sheetDest.Cells[j + 3, 1] = "检查员：" + para_checker;
               sheetDest.Cells[j + 3, 4] = "检查日期：" + para_date;

               //设置数据区边框为实线
               Range borderline = (Range)sheetDest.get_Range("A1", "I" + (j + 3).ToString());
               borderline.Borders.LineStyle = 1;

               sheetDest.Columns.EntireColumn.AutoFit();//列宽自适应          
               bookDest.Saved = true;
               string excel_savepath = para_savepath + "\\" + para_excelname + ".xlsx";
               bookDest.SaveCopyAs(excel_savepath);  //保存在shp同目录下
               excel.Quit();
               excel = null;
               GC.Collect();  //垃圾回收  
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "提示！");
           }
       }


       /// <summary>
       /// 读取shp文件的featureclass
       /// </summary>
       /// <param name="aFileName"></param>
       /// <returns></returns>
       public IFeatureClass readshpfeatureclass(string aFileName)
       {
           string fullPath;
           string path;
           string fileName;
           IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
           fullPath = aFileName;
           path = System.IO.Path.GetDirectoryName(fullPath);
           fileName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
           IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(path, 0);
           IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
           IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(fileName);
           return pFeatureClass;
       }

       /// <summary>
       /// 结果保留两位小数
       /// </summary>
       /// <param name="point"></param>
       /// <returns></returns>
       public double Save2(double point)
       {
           string str = point.ToString("f2");
           double deal = Convert.ToDouble(str);
           return deal;
       }

       /// <summary>
       /// 计算差方
       /// </summary>
       /// <param name="x1"></param>
       /// <param name="y1"></param>
       /// <param name="x2"></param>
       /// <param name="y2"></param>
       /// <returns></returns>
       public double Square(double x1, double y1, double x2, double y2)
       {
           double sum = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
           return sum;
       }

       /// <summary>
       /// 计算最大差方开方值
       /// </summary>
       /// <param name="jihe"></param>
       /// <returns></returns>
       public double Sort(List<double> jihe)
       {
           double A = jihe[0];
           for (int i = 1; i < jihe.Count; i++)
           {
               double B = jihe[i];
               if (B > A)
               {
                   A = B;
               }
           }
           double sr = Math.Sqrt(A);
           return sr;
       }

       /// <summary>
       /// 计算差方和
       /// </summary>
       /// <param name="jihe"></param>
       /// <returns></returns>
       public double Sum(List<double> jihe)
       {
           double sum = 0;
           for (int i = 0; i < jihe.Count; i++)
           {
               sum = sum + jihe[i];
           }
           return sum;
       }
    }
}
