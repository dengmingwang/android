using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 干式荧光免疫实验管理软件.Common_Method;

namespace POCT实验室管理软件.Common_Method
{
    class Common_use
    {
        WordConvertToBytes contobyte = new WordConvertToBytes();
        #region  获取表格中选中的浓度值以及反应值
        /// <summary>
        /// 获取表格中选中的浓度值以及反应值
        /// </summary>
        /// <param name="j">单元格索引</param>
        /// <param name="dtv">表格名称</param>
        /// <returns></returns>
        public List<double> GetCheckValue(int j, DataGridView dtv)
        {
            List<double> str_double = new List<double>();
            try
            {
                for(int i=0;i<dtv.Rows.Count;i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dtv.Rows[i].Cells["select"];
                    Boolean flag = Convert.ToBoolean(checkCell.Value);
                    if(flag)
                    {
                        str_double.Add(Convert.ToDouble(dtv.Rows[i].Cells[j].Value));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return str_double;
        }
        #endregion

        #region  绘制曲线 拟合浓度值与反应值时的方法直线拟合专用点图
        /// <summary>
        /// 绘制曲线 拟合浓度值与反应值时的方法
        /// </summary>
        /// <param name="X">X坐标值</param>
        /// <param name="Y">Y坐标值</param>
        /// <param name="color">曲线颜色</param>
        /// <param name="n">要绘制的第几条曲线</param>
        /// <param name="strname">曲线名称</param>
        /// <param name="chart1">曲线表
        /// </param>

        public void DrawFitLine(double[] X, double[] Y, Color color, string strname,Chart chart1)
        {
            int count = X.Length;
            var chart = chart1.ChartAreas[0];
            //设置坐标轴样式
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = Math.Round((X.Min() - 0.1),2);
            chart.AxisX.Maximum = Math.Round((X.Max() + 0.5),2);
            chart.AxisY.Minimum = Math.Round((Y.Min() - 0.1),2);
            chart.AxisY.Maximum = Math.Round((Y.Max() + 0.5),2);
            chart.AxisX.Interval = Math.Round((X.Max() - X.Min()) / 8, 2);
            chart.AxisY.Interval = Math.Round((Y.Max() - Y.Min()) / 8, 2);
            chart.BackColor = System.Drawing.Color.Transparent;//设置区域内背景透明
            int n1 = chart1.Series.Count;
            string str = n1.ToString();
            chart1.Series.Add(str);
            //绘制曲线图
            chart1.Series[str].ChartType = SeriesChartType.Point;
            chart1.Series[str].Color = color;        
            for (int i = 0; i < count; i++)
            {
                chart1.Series[str].Points.AddXY(X[i], Y[i]);
            }
        }
        #endregion

        #region 绘制拟合曲线折线图
        public void DrawLineofCal(double[] X,double[] Y,Color color,Chart chart1)
        {
            int count = X.Length;
            var chart = chart1.ChartAreas[0];
            //设置坐标轴样式
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = Math.Round((X.Min() - 0.1), 2);
            chart.AxisX.Maximum = Math.Round((X.Max() + 0.5), 2);
            chart.AxisY.Minimum = Math.Round((Y.Min() - 0.1), 2);
            chart.AxisY.Maximum = Math.Round((Y.Max() + 0.5), 2);
            chart.AxisX.Interval = Math.Round((X.Max() - X.Min()) / 8, 2);
            chart.AxisY.Interval = Math.Round((Y.Max() - Y.Min()) / 8, 2);
            chart.BackColor = System.Drawing.Color.Transparent;//设置区域内背景透明
            int n1 = chart1.Series.Count;
            string str = n1.ToString();
            chart1.Series.Add(str);
            //绘制曲线图
            chart1.Series[str].ChartType = SeriesChartType.Line;
            chart1.Series[str].Color = color;
            for (int i = 0; i < count; i++)
            {
                chart1.Series[str].Points.AddXY(X[i], Y[i]);
            }
        }
        #endregion

        #region 绘制曲线 接收到的数据绘制曲线
        public void DrawLine(string[] data, Color color, Chart chart1)
        {
            int[] output = SortArray(data);          
            int interval = GetInterval(output);
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.AxisX.MajorGrid.LineColor = Color.Gray;
            chart.AxisY.MajorGrid.LineColor = Color.Gray;
            chart.AxisX.Minimum = 0;
            chart.AxisX.Maximum = 390;
            chart.AxisY.Maximum = output[output.Length-1]+500;
            chart.AxisY.Minimum = output[0]-50;
            chart.AxisX.Interval = 20;
            chart.AxisY.Interval = interval;
            int count = output.Length;
            int y = chart1.Series.Count;          
            string str = y.ToString();
            chart1.Series.Add((Convert.ToInt32(str)+1).ToString());
            chart1.Series[y].ChartType = SeriesChartType.Spline;
            chart1.Series[y].Color = color;
            for(int i=0;i<count;i++)
            {
                float x = float.Parse(data[i]);
                chart1.Series[y].Points.AddXY(i, x);
            }
        }
        #endregion

        #region 数值变换
        /// <summary>
        /// 数值变换
        /// </summary>
        /// <param name="str">数据源</param>
        /// <param name="cobotext">数值变换方法</param>
        /// <returns></returns>
        public double[] NumConvert(List<double> str, string cobotext)
        {
            List<double> str1 = new List<double>();
            double[] str2 = new double[str.Count];
            if (cobotext == "无变换")
            {
                for (int i = 0; i < str.Count; i++)
                {
                    str1.Add(str[i]);
                }
                str2 = (double[])str1.ToArray();
            }
            else if (cobotext == "取对数")
            {
                for (int i = 0; i < str.Count; i++)
                {
                    str1.Add(Math.Log10(str[i]));
                }
                str2 = (double[])str1.ToArray();
            }
            else if (cobotext == "自然对数")
            {
                for (int i = 0; i < str.Count; i++)
                {
                    str1.Add(Math.Log(str[i]));
                }
                str2 = (double[])str1.ToArray();
            }
            else if (cobotext == "底为2对数")
            {
                for (int i = 0; i < str.Count; i++)
                {
                    str1.Add(Math.Log(str[i], 2));
                }
                str2 = (double[])str1.ToArray();
            }
            return str2;
        }
        #endregion

        #region DataGridView中的数据导入Excel
        //DataGridView中的数据导入Excel
        /// <summary>
        /// DataGridView中的数据导入Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="myDGV"></param>
        public void ExportExcels(string fileName, DataGridView myDGV)
        {
            string saveFilename = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFilename = saveDialog.FileName;
            if (saveFilename.IndexOf(":") < 0) return;
            Microsoft.Office.Interop.Excel._Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//获取excel的sheet1
            //写入标题
            for (int i = 0; i <= myDGV.ColumnCount - 1; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFilename != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFilename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错，文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("文件:" + fileName + ".xls保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Excel中的数据读取到DataGridView
        //Excel中的数据读取到DataGridView
        /// <summary>
        /// Excel中的数据读取到DataGridView
        /// </summary>
        /// <returns></returns>
        public DataSet getData()
        {
            //打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel files office2003(*.xls)|*.xls|Excel office2010(*.xlsx)|*.xlsx";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                return null;
            //判断文件后缀
            var path = file.FileName;
            string filesuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(filesuffix))
                return null;
            using (DataSet ds = new DataSet())
            {
                //判断excel文件的版本
                string connString = "";
                //if (filesuffix == ".xls")
                //    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
                ////connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + path + ";" + ";Extend Properties = 'Excel 8.0; HDR = No; IMEX = 1;'";
                //else
                    //connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source = " + path + ";" + ";Extend Properties = \"Excel 12.0; HDR = No; IMEX = 1 \"";
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No\"";
                //读取文件
                string sql_select = "SELECT * FROM[Sheet1$]";
                using (OleDbConnection conn = new OleDbConnection(connString))
                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {
                    NewMethod1(conn);
                    cmd.Fill(ds);
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }
        }
        #endregion

        #region 建立与excel的链接
        //建立与excel的链接
        /// <summary>
        /// 建立与excel的链接
        /// </summary>
        /// <param name="conn"></param>
        private static void NewMethod1(OleDbConnection conn)
        {
            conn.Open();
        }
        public DataSet NewMethod()
        {
            return getData();
        }
        #endregion

        #region 延迟
        //延迟函数
        /// <summary>
        /// 延迟函数
        /// </summary>
        /// <param name="mm"></param>
        public  void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm * 1000) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }
        #endregion

        #region 求数组的最大值和最小值 SortArray 从小到大排列
        //求数组的最大和最小值
        /// <summary>
        /// 求数组的最大值和最小值
        /// </summary>
        /// <param name="stringdata">数据源</param>
        /// <returns></returns>
        public int[] SortArray(string[] stringdata)
        {
            int[] output = Array.ConvertAll<string, int>(stringdata, delegate(string s) { return int.Parse(s); });
            Array.Sort(output);
            return output;
        }
        #endregion

        #region 求Y轴间隔
        //求Y轴的间隔
        /// <summary>
        /// Y轴间隔
        /// </summary>
        /// <param name="input">数据源</param>
        /// <returns></returns>
        public int GetInterval(int[] input)
        {
            int count = input.Length;
            int space = (input[count - 1] - input[0]) / 30;
            return space;
        }
        #endregion

        #region 向表格中添加新列
        //datagridview添加新列
        /// <summary>
        /// 向表格中添加列
        /// </summary>
        /// <param name="num">需要添加的列的数量</param>
        /// <param name="dtv">需要添加列的表格</param>
        public void AddColumn(int num,DataGridView dtv)
        {
            for(int i=1;i<=num;i++)
            {
                DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();
                acCode1.Name = i.ToString();
                acCode1.DataPropertyName = i.ToString();
                acCode1.HeaderText = " ";
                if (!dtv.Columns.Contains(i.ToString()))
                {
                    dtv.Columns.Add(acCode1);
                }
            }
        }
        #endregion

        #region 求均值、标准差以及CV值
        //求均值、标准差和CV值
        public double[] MeanAndSd(double[] stringdata)
        {
            int n = stringdata.Length;
            double sum = 0;
            double data_mean = stringdata.Average();
            for (int i = 0; i < n; i++)
            {
                sum = sum + (stringdata[i] - data_mean) * (stringdata[i] - data_mean);
            }
            double data_sd = Math.Sqrt(sum / (n - 1));
            double percent = data_sd / data_mean;
            double[] result = new double[3];
            result[0] = data_mean;//平均值
            result[1] = data_sd;//标准差
            result[2] = percent;//CV值
            return result;
        }
        #endregion

        #region 求数据峰值 可根据峰值大致坐标位置数组进行判断峰的个数
        //查找接收到的数据的峰值
        /// <summary>
        /// 查找曲线峰值
        /// </summary>
        /// <param name="strdata"> 接收到的数据</param>
        /// <param name="index">峰值大致坐标位</param>
        /// <returns></returns>
        public double[] GetPeakPointOfData(string[] strdata, List<string[]> index)
        {
            double[] peak = new double[(index.Count() * 2)];
            double[] sourcedata = new double[strdata.Length];
            int peakindex = 0;
            for (int i = 0; i < index.Count; i++)
            {
                for (int j = 0; j < strdata.Length; j++)
                {
                    if (j >= Convert.ToInt32(index[i][0]) && j <= Convert.ToInt32(index[i][1]))
                    {
                        sourcedata[j] = Convert.ToDouble(strdata[j]);
                    }
                    else
                    {
                        sourcedata[j] = 0;
                    }
                }
                peak[peakindex] = sourcedata.Max();//峰值
                peak[peakindex + 1] = sourcedata.ToList().IndexOf(peak[peakindex]);//峰值索引
                peakindex = peakindex + 2;
                Array.Clear(sourcedata, 0, sourcedata.Length);
            }
            return peak;
        }
        #endregion

        #region 字符串转换为datatable
        public static DataTable Converttodatatable(string ColumnName, string[] Array)
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn(ColumnName, Type.GetType("System.String"));
            dt.Columns.Add(dc1);
            for (int i = 0; i <= Array.Length - 1; i++)
            {
                DataRow dr = dt.NewRow();
                string a = Array[i].ToString();
                dr[dc1] = a;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region 将数据赋值给datagridview的指定列
        //将指定列赋值给datagridview
        /// <summary>
        /// 将数据赋值给datagridview的指定列
        /// </summary>
        /// <param name="x"></param>
        /// <param name="stringdata"></param>
        /// <param name="dtv"></param>
        public void DataGridViewGetValue(string[] datasource, DataGridView dtv)
        {         
            int col = dtv.ColumnCount;
            string columnname =(col-1).ToString();           
            string nowcolumn = col.ToString();
            bool columnexist = dtv.Columns.Contains(columnname);
            if (columnexist)
            {
                DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                acCode.Name = nowcolumn;
                acCode.DataPropertyName = nowcolumn;
                acCode.HeaderText = (Convert.ToInt32(nowcolumn)+1).ToString();
                dtv.Columns.Add(acCode);
                for (int i = 0; i < datasource.Length; i++)
                {
                    string a = datasource[i];
                    dtv.Rows[i].Cells[Convert.ToInt32(nowcolumn)].Value = a;
                }
            }
            else
            {
                DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                acCode.Name = nowcolumn;
                acCode.DataPropertyName = nowcolumn;
                acCode.HeaderText = (Convert.ToInt32(nowcolumn) + 1).ToString();
                dtv.Columns.Add(acCode);
                for (int i = 0; i < datasource.Length; i++)
                {
                    i = dtv.Rows.Add();
                    string a = datasource[i];
                    dtv.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    dtv.Rows[i].Cells[Convert.ToInt32(nowcolumn)].Value = a;
                }
            }
        }
        #endregion

        #region 处理串口接收到的数据并保存到List<string[]>中
        //处理串口接收到的数据
        /// <summary>
        /// 处理串口接收到的数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public string[] Dataprocess(byte[] buffer)
        {
            int length = buffer.Count();
            string[] datastr = new string[762];
            string a;
            string b;
            string mid;
            double c;
            double d;
            double e;
            List<string> list = new List<string>();         
            for (int i = 1; i <= length - 2; i = i + 2)
            {
                a = buffer[i].ToString();
                b = buffer[i + 1].ToString();
                c = double.Parse(a);//高位
                d = double.Parse(b);
                e = c * 256 + d;
                mid = e.ToString();
                datastr[i] = mid;
            }
            datastr.ToList().ForEach(
                (s) =>
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        list.Add(s);
                    }
                }
                );
            datastr = list.ToArray();
            return datastr;
        }
        #endregion

        #region 遍历控件得到textbox、comboBox、CheckBox以及datagridview并设置其Enabled属性
        public void GetControl(Control frm,bool str)
        {
            foreach (Control ctl in frm.Controls)
            {
                if(ctl is TextBox)
                {
                    TextBox tb = ctl as TextBox;
                    tb.Enabled = str;
                }
                else if(ctl is DataGridView)
                {
                    DataGridView dtv = ctl as DataGridView;
                    dtv.Enabled = str;
                }
                else if(ctl is ComboBox)
                {
                    ComboBox cmb = ctl as ComboBox;
                    cmb.Enabled = str;
                }
                else if(ctl is CheckBox)
                {
                    CheckBox ckb = ctl as CheckBox;
                    ckb.Enabled = str;
                }
            }
        }
        #endregion
        
        #region 获取指定文件夹下的指定文件类型并显示在指定的datagridview中
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="str1">指定路径</param>
        /// <param name="str2">指定文件类型</param>
        /// <param name="dtv">需要进行显示的表格</param>
        public bool FindFileType(string str1,string str2,DataGridView dtv)
        {
            DirectoryInfo dir = new DirectoryInfo(str1);
            FileInfo[] inf = dir.GetFiles();
            int index = 0;
            bool exist;
            if(inf.Length>0)
            {
                foreach (FileInfo finf in inf)
                {
                    if (finf.Extension.Equals(str2))
                    {
                        index = dtv.Rows.Add();
                        dtv.Rows[index].Cells[0].Value = finf.Name.Replace(str2, "");                   
                    }
                }
                exist = true;
            }
            else
            {
                exist = false;
            }
            return exist;
        }
        #endregion

        #region 查找文件夹中是否有某文件
        public bool FindFile(string strpath,string strname)
        {
            DirectoryInfo dir = new DirectoryInfo(strpath);
            FileInfo[] inf = dir.GetFiles();
            bool exist = false;
            if (inf.Length > 0)
            {
                foreach (FileInfo finf in inf)
                {
                    if (finf.FullName.Equals(strname))
                    {
                        exist = true;
                    }
                }
            }
            else
            {
                exist = false;
            }
            return exist;
        }
        #endregion

        #region 文件夹中是否包含文件
        public bool FileExist(string str1, string str2)
        {         
            bool exist;
            if (System.IO.Directory.GetFiles(str1,"*"+str2).Length > 0)
            {
                exist = true;           
            }
            else
            {
                exist = false;
            }
            return exist;
        }
        #endregion

        #region 写BAS文件
        public void WriteBasFile(string pathstr, List<itemparam> param, List<Sample> sam,
            List<SubItemParam> subitemparam, List<SampleCoefficient> samcoeffi, List<PeakValue> peak,
            List<GroupOutPut> group)
        {
            using(StreamWriter writer = new StreamWriter(pathstr))
            {
                for(int i=0;i<param.Count;i++)//大项目参数
                {
                    writer.WriteLine(@param[i].Itemname);//项目名称
                    writer.WriteLine(@param[i].Itemnum);//项目代码
                    writer.WriteLine(@param[i].devicetype);//仪器类型
                    writer.WriteLine(@param[i].subitemnum);//子项目个数
                    writer.WriteLine(@param[i].pre_readtime);//预读时间
                    writer.WriteLine(@param[i].testtime);//测试时间
                    writer.WriteLine(@param[i].customsecddilution);//是否自定义二次稀释
                    writer.WriteLine(@param[i].judgeaddsam);//是否判断未加样
                    writer.WriteLine(@param[i].peakvalue_num);//峰个数
                    writer.WriteLine(@param[i].secondary_buffer);//二次缓冲液量
                    writer.WriteLine(@param[i].judgeaddsam_value);//预读判断阈值
                    writer.WriteLine(@param[i].referencepeak);//基准峰
                    writer.WriteLine(@param[i].secondary_mixture);//二次混合液量
                    writer.WriteLine(@param[i].peaknumber);//峰序号
                    writer.WriteLine(@param[i].methodofgetpeak);//取峰算法
                }
                for(int i=0;i<sam.Count;i++)//样本类型、加样量、缓冲液量以及混合液量
                {
                    writer.WriteLine(@sam[i].samtype + "\t" + sam[i].samvalue + 
                        "\t" + sam[i].buffer_value + "\t" + sam[i].mixture_value);
                }
                for(int i=0;i<subitemparam.Count;i++)//子项目参数
                {
                    writer.WriteLine(@subitemparam[i].segmentcount + "\t"+ subitemparam[i].decimalplace + "\t" +
                        subitemparam[i].coefficient_decimalplace + "\t" + subitemparam[i].subitem_num + "\t" +
                        subitemparam[i].subitem_name + "\t" + subitemparam[i].subitem_unit + "\t" +
                        subitemparam[i].subitem_min + "\t" + subitemparam[i].subitem_max + "\t" +
                        subitemparam[i].subitem_P1 + "\t" + subitemparam[i].subitem_P2 + "\t" +
                        subitemparam[i].subitem_P3 + "\t" + subitemparam[i].subitem_TCformula + "\t" +
                        subitemparam[i].subitem_doubleTC );
                }
                for(int i=0;i<samcoeffi.Count;i++)//子项目的样本系数
                {
                    writer.WriteLine(samcoeffi[i].sampletype + "\t" + samcoeffi[i].samcoeffi+"\t"+samcoeffi[i].samcoeffi_decimalplace);
                }
                for(int i=0;i<peak.Count;i++)//峰值
                {
                    writer.WriteLine(peak[i].peaknum + "\t" + peak[i].peak_start +
                        "\t"+peak[i].peak_end + "\t" + peak[i].peaknumber + "\t");
                }
                for(int i=0;i<group.Count;i++)//组合输出
                {
                    writer.WriteLine(group[i].group_num + "\t" + group[i].group_name +
                        "\t"+group[i].group_unit + "\t" + group[i].group_decimalplace +
                        "\t"+group[i].group_min + "\t" + group[i].group_max +
                        "\t"+group[i].rangedecimals + "\t" + group[i].v0 +
                        "\t"+group[i].calculationformula);
                }
                writer.Flush();
                writer.Close();
            }
        }
        #endregion

        #region 读BAS文件
        public List<string> ReadBasFile(string pathstr)
        {
            List<string> result = new List<string>();
            using(StreamReader reader = new StreamReader(pathstr))
            {
                string line;
                while((line=reader.ReadLine())!=null)
                {
                    result.Add(line);
                }
            }
            return result;
        }
        #endregion

        #region 删除指定路径下的文件
        public void DeleteOneFile(string fileFullPath)
        {
           try
           {
               if (File.Exists(fileFullPath))
               {
                   FileAttributes attr = File.GetAttributes(fileFullPath);
                   if (attr == FileAttributes.Directory)
                   {
                       Directory.Delete(fileFullPath, true);
                   }
                   else
                   {
                       File.Delete(fileFullPath);
                   }
               }
           }
           catch(Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
        }
        #endregion

        #region 文件属性
        public string FileName_jiami { get; set; }
        #endregion

        #region 查找List<string>中特定符号的个数以及所在位置以及在表格中的索引
        public List<string> FindSignInString(List<string> datasource,int start,char sign,int sign_num)
        {
            List<string> indexs = new List<string>();
            for(int i=start;i<datasource.Count;i++)
            {
                int num = datasource[i].Count(f=>(f==sign));
                if (num == sign_num)
                {
                    indexs.Add(datasource[i]);
                }
            }
            return indexs;
        }
        #endregion

        #region 将string赋值给datagridview（峰值）
        public void AddDatagridview(List<string> datasource,DataGridView dgv)
        {
            if(dgv.Rows.Count==datasource.Count)
            {
                for (int i = 0; i < datasource.Count; i++)
                {
                    string[] strArray = datasource[i].Split('\t');
                    for(int j=0;j<strArray.Length;j++)
                    {
                       if(strArray[j]!= "")
                       {
                           dgv.Rows[i].Cells[j].Value = strArray[j];
                       }
                    }
                }
            }
            else
            {
                dgv.Rows.Clear();
                for (int i = 0; i < datasource.Count; i++)
                {
                    i = dgv.Rows.Add();
                    string[] strArray = datasource[i].Split('\t');
                    for (int j = 0; j < strArray.Length-1; j++)
                    {                      
                        dgv.Rows[i].Cells[j].Value = strArray[j];
                    }
                }
            }
        }
        #endregion

        #region 将string值赋值给样本类型表格
        public void AddSamDatagridview(List<string> datasource, DataGridView dgv)
        {
            if (dgv.Rows.Count == datasource.Count)
            {
                for (int i = 0; i < datasource.Count; i++)
                {
                    string[] strArray = datasource[i].Split('\t');
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (strArray[j] != "")
                        {
                            dgv.Rows[i].Cells[j].Value = strArray[j];
                        }
                    }
                }
            }
            else
            {
                dgv.Rows.Clear();
                for (int i = 0; i < datasource.Count; i++)
                {
                    i = dgv.Rows.Add();
                    string[] strArray = datasource[i].Split('\t');
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        dgv.Rows[i].Cells[j].Value = strArray[j];
                    }
                }
            }
        }
        #endregion

        #region 将string值赋给组合输出的表格
        public void AddDataToGroup(List<string> datasource,DataGridView dgv)
        {
            for(int i=0;i<datasource.Count;i++)
            {
                string[] strArray = datasource[i].Split('\t');
                for(int j=1;j<strArray.Length;j++)
                {
                    dgv.Rows[j-1].Cells[i+1].Value = strArray[j];
                }
            }
        }
        #endregion

        #region 将子项参数分离出来
        public List<string[]> SeparateSubItem(List<string> datasource,int start,
            char sign,int num)
        {
            List<string> data_or = new List<string>();
           data_or = FindSignInString(datasource, start, sign, num);
           List<string[]> subitem = new List<string[]>();
           for (int i = 0; i < data_or.Count; i++)
           {
               string[] strArray = data_or[i].Split('\t');
               subitem.Add(strArray);
           }
           return subitem;
        }
        #endregion

        #region 分离出峰值位置
        public List<string[]> SeparatePeakpos(List<string> datasource,int start,
            char sign,int num)
        {
            List<string> data_or = new List<string>();
            data_or = FindSignInString(datasource, start, sign, num);
            List<string[]> peak = new List<string[]>();
            for (int i = 0; i < data_or.Count; i++)
            {
                string[] strArray = data_or[i].Split('\t');
                peak.Add(strArray);
            }
            return peak;
        }
        #endregion

        #region 将string值赋给子项参数的表格
        public List<string[]> AddDataToSubItem(List<string>datasource,DataGridView dgv1,DataGridView dgv2,
            DataGridView dgv3,DataGridView dgv4,DataGridView dgv5)
        {
            List<string[]> subitem = new List<string[]>();
            for(int i=0;i<datasource.Count;i++)
            {
                string[] strArray = datasource[i].Split('\t');
                subitem.Add(strArray);
            }
            for(int i = 0;i < subitem.Count; i++)
            {
                switch(i)
                {
                    case 0:
                        for (int j = 0; j < subitem[i].Length - 9; j++)
                        {
                            dgv1.Rows[0].Cells[j+1].Value = subitem[i][j + 4];
                        }
                        break;
                    case 1:
                        for (int j = 0; j < subitem[i].Length - 9; j++)
                        {
                            dgv2.Rows[0].Cells[j+1].Value = subitem[i][j + 4];
                        }
                        break;
                    case 2:
                        for (int j = 0; j < subitem[i].Length - 9; j++)
                        {
                            dgv3.Rows[0].Cells[j+1].Value = subitem[i][j + 4];
                        }
                        break;
                    case 3:
                        for (int j = 0; j < subitem[i].Length - 9; j++)
                        {
                            dgv4.Rows[0].Cells[j+1].Value = subitem[i][j + 4];
                        }
                        break;
                    case 4:
                        for (int j = 0; j < subitem[i].Length - 9; j++)
                        {
                            dgv5.Rows[0].Cells[j+1].Value = subitem[i][j + 4];
                        }
                        break;
                }
            }
            return subitem;
        }
        #endregion

        #region 将string值赋给样本系数的表格
        public List<string[]> AddSamCoffi(List<string> datasource, DataGridView dgv1, DataGridView dgv2,
            DataGridView dgv3, DataGridView dgv4, DataGridView dgv5)
        {
             List<string[]> samcoffi = new List<string[]>();
            for(int i=0;i<datasource.Count;i++)
            {
                string[] strArray = datasource[i].Split('\t');
                samcoffi.Add(strArray);
            }
             for(int i = 1;i<=samcoffi.Count/9-1;i++)
            {
                switch(i)
                {
                    case 1:
                        for (int j = 0; j < 9; j++)
                        {
                            dgv1.Rows[j].Cells[1].Value = samcoffi[(i - 1) * 9 + j][i];
                        }
                        break;
                    case 2:
                        for (int j = 0; j < 9; j++)
                        {
                            dgv2.Rows[j].Cells[1].Value = samcoffi[(i - 1) * 9 + j][i];
                        }
                        break;
                    case 3:
                        for (int j = 0; j < 9; j++)
                        {
                            dgv3.Rows[j].Cells[1].Value = samcoffi[(i - 1) * 9 + j][i];
                        }
                        break;
                    case 4:
                        for (int j = 0; j < 9; j++)
                        {
                            dgv4.Rows[j].Cells[1].Value = samcoffi[(i - 1) * 9 + j][i];
                        }
                        break;
                    case 5:
                        for (int j = 0; j < 9; j++)
                        {
                            dgv5.Rows[i].Cells[1].Value = samcoffi[(i - 1) * 9 + j][i];
                        }
                        break;
                }
            }
            return samcoffi;
        }
        #endregion

        #region 新建文件夹
        public  void CreateDirectoryOrFile(string path , string newDirectoryName)
        {
           string newPath = System.IO.Path.Combine(path +"\\"+ newDirectoryName);
           if (!System.IO.Directory.Exists(newPath))
            {
                System.IO.Directory.CreateDirectory(newPath);              
            }
            else
            {
                MessageBox.Show("文件夹已存在，请更换实验编号！");
            }
        }
        #endregion

        #region 写TST文件
        public void WriteTstFile(List<string> datasource,string Path)
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                for (int i = 0; i < datasource.Count; i++)
                {
                    writer.WriteLine(@datasource[i]);
                }
                writer.Flush();
                writer.Close();
            }         
        }
        #endregion

        #region 判断指定路径下是否有文件夹，同时文件夹下是否存在指定文件
        public List<string> FindDirectAndFile(string Path, string Filetype)
        {
            DirectoryInfo dir = new DirectoryInfo(Path);
            DirectoryInfo[] direct = dir.GetDirectories();
            List<string> filename = new List<string>();
            if (direct.Length > 0)
            {
                foreach (DirectoryInfo dind in direct)
                {
                    FileInfo[] inf = dind.GetFiles();
                    foreach (FileInfo finf in inf)
                    {
                        if (finf.Extension.Equals(Filetype))
                        {
                            filename.Add(finf.Name.Replace(Filetype, ""));
                        }
                    }
                }
            }
            return filename;
        }
        #endregion

        #region 获取子项参数相关信息加载到测试实验界面的datagridview中
        public void GetSubitemInfo(List<string>datasource,DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<string[]> subitem = new List<string[]>();
            for (int i = 0; i < datasource.Count; i++)
            {
                string[] strArray = datasource[i].Split('\t');
                subitem.Add(strArray);
            }
            for(int i=0;i<subitem.Count;i++)
            {
                i = dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = "项目" + (i + 1).ToString();
                dgv.Rows[i].Cells[1].Value =  subitem[i][8];
                dgv.Rows[i].Cells[2].Value = subitem[i][9];
                dgv.Rows[i].Cells[3].Value = subitem[i][10];
                dgv.Rows[i].Cells[4].Value=subitem[i][11];
            }
        }
        #endregion

        #region 保存项目名称、项目编号以及说明
        public void WriteItem(string str1,string str2,string str3,string path,string type)
        {
            List<ItemInfo> iteminf = new List<ItemInfo>();
            iteminf.Add(new ItemInfo { Itemnumber = str2, Itemname = str1, Itemexplain = str3 });
            if (FileExist(path,type))
            {
                using (StreamReader reader = new StreamReader(path + "\\"  + ".TST"))
                {
                    string line;
                    while ((line=reader.ReadLine())!= null)
                    {
                        string[] strArray = line.Split('\t');
                        iteminf.Add(new ItemInfo { Itemnumber = strArray[0], Itemname = strArray[1], Itemexplain = strArray[2] });
                    }                 
                }
                using (StreamWriter writer = new StreamWriter(path + "\\"  + ".TST"))
                {
                    for (int i = 0; i < iteminf.Count; i++)
                    {
                        writer.WriteLine(@iteminf[i].Itemnumber + '\t' + @iteminf[i].Itemname + '\t' + @iteminf[i].Itemexplain);
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(path+"\\"+".TST"))
                {              
                    for (int i = 0; i < iteminf.Count; i++)
                    {
                        writer.WriteLine(@iteminf[i].Itemnumber + '\t' + @iteminf[i].Itemname + '\t' + @iteminf[i].Itemexplain);
                    }
                }
            }
            
        }
        #endregion

        #region 读Item
        public List<ItemInfo> ReadItem(string path)
        {
            List<ItemInfo> iteminf = new List<ItemInfo>();
            List<string> result = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            for (int i = 0; i < result.Count; i++)
            {
                string[] strArray = result[i].Split('\t');
                iteminf.Add(new ItemInfo { Itemnumber = strArray[0], Itemname = strArray[1], Itemexplain = strArray[2] });
            }
            return iteminf;
        }
        #endregion

        #region 删除按钮的写入
        public void WriteTSTFile(List<ItemInfo> datasource, string Path)
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                for (int i = 0; i < datasource.Count; i++)
                {
                    writer.WriteLine(@datasource[i].Itemnumber+'\t'+@datasource[i].Itemname+'\t'+@datasource[i].Itemexplain);
                }
                writer.Flush();
                writer.Close();
            }
        }
        #endregion

        #region 写曲线拟合参数
        public void WriteLinefitInfo(List<LineImfo> linfitinfo,string path)
        {
             string str = string.Empty;
             using(StreamWriter writer = new StreamWriter(path))
             {
                 for(int i=0;i<linfitinfo.Count;i++)
                 {
                     writer.WriteLine("curve"+@linfitinfo[i].Linenum);//曲线序号0
                     for(int j=0;j<linfitinfo[i].potency.Count;j++)
                     {
                        
                         if(j<linfitinfo[i].potency.Count-1)
                         {
                             str += linfitinfo[i].potency[j] + '\t';
                         }
                         else
                         {
                             str = str + linfitinfo[i].potency[j];
                         }
                     }
                     writer.WriteLine(str);//浓度1
                     str = string.Empty;
                     for (int j = 0; j < linfitinfo[i].reponse.Count; j++)
                     {

                         if (j < linfitinfo[i].reponse.Count - 1)
                         {
                             str += linfitinfo[i].reponse[j] + '\t';
                         }
                         else
                         {
                             str = str + linfitinfo[i].reponse[j];
                         }
                     }
                     writer.WriteLine(str);//反应值2
                     str = string.Empty;
                     for (int j = 0; j < linfitinfo[i].calpotency.Count; j++)
                     {

                         if (j < linfitinfo[i].calpotency.Count - 1)
                         {
                             str += linfitinfo[i].calpotency[j] + '\t';
                         }
                         else
                         {
                             str = str  + linfitinfo[i].calpotency[j];
                         }
                     }
                     writer.WriteLine(str);//计算浓度3
                     str = string.Empty;
                     for (int j = 0; j < linfitinfo[i].Std.Count; j++)
                     {

                         if (j < linfitinfo[i].Std.Count - 1)
                         {
                             str += linfitinfo[i].Std[j] + '\t';
                         }
                         else
                         {
                             str = str  + linfitinfo[i].Std[j];
                         }
                     }
                     writer.WriteLine(str);//偏差4
                     str = string.Empty;
                     writer.WriteLine(@linfitinfo[i].result_linefit);//拟合结果
                     writer.WriteLine(@linfitinfo[i].method_potency);//浓度值变换方法6
                     writer.WriteLine(@linfitinfo[i].method_reponse);//反应值变换方法7
                     writer.WriteLine(@linfitinfo[i].demicalplace_potency);//浓度值小数位数8
                     writer.WriteLine(@linfitinfo[i].deicalplace_reponse);//反应值小数位数9
                     writer.WriteLine(@linfitinfo[i].methodLinefit);//拟合算法10
                 }                                                                              
                 writer.Flush();
                 writer.Close();
             }
        }
        #endregion

        #region 读曲线参数
        public List<LineImfo> ReadLineImf(string path)
        {
            List<string> result = new List<string>();
            List<LineImfo> lineimf = new List<LineImfo>();          
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
                for(int i=1;i<=result.Count/11;i++)
                {
                    LineImfo lineim = new LineImfo();
                    lineim.potency = new List<string>();
                    lineim.reponse = new List<string>();
                    lineim.calpotency = new List<string>();
                    lineim.Std = new List<string>();
                    for (int j = 0 + (i - 1) * 11; j < 11 + (i - 1) * 11; j++)
                    {
                       if(j==0 + (i - 1) * 11)
                       {
                           lineim.Linenum = result[j];
                       }
                       else if (j >= 1 + (i - 1) * 11 && j <= 4 + (i - 1) * 11)
                       {
                           if (j == 1 + (i - 1) * 11)//浓度值
                           {
                               string[] strArray = result[j].Split('\t');
                               for(int k=0;k<strArray.Length;k++)
                               {
                                   lineim.potency.Add(strArray[k]);
                               }
                           }
                           else if(j==2 + (i - 1) * 11)//反应值
                           {
                               string[] strArray = result[j].Split('\t');
                               for (int k = 0; k < strArray.Length; k++)
                               {
                                   lineim.reponse.Add(strArray[k]);
                               }
                           }
                           else if (j == 3 + (i - 1) * 11)//计算浓度值
                           {
                               string[] strArray = result[j].Split('\t');
                               for (int k = 0; k < strArray.Length; k++)
                               {
                                   lineim.calpotency.Add(strArray[k]);
                               }
                           }
                           else if (j == 4 + (i - 1) * 11)//偏差
                           {
                               string[] strArray = result[j].Split('\t');
                               for (int k = 0; k < strArray.Length; k++)
                               {
                                   lineim.Std.Add(strArray[k]);
                               }
                           }                         
                       }
                       else if (j >= 5 + (i - 1) * 11)
                       {
                            if (j == 5 + (i - 1) * 11)//拟合结果
                           {
                               lineim.result_linefit = result[j];
                           }
                           else if(j==6+(i-1)*11)//浓度值变换
                           {
                               lineim.method_potency = result[j];
                           }
                           else if(j==7+(i-1)*11)//反应值变换
                           {
                               lineim.method_reponse = result[j];
                           }
                           else if (j == 8 + (i - 1) * 11)//浓度值小数位数
                           {
                               lineim.demicalplace_potency = result[j];
                           }
                           else if (j == 9 + (i - 1) * 11)//反应值小数位数
                           {
                               lineim.deicalplace_reponse = result[j];
                           }
                           else if (j == 10 + (i - 1) * 11)//拟合算法
                           {
                               lineim.methodLinefit = result[j];
                           }                       
                       }
                       
                    }
                    lineimf.Add(lineim); 
                }
            }
            return lineimf;
        }
        #endregion

        #region 判断字符串是否为汉字
        public bool isChinese(string str)
        {
            if ((int)str[0] > 0x4E00 && (int)str[0] < 0x9FA5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 写HEX文件
        public void WriteHEXFile(List<byte[]> order1, string path)
        {
            string str = string.Empty;
            using (StreamWriter writer = new StreamWriter(path))
            {
                for(int i=0;i<order1.Count;i++)
                {
                    for(int j=0;j<order1[i].Length;j++)
                    {
                        if(j<order1[i].Length-1)
                        {
                            str += order1[i][j].ToString("X2") + '\t';
                        }
                        else
                        {
                            str = str + order1[i][j].ToString("X2");
                        }
                    }
                    writer.WriteLine(str);
                    str = string.Empty;
                }
                writer.Flush();
                writer.Close();
            }
        }
        #endregion

        #region 读HEX文件
        public List<string> ReadHexFile(string Path)
        {
            List<string> result = new List<string>();
            using (StreamReader reader = new StreamReader(Path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            return result;
        }
        #endregion

        #region 进制转换
        public static string getASCIItoStr(string str)
        {
            byte[] bb = Hex2Bytes(str, false);
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            string strCharacter = asciiEncoding.GetString(bb);
            Console.WriteLine(strCharacter);
            return strCharacter;
        }
        //先将16进制ASCII码转字节数组
        public static byte[] Hex2Bytes(string sHex, bool isExchange)
        {
            if (sHex == null || sHex.Length == 0)
                return null;
            sHex = sHex.Length % 2 == 0 ? sHex : "0" + sHex;
            byte[] bRtns = new byte[sHex.Length / 2];
            for (int i = 0; i < bRtns.Length; i++)
            {
                if (isExchange)
                    bRtns[bRtns.Length - 1 - i] = Convert.ToByte(sHex.Substring(i * 2, 2), 16);
                else
                    bRtns[i] = Convert.ToByte(sHex.Substring(i * 2, 2), 16);
            }
            return bRtns;
        }
    
        #endregion

        #region 检测字符串是否为ASCII码
        public bool HasNonASCIICHars(string str)
        {
            return (System.Text.Encoding.UTF8.GetByteCount(str) != str.Length);
        }
        #endregion

        #region 截取字符串
        public string[] MidStrEx(string sourse, string startstr, string endstr)
        {
            List<int> index_start = new List<int>();
            List<int> index_end = new List<int>();
            for (int i = 0; i < sourse.Length; i = i + 2)
            {
                string str = sourse.Substring(i, 2);
                if (str == "5B")
                {
                    index_start.Add(i + 2);
                }
                else if (str == "5D")
                {
                    index_end.Add(i);
                }
            }
            string[] strArray = new string[index_end.Count];
            for (int i = 0; i < index_start.Count; i++)
            {
                strArray[i] = sourse.Substring(index_start[i], (index_end[i] - index_start[i]));
            }
            return strArray;
        }
        #endregion

        #region 合并同类型
        public List<string[]> CombineIteam(List<string[]> order,string str)
        {
            List<string[]> order_seam = new List<string[]>();
            for(int i=0;i<order.Count;i++)
            {
                if(order[i][2]==str)
                {
                    order_seam.Add(order[i]);
                }
            }
            return order_seam;
        }
        #endregion

        #region HEX文件解析
        public List<string[]> AnyHex(List<string[]> order)
        {
            List<string[]> order_list = new List<string[]>();
            for (int i = 0; i < order.Count; i++)
            {
                switch (order[i][2])
                {
                    case"01"://ok
                        string str = string.Empty;                        
                        for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        string[] strArray = MidStrEx(str, "5B", "5D");
                        string[] str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = contobyte.Convert2Word(strArray[0]);
                                    break;
                                case 1:                                  
                                    str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                                    break;
                            }
                        }
                        order_list.Add(str1);
                            break;
                    case "02"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                        }
                        order_list.Add(str1);
                            break;
                    case "03"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                        }
                        order_list.Add(str1);
                        break;
                    case "04"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            byte[] bytes = System.Text.Encoding.Default.GetBytes(strArray[j]);
                            str1[j] = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                        }
                        order_list.Add(str1);
                        break;
                    case "05"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            byte[] bytes = System.Text.Encoding.Default.GetBytes(strArray[j]);
                            str1[j] = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                        }
                        order_list.Add(str1);
                        break;
                    case "06"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "07"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "08"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "09"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            int index = 0;
                            if (strArray[j].Contains("2D"))
                            {
                                index = strArray[j].IndexOf("2D");
                            }
                            string str2 = strArray[j].Substring(0, index + 2);
                            string str3 = strArray[j].Replace(str2,"");
                            string str4 = getASCIItoStr(str2.Replace("2D",""))+getASCIItoStr("2D");
                            str1[j] = str4 + contobyte.Convert2Word(str3);
                        }
                        order_list.Add(str1);
                        break;
                    case "0A"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "0B"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "0C"://ok
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                        }
                        order_list.Add(str1);
                        break;
                    case "0D"://ok
                          str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "0E"://ok
                        str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "0F"://ok
                        str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            int index=0;
                            if(strArray[j].Contains("2E"))
                            {
                                 index= strArray[j].IndexOf("2E");
                            }
                            string deletsign = strArray[j].Substring(0, index + 2);
                            string str2 = strArray[j].Substring(0, index + 2).Replace("2E","");
                            string str3 = strArray[j].Replace(deletsign, "");
                            byte[] bytes = System.Text.Encoding.Default.GetBytes(str2);                        
                            string str4 = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length) + getASCIItoStr("2E");
                            str1[j] = str4 + contobyte.Convert2Word(str3);
                        }
                        order_list.Add(str1);
                        break;
                    case "11":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];                     
                        for (int j = 0; j < strArray.Length;j++ )
                        {
                            switch(j)
                            {
                                case 0:
                                    if(HasNonASCIICHars(strArray[0]))
                                    {
                                        str1[j] = contobyte.Convert2Word(strArray[0]);
                                    }
                                    else
                                    {
                                        str1[j] = getASCIItoStr(strArray[j]);
                                    }
                                    break;
                                case 1:
                                    str1[j] = getASCIItoStr(strArray[j]);//转换字母以及数字例如0x30、0x31转换为0、1
                                     break;
                                case 2:
                                     str1[j] = getASCIItoStr(strArray[j]);//转换字母以及数字例如0x30、0x31转换为0、1
                                     break; 
                            }
                        }
                        order_list.Add(str1);
                        break;
                    case "12":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                            break;
                    case "13":
                          str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "14":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                          for (int j = 0; j < strArray.Length;j++)
                        {
                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "15":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                           for (int j = 0; j < strArray.Length;j++)
                        {
                            if (j != strArray.Length - 1)
                            {
                                str1[j] = getASCIItoStr(strArray[j]);
                            }
                               else 
                            {
                                byte[] bytes = System.Text.Encoding.Default.GetBytes(strArray[j]);
                                string str4 = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                                str1[j] = str4;
                            }
                            
                        }
                     
                        order_list.Add(str1);
                        break;
                    case "16":
                          str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 1:
                                    str1[j] = contobyte.Convert2Word(strArray[j]);
                                    break;
                                case 2:
                                     byte[] bytes = System.Text.Encoding.Default.GetBytes(strArray[j]);
                                     string str4 = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                                    if(str4.IndexOf('0',0)==0)
                                    {
                                        str4 = str4.Replace('0', ' ');
                                    }
                                     str1[j] = str4;
                                     break;
                                case 3:
                                     bytes = System.Text.Encoding.Default.GetBytes(strArray[j]);
                                     str4 = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                                     if (str4.IndexOf('0', 0) == 0)
                                     {
                                         str4 = str4.Replace('0', ' ');
                                     }
                                     str1[j] = str4;
                                     break;
                                case 4:
                                     str4 = Convert.ToInt64(strArray[j], 16).ToString();
                                     str1[j] = str4;
                                     break;
                            }                           
                        }
                        order_list.Add(str1);
                            break;
                    case "17":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 1:                                   
                                     str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                                     break;
                                case 2:
                                     string[] value = Regex.Split(strArray[j],"20",RegexOptions.IgnoreCase);
                                     for (int k = 0; k < value.Length;k++)
                                     {
                                         if(value[k]!="")
                                         {
                                             if(k!=value.Length-1)
                                             {
                                                 str1[j] += getASCIItoStr(value[k])+'\t';
                                             }
                                             else
                                             {
                                                 str1[j] = getASCIItoStr(value[k]);
                                             }
                                         }
                                     }
                                         break;
                            }                          
                        }
                        order_list.Add(str1);
                            break;
                    case "18":
                          str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 1:                               
                                     str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                                     break;
                                case 2:
                                     string[] value = Regex.Split(strArray[j],"20",RegexOptions.IgnoreCase);
                                     for (int k = 0; k < value.Length;k++)
                                     {
                                         if(value[k]!="")
                                         {
                                             if(k!=value.Length-1)
                                             {
                                                 str1[j] += getASCIItoStr(value[k])+'\t';
                                             }
                                             else
                                             {
                                                 str1[j] = getASCIItoStr(value[k]);
                                             }
                                         }
                                     }
                                         break;
                            }
                           
                        }
                        order_list.Add(str1);
                        break;
                    case "19":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 1:                                 
                                     str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                                     break;
                                case 2:
                                     string[] value = Regex.Split(strArray[j],"20",RegexOptions.IgnoreCase);
                                     for (int k = 0; k < value.Length;k++)
                                     {
                                         if(value[k]!="")
                                         {
                                             if(k!=value.Length-1)
                                             {
                                                 str1[j] += getASCIItoStr(value[k])+'\t';
                                             }
                                             else
                                             {
                                                 str1[j] = getASCIItoStr(value[k]);
                                             }
                                         }
                                     }
                                         break;
                            }                        
                        }
                        order_list.Add(str1);
                        break;
                    case "1A":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 1:                                  
                                     str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                                     break;
                                case 2:
                                     string[] value = Regex.Split(strArray[j],"20",RegexOptions.IgnoreCase);
                                     for (int k = 0; k < value.Length;k++)
                                     {
                                         if(value[k]!="")
                                         {
                                             if(k!=value.Length-1)
                                             {
                                                 str1[j] += getASCIItoStr(value[k])+'\t';
                                             }
                                             else
                                             {
                                                 str1[j] = getASCIItoStr(value[k]);
                                             }
                                         }
                                     }
                                         break;
                            }                         
                        }
                        order_list.Add(str1);
                        break;
                    case "1B":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {                       
                            str1[j] = Convert.ToInt64(strArray[j], 16).ToString();                     
                        }
                        order_list.Add(str1);
                            break;
                    case "1C":
                           str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            switch(j)
                            {
                                case 0:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 1:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 2:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                                case 3:
                                    str1[j] = getASCIItoStr(strArray[j]);
                                    break;
                            }                        
                        }
                        order_list.Add(str1);
                            break;
                    case "1D":
                           str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            if(j==0||j==3||j==6||j==7)
                            {

                                str1[j] = Convert.ToInt64(strArray[j], 16).ToString();                               
                            }
                            else if(j==1||j==4||j==5||j==8)
                            {
                                str1[j] = getASCIItoStr(strArray[j]);
                            }
                            else if(j==2)
                            {
                                str1[j] = contobyte.Convert2Word(strArray[j]);
                            }                         
                        }
                        order_list.Add(str1);
                            break;
                    case "1E":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            if(j==0)
                            {
                                str1[j] = contobyte.Convert2Word(strArray[j]);
                            }
                            else
                            {
                                str1[j] = Convert.ToInt64(strArray[j], 16).ToString();
                            }                        
                        }
                        order_list.Add(str1);
                            break;
                    case "1F":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {
                            str1[j] = getASCIItoStr(strArray[j]);                      
                        }
                        order_list.Add(str1);
                        break;
                    case "20":
                          str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {

                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "21":
                          str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {

                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "22":
                        str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {

                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                    case "23":
                         str = string.Empty;
                          for (int j = 4; j < order[i].Length-1;j++)
                        {
                            str += order[i][j];
                        }
                        strArray = MidStrEx(str, "5B", "5D");
                        str1 = new string[strArray.Length];
                        for (int j = 0; j < strArray.Length;j++)
                        {

                            str1[j] = getASCIItoStr(strArray[j]);
                        }
                        order_list.Add(str1);
                        break;
                }
            }
            return order_list;
        }
        #endregion

        #region 根据输入取值区间计算曲线面积
        //str曲线数据
        //range中包含起点，终点，以及取值区间
        //background本底值
        public string[] CalArea(string[] str,List<string[]> range,string background)
        {         
            string[] str_area = new string[range.Count];//单条曲线峰值面积
            for(int i=0;i<range.Count;i++)
            {              
                string[] strarry = new string[Convert.ToInt32(range[i][1]) - Convert.ToInt32(range[i][0])];//在此区间内找第i个峰值
                Array.Copy(str,Convert.ToInt32(range[i][0]),strarry,0,
                    (Convert.ToInt32(range[i][1]) - Convert.ToInt32(range[i][0])));//（源数组，源数组索引起始位，目标数组，目标数组起始位，数据长度）
                int[] out_str = Array.ConvertAll<string, int>(strarry, delegate(string s) { return int.Parse(s); });
                int max = out_str.Max();//最大值
                int[] str2int = Array.ConvertAll<string, int>(str, delegate(string s) { return int.Parse(s); });
                int index_max = Array.IndexOf(str2int, max);//最大值索引
                string[] strarry1 = new string[Convert.ToInt32(range[i][2])];//需要计算面积的数组
                Array.Copy(str, index_max - (Convert.ToInt32(range[i][2]) / 2), strarry1, 0, Convert.ToInt32(range[i][2]));
                int areas = 0;
                for(int j=0;j<strarry1.Length;j++)
                {
                    areas += Convert.ToInt32(strarry1[j]) - Convert.ToInt32(background);
                }
                str_area[i] = areas.ToString();
            }

            return (str_area);
        }
        #endregion

        #region 重复性测试页进行表格表头添加
        public void AddRows(DataGridView dgv1,DataGridView dgv2,DataGridView dgv3,DataGridView dgv4,DataGridView dgv5)
        {
            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();//添加表头第一列,DataGridView3
            acCode.Name = "0";
            acCode.DataPropertyName = "0";
            acCode.HeaderText = " ";
            dgv1.Columns.Add(acCode);
            dgv1.Rows.Add("X1");//添加数据行X1
            dgv1.Rows.Add("X2");//添加数据行X2
            DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();//DataGridView4的数据项列
            acCode1.Name = "dataitem";
            acCode1.DataPropertyName = "dataitem";
            acCode1.HeaderText = "数据项";
            dgv2.Columns.Add(acCode1);
            DataGridViewTextBoxColumn acCode2 = new DataGridViewTextBoxColumn();//DataGridView4的平均值列
            acCode2.Name = "meanvalue";
            acCode2.DataPropertyName = "meanvalue";
            acCode2.HeaderText = "平均值";
            dgv2.Columns.Add(acCode2);
            DataGridViewTextBoxColumn acCode3 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode3.Name = "sdvalue";
            acCode3.DataPropertyName = "sdvalue";
            acCode3.HeaderText = "标准偏差";
            dgv2.Columns.Add(acCode3);
            DataGridViewTextBoxColumn acCode4 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode4.Name = "cvvalue";
            acCode4.DataPropertyName = "cvvalue";
            acCode4.HeaderText = "CV值";
            dgv2.Columns.Add(acCode4);
            DataGridViewTextBoxColumn acCode5 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode5.Name = "maxvalue";
            acCode5.DataPropertyName = "maxvalue";
            acCode5.HeaderText = "最大值";
            dgv2.Columns.Add(acCode5);
            DataGridViewTextBoxColumn acCode6 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode6.Name = "minvalue";
            acCode6.DataPropertyName = "minvalue";
            acCode6.HeaderText = "最小值";
            dgv2.Columns.Add(acCode6);
            dgv2.Rows.Add("X1");//添加数据行X1
            dgv2.Rows.Add("X2");//添加数据行X2
            DataGridViewTextBoxColumn acCode7 = new DataGridViewTextBoxColumn();//DataGridView2的序号列
            acCode7.Name = "num";
            acCode7.DataPropertyName = "num";
            acCode7.HeaderText = "序号";
            dgv3.Columns.Add(acCode7);
            DataGridViewTextBoxColumn acCode8 = new DataGridViewTextBoxColumn();//DataGridView2的起点列
            acCode8.Name = "start";
            acCode8.DataPropertyName = "start";
            acCode8.HeaderText = "起点";
            dgv3.Columns.Add(acCode8);
            DataGridViewTextBoxColumn acCode9 = new DataGridViewTextBoxColumn();//DataGridView2的终点列
            acCode9.Name = "stop";
            acCode9.DataPropertyName = "stop";
            acCode9.HeaderText = "终点";
            dgv3.Columns.Add(acCode9);
            DataGridViewTextBoxColumn acCode10 = new DataGridViewTextBoxColumn();//DataGridView2的终点列
            acCode10.Name = "value";
            acCode10.DataPropertyName = "value";
            acCode10.HeaderText = "取值";
            dgv3.Columns.Add(acCode10);
            dgv3.Rows.Add("X1");//添加数据行X1
            dgv3.Rows.Add("X2");//添加数据行X2
            dgv3.Rows[0].Cells[1].Value = "93";
            dgv3.Rows[0].Cells[2].Value = "208";
            dgv3.Rows[0].Cells[3].Value = "60";
            dgv3.Rows[1].Cells[1].Value = "240";
            dgv3.Rows[1].Cells[2].Value = "364";
            dgv3.Rows[1].Cells[3].Value = "60";
            DataGridViewTextBoxColumn acCode11 = new DataGridViewTextBoxColumn();//DataGridView5 序号
            acCode11.Name = "num";
            acCode11.DataPropertyName = "num";
            acCode11.HeaderText = "序号";
            dgv4.Columns.Add(acCode11);
            DataGridViewTextBoxColumn acCode12 = new DataGridViewTextBoxColumn();//DataGridView5 P1
            acCode12.Name = "P1";
            acCode12.DataPropertyName = "P1";
            acCode12.HeaderText = "P1";
            dgv4.Columns.Add(acCode12);
            DataGridViewTextBoxColumn acCode13 = new DataGridViewTextBoxColumn();//DataGridView5 P2
            acCode13.Name = "P2";
            acCode13.DataPropertyName = "P2";
            acCode13.HeaderText = "P2";
            dgv4.Columns.Add(acCode13);
            DataGridViewTextBoxColumn acCode14 = new DataGridViewTextBoxColumn();//DataGridView5 P3
            acCode14.Name = "P3";
            acCode14.DataPropertyName = "P3";
            acCode14.HeaderText = "P3";
            dgv4.Columns.Add(acCode14);
            DataGridViewTextBoxColumn acCode15 = new DataGridViewTextBoxColumn();//DataGridView5 公式
            acCode15.Name = "method";
            acCode15.DataPropertyName = "method";
            acCode15.HeaderText = "公式";
            dgv4.Columns.Add(acCode15);
            for(int i=0;i<5;i++)
            {
                i = dgv4.Rows.Add();
                dgv4.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            DataGridViewTextBoxColumn acCode16 = new DataGridViewTextBoxColumn();//添加表头第一列,DataGridView3
            acCode16.Name = "0";
            acCode16.DataPropertyName = "0";
            acCode16.HeaderText = " ";
            dgv5.Columns.Add(acCode16);
            for(int i=0;i<2;i++)
            {
                i = dgv5.Rows.Add();
                dgv5.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
            }
        }
        #endregion
        //dgv1=datagridview1
        //dgv2=datagridview4
        #region 取值及计算
        public List<string[]> GetDataToCal(DataGridView dgv1,DataGridView dgv2,string reference_peak,string background)
        {
            List<string[]> result = new List<string[]>();//存储数据顺序为第1条至第n条曲线各个峰的峰面积，第1条至第n条曲线各个峰的峰值，
            //峰值及峰值索引，峰面积比值（共str_range-1组）、峰值比值（共str_range-1组）
            //峰面积的均值标准差以及CV值、峰值的均值标准差以及CV值
            //峰面积的比值的均值标准差以及CV值、峰值的比值的均值标准差以及CV值
            List<string[]> str_range = new List<string[]>();//峰值区间及采样点数
            List<double[]> maxindex = new List<double[]>();//峰值及峰值索引,顺序为双数为峰值，单数为峰值所对应的索引
            List<string[]> area_all = new List<string[]>();//所有曲线峰值面积 
            List<double[]> cal_result = new List<double[]>();//存储数据顺序为第1条至第n条曲线除基准峰外峰面积与基准峰峰面积的比值（共str_range-1组）、
            //除基准峰外各个峰峰值与基准峰峰值的比值（共str_range-1组）
            List<double[]> area_max_CVet = new List<double[]>();//数据存储顺序为峰面积的平均差标准差以及CV值、峰值的平均差标准差以及CV值、
            //峰面积的比值的均值标准差以及CV值、峰值的比值的均值标准差以及CV值


            //取峰值区间及采样点数
            if (dgv2.Rows.Count > 0 && dgv2.Rows[1].Cells[2].Value != null)
            {              
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string[] strArray1 = new string[3];
                    for (int j = 0; j < 3; j++)
                    {
                        strArray1[j] = dgv2.Rows[i].Cells[j + 1].Value.ToString();
                    }
                    str_range.Add(strArray1);
                }
            }
            else
            {
                MessageBox.Show("请输入坐标信息");
            }
            //获取曲线峰值及其峰值所在索引
            if (dgv1.ColumnCount > 0)
            {
                for (int i = 0; i < dgv1.ColumnCount; i++)
                {
                    string[] strArray = new string[dgv1.Rows.Count];
                    for (int j = 0; j < dgv1.Rows.Count; j++)
                    {
                        strArray[j] = dgv1.Rows[j].Cells[i].Value.ToString();//取出曲线数据
                    }
                    double[] maxandindex = GetPeakPointOfData(strArray, str_range);//获取第i条曲线数据的峰值及其索引
                    maxindex.Add(maxandindex);
                    string[] area = CalArea(strArray, str_range, background);//面积
                    area_all.Add(area);
                }
            }
            else
            {
                MessageBox.Show("无曲线数据！");
            }
            //获取计算CV值、平均值以及标准差的数列
            for (int i = 0; i < str_range.Count; i++)//峰个数
            {
                string[] strarray2 = new string[dgv1.ColumnCount];
                for (int j = 0; j < dgv1.ColumnCount; j++)
                {
                    strarray2[j] = area_all[j][i];//当i=0时，即为第1至n条曲线的第一个峰的峰值面积
                }
                result.Add(strarray2);
                //举例当有两个峰时，result中第一部分保存的为第一个峰的峰面积
                //第二个峰的峰面积即strarray1=[line1-area1,line2-area1,line3-area1,line4-area1,line5-area1.....]
                //以及strarray1=[line1-area2,line2-area2,line3-area2,line4-area2,line5-area2.....]
            }

            for (int i = 0; i < str_range.Count; i = i + 1)//峰个数
            {
                string[] strarray2 = new string[dgv1.ColumnCount];            
                for (int j = 0; j < maxindex.Count; j=j+1)//曲线个数
                {
                    strarray2[j] = maxindex[j][i*2].ToString(); ;
                }
                result.Add(strarray2);
                //举例当有两个峰时result中第二部分保存的为第一个峰的峰值以及第二个峰的峰值
                //即strarray1=[line1-max1,line2-max1,line3-max1,line4-max1,line5-max1.....]
                //以及strarray1=[line1-max2,line2-max2,line3-max2,line4-max2,line5-max2.....]
            }
            int ref_peak = Convert.ToInt32(reference_peak.Replace("X", "")) - 1;//基准峰为X2，则其存储的索引为area_all[0][1]
            for(int i=0;i<result.Count;i=i+1)
            {
                double[] strarray2 = new double[dgv1.ColumnCount];
                if(i<=str_range.Count-1)
                {
                    for (int j = 0; j < result[i].Length; j++)
                    {
                        strarray2[j] = Convert.ToDouble(result[i][j]) / Convert.ToDouble(result[ref_peak][j]);
                    }
                    cal_result.Add(strarray2);//除基准峰外第1个至第n个峰的峰值与基准峰的峰值的比值
                   
                }
                else 
                {

                    for (int j = 0; j < result[i].Length; j++)
                    {

                        strarray2[j] = Convert.ToDouble(result[i][j]) / Convert.ToDouble(result[ref_peak + str_range.Count][j]);
                    }
                    cal_result.Add(strarray2);//除基准峰外第1个至第n个峰的峰值与基准峰的峰值的比值
                   
                }
               
            }//OK
            //计算每个峰的峰面积的均值等以及峰值的均值等
            for (int i = 0; i < result.Count; i++)
            {
                double[] array = new double[result[i].Length];
                for (int j = 0; j < result[i].Length; j++)
                {
                    array[j] = Convert.ToDouble(result[i][j]);
                }
                double[] res_cal = MeanAndSd(array);
                area_max_CVet.Add(res_cal);
            }
            //计算比值的均值、标准差以及CV值等
            for (int i = 0; i < cal_result.Count; i++)
            {
                double[] res_cal = MeanAndSd(cal_result[i]);
                area_max_CVet.Add(res_cal);
            }

            //获取每个峰峰面积的最大值以及最小值以及峰值的最大值最小值以及峰面积比值的最大值最小值以及峰值比值的最大值最小值
            string[] MaxandMin = new string[(str_range.Count * 2 + str_range.Count * 2 + str_range.Count * 2 + str_range.Count * 2)];
            int index = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int[] data = SortArray(result[i]);
                MaxandMin[index] = data[data.Length - 1].ToString();
                MaxandMin[index + 1] = data[0].ToString();
                index = index + 2;
            }//求各峰面积的最大值最小值以及峰值的最大值最小值
            for (int i = 0; i < cal_result.Count; i++)
            {
                MaxandMin[index] = cal_result[i].Max().ToString();
                MaxandMin[index + 1] = cal_result[i].Min().ToString();
                index = index + 2;
            }

            //将峰值及其索引值添加到result中
            for (int i = 0; i < maxindex.Count; i = i + 1)
            {
                string[] str = new string[maxindex[i].Length];
                for (int j = 0; j < maxindex[i].Length; j++)
                {
                    str[j] = maxindex[i][j].ToString();
                }
                result.Add(str);
            }
            //将比值计算结果添加到result中
            for (int i = 0; i < cal_result.Count; i++)
            {
                string[] str = new string[cal_result[i].Length];
                for(int j=0;j<cal_result[i].Length;j++)
                {
                    str[j] = string.Format("{0:0.0000}", cal_result[i][j]);
                }
                result.Add(str);
            }
            //将CV值等计算结果添加到result中去
            for (int i = 0; i < area_max_CVet.Count; i++)
            {
                string[] str = new string[area_max_CVet[i].Length];
                for (int j = 0; j < area_max_CVet[i].Length; j++)
                {
                    str[j] = string.Format("{0:0.0000}", area_max_CVet[i][j]);
                }
                result.Add(str);
            }
            result.Add(MaxandMin);
            return result;
        }
        #endregion

        #region 给峰中心位置表格赋值
        public void AddMaxAndIndexToDgv(List<string[]> data,DataGridView dgv,int peak_num,int line_num)
        {
            int col = 1;
            int cell = 1;
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            if (dgv.ColumnCount < line_num + 1)
            {
                for (int i = 0; i <= line_num; i++)
                {
                  if(i!=0)
                  {
                      DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                      acCode.Name = col.ToString();
                      acCode.DataPropertyName = col.ToString();
                      acCode.HeaderText = col.ToString();
                      dgv.Columns.Add(acCode);
                      col++;
                  }
                  else
                  {
                      DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                      acCode.Name = "0";
                      acCode.DataPropertyName = "0";
                      acCode.HeaderText = " ";
                      dgv.Columns.Add(acCode);
                  }
                }
            }
            for (int i = 0; i < peak_num; i++)
            {
                i = dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
            }
            for (int i = peak_num * 2; i < line_num + peak_num * 2; i++)//从峰值及其索引值位置处开始取值
            {
                int row = 0;

                for (int j = 1; j < data[i].Length; j = j + 2)
                {
                    dgv.Rows[row].Cells[cell].Value = data[i][j];
                    row++;
                }
                cell++;
            }
        }
        #endregion

        #region 给各峰峰面积或峰值以及各峰与基准峰比值表格赋值
        public void AddDataToDgv(string calmethod,List<string[]> data,DataGridView dgv,int peak_num,int linenum,string referencepeak)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            int col = 1;
            if (dgv.ColumnCount < linenum + 1)
            {
                for (int i = 0; i <= linenum; i++)
                {
                    if (i != 0)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        acCode.Name = col.ToString();
                        acCode.DataPropertyName = col.ToString();
                        acCode.HeaderText = col.ToString();
                        dgv.Columns.Add(acCode);
                        col++;
                    }
                    else
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        acCode.Name = "0";
                        acCode.DataPropertyName = "0";
                        acCode.HeaderText = " ";
                        dgv.Columns.Add(acCode);
                    }
                }
            }
            int peak_ref = Convert.ToInt32(referencepeak.Replace("X", ""))-1;
            switch(calmethod)
            {
                case "1.荧光面积":
                    for (int i = 0; i < peak_num; i++)
                    {
                        i = dgv.Rows.Add();
                        dgv.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            if(dgv.ColumnCount<linenum+1)
                            {
                                DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                                acCode.Name = (j + 1).ToString();
                                acCode.DataPropertyName = (j + 1).ToString();
                                acCode.HeaderText = (j + 1).ToString();
                                dgv.Columns.Add(acCode);
                            }
                            dgv.Rows[i].Cells[j + 1].Value = data[i][j];
                        }
                    } 
                    for (int i = peak_num * 2 + linenum; i < peak_num  + peak_num * 2 + linenum; i++)
                    {
                        if (i - (peak_num * 2 + linenum) == peak_ref)
                        {
                            
                        }
                        else
                        {
                            int index = peak_num + i - (peak_num * 2 + linenum);
                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = "X" + (i - (peak_num * 2 + linenum)+1).ToString() + "/" + referencepeak;
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                dgv.Rows[index].Cells[j + 1].Value = data[i][j];
                            } 
                        }
                    }
                    break;
                case "2.荧光最大值":
                    for (int i = peak_num; i < peak_num + peak_num; i++)
                    {
                        int rowindex = (i - peak_num) ;
                        rowindex = dgv.Rows.Add();
                        dgv.Rows[rowindex].Cells[0].Value = "X" + (rowindex + 1).ToString();
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            if(dgv.ColumnCount<linenum+1)
                            {
                                DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                                acCode.Name = (j + 1).ToString();
                                acCode.DataPropertyName = (j + 1).ToString();
                                acCode.HeaderText = (j + 1).ToString();
                                dgv.Columns.Add(acCode);
                            }
                            dgv.Rows[i-peak_num].Cells[j + 1].Value = data[i][j];
                        }
                    }
                    for (int i = peak_num * 2 + linenum + peak_num; i < peak_num + peak_num * 2 + linenum + peak_num ; i++)
                    {
                        if (i - (peak_num * 2 + linenum + peak_num)!=peak_ref)
                        {
                            int index = peak_num + i - peak_num * 2 + linenum + peak_num - 1;
                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = "X" + (i - (peak_num * 2 + linenum + peak_num)+1).ToString() + "/" + referencepeak;
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                dgv.Rows[index].Cells[j + 1].Value = data[i][j];
                            }
                        }                    
                    }
                    break;
            }
        }
        #endregion

        #region 刷新平均值、标准差以及CV值表格
        public void ResetDgv(DataGridView dgv2)
        {
            dgv2.Columns.Clear();
            dgv2.Rows.Clear();
            DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();//DataGridView4的数据项列
            acCode1.Name = "dataitem";
            acCode1.DataPropertyName = "dataitem";
            acCode1.HeaderText = "数据项";
            dgv2.Columns.Add(acCode1);
            DataGridViewTextBoxColumn acCode2 = new DataGridViewTextBoxColumn();//DataGridView4的平均值列
            acCode2.Name = "meanvalue";
            acCode2.DataPropertyName = "meanvalue";
            acCode2.HeaderText = "平均值";
            dgv2.Columns.Add(acCode2);
            DataGridViewTextBoxColumn acCode3 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode3.Name = "sdvalue";
            acCode3.DataPropertyName = "sdvalue";
            acCode3.HeaderText = "标准偏差";
            dgv2.Columns.Add(acCode3);
            DataGridViewTextBoxColumn acCode4 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode4.Name = "cvvalue";
            acCode4.DataPropertyName = "cvvalue";
            acCode4.HeaderText = "CV值";
            dgv2.Columns.Add(acCode4);
            DataGridViewTextBoxColumn acCode5 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode5.Name = "maxvalue";
            acCode5.DataPropertyName = "maxvalue";
            acCode5.HeaderText = "最大值";
            dgv2.Columns.Add(acCode5);
            DataGridViewTextBoxColumn acCode6 = new DataGridViewTextBoxColumn();//DataGridView4的标准偏差列
            acCode6.Name = "minvalue";
            acCode6.DataPropertyName = "minvalue";
            acCode6.HeaderText = "最小值";
            dgv2.Columns.Add(acCode6);
        }
        #endregion

        #region 给平均值、标准差以及CV值表格赋值
        public void MenStaCVDgvGetdata(List<string[]> data,string calmethod,DataGridView dgv,int peaknum,int linenum,string referencepeak)
        {
            int row=0;
            int peak_ref = Convert.ToInt32(referencepeak.Replace("X", "")) - 1;
            List<string[]> maxandmin = new List<string[]>();
            ResetDgv(dgv);
            switch(calmethod)
            {
                case "1.荧光面积":
                    row = 0;
                    for (int i = peaknum * 2 + linenum + peaknum * 2; i < peaknum * 3 + linenum + peaknum * 2; i++)//各峰值面积的均值标准差以及CV值
                    {
                        row = dgv.Rows.Add();
                        dgv.Rows[row].Cells[0].Value = "X" + (i - (peaknum * 2 + linenum + peaknum * 2) + 1).ToString();
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            dgv.Rows[row].Cells[j + 1].Value = data[i][j];
                        }
                        row++;
                    }
                    for (int i = peaknum * 2 + linenum + peaknum * 2 + 2 * peaknum; i < peaknum + peaknum * 2 + linenum + peaknum * 2 + 2 * peaknum; i++)//比值的均值标准差以及CV值
                    {
                        int peak_num = i - (peaknum * 2 + linenum + peaknum * 2 + 2 * peaknum);
                        if (peak_num != peak_ref)
                        {
                            row = peak_num + i - (peaknum * 2 + linenum + peaknum * 2 + 2 * peaknum);
                            row = dgv.Rows.Add();
                            dgv.Rows[row].Cells[0].Value = "X" + (i - (peaknum * 2 + linenum + peaknum * 2 + 2 * peaknum)+1).ToString() + "/" + referencepeak;
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                dgv.Rows[row].Cells[j + 1].Value = data[i][j];
                            }
                        }
                    }
                    row = 0;
                    for (int i = 0; i < peaknum * 2; i = i + 2)//最大值最小值
                    {
                        dgv.Rows[row].Cells[4].Value = data[data.Count - 1][i];
                        dgv.Rows[row].Cells[5].Value = data[data.Count - 1][i + 1];
                        row++;
                    }
                    for (int i = 0; i < peaknum*2;i=i+2)
                    {
                        string[] maxandminvalue = new string[2];
                        maxandminvalue[0] = data[data.Count - 1][(i + (peaknum * 2 * 2))]; 
                        maxandminvalue[1] = data[data.Count - 1][(i + (peaknum * 2 * 2) + 1)];
                        maxandmin.Add(maxandminvalue);
                    }
                    for (int i = 0; i < peaknum;i++)
                    {
                        if(i!=peak_ref)
                        {
                            dgv.Rows[row].Cells[4].Value = maxandmin[i][0];
                            dgv.Rows[row].Cells[5].Value = maxandmin[i][1];
                        }
                    }
                        break;
                case "2.荧光最大值":
                    row = 0;
                    for (int i = peaknum * 2 + linenum + peaknum * 2 + peaknum; i < peaknum + peaknum * 2 + linenum + peaknum * 2 + peaknum; i++)
                    {
                        row = dgv.Rows.Add();
                        dgv.Rows[row].Cells[0].Value = "X" + (i - (peaknum * 2 + linenum + peaknum * 2 + peaknum) + 1).ToString();
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            dgv.Rows[row].Cells[j + 1].Value = data[i][j];
                        }
                        row++;
                    }
                    for (int i = peaknum * 2 + linenum + peaknum * 2 + peaknum * 2 + peaknum; i < peaknum + peaknum * 2 + linenum + peaknum * 2 + peaknum * 2 + peaknum ; i++)
                    {                    
                        int peak_num = i - (peaknum * 2 + linenum + peaknum * 2 + peaknum * 2 + peaknum);
                        if(peak_num!=peak_ref)
                        {
                            row = peaknum + i - (peaknum * 2 + linenum + peaknum * 2 + peaknum * 2 + peaknum);
                            row = dgv.Rows.Add();
                            dgv.Rows[row].Cells[0].Value = "X" + (i - (peaknum * 2 + linenum + peaknum * 2 + peaknum * 2 + peaknum)+1).ToString() + "/" + referencepeak;//峰值比值
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                dgv.Rows[row].Cells[j + 1].Value = data[i][j];
                            }
                        }
                    }
                    row = 0;
                    for (int i = peaknum * 2; i < peaknum * 2 + peaknum * 2; i = i + 2)
                    {
                        dgv.Rows[row].Cells[4].Value = data[data.Count - 1][i];
                        dgv.Rows[row].Cells[5].Value = data[data.Count - 1][i + 1];
                        row++;
                    }
                     for (int i = 0; i < peaknum*2;i=i+2)
                    {
                        string[] maxandminvalue = new string[2];
                        maxandminvalue[0] = data[data.Count - 1][i + (peaknum * 2 * 2 + peaknum * 2)]; 
                        maxandminvalue[1] = data[data.Count - 1][i + (peaknum * 2 * 2 + peaknum * 2) + 1];
                        maxandmin.Add(maxandminvalue);
                    }
                    for (int i = 0; i < peaknum;i++)
                    {
                        if(i!=peak_ref)
                        {
                            dgv.Rows[row].Cells[4].Value = maxandmin[i][0];
                            dgv.Rows[row].Cells[5].Value = maxandmin[i][1];
                        }
                    }
                    break;
            }
        }
        #endregion 

        #region 将datagridview中的值取到list<string[]>中
        public List<string[]> GetValueToList(DataGridView dgv)
        {
            List<string[]> data = new List<string[]>(); 
            for(int i=0;i<dgv.ColumnCount;i++)
            {
                string[] strArray = new string[dgv.Rows.Count];
                for (int j = 0; j < dgv.Rows.Count; j++)
                {
                    strArray[j] = dgv.Rows[j].Cells[i].Value.ToString();//取出曲线数据
                }
                data.Add(strArray);
            }
            return data;
        }
        #endregion

        #region 粘贴数据
        public void PasteData(DataGridView dgv)
        {
            int col = 1;
            string clipboardText = Clipboard.GetText();//获取剪贴板中的内容
            if(string.IsNullOrEmpty(clipboardText))
            {
                return;
            }
            int column = 0;
            int rownum = 0;
            for(int i=0;i<clipboardText.Length;i++)
            {
                if(clipboardText.Substring(i,1)=="\t")
                {
                    column++;
                }
                if(clipboardText.Substring(i,1)=="\n")
                {
                    rownum++;
                }
            }
            column = column / rownum + 1;
            int selectedRowIndex, selectedColIndex;
            if(dgv.ColumnCount==0)
            {
                selectedRowIndex = 0;
                selectedColIndex = 0;
            }
            else
            {
                selectedRowIndex = dgv.CurrentRow.Index;
                selectedColIndex = dgv.CurrentCell.ColumnIndex;
            }
            if (selectedColIndex + column > dgv.ColumnCount)
            {
                for (int i = dgv.ColumnCount; i < selectedColIndex + column; i++)
                {
                    DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                    acCode.Name = col.ToString();
                    acCode.DataPropertyName = col.ToString();
                    acCode.HeaderText = " ";
                    dgv.Columns.Add(acCode);
                    col++;
                }
            }
            if (selectedRowIndex + rownum > dgv.Rows.Count)
            {
                for (int i = dgv.Rows.Count; i < selectedRowIndex + rownum; i++)
                {
                    i = dgv.Rows.Add();
                }
            }          
            if (selectedRowIndex + rownum > dgv.Rows.Count && selectedColIndex + column > dgv.ColumnCount)
            {
                for (int i = dgv.Rows.Count; i < selectedRowIndex + rownum; i++)
                {
                    i = dgv.Rows.Add();
                }
                for (int i = dgv.ColumnCount; i < selectedColIndex + column; i++)
                {
                    DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                    acCode.Name = col.ToString();
                    acCode.DataPropertyName = col.ToString();
                    acCode.HeaderText = " ";
                    dgv.Columns.Add(acCode);
                    col++;
                }
            }
            string[][] temp = new string[rownum][];
            for(int i=0;i<rownum;i++)
            {
                temp[i] = new string[column];
            }
            int m = 0, n = 0, len = 0;
            while(len!=clipboardText.Length)
            {
                string str = clipboardText.Substring(len, 1);
                if(str=="\t")
                {
                    n++;
                }
                else if(str=="\n")
                {
                    m++;
                    n = 0;
                }
                else
                {
                    temp[m][n] += str;
                }
                len++;
            }
            for(int i=selectedRowIndex;i<selectedRowIndex+rownum;i++)
            {
                for(int j=selectedColIndex;j<selectedColIndex+column;j++)
                {
                    dgv.Rows[i].Cells[j].Value = temp[i - selectedRowIndex][j - selectedColIndex];
                }
            }
        }
        #endregion

        #region 复制单列数据
        public void CopyFromDgv(DataGridView dgv,int column)
        {
            if(dgv.GetCellCount(DataGridViewElementStates.Selected)>0)
            {
                try
                {
                    string str=string.Empty;
                    for(int i=0;i<dgv.Rows.Count;i++)
                    {
                        str+= dgv.Rows[i].Cells[column].Value.ToString()+Environment.NewLine;       
                    }
                    Clipboard.SetDataObject(str);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 复制多列数据
        public void CopyMultiColFromDgv(DataGridView dgv, int start,int len)
        {
            if(dgv.GetCellCount(DataGridViewElementStates.Selected)>0)
            {
                try
                {
                    string str = string.Empty;
                    for(int i=0;i<dgv.Rows.Count;i++)
                    {
                        for(int j=start;j<=(start+len);j++)
                        {
                            str += @dgv.Rows[i].Cells[j].Value.ToString() + "\t";
                        }
                        str = str + Environment.NewLine;
                    }
                    Clipboard.SetDataObject(str);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 自动选中表格中的有效数据
        public void SetChecked(DataGridView dgv,int col_check,int col1,int col2)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if(dgv.Rows[i].Cells[col1].Value.ToString()=="0"&&
                    dgv.Rows[i].Cells[col2].Value.ToString()=="0")
                {
                   
                }
                else
                {
                    dgv.Rows[i].Cells[col_check].Value = CheckState.Checked;
                }
            }
        }
        #endregion

        #region 粘贴数据到固定列
        public void PasteDataToCol(DataGridView dgv,int col_num)
        {
            int col = 1;
            string clipboardText = Clipboard.GetText();//获取剪贴板中的内容
            if (string.IsNullOrEmpty(clipboardText))
            {
                return;
            }
            int column = 0;
            int rownum = 0;
            for (int i = 0; i < clipboardText.Length; i++)
            {
                if (clipboardText.Substring(i, 1) == "\t")
                {
                    column++;
                }
                if (clipboardText.Substring(i, 1) == "\n")
                {
                    rownum++;
                }
            }
            column = column / rownum + 1;
            int selectedRowIndex = 0, selectedColIndex = col_num;
            if (selectedColIndex + column > dgv.ColumnCount)
            {
                for (int i = dgv.ColumnCount; i < selectedColIndex + column; i++)
                {
                    DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                    acCode.Name = col.ToString();
                    acCode.DataPropertyName = col.ToString();
                    acCode.HeaderText = " ";
                    dgv.Columns.Add(acCode);
                    col++;
                }
            }
            if (selectedRowIndex + rownum > dgv.Rows.Count)
            {
                for (int i = dgv.Rows.Count; i < selectedRowIndex + rownum; i++)
                {
                    i = dgv.Rows.Add();
                }
            }
            if (selectedRowIndex + rownum > dgv.Rows.Count && selectedColIndex + column > dgv.ColumnCount)
            {
                for (int i = dgv.Rows.Count; i < selectedRowIndex + rownum; i++)
                {
                    i = dgv.Rows.Add();
                }
                for (int i = dgv.ColumnCount; i < selectedColIndex + column; i++)
                {
                    DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                    acCode.Name = col.ToString();
                    acCode.DataPropertyName = col.ToString();
                    acCode.HeaderText = " ";
                    dgv.Columns.Add(acCode);
                    col++;
                }
            }
            string[][] temp = new string[rownum][];
            for (int i = 0; i < rownum; i++)
            {
                temp[i] = new string[column];
            }
            int m = 0, n = 0, len = 0;
            while (len != clipboardText.Length)
            {
                string str = clipboardText.Substring(len, 1);
                if (str == "\t")
                {
                    n++;
                }
                else if (str == "\n")
                {
                    m++;
                    n = 0;
                }
                else
                {
                    temp[m][n] += str;
                }
                len++;
            }
            for (int i = selectedRowIndex; i < selectedRowIndex + rownum; i++)
            {
                for (int j = selectedColIndex; j <= selectedColIndex ; j++)
                {
                    dgv.Rows[i].Cells[j].Value = temp[i - selectedRowIndex][j - selectedColIndex];
                }
            }
        }

        #endregion

        #region 将表格中的数据进行排序
        public void SortData(DataGridView dgv,int col1,int col2)
        {
            int col = 0;
            List<string> data = new List<string>();
            List<string[]> data_result = new List<string[]>();
            for(int i=0;i<dgv.Rows.Count;i++)
            {
              if(dgv.Rows[i].Cells[col1].Value.ToString()!="0")
              {                
                  data.Add(dgv.Rows[i].Cells[col1].Value.ToString());
              }
            }
            string[] data_str = new string[data.Count];
            for(int i=0;i<data.Count;i++)
            {
                data_str[i] = data[i];
            }
            int[] data_int = SortArray(data_str);
            List<int> index = new List<int>();
            for(int i=0;i<data.Count;i++)
            {
                int index1 = data.IndexOf(data_int[i].ToString());
                index.Add(index1);
            }
            for(int i=0;i<index.Count;i++)/////
            {
                col = 0;
                string[] str1 = new string[(col2-col1+1)];
                for(int j=col1;j<=col2;j++)
                {
                    str1[col] = dgv.Rows[index[i]].Cells[j].Value.ToString();
                    col++;
                }
                data_result.Add(str1);
            }
            for(int i=0;i<data_result.Count;i++)
            {
                dgv.Rows[i].Cells[col1].Value = data_result[i][0];
            }
            for (int i = 0; i < data_result.Count; i++)
            {
                dgv.Rows[i].Cells[col2].Value = data_result[i][1];
            }
        }
        #endregion

        #region List<double>转换为double[]
        public double[] ConvertList(List<double> data)
        {
            double[] data_con = new double[data.Count];
            for(int i=0;i<data.Count;i++)
            {
                data_con[i] = data[i];
            }
            return data_con;
        }
        #endregion

        #region 根据y=ax+b计算y值
        public double[] Cal_Y(double[] x,double a,double b,string stry)
        {
            double[] y = new double[x.Length];
            double[] result = new double[x.Length];
            for(int i=0;i<x.Length;i++)
            {
                y[i] = x[i] * a + b;
            }
            if(stry=="无变换")
            {
                for(int i=0;i<y.Length;i++)
                {
                    result[i] = y[i];
                }
            }
            else if(stry=="取对数")
            {
                for(int i=0;i<y.Length;i++)
                {
                    result[i] = Math.Pow(10, y[i]);
                }
            }
            else if (stry == "自然对数")
            {
                for (int i = 0; i < y.Length; i++)
                {
                    result[i] = Math.Pow(Math.E, y[i]);
                }
            }
            else if(stry=="底为2对数")
            {
                for (int i = 0; i < y.Length; i++)
                {
                    result[i] = Math.Pow(2, y[i]);
                }
            }
            return result;
        }
        #endregion

        #region 查找某一数值在数组中的最接近的值
        public double ClosesTo(double[] collection,double target)
        {
            var closet = collection.OrderBy(x => Math.Abs(target - x)).First();
            double result = Convert.ToDouble(closet);
            return result;
        }
        #endregion

        #region 线性插值
        public double LinearFit(double[] x, double[] y, double xi,string str)
        {
            double yi = 0;
            Array.Sort(x);
            if(str=="无变换")
            {
                
            }
            else if(str=="取对数")
            {
                xi = Math.Log10(xi);
            }
            else if(str=="自然对数")
            {
                xi = Math.Log(xi);
            }
            else if(str=="底为2对数")
            {
                xi = Math.Log(xi, 2);
            }
            if (xi < x[0])
            {
                yi = ((xi - x[1]) / (x[0] - x[1])) * y[0] + ((xi - x[0]) / (x[1] - x[0])) * y[1];
            }
            else if (xi > x[x.Length - 1])
            {
                yi = ((xi - x[x.Length - 1]) / (x[x.Length - 2] - x[x.Length - 1])) * y[y.Length - 2] + ((xi - x[x.Length - 2]) / (x[x.Length - 1] - x[x.Length - 2])) * y[y.Length - 1];
            }
            else if (xi >= x[0] && xi <= x[x.Length - 1])
            {
                double closest = ClosesTo(x, xi);
                double dif = xi - closest;
                int index = Array.IndexOf(x, closest);
                if (dif > 0)
                {
                    yi = ((xi - x[index + 1]) / (closest - x[index + 1])) * y[index] + ((xi - closest) / (x[index + 1] - closest)) * y[index];
                }
                else if (dif < 0)
                {
                    yi = ((xi - closest) / (x[index - 1] - closest)) * y[index - 1] + ((xi - x[index - 1]) / (closest - x[index - 1])) * y[index];
                }
                else if (dif == 0)
                {
                    yi = y[index];
                }
            }
            if (str == "无变换")
            {
                
            }
            else if (str == "取对数")
            {
                yi = Math.Pow(10, yi);
            }
            else if (str == "自然对数")
            {
                yi = Math.Pow(Math.E, yi);
            }
            else if (str == "底为2对数")
            {
                yi = Math.Pow(2, yi);
            }
            return yi;
        }
        #endregion

        #region 当浓度值与反应值为取对数且拟合方法为线性插值时绘制曲线
        public void DrawLinearLines(double[] con_x,double[] con_y,Color color,Chart chart1)
        {
            int count = con_x.Length;
            var chart = chart1.ChartAreas[0];
            //设置坐标轴样式
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.AxisX.MajorGrid.LineColor = Color.Gray;
            chart.AxisY.MajorGrid.LineColor = Color.Gray;

            chart.AxisX.Minimum = Math.Round((con_x.Min()-0.01),2);
            chart.AxisX.Maximum = Math.Round((con_x.Max() + 0.01),2);
            chart.AxisY.Minimum = Math.Round((con_y.Min()-0.01),2);
            chart.AxisY.Maximum = Math.Round((con_y.Max() + 0.01),2);
            chart.AxisX.Interval = Math.Round((con_x.Max()-con_x.Min()) / 10,2);
            chart.AxisY.Interval = Math.Round((con_y.Max()-con_y.Min()) / 10,2);
            chart.BackColor = System.Drawing.Color.Transparent;//设置区域内背景透明
            int n1 = chart1.Series.Count;
            string str = n1.ToString();
            chart1.Series.Add(str);
            //绘制曲线图
            chart1.Series[str].ChartType = SeriesChartType.Line;
            chart1.Series[str].Color = color;
            for (int i = 0; i < count; i++)
            {
                chart1.Series[str].Points.AddXY(con_x[i], con_y[i]);
            }
        }
        #endregion

        #region 计算偏差
        public double[] Cal_dif(double[] y,double[] y_cal)
        {
            double[] dif = new double[y.Length];
            for(int i=0;i<y.Length;i++)
            {
                dif[i] = ((y_cal[i] - y[i]) / y[i]) * 100;
            }
            return dif;
        }
        #endregion

        #region 向计算偏差表格中填值
        public void AddValueToDgv(double[] data,int col,DataGridView dgv)
        {
            if (col == 3)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    dgv.Rows[i].Cells[col].Value = Math.Round(data[i], 2).ToString();
                }
            }
            else if (col == 4)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    dgv.Rows[i].Cells[col].Value = Math.Round(data[i], 4).ToString();
                }
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    dgv.Rows[i].Cells[col].Value = data[i].ToString();
                }
            }
        }
        #endregion

        #region 计算直线拟合的X值
        public double[] Cal_X(double[] X,double[] Y,string ChangeMethod)
        {
            double[] result = new double[X.Length];
            double[] f = ecf.Linear(X, Y);
            for(int i=0;i<X.Length;i++)
            {
                result[i] = (X[i] - f[0]) / f[1];
            }
            switch(ChangeMethod)
            {
                case"无变换":
                    break;
                case"取对数":
                    for(int i=0;i<result.Length;i++)
                    {
                        result[i] = Math.Pow(10, result[i]);
                    }
                    break;
                case"自然对数":
                    for(int i=0;i<result.Length;i++)
                    {
                        result[i] = Math.Pow(Math.E, result[i]);
                    }
                    break;
                case"底为2对数":
                    for(int i=0;i<result.Length;i++)
                    {
                        result[i] = Math.Pow(2, result[i]);
                    }
                    break;
            }
            return result;
        }
        #endregion

        
    }
}
