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
    public partial class checkID : Form
    {
        private static checkID formInstance5;
        Common_use comuse = new Common_use();
        string root_directory = @"E:\VS13\POCT\POCT实验室管理软件\Item\";
        List<string> result = new List<string>();
        List<string[]> order = new List<string[]>();
        List<string[]> read_order = new List<string[]>();
        List<string[]> subitem = new List<string[]>();//子项目名称
        List<string[]> subiemlimt = new List<string[]>();//子项参考范围
        List<string[]> subitemunit = new List<string[]>();//子项单位
        List<string[]> subitemP1P2P3 = new List<string[]>();//子项计算参数
        List<string[]> subitemTCformal = new List<string[]>();//子项计算公式及双TC功能
        List<string[]> SamType = new List<string[]>();//样本类型、系数/分界值、TC1以及TC2
        List<string[]> Poenty = new List<string[]>();//曲线浓度
        List<string[]> Response = new List<string[]>();//曲线反应值
        List<string[]> Calpoen = new List<string[]>();//计算浓度
        List<string[]> STD = new List<string[]>();//偏差
        List<string[]> ChangeMethod = new List<string[]>();//浓度反应值变换方法及拟合算法
        List<string[]> Peakpos = new List<string[]>();//峰值区间及采样点数
        List<string[]> Groupout = new List<string[]>();//组合输出
        List<string[]> AddSam = new List<string[]>();//样本类型以及加样量等信息
        List<string[]> Linefitre = new List<string[]>();//拟合结果
        List<string[]> Device = new List<string[]>();//仪器类型
        List<string[]> Itemnumber = new List<string[]>();//项目代码
        List<string[]> samcoffi = new List<string[]>();//子项样本系数
        List<string[]> strr = new List<string[]>();
        List<string[]> strc = new List<string[]>();//计算浓度
        List<string[]> strstd = new List<string[]>();//偏差
        List<string[]> strp = new List<string[]>();
        BaseCom com;
        public delegate void delegateOnOff(bool onoff);
        public delegate void delegateShow(string msg);
        string receivemsg = string.Empty;
        public checkID()
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
        public static checkID GetIntance
        {
            get
            {
                if (formInstance5 != null)
                {
                    return formInstance5;
                }
                else
                {
                    formInstance5 = new checkID();
                    return formInstance5;
                }
            }
        }

        private void checkID_Load(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            result.Clear();
            order.Clear();
            read_order.Clear();
            subitem.Clear();//子项目名称
            subiemlimt.Clear();//子项参考范围
            subitemunit.Clear();//子项单位
            subitemP1P2P3.Clear();//子项计算参数
            subitemTCformal.Clear();//子项计算公式及双TC功能
            SamType.Clear();//样本类型、系数/分界值、TC1以及TC2
            Poenty.Clear();//曲线浓度
            Response.Clear();//曲线反应值
            Calpoen.Clear();//计算浓度
            STD.Clear();//偏差
            ChangeMethod.Clear();//浓度反应值变换方法及拟合算法
            Peakpos.Clear();//峰值区间及采样点数
            Groupout.Clear();//组合输出
            AddSam.Clear();//样本类型以及加样量等信息
            Linefitre.Clear();//拟合结果
            Device.Clear();//仪器类型
            Itemnumber.Clear();//项目代码
            samcoffi.Clear();//子项样本系数
            strr.Clear();
            strc.Clear();
            strstd.Clear();
            strp.Clear();
            if(comuse.FileExist(root_directory,"HEX"))
            {
                comuse.FindFileType(root_directory, ".HEX", dataGridView1);
                result = comuse.ReadHexFile(root_directory + dataGridView1.Rows[0].Cells[0].Value.ToString()+".HEX");
                for (int i = 0; i < result.Count;i++)
                {
                    string[] strArry = result[i].Split('\t');
                    order.Add(strArry);
                }
                read_order=comuse.AnyHex(order);
                textBox1.Text = read_order[0][0];
                panel2.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                switch(read_order[0][1])
                {
                    case"1":
                                 
                        panel2.Visible = true;
                        panel2.Location=new Point(715,1);
                        button1.Visible = true;   
                        button2.Visible = false;
                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;
                        break;
                    case "2":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;  
                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;                  
                        break;
                    case "3":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = false;
                        button5.Visible = false;
                        break;
                    case "4":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        button5.Visible = false;
                        break;
                    case "5":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        button5.Visible = true;
                        break;                  
                }
                if(read_order[1][0]!="0")
                {
                    textBox4.Text = read_order[1][0];
                    checkBox3.Checked = true;
                    textBox8.Text = read_order[1][1];
                }
                else
                {
                    textBox4.Text = read_order[1][0];
                    checkBox3.Checked = false;
                    textBox8.Text = read_order[1][1];
                }
                textBox5.Text = read_order[2][0];
                textBox6.Text = read_order[3][0];
                switch(read_order[3][0])
                {
                    case "2":
                        label36.Visible = false;
                        textBox23.Visible = false;
                        break;
                    case "3":
                        label36.Visible = false;
                        textBox23.Visible = false;
                        break;
                    case "4":
                        label36.Visible = false;
                        textBox23.Visible = false;
                        break;
                    case "5":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "6":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "7":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "8":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "9":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "10":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                }
                textBox7.Text = Convert.ToInt32(read_order[4][0]).ToString();
                textBox9.Text = Convert.ToInt32(read_order[4][1]).ToString(); 
                textBox10.Text = read_order[5][0];
                textBox12.Text = read_order[7][0];
                comboBox1.Text = read_order[8][0];
                textBox14.Text = read_order[9][0];
                comboBox2.Text = read_order[10][0];
                comboBox3.Text = read_order[11][0];
                textBox13.Text = read_order[12][0];
                textBox15.Text = read_order[13][0];
                textBox11.Text = read_order[14][0];
                subitem = comuse.CombineIteam(order, "11");//子项目名称
                subitem = comuse.AnyHex(subitem);
               
                subiemlimt = comuse.CombineIteam(order, "12");//子项参考范围
                subiemlimt = comuse.AnyHex(subiemlimt);

                subitemunit = comuse.CombineIteam(order, "13");//子项单位
                subitemunit = comuse.AnyHex(subitemunit);

                subitemP1P2P3 = comuse.CombineIteam(order, "14");//子项计算参数
                subitemP1P2P3 = comuse.AnyHex(subitemP1P2P3);

                subitemTCformal = comuse.CombineIteam(order, "15");//子项计算公式及双TC功能
                subitemTCformal = comuse.AnyHex(subitemTCformal);

                SamType = comuse.CombineIteam(order, "16");//样本类型、系数/分界值、TC1以及TC2
                SamType = comuse.AnyHex(SamType);

                Poenty = comuse.CombineIteam(order, "17");//曲线浓度
                Poenty = comuse.AnyHex(Poenty);

              
                for (int i = 0; i < Poenty.Count; i++)
                {
                    string[] str = Poenty[i][2].Split('\t');
                    strp.Add(str);
                }

                Response = comuse.CombineIteam(order, "18");//曲线反应值
                Response = comuse.AnyHex(Response);

            
                for (int i = 0; i < Response.Count; i++)
                {
                    string[] str = Response[i][2].Split('\t');
                    strr.Add(str);
                }

                //Calpoen = comuse.CombineIteam(order, "19");//计算浓度
                //Calpoen = comuse.AnyHex(Calpoen);

               
                //for (int i = 0; i < Calpoen.Count; i++)
                //{
                //    string[] str = Calpoen[i][2].Split('\t');
                //    strc.Add(str);
                //}


                //STD = comuse.CombineIteam(order, "1A");//偏差
                //STD = comuse.AnyHex(STD);

          
                //for (int i = 0; i < STD.Count; i++)
                //{
                //    string[] str = STD[i][2].Split('\t');
                //    strstd.Add(str);
                //}

                ChangeMethod = comuse.CombineIteam(order, "1B");//浓度反应值变换方法及拟合算法
                ChangeMethod = comuse.AnyHex(ChangeMethod);

                Peakpos = comuse.CombineIteam(order, "1C");//峰值区间及采样点数
                Peakpos = comuse.AnyHex(Peakpos);

                Groupout = comuse.CombineIteam(order, "1D");//组合输出
                Groupout = comuse.AnyHex(Groupout);

                AddSam = comuse.CombineIteam(order, "1E");//样本类型以及加样量等信息
                AddSam = comuse.AnyHex(AddSam);

                Linefitre = comuse.CombineIteam(order, "1F");//拟合结果
                Linefitre = comuse.AnyHex(Linefitre);

                Device = comuse.CombineIteam(order, "20");//仪器类型
                Device = comuse.AnyHex(Device);
                textBox2.Text = Device[0][0];

                Itemnumber = comuse.CombineIteam(order, "21");//项目代码
                Itemnumber = comuse.AnyHex(Itemnumber);

                samcoffi = comuse.CombineIteam(order, "22");//子项样本系数
                samcoffi = comuse.AnyHex(samcoffi);
                comboBox4.Text = samcoffi[0][1];

                textBox3.Text = Itemnumber[0][0];

                if (subitem.Count > 0)
                {
                    int index = 0;
                    index = dataGridView4.Rows.Add();
                    dataGridView4.Rows[index].Cells[0].Value = subitem[0][2];
                    dataGridView4.Rows[index].Cells[1].Value = subitem[0][0];
                    dataGridView4.Rows[index].Cells[2].Value = subitemunit[0][1];
                    dataGridView4.Rows[index].Cells[3].Value = subiemlimt[0][1];
                    dataGridView4.Rows[index].Cells[4].Value = subiemlimt[0][2];
                    comboBox6.Text = subitem[0][1];
                    comboBox12.Text = subitemP1P2P3[0][1];
                    comboBox11.Text = subitemP1P2P3[0][2];
                    comboBox10.Text = subitemP1P2P3[0][3];
                    comboBox9.Text = subitemTCformal[0][1];
                    if(subitemTCformal[0][2]=="00")
                    {
                        comboBox8.Text = "0-禁用";
                    }
                    else if(subitemTCformal[0][2]=="01")
                    {
                        comboBox8.Text = "1-TC1为分界点";
                    }
                    else if (subitemTCformal[0][2] == "02")
                    {
                        comboBox8.Text = "2-TC2为分界点";
                    }
                    for(int i=0;i<SamType.Count/Convert.ToInt32(read_order[0][1]);i++)
                    {
                        i = dataGridView5.Rows.Add();
                        dataGridView5.Rows[i].Cells[0].Value = SamType[i][1];
                        dataGridView5.Rows[i].Cells[1].Value = SamType[i][2];
                        dataGridView5.Rows[i].Cells[2].Value = SamType[i][3];
                        dataGridView5.Rows[i].Cells[3].Value = SamType[i][4];
                    }                  
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
                if (Groupout.Count == 0)
                {
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
                    for (int j = 0; j < Groupout.Count; j++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            i = dataGridView9.Rows.Add();
                            switch (i)
                            {
                                case 0:
                                    dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][1];
                                    break;
                                case 1:
                                    dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][2];
                                    break;
                                case 2:
                                    dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][3];
                                    break;
                                case 3:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][4];
                                    break;
                                case 4:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][5];
                                    break;
                                case 5:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][6];
                                    break;
                                case 6:
                                    dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][7];
                                    break;
                                case 7:
                                    dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][8];
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < AddSam.Count; i++)
                {
                    i = dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = AddSam[i][0];
                    dataGridView2.Rows[i].Cells[1].Value = AddSam[i][1];
                    dataGridView2.Rows[i].Cells[2].Value = AddSam[i][2];
                    dataGridView2.Rows[i].Cells[3].Value = AddSam[i][3];

                }
                for (int i = 0; i < Peakpos.Count; i++)
                {
                    i = dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = Peakpos[i][0];
                    dataGridView3.Rows[i].Cells[1].Value = Peakpos[i][1];
                    dataGridView3.Rows[i].Cells[2].Value = Peakpos[i][2];
                    dataGridView3.Rows[i].Cells[3].Value = Peakpos[i][3];

                }
                if (strp.Count == 0)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                        dataGridView6.Rows[i].Cells[1].Value = "选中";
                        dataGridView6.Rows[i].Cells[2].Value = "0";
                        dataGridView6.Rows[i].Cells[3].Value = "0";
                        i = dataGridView7.Rows.Add();
                        i = dataGridView8.Rows.Add();
                        dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                    }
                }
                else
                {
                    for (int i = 0; i < strp[0].Length; i++)
                    {
                        if (strp[0][i] != "")
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = strp[0][i];
                            dataGridView6.Rows[i].Cells[3].Value = strr[0][i];
                            dataGridView7.Rows[i].Cells[1].Value = strr[0][i];
                            dataGridView7.Rows[i].Cells[2].Value = strp[0][i];
                            //dataGridView7.Rows[i].Cells[3].Value = strc[0][i];
                            //dataGridView7.Rows[i].Cells[4].Value = strstd[0][i];

                        }
                    }
                    textBox19.Text = Poenty[0][1];
                    textBox17.Text = Response[0][1];
                }


                switch (ChangeMethod[0][3])
                {
                    case "0":
                        textBox20.Text = "多项式拟合";
                        break;
                    case "1":
                        textBox20.Text = "指数拟合";
                        break;
                    case "2":
                        textBox20.Text = "对数拟合";
                        break;
                    case "3":
                        textBox20.Text = "幂函数拟合";
                        break;
                    case "4":
                        textBox20.Text = "直线拟合";
                        break;
                }
                switch (ChangeMethod[0][2])
                {
                    case "0":
                        textBox16.Text = "无变换";
                        break;
                    case "1":
                        textBox16.Text = "取对数";
                        break;
                    case "2":
                        textBox16.Text = "自然对数";
                        break;
                    case "3":
                        textBox16.Text = "底为2对数";
                        break;
                }
                switch (ChangeMethod[0][1])
                {
                    case "0":
                        textBox18.Text = "无变换";
                        break;
                    case "1":
                        textBox18.Text = "取对数";
                        break;
                    case "2":
                        textBox18.Text = "自然对数";
                        break;
                    case "3":
                        textBox18.Text = "底为2对数";
                        break;
                }
                //label37.Text ="R=" + Linefitre[0][1];
            }
        }
        //子项1 0
        private void button1_Click(object sender, EventArgs e)
        {
           if(subitem.Count==0)
           {
               return;
           }
            else
           {
               dataGridView4.Rows.Clear();
               dataGridView5.Rows.Clear();
               int index = 0;
               index = dataGridView4.Rows.Add();
               dataGridView4.Rows[index].Cells[0].Value = subitem[0][2];
               dataGridView4.Rows[index].Cells[1].Value = subitem[0][0];
               dataGridView4.Rows[index].Cells[2].Value = subitemunit[0][1];
               dataGridView4.Rows[index].Cells[3].Value = subiemlimt[0][1];
               dataGridView4.Rows[index].Cells[4].Value = subiemlimt[0][2];
               comboBox6.Text = subitem[0][1];
               comboBox12.Text = subitemP1P2P3[0][1];
               comboBox11.Text = subitemP1P2P3[0][2];
               comboBox10.Text = subitemP1P2P3[0][3];
               comboBox9.Text = subitemTCformal[0][1];
               if (subitemTCformal[0][2] == "00")
               {
                   comboBox8.Text = "0-禁用";
               }
               else if (subitemTCformal[0][2] == "01")
               {
                   comboBox8.Text = "1-TC1为分界点";
               }
               else if (subitemTCformal[0][2] == "02")
               {
                   comboBox8.Text = "2-TC2为分界点";
               }
               for (int i = 0; i < SamType.Count / Convert.ToInt32(read_order[0][1]); i++)
               {
                   i = dataGridView5.Rows.Add();
                   dataGridView5.Rows[i].Cells[0].Value = SamType[i][1];
                   dataGridView5.Rows[i].Cells[1].Value = SamType[i][2];
                   dataGridView5.Rows[i].Cells[2].Value = SamType[i][3];
                   dataGridView5.Rows[i].Cells[3].Value = SamType[i][4];
               }
           }
        }
        //子项2 1
        private void button2_Click(object sender, EventArgs e)
        {
           if(subitem.Count<=1)
           {
               return;
           }
            else
           {
               dataGridView4.Rows.Clear();
               dataGridView5.Rows.Clear();
               int index = 0;
               index = dataGridView4.Rows.Add();
               dataGridView4.Rows[index].Cells[0].Value = subitem[1][2];
               dataGridView4.Rows[index].Cells[1].Value = subitem[1][0];
               dataGridView4.Rows[index].Cells[2].Value = subitemunit[1][1];
               dataGridView4.Rows[index].Cells[3].Value = subiemlimt[1][1];
               dataGridView4.Rows[index].Cells[4].Value = subiemlimt[1][2];
               comboBox6.Text = subitem[1][1];
               comboBox12.Text = subitemP1P2P3[1][1];
               comboBox11.Text = subitemP1P2P3[1][2];
               comboBox10.Text = subitemP1P2P3[1][3];
               comboBox9.Text = subitemTCformal[1][1];
               if (subitemTCformal[1][2] == "00")
               {
                   comboBox8.Text = "0-禁用";
               }
               else if (subitemTCformal[1][2] == "01")
               {
                   comboBox8.Text = "1-TC1为分界点";
               }
               else if (subitemTCformal[1][2] == "02")
               {
                   comboBox8.Text = "2-TC2为分界点";
               }
               List<string[]> samsub2 = new List<string[]>();
               for (int i = 0; i < SamType.Count / Convert.ToInt32(read_order[0][1]); i++)
               {
                   samsub2.Add(SamType[i + 1 * (SamType.Count / Convert.ToInt32(read_order[0][1]))]);
               }
               for (int i = 0; i < samsub2.Count; i++)
               {
                   i = dataGridView5.Rows.Add();
                   dataGridView5.Rows[i].Cells[0].Value = samsub2[i][1];
                   dataGridView5.Rows[i].Cells[1].Value = samsub2[i][2];
                   dataGridView5.Rows[i].Cells[2].Value = samsub2[i][3];
                   dataGridView5.Rows[i].Cells[3].Value = samsub2[i][4];
               }
           }
                
        }
        //子项3 2
        private void button3_Click(object sender, EventArgs e)
        {
           if(subitem.Count<=2)
           {
               return;
           }
            else
           {
               dataGridView4.Rows.Clear();
               dataGridView5.Rows.Clear();
               int index = 0;
               index = dataGridView4.Rows.Add();
               dataGridView4.Rows[index].Cells[0].Value = subitem[2][2];
               dataGridView4.Rows[index].Cells[1].Value = subitem[2][0];
               dataGridView4.Rows[index].Cells[2].Value = subitemunit[2][1];
               dataGridView4.Rows[index].Cells[3].Value = subiemlimt[2][1];
               dataGridView4.Rows[index].Cells[4].Value = subiemlimt[2][2];
               comboBox6.Text = subitem[2][1];
               comboBox12.Text = subitemP1P2P3[2][1];
               comboBox11.Text = subitemP1P2P3[2][2];
               comboBox10.Text = subitemP1P2P3[2][3];
               comboBox9.Text = subitemTCformal[2][1];
               if (subitemTCformal[2][2] == "00")
               {
                   comboBox8.Text = "0-禁用";
               }
               else if (subitemTCformal[2][2] == "01")
               {
                   comboBox8.Text = "1-TC1为分界点";
               }
               else if (subitemTCformal[2][2] == "02")
               {
                   comboBox8.Text = "2-TC2为分界点";
               }
               List<string[]> samsub2 = new List<string[]>();
               for (int i = 0; i < SamType.Count / Convert.ToInt32(read_order[0][1]); i++)
               {
                   samsub2.Add(SamType[i + 2 * (SamType.Count / Convert.ToInt32(read_order[0][1]))]);
               }
               for (int i = 0; i < samsub2.Count; i++)
               {
                   i = dataGridView5.Rows.Add();
                   dataGridView5.Rows[i].Cells[0].Value = samsub2[i][1];
                   dataGridView5.Rows[i].Cells[1].Value = samsub2[i][2];
                   dataGridView5.Rows[i].Cells[2].Value = samsub2[i][3];
                   dataGridView5.Rows[i].Cells[3].Value = samsub2[i][4];
               }
           }
        }
        //子项4 3
        private void button4_Click(object sender, EventArgs e)
        {
            if(subitem.Count<=3)
            {
                return;
            }
            else
            {
                dataGridView4.Rows.Clear();
                dataGridView5.Rows.Clear();
                int index = 0;
                index = dataGridView4.Rows.Add();
                dataGridView4.Rows[index].Cells[0].Value = subitem[3][2];
                dataGridView4.Rows[index].Cells[1].Value = subitem[3][0];
                dataGridView4.Rows[index].Cells[2].Value = subitemunit[3][1];
                dataGridView4.Rows[index].Cells[3].Value = subiemlimt[3][1];
                dataGridView4.Rows[index].Cells[4].Value = subiemlimt[3][2];
                //comboBox6.Text = subitem[3][1];
                comboBox12.Text = subitemP1P2P3[3][1];
                comboBox11.Text = subitemP1P2P3[3][2];
                comboBox10.Text = subitemP1P2P3[3][3];
                comboBox9.Text = subitemTCformal[3][1];
                if (subitemTCformal[3][2] == "00")
                {
                    comboBox8.Text = "0-禁用";
                }
                else if (subitemTCformal[3][2] == "01")
                {
                    comboBox8.Text = "1-TC1为分界点";
                }
                else if (subitemTCformal[3][2] == "02")
                {
                    comboBox8.Text = "2-TC2为分界点";
                }
                List<string[]> samsub2 = new List<string[]>();
                for (int i = 0; i < SamType.Count / Convert.ToInt32(read_order[0][1]); i++)
                {
                    samsub2.Add(SamType[i + 3 * (SamType.Count / Convert.ToInt32(read_order[0][1]))]);
                }
                for (int i = 0; i < samsub2.Count; i++)
                {
                    i = dataGridView5.Rows.Add();
                    dataGridView5.Rows[i].Cells[0].Value = samsub2[i][1];
                    dataGridView5.Rows[i].Cells[1].Value = samsub2[i][2];
                    dataGridView5.Rows[i].Cells[2].Value = samsub2[i][3];
                    dataGridView5.Rows[i].Cells[3].Value = samsub2[i][4];
                }
            }
        }
        //子项5 4
        private void button5_Click(object sender, EventArgs e)
        {
           if(subitem.Count<=4)
           {
               return;
           }
            else
           {
               dataGridView4.Rows.Clear();
               dataGridView5.Rows.Clear();
               int index = 0;
               index = dataGridView4.Rows.Add();
               dataGridView4.Rows[index].Cells[0].Value = subitem[4][2];
               dataGridView4.Rows[index].Cells[1].Value = subitem[4][0];
               dataGridView4.Rows[index].Cells[2].Value = subitemunit[4][1];
               dataGridView4.Rows[index].Cells[3].Value = subiemlimt[4][1];
               dataGridView4.Rows[index].Cells[4].Value = subiemlimt[4][2];
               comboBox6.Text = subitem[4][1];
               comboBox12.Text = subitemP1P2P3[4][1];
               comboBox11.Text = subitemP1P2P3[4][2];
               comboBox10.Text = subitemP1P2P3[4][3];
               comboBox9.Text = subitemTCformal[4][1];
               if (subitemTCformal[4][2] == "00")
               {
                   comboBox8.Text = "0-禁用";
               }
               else if (subitemTCformal[4][2] == "01")
               {
                   comboBox8.Text = "1-TC1为分界点";
               }
               else if (subitemTCformal[4][2] == "02")
               {
                   comboBox8.Text = "2-TC2为分界点";
               }
               List<string[]> samsub2 = new List<string[]>();
               for (int i = 0; i < SamType.Count / Convert.ToInt32(read_order[0][1]); i++)
               {
                   samsub2.Add(SamType[i + 4 * (SamType.Count / Convert.ToInt32(read_order[0][1]))]);
               }
               for (int i = 0; i < samsub2.Count; i++)
               {
                   i = dataGridView5.Rows.Add();
                   dataGridView5.Rows[i].Cells[0].Value = samsub2[i][1];
                   dataGridView5.Rows[i].Cells[1].Value = samsub2[i][2];
                   dataGridView5.Rows[i].Cells[2].Value = samsub2[i][3];
                   dataGridView5.Rows[i].Cells[3].Value = samsub2[i][4];
               }
           }
        }
        //改变曲线序号改变数值
        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            switch(comboBox5.Text)
            {
                case "1":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                      if(strp.Count==0)
                    {
                        for(int i=0;i<16;i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[0].Length; i++)
                        {
                            if (strp[0][i]!="")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[0][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[0][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[0][i];  
                                dataGridView7.Rows[i].Cells[2].Value = strp[0][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[0][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[0][i];
                               
                            }
                        }
                        textBox19.Text = Poenty[0][1];
                        textBox17.Text = Response[0][1];
                    }                   


                    switch (ChangeMethod[0][3])
                    {
                        case "0":
                            textBox20.Text = "多项式拟合";
                            break;
                        case "1":
                            textBox20.Text = "指数拟合";
                            break;
                        case "2":
                            textBox20.Text = "对数拟合";
                            break;
                        case "3":
                            textBox20.Text = "幂函数拟合";
                            break;
                        case "4":
                            textBox20.Text = "直线拟合";
                            break;
                    }
                    switch(ChangeMethod[0][2])
                    {
                        case "0":
                            textBox16.Text = "无变换";
                            break;
                        case "1":
                            textBox16.Text = "取对数";
                            break;
                        case "2":
                            textBox16.Text = "自然对数";
                            break;
                        case "3":
                            textBox16.Text = "底为2对数";
                            break;
                    }
                    switch(ChangeMethod[0][1])
                    {
                        case "0":
                            textBox18.Text = "无变换";
                            break;
                        case "1":
                            textBox18.Text = "取对数";
                            break;
                        case "2":
                            textBox18.Text = "自然对数";
                            break;
                        case "3":
                            textBox18.Text = "底为2对数";
                            break;
                    }
                    //label37.Text = "R=" + Linefitre[0][1];
                    break;
                case "2":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 1)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < strp[1].Length; i++)
                        {
                            if (strp[1][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[1][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[1][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[1][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[1][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[1][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[1][i];

                            }
                        }
                        textBox19.Text = Poenty[1][1];
                        textBox17.Text = Response[1][1];

                        switch (ChangeMethod[1][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[1][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[1][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                        //label37.Text = "R=" + Linefitre[1][1];
                    }
                   
                    break;
                case "3":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 2)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[2].Length; i++)
                        {
                            if (strp[2][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[2][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[2][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[2][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[2][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[2][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[2][i];

                            }
                        }
                        textBox19.Text = Poenty[2][1];
                        textBox17.Text = Response[2][1];

                        switch (ChangeMethod[2][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[2][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[2][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                        //label37.Text = "R=" + Linefitre[2][1];
                    }                   
                    break;
                case "4":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 3)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[3].Length; i++)
                        {
                            if (strp[3][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[3][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[3][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[3][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[3][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[3][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[3][i];

                            }
                        }
                        textBox19.Text = Poenty[3][1];
                        textBox17.Text = Response[3][1];

                        switch (ChangeMethod[3][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[3][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[3][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                        //label37.Text = "R=" + Linefitre[3][1];
                    }


                    
                    break;
                case "5":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 4)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[4].Length; i++)
                        {
                            if (strp[4][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[4][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[4][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[4][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[4][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[4][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[4][i];

                            }
                        }
                        textBox19.Text = Poenty[4][1];
                        textBox17.Text = Response[4][1];

                        switch (ChangeMethod[4][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[4][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[4][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                       // label37.Text = "R=" + Linefitre[4][1];
                    }


                   
                    break;
                case "6":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 5)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[5].Length; i++)
                        {
                            if (strp[5][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[5][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[5][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[5][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[5][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[5][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[5][i];

                            }
                        }
                        textBox19.Text = Poenty[5][1];
                        textBox17.Text = Response[5][1];
                        switch (ChangeMethod[5][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[5][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[5][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                        //label37.Text = "R=" + Linefitre[5][1];
                    }


                  
                    break;
                case "7":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 6)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[6].Length; i++)
                        {
                            if (strp[6][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[6][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[6][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[6][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[6][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[6][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[6][i];

                            }
                        }
                        textBox19.Text = Poenty[6][1];
                        textBox17.Text = Response[6][1];

                        switch (ChangeMethod[6][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[6][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[6][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                       // label37.Text = "R=" + Linefitre[6][1];
                    }
                    break;
                case "8":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 7)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[7].Length; i++)
                        {
                            if (strp[7][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[7][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[7][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[7][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[7][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[7][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[7][i];

                            }
                        }
                        textBox19.Text = Poenty[7][1];
                        textBox17.Text = Response[7][1];

                        switch (ChangeMethod[7][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[7][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[7][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                        //label37.Text = "R=" + Linefitre[7][1];
                    }                
                    break;
                case "9":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 8)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[8].Length; i++)
                        {
                            if (strp[8][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[8][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[8][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[8][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[8][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[8][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[8][i];

                            }
                        }
                        textBox19.Text = Poenty[8][1];
                        textBox17.Text = Response[8][1];

                        switch (ChangeMethod[8][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[8][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[8][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                       // label37.Text = "R=" + Linefitre[8][1];
                    }                  
                    break;
                case "10":
                    dataGridView6.Rows.Clear();
                    dataGridView7.Rows.Clear();
                    dataGridView8.Rows.Clear();
                    if (strp.Count <= 9)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            i = dataGridView6.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = "0";
                            dataGridView6.Rows[i].Cells[3].Value = "0";
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strp[9].Length; i++)
                        {
                            if (strp[9][i] != "")
                            {
                                i = dataGridView6.Rows.Add();
                                i = dataGridView7.Rows.Add();
                                i = dataGridView8.Rows.Add();
                                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                                dataGridView6.Rows[i].Cells[1].Value = "选中";
                                dataGridView6.Rows[i].Cells[2].Value = strp[9][i];
                                dataGridView6.Rows[i].Cells[3].Value = strr[9][i];
                                dataGridView7.Rows[i].Cells[1].Value = strr[9][i];
                                dataGridView7.Rows[i].Cells[2].Value = strp[9][i];
                                //dataGridView7.Rows[i].Cells[3].Value = strc[9][i];
                                //dataGridView7.Rows[i].Cells[4].Value = strstd[9][i];

                            }
                        }
                        textBox19.Text = Poenty[9][1];
                        textBox17.Text = Response[9][1];

                        switch (ChangeMethod[9][3])
                        {
                            case "0":
                                textBox20.Text = "多项式拟合";
                                break;
                            case "1":
                                textBox20.Text = "指数拟合";
                                break;
                            case "2":
                                textBox20.Text = "对数拟合";
                                break;
                            case "3":
                                textBox20.Text = "幂函数拟合";
                                break;
                            case "4":
                                textBox20.Text = "直线拟合";
                                break;
                        }
                        switch (ChangeMethod[9][2])
                        {
                            case "0":
                                textBox16.Text = "无变换";
                                break;
                            case "1":
                                textBox16.Text = "取对数";
                                break;
                            case "2":
                                textBox16.Text = "自然对数";
                                break;
                            case "3":
                                textBox16.Text = "底为2对数";
                                break;
                        }
                        switch (ChangeMethod[9][1])
                        {
                            case "0":
                                textBox18.Text = "无变换";
                                break;
                            case "1":
                                textBox18.Text = "取对数";
                                break;
                            case "2":
                                textBox18.Text = "自然对数";
                                break;
                            case "3":
                                textBox18.Text = "底为2对数";
                                break;
                        }
                        //label37.Text = "R=" + Linefitre[9][1];
                    }                
                    break;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            #region 变量清零
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            result.Clear();
            order.Clear();
            read_order.Clear();
            subitem.Clear();//子项目名称
            subiemlimt.Clear();//子项参考范围
            subitemunit.Clear();//子项单位
            subitemP1P2P3.Clear();//子项计算参数
            subitemTCformal.Clear();//子项计算公式及双TC功能
            SamType.Clear();//样本类型、系数/分界值、TC1以及TC2
            Poenty.Clear();//曲线浓度
            Response.Clear();//曲线反应值
            Calpoen.Clear();//计算浓度
            STD.Clear();//偏差
            ChangeMethod.Clear();//浓度反应值变换方法及拟合算法
            Peakpos.Clear();//峰值区间及采样点数
            Groupout.Clear();//组合输出
            AddSam.Clear();//样本类型以及加样量等信息
            Linefitre.Clear();//拟合结果
            Device.Clear();//仪器类型
            Itemnumber.Clear();//项目代码
            samcoffi.Clear();//子项样本系数
            strr.Clear();
            strc.Clear();
            strstd.Clear();
            strp.Clear();
            #endregion
            int index_Select = dataGridView1.CurrentRow.Index;
            if (comuse.FileExist(root_directory, ".HEX"))
            {
                result = comuse.ReadHexFile(root_directory + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + ".HEX");
                for (int i = 0; i < result.Count; i++)
                {
                    string[] strArry = result[i].Split('\t');
                    order.Add(strArry);
                }
                read_order = comuse.AnyHex(order);
                textBox1.Text = read_order[0][0];
                panel2.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                switch (read_order[0][1])
                {
                    case "1":

                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = false;
                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;
                        break;
                    case "2":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;
                        break;
                    case "3":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = false;
                        button5.Visible = false;
                        break;
                    case "4":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        button5.Visible = false;
                        break;
                    case "5":
                        panel2.Visible = true;
                        panel2.Location = new Point(715, 1);
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        button5.Visible = true;
                        break;
                }
                if (read_order[1][0] != "0")
                {
                    textBox4.Text = read_order[1][0];
                    checkBox3.Checked = true;
                    textBox8.Text = read_order[1][1];
                }
                else
                {
                    textBox4.Text = read_order[1][0];
                    checkBox3.Checked = false;
                    textBox8.Text = read_order[1][1];
                }
                textBox5.Text = read_order[2][0];
                textBox6.Text = read_order[3][0];
                switch (read_order[3][0])
                {
                    case "2":
                        label36.Visible = false;
                        textBox23.Visible = false;
                        break;
                    case "3":
                        label36.Visible = false;
                        textBox23.Visible = false;
                        break;
                    case "4":
                        label36.Visible = false;
                        textBox23.Visible = false;
                        break;
                    case "5":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "6":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "7":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "8":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "9":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                    case "10":
                        label36.Visible = true;
                        textBox23.Visible = true;
                        textBox23.Text = read_order[6][0];
                        break;
                }
                textBox7.Text = Convert.ToInt32(read_order[4][0]).ToString();
                textBox9.Text = Convert.ToInt32(read_order[4][1]).ToString();
                textBox10.Text = read_order[5][0];
                textBox12.Text = read_order[7][0];
                comboBox1.Text = read_order[8][0];
                textBox14.Text = read_order[9][0];
                comboBox2.Text = read_order[10][0];
                comboBox3.Text = read_order[11][0];
                textBox13.Text = read_order[12][0];
                textBox15.Text = read_order[13][0];
                textBox11.Text = read_order[14][0];
                subitem = comuse.CombineIteam(order, "11");//子项目名称
                subitem = comuse.AnyHex(subitem);

                subiemlimt = comuse.CombineIteam(order, "12");//子项参考范围
                subiemlimt = comuse.AnyHex(subiemlimt);

                subitemunit = comuse.CombineIteam(order, "13");//子项单位
                subitemunit = comuse.AnyHex(subitemunit);

                subitemP1P2P3 = comuse.CombineIteam(order, "14");//子项计算参数
                subitemP1P2P3 = comuse.AnyHex(subitemP1P2P3);

                subitemTCformal = comuse.CombineIteam(order, "15");//子项计算公式及双TC功能
                subitemTCformal = comuse.AnyHex(subitemTCformal);

                SamType = comuse.CombineIteam(order, "16");//样本类型、系数/分界值、TC1以及TC2
                SamType = comuse.AnyHex(SamType);

                Poenty = comuse.CombineIteam(order, "17");//曲线浓度
                Poenty = comuse.AnyHex(Poenty);


                for (int i = 0; i < Poenty.Count; i++)
                {
                    string[] str = Poenty[i][2].Split('\t');
                    strp.Add(str);
                }

                Response = comuse.CombineIteam(order, "18");//曲线反应值
                Response = comuse.AnyHex(Response);


                for (int i = 0; i < Response.Count; i++)
                {
                    string[] str = Response[i][2].Split('\t');
                    strr.Add(str);
                }

                //Calpoen = comuse.CombineIteam(order, "19");//计算浓度
                //Calpoen = comuse.AnyHex(Calpoen);


                //for (int i = 0; i < Calpoen.Count; i++)
                //{
                //    string[] str = Calpoen[i][2].Split('\t');
                //    strc.Add(str);
                //}


                //STD = comuse.CombineIteam(order, "1A");//偏差
                //STD = comuse.AnyHex(STD);


                //for (int i = 0; i < STD.Count; i++)
                //{
                //    string[] str = STD[i][2].Split('\t');
                //    strstd.Add(str);
                //}

                ChangeMethod = comuse.CombineIteam(order, "1B");//浓度反应值变换方法及拟合算法
                ChangeMethod = comuse.AnyHex(ChangeMethod);

                Peakpos = comuse.CombineIteam(order, "1C");//峰值区间及采样点数
                Peakpos = comuse.AnyHex(Peakpos);

                Groupout = comuse.CombineIteam(order, "1D");//组合输出
                Groupout = comuse.AnyHex(Groupout);

                AddSam = comuse.CombineIteam(order, "1E");//样本类型以及加样量等信息
                AddSam = comuse.AnyHex(AddSam);

                Linefitre = comuse.CombineIteam(order, "1F");//拟合结果
                Linefitre = comuse.AnyHex(Linefitre);

                Device = comuse.CombineIteam(order, "20");//仪器类型
                Device = comuse.AnyHex(Device);
                textBox2.Text = Device[0][0];

                Itemnumber = comuse.CombineIteam(order, "21");//项目代码
                Itemnumber = comuse.AnyHex(Itemnumber);

                samcoffi = comuse.CombineIteam(order, "22");//子项样本系数
                samcoffi = comuse.AnyHex(samcoffi);
                comboBox4.Text = samcoffi[0][1];

                textBox3.Text = Itemnumber[0][0];

                if (subitem.Count > 0)
                {
                    int index = 0;
                    index = dataGridView4.Rows.Add();
                    dataGridView4.Rows[index].Cells[0].Value = subitem[0][2];
                    dataGridView4.Rows[index].Cells[1].Value = subitem[0][0];
                    dataGridView4.Rows[index].Cells[2].Value = subitemunit[0][1];
                    dataGridView4.Rows[index].Cells[3].Value = subiemlimt[0][1];
                    dataGridView4.Rows[index].Cells[4].Value = subiemlimt[0][2];
                    comboBox6.Text = subitem[0][1];
                    comboBox12.Text = subitemP1P2P3[0][1];
                    comboBox11.Text = subitemP1P2P3[0][2];
                    comboBox10.Text = subitemP1P2P3[0][3];
                    comboBox9.Text = subitemTCformal[0][1];
                    if (subitemTCformal[0][2] == "00")
                    {
                        comboBox8.Text = "0-禁用";
                    }
                    else if (subitemTCformal[0][2] == "01")
                    {
                        comboBox8.Text = "1-TC1为分界点";
                    }
                    else if (subitemTCformal[0][2] == "02")
                    {
                        comboBox8.Text = "2-TC2为分界点";
                    }                   
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
                for (int i = 0; i < SamType.Count / Convert.ToInt32(read_order[0][1]); i++)
                {
                    i = dataGridView5.Rows.Add();
                    dataGridView5.Rows[i].Cells[0].Value = SamType[i][1];
                    dataGridView5.Rows[i].Cells[1].Value = SamType[i][2];
                    dataGridView5.Rows[i].Cells[2].Value = SamType[i][3];
                    dataGridView5.Rows[i].Cells[3].Value = SamType[i][4];
                }
                if (Groupout.Count == 0)
                {
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
                    for (int j = 0; j < Groupout.Count; j++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            i = dataGridView9.Rows.Add();
                            switch (i)
                            {
                                case 0:
                                    dataGridView9.Rows[i].Cells[0].Value = "输出名称";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][1];
                                    break;
                                case 1:
                                    dataGridView9.Rows[i].Cells[0].Value = "计量单位";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][2];
                                    break;
                                case 2:
                                    dataGridView9.Rows[i].Cells[0].Value = "小数位数";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][3];
                                    break;
                                case 3:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小值";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][4];
                                    break;
                                case 4:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围大值";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][5];
                                    break;
                                case 5:
                                    dataGridView9.Rows[i].Cells[0].Value = "范围小数";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][6];
                                    break;
                                case 6:
                                    dataGridView9.Rows[i].Cells[0].Value = "常数项V0";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][7];
                                    break;
                                case 7:
                                    dataGridView9.Rows[i].Cells[0].Value = "计算公式";
                                    dataGridView9.Rows[i].Cells[j + 1].Value = Groupout[j][8];
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < AddSam.Count; i++)
                {
                    i = dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = AddSam[i][0];
                    dataGridView2.Rows[i].Cells[1].Value = AddSam[i][1];
                    dataGridView2.Rows[i].Cells[2].Value = AddSam[i][2];
                    dataGridView2.Rows[i].Cells[3].Value = AddSam[i][3];

                }
                for (int i = 0; i < Peakpos.Count; i++)
                {
                    i = dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = Peakpos[i][0];
                    dataGridView3.Rows[i].Cells[1].Value = Peakpos[i][1];
                    dataGridView3.Rows[i].Cells[2].Value = Peakpos[i][2];
                    dataGridView3.Rows[i].Cells[3].Value = Peakpos[i][3];

                }
                if (strp.Count == 0)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                        dataGridView6.Rows[i].Cells[1].Value = "选中";
                        dataGridView6.Rows[i].Cells[2].Value = "0";
                        dataGridView6.Rows[i].Cells[3].Value = "0";
                        i = dataGridView7.Rows.Add();
                        i = dataGridView8.Rows.Add();
                        dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();

                    }
                }
                else
                {
                    for (int i = 0; i < strp[0].Length; i++)
                    {
                        if (strp[0][i] != "")
                        {
                            i = dataGridView6.Rows.Add();
                            i = dataGridView7.Rows.Add();
                            i = dataGridView8.Rows.Add();
                            dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView7.Rows[i].Cells[0].Value = (i + 1).ToString();
                            dataGridView6.Rows[i].Cells[1].Value = "选中";
                            dataGridView6.Rows[i].Cells[2].Value = strp[0][i];
                            dataGridView6.Rows[i].Cells[3].Value = strr[0][i];
                            dataGridView7.Rows[i].Cells[1].Value = strr[0][i];
                            dataGridView7.Rows[i].Cells[2].Value = strp[0][i];
                            //dataGridView7.Rows[i].Cells[3].Value = strc[0][i];
                            //dataGridView7.Rows[i].Cells[4].Value = strstd[0][i];

                        }
                    }
                    textBox19.Text = Poenty[0][1];
                    textBox17.Text = Response[0][1];
                }


                switch (ChangeMethod[0][3])
                {
                    case "0":
                        textBox20.Text = "多项式拟合";
                        break;
                    case "1":
                        textBox20.Text = "指数拟合";
                        break;
                    case "2":
                        textBox20.Text = "对数拟合";
                        break;
                    case "3":
                        textBox20.Text = "幂函数拟合";
                        break;
                    case "4":
                        textBox20.Text = "直线拟合";
                        break;
                }
                switch (ChangeMethod[0][2])
                {
                    case "0":
                        textBox16.Text = "无变换";
                        break;
                    case "1":
                        textBox16.Text = "取对数";
                        break;
                    case "2":
                        textBox16.Text = "自然对数";
                        break;
                    case "3":
                        textBox16.Text = "底为2对数";
                        break;
                }
                switch (ChangeMethod[0][1])
                {
                    case "0":
                        textBox18.Text = "无变换";
                        break;
                    case "1":
                        textBox18.Text = "取对数";
                        break;
                    case "2":
                        textBox18.Text = "自然对数";
                        break;
                    case "3":
                        textBox18.Text = "底为2对数";
                        break;
                }
                //label37.Text = "R=" + Linefitre[0][1];
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            #region 变量清零
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            result.Clear();
            order.Clear();
            read_order.Clear();
            subitem.Clear();//子项目名称
            subiemlimt.Clear();//子项参考范围
            subitemunit.Clear();//子项单位
            subitemP1P2P3.Clear();//子项计算参数
            subitemTCformal.Clear();//子项计算公式及双TC功能
            SamType.Clear();//样本类型、系数/分界值、TC1以及TC2
            Poenty.Clear();//曲线浓度
            Response.Clear();//曲线反应值
            Calpoen.Clear();//计算浓度
            STD.Clear();//偏差
            ChangeMethod.Clear();//浓度反应值变换方法及拟合算法
            Peakpos.Clear();//峰值区间及采样点数
            Groupout.Clear();//组合输出
            AddSam.Clear();//样本类型以及加样量等信息
            Linefitre.Clear();//拟合结果
            Device.Clear();//仪器类型
            Itemnumber.Clear();//项目代码
            samcoffi.Clear();//子项样本系数
            strr.Clear();
            strc.Clear();
            strstd.Clear();
            strp.Clear();
            #endregion
            List<byte[]> order_byte = new List<byte[]>();
            int index_Select = dataGridView1.CurrentRow.Index;
            if (comuse.FileExist(root_directory, ".HEX"))
            {
                result = comuse.ReadHexFile(root_directory + dataGridView1.Rows[index_Select].Cells[0].Value.ToString() + ".HEX");
                for (int i = 0; i < result.Count; i++)
                {
                    string[] strArry = result[i].Split('\t');
                    order.Add(strArry);
                }
            }
            for (int i = 0; i < order.Count; i++)
            {
                byte[] or_byte = new byte[order[i].Length];
                for(int j=0;j<order[i].Length;j++)
                {              
                    string str = "0x"+order[i][j].Trim('"');
                    or_byte[j] = Convert.ToByte(str,16);                 
                }
                order_byte.Add(or_byte);
            }
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
            for (int i = 0; i < order_byte.Count; i++)
            {
                com.comPort_SendData(order_byte[i]);
                while (receivemsg == null)
                {
                    Application.DoEvents();
                }
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

