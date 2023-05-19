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
    public partial class TestItem : Form
    {
        private static TestItem formInstance6;
        Common_use comuse = new Common_use();
        List<string> re = new List<string>();
        List<ItemInfo> iteminf = new List<ItemInfo>();
        string path_in=@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "_" + ".TST";
        string path_or=@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" +"副本"+ ".TST";
        public TestItem()
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
        public static TestItem GetIntance
        {
            get
            {
                if (formInstance6 != null)
                {
                    return formInstance6;
                }
                else
                {
                    formInstance6 = new TestItem();
                    return formInstance6;
                }
            }
        }
        //新建以实验编号为名的文件夹以及TST文件
        private void button6_Click(object sender, EventArgs e)
        {
            New_Exp newexp = new New_Exp();
            comuse.FindFileType(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase", ".BAS", newexp.dataGridView1);
            newexp.ShowDialog();
            int index_item = dataGridView1.Rows.Count;
            index_item = dataGridView1.Rows.Add();
            dataGridView1.Rows[index_item].Cells[0].Value = newexp.ItemNumber;
            dataGridView1.Rows[index_item].Cells[1].Value = newexp.Itemname;
            dataGridView1.Rows[index_item].Cells[2].Value = newexp.Itemexplain;
        }
        //删除按键
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int index_delete = dataGridView1.CurrentRow.Index;
                if (dataGridView1.Rows[index_delete].Cells[0].Selected)
                {
                    DataGridViewRow row = dataGridView1.Rows[index_delete];
                    comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + ".TST");
                    MyEncrypt.SHA_Dencrypt(path_in, path_or, "179346");//解密             
                    iteminf = comuse.ReadItem(path_or);
                    comuse.DeleteOneFile(path_or);//删除解密文
                    comuse.DeleteOneFile(path_in);//删除原文
                    for (int i = iteminf.Count - 1; i >= 0; i--)
                    {
                        if (iteminf[i].Itemnumber == dataGridView1.Rows[index_delete].Cells[0].Value.ToString())
                        {
                            iteminf.Remove(iteminf[i]);
                        }
                    }
                    dataGridView1.Rows.Remove(row);
                    comuse.WriteTSTFile(iteminf, path_or);
                    MyEncrypt.SHA_Encrypt(path_or, path_in, "179346");
                    comuse.DeleteOneFile(path_or);
                }
                else
                {
                    MessageBox.Show("未找到需要删除的选项！！");
                }
            }
        }
        //界面加载事件
        private void TestItem_Load(object sender, EventArgs e)
        {
            re.Clear();
            if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest", ".TST"))//判断是否有记录文件
            {
                MyEncrypt.SHA_Dencrypt(path_in , path_or, "179346");//解密             
                iteminf = comuse.ReadItem(path_or);
                comuse.DeleteOneFile(path_or);//删除解密文件           
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();
                for (int i = 0; i < iteminf.Count; i++)
                {
                    i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = iteminf[i].Itemnumber;
                    dataGridView1.Rows[i].Cells[1].Value = iteminf[i].Itemname;
                    dataGridView1.Rows[i].Cells[2].Value = iteminf[i].Itemexplain;
                }
                if (dataGridView1.Rows.Count > 0)
                {
                    if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[0].Cells[0].Value.ToString(), ".TST"))
                    {
                        MyEncrypt.SHA_Dencrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\"
                            + dataGridView1.Rows[0].Cells[0].Value.ToString() + ".TST",
                       @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "-副本"
                       + ".TST", "179346");//解密
                        re = comuse.ReadBasFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\"
                            + dataGridView1.Rows[0].Cells[0].Value.ToString() + "-副本" + ".TST");//读取解密文件
                        comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "\\"
                            + dataGridView1.Rows[0].Cells[0].Value.ToString() + "-副本" + ".TST");//删除解密文件
                        comuse.AddDatagridview(comuse.FindSignInString(re, 0, '\t', 4), dataGridView2);//峰值
                        comuse.GetSubitemInfo(comuse.FindSignInString(re, 0, '\t', 12), dataGridView3);//子项参数P1、P2以及P3
                    }
                }
                if(dataGridView3.Rows.Count>0)
                {
                    for (int i = 0; i < dataGridView3.Rows.Count + dataGridView2.Rows.Count; i++)
                    {
                        i = dataGridView4.Rows.Add();
                        if (i >= 0 && i < dataGridView2.Rows.Count)
                        {
                            dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        }
                        else
                        {
                            dataGridView4.Rows[i].Cells[0].Value = dataGridView3.Rows[i - (dataGridView2.Rows.Count)].Cells[4].Value;
                        }
                    }
                }
                else
                {
                    for(int i=0;i<2;i++)
                    {
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                }
               
            }
            
        }
        //单击表格显示相应的数据
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();
                int index_delete = dataGridView1.CurrentRow.Index;
                if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString(), ".TST"))
                {
                    MyEncrypt.SHA_Dencrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "\\"
                        + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + ".TST",
                   @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "\\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "-副本"
                   + ".TST", "179346");//解密
                    re = comuse.ReadBasFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "\\"
                        + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "-副本" + ".TST");//读取解密文件
                    comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "\\"
                        + dataGridView1.Rows[index_delete].Cells[0].Value.ToString() + "-副本" + ".TST");//删除解密文件
                    comuse.AddDatagridview(comuse.FindSignInString(re, 0, '\t', 4), dataGridView2);//峰值
                    comuse.GetSubitemInfo(comuse.FindSignInString(re, 0, '\t', 12), dataGridView3);//子项参数P1、P2以及P3
                }
               if(dataGridView3.Rows.Count > 2)
               {
                   for (int i = 0; i < dataGridView3.Rows.Count + 4; i++)
                   {
                       i = dataGridView4.Rows.Add();
                       if (i >= 0 && i < dataGridView3.Rows.Count + 1)
                       {
                           dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                       }
                       else
                       {
                           dataGridView4.Rows[i].Cells[0].Value = dataGridView3.Rows[i - (dataGridView3.Rows.Count + 1)].Cells[4].Value;
                       }
                   }
               }
               else if ( dataGridView3.Rows.Count == 2)
               {
                   for (int i = 0; i < dataGridView3.Rows.Count + 3; i++)
                   {
                       i = dataGridView4.Rows.Add();
                       if (i >= 0 && i < dataGridView3.Rows.Count + 1)
                       {
                           dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                       }
                       else
                       {
                           dataGridView4.Rows[i].Cells[0].Value = dataGridView3.Rows[i - (dataGridView3.Rows.Count + 1)].Cells[4].Value;
                       }
                   }
               }
                else if(dataGridView3.Rows.Count==0)
               {
                   for(int i=0;i<2;i++)
                   {
                       i = dataGridView4.Rows.Add();
                       dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                   }
               }
            }
        }
      }
  }

