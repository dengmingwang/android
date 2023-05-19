using POCT实验室管理软件.Common_Method;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCT实验室管理软件.MenuForm
{
    public partial class signaltest : Form
    {
        private static signaltest formInstance1;
        Common_use comuse = new Common_use();
        ComPort cp = new ComPort();
        //实例化监听事件
        SerialPortListener m_SerialPort = null;
        //存储接收到的数据的List<string[]>
        string[] sourcedata ;
        int save_num = 0;
        string root = @"E:\VS13\POCT\POCT实验室管理软件";
        //实例化datatable
        System.Data.DataTable data = new System.Data.DataTable();
        public signaltest()
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
        public static signaltest GetIntance
        {
            get
            {
                if (formInstance1 != null)
                {
                    return formInstance1;
                }
                else
                {
                    formInstance1 = new signaltest();
                    return formInstance1;
                }
            }
        }
        //打开串口
        private void button42_Click(object sender, EventArgs e)
        {
            string portName = "COM" + ComPort.GetComNum().ToString();
            int baudRate = 9600;
            try
            {
                if(m_SerialPort==null)
                {
                    m_SerialPort = new SerialPortListener(portName, baudRate);
                    m_SerialPort.StopBits = StopBits.Two;
                    m_SerialPort.Handshake = Handshake.None;
                    m_SerialPort.DataBits = 8;
                    m_SerialPort.ReadBufferSize = 88192;
                    m_SerialPort.ReceivedBytesThreshold = 1;
                    m_SerialPort.BufferSize = 1024;
                    m_SerialPort.ReceiveTimeout = 500;
                    m_SerialPort.WriteBufferSize = 100;
                    m_SerialPort.SendInterval = 100;
                    m_SerialPort.SerialPortResult += new HandResult(SerialPort_Result);
                }
                if(m_SerialPort.IsOpen)
                {
                    m_SerialPort.Stop();
                }
                else
                {
                    m_SerialPort.Start();
                    MessageBox.Show("已打开串口" + portName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SerialPort_Result(object sender,SerialPortEvents e)
        {
            if(e.BufferData!=null)
            {
                sourcedata = comuse.Dataprocess(e.BufferData);
                this.Invoke(new MethodInvoker(() =>
                {
                    comuse.DataGridViewGetValue(sourcedata, dataGridView1);
                    comuse.DrawLine(sourcedata, Color.Red, chart1);
                }));
            }
        }
        //加卡
        private void button51_Click(object sender, EventArgs e)
        {
            m_SerialPort.Send(cp.Test());
        }
        //丢卡
        private void button52_Click(object sender, EventArgs e)
        {
            m_SerialPort.Send(cp.TestofThrowd());
        }
        //新建按钮
        private void button43_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            dataGridView3.Columns.Clear();
            dataGridView4.Columns.Clear();
            chart1.Series.Clear();
            comuse.AddRows(dataGridView2, dataGridView3, dataGridView4,dataGridView5,dataGridView6);
        }

        private void signaltest_Load(object sender, EventArgs e)
        {
            comuse.AddRows(dataGridView2, dataGridView3, dataGridView4,dataGridView5,dataGridView6);
        }
        //重复性测试打开按钮，将excel中的数据加载到datagridview中，并绘制曲线
        private void button44_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear() ;
                chart1.Series.Clear();
                data = comuse.NewMethod().Tables[0];
                List<string[]> data_source = new List<string[]>();
                for(int j=0;j<data.Columns.Count;j++)
                {
                    string[] str = new string[data.Rows.Count - 1];
                    for (int i = 1; i <= data.Rows.Count-1; i++)
                    {                        
                        str[i-1] = data.Rows[i][j].ToString();
                    }
                    data_source.Add(str);
                }
                for(int i=0;i<data_source.Count;i++)
                {
                    comuse.DataGridViewGetValue(data_source[i], dataGridView1);
                    comuse.DrawLine(data_source[i],Color.Red,chart1);
                }
                string reference_peak = string.Empty;
                int peak_num = Convert.ToInt32(comboBox6.Text.Trim());//峰个数
                int line_num = dataGridView1.ColumnCount;//曲线个数
                if (comboBox7.Text.Trim() != "")
                {
                    reference_peak = comboBox7.Text.Trim();
                }
                else
                {
                    MessageBox.Show("请选择基准峰");
                    return;
                }
                List<string[]> cal_result = new List<string[]>();
                cal_result = comuse.GetDataToCal(dataGridView1, dataGridView4, reference_peak, "280");
                comuse.AddMaxAndIndexToDgv(cal_result, dataGridView6, peak_num, line_num);
                comuse.AddDataToDgv(comboBox8.Text.Trim(), cal_result, dataGridView2, peak_num, line_num, reference_peak);
                comuse.MenStaCVDgvGetdata(cal_result, comboBox8.Text.Trim(), dataGridView3, peak_num, line_num, reference_peak);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        //保存数据
        private void button45_Click(object sender, EventArgs e)
        {
            save_num = save_num + 1;
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("当前无数据可导出！");
            }
            else
            {
                string filename = "D:" + "\\" + save_num.ToString() + ".xls";
                comuse.ExportExcels(filename, dataGridView1);
            }
        }
        //峰个数combobox
        private void comboBox6_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView6.Rows.Clear();
            comboBox7.Items.Clear();
            switch(comboBox6.Text)
            {
                case"2":
                    for(int i=0;i<2;i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "93";
                    dataGridView4.Rows[0].Cells[2].Value = "208";
                    dataGridView4.Rows[0].Cells[3].Value = "60";
                    dataGridView4.Rows[1].Cells[1].Value = "240";
                    dataGridView4.Rows[1].Cells[2].Value = "364";
                    dataGridView4.Rows[1].Cells[3].Value = "60";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2" });
                    break;
                case "3":
                    for (int i = 0; i < 3; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "40";
                    dataGridView4.Rows[0].Cells[2].Value = "165";
                    dataGridView4.Rows[0].Cells[3].Value = "60";
                    dataGridView4.Rows[1].Cells[1].Value = "166";
                    dataGridView4.Rows[1].Cells[2].Value = "260";
                    dataGridView4.Rows[1].Cells[3].Value = "60";
                    dataGridView4.Rows[2].Cells[1].Value = "261";
                    dataGridView4.Rows[2].Cells[2].Value = "340";
                    dataGridView4.Rows[2].Cells[3].Value = "60";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3" });
                    break;
                case "4":
                    for (int i = 0; i < 4; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "10";
                    dataGridView4.Rows[0].Cells[2].Value = "90";
                    dataGridView4.Rows[0].Cells[3].Value = "40";
                    dataGridView4.Rows[1].Cells[1].Value = "91";
                    dataGridView4.Rows[1].Cells[2].Value = "180";
                    dataGridView4.Rows[1].Cells[3].Value = "40";
                    dataGridView4.Rows[2].Cells[1].Value = "181";
                    dataGridView4.Rows[2].Cells[2].Value = "270";
                    dataGridView4.Rows[2].Cells[3].Value = "40";
                    dataGridView4.Rows[3].Cells[1].Value = "271";
                    dataGridView4.Rows[3].Cells[2].Value = "350";
                    dataGridView4.Rows[3].Cells[3].Value = "40";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4" });
                    break;
                case "5":
                    for (int i = 0; i < 5; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "0";
                    dataGridView4.Rows[0].Cells[2].Value = "34";
                    dataGridView4.Rows[0].Cells[3].Value = "5";
                    dataGridView4.Rows[1].Cells[1].Value = "35";
                    dataGridView4.Rows[1].Cells[2].Value = "69";
                    dataGridView4.Rows[1].Cells[3].Value = "5";
                    dataGridView4.Rows[2].Cells[1].Value = "70";
                    dataGridView4.Rows[2].Cells[2].Value = "114";
                    dataGridView4.Rows[2].Cells[3].Value = "5";
                    dataGridView4.Rows[3].Cells[1].Value = "115";
                    dataGridView4.Rows[3].Cells[2].Value = "149";
                    dataGridView4.Rows[3].Cells[3].Value = "5";
                    dataGridView4.Rows[4].Cells[1].Value = "150";
                    dataGridView4.Rows[4].Cells[2].Value = "173";
                    dataGridView4.Rows[4].Cells[3].Value = "5";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5" });
                    break;
                case "6":
                    for (int i = 0; i < 6; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "0";
                    dataGridView4.Rows[0].Cells[2].Value = "165";
                    dataGridView4.Rows[0].Cells[3].Value = "60";
                    dataGridView4.Rows[1].Cells[1].Value = "166";
                    dataGridView4.Rows[1].Cells[2].Value = "330";
                    dataGridView4.Rows[1].Cells[3].Value = "60";
                    dataGridView4.Rows[2].Cells[1].Value = "331";
                    dataGridView4.Rows[2].Cells[2].Value = "495";
                    dataGridView4.Rows[2].Cells[3].Value = "60";
                    dataGridView4.Rows[3].Cells[1].Value = "496";
                    dataGridView4.Rows[3].Cells[2].Value = "660";
                    dataGridView4.Rows[3].Cells[3].Value = "60";
                    dataGridView4.Rows[4].Cells[1].Value = "661";
                    dataGridView4.Rows[4].Cells[2].Value = "800";
                    dataGridView4.Rows[4].Cells[3].Value = "60";
                    dataGridView4.Rows[5].Cells[1].Value = "800";
                    dataGridView4.Rows[5].Cells[2].Value = "960";
                    dataGridView4.Rows[5].Cells[3].Value = "60";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6" });
                    break;
                case "7":
                    for (int i = 0; i < 7; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "0";
                    dataGridView4.Rows[0].Cells[2].Value = "24";
                    dataGridView4.Rows[0].Cells[3].Value = "5";
                    dataGridView4.Rows[1].Cells[1].Value = "25";
                    dataGridView4.Rows[1].Cells[2].Value = "48";
                    dataGridView4.Rows[1].Cells[3].Value = "5";
                    dataGridView4.Rows[2].Cells[1].Value = "49";
                    dataGridView4.Rows[2].Cells[2].Value = "72";
                    dataGridView4.Rows[2].Cells[3].Value = "5";
                    dataGridView4.Rows[3].Cells[1].Value = "73";
                    dataGridView4.Rows[3].Cells[2].Value = "96";
                    dataGridView4.Rows[3].Cells[3].Value = "5";
                    dataGridView4.Rows[4].Cells[1].Value = "97";
                    dataGridView4.Rows[4].Cells[2].Value = "120";
                    dataGridView4.Rows[4].Cells[3].Value = "5";
                    dataGridView4.Rows[5].Cells[1].Value = "121";
                    dataGridView4.Rows[5].Cells[2].Value = "144";
                    dataGridView4.Rows[5].Cells[3].Value = "5";
                    dataGridView4.Rows[6].Cells[1].Value = "145";
                    dataGridView4.Rows[6].Cells[2].Value = "173";
                    dataGridView4.Rows[6].Cells[3].Value = "5";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7" });
                    break;
                case "8":
                    for (int i = 0; i < 8; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "0";
                    dataGridView4.Rows[0].Cells[2].Value = "70";
                    dataGridView4.Rows[0].Cells[3].Value = "40";
                    dataGridView4.Rows[1].Cells[1].Value = "80";
                    dataGridView4.Rows[1].Cells[2].Value = "120";
                    dataGridView4.Rows[1].Cells[3].Value = "40";
                    dataGridView4.Rows[2].Cells[1].Value = "125";
                    dataGridView4.Rows[2].Cells[2].Value = "170";
                    dataGridView4.Rows[2].Cells[3].Value = "40";
                    dataGridView4.Rows[3].Cells[1].Value = "180";
                    dataGridView4.Rows[3].Cells[2].Value = "220";
                    dataGridView4.Rows[3].Cells[3].Value = "40";
                    dataGridView4.Rows[4].Cells[1].Value = "225";
                    dataGridView4.Rows[4].Cells[2].Value = "270";
                    dataGridView4.Rows[4].Cells[3].Value = "40";
                    dataGridView4.Rows[5].Cells[1].Value = "275";
                    dataGridView4.Rows[5].Cells[2].Value = "320";
                    dataGridView4.Rows[5].Cells[3].Value = "40";
                    dataGridView4.Rows[6].Cells[1].Value = "325";
                    dataGridView4.Rows[6].Cells[2].Value = "370";
                    dataGridView4.Rows[6].Cells[3].Value = "40";
                    dataGridView4.Rows[7].Cells[1].Value = "375";
                    dataGridView4.Rows[7].Cells[2].Value = "425";
                    dataGridView4.Rows[7].Cells[3].Value = "40";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8" });
                    break;
                case "9":
                    for (int i = 0; i < 9; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "0";
                    dataGridView4.Rows[0].Cells[2].Value = "19";
                    dataGridView4.Rows[0].Cells[3].Value = "5";
                    dataGridView4.Rows[1].Cells[1].Value = "20";
                    dataGridView4.Rows[1].Cells[2].Value = "38";
                    dataGridView4.Rows[1].Cells[3].Value = "5";
                    dataGridView4.Rows[2].Cells[1].Value = "39";
                    dataGridView4.Rows[2].Cells[2].Value = "57";
                    dataGridView4.Rows[2].Cells[3].Value = "5";
                    dataGridView4.Rows[3].Cells[1].Value = "58";
                    dataGridView4.Rows[3].Cells[2].Value = "76";
                    dataGridView4.Rows[3].Cells[3].Value = "5";
                    dataGridView4.Rows[4].Cells[1].Value = "77";
                    dataGridView4.Rows[4].Cells[2].Value = "95";
                    dataGridView4.Rows[4].Cells[3].Value = "5";
                    dataGridView4.Rows[5].Cells[1].Value = "96";
                    dataGridView4.Rows[5].Cells[2].Value = "114";
                    dataGridView4.Rows[5].Cells[3].Value = "5";
                    dataGridView4.Rows[6].Cells[1].Value = "115";
                    dataGridView4.Rows[6].Cells[2].Value = "133";
                    dataGridView4.Rows[6].Cells[3].Value = "5";
                    dataGridView4.Rows[7].Cells[1].Value = "134";
                    dataGridView4.Rows[7].Cells[2].Value = "152";
                    dataGridView4.Rows[7].Cells[3].Value = "5";
                    dataGridView4.Rows[8].Cells[1].Value = "153";
                    dataGridView4.Rows[8].Cells[2].Value = "173";
                    dataGridView4.Rows[8].Cells[3].Value = "5";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9" });
                    break;
                case "10":
                    for (int i = 0; i < 10; i++)
                    {
                        i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView3.Rows.Add();
                        dataGridView3.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                        i = dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "X" + (i + 1).ToString();
                    }
                    dataGridView4.Rows[0].Cells[1].Value = "0";
                    dataGridView4.Rows[0].Cells[2].Value = "17";
                    dataGridView4.Rows[0].Cells[3].Value = "5";
                    dataGridView4.Rows[1].Cells[1].Value = "18";
                    dataGridView4.Rows[1].Cells[2].Value = "34";
                    dataGridView4.Rows[1].Cells[3].Value = "5";
                    dataGridView4.Rows[2].Cells[1].Value = "35";
                    dataGridView4.Rows[2].Cells[2].Value = "51";
                    dataGridView4.Rows[2].Cells[3].Value = "5";
                    dataGridView4.Rows[3].Cells[1].Value = "52";
                    dataGridView4.Rows[3].Cells[2].Value = "68";
                    dataGridView4.Rows[3].Cells[3].Value = "5";
                    dataGridView4.Rows[4].Cells[1].Value = "69";
                    dataGridView4.Rows[4].Cells[2].Value = "85";
                    dataGridView4.Rows[4].Cells[3].Value = "5";
                    dataGridView4.Rows[5].Cells[1].Value = "86";
                    dataGridView4.Rows[5].Cells[2].Value = "102";
                    dataGridView4.Rows[5].Cells[3].Value = "5";
                    dataGridView4.Rows[6].Cells[1].Value = "103";
                    dataGridView4.Rows[6].Cells[2].Value = "119";
                    dataGridView4.Rows[6].Cells[3].Value = "5";
                    dataGridView4.Rows[7].Cells[1].Value = "120";
                    dataGridView4.Rows[7].Cells[2].Value = "136";
                    dataGridView4.Rows[7].Cells[3].Value = "5";
                    dataGridView4.Rows[8].Cells[1].Value = "137";
                    dataGridView4.Rows[8].Cells[2].Value = "153";
                    dataGridView4.Rows[8].Cells[3].Value = "5";
                    dataGridView4.Rows[8].Cells[1].Value = "154";
                    dataGridView4.Rows[8].Cells[2].Value = "173";
                    dataGridView4.Rows[8].Cells[3].Value = "5";
                    this.comboBox7.Items.AddRange(new object[] { "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10" });
                    break;
            }
        }
        //测试
        private void button50_Click(object sender, EventArgs e)
        {
            int spacetime = 0;
            try
            {
                if (comboBox3.Text != "")
                {
                    spacetime = Convert.ToInt32(comboBox3.Text.Trim());
                }
                else
                {
                    spacetime = 9;
                }
                if (checkBox3.Checked == true)
                {
                    for (int i = 1; i <= Convert.ToInt32(comboBox2.Text); i++)
                    {
                        m_SerialPort.Send(cp.TestofThrowd());
                        comuse.Delay(spacetime);
                    }
                }
                else
                {
                    for (int i = 1; i <= Convert.ToInt32(comboBox2.Text); i++)
                    {
                        m_SerialPort.Send(cp.Test());
                        comuse.Delay(spacetime);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

        }
        //关闭串口
        private void button41_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SerialPort.IsOpen)
                {
                    m_SerialPort.Stop();
                }
                else
                {
                    MessageBox.Show("串口已关闭");
                }
                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //计算CV值均值等
        private void button53_Click(object sender, EventArgs e)
        {
            string reference_peak = string.Empty;
            int peak_num = Convert.ToInt32(comboBox6.Text.Trim());//峰个数
            int line_num = dataGridView1.ColumnCount;//曲线个数
            if(comboBox7.Text.Trim()!="")
            {
                reference_peak = comboBox7.Text.Trim();   
            }
            else
            {
                MessageBox.Show("请选择基准峰");
                return;
            }
            List<string[]> cal_result = new List<string[]>();
            cal_result = comuse.GetDataToCal(dataGridView1, dataGridView4, reference_peak, "280");
            comuse.AddMaxAndIndexToDgv(cal_result, dataGridView6, peak_num, line_num);
            comuse.AddDataToDgv(comboBox8.Text.Trim(), cal_result, dataGridView2, peak_num, line_num,reference_peak);
            comuse.MenStaCVDgvGetdata(cal_result, comboBox8.Text.Trim(),dataGridView3,peak_num,line_num,reference_peak);
        }
       //删除指定列
        private void button48_Click(object sender, EventArgs e)
        {
            List<string[]> data = comuse.GetValueToList(dataGridView1);
            int index = dataGridView1.CurrentCell.ColumnIndex;
            data.RemoveAt(index);
            dataGridView1.Columns.Clear();
            chart1.Series.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                comuse.DataGridViewGetValue(data[i], dataGridView1);
                comuse.DrawLine(data[i], Color.Red, chart1);
            }
            string reference_peak = string.Empty;
            int peak_num = Convert.ToInt32(comboBox6.Text.Trim());//峰个数
            int line_num = dataGridView1.ColumnCount;//曲线个数
            if (comboBox7.Text.Trim() != "")
            {
                reference_peak = comboBox7.Text.Trim();
            }
            else
            {
                MessageBox.Show("请选择基准峰");
                return;
            }
            List<string[]> cal_result = new List<string[]>();
            cal_result = comuse.GetDataToCal(dataGridView1, dataGridView4, reference_peak, "280");
            comuse.AddMaxAndIndexToDgv(cal_result, dataGridView6, peak_num, line_num);
            comuse.AddDataToDgv(comboBox8.Text.Trim(), cal_result, dataGridView2, peak_num, line_num, reference_peak);
            comuse.MenStaCVDgvGetdata(cal_result, comboBox8.Text.Trim(), dataGridView3, peak_num, line_num, reference_peak);
        }
        //粘贴键
        private void button47_Click(object sender, EventArgs e)
        {
            comuse.PasteData(dataGridView1); 
        }
        //计算键
        private void button49_Click(object sender, EventArgs e)
        {
            List<string[]> data=comuse.GetValueToList(dataGridView1);
            dataGridView1.Columns.Clear();
            chart1.Series.Clear();
            for(int i=data.Count-2;i<data.Count-1;i++)
            {
                comuse.DataGridViewGetValue(data[i], dataGridView1);
                comuse.DrawLine(data[i], Color.Red, chart1);
            }
            string reference_peak = string.Empty;
            int peak_num = Convert.ToInt32(comboBox6.Text.Trim());//峰个数
            int line_num = dataGridView1.ColumnCount;//曲线个数
            if (comboBox7.Text.Trim() != "")
            {
                reference_peak = comboBox7.Text.Trim();
            }
            else
            {
                MessageBox.Show("请选择基准峰");
                return;
            }
            List<string[]> cal_result = new List<string[]>();
            cal_result = comuse.GetDataToCal(dataGridView1, dataGridView4, reference_peak, "280");
            comuse.AddMaxAndIndexToDgv(cal_result, dataGridView6, peak_num, line_num);
            comuse.AddDataToDgv(comboBox8.Text.Trim(), cal_result, dataGridView2, peak_num, line_num, reference_peak);
            comuse.MenStaCVDgvGetdata(cal_result, comboBox8.Text.Trim(), dataGridView3, peak_num, line_num, reference_peak);
        }

        
       
    }
}
