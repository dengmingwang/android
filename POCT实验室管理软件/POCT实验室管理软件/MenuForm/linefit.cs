using POCT实验室管理软件.Common_Method;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCT实验室管理软件.MenuForm
{
    public partial class linefit : Form
    {
        //表格中选取的数值
        List<double> selectdata1;
        List<double> selectdata2;     
        private static linefit formInstance2;
        Common_use comuse = new Common_use();
        string path_in = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "_" + ".TST";
        string path_or = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "副本" + ".TST";
        string root_directory = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\";
        List<ItemInfo> iteminf = new List<ItemInfo>();
        List<LineImfo> lineimfo = new List<LineImfo>();//保存拟合曲线实验参数
        public linefit()
        {
            InitializeComponent();
        }

        //解决闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        //实例化子窗体
        public static linefit GetIntance
        {
            get
            {
                if (formInstance2 != null)
                {
                    return formInstance2;
                }
                else
                {
                    formInstance2 = new linefit();
                    return formInstance2;
                }
            }
        }
 
        /// <summary>
        /// 将表格中选中行的TRUE VALUE 置为true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentCell.ColumnIndex==1)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.CurrentRow.Cells["select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                checkCell.Value = (!flag);
            }
        }
       //拟合方法
        public void DrawLine(double[] source_x,double[] source_y,double[] strX, double[] strY, string str1,string str2,string comboxtext)
        {
            label10.Visible = false;
            textBox2.Visible = false;
            chart1.Series.Clear();
            dataGridView3.Rows.Clear();
            int length = strX.Count();
            double[] Y_fit = new double[length];
            if (comboxtext == "多项式拟合")
            {
                double[] f = ecf.MultiLine(strX, strY, length, 2);
                for (int i = 0; i < length; i++)
                {
                    Y_fit[i] = f[0] + f[1] * strX[i] + f[2] * strX[i] * strX[i];
                }
                string text = comboxtext;
                textBox1.Text = ecf.Person(strX, strY).ToString() ;
                label10.Text = "y=ax＾2+bx+c";
                label10.Visible = true;
                textBox2.Text = "a" + "\t" + f[2].ToString() + Environment.NewLine + 
                    "b" + "\t" + f[1].ToString() + Environment.NewLine + "c" + "\t" + f[0].ToString();
                textBox2.Visible = true;
                comuse.DrawFitLine(source_x, source_y, Color.Red, text, chart1);
               
            }
            else if (comboxtext == "直线拟合")
            {
                double[] f = ecf.Linear(strY, strX);
                for (int i = 0; i < length; i++)
                {
                    Y_fit[i] = f[0] + f[1] * strX[i];
                }
                string text = comboxtext;
                label10.Visible = true;
                textBox2.Visible = true;
                textBox1.Text = ecf.Person(strX, strY).ToString();
                textBox2.Text = "a" + "\t" + f[1].ToString() + Environment.NewLine + "b" + "\t" + f[0].ToString();
                double[] y_cal = comuse.Cal_Y(strX, f[1], f[0],str2);
                double[] x_cal = comuse.Cal_X(strY, strX, str1);
                comuse.DrawFitLine(source_x, source_y,Color.Red, text, chart1);
                comuse.DrawLineofCal(source_x, y_cal, Color.Blue, chart1);
                double[] dif = comuse.Cal_dif(source_x, x_cal);
                for(int i=0;i<source_y.Length;i++)
                {
                    i = dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
                comuse.AddValueToDgv(source_y, 1,dataGridView3);
                comuse.AddValueToDgv(source_x, 2, dataGridView3);
                comuse.AddValueToDgv(x_cal, 3, dataGridView3);
                comuse.AddValueToDgv(dif, 4, dataGridView3);
            }
            else if (comboxtext == "幂函数拟合")
            {
                double[] f = ecf.PowEST(strY, strX);
                for (int i = 0; i < length; i++)
                {
                    Y_fit[i] = f[0] * Math.Pow(strX[i], f[1]);
                }
                string text = comboxtext;
                textBox1.Text = ecf.Person(strX, strY).ToString();
                comuse.DrawFitLine(source_x, source_y, Color.Red, text, chart1);                            
            }
            else if (comboxtext == "对数拟合")
            {
                double[] f = ecf.LOGEST(strY, strX);
                for (int i = 0; i < length; i++)
                {
                    Y_fit[i] = f[1] * Math.Log(strX[i]) + f[0];
                }
                string text = comboxtext;
                textBox1.Text = ecf.Person(strX, strY).ToString();
                comuse.DrawFitLine(source_x, source_y, Color.Red, text, chart1);                          
            }
            else if (comboxtext == "指数拟合")
            {
                double[] f = ecf.IndexEST(strY, strX);
                for (int i = 0; i < length; i++)
                {
                    Y_fit[i] = f[0] * Math.Pow(f[1], strX[i]);
                }
                string text = comboxtext;
                textBox1.Text = ecf.Person(strX, strY).ToString();
                comuse.DrawFitLine(source_x, source_y, Color.Red, text, chart1);                         
            }
            else if(comboxtext=="线性插值")
            {
                textBox1.Text = ecf.Person(strX, strY).ToString();
                double y1 = comuse.LinearFit(strX,strY,2,str1);
                if(str1=="无变换")
                {
                    string text = comboxtext;
                    comuse.DrawLinearLines(source_x, source_y, Color.Blue, chart1);
                    comuse.DrawFitLine(source_x, source_y, Color.Red, text, chart1);
                }
                else
                {
                    string text = comboxtext;
                    comuse.DrawLinearLines(strX, strY, Color.Blue, chart1);
                    comuse.DrawFitLine(strX, strY, Color.Red, text, chart1);                 
                }
                for (int i = 0; i < source_y.Length; i++)
                {
                    i = dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
                comuse.AddValueToDgv(source_y, 1, dataGridView3);
                comuse.AddValueToDgv(source_x, 2, dataGridView3);
                comuse.AddValueToDgv(source_x, 3, dataGridView3);
                for(int i=0;i<source_y.Length;i++)
                {
                    dataGridView3.Rows[i].Cells[4].Value = "0";
                }
            }

        }
        //拟合按钮
        private void button2_Click(object sender, EventArgs e)
        {
           try
           {
               string combotext1 = string.Empty;
               string combotext2 = string.Empty;
               string combotext3 = string.Empty;
               if (comboBox1.Text != null)
               {
                   combotext1 = comboBox1.Text.Trim();
               }
               else
               {
                   combotext1 = "无变换";
               }
               if (comboBox2.Text != null)
               {
                   combotext2 = comboBox2.Text.Trim();
               }
               else
               {
                   combotext2 = "无变换";
               }
               if (comboBox3.Text != null)
               {
                   combotext3 = comboBox3.Text.Trim();
               }
               else
               {
                   combotext3 = "直线拟合";
               }
               label12.Text = combotext2;//浓度值
               label14.Text = combotext1;//反应值
               label16.Text = combotext3;//拟合方法
               selectdata1 = comuse.GetCheckValue(2, dataGridView1);//浓度值
               selectdata2 = comuse.GetCheckValue(3, dataGridView1);//反应值
               double[] data_x = comuse.ConvertList(selectdata1);
               double[] data_y = comuse.ConvertList(selectdata2);
               double[] convertdata1 = comuse.NumConvert(selectdata1, combotext2);//浓度值
               double[] convertdata2 = comuse.NumConvert(selectdata2, combotext1);//反应值
               int length = convertdata1.Length;
               DrawLine(data_x,data_y,convertdata1, convertdata2, combotext2,combotext1,combotext3);
           }
           catch(Exception ex)
           {
               MessageBox.Show(ex.Message);
               return;
           }
           
        }
        //页面加载
        private void linefit_Load(object sender, EventArgs e)
        {
             if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest", ".TST"))//判断文件夹中是否含有已保存的实验名称
             {
                 MyEncrypt.SHA_Dencrypt(path_in, path_or, "179346");//解密             
                 iteminf = comuse.ReadItem(path_or);
                 comuse.DeleteOneFile(path_or);//删除解密文件           
                 dataGridView2.Rows.Clear();
                 for (int i = 0; i < iteminf.Count; i++)
                 {
                     i = dataGridView2.Rows.Add();
                     dataGridView2.Rows[i].Cells[0].Value = iteminf[i].Itemnumber;
                     dataGridView2.Rows[i].Cells[1].Value = iteminf[i].Itemname;
                     dataGridView2.Rows[i].Cells[2].Value = iteminf[i].Itemexplain;
                 }
             }
            for(int i=0;i<20;i++)
            {
                i = dataGridView1.Rows.Add();
                i = dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[2].Value = "0";
                dataGridView1.Rows[i].Cells[3].Value = "0";
                dataGridView3.Rows[i].Cells[1].Value = "0";
                dataGridView3.Rows[i].Cells[2].Value = "0";
                dataGridView3.Rows[i].Cells[3].Value = "0";
                dataGridView3.Rows[i].Cells[4].Value = "0";
            }
        }
        //保存拟合曲线实验参数按钮
        private void button4_Click(object sender, EventArgs e)
        {
            int index_select = dataGridView2.CurrentRow.Index;
            string path = root_directory + "\\" + dataGridView2.Rows[index_select].Cells[0].Value.ToString() + "\\" + dataGridView2.Rows[index_select].Cells[0].Value.ToString()+".TCS";
            LineImfo line = new LineImfo();
            line.potency = new List<string>();
            line.reponse = new List<string>();
            line.calpotency = new List<string>();
            line.Std = new List<string>();
            if (comboBox6.Text.Trim()!= "")
            {
                line.Linenum = comboBox6.Text.Trim();//曲线序号
            }
            else
            {
                MessageBox.Show("请选择曲线序号！！！");
                return;
            }
            line.method_reponse = comboBox1.Text.Trim();//反应值变换方法
            line.method_potency = comboBox2.Text.Trim();//浓度值变换方法
            line.deicalplace_reponse = comboBox4.Text.Trim();//反应值小数位数
            line.demicalplace_potency = comboBox5.Text.Trim();//浓度值小数位数
            line.methodLinefit = comboBox3.Text.Trim();//拟合算法
            line.result_linefit= textBox1.Text.Trim()+"\t"+ textBox2.Text.Trim();//拟合结果         
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if(dataGridView1.Rows[i].Cells[2].Value.ToString()!="0")
                {
                    line.potency.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());//浓度
                    line.reponse.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());//反应值
                    line.calpotency.Add(dataGridView3.Rows[i].Cells[3].Value.ToString());//计算浓度
                    line.Std.Add(dataGridView3.Rows[i].Cells[4].Value.ToString());//偏差
                }
            }
            lineimfo.Add(line);
            if(comuse.FileExist(root_directory + "\\" + dataGridView2.Rows[index_select].Cells[0].Value.ToString() + "\\",".TCS"))
            {
                //List<LineImfo> line_or = new List<LineImfo>();
                //line_or = comuse.ReadLineImf(path);
                //for(int i=0;i<lineimfo.Count;i++)
               // {
                    //line_or.Add(lineimfo[i]);                  
                //}
               // comuse.WriteLinefitInfo(line_or, path);
                comuse.WriteLinefitInfo(lineimfo, path);
            }
            else
            {
                comuse.WriteLinefitInfo(lineimfo, path);
            }
        }
        //表格右键选择复制或粘贴数据
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
             if(e.Button==MouseButtons.Right)//点击鼠标右键
             {
                 for(int i=0;i<5;i++)
                 {
                     i = dataGridView6.Rows.Add();
                     switch(i)
                     {
                         case 0:
                             dataGridView6.Rows[i].Cells[0].Value = "复制浓度";
                             break;
                         case 1:
                             dataGridView6.Rows[i].Cells[0].Value = "复制反应值";
                             break;
                         case 2:
                             dataGridView6.Rows[i].Cells[0].Value = "复制数值";
                             break;
                         case 3:
                             dataGridView6.Rows[i].Cells[0].Value = "粘贴浓度";
                             break;
                         case 4:
                             dataGridView6.Rows[i].Cells[0].Value = "粘贴反应值";
                             break;
                     }
                 }
                 dataGridView6.Visible = true;
                 panel3.Visible = true;
             }
        }
        //粘贴复制表格
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(dataGridView6.CurrentCell.Value.ToString())
            {
                case"复制浓度":
                    comuse.CopyFromDgv(dataGridView1,2);
                    dataGridView6.Visible = false;
                    panel3.Visible=false;
                    break;
                case"复制反应值":
                    comuse.CopyFromDgv(dataGridView1, 3);
                    dataGridView6.Visible = false;
                    panel3.Visible = false;
                    break;
                case "复制数值":
                    comuse.CopyMultiColFromDgv(dataGridView1, 2, 1);
                    dataGridView6.Visible = false;
                    panel3.Visible = false;
                    break;
                case "粘贴浓度":
                    comuse.PasteDataToCol(dataGridView1, 2);
                    dataGridView6.Visible = false;
                    panel3.Visible = false;
                    break;
                case "粘贴反应值":
                    comuse.PasteDataToCol(dataGridView1, 3);
                    dataGridView6.Visible = false;
                    panel3.Visible = false;
                    break;
            }
        }
        //自动选中
        private void button1_Click(object sender, EventArgs e)
        {
            comuse.SetChecked(dataGridView1, 1, 2, 3);
        }
        //自动排序
        private void button3_Click(object sender, EventArgs e)
        {
            comuse.SortData(dataGridView1, 2, 3);
        }
        //清除按钮
        private void btn_clear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView3.Rows.Clear();
            chart1.Series.Clear();
            for (int i = 0; i < 20; i++)
            {
                i = dataGridView1.Rows.Add();
                i = dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[2].Value = "0";
                dataGridView1.Rows[i].Cells[3].Value = "0";
                dataGridView3.Rows[i].Cells[1].Value = "0";
                dataGridView3.Rows[i].Cells[2].Value = "0";
                dataGridView3.Rows[i].Cells[3].Value = "0";
                dataGridView3.Rows[i].Cells[4].Value = "0";
            }
        }

       
    }
}
