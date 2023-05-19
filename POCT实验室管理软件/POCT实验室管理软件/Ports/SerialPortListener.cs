using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace POCT实验室管理软件
{
    /// <summary>
    /// 委托处理结果
    /// </summary>
    /// <param name="data"></param>
    public delegate void HandResult(object sender,SerialPortEvents e);

    /// <summary>
    /// 委托处理接收数据完成
    /// </summary>
    /// <param name="data"></param>
    public delegate void OnReceivedData(object sender,SerialPortEvents e);

    /// <summary>
    /// 发送数据回调
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void OnSendData(object sender,SerialPortEvents e);

    public class SerialPortListener
    {
        #region 成员变量
        /// <summary>
        /// 定义接收缓冲区数据
        /// </summary>
        private List<byte> m_buffer = new List<byte> ();

        public event HandResult SerialPortResult;

        public event OnReceivedData OnSerialPortReceived;

        public event OnSendData OnSeriaPortSend;

        delegate void AsyncCallResult(byte[] data);

        delegate void AsyncSend(byte[] data);

        bool m_isReceiving = false;

        /// <summary>
        /// 串口对象
        /// </summary>
        SerialPort m_serialPort = null;

        //Thread m_threadMonitor = null;

        /// <summary>
        /// 结束符号
        /// </summary>
        char m_endChar = '\0';
        public char EndChar
        {
            get { return m_endChar; }
            set { m_endChar = value; }
        }

        /// <summary>
        /// 发送缓冲区大小
        /// </summary>
        int m_writeBufferSize = 1024;
        public int WriteBufferSize
        {
            get { return m_writeBufferSize; }
            set { m_writeBufferSize = value;}
        }

        /// <summary>
        /// 是否启用结束符
        /// </summary>
        bool m_useEndChar = true;


        /// <summary>
        /// 发送间隔时间
        /// </summary>
        int m_sendInterval = 100;

        public int SendInterval
        {
            get { return m_sendInterval; }
            set { m_sendInterval = value; }
        }

        /// <summary>
        /// 连续接收超时时间
        /// </summary>
        int m_receiveTimeout =1000;

        public int ReceiveTimeout
        {
            get { return m_receiveTimeout; }
            set { m_receiveTimeout = value; }
        }

        /// <summary>
        /// 最后接收时间
        /// </summary>
        DateTime m_lastReceiveTime;

        public DateTime LastReceiveTime
        {
            get { return m_lastReceiveTime; }
        }

        /// <summary>
        /// 缓冲区接收大小
        /// </summary>
        private int m_bufferSize = 1;

        public int BufferSize
        {
            get { return m_bufferSize; }
            set { m_bufferSize = value; }
        }

        /// <summary>
        /// 串口名称
        /// </summary>
        private string m_portName = "COM1";

        public string PortName
        {
            get { return m_portName; }
            set { m_portName = value; }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        private int m_baudRate = 9600;

        public int BaudRate
        {
            get { return m_baudRate; }
            set { m_baudRate = value; }
        }

        /// <summary>
        /// 停止位长度
        /// </summary>
        private StopBits m_stopBits = StopBits.None;

        public StopBits StopBits
        {
            get { return m_stopBits; }
            set { m_stopBits = value; }
        }

        /// <summary>
        /// 基偶校验位
        /// </summary>
        private Parity m_parity = Parity.None;

        public Parity Parity
        {
            get { return m_parity; }
            set { m_parity = value; }
        }

        /// <summary>
        /// 控制协议
        /// </summary>
        private Handshake m_handshake = Handshake.None;

        public Handshake Handshake
        {
            get { return m_handshake; }
            set { m_handshake = value; }
        }

        /// <summary>
        /// 标准数据长度
        /// </summary>
        private int m_dataBits = 8;

        public int DataBits
        {
            get { return m_dataBits; }
            set { m_dataBits = value; }
        }

        /// <summary>
        /// 缓冲区大小
        /// </summary>
        private int m_readBufferSize = 4;

        public int ReadBufferSize
        {
            get { return m_readBufferSize; }
            set { m_readBufferSize = value; }
        }

        /// <summary>
        /// 输入缓冲区中的字节数
        /// </summary>
        private int m_receivedBytesThreshold = 1;

        public int ReceivedBytesThreshold
        {
            get { return m_receivedBytesThreshold; }
            set { m_receivedBytesThreshold = value;}
        }

        /// <summary>
        /// 获取当前是否打开
        /// </summary>
        public bool IsOpen {
            get {
                return m_serialPort == null ? false : m_serialPort.IsOpen;
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        public SerialPortListener(string portName, int baudRate)
        {
            m_portName = portName;
            m_baudRate = baudRate;
            m_buffer = new List<byte>();
            m_serialPort = new SerialPort(portName, baudRate);
            m_serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }
        #endregion

        #region 开启关闭串口
        /// <summary>
        /// 开始接收
        /// </summary>
        public bool Start()
        {
            try
            {
                m_serialPort.Parity = m_parity;
                m_serialPort.StopBits = m_stopBits;
                m_serialPort.Handshake = m_handshake;
                m_serialPort.DataBits = m_dataBits;
                m_serialPort.ReadBufferSize = m_readBufferSize;
                m_serialPort.ReceivedBytesThreshold = m_receivedBytesThreshold;
                m_serialPort.WriteBufferSize = m_writeBufferSize;
                m_serialPort.Open();
                //m_threadMonitor = new Thread(new ThreadStart(SerialPortMonitor));
                //m_threadMonitor.IsBackground = true;
                //m_threadMonitor.Start();
                return true;
            }
            catch
            {
                throw new SerialPortException(string.Format("无法打开串口:{0}",m_serialPort.PortName));
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            //m_threadMonitor.Abort();
            //m_serialPort.Close();
            m_buffer.Clear();
            GC.Collect();
        }
        #endregion

        #region 接收数据
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void SerialPort_DataReceived(object sender, EventArgs e)
        //{
        //    StartReceive();
        //    int availCount = m_serialPort.BytesToRead;
        //    byte[] bs = new byte[availCount];
        //    bool bEnd = false;
        //    m_serialPort.Read(bs, 0, availCount);
        //    lock (m_buffer)
        //        m_buffer.AddRange(bs);
        //    if (m_buffer.Count == 762)
        //    {
        //        bEnd = true;
        //    }
        //    EndReceive(bs);
        //    if (bEnd)
        //        CallEndResult();
        //}

        void SerialPort_DataReceived(object sender, EventArgs e)
        {
            StartReceive();
            byte[] readbuffer = null;
            int n = m_serialPort.BytesToRead;
            byte[] buf = new byte[n];
            bool bEnd = false;
            m_serialPort.Read(buf, 0, n);
            //缓存数据
            m_buffer.AddRange(buf);
            //完整性判断
            while (m_buffer.Count >= 762)
            {
                if (m_buffer[0] == 0xee)
                {
                    int len = 760;
                    if (m_buffer.Count < len + 2)
                    {
                        break;
                    }
                    readbuffer = new byte[762];
                    m_buffer.CopyTo(0, readbuffer, 0, 762);
                    bEnd = true;
                    break;
                }
            }

            EndReceive(buf);
            if (bEnd)
                CallEndResult();
        }
        /// <summary>
        /// 开始接收
        /// </summary>
        void StartReceive()
        {
            if (!m_isReceiving)
                m_isReceiving = true;
        }

        /// <summary>
        /// 结束接收
        /// </summary>
        void EndReceive(byte[] bs)
        {
            //设置最后接收时间
            m_lastReceiveTime = DateTime.Now;
            m_isReceiving = false;
            if (OnSerialPortReceived != null
                &&bs!=null
                && bs.Length != 0)
            {
                AsyncCallResult call = new AsyncCallResult(CalReceived);
                call.BeginInvoke(bs, null, null); 
            }  
        }

        void CalReceived(byte[] data)
        {
            OnSerialPortReceived(this, new SerialPortEvents(data));
        }

        /// <summary>
        /// 读取结束监控
        /// </summary>
        void SerialPortMonitor()
        {
            if (!m_isReceiving)
            {
                if (DateTime.Now.Subtract(m_lastReceiveTime).TotalMilliseconds > m_receiveTimeout && m_buffer.Count != 0)
                {
                    CallEndResult();
                }
            }
            Thread.Sleep(50);
            var T = new Thread(delegate() { SerialPortMonitor(); }, 1073741824);
            T.Start();
        }

        /// <summary>
        /// 处理接收数据完成
        /// </summary>
        void CallEndResult()
        {
            byte[] result = CopyBuffer(true);
            if (result != null && result.Length >0)
            {
                if (SerialPortResult != null)
                {
                    //异步通知接收完成
                    AsyncCallResult call = new AsyncCallResult(CalResult);
                    call.BeginInvoke(result, null, null);
                }
            }
        }

        void CalResult(byte[] data)
        {
            SerialPortResult(this, new SerialPortEvents(data));   
        }

        byte[] CopyBuffer(bool isClean=false)
        {
            byte[] bs=null;
            lock(m_buffer)
            {
                bs = new byte[m_buffer.Count];
                m_buffer.CopyTo(0,bs,0,762);
                if (isClean)
                    m_buffer.Clear();
            }
            return bs;
        }
        #endregion

        #region 发送数据
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bs"></param>
        public void Send(byte[] bs)
        {
            if (!m_serialPort.IsOpen)
            {
                throw new SerialPortException("不能在串口关闭状态发送数据");
            }
            //异步发送
            AsyncSend call = new AsyncSend(DoAsyncSend);
            call.BeginInvoke(bs, null, null);
        }
        
        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="data"></param>
        void DoAsyncSend(byte[] data)
        {
            int s = 0;

            if (data == null || data.Length == 0)
                return;
            if (m_useEndChar)
            {
                Array.Resize<byte>(ref data, data.Length + 1);
                data[data.Length - 1] = BitConverter.GetBytes(m_endChar)[0];
            }
            if (data.Length > m_writeBufferSize)
            {
                //分包发送
                int sendCount = 0;
                while (sendCount < data.Length)
                {
                    byte[] sendBytes;
                    if (sendCount + m_writeBufferSize > data.Length)
                    {
                        sendBytes = new byte[data.Length - sendCount];
                        Array.Copy(data, sendCount == 0 ? 0 : sendCount - 1, sendBytes, 0, sendBytes.Length);
                        sendCount = data.Length;
                    }
                    else
                    {
                        sendBytes = new byte[m_writeBufferSize];
                        Array.Copy(data, sendCount == 0 ? 0 : sendCount - 1, sendBytes, 0, sendBytes.Length);
                        sendCount += m_writeBufferSize;
                    }
                    //发送数据
                    m_serialPort.Write(sendBytes, 0, sendBytes.Length);
                    s += sendBytes.Length;
                    if (OnSeriaPortSend != null)
                        OnSeriaPortSend(this, new SerialPortEvents(sendBytes));
                    Array.Resize<byte>(ref sendBytes, 0);
                    Thread.Sleep(m_sendInterval);
                }
            }
            else
            { 
                //一次发送
                m_serialPort.Write(data, 0, data.Length);
            }
            //清理数据
            Array.Clear(data, 0, data.Length);
            data = null;
        }
        #endregion
    }
}
