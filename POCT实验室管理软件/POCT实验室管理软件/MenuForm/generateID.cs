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
using 干式荧光免疫实验管理软件.Common_Method;

namespace POCT实验室管理软件.MenuForm
{
    public partial class generateID : Form
    {
        private static generateID formInstance4;
        Common_use comuse = new Common_use();
        string path_in = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "_" + ".TST";
        string path_or = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "副本" + ".TST";
        string path_tst = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\";
        string root_directory = @"E:\VS13\POCT\POCT实验室管理软件\Item\";
        int selectColumnCount;
        List<string> re = new List<string>();//保存项目相关信息的数组
        List<ItemInfo> iteminf = new List<ItemInfo>();
        List<string[]> subitem = new List<string[]>();//子项参数
        List<string[]> samcoffi = new List<string[]>();//样本系数
        List<string[]> groupoutput = new List<string[]>();//组合输出
        List<string[]> peakpos = new List<string[]>();//峰值位置及点数
        List<string[]> samtype = new List<string[]>();//样本类型
        List<LineImfo> line_or = new List<LineImfo>();
        List<string> sam_de = new List<string>();//样本系数小数位
        Order or = new Order();
        BaseCom com;
        public delegate void delegateOnOff(bool onoff);
        public delegate void delegateShow(string msg);
        string receivemsg = string.Empty;
        CreatLineOrder creatline = new CreatLineOrder();
        List<byte[]> order_list = new List<byte[]>();//通讯协议储存表
        List<byte[]> order_list_line = new List<byte[]>();//通讯协议储存表
        List<string[]> SAMTC1TC2 = new List<string[]>();//样本类型、样本系数、TC1、TC2
        string[] TC1_1 = new string[9];
        string[] TC2_1 = new string[9];
        string[] TC1_2 = new string[9];
        string[] TC2_2 = new string[9];
        string[] TC1_3 = new string[9];
        string[] TC2_3 = new string[9];
        string[] TC1_4 = new string[9];
        string[] TC2_4 = new string[9];
        string[] TC1_5 = new string[9];
        string[] TC2_5 = new string[9];
        bool btn1click = false;
        bool btn2click = false;
        bool btn3click = false;
        bool btn4click = false;
        bool btn5click = false;
        public generateID()
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
        public static generateID GetIntance
        {
            get
            {
                if (formInstance4 != null)
                {
                    return formInstance4;
                }
                else
                {
                    formInstance4 = new generateID();
                    return formInstance4;
                }
            }
        }

        private void generateID_Load(object sender, EventArgs e)
        {
            btn1click = false;
            btn2click = false;
            btn3click = false;
            btn4click = false;
            btn5click = false;
            #region UI界面填充
            for (int i = 0; i < 5;i++)
            {
                i = dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            for (int i = 0; i < 9; i++)
            {
                i = dataGridView5.Rows.Add();
                dataGridView5.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            #endregion
            #region 实验相关数据填充
            if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest", ".TST"))//判断文件夹中是否含有已保存的实验名称
            {
                MyEncrypt.SHA_Dencrypt(path_in, path_or, "179346");//解密             
                iteminf = comuse.ReadItem(path_or);
                comuse.DeleteOneFile(path_or);//删除解密文件           
                dataGridView1.Rows.Clear();
                for (int i = 0; i < iteminf.Count; i++)
                {
                    i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = iteminf[i].Itemnumber;
                    dataGridView1.Rows[i].Cells[1].Value = iteminf[i].Itemname;
                    dataGridView1.Rows[i].Cells[2].Value = iteminf[i].Itemexplain;
                }
            }
            if(dataGridView1.Rows[0].Cells[0].Value!=null)
            {
                MyEncrypt.SHA_Dencrypt(path_tst + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + ".TST",
                    path_tst + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "副本.TST", "179346");//解密    
                re = comuse.ReadBasFile(path_tst + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "副本.TST");
                comuse.DeleteOneFile(path_tst + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "副本.TST");
                textBox1.Text = re[0];//项目名称
                textBox3.Text = re[1];//项目代码
                textBox2.Text = re[2];//仪器类型
                panel2.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                switch (re[3])//子项目个数
                {
                    case"1":
                        panel2.Visible = true;
                        panel2.Location=new Point(1080,2);
                        button1.Visible = true;
                        break;
                    case"2":
                        panel2.Visible = true;
                        panel2.Location = new Point(1080, 2);
                        button1.Visible = true;
                        button2.Visible = true;
                        break;
                    case"3":
                        panel2.Visible = true;
                        panel2.Location = new Point(1080, 2);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        break;
                    case"4":
                        panel2.Visible = true;
                        panel2.Location = new Point(1080, 2);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        break;
                    case"5":
                        panel2.Visible = true;
                        panel2.Location = new Point(1080, 2);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        button5.Visible = true;
                        break;
                }
                textBox4.Text = re[4];//预读时间
                textBox5.Text = re[5];//测试时间
                if (re[6] == "是")//是否二次稀释
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if (re[7] == "判定")//是否判定未加样
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                textBox6.Text = re[8];//峰个数
                textBox7.Text = re[9];//二次缓冲液量
                textBox8.Text = re[10];//预读判断阈值
                if (Convert.ToInt32(re[8])>5)
                {
                    label36.Visible = true;
                    textBox23.Visible = true;
                    textBox23.Text = re[11];
                }
               textBox9.Text = re[12];//二次混合液量
               textBox10.Text = re[13];//峰序号
               textBox11.Text = re[14];//取峰算法
                //组合输出
               if (comuse.FindSignInString(re, 0, '\t', 8).Count == 0)
               {
                   dataGridView9.Rows.Clear();
                   for (int i = 0; i < 8; i++)
                   {
                       i = dataGridView9.Rows.Add();
                       switch (i)
                       {
                           case 0:
                               dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                               break;
                           case 1:
                               dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                               break;
                           case 2:
                               dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                               break;
                           case 3:
                               dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                               break;
                           case 4:
                               dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                               break;
                           case 5:
                               dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                               break;
                           case 6:
                               dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                               break;
                           case 7:
                               dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                               break;
                       }
                   }
               }
               else
               {
                   dataGridView9.Rows.Clear();
                   for (int i = 0; i < 8; i++)
                   {
                       i = dataGridView9.Rows.Add();
                       switch (i)
                       {
                           case 0:
                               dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                               break;
                           case 1:
                               dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                               break;
                           case 2:
                               dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                               break;
                           case 3:
                               dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                               break;
                           case 4:
                               dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                               break;
                           case 5:
                               dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                               break;
                           case 6:
                               dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                               break;
                           case 7:
                               dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                               break;
                       }
                   }
                   comuse.AddDataToGroup(comuse.FindSignInString(re, 0, '\t', 8), dataGridView9);//组合输出
               }
                //样本类型
               comuse.AddSamDatagridview(comuse.FindSignInString(re, 0, '\t', 3), dataGridView2);//样本类型，加样量，缓冲液量以及混合液量
                //峰值
               comuse.AddDatagridview(comuse.FindSignInString(re, 0, '\t', 4), dataGridView3);//峰值
               subitem = comuse.SeparateSubItem(re, 0, '\t', 12);//子项基本参数
                if(subitem.Count>0)
                {
                    for (int j = 0; j < subitem[0].Length - 9; j++)
                    {
                        dataGridView4.Rows[0].Cells[j + 1].Value = subitem[0][j + 4];
                    }
                    comboBox6.Text = subitem[0][1];
                    comboBox12.Text = subitem[0][8];
                    comboBox11.Text = subitem[0][9];
                    comboBox10.Text = subitem[0][10];
                    comboBox9.Text = subitem[0][11];
                    comboBox8.Text = subitem[0][12];
                }
                else
                {
                    dataGridView4.Rows.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = (i + 1).ToString();
                    }
                    comboBox6.Text = " ";
                    comboBox12.Text = " ";
                    comboBox11.Text = " ";
                    comboBox10.Text = " ";
                    comboBox9.Text = " ";
                    comboBox8.Text = " ";
                }              
               samcoffi = comuse.SeparateSubItem(re, 0, '\t', 2);//子项样本系数
                if(samcoffi.Count>0)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                        dataGridView5.Rows[j].Cells[2].Value = "1";
                        dataGridView5.Rows[j].Cells[3].Value = "1";
                    }
                    comboBox4.Text = samcoffi[0][2];
                }
                else
                {
                    for(int i=0;i<9;i++)
                    {
                        dataGridView5.Rows[i].Cells[1].Value = "1";
                        dataGridView5.Rows[i].Cells[2].Value = "1";
                        dataGridView5.Rows[i].Cells[3].Value = "1";
                    }
                }
            }
            else
            {
                dataGridView9.Rows.Clear();
                for (int i = 0; i < 8; i++)
                {
                    i = dataGridView9.Rows.Add();
                    switch (i)
                    {
                        case 0:
                            dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                            break;
                        case 1:
                            dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                            break;
                        case 2:
                            dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                            break;
                        case 3:
                            dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                            break;
                        case 4:
                            dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                            break;
                        case 5:
                            dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                            break;
                        case 6:
                            dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                            break;
                        case 7:
                            dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                            break;
                    }
                }
            }
            #endregion
            #region 曲线拟合相关数据填充
            string path = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString()
                + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + ".TCS";
            line_or = comuse.ReadLineImf(path);//读取数据
            for(int i=0;i<line_or[0].potency.Count;i++)
            {
                i = dataGridView6.Rows.Add();
                i = dataGridView7.Rows.Add();
                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView6.Rows[i].Cells[1].Value = "选中";
                dataGridView6.Rows[i].Cells[2].Value = line_or[0].potency[i];
                dataGridView6.Rows[i].Cells[3].Value = line_or[0].reponse[i];
                dataGridView7.Rows[i].Cells[1].Value = line_or[0].reponse[i];
                dataGridView7.Rows[i].Cells[2].Value = line_or[0].potency[i];
                dataGridView7.Rows[i].Cells[3].Value = line_or[0].calpotency[i];
                dataGridView7.Rows[i].Cells[4].Value = line_or[0].Std[i];
            }
            textBox16.Text = line_or[0].method_reponse;
            textBox17.Text = line_or[0].deicalplace_reponse;
            textBox18.Text = line_or[0].method_potency;
            textBox19.Text = line_or[0].demicalplace_potency;
            textBox20.Text = line_or[0].methodLinefit;
            #endregion
        }
        //子项1
        private void button1_Click(object sender, EventArgs e)
        {
            btn1click = true;
            btn2click = false;
            btn3click = false;
            btn4click = false;
            btn5click = false;
            if(subitem.Count!=0)
            {
                for (int j = 0; j < subitem[0].Length - 9; j++)
                {
                    dataGridView4.Rows[0].Cells[j + 1].Value = subitem[0][j + 4];
                }
                comboBox6.Text = subitem[0][1];
                comboBox12.Text = subitem[0][8];
                comboBox11.Text = subitem[0][9];
                comboBox10.Text = subitem[0][10];
                comboBox9.Text = subitem[0][11];
                comboBox8.Text = subitem[0][12];
                for (int j = 0; j < 9; j++)
                {
                    dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                }
                comboBox4.Text = samcoffi[0][2];
            }
        }
        //子项2
        private void button2_Click(object sender, EventArgs e)
        {
            btn2click = true;
            btn1click = false;
            btn3click = false;
            btn4click = false;
            btn5click = false;
            if(subitem.Count>=1)
            {
                for (int j = 0; j < subitem[1].Length - 9; j++)
                {
                    dataGridView4.Rows[0].Cells[j + 1].Value = subitem[1][j + 4];
                }
                comboBox6.Text = subitem[1][1];
                comboBox12.Text = subitem[1][8];
                comboBox11.Text = subitem[1][9];
                comboBox10.Text = subitem[1][10];
                comboBox9.Text = subitem[1][11];
                comboBox8.Text = subitem[1][12];
                for (int j = 0; j < 9; j++)
                {
                    dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                }
                comboBox4.Text = samcoffi[10][2];
            }
        }
        //子项3
        private void button3_Click(object sender, EventArgs e)
        {
            btn3click = true;
            btn1click = false;
            btn2click = false;
            btn4click = false;
            btn5click = false;
            if (subitem.Count >= 2)
            {
                for (int j = 0; j < subitem[2].Length - 9; j++)
                {
                    dataGridView4.Rows[0].Cells[j + 1].Value = subitem[2][j + 4];
                }
                comboBox6.Text = subitem[2][1];
                comboBox12.Text = subitem[2][8];
                comboBox11.Text = subitem[2][9];
                comboBox10.Text = subitem[2][10];
                comboBox9.Text = subitem[2][11];
                comboBox8.Text = subitem[2][12];
                for (int j = 0; j < 9; j++)
                {
                    dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                }
                comboBox4.Text = samcoffi[19][2];
            }
        }
        //子项4
        private void button4_Click(object sender, EventArgs e)
        {
            btn4click = true;
            btn1click = false;
            btn2click = false;
            btn3click = false;
            btn5click = false;
            if (subitem.Count >= 3)
            {
                for (int j = 0; j < subitem[3].Length - 9; j++)
                {
                    dataGridView4.Rows[0].Cells[j + 1].Value = subitem[3][j + 4];
                }
                comboBox6.Text = subitem[3][1];
                comboBox12.Text = subitem[3][8];
                comboBox11.Text = subitem[3][9];
                comboBox10.Text = subitem[3][10];
                comboBox9.Text = subitem[3][11];
                comboBox8.Text = subitem[3][12];
                for (int j = 0; j < 9; j++)
                {
                    dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                }
                comboBox4.Text = samcoffi[28][2];
            }
        }
        //子项5
        private void button5_Click(object sender, EventArgs e)
        {
            btn5click = true;
            btn1click = false;
            btn2click = false;
            btn3click = false;
            btn4click = false;
            if (subitem.Count >= 4)
            {
                for (int j = 0; j < subitem[4].Length - 9; j++)
                {
                    dataGridView4.Rows[0].Cells[j + 1].Value = subitem[4][j + 4];
                }
                comboBox6.Text = subitem[4][1];
                comboBox12.Text = subitem[4][8];
                comboBox11.Text = subitem[4][9];
                comboBox10.Text = subitem[4][10];
                comboBox9.Text = subitem[4][11];
                comboBox8.Text = subitem[4][12];
                for (int j = 0; j < 9; j++)
                {
                    dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                }
                comboBox4.Text = samcoffi[37][2];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            #region 实验相关数据填充
            int index_Select = dataGridView1.CurrentRow.Index;
            if (comuse.FileExist(path_tst + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() , ".TST"))
            {
                if (dataGridView1.Rows[index_Select].Cells[0].Value != null)
                {
                    MyEncrypt.SHA_Dencrypt(path_tst + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + ".TST",
                        path_tst + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "副本.TST", "179346");//解密    
                    re = comuse.ReadBasFile(path_tst + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "副本.TST");
                    comuse.DeleteOneFile(path_tst + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + "副本.TST");
                    textBox1.Text = re[0];//项目名称
                    textBox3.Text = re[1];//项目代码
                    textBox2.Text = re[2];//仪器类型
                    panel2.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    switch (re[3])//子项目个数
                    {
                        case "0":
                            panel2.Visible = false;
                            button1.Visible = false;
                            button2.Visible = false;
                            button3.Visible = false;
                            button4.Visible = false;
                            button5.Visible = false;
                            break;
                        case "1":
                            panel2.Visible = true;
                            panel2.Location = new Point(1080, 2);
                            button1.Visible = true;
                            break;
                        case "2":
                            panel2.Visible = true;
                            panel2.Location = new Point(1080, 2);
                            button1.Visible = true;
                            button2.Visible = true;
                            break;
                        case "3":
                            panel2.Visible = true;
                            panel2.Location = new Point(1080, 2);
                            button1.Visible = true;
                            button2.Visible = true;
                            button3.Visible = true;
                            break;
                        case "4":
                            panel2.Visible = true;
                            panel2.Location = new Point(1080, 2);
                            button1.Visible = true;
                            button2.Visible = true;
                            button3.Visible = true;
                            button4.Visible = true;
                            break;
                        case "5":
                            panel2.Visible = true;
                            panel2.Location = new Point(1080, 2);
                            button1.Visible = true;
                            button2.Visible = true;
                            button3.Visible = true;
                            button4.Visible = true;
                            button5.Visible = true;
                            break;
                    }
                    textBox4.Text = re[4];//预读时间
                    textBox5.Text = re[5];//测试时间
                    if (re[6] == "是")//是否二次稀释
                    {
                        checkBox2.Checked = true;
                    }
                    else
                    {
                        checkBox2.Checked = false;
                    }
                    if (re[7] == "判定")//是否判定未加样
                    {
                        checkBox3.Checked = true;
                    }
                    else
                    {
                        checkBox3.Checked = false;
                    }
                    textBox6.Text = re[8];//峰个数
                    textBox7.Text = re[9];//二次缓冲液量
                    textBox8.Text = re[10];//预读判断阈值
                    if (Convert.ToInt32(re[8]) > 5)
                    {
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = re[11];
                    }
                    textBox9.Text = re[12];//二次混合液量
                    textBox10.Text = re[13];//峰序号
                    textBox11.Text = re[14];//取峰算法
                    //组合输出
                    if (comuse.FindSignInString(re, 0, '\t', 8).Count == 0)
                    {
                        dataGridView9.Rows.Clear();
                        for (int i = 0; i < 8; i++)
                        {
                            i = dataGridView9.Rows.Add();
                            switch (i)
                            {
                                case 0:
                                    dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                                    break;
                                case 1:
                                    dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                                    break;
                                case 2:
                                    dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                                    break;
                                case 3:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                                    break;
                                case 4:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                                    break;
                                case 5:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                                    break;
                                case 6:
                                    dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                                    break;
                                case 7:
                                    dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        dataGridView9.Rows.Clear();
                        for (int i = 0; i < 8; i++)
                        {
                            i = dataGridView9.Rows.Add();
                            switch (i)
                            {
                                case 0:
                                    dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                                    break;
                                case 1:
                                    dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                                    break;
                                case 2:
                                    dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                                    break;
                                case 3:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                                    break;
                                case 4:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                                    break;
                                case 5:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                                    break;
                                case 6:
                                    dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                                    break;
                                case 7:
                                    dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                                    break;
                            }
                        }
                        comuse.AddDataToGroup(comuse.FindSignInString(re, 0, '\t', 8), dataGridView9);//组合输出
                    }
                    //样本类型
                    comuse.AddSamDatagridview(comuse.FindSignInString(re, 0, '\t', 3), dataGridView2);//样本类型，加样量，缓冲液量以及混合液量
                    //峰值
                    comuse.AddDatagridview(comuse.FindSignInString(re, 0, '\t', 4), dataGridView3);//峰值
                    subitem = comuse.SeparateSubItem(re, 0, '\t', 12);//子项基本参数
                    if (subitem.Count > 0)
                    {
                        for (int j = 0; j < subitem[0].Length - 9; j++)
                        {
                            dataGridView4.Rows[0].Cells[j + 1].Value = subitem[0][j + 4];
                        }
                        comboBox6.Text = subitem[0][1];
                        comboBox12.Text = subitem[0][8];
                        comboBox11.Text = subitem[0][9];
                        comboBox10.Text = subitem[0][10];
                        comboBox9.Text = subitem[0][11];
                        comboBox8.Text = subitem[0][12];
                    }
                    else
                    {
                        dataGridView4.Rows.Clear();
                        for (int i = 0; i < 5; i++)
                        {
                            i = dataGridView4.Rows.Add();
                            dataGridView4.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                        comboBox6.Text = " ";
                        comboBox12.Text = " ";
                        comboBox11.Text = " ";
                        comboBox10.Text = " ";
                        comboBox9.Text = " ";
                        comboBox8.Text = " ";
                    }
                    samcoffi = comuse.SeparateSubItem(re, 0, '\t', 2);//子项样本系数
                    if (samcoffi.Count > 0)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            dataGridView5.Rows[j].Cells[1].Value = samcoffi[j][1];
                            dataGridView5.Rows[j].Cells[2].Value = "1";
                            dataGridView5.Rows[j].Cells[3].Value = "1";
                        }
                        comboBox4.Text = samcoffi[0][2];
                    }
                    else
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            dataGridView5.Rows[i].Cells[1].Value = "1";
                            dataGridView5.Rows[i].Cells[2].Value = "1";
                            dataGridView5.Rows[i].Cells[3].Value = "1";
                        }
                    }
                }
                else
                {
                    dataGridView9.Rows.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        i = dataGridView9.Rows.Add();
                        switch (i)
                        {
                            case 0:
                                dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                                break;
                            case 1:
                                dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                                break;
                            case 2:
                                dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                                break;
                            case 3:
                                dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                                break;
                            case 4:
                                dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                                break;
                            case 5:
                                dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                                break;
                            case 6:
                                dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                                break;
                            case 7:
                                dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("未找到其实验相关信息！！");
                return;
            }
            #endregion
            if (comuse.FileExist(path_tst + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() , ".TCS"))
            {
                #region 曲线拟合相关数据填充
                line_or.Clear();
                dataGridView6.Rows.Clear();
                dataGridView7.Rows.Clear();
                string path = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString()
                    + "\\" + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + ".TCS";
                line_or = comuse.ReadLineImf(path);//读取数据
                for (int i = 0; i < line_or[0].potency.Count; i++)
                {
                    i = dataGridView6.Rows.Add();
                    i = dataGridView7.Rows.Add();
                    dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dataGridView6.Rows[i].Cells[1].Value = "选中";
                    dataGridView6.Rows[i].Cells[2].Value = line_or[0].potency[i];
                    dataGridView6.Rows[i].Cells[3].Value = line_or[0].reponse[i];
                    dataGridView7.Rows[i].Cells[1].Value = line_or[0].reponse[i];
                    dataGridView7.Rows[i].Cells[2].Value = line_or[0].potency[i];
                    dataGridView7.Rows[i].Cells[3].Value = line_or[0].calpotency[i];
                    dataGridView7.Rows[i].Cells[4].Value = line_or[0].Std[i];
                }
                textBox16.Text = line_or[0].method_reponse;
                textBox17.Text = line_or[0].deicalplace_reponse;
                textBox18.Text = line_or[0].method_potency;
                textBox19.Text = line_or[0].demicalplace_potency;
                textBox20.Text = line_or[0].methodLinefit;
                #endregion
            }
            else
            {
                MessageBox.Show("未找到其实验相关信息！！");
                return;
            }
        }

        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            switch(comboBox5.Text)
            {
                case "1":
                    if(line_or.Count>=1)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[0].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[0].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[0].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[0].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[0].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[0].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[0].Std[i];
                        }
                        textBox16.Text = line_or[0].method_reponse;
                        textBox17.Text = line_or[0].deicalplace_reponse;
                        textBox18.Text = line_or[0].method_potency;
                        textBox19.Text = line_or[0].demicalplace_potency;
                        textBox20.Text = line_or[0].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "2":
                    if(line_or.Count>=2)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[1].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[1].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[1].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[1].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[1].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[1].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[1].Std[i];
                        }
                        textBox16.Text = line_or[1].method_reponse;
                        textBox17.Text = line_or[1].deicalplace_reponse;
                        textBox18.Text = line_or[1].method_potency;
                        textBox19.Text = line_or[1].demicalplace_potency;
                        textBox20.Text = line_or[1].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "3":
                    if(line_or.Count>=3)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[2].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[2].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[2].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[2].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[2].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[2].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[2].Std[i];
                        }
                        textBox16.Text = line_or[2].method_reponse;
                        textBox17.Text = line_or[2].deicalplace_reponse;
                        textBox18.Text = line_or[2].method_potency;
                        textBox19.Text = line_or[2].demicalplace_potency;
                        textBox20.Text = line_or[2].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "4":
                    if(line_or.Count>=4)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[3].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[3].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[3].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[3].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[3].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[3].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[3].Std[i];
                        }
                        textBox16.Text = line_or[3].method_reponse;
                        textBox17.Text = line_or[3].deicalplace_reponse;
                        textBox18.Text = line_or[3].method_potency;
                        textBox19.Text = line_or[3].demicalplace_potency;
                        textBox20.Text = line_or[3].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "5":
                    if(line_or.Count>=5)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[4].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[4].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[4].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[4].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[4].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[4].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[4].Std[i];
                        }
                        textBox16.Text = line_or[4].method_reponse;
                        textBox17.Text = line_or[4].deicalplace_reponse;
                        textBox18.Text = line_or[4].method_potency;
                        textBox19.Text = line_or[4].demicalplace_potency;
                        textBox20.Text = line_or[4].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "6":
                    if(line_or.Count>=6)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[5].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[5].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[5].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[5].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[5].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[5].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[5].Std[i];
                        }
                        textBox16.Text = line_or[5].method_reponse;
                        textBox17.Text = line_or[5].deicalplace_reponse;
                        textBox18.Text = line_or[5].method_potency;
                        textBox19.Text = line_or[5].demicalplace_potency;
                        textBox20.Text = line_or[5].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "7":
                    if(line_or.Count>=7)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[6].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[6].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[6].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[6].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[6].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[6].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[6].Std[i];
                        }
                        textBox16.Text = line_or[6].method_reponse;
                        textBox17.Text = line_or[6].deicalplace_reponse;
                        textBox18.Text = line_or[6].method_potency;
                        textBox19.Text = line_or[6].demicalplace_potency;
                        textBox20.Text = line_or[6].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case"8":
                    if(line_or.Count>=8)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[7].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[7].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[7].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[7].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[7].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[7].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[7].Std[i];
                        }
                        textBox16.Text = line_or[7].method_reponse;
                        textBox17.Text = line_or[7].deicalplace_reponse;
                        textBox18.Text = line_or[7].method_potency;
                        textBox19.Text = line_or[7].demicalplace_potency;
                        textBox20.Text = line_or[7].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case"9":
                    if(line_or.Count>=9)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[8].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[8].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[8].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[8].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[8].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[8].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[8].Std[i];
                        }
                        textBox16.Text = line_or[8].method_reponse;
                        textBox17.Text = line_or[8].deicalplace_reponse;
                        textBox18.Text = line_or[8].method_potency;
                        textBox19.Text = line_or[8].demicalplace_potency;
                        textBox20.Text = line_or[8].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;
                case "10":
                    if(line_or.Count>=10)
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for (int i = 0; i < line_or[9].potency.Count; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = line_or[9].potency[i];
                            dataGridView6.Rows[i].Cells[3].Value = line_or[9].reponse[i];
                            dataGridView7.Rows[i].Cells[1].Value = line_or[9].reponse[i];
                            dataGridView7.Rows[i].Cells[2].Value = line_or[9].potency[i];
                            dataGridView7.Rows[i].Cells[3].Value = line_or[9].calpotency[i];
                            dataGridView7.Rows[i].Cells[4].Value = line_or[9].Std[i];
                        }
                        textBox16.Text = line_or[9].method_reponse;
                        textBox17.Text = line_or[9].deicalplace_reponse;
                        textBox18.Text = line_or[9].method_potency;
                        textBox19.Text = line_or[9].demicalplace_potency;
                        textBox20.Text = line_or[9].methodLinefit;
                    }
                    else
                    {
                        dataGridView6.Rows.Clear();
                        dataGridView7.Rows.Clear();
                        for(int i=0;i<20;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                        }
                    }
                    break;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            order_list.Clear();
            line_or.Clear();
            re.Clear();
            subitem.Clear();
            peakpos.Clear();
            groupoutput.Clear();
            iteminf.Clear();
            samcoffi.Clear();
            samtype.Clear();
            sam_de.Clear();
            order_list_line.Clear();
            SAMTC1TC2.Clear();
            int index_creatid = dataGridView1.CurrentRow.Index;
            if (comuse.FindFile(root_directory, root_directory + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString()+".HEX"))
            {
                comuse.DeleteOneFile(root_directory + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + ".HEX");
            }
            if(dataGridView1.Rows[index_creatid].Cells[0].Value!=null)
            {
                if (comuse.FileExist(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString(), ".TST")&&
                    comuse.FileExist(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString(), ".TCS"))
                 {
                     MyEncrypt.SHA_Dencrypt(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + ".TST",
                       path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "副本.TST", "179346");//解密    
                     re = comuse.ReadBasFile(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "副本.TST");
                     comuse.DeleteOneFile(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + "副本.TST");
                     subitem = comuse.SeparateSubItem(re, 0, '\t', 12);//子项基本参数
                     peakpos = comuse.SeparatePeakpos(re, 0, '\t', 4);//峰值区间
                     groupoutput = comuse.SeparatePeakpos(re, 0, '\t', 8);//组合输出
                     samcoffi = comuse.SeparateSubItem(re, 0, '\t', 2);//子项样本系数
                      string path = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString()
                 + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + ".TCS";
                    line_or = comuse.ReadLineImf(path);//读取数据
                    order_list_line = creatline.GetLineOrder(line_or);//浓度值、反应值以及拟合曲线个数
                    for(int i=0;i<dataGridView2.Rows.Count;i++)//样本类型及加样量
                    {
                        string[] sam = new string[4];
                       
                        sam[0] = dataGridView2.Rows[i].Cells[0].Value.ToString();
                        sam[1] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                        sam[2] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                        sam[3] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                        samtype.Add(sam);
                    }
                    for(int i=1;i<=samcoffi.Count/9;i++)//样本类型、系数、TC1、TC2
                    {
                        string[] TC1TC2 = new string[4];                       
                        switch(i)
                        {
                            case 1:
                                for(int j=0;j<9;j++)
                                {
                                    TC1TC2 = new string[4];
                                    TC1TC2[0] = samcoffi[j+0*9][0];
                                    TC1TC2[1] = samcoffi[j+0*9][1];
                                    TC1TC2[2] = TC1_1[j];
                                    TC1TC2[2] = TC2_1[j];
                                    SAMTC1TC2.Add(TC1TC2);
                                    sam_de.Add(samcoffi[j + 0 * 9][2]);
                                }
                               
                                break;
                            case 2:
                                 for(int j=0;j<9;j++)
                                {
                                    TC1TC2 = new string[4];
                                    TC1TC2[0] = samcoffi[j+1*9][0];
                                    TC1TC2[1] = samcoffi[j+1*9][1];
                                    TC1TC2[2] = TC1_2[j];
                                    TC1TC2[2] = TC2_2[j];
                                    SAMTC1TC2.Add(TC1TC2);
                                    sam_de.Add(samcoffi[j + 0 * 9][2]);
                                }
                                
                                break;
                            case 3:
                                 for(int j=0;j<9;j++)
                                {
                                    TC1TC2 = new string[4];
                                    TC1TC2[0] = samcoffi[j+2*9][0];
                                    TC1TC2[1] = samcoffi[j+2*9][1];
                                    TC1TC2[2] = TC1_3[j];
                                    TC1TC2[2] = TC2_3[j];
                                    SAMTC1TC2.Add(TC1TC2);
                                    sam_de.Add(samcoffi[j + 0 * 9][2]);
                                }
                                
                                break;
                            case 4:
                                for(int j=0;j<9;j++)
                                {
                                    TC1TC2 = new string[4];
                                    TC1TC2[0] = samcoffi[j+3*9][0];
                                    TC1TC2[1] = samcoffi[j+3*9][1];
                                    TC1TC2[2] = TC1_4[j];
                                    TC1TC2[2] = TC2_4[j];
                                    SAMTC1TC2.Add(TC1TC2);
                                    sam_de.Add(samcoffi[j + 0 * 9][2]);
                                }
                               
                                break;
                            case 5:
                                for(int j=0;j<9;j++)
                                {
                                    TC1TC2 = new string[4];
                                    TC1TC2[0] = samcoffi[j+4*9][0];
                                    TC1TC2[1] = samcoffi[j+4*9][1];
                                    TC1TC2[2] = TC1_5[j];
                                    TC1TC2[2] = TC2_5[j];
                                    SAMTC1TC2.Add(TC1TC2);
                                    sam_de.Add(samcoffi[j + 0 * 9][2]);
                                }
                              
                                break;
                        }
                    }
                    order_list.Add(or.ItemName(re[0], re[3]));//子项名称及数量1
                    order_list.Add(or.Pre_readtime(re[4], re[10]));//预读时间与阈值2
                    order_list.Add(or.Testtime(re[5]));//测试时间3
                    order_list.Add(or.Peak_num(re[8]));//峰个数4
                    order_list.Add(or.Secondary_mixandbuffer(re[9], re[12]));//二次缓冲液量以及二次混合液量5
                    order_list.Add(or.Peaknumber(re[13]));//峰序号6
                    order_list.Add(or.Referencepeak(re[11]));//基准峰7
                    order_list.Add(or.Project_batchnum(textBox12));//项目批号8
                    order_list.Add(or.UseArea(comboBox1));//使用区域9
                    order_list.Add(or.YearofProdution(textBox14));//生产年A
                    order_list.Add(or.MonthofProdution(comboBox2));//生产月B
                    order_list.Add(or.Batch(comboBox3));//生产批次C
                    order_list.Add(or.ValidMonths(textBox13));//有效月数D
                    order_list.Add(or.Dataman(textBox15));//条形码E
                    order_list.Add(or.CalPeak(re[14]));//取峰算法F                    
                    for (int i = 0; i < subitem.Count; i++)
                    {
                        order_list.Add(or.NameofSubitem(subitem[i], i));//子项目名称 11
                    }
                    for (int i = 0; i < subitem.Count;i++)
                    {
                        order_list.Add(or.LimitsofSubitem(subitem[i], i));//子项目参考范围 12
                    }
                    for (int i = 0; i < subitem.Count; i++)
                    {
                        order_list.Add(or.UnitofSubitem(subitem[i], i));//子项目单位 13
                    }
                    for (int i = 0; i < subitem.Count; i++)
                    {
                        order_list.Add(or.P1P2P3(subitem[i], i));//子项目计算参数P1P2P3 14
                    }
                    for (int i = 0; i < subitem.Count; i++)
                    {
                        order_list.Add(or.TCformulaandDoubleTC(subitem[i], i)); //子项目TC计算公式 15              
                    }
                    for (int i = 0; i < SAMTC1TC2.Count; i++)
                    {
                        order_list.Add(or.SamCoffiTC1TC2(SAMTC1TC2[i][0], SAMTC1TC2[i][1], SAMTC1TC2[i][2], SAMTC1TC2[i][3], i));//样本类型、系数/分界值、TC1、TC2 16
                    }
                    for (int i = 0; i < (order_list_line.Count - 2) / 2;i++)
                    {
                        order_list.Add(order_list_line[i]);//浓度值及浓度值小数位数 17
                    }
                    for (int i = (order_list_line.Count - 2) / 2; i < order_list_line.Count - 2; i++)
                    {
                        order_list.Add(order_list_line[i]);//反应值及反应值小数位数 18
                    }
                    order_list.Add(order_list_line[order_list_line.Count - 2]);//拟合曲线浓度值以及反应值变换方法以及拟合算法1B
                    for (int i = 0; i < peakpos.Count; i++)
                    {
                        order_list.Add(or.PeakPosAndNum(peakpos[i]));//峰值位置及取样点数1C
                    }
                    for (int i = 0; i < groupoutput.Count; i++)
                    {
                        order_list.Add(or.Group(groupoutput[i]));//组合输出1D
                    }    
                    for(int i=0;i<samtype.Count;i++)
                    {
                        order_list.Add(or.Sample(samtype[i]));//样本类型、加样量、缓冲液量以及混合液量 1E
                    }
                    order_list.Add(order_list_line[order_list_line.Count - 1]);//拟合曲线个数 1F
                    order_list.Add(or.DeviceType(re[2]));//仪器类型20
                    order_list.Add(or.Itemnumber(re[1]));//项目代码21
                    for(int i=0;i<sam_de.Count;i++)
                    {
                        order_list.Add(or.SamTimes_de(sam_de[i], (i + 1)));//子项目样本系数小数位22
                    }
                    order_list.Add(or.GroupNum(groupoutput.Count.ToString()));//组合输出个数23
                 }
                else if (!comuse.FileExist(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString(), ".TST"))
                 {
                     re.Clear();
                     MessageBox.Show("未对实验信息进行配置");
                     return;
                 }

                else if (!comuse.FileExist(path_tst + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString(), ".TCS"))
                {
                    line_or.Clear();
                    MessageBox.Show("未进行曲线拟合");
                    return;
                }
                comuse.WriteHEXFile(order_list,root_directory + "\\" + dataGridView1.Rows[index_creatid].Cells[0].Value.ToString() + ".HEX");
            }
            else
            {
                MessageBox.Show("请选择正确的实验项目生成ID文件！");
                return;
            }
        }

        private void dataGridView5_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right)//点击鼠标右键
            {
                selectColumnCount = dataGridView5.CurrentCell.ColumnIndex;
                if(selectColumnCount==2||selectColumnCount==3)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        i = dataGridView10.Rows.Add();
                        dataGridView10.Rows[i].Cells[0].Value = (i + 1).ToString();
                    }
                    if(selectColumnCount==2)
                    {
                        panel4.Location = new Point(314, 49);
                    }
                    else
                    {
                        panel4.Location = new Point(399, 49);
                    }
                    dataGridView10.Visible = true;
                    panel4.Visible = true;
                   
                }
            }
        }

        private void dataGridView10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string selectedvalue = dataGridView10.CurrentCell.Value.ToString();
            if(selectColumnCount==2)
            {
                if(btn1click)
                {
                    TC1_1[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if(btn2click)
                {
                    TC1_2[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn3click)
                {
                    TC1_3[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn4click)
                {
                    TC1_4[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn5click)
                {
                    TC1_5[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[2].Value = selectedvalue;
            }
            else
            {
                if (btn1click)
                {
                    TC2_1[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn2click)
                {
                    TC2_2[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn3click)
                {
                    TC2_3[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn4click)
                {
                    TC2_4[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                else if (btn5click)
                {
                    TC2_5[dataGridView5.CurrentCell.RowIndex] = selectedvalue;
                }
                dataGridView5.Rows[dataGridView5.CurrentCell.RowIndex].Cells[3].Value = selectedvalue;
            }
            dataGridView10.Visible = false;
            panel4.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            #region 打开串口
            //string portName = "COM" + signaltest.GetComNum().ToString();
            string portName = "COM2";
            string cmd = ((Button)sender).Text.Split(' ')[0];
            if (com != null)
                com.ClosePort();
            if (cmd == "Close")
                return;
            string s1 = portName;
            string s2 = "9600";
            string s3 = "One";
            string s4 = "8";
            string s5 = "None";
            com = new BaseCom(s1, s2, s3, s4, s5);
            //com.OnOpen += SignalState;
            com.OnShow += ShowMsg;
            com.OpenPort();
            #endregion
            for(int i=0;i<order_list.Count;i++)
            {
                com.comPort_SendData(order_list[i]);
            }
            for (int i = 0; i < order_list_line.Count; i++)
            {
                com.comPort_SendData(order_list_line[i]);
            }
        }
        public void ShowMsg(string msg)
        {
            if (this.InvokeRequired)
            {
                delegateShow d = new delegateShow(ShowMsg);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                receivemsg = msg;
                MessageBox.Show(msg);
            }
        }
    }
}
