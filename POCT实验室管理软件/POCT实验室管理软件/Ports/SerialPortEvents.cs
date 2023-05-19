using System;

namespace POCT实验室管理软件
{
    public class SerialPortEvents:EventArgs
    {
        /// <summary>
        /// 传递数据
        /// </summary>
        private byte[] m_bufferData;
        public byte[] BufferData
        {
            get { return m_bufferData; }
            set { m_bufferData = value; }
        }

        public SerialPortEvents()
            : base()
        { 
        }

        public SerialPortEvents(byte[] data)
            : base()
        {
            m_bufferData = data;
        }

    }
}
