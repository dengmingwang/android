using System;

namespace POCT实验室管理软件
{
    public class SerialPortException:Exception
    {
        public SerialPortException(string message)
            : base(message)
        { 
        }
    }
}
