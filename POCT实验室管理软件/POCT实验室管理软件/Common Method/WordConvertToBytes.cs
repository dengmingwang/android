using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 干式荧光免疫实验管理软件.Common_Method
{
    class WordConvertToBytes
    {
        //汉字转字节数组最后转为字符串（utf8编码转GB2312编码）
        public byte[] StringToBytes(string TheString) 
        {
            string convertstr = string.Empty;
            Encoding FromEcoding = Encoding.GetEncoding("UTF-8");//UTF8编码
            Encoding ToEcoding = Encoding.GetEncoding("gb2312");//GB2312编码
            byte[] FromBytes = FromEcoding.GetBytes(TheString);//获取汉字的UTF8字节序列
            byte[] Tobytes = Encoding.Convert(FromEcoding, ToEcoding, FromBytes);//转换为GB2312字节码
            //foreach (byte Mybyte in Tobytes)
            //{
            //    string str = Mybyte.ToString("x").ToUpper();
            //    convertstr += "0x" + (str.Length == 1 ? "0" + str : str) + " ";
            //}
            return Tobytes;
        }
        //字节数组转换为汉字
        public string BytesToString(byte[] Bytes)
        {
            string Mystring;
            Encoding FromEcoding = Encoding.GetEncoding("gb2312");
            Encoding ToEcoding = Encoding.GetEncoding("UTF-8");
            byte[] Tobytes = Encoding.Convert(FromEcoding, ToEcoding, Bytes);
            Mystring = ToEcoding.GetString(Tobytes);
            return Mystring;
        }
        //接收到的数据转换回汉字
        public string Convert2Word(string  buffer)
        {
            byte[] data = new byte[buffer.Length / 2];
            string convertword = string.Empty;
            try
            {                
                for (int i=0;i<buffer.Length/2;i++)
                {
                    data[i] = Convert.ToByte(buffer.Substring(i * 2, 2), 16);
                }
                convertword = BytesToString(data);
            }
            catch(Exception ex)
            {
                MessageBox.Show("数据转换错误" + ex.ToString());
            }
            return convertword;
        }
    }
}
