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
    public partial class New_Exp : Form
    {
        string path = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest";
        Common_use comuse = new Common_use();
        List<string> re = new List<string>();
        public string Itemname;
        public string ItemNumber;
        public string Itemexplain;
        public New_Exp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox1.Text.Trim()!=null)
            {
                string Directoryname = textBox1.Text.Trim();
                comuse.CreateDirectoryOrFile(path, Directoryname);          
                MyEncrypt.SHA_Dencrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + ".BAS",
                   @"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + "-副本" + ".BAS", "179346");
                re = comuse.ReadBasFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + "-副本" + ".BAS");
                comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemBase\" + dataGridView1.CurrentCell.Value.ToString() + "-副本" + ".BAS");
                comuse.WriteTstFile(re, @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + textBox1.Text.Trim()+ "\\" + textBox1.Text.Trim() + "-" + ".TST");
                string finalname = @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + textBox1.Text.Trim() + "\\" + textBox1.Text.Trim() + ".TST";
                MyEncrypt.SHA_Encrypt(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + textBox1.Text.Trim() + "\\" + textBox1.Text.Trim() + "-" + ".TST", finalname, "179346");
                comuse.DeleteOneFile(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + textBox1.Text.Trim() + "\\" + textBox1.Text.Trim() + "-" + ".TST");
                Itemname = dataGridView1.CurrentCell.Value.ToString();
                ItemNumber = textBox1.Text.Trim();
                Itemexplain = textBox2.Text;
                string path_or=@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + ".TST";//输入文件
                string path_fina=@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\" + "_" + ".TST";//输出文件
                //将新增实验插入记录实验名称、实验编号以及实验说明的TST文件中，先判断是否有记录文件，若有先进行解密然后再插入新纪录，若无直接进行插入即可
                if (comuse.FileExist(@"E:\VS13\POCT\POCT实验室管理软件\ItemTest\", ".TST"))
                {
                    MyEncrypt.SHA_Dencrypt(path_fina, path_or, "179346");//解密
                    comuse.WriteItem(Itemname, ItemNumber, Itemexplain, @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\", ".TST");//写入
                    MyEncrypt.SHA_Encrypt(path_or, path_fina, "179346");//加密
                    comuse.DeleteOneFile(path_or);//删除未加密的源文件
                }
                else
                {
                    comuse.WriteItem(Itemname, ItemNumber, Itemexplain, @"E:\VS13\POCT\POCT实验室管理软件\ItemTest\", ".TST");//写入
                    MyEncrypt.SHA_Encrypt(path_or, path_fina, "179346");//加密
                    comuse.DeleteOneFile(path_or);//删除未加密的源文件
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("请填写实验编号！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
