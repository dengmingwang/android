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
    public partial class AddSamType : Form
    {
        public  string SampleType;
        public  string Sam_Add_Value;
        public  string Buffer_Add_Value;
        public  string Mixture_Add_Vale;
        public AddSamType()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SampleType = textBox1.Text;
            Sam_Add_Value = textBox2.Text;
            Buffer_Add_Value = textBox3.Text;
            Mixture_Add_Vale = textBox4.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
