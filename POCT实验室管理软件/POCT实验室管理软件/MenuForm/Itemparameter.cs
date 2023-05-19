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
    public partial class Itemparameter : Form
    {
        private static Itemparameter formInstance3;
        Common_use comuse = new Common_use();
        List<itemparam> item_list = new List<itemparam>();//获取到的项目参数的属性进行保存
        List<Sample> sam_list = new List<Sample>();//获取到的样本类型属性进行保存
        List<SubItemParam> subItemparam_list = new List<SubItemParam>();//获取到的子项相关参数进行保存
        List<SampleCoefficient> samcoeffi_list = new List<SampleCoefficient>();//获取到的样本系数属性进行保存
        List<PeakValue> peak_list = new List<PeakValue>();//获取到的峰值相关信息进行保存
        List<GroupOutPut> group_list = new List<GroupOutPut>();//获取到的组合输出的相关信息进行保存
        List<string> re = new List<string>();//读取.BAS文件得到的数据保存在re中
        List<string[]> subitem = new List<string[]>();//读取到的子项参数相关
        List<string[]> samcoffi = new List<string[]>();//读取到的子项参数相关
        string Path = "E:\\VS13\\POCT\\POCT实验室管理软件\\ItemBase";
        //增加样本类型回传的值
       
        
        public Itemparameter()
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
        public static Itemparameter GetIntance
        {
            get
            {
                if (formInstance3 != null)
                {
                    return formInstance3;
                }
                else
                {
                    formInstance3 = new Itemparameter();
                    return formInstance3;
                }
            }
        }
        //新建
        private void btn_new_Click(object sender, EventArgs e)
        {
            //comuse.GetControl(panel4, false);//测试用
            //comuse.FindFileType("F:\\琐事\\血小板抗体分析仪", ".docx", dataGridView1);//测试用
            comuse.GetControl(panel1, false);
            btn_new.Enabled = false;
            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Text = "取消";
            comuse.GetControl(panel4, true);
            comuse.GetControl(panel5, true);
            comuse.GetControl(panel6, true);
            comuse.GetControl(panel7, true);
            comuse.GetControl(groupBox1, true);
            comuse.GetControl(groupBox2, true);
            comuse.GetControl(groupBox3, true);
            comuse.GetControl(groupBox4, true);
            comuse.GetControl(groupBox5, true);
            comuse.GetControl(panel8, true);
            comuse.GetControl(tabPage6, true);
            comuse.GetControl(panel9, true);
            comuse.GetControl(panel10, true);
            comuse.GetControl(panel11, true);
            comuse.GetControl(panel12, true);
            comuse.GetControl(panel13, true);
            comuse.GetControl(panel14, true);
            comuse.GetControl(panel15, true);
            comuse.GetControl(panel16, true);
            comuse.GetControl(panel17, true);
            comuse.GetControl(panel20, true);
            comuse.GetControl(panel21, true);
            comuse.GetControl(panel22, true);
            textBox1.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView6.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView10.Rows.Clear();
            dataGridView12.Rows.Clear();
            dataGridView14.Rows.Clear();
            for (int i = 0; i < 5; i++)
            {
                i = dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView6.Rows.Add();
                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView8.Rows.Add();
                dataGridView8.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView10.Rows.Add();
                dataGridView10.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView12.Rows.Add();
                dataGridView12.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            for (int i = 0; i < 8; i++)
            {
                i = dataGridView14.Rows.Add();
                switch (i)
                {
                    case 0:
                        dataGridView14.Rows[i].Cells[0].Value = "输出名称";
                        break;
                    case 1:
                        dataGridView14.Rows[i].Cells[0].Value = "计量单位";
                        break;
                    case 2:
                        dataGridView14.Rows[i].Cells[0].Value = "小数位数";
                        break;
                    case 3:
                        dataGridView14.Rows[i].Cells[0].Value = "范围小值";
                        break;
                    case 4:
                        dataGridView14.Rows[i].Cells[0].Value = "范围大值";
                        break;
                    case 5:
                        dataGridView14.Rows[i].Cells[0].Value = "范围小数";
                        break;
                    case 6:
                        dataGridView14.Rows[i].Cells[0].Value = "常数项V0";
                        break;
                    case 7:
                        dataGridView14.Rows[i].Cells[0].Value = "计算公式";
                        break;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                i = dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                switch(i)
                {
                    case 0:
                        dataGridView3.Rows[i].Cells[1].Value = "93";
                        dataGridView3.Rows[i].Cells[2].Value = "208";
                        dataGridView3.Rows[i].Cells[3].Value = "60";
                        break;
                    case 1:
                         dataGridView3.Rows[i].Cells[1].Value = "240";
                        dataGridView3.Rows[i].Cells[2].Value = "364";
                        dataGridView3.Rows[i].Cells[3].Value = "60";
                        break;
                }
            }
            item_list.Clear();
            sam_list.Clear();
            subItemparam_list.Clear();
            samcoeffi_list.Clear();
            peak_list.Clear();
            group_list.Clear();
        }    

        private void Itemparameter_Load(object sender, EventArgs e)
        {
            re.Clear();
            samcoffi.Clear();
            subitem.Clear();
            #region UI界面显示设计
            tabPage2.Parent = null;
            tabPage3.Parent = null;
            tabPage4.Parent = null;
            tabPage5.Parent = null;
            tabPage6.Parent = null;
            if (comboBox2.Text == "1")
            {
                tabPage6.Parent = tabControl1;
            }
            comuse.GetControl(panel4, false);
            comuse.GetControl(panel5, false);
            comuse.GetControl(panel6, false);
            comuse.GetControl(panel7, false);
            comuse.GetControl(groupBox1, false);
            comuse.GetControl(groupBox2, false);
            comuse.GetControl(groupBox3, false);
            comuse.GetControl(groupBox4, false);
            comuse.GetControl(groupBox5, false);
            comuse.GetControl(panel8, false);
            comuse.GetControl(tabPage6, false);
            comuse.GetControl(panel9, false);
            comuse.GetControl(panel10, false);
            comuse.GetControl(panel11, false);
            comuse.GetControl(panel12, false);
            comuse.GetControl(panel13, false);
            comuse.GetControl(panel14, false);
            comuse.GetControl(panel15, false);
            comuse.GetControl(panel16, false);
            comuse.GetControl(panel17, false);
            comuse.GetControl(panel20, false);
            comuse.GetControl(panel21, false);
            comuse.GetControl(panel22, false);
            for (int i = 0; i < 5; i++)
            {
                i = dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView6.Rows.Add();
                dataGridView6.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView8.Rows.Add();
                dataGridView8.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView10.Rows.Add();
                dataGridView10.Rows[i].Cells[0].Value = (i + 1).ToString();
                i = dataGridView12.Rows.Add();
                dataGridView12.Rows[i].Cells[0].Value = (i + 1).ToString();
            }         
            for (int i = 0; i < 8; i++)
            {
                i = dataGridView14.Rows.Add();
                switch (i)
                {
                    case 0:
                        dataGridView14.Rows[i].Cells[0].Value = "输出名称";
                        break;
                    case 1:
                        dataGridView14.Rows[i].Cells[0].Value = "计量单位";
                        break;
                    case 2:
                        dataGridView14.Rows[i].Cells[0].Value = "小数位数";
                        break;
                    case 3:
                        dataGridView14.Rows[i].Cells[0].Value = "范围小值";
                        break;
                    case 4:
                        dataGridView14.Rows[i].Cells[0].Value = "范围大值";
                        break;
                    case 5:
                        dataGridView14.Rows[i].Cells[0].Value = "范围小数";
                        break;
                    case 6:
                        dataGridView14.Rows[i].Cells[0].Value = "常数项V0";
                        break;
                    case 7:
                        dataGridView14.Rows[i].Cells[0].Value = "计算公式";
                        break;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                i = dataGridView2.Rows.Add();
                i = dataGridView5.Rows.Add();
                i = dataGridView7.Rows.Add();
                i = dataGridView9.Rows.Add();
                i = dataGridView11.Rows.Add();
                i = dataGridView13.Rows.Add();
                switch (i)
                {
                    case 0:
                        dataGridView2.Rows[i].Cells[0].Value = "血清血浆";
                        dataGridView5.Rows[i].Cells[0].Value = "血清血浆";
                        dataGridView7.Rows[i].Cells[0].Value = "血清血浆";
                        dataGridView9.Rows[i].Cells[0].Value = "血清血浆";
                        dataGridView11.Rows[i].Cells[0].Value = "血清血浆";
                        dataGridView13.Rows[i].Cells[0].Value = "血清血浆";
                        break;
                    case 1:
                        dataGridView2.Rows[i].Cells[0].Value = "全血";
                        dataGridView5.Rows[i].Cells[0].Value = "全血";
                        dataGridView7.Rows[i].Cells[0].Value = "全血";
                        dataGridView9.Rows[i].Cells[0].Value = "全血";
                        dataGridView11.Rows[i].Cells[0].Value = "全血";
                        dataGridView13.Rows[i].Cells[0].Value = "全血";
                        break;
                    case 2:
                        dataGridView2.Rows[i].Cells[0].Value = "血清";
                        dataGridView5.Rows[i].Cells[0].Value = "血清";
                        dataGridView7.Rows[i].Cells[0].Value = "血清";
                        dataGridView9.Rows[i].Cells[0].Value = "血清";
                        dataGridView11.Rows[i].Cells[0].Value = "血清";
                        dataGridView13.Rows[i].Cells[0].Value = "血清";
                        break;
                    case 3:
                        dataGridView2.Rows[i].Cells[0].Value = "血浆";
                        dataGridView5.Rows[i].Cells[0].Value = "血浆";
                        dataGridView7.Rows[i].Cells[0].Value = "血浆";
                        dataGridView9.Rows[i].Cells[0].Value = "血浆";
                        dataGridView11.Rows[i].Cells[0].Value = "血浆";
                        dataGridView13.Rows[i].Cells[0].Value = "血浆";
                        break;
                    case 4:
                        dataGridView2.Rows[i].Cells[0].Value = "末梢血";
                        dataGridView5.Rows[i].Cells[0].Value = "末梢血";
                        dataGridView7.Rows[i].Cells[0].Value = "末梢血";
                        dataGridView9.Rows[i].Cells[0].Value = "末梢血";
                        dataGridView11.Rows[i].Cells[0].Value = "末梢血";
                        dataGridView13.Rows[i].Cells[0].Value = "末梢血";
                        break;
                    case 5:
                        dataGridView2.Rows[i].Cells[0].Value = "质控";
                        dataGridView5.Rows[i].Cells[0].Value = "质控";
                        dataGridView7.Rows[i].Cells[0].Value = "质控";
                        dataGridView9.Rows[i].Cells[0].Value = "质控";
                        dataGridView11.Rows[i].Cells[0].Value = "质控";
                        dataGridView13.Rows[i].Cells[0].Value = "质控";
                        break;
                    case 6:
                        dataGridView2.Rows[i].Cells[0].Value = "其他";
                        dataGridView5.Rows[i].Cells[0].Value = "其他";
                        dataGridView7.Rows[i].Cells[0].Value = "其他";
                        dataGridView9.Rows[i].Cells[0].Value = "其他";
                        dataGridView11.Rows[i].Cells[0].Value = "其他";
                        dataGridView13.Rows[i].Cells[0].Value = "其他";
                        break;
                    case 7:
                        dataGridView2.Rows[i].Cells[0].Value = "鼻/咽拭子";
                        dataGridView5.Rows[i].Cells[0].Value = "鼻/咽拭子";
                        dataGridView7.Rows[i].Cells[0].Value = "鼻/咽拭子";
                        dataGridView9.Rows[i].Cells[0].Value = "鼻/咽拭子";
                        dataGridView11.Rows[i].Cells[0].Value = "鼻/咽拭子";
                        dataGridView13.Rows[i].Cells[0].Value = "鼻/咽拭子";
                        break;
                    case 8:
                        dataGridView2.Rows[i].Cells[0].Value = "阴道分泌物";
                        dataGridView5.Rows[i].Cells[0].Value = "阴道分泌物";
                        dataGridView7.Rows[i].Cells[0].Value = "阴道分泌物";
                        dataGridView9.Rows[i].Cells[0].Value = "阴道分泌物";
                        dataGridView11.Rows[i].Cells[0].Value = "阴道分泌物";
                        dataGridView13.Rows[i].Cells[0].Value = "阴道分泌物";
                        break;
                }
                dataGridView5.Rows[i].Cells[1].Value = "1";
                dataGridView7.Rows[i].Cells[1].Value = "1";
                dataGridView9.Rows[i].Cells[1].Value = "1";
                dataGridView11.Rows[i].Cells[1].Value = "1";
                dataGridView13.Rows[i].Cells[1].Value = "1";
                if (i == 0)
                {
                    dataGridView2.Rows[i].Cells[1].Value = "10";
                }
                else
                {
                    dataGridView2.Rows[i].Cells[1].Value = "0";
                }
                dataGridView2.Rows[i].Cells[2].Value = "0";
                dataGridView2.Rows[i].Cells[3].Value = "0";
            }         
            #endregion
            #region 加载现有的.BAS文件          
            if (comuse.FindFileType(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase",".BAS",dataGridView1))
            {
                MyEncrypt.SHA_Dencrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\"+dataGridView1.Rows[0].Cells[0].Value.ToString()+".BAS",
                    @"E:\VS13\POCT\POCT实验室管理软件\ItemBase\"+dataGridView1.Rows[0].Cells[0].Value.ToString()+"-副本"+".BAS", "179346");             
                re = comuse.ReadBasFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "-副本" + ".BAS");
                comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "-副本" + ".BAS");
                textBox1.Text = re[0]; textBox2.Text = re[1]; comboBox1.Text = re[2]; comboBox2.Text = re[3];
                textBox3.Text = re[4]; textBox4.Text = re[5];
                if(re[6]=="是")
                {
                    checkBox2.Checked=true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if(re[7]=="判定")
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                comboBox3.Text = re[8];
                textBox5.Text = re[9];
                textBox6.Text = re[10];
                comboBox46.Text = re[11];
                textBox7.Text = re[12];
                comboBox4.Text = re[13];
                comboBox5.Text = re[14];
                if (comuse.FindSignInString(re, 0, '\t', 8).Count == 0)
                {
                    dataGridView14.Rows.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        i = dataGridView14.Rows.Add();
                        switch (i)
                        {
                            case 0:
                                dataGridView14.Rows[i].Cells[0].Value = "输出名称";
                                break;
                            case 1:
                                dataGridView14.Rows[i].Cells[0].Value = "计量单位";
                                break;
                            case 2:
                                dataGridView14.Rows[i].Cells[0].Value = "小数位数";
                                break;
                            case 3:
                                dataGridView14.Rows[i].Cells[0].Value = "范围小值";
                                break;
                            case 4:
                                dataGridView14.Rows[i].Cells[0].Value = "范围大值";
                                break;
                            case 5:
                                dataGridView14.Rows[i].Cells[0].Value = "范围小数";
                                break;
                            case 6:
                                dataGridView14.Rows[i].Cells[0].Value = "常数项V0";
                                break;
                            case 7:
                                dataGridView14.Rows[i].Cells[0].Value = "计算公式";
                                break;
                        }
                    }
                }
                else
                {
                    comuse.AddDataToGroup(comuse.FindSignInString(re, 0, '\t', 8), dataGridView14);//组合输出
                }               
                comuse.AddDatagridview(comuse.FindSignInString(re, 0, '\t', 4), dataGridView3);//峰值
                comuse.AddSamDatagridview(comuse.FindSignInString(re, 0, '\t', 3), dataGridView2);//样本类型，加样量，缓冲液量以及混合液量
                subitem=comuse.AddDataToSubItem(comuse.FindSignInString(re, 0, '\t', 12), dataGridView4, dataGridView6, dataGridView8, dataGridView10, dataGridView12);//子项参数
                samcoffi=comuse.AddSamCoffi(comuse.FindSignInString(re, 0, '\t', 1), dataGridView5, dataGridView7, dataGridView9, dataGridView11, dataGridView13);//子项的样本系数
                for(int i=0;i<subitem.Count;i++)
                {
                    switch (i)
                    {
                        case 0:
                            comboBox7.Text = subitem[i][0];
                            comboBox6.Text = subitem[i][1];
                            comboBox41.Text = subitem[i][2];
                            comboBox12.Text = subitem[i][8];
                            comboBox11.Text = subitem[i][9];
                            comboBox10.Text = subitem[i][10];
                            comboBox9.Text = subitem[i][11];
                            comboBox8.Text = subitem[i][12];
                            break;
                        case 1:
                            comboBox14.Text = subitem[i][0];
                            comboBox13.Text = subitem[i][1];
                            comboBox42.Text = subitem[i][2];
                            comboBox19.Text = subitem[i][8];
                            comboBox18.Text = subitem[i][9];
                            comboBox17.Text = subitem[i][10];
                            comboBox16.Text = subitem[i][11];
                            comboBox15.Text = subitem[i][12];
                            break;
                        case 2:
                            comboBox21.Text = subitem[i][0];
                            comboBox20.Text = subitem[i][1];
                            comboBox43.Text = subitem[i][2];
                            comboBox26.Text = subitem[i][8];
                            comboBox25.Text = subitem[i][9];
                            comboBox24.Text = subitem[i][10];
                            comboBox23.Text = subitem[i][11];
                            comboBox22.Text = subitem[i][12];
                            break;
                        case 3:
                            comboBox28.Text = subitem[i][0];
                            comboBox27.Text = subitem[i][1];
                            comboBox44.Text = subitem[i][2];
                            comboBox32.Text = subitem[i][8];
                            comboBox31.Text = subitem[i][9];
                            comboBox30.Text = subitem[i][10];
                            comboBox29.Text = subitem[i][11];
                            comboBox33.Text = subitem[i][12];
                            break;
                        case 4:
                            comboBox34.Text = subitem[i][0];
                            comboBox35.Text = subitem[i][1];
                            comboBox45.Text = subitem[i][2];
                            comboBox40.Text = subitem[i][8];
                            comboBox39.Text = subitem[i][9];
                            comboBox38.Text = subitem[i][10];
                            comboBox37.Text = subitem[i][11];
                            comboBox36.Text = subitem[i][12];
                            break;


                    }
                }
            }
            #endregion
            #region 加载信息结束后禁止更改表头加载的信息
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView6.Columns[0].ReadOnly = true;
            dataGridView8.Columns[0].ReadOnly = true;
            dataGridView10.Columns[0].ReadOnly = true;
            dataGridView12.Columns[0].ReadOnly = true;
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView5.Columns[0].ReadOnly = true;
            dataGridView7.Columns[0].ReadOnly = true;
            dataGridView9.Columns[0].ReadOnly = true;
            dataGridView11.Columns[0].ReadOnly = true;
            dataGridView13.Columns[0].ReadOnly = true;
            #endregion
        }       
        //取消
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_new.Enabled = true;
            btn_edit.Enabled = true;
            btn_save.Enabled = false;
            if(btn_cancel.Text=="取消")
            {
                btn_cancel.Text = "删除";
                comuse.GetControl(panel1, true);
                return;
            }
            if(btn_cancel.Text=="删除")
            {
                comuse.GetControl(panel1, true);
                comuse.GetControl(panel4, false);
                comuse.GetControl(panel5, false);
                comuse.GetControl(panel6, false);
                comuse.GetControl(panel7, false);
                comuse.GetControl(groupBox1, false);
                comuse.GetControl(groupBox2, false);
                comuse.GetControl(groupBox3, false);
                comuse.GetControl(groupBox4, false);
                comuse.GetControl(groupBox5, false);
                comuse.GetControl(panel8, false);
                comuse.GetControl(tabPage6, false);
                comuse.GetControl(panel9, false);
                comuse.GetControl(panel10, false);
                comuse.GetControl(panel11, false);
                comuse.GetControl(panel12, false);
                comuse.GetControl(panel13, false);
                comuse.GetControl(panel14, false);
                comuse.GetControl(panel15, false);
                comuse.GetControl(panel16, false);
                comuse.GetControl(panel17, false);
                comuse.GetControl(panel20, false);
                comuse.GetControl(panel21, false);
                comuse.GetControl(panel22, false);
                if (dataGridView1.Rows.Count>0)
                {
                    int index_delete = dataGridView1.CurrentRow.Index;
                    if(dataGridView1.Rows[index_delete].Cells[0].Selected)
                    {
                        DataGridViewRow row = dataGridView1.Rows[index_delete];
                        comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + ".BAS");
                        dataGridView1.Rows.Remove(row);
                    }
                }
                else
                {
                    MessageBox.Show("未找到需要删除的选项！！");
                }
            }
        }
        //编辑
        private void btn_edit_Click(object sender, EventArgs e)
        {
            btn_edit.Enabled = false;
            btn_new.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Text = "取消";
            comuse.GetControl(panel4, true);
            comuse.GetControl(panel5, true);
            comuse.GetControl(panel6, true);
            comuse.GetControl(panel7, true);
            comuse.GetControl(groupBox1, true);
            comuse.GetControl(groupBox2, true);
            comuse.GetControl(groupBox3, true);
            comuse.GetControl(groupBox4, true);
            comuse.GetControl(groupBox5, true);
            comuse.GetControl(panel8, true);
            comuse.GetControl(tabPage6, true);
            comuse.GetControl(panel9, true);
            comuse.GetControl(panel10, true);
            comuse.GetControl(panel11, true);
            comuse.GetControl(panel12, true);
            comuse.GetControl(panel13, true);
            comuse.GetControl(panel14, true);
            comuse.GetControl(panel15, true);
            comuse.GetControl(panel16, true);
            comuse.GetControl(panel17, true);
            comuse.GetControl(panel20, true);
            comuse.GetControl(panel21, true);
            comuse.GetControl(panel22, true);
            //测试用
            //MyEncrypt.SHA_Dencrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\11-胃蛋白酶原.BAS", @"E:\VS13\POCT\POCT实验室管理软件\ItemBase\12-胃蛋白酶原.BAS", "179346");
            //List<string> re = new List<string>();
            //re = comuse.ReadBasFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\12-胃蛋白酶原.BAS");
            //MessageBox.Show("数据读取");
          
        }
        //保存按钮
        private void btn_save_Click(object sender, EventArgs e)
        {
            itemparam ItemParam = new itemparam();
            ItemParam.Itemname = textBox1.Text.Trim();
            ItemParam.Itemnum = textBox2.Text.Trim();
            ItemParam.devicetype = comboBox1.Text.Trim();
            ItemParam.subitemnum = comboBox2.Text.Trim();
            ItemParam.pre_readtime = textBox3.Text.Trim();
            ItemParam.testtime = textBox4.Text.Trim();
            item_list.Clear();
            sam_list.Clear(); 
            subItemparam_list.Clear();
            samcoeffi_list.Clear(); 
            peak_list.Clear(); 
            group_list.Clear();
            #region 项目参数相关
            if (checkBox2.CheckState==CheckState.Checked)
            {
                ItemParam.customsecddilution = "是";
            }
            else
            {
                ItemParam.customsecddilution = "否";
            }
            if(checkBox3.CheckState==CheckState.Checked)
            {
                ItemParam.judgeaddsam = "判定";
            }
            else
            {
                ItemParam.judgeaddsam = "不判定";
            }
            ItemParam.peakvalue_num = comboBox3.Text.Trim();
            ItemParam.secondary_buffer = textBox5.Text.Trim();
            ItemParam.judgeaddsam_value = textBox6.Text.Trim();
            ItemParam.referencepeak = comboBox46.Text.Trim();
            ItemParam.secondary_mixture = textBox7.Text.Trim();
            ItemParam.peaknumber = comboBox4.Text.Trim();
            ItemParam.methodofgetpeak = comboBox5.Text.Trim();
            item_list.Add(ItemParam);
            #endregion
            #region 样本类型及加样量相关
            for (int i=0;i<dataGridView2.Rows.Count;i++)
            {
                Sample sam = new Sample();
                sam.samtype = dataGridView2.Rows[i].Cells[0].Value.ToString();
                sam.samvalue = dataGridView2.Rows[i].Cells[1].Value.ToString();
                sam.buffer_value = dataGridView2.Rows[i].Cells[2].Value.ToString();
                sam.mixture_value = dataGridView2.Rows[i].Cells[3].Value.ToString();
                sam_list.Add(sam);
            }
            #endregion
            #region 取峰区间范围
            for (int i=0;i<dataGridView3.Rows.Count;i++)
            {
                PeakValue peak = new PeakValue();
                peak.peaknum = dataGridView3.Rows[i].Cells[0].Value.ToString();
                peak.peak_start = dataGridView3.Rows[i].Cells[1].Value.ToString();
                peak.peak_end = dataGridView3.Rows[i].Cells[2].Value.ToString();
                peak.peaknumber = dataGridView3.Rows[i].Cells[3].Value.ToString();
                peak_list.Add(peak);
            }
            #endregion
            #region 子项1
            for (int i=0;i<dataGridView4.Rows.Count;i++)
            {
                SubItemParam subitem = new SubItemParam();
                if (dataGridView4.Rows[i].Cells[1].Value!=null)
                {
                    subitem.subitem_num = dataGridView4.Rows[i].Cells[0].Value.ToString();
                    subitem.subitem_name = dataGridView4.Rows[i].Cells[1].Value.ToString();
                    subitem.subitem_unit = dataGridView4.Rows[i].Cells[2].Value.ToString();
                    subitem.subitem_min = dataGridView4.Rows[i].Cells[3].Value.ToString();
                    subitem.subitem_max = dataGridView4.Rows[i].Cells[4].Value.ToString();
                    subitem.segmentcount = comboBox7.Text.Trim();
                    subitem.decimalplace = comboBox6.Text.Trim();
                    subitem.coefficient_decimalplace = comboBox41.Text.Trim();
                    subitem.subitem_P1 = comboBox12.Text.Trim();
                    subitem.subitem_P2 = comboBox11.Text.Trim();
                    subitem.subitem_P3 = comboBox10.Text.Trim();
                    subitem.subitem_TCformula = comboBox9.Text.Trim();
                    subitem.subitem_doubleTC = comboBox8.Text.Trim();
                    subItemparam_list.Add(subitem);
                }
                else
                {
                    break;
                }
            }
            #endregion
            #region 子项2
            for (int i = 0; i < dataGridView6.Rows.Count; i++)
            {
                SubItemParam subitem = new SubItemParam();
                if (dataGridView6.Rows[i].Cells[1].Value != null)
                {
                    subitem.subitem_num = dataGridView6.Rows[i].Cells[0].Value.ToString();
                    subitem.subitem_name = dataGridView6.Rows[i].Cells[1].Value.ToString();
                    subitem.subitem_unit = dataGridView6.Rows[i].Cells[2].Value.ToString();
                    subitem.subitem_min = dataGridView6.Rows[i].Cells[3].Value.ToString();
                    subitem.subitem_max = dataGridView6.Rows[i].Cells[4].Value.ToString();
                    subitem.segmentcount = comboBox14.Text.Trim();
                    subitem.decimalplace = comboBox13.Text.Trim();
                    subitem.coefficient_decimalplace = comboBox42.Text.Trim();
                    subitem.subitem_P1 = comboBox19.Text.Trim();
                    subitem.subitem_P2 = comboBox18.Text.Trim();
                    subitem.subitem_P3 = comboBox17.Text.Trim();
                    subitem.subitem_TCformula = comboBox16.Text.Trim();
                    subitem.subitem_doubleTC = comboBox15.Text.Trim();
                    subItemparam_list.Add(subitem);
                }
               else
                {
                    break;
                }
            }
            #endregion
            #region 子项3
            for (int i = 0; i < dataGridView8.Rows.Count; i++)
            {
                SubItemParam subitem = new SubItemParam();
                if (dataGridView8.Rows[i].Cells[1].Value != null)
                {
                    subitem.subitem_num = dataGridView8.Rows[i].Cells[0].Value.ToString();
                    subitem.subitem_name = dataGridView8.Rows[i].Cells[1].Value.ToString();
                    subitem.subitem_unit = dataGridView8.Rows[i].Cells[2].Value.ToString();
                    subitem.subitem_min = dataGridView8.Rows[i].Cells[3].Value.ToString();
                    subitem.subitem_max = dataGridView8.Rows[i].Cells[4].Value.ToString();
                    subitem.segmentcount = comboBox21.Text.Trim();
                    subitem.decimalplace = comboBox20.Text.Trim();
                    subitem.coefficient_decimalplace = comboBox43.Text.Trim();
                    subitem.subitem_P1 = comboBox26.Text.Trim();
                    subitem.subitem_P2 = comboBox25.Text.Trim();
                    subitem.subitem_P3 = comboBox24.Text.Trim();
                    subitem.subitem_TCformula = comboBox23.Text.Trim();
                    subitem.subitem_doubleTC = comboBox22.Text.Trim();
                    subItemparam_list.Add(subitem);
                }              
                else
                {
                    break;
                }
            }
            #endregion
            #region 子项4
            for (int i = 0; i < dataGridView10.Rows.Count; i++)
            {
                SubItemParam subitem = new SubItemParam();
                if (dataGridView10.Rows[i].Cells[1].Value != null)
                {
                    subitem.subitem_num = dataGridView10.Rows[i].Cells[0].Value.ToString();
                    subitem.subitem_name = dataGridView10.Rows[i].Cells[1].Value.ToString();
                    subitem.subitem_unit = dataGridView10.Rows[i].Cells[2].Value.ToString();
                    subitem.subitem_min = dataGridView10.Rows[i].Cells[3].Value.ToString();
                    subitem.subitem_max = dataGridView10.Rows[i].Cells[4].Value.ToString();
                    subitem.segmentcount = comboBox28.Text.Trim();
                    subitem.decimalplace = comboBox27.Text.Trim();
                    subitem.coefficient_decimalplace = comboBox44.Text.Trim();
                    subitem.subitem_P1 = comboBox32.Text.Trim();
                    subitem.subitem_P2 = comboBox31.Text.Trim();
                    subitem.subitem_P3 = comboBox30.Text.Trim();
                    subitem.subitem_TCformula = comboBox29.Text.Trim();
                    subitem.subitem_doubleTC = comboBox33.Text.Trim();
                    subItemparam_list.Add(subitem);
                }   
                else
                {
                    break;
                }

            }
            #endregion
            #region 子项5
            for (int i = 0; i < dataGridView12.Rows.Count; i++)
            {
               
                SubItemParam subitem = new SubItemParam();
                if (dataGridView12.Rows[i].Cells[1].Value != null)
                {
                    subitem.subitem_num = dataGridView12.Rows[i].Cells[0].Value.ToString();
                    subitem.subitem_name = dataGridView12.Rows[i].Cells[1].Value.ToString();
                    subitem.subitem_unit = dataGridView12.Rows[i].Cells[2].Value.ToString();
                    subitem.subitem_min = dataGridView12.Rows[i].Cells[3].Value.ToString();
                    subitem.subitem_max = dataGridView12.Rows[i].Cells[4].Value.ToString();
                    subitem.segmentcount = comboBox34.Text.Trim();
                    subitem.decimalplace = comboBox35.Text.Trim();
                    subitem.coefficient_decimalplace = comboBox45.Text.Trim();
                    subitem.subitem_P1 = comboBox40.Text.Trim();
                    subitem.subitem_P2 = comboBox39.Text.Trim();
                    subitem.subitem_P3 = comboBox38.Text.Trim();
                    subitem.subitem_TCformula = comboBox37.Text.Trim();
                    subitem.subitem_doubleTC = comboBox36.Text.Trim();
                    subItemparam_list.Add(subitem);
                }   
                else
                {
                    break;
                }
            
            }
            #endregion
            if(comboBox2.Text.Trim()!=null)
            {
                switch(comboBox2.Text.Trim())
                {
                    case "5":
                         #region 子项1样本系数
            for (int i=0;i<dataGridView5.Rows.Count;i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView5.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView5.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox41.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项2样本系数
            for (int i = 0; i < dataGridView7.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView7.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView7.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox42.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项3样本系数
            for (int i = 0; i < dataGridView9.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView9.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView9.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox43.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项4样本系数
            for (int i = 0; i < dataGridView11.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView11.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView11.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox44.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项5样本系数
            for (int i = 0; i < dataGridView13.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView13.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView13.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox45.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         break;
                    case"4":
                         #region 子项1样本系数
            for (int i=0;i<dataGridView5.Rows.Count;i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView5.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView5.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox41.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项2样本系数
            for (int i = 0; i < dataGridView7.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView7.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView7.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox42.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项3样本系数
            for (int i = 0; i < dataGridView9.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView9.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView9.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox43.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项4样本系数
            for (int i = 0; i < dataGridView11.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView11.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView11.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox44.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         break;
                    case"3":
                         #region 子项1样本系数
            for (int i=0;i<dataGridView5.Rows.Count;i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView5.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView5.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox41.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项2样本系数
            for (int i = 0; i < dataGridView7.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView7.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView7.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox42.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项3样本系数
            for (int i = 0; i < dataGridView9.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView9.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView9.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox43.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         break;
                    case"2":
                         #region 子项1样本系数
            for (int i=0;i<dataGridView5.Rows.Count;i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView5.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView5.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox41.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         #region 子项2样本系数
            for (int i = 0; i < dataGridView7.Rows.Count; i++)
            {
                SampleCoefficient samcoeffi = new SampleCoefficient();
                samcoeffi.sampletype = dataGridView7.Rows[i].Cells[0].Value.ToString();
                samcoeffi.samcoeffi = dataGridView7.Rows[i].Cells[1].Value.ToString();
                samcoeffi.samcoeffi_decimalplace = comboBox42.Text.Trim();
                samcoeffi_list.Add(samcoeffi);
            }
            #endregion
                         break;
                    case"1":
                         #region 子项1样本系数
                         for (int i = 0; i < dataGridView5.Rows.Count; i++)
                         {
                             SampleCoefficient samcoeffi = new SampleCoefficient();
                             samcoeffi.sampletype = dataGridView5.Rows[i].Cells[0].Value.ToString();
                             samcoeffi.samcoeffi = dataGridView5.Rows[i].Cells[1].Value.ToString();
                             samcoeffi.samcoeffi_decimalplace = comboBox41.Text.Trim();
                             samcoeffi_list.Add(samcoeffi);
                         }
                         #endregion
                         break;
                }
            }
            else
            {
                MessageBox.Show("请输入子项目个数!!!");
            }
            #region 组合输出
            for (int i=0;i<dataGridView14.Columns.Count;i++)
            {
                GroupOutPut group = new GroupOutPut();
                group.group_num = (i + 1).ToString();
                if(i != (dataGridView14.Columns.Count-1))
                {
                    if(dataGridView14.Rows[1].Cells[i + 1].Value != null)
                    {
                        switch (i)
                        {
                            case 0:
                                group.group_name = dataGridView14.Rows[0].Cells[1].Value.ToString();
                                group.group_unit = dataGridView14.Rows[1].Cells[1].Value.ToString();
                                group.group_decimalplace = dataGridView14.Rows[2].Cells[1].Value.ToString();
                                group.group_min = dataGridView14.Rows[3].Cells[1].Value.ToString();
                                group.group_max = dataGridView14.Rows[4].Cells[1].Value.ToString();
                                group.rangedecimals = dataGridView14.Rows[5].Cells[1].Value.ToString();
                                group.v0 = dataGridView14.Rows[6].Cells[1].Value.ToString();
                                group.calculationformula = dataGridView14.Rows[7].Cells[1].Value.ToString();
                                group_list.Add(group);
                                break;
                            case 1:
                                group.group_name = dataGridView14.Rows[0].Cells[2].Value.ToString();
                                group.group_unit = dataGridView14.Rows[1].Cells[2].Value.ToString();
                                group.group_decimalplace = dataGridView14.Rows[2].Cells[2].Value.ToString();
                                group.group_min = dataGridView14.Rows[3].Cells[2].Value.ToString();
                                group.group_max = dataGridView14.Rows[4].Cells[2].Value.ToString();
                                group.rangedecimals = dataGridView14.Rows[5].Cells[2].Value.ToString();
                                group.v0 = dataGridView14.Rows[6].Cells[2].Value.ToString();
                                group.calculationformula = dataGridView14.Rows[7].Cells[2].Value.ToString();
                                group_list.Add(group);
                                break;
                            case 2:
                                group.group_name = dataGridView14.Rows[0].Cells[3].Value.ToString();
                                group.group_unit = dataGridView14.Rows[1].Cells[3].Value.ToString();
                                group.group_decimalplace = dataGridView14.Rows[2].Cells[3].Value.ToString();
                                group.group_min = dataGridView14.Rows[3].Cells[3].Value.ToString();
                                group.group_max = dataGridView14.Rows[4].Cells[3].Value.ToString();
                                group.rangedecimals = dataGridView14.Rows[5].Cells[3].Value.ToString();
                                group.v0 = dataGridView14.Rows[6].Cells[3].Value.ToString();
                                group.calculationformula = dataGridView14.Rows[7].Cells[3].Value.ToString();
                                group_list.Add(group);
                                break;
                        }
                    }
                }
            }
            #endregion   
            string filefullname_or = Path + "\\" + item_list[0].Itemname + ".BAS";
            comuse.FileName_jiami = Path + "\\" + item_list[0].Itemnum + "-" +item_list[0].Itemname + ".BAS";
            comuse.WriteBasFile(filefullname_or, item_list, sam_list, subItemparam_list, samcoeffi_list, peak_list, group_list);
            MyEncrypt.SHA_Encrypt(filefullname_or, comuse.FileName_jiami, "179346");
            comuse.DeleteOneFile(filefullname_or);
            if(dataGridView1.Rows.Count==0)
            {
                int index = 0;
                index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item_list[0].Itemnum + "-" + item_list[0].Itemname;
            }
            else
            {
                int index = dataGridView1.Rows.Count-1;
                index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item_list[0].Itemnum + "-" + item_list[0].Itemname;
            }
            comuse.GetControl(panel4, false);
            comuse.GetControl(panel5, false);
            comuse.GetControl(panel6, false);
            comuse.GetControl(panel7, false);
            comuse.GetControl(groupBox1, false);
            comuse.GetControl(groupBox2, false);
            comuse.GetControl(groupBox3, false);
            comuse.GetControl(groupBox4, false);
            comuse.GetControl(groupBox5, false);
            comuse.GetControl(panel8, false);
            comuse.GetControl(tabPage6, false);
            comuse.GetControl(panel9, false);
            comuse.GetControl(panel10, false);
            comuse.GetControl(panel11, false);
            comuse.GetControl(panel12, false);
            comuse.GetControl(panel13, false);
            comuse.GetControl(panel14, false);
            comuse.GetControl(panel15, false);
            comuse.GetControl(panel16, false);
            comuse.GetControl(panel17, false);
            comuse.GetControl(panel20, false);
            comuse.GetControl(panel21, false);
            comuse.GetControl(panel22, false);
            comuse.GetControl(panel1, true);
        }       
        //根据峰个数更新基准峰、峰序号以及TC计算参数的可选择项
        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();
            comboBox19.Items.Clear();
            comboBox18.Items.Clear();
            comboBox17.Items.Clear();
            comboBox26.Items.Clear();
            comboBox25.Items.Clear();
            comboBox24.Items.Clear();
            comboBox32.Items.Clear();
            comboBox31.Items.Clear();
            comboBox30.Items.Clear();
            comboBox40.Items.Clear();
            comboBox39.Items.Clear();
            comboBox38.Items.Clear();
            comboBox46.Items.Clear();
            switch (comboBox3.Text)
            {
                case "2":
                    label69.Visible = false;
                    comboBox46.Visible = false;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2" });

                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2" });
                    for (int i = 0; i < 2; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "3":
                    label69.Visible = false;
                    comboBox46.Visible = false;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3" });

                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    for (int i = 0; i < 3; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "4":
                    label69.Visible = false;
                    comboBox46.Visible = false;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });

                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    for (int i = 0; i < 4; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "5":
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox46.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });


                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    for (int i = 0; i < 5; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "6":
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox46.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });

                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    for (int i = 0; i < 6; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "7":
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox46.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });

                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    for (int i = 0; i < 7; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "8":
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox46.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });

                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    for (int i = 0; i < 8; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "9":
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox46.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });


                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    for (int i = 0; i < 9; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
                case "10":
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    dataGridView3.Rows.Clear();
                    this.comboBox4.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox10.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox11.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox12.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox46.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });


                    this.comboBox19.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox18.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox17.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });

                    this.comboBox26.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox25.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox24.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });

                    this.comboBox32.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox31.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox30.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });

                    this.comboBox40.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox39.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    this.comboBox38.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    for (int i = 0; i < 10; i++)
                    {
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    break;
            }
        }
        //根据子项个数新建对应的tabpages页
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox2.Text)
            {
                case "1":
                    tabPage2.Parent = null;
                    tabPage3.Parent = null;
                    tabPage4.Parent = null;
                    tabPage5.Parent = null;
                    tabPage6.Parent = null;
                    tabPage6.Parent = tabControl1;
                    break;
                case "2":
                    tabPage6.Parent = null;
                    tabPage3.Parent = null;
                    tabPage4.Parent = null;
                    tabPage5.Parent = null;
                    tabPage2.Parent = tabControl1;
                    tabPage6.Parent = tabControl1;
                    break;
                case "3":
                    tabPage6.Parent = null;
                    tabPage4.Parent = null;
                    tabPage5.Parent = null;
                    tabPage2.Parent = tabControl1;
                    tabPage3.Parent = tabControl1;
                    tabPage6.Parent = tabControl1;
                    break;
                case "4":
                    tabPage6.Parent = null;
                    tabPage5.Parent = null;
                    tabPage2.Parent = tabControl1;
                    tabPage3.Parent = tabControl1;
                    tabPage4.Parent = tabControl1;
                    tabPage6.Parent = tabControl1;
                    break;
                case "5":
                    tabPage6.Parent = null;
                    tabPage2.Parent = tabControl1;
                    tabPage3.Parent = tabControl1;
                    tabPage4.Parent = tabControl1;
                    tabPage5.Parent = tabControl1;
                    tabPage6.Parent = tabControl1;
                    break;

            }
        }

        private void comboBox12_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox9.Items.Clear();
            this.comboBox9.Items.AddRange(new object[] {"0:"+comboBox12.Text+"/"+comboBox11.Text ,
                                                        "1:"+ comboBox12.Text ,
                                                        "2:"+comboBox12.Text+"+"+comboBox11.Text ,
                                                        "3:"+comboBox12.Text+"+"+comboBox11.Text+"+"+comboBox10.Text ,
                                                        "4:"+"("+comboBox12.Text+"+"+comboBox11.Text+")"+"/"+comboBox10.Text,
                                                        "5:"+comboBox12.Text+"/"+"("+comboBox12.Text+"+"+comboBox11.Text+"+"+comboBox10.Text+")",
                                                        "6:"+comboBox12.Text+"/"+"("+comboBox12.Text+"+"+comboBox11.Text+")",
                                                        "7:"+comboBox12.Text+"/"+"("+comboBox11.Text+"+"+comboBox10.Text+")",
                                                        "8:"+"("+comboBox12.Text+"-"+"B"+")"+"/"+"("+comboBox11.Text+"-"+"B"+")",
                                                        "9:"+comboBox11.Text+"/"+"("+comboBox12.Text+"-"+"B"+")"});
        }

        private void comboBox11_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox9.Items.Clear();
            this.comboBox9.Items.AddRange(new object[] {"0:"+comboBox12.Text+"/"+comboBox11.Text ,
                                                        "1:"+ comboBox12.Text ,
                                                        "2:"+comboBox12.Text+"+"+comboBox11.Text ,
                                                        "3:"+comboBox12.Text+"+"+comboBox11.Text+"+"+comboBox10.Text ,
                                                        "4:"+"("+comboBox12.Text+"+"+comboBox11.Text+")"+"/"+comboBox10.Text,
                                                        "5:"+comboBox12.Text+"/"+"("+comboBox12.Text+"+"+comboBox11.Text+"+"+comboBox10.Text+")",
                                                        "6:"+comboBox12.Text+"/"+"("+comboBox12.Text+"+"+comboBox11.Text+")",
                                                        "7:"+comboBox12.Text+"/"+"("+comboBox11.Text+"+"+comboBox10.Text+")",
                                                        "8:"+"("+comboBox12.Text+"-"+"B"+")"+"/"+"("+comboBox11.Text+"-"+"B"+")",
                                                        "9:"+comboBox11.Text+"/"+"("+comboBox12.Text+"-"+"B"+")"});
        }

        private void comboBox10_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox9.Items.Clear();
            this.comboBox9.Items.AddRange(new object[] {"0:"+comboBox12.Text+"/"+comboBox11.Text ,
                                                        "1:"+ comboBox12.Text ,
                                                        "2:"+comboBox12.Text+"+"+comboBox11.Text ,
                                                        "3:"+comboBox12.Text+"+"+comboBox11.Text+"+"+comboBox10.Text ,
                                                        "4:"+"("+comboBox12.Text+"+"+comboBox11.Text+")"+"/"+comboBox10.Text,
                                                        "5:"+comboBox12.Text+"/"+"("+comboBox12.Text+"+"+comboBox11.Text+"+"+comboBox10.Text+")",
                                                        "6:"+comboBox12.Text+"/"+"("+comboBox12.Text+"+"+comboBox11.Text+")",
                                                        "7:"+comboBox12.Text+"/"+"("+comboBox11.Text+"+"+comboBox10.Text+")",
                                                        "8:"+"("+comboBox12.Text+"-"+"B"+")"+"/"+"("+comboBox11.Text+"-"+"B"+")",
                                                        "9:"+comboBox11.Text+"/"+"("+comboBox12.Text+"-"+"B"+")"});
        }

        private void comboBox19_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox16.Items.Clear();
            this.comboBox16.Items.AddRange(new object[] {"0:"+comboBox19.Text+"/"+comboBox18.Text ,
                                                        "1:"+ comboBox19.Text ,
                                                        "2:"+comboBox19.Text+"+"+comboBox18.Text ,
                                                        "3:"+comboBox19.Text+"+"+comboBox18.Text+"+"+comboBox17.Text ,
                                                        "4:"+"("+comboBox19.Text+"+"+comboBox18.Text+")"+"/"+comboBox17.Text,
                                                        "5:"+comboBox19.Text+"/"+"("+comboBox19.Text+"+"+comboBox18.Text+"+"+comboBox17.Text+")",
                                                        "6:"+comboBox19.Text+"/"+"("+comboBox19.Text+"+"+comboBox18.Text+")",
                                                        "7:"+comboBox19.Text+"/"+"("+comboBox18.Text+"+"+comboBox17.Text+")",
                                                        "8:"+"("+comboBox19.Text+"-"+"B"+")"+"/"+"("+comboBox18.Text+"-"+"B"+")",
                                                        "9:"+comboBox18.Text+"/"+"("+comboBox19.Text+"-"+"B"+")"});
        }

        private void comboBox18_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox16.Items.Clear();
            this.comboBox16.Items.AddRange(new object[] {"0:"+comboBox19.Text+"/"+comboBox18.Text ,
                                                        "1:"+ comboBox19.Text ,
                                                        "2:"+comboBox19.Text+"+"+comboBox18.Text ,
                                                        "3:"+comboBox19.Text+"+"+comboBox18.Text+"+"+comboBox17.Text ,
                                                        "4:"+"("+comboBox19.Text+"+"+comboBox18.Text+")"+"/"+comboBox17.Text,
                                                        "5:"+comboBox19.Text+"/"+"("+comboBox19.Text+"+"+comboBox18.Text+"+"+comboBox17.Text+")",
                                                        "6:"+comboBox19.Text+"/"+"("+comboBox19.Text+"+"+comboBox18.Text+")",
                                                        "7:"+comboBox19.Text+"/"+"("+comboBox18.Text+"+"+comboBox17.Text+")",
                                                        "8:"+"("+comboBox19.Text+"-"+"B"+")"+"/"+"("+comboBox18.Text+"-"+"B"+")",
                                                        "9:"+comboBox18.Text+"/"+"("+comboBox19.Text+"-"+"B"+")"});
        }

        private void comboBox17_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox16.Items.Clear();
            this.comboBox16.Items.AddRange(new object[] {"0:"+comboBox19.Text+"/"+comboBox18.Text ,
                                                        "1:"+ comboBox19.Text ,
                                                        "2:"+comboBox19.Text+"+"+comboBox18.Text ,
                                                        "3:"+comboBox19.Text+"+"+comboBox18.Text+"+"+comboBox17.Text ,
                                                        "4:"+"("+comboBox19.Text+"+"+comboBox18.Text+")"+"/"+comboBox17.Text,
                                                        "5:"+comboBox19.Text+"/"+"("+comboBox19.Text+"+"+comboBox18.Text+"+"+comboBox17.Text+")",
                                                        "6:"+comboBox19.Text+"/"+"("+comboBox19.Text+"+"+comboBox18.Text+")",
                                                        "7:"+comboBox19.Text+"/"+"("+comboBox18.Text+"+"+comboBox17.Text+")",
                                                        "8:"+"("+comboBox19.Text+"-"+"B"+")"+"/"+"("+comboBox18.Text+"-"+"B"+")",
                                                        "9:"+comboBox18.Text+"/"+"("+comboBox19.Text+"-"+"B"+")"});
        }

        private void comboBox25_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox23.Items.Clear();
            this.comboBox23.Items.AddRange(new object[] {"0:"+comboBox26.Text+"/"+comboBox25.Text ,
                                                        "1:"+ comboBox26.Text ,
                                                        "2:"+comboBox26.Text+"+"+comboBox25.Text ,
                                                        "3:"+comboBox26.Text+"+"+comboBox25.Text+"+"+comboBox24.Text ,
                                                        "4:"+"("+comboBox26.Text+"+"+comboBox25.Text+")"+"/"+comboBox24.Text,
                                                        "5:"+comboBox26.Text+"/"+"("+comboBox26.Text+"+"+comboBox25.Text+"+"+comboBox24.Text+")",
                                                        "6:"+comboBox26.Text+"/"+"("+comboBox26.Text+"+"+comboBox25.Text+")",
                                                        "7:"+comboBox26.Text+"/"+"("+comboBox25.Text+"+"+comboBox24.Text+")",
                                                        "8:"+"("+comboBox26.Text+"-"+"B"+")"+"/"+"("+comboBox25.Text+"-"+"B"+")",
                                                        "9:"+comboBox25.Text+"/"+"("+comboBox26.Text+"-"+"B"+")"});
        }

        private void comboBox24_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox23.Items.Clear();
            this.comboBox23.Items.AddRange(new object[] {"0:"+comboBox26.Text+"/"+comboBox25.Text ,
                                                        "1:"+ comboBox26.Text ,
                                                        "2:"+comboBox26.Text+"+"+comboBox25.Text ,
                                                        "3:"+comboBox26.Text+"+"+comboBox25.Text+"+"+comboBox24.Text ,
                                                        "4:"+"("+comboBox26.Text+"+"+comboBox25.Text+")"+"/"+comboBox24.Text,
                                                        "5:"+comboBox26.Text+"/"+"("+comboBox26.Text+"+"+comboBox25.Text+"+"+comboBox24.Text+")",
                                                        "6:"+comboBox26.Text+"/"+"("+comboBox26.Text+"+"+comboBox25.Text+")",
                                                        "7:"+comboBox26.Text+"/"+"("+comboBox25.Text+"+"+comboBox24.Text+")",
                                                        "8:"+"("+comboBox26.Text+"-"+"B"+")"+"/"+"("+comboBox25.Text+"-"+"B"+")",
                                                        "9:"+comboBox25.Text+"/"+"("+comboBox26.Text+"-"+"B"+")"});
        }

        private void comboBox31_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox29.Items.Clear();
            this.comboBox29.Items.AddRange(new object[] {"0:"+comboBox32.Text+"/"+comboBox31.Text ,
                                                        "1:"+ comboBox32.Text ,
                                                        "2:"+comboBox32.Text+"+"+comboBox31.Text ,
                                                        "3:"+comboBox32.Text+"+"+comboBox31.Text+"+"+comboBox30.Text ,
                                                        "4:"+"("+comboBox32.Text+"+"+comboBox31.Text+")"+"/"+comboBox30.Text,
                                                        "5:"+comboBox32.Text+"/"+"("+comboBox32.Text+"+"+comboBox31.Text+"+"+comboBox30.Text+")",
                                                        "6:"+comboBox32.Text+"/"+"("+comboBox32.Text+"+"+comboBox31.Text+")",
                                                        "7:"+comboBox32.Text+"/"+"("+comboBox31.Text+"+"+comboBox30.Text+")",
                                                        "8:"+"("+comboBox32.Text+"-"+"B"+")"+"/"+"("+comboBox31.Text+"-"+"B"+")",
                                                        "9:"+comboBox31.Text+"/"+"("+comboBox32.Text+"-"+"B"+")"});
        }

        private void comboBox30_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox29.Items.Clear();
            this.comboBox29.Items.AddRange(new object[] {"0:"+comboBox32.Text+"/"+comboBox31.Text ,
                                                        "1:"+ comboBox32.Text ,
                                                        "2:"+comboBox32.Text+"+"+comboBox31.Text ,
                                                        "3:"+comboBox32.Text+"+"+comboBox31.Text+"+"+comboBox30.Text ,
                                                        "4:"+"("+comboBox32.Text+"+"+comboBox31.Text+")"+"/"+comboBox30.Text,
                                                        "5:"+comboBox32.Text+"/"+"("+comboBox32.Text+"+"+comboBox31.Text+"+"+comboBox30.Text+")",
                                                        "6:"+comboBox32.Text+"/"+"("+comboBox32.Text+"+"+comboBox31.Text+")",
                                                        "7:"+comboBox32.Text+"/"+"("+comboBox31.Text+"+"+comboBox30.Text+")",
                                                        "8:"+"("+comboBox32.Text+"-"+"B"+")"+"/"+"("+comboBox31.Text+"-"+"B"+")",
                                                        "9:"+comboBox31.Text+"/"+"("+comboBox32.Text+"-"+"B"+")"});
        }

        private void comboBox39_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox37.Items.Clear();
            this.comboBox37.Items.AddRange(new object[] {"0:"+comboBox40.Text+"/"+comboBox39.Text ,
                                                        "1:"+ comboBox40.Text ,
                                                        "2:"+comboBox40.Text+"+"+comboBox39.Text ,
                                                        "3:"+comboBox40.Text+"+"+comboBox39.Text+"+"+comboBox38.Text ,
                                                        "4:"+"("+comboBox40.Text+"+"+comboBox39.Text+")"+"/"+comboBox38.Text,
                                                        "5:"+comboBox40.Text+"/"+"("+comboBox40.Text+"+"+comboBox39.Text+"+"+comboBox38.Text+")",
                                                        "6:"+comboBox40.Text+"/"+"("+comboBox40.Text+"+"+comboBox39.Text+")",
                                                        "7:"+comboBox40.Text+"/"+"("+comboBox39.Text+"+"+comboBox38.Text+")",
                                                        "8:"+"("+comboBox40.Text+"-"+"B"+")"+"/"+"("+comboBox39.Text+"-"+"B"+")",
                                                        "9:"+comboBox39.Text+"/"+"("+comboBox40.Text+"-"+"B"+")"});
        }

        private void comboBox38_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox37.Items.Clear();
            this.comboBox37.Items.AddRange(new object[] {"0:"+comboBox40.Text+"/"+comboBox39.Text ,
                                                        "1:"+ comboBox40.Text ,
                                                        "2:"+comboBox40.Text+"+"+comboBox39.Text ,
                                                        "3:"+comboBox40.Text+"+"+comboBox39.Text+"+"+comboBox38.Text ,
                                                        "4:"+"("+comboBox40.Text+"+"+comboBox39.Text+")"+"/"+comboBox38.Text,
                                                        "5:"+comboBox40.Text+"/"+"("+comboBox40.Text+"+"+comboBox39.Text+"+"+comboBox38.Text+")",
                                                        "6:"+comboBox40.Text+"/"+"("+comboBox40.Text+"+"+comboBox39.Text+")",
                                                        "7:"+comboBox40.Text+"/"+"("+comboBox39.Text+"+"+comboBox38.Text+")",
                                                        "8:"+"("+comboBox40.Text+"-"+"B"+")"+"/"+"("+comboBox39.Text+"-"+"B"+")",
                                                        "9:"+comboBox39.Text+"/"+"("+comboBox40.Text+"-"+"B"+")"});
        }

        private void comboBox40_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox37.Items.Clear();
            this.comboBox37.Items.AddRange(new object[] {"0:"+comboBox40.Text+"/"+comboBox39.Text ,
                                                        "1:"+ comboBox40.Text ,
                                                        "2:"+comboBox40.Text+"+"+comboBox39.Text ,
                                                        "3:"+comboBox40.Text+"+"+comboBox39.Text+"+"+comboBox38.Text ,
                                                        "4:"+"("+comboBox40.Text+"+"+comboBox39.Text+")"+"/"+comboBox38.Text,
                                                        "5:"+comboBox40.Text+"/"+"("+comboBox40.Text+"+"+comboBox39.Text+"+"+comboBox38.Text+")",
                                                        "6:"+comboBox40.Text+"/"+"("+comboBox40.Text+"+"+comboBox39.Text+")",
                                                        "7:"+comboBox40.Text+"/"+"("+comboBox39.Text+"+"+comboBox38.Text+")",
                                                        "8:"+"("+comboBox40.Text+"-"+"B"+")"+"/"+"("+comboBox39.Text+"-"+"B"+")",
                                                        "9:"+comboBox39.Text+"/"+"("+comboBox40.Text+"-"+"B"+")"});
        }
        //点击表格单元格显示对应的.BAS保存的信息 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            re.Clear();
            #region 加载现有的.BAS文件
            if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase", ".BAS"))
            {
                MyEncrypt.SHA_Dencrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + ".BAS",
                    @"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + "-副本" + ".BAS", "179346");
                re = comuse.ReadBasFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + "-副本" + ".BAS");
                comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + "-副本" + ".BAS");             
                textBox1.Text = re[0]; textBox2.Text = re[1]; comboBox1.Text = re[2]; comboBox2.Text = re[3];
                textBox3.Text = re[4]; textBox4.Text = re[5];
                if (re[6] == "是")
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if (re[7] == "判定")
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                comboBox3.Text = re[8];
                textBox5.Text = re[9];
                textBox6.Text = re[10];
                comboBox46.Text = re[11];
                if (Convert.ToInt32(re[8]) > 5)
                {
                    label69.Visible = true;
                    comboBox46.Visible = true;
                    comboBox46.Text = re[11];
                }
                textBox7.Text = re[12];
                comboBox4.Text = re[13];
                comboBox5.Text = re[14];
                if (comuse.FindSignInString(re, 0, '\t', 8).Count == 0)
                {
                    dataGridView14.Rows.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        i = dataGridView14.Rows.Add();
                        switch (i)
                        {
                            case 0:
                                dataGridView14.Rows[i].Cells[0].Value = "输出名称";
                                break;
                            case 1:
                                dataGridView14.Rows[i].Cells[0].Value = "计量单位";
                                break;
                            case 2:
                                dataGridView14.Rows[i].Cells[0].Value = "小数位数";
                                break;
                            case 3:
                                dataGridView14.Rows[i].Cells[0].Value = "范围小值";
                                break;
                            case 4:
                                dataGridView14.Rows[i].Cells[0].Value = "范围大值";
                                break;
                            case 5:
                                dataGridView14.Rows[i].Cells[0].Value = "范围小数";
                                break;
                            case 6:
                                dataGridView14.Rows[i].Cells[0].Value = "常数项V0";
                                break;
                            case 7:
                                dataGridView14.Rows[i].Cells[0].Value = "计算公式";
                                break;
                        }
                    }
                }
                else
                {
                    comuse.AddDataToGroup(comuse.FindSignInString(re, 0, '\t', 8), dataGridView14);//组合输出
                }
                comuse.AddDatagridview(comuse.FindSignInString(re, 0, '\t', 4), dataGridView3);//峰值
                comuse.AddSamDatagridview(comuse.FindSignInString(re, 0, '\t', 3), dataGridView2);//样本类型，加样量，缓冲液量以及混合液量
                subitem = comuse.AddDataToSubItem(comuse.FindSignInString(re, 0, '\t', 12), dataGridView4, dataGridView6, dataGridView8, dataGridView10, dataGridView12);//子项参数
                samcoffi = comuse.AddSamCoffi(comuse.FindSignInString(re, 0, '\t', 2), dataGridView5, dataGridView7, dataGridView9, dataGridView11, dataGridView13);//子项的样本系数
                for (int i = 0; i < subitem.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            comboBox7.Text = subitem[i][0];
                            comboBox6.Text = subitem[i][1];
                            comboBox41.Text = subitem[i][2];
                            comboBox12.Text = subitem[i][8];
                            comboBox11.Text = subitem[i][9];
                            comboBox10.Text = subitem[i][10];
                            comboBox9.Text = subitem[i][11];
                            comboBox8.Text = subitem[i][12];
                            break;
                        case 1:
                            comboBox14.Text = subitem[i][0];
                            comboBox13.Text = subitem[i][1];
                            comboBox42.Text = subitem[i][2];
                            comboBox19.Text = subitem[i][8];
                            comboBox18.Text = subitem[i][9];
                            comboBox17.Text = subitem[i][10];
                            comboBox16.Text = subitem[i][11];
                            comboBox15.Text = subitem[i][12];
                            break;
                        case 2:
                            comboBox21.Text = subitem[i][0];
                            comboBox20.Text = subitem[i][1];
                            comboBox43.Text = subitem[i][2];
                            comboBox26.Text = subitem[i][8];
                            comboBox25.Text = subitem[i][9];
                            comboBox24.Text = subitem[i][10];
                            comboBox23.Text = subitem[i][11];
                            comboBox22.Text = subitem[i][12];
                            break;
                        case 3:
                            comboBox28.Text = subitem[i][0];
                            comboBox27.Text = subitem[i][1];
                            comboBox44.Text = subitem[i][2];
                            comboBox32.Text = subitem[i][8];
                            comboBox31.Text = subitem[i][9];
                            comboBox30.Text = subitem[i][10];
                            comboBox29.Text = subitem[i][11];
                            comboBox33.Text = subitem[i][12];
                            break;
                        case 4:
                            comboBox34.Text = subitem[i][0];
                            comboBox35.Text = subitem[i][1];
                            comboBox45.Text = subitem[i][2];
                            comboBox40.Text = subitem[i][8];
                            comboBox39.Text = subitem[i][9];
                            comboBox38.Text = subitem[i][10];
                            comboBox37.Text = subitem[i][11];
                            comboBox36.Text = subitem[i][12];
                            break;


                    }
                }
            }
            #endregion
            //dataGridView1.Rows.Clear();
            //comuse.FindFileType(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase", ".BAS", dataGridView1);
            #region 加载信息结束后禁止更改表头加载的信息
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView6.Columns[0].ReadOnly = true;
            dataGridView8.Columns[0].ReadOnly = true;
            dataGridView10.Columns[0].ReadOnly = true;
            dataGridView12.Columns[0].ReadOnly = true;
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView5.Columns[0].ReadOnly = true;
            dataGridView7.Columns[0].ReadOnly = true;
            dataGridView9.Columns[0].ReadOnly = true;
            dataGridView11.Columns[0].ReadOnly = true;
            dataGridView13.Columns[0].ReadOnly = true;
            #endregion
            #region 加载信息后禁用编辑
            comuse.GetControl(panel4, false);
            comuse.GetControl(panel5, false);
            comuse.GetControl(panel6, false);
            comuse.GetControl(panel7, false);
            comuse.GetControl(groupBox1, false);
            comuse.GetControl(groupBox2, false);
            comuse.GetControl(groupBox3, false);
            comuse.GetControl(groupBox4, false);
            comuse.GetControl(groupBox5, false);
            comuse.GetControl(panel8, false);
            comuse.GetControl(tabPage6, false);
            comuse.GetControl(panel9, false);
            comuse.GetControl(panel10, false);
            comuse.GetControl(panel11, false);
            comuse.GetControl(panel12, false);
            comuse.GetControl(panel13, false);
            comuse.GetControl(panel14, false);
            comuse.GetControl(panel15, false);
            comuse.GetControl(panel16, false);
            comuse.GetControl(panel17, false);
            comuse.GetControl(panel20, false);
            comuse.GetControl(panel21, false);
            comuse.GetControl(panel22, false);
            #endregion
        }
        //清除组合输出已输入的信息
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView14.Rows.Clear();
            for (int i = 0; i < 8; i++)
            {
                i = dataGridView14.Rows.Add();
                switch (i)
                {
                    case 0:
                        dataGridView14.Rows[i].Cells[0].Value = "输出名称";
                        break;
                    case 1:
                        dataGridView14.Rows[i].Cells[0].Value = "计量单位";
                        break;
                    case 2:
                        dataGridView14.Rows[i].Cells[0].Value = "小数位数";
                        break;
                    case 3:
                        dataGridView14.Rows[i].Cells[0].Value = "范围小值";
                        break;
                    case 4:
                        dataGridView14.Rows[i].Cells[0].Value = "范围大值";
                        break;
                    case 5:
                        dataGridView14.Rows[i].Cells[0].Value = "范围小数";
                        break;
                    case 6:
                        dataGridView14.Rows[i].Cells[0].Value = "常数项V0";
                        break;
                    case 7:
                        dataGridView14.Rows[i].Cells[0].Value = "计算公式";
                        break;
                }
            }
        }
        //右键添加样本类型
        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
           if(e.Button==MouseButtons.Right)//点击鼠标右键
           {          
               AddSamType addsam = new AddSamType();
               int index_addsam = dataGridView2.Rows.Count;
               index_addsam = dataGridView2.Rows.Add();             
               addsam.ShowDialog();
               dataGridView2.Rows[index_addsam].Cells[0].Value = addsam.SampleType;
               dataGridView2.Rows[index_addsam].Cells[1].Value = addsam.Sam_Add_Value;
               dataGridView2.Rows[index_addsam].Cells[2].Value = addsam.Buffer_Add_Value;
               dataGridView2.Rows[index_addsam].Cells[3].Value = addsam.Mixture_Add_Vale;
              
           }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
         
      

     

    

    }
}
