using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCT实验室管理软件.MenuForm
{
    class BaseCom
    {
         public delegate void delegateOnShowMsg(string msg);
        public delegate void delegateOnOpenCom(bool onOff);
        public event delegateOnShowMsg OnShow;
        public event delegateOnOpenCom OnOpen;
        string baudRate = string.Empty;
        string parity = string.Empty;
        string stopBits = string.Empty;
        string dataBits = string.Empty;
        string portName = string.Empty;
        public System.IO.Ports.SerialPort comPort = new System.IO.Ports.SerialPort();
        public BaseCom(string portName, string baudRate, string stopBits, string dataBits, string parity)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.stopBits = stopBits;
            this.dataBits = dataBits;
            this.parity = parity;
        }      
        //打开和关闭串口
        public void OpenPort()
        {
            try
            {
                if (comPort.IsOpen)
                    comPort.Close();
                comPort.BaudRate = int.Parse(baudRate);
                comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                comPort.DataBits = int.Parse(dataBits);
                comPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                comPort.PortName = portName;
                comPort.Handshake = Handshake.None;
                comPort.Open();
                if (comPort.IsOpen)
                {
                    showOnoff(true);
                    comPort.Handshake = Handshake.None;
                    comPort.DataReceived += comPort_DataReceived;
                }
                MessageBox.Show(portName + " Open Success");
            }
            catch (Exception ex)
            {
                showMsg("Unable to Open " + portName);
                showMsg(ex.Message);
                showOnoff(false);
            }
        }
        public void ClosePort()
        {
            if (comPort.IsOpen)
            {
                comPort.Close();
                comPort.Dispose();
                showOnoff(false);               
            }
            MessageBox.Show(portName + " Close Success");
        }
        //接收数据
        public void comPort_DataReceived(object sender,SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = comPort.BytesToRead;
                if (n == 0)
                    return;
                byte[] buffer = new byte[n];
                comPort.Read(buffer, 0, n);
                string rcv = ToHexString(buffer);
                showMsg("RECV:" + rcv);
                buffer = new byte[n];
            }
            catch (Exception ex)
            {
                ClosePort();
                showMsg("Com Closed：" + ex.Message);
            }
        }
        public void comPort_SendData(byte[] data)
        {
            if (data == null)
            {
                showMsg("CAN Not EMPTY");
                return;
            }
            if (comPort == null)
            {
                showMsg("First Open Port");
                return;
            }
            if (!comPort.IsOpen)
            {
                showMsg("COM Not Open");
                return;
            }
            comPort.Write(data,0,data.Length);
        }
        //显示数据
        public void showMsg(string msg)
        {
            if (OnShow != null)
                OnShow(msg);
        }
        public void showOnoff(bool flag)
        {
            if (OnOpen != null)
                OnOpen(flag);
        }
        public string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }
    }
}
