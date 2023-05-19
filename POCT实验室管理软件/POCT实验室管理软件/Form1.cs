using POCT实验室管理软件.Common_Method;
using POCT实验室管理软件.MenuForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCT实验室管理软件
{
    public partial class Form1 : Form
    {
        public static Form1 formInstance;
        public static Form1 SForm1 = null;
        ///<summary>
        ///子窗体界面单例元素
        ///</summary>
        public static signaltest form1 = null;
        public static linefit form2 = null;
        public static Itemparameter form3 = null;
        public static generateID form4 = null;
        public static checkID form5 = null;
        public static TestItem form6 = null;
        public object lockObj = new object();
        public bool formSwitchFlag = false;
        //按钮初始化
        private bool initButton()
        {
            try
            {
                this.btn_test.BackColor = Color.FromArgb(255, 255, 220);
                this.btn_line.BackColor = Color.FromArgb(255, 255, 220);
                this.btn_parameter.BackColor = Color.FromArgb(255, 255, 220);
                this.btn_createID.BackColor = Color.FromArgb(255, 255, 200);
                this.btn_checkid.BackColor = Color.FromArgb(255, 255, 200);
                this.btn_testitem.BackColor = Color.FromArgb(255, 255, 200);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        //父窗口与子窗口设计
        public static Form1 GetIntance
        {
            get
            {
                if (formInstance != null)
                {
                    return formInstance;
                }
                else
                {
                    formInstance = new Form1();
                    return formInstance;
                }
            }
        }
        ///<summary>
        ///当前显示窗体
        ///</summary>
        private System.Windows.Forms.Form currentForm;
        ///<summary>
        ///显示窗体
        ///</summary>
        ///<param name="panel1"></param>
        ///<param name="frm"></param>
        public void ShowForm(System.Windows.Forms.Panel panel, System.Windows.Forms.Form frm)
        {
            lock (this)
            {
                try
                {
                    if (this.currentForm != null && this.currentForm == frm)
                    {
                        return;
                    }
                    else if (this.currentForm != null)
                    {
                        if (this.ActiveMdiChild != null)
                        {
                            this.ActiveMdiChild.Hide();
                        }
                    }
                    this.currentForm = frm;
                    frm.TopLevel = false;
                    frm.MdiParent = this;
                    panel.Controls.Clear();
                    panel.Controls.Add(frm);
                    frm.Show();
                    frm.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Refresh();
                    foreach (Control item in frm.Controls)
                    {
                        item.Focus();
                        break;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //解决闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;
                return cp;
            }
        }
        public Form1()
        {
            InitializeComponent();
            SForm1 = this;
            //主窗体容器打开
            this.IsMdiContainer = true;
            //实例化子窗体界面
            form1 = signaltest.GetIntance;
            form2 = linefit.GetIntance;
            form3 = Itemparameter.GetIntance;
            form4 = generateID.GetIntance;
            form5 = checkID.GetIntance;
            form6 = TestItem.GetIntance;
            //初始化按钮
            this.initButton();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            this.Connect.Text = "通讯状态：未通讯";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Systime.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            try
            {
                this.initButton();
                this.btn_test.BackColor = Color.FromArgb(95, 129, 174);
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(panel2, form1);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            try
            {
                this.initButton();
                this.btn_line.BackColor = Color.FromArgb(95, 129, 174);
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(panel2, form2);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }

        private void btn_parameter_Click(object sender, EventArgs e)
        {
            try
            {
                this.initButton();
                this.btn_parameter.BackColor = Color.FromArgb(95, 129, 174);
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(panel2, form3);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }         
        }

        private void btn_createID_Click(object sender, EventArgs e)
        {
            try
            {
                this.initButton();
                this.btn_createID.BackColor = Color.FromArgb(95, 129, 174);
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(panel2, form4);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }

       

        private void btn_checkid_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.initButton();
                this.btn_checkid.BackColor = Color.FromArgb(95, 129, 174);
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(panel2, form5);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }

        private void btn_testitem_Click(object sender, EventArgs e)
        {
            try
            {
                this.initButton();
                this.btn_testitem.BackColor = Color.FromArgb(95, 129, 174);
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(panel2, form6);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }

       
    }
}
