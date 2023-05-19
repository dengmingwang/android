using POCT实验室管理软件.Common_Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 干式荧光免疫实验管理软件.Common_Method
{
    class Order
    {
        WordConvertToBytes convert = new WordConvertToBytes();
        Common_use comuse = new Common_use();
        #region 项目名称及子项目数量1
        public byte[] ItemName(string str1, string  str2)
        {
            List<byte> order_byte = new List<byte>();
            if (str1 != "" && str2 != "")
            {
                byte[] itemname = convert.StringToBytes(str1);
               
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x01);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);//[
                for(int i=0;i<itemname.Length;i++)
                {
                    order_byte.Add(itemname[i]);
                }
                order_byte.Add(0x5D);//]
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(str2));//子项目个数
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if(i!=3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x02; order[3] = 0x00;order[4] = 0xFF;
                return order;
            }           
        }
        #endregion
        #region 预读时间以及预读阈值2
        public byte[] Pre_readtime(string str1,string str2)
        {
            List<byte> order_byte = new List<byte>();
            if(str1!=""&&str2!="")
            {           
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x02);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str1).ToString("X4").Substring(0, 2), 16));
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str1).ToString("X4").Substring(2, 2), 16));
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str2).ToString("X4").Substring(0, 2), 16));
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str2).ToString("X4").Substring(2, 2), 16));
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x02; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }   
        }
        #endregion
        #region 测试时间3
        public byte[] Testtime(string str)
        {
            List<byte> order_byte = new List<byte>();
            if(str!="")
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x03);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str).ToString("X4").Substring(0, 2), 16));
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str).ToString("X4").Substring(2, 2), 16));
                order_byte.Add(0x5D);            
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x03; order[3] = 0x00; order[4] = 0xFF;
                return order;
            } 
        }
        #endregion
        #region 峰个数4
        public byte[] Peak_num(string str)
        {
            List<byte> order_byte = new List<byte>();
            if(str!="")
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x04);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(str));
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x04; order[3] = 0x00; order[4] = 0xFF;
                return order;
            } 
        }
        #endregion
        #region 二次缓冲液量以及二次混合液量5
        public byte[] Secondary_mixandbuffer(string str1,string str2)
        {
            List<byte> order_byte = new List<byte>();
            if (str1 != "" && str2 != "")
            {
                
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x05);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str1).ToString("X4").Substring(0, 2), 16));
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str1).ToString("X4").Substring(2, 2), 16));
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str2).ToString("X4").Substring(2, 2), 16));
                order_byte.Add(Convert.ToByte(Convert.ToInt32(str2).ToString("X4").Substring(2, 2), 16));
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x05; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }   
        }
        #endregion    
        #region 峰序号6
        public byte[] Peaknumber(string str_OR)
        {
            List<byte> order_byte = new List<byte>();
            if (str_OR != "")
            {             
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x06);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
             
                for (int l = 0; l < str_OR.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(str_OR[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x06; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 基准峰7
        public byte[] Referencepeak(string str_OR)
        {
            List<byte> order_byte = new List<byte>();
            if (str_OR != "")
            {
                string str = "[" + str_OR + "]";
                string lenofbyete = System.Text.Encoding.Default.GetByteCount(str).ToString("X2");
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x07);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = Encoding.Default.GetBytes(str_OR);
                for (int l = 0; l < str_OR.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(str_OR[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x07; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 项目批号8
        public byte[] Project_batchnum(TextBox text)
        {
            List<byte> order_byte = new List<byte>();
            if (text.Text != "")
            {
                string str = "[" + text.Text + "]";
                string lenofbyete = System.Text.Encoding.Default.GetByteCount(str).ToString("X2");
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x08);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);              
                for (int l = 0; l < text.Text.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(text.Text[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x08; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 使用区域9
        public byte[] UseArea(ComboBox comb)
        {
            List<byte> order_byte = new List<byte>();
            if (comb.Text != "")
            {                              
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x09);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                string[] prefix=comb.Text.Split('-');
                for (int l = 0; l < prefix[0].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(prefix[0][l]));
                }
                order_byte.Add(Convert.ToByte('-'));
                byte[] itemname = convert.StringToBytes(comb.Text.Substring(2, comb.Text.Trim().Length-2));
                for (int i = 0; i < itemname.Length; i++)
                {
                    order_byte.Add(itemname[i]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x09; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion       
        #region 生产年A
        public byte[] YearofProdution(TextBox text)
        {
            List<byte> order_byte = new List<byte>();
            if (text.Text != "")
            {
                string str = "[" + text.Text.Trim() + "]";
                string lenofbyete = System.Text.Encoding.Default.GetByteCount(str).ToString("X2");
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x0A);
                order_byte.Add(Convert.ToByte(lenofbyete, 16));//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = Encoding.Default.GetBytes(text.Text.Trim());
                for (int l = 0; l < bytes.Length; l++)
                {
                    order_byte.Add(bytes[l]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x0A; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 生产月B
        public byte[] MonthofProdution(ComboBox text)
        {
            List<byte> order_byte = new List<byte>();
            if (text.Text != "")
            {
                string str = "[" + text.Text.Trim() + "]";
                string lenofbyete = System.Text.Encoding.Default.GetByteCount(str).ToString("X2");
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x0B);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = Encoding.Default.GetBytes(text.Text.Trim());
                for (int l = 0; l < bytes.Length; l++)
                {
                    order_byte.Add(bytes[l]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x0B; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 生产批次C
        public byte[] Batch(ComboBox comb)
        {
            List<byte> order_byte = new List<byte>();
            if (comb.Text != "")
            {              
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x0C);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(comb.Text));
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x0C; order[3] = 0x00; order[4] = 0xFF;
                return order;
            } 
        }
        #endregion
        #region 有效月数D
        public byte[] ValidMonths(TextBox str)
        {
            List<byte> order_byte = new List<byte>();
            if (str.Text != "")
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x0D);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(str.Text);
                for (int i = 0; i < bytes.Length; i++)
                {
                    order_byte.Add(bytes[i]);
                }                
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x0D; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 条形码E
        public byte[] Dataman(TextBox text)
        {         
            List<byte> order_byte = new List<byte>();
            if (text.Text != "")
            {
                string str = "[" + text.Text + "]";
                string lenofbyete = System.Text.Encoding.Default.GetByteCount(str).ToString("X2");
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x0E);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                for (int l = 0; l < text.Text.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(text.Text[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x0E; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 取峰算法F
        public byte[] CalPeak(string str_or)
        {
            List<byte> order_byte = new List<byte>();
            if (str_or != "")
            {
               
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x0F);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                string[] strarry = str_or.Split('.');
                for (int i = 0; i < strarry.Length; i++)
                {
                    if(i==0)
                    {
                        order_byte.Add(Convert.ToByte(strarry[i]));
                        order_byte.Add(Convert.ToByte('.'));
                    }
                    else
                    {
                        byte[] bytes = convert.StringToBytes(strarry[i]);
                        for(int j=0;j<bytes.Length;j++)
                        {
                            order_byte.Add(bytes[j]);
                        }
                    }
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x0F; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }

        }
        #endregion
        #region 子项目名称11
        public byte[] NameofSubitem(string[] strArray,int i)
        {
            List<byte> order_byte = new List<byte>();
            if (strArray.Length>0)
            {
                string str = "[" + (i+1).ToString() + "]" + "[" + strArray[4] + "]";
                string numofsubitem = "[" + (i + 1).ToString("X2") + "]";//子项序号
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x11);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                for (int j = 0; j < strArray[4].Length; j++)
                {
                    if(comuse.isChinese(strArray[4][j].ToString()))
                    {
                        byte[] nameofsubitem = convert.StringToBytes(strArray[4][j].ToString());//名称
                        for (int k = 0; k < nameofsubitem.Length; k++)
                        {
                            order_byte.Add(nameofsubitem[k]);
                        }
                    }
                    else
                    {
                        order_byte.Add(Convert.ToByte(strArray[4][j]));
                    }
                }
                      
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                for (int l = 0; l < strArray[1].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(strArray[1][l]));//范围小数位
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((i+1).ToString());//子项序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                } 
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x11; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 子项目参考范围12
        public byte[] LimitsofSubitem(string[] strArray,int i)
        {
            List<byte> order_byte = new List<byte>();
            if (strArray[6] != "" && strArray[7] != "")
            {            
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x12);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((i + 1).ToString());//子项序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                //范围小值
                for (int j = 0; j < strArray[6].Length;j++)
                {
                    order_byte.Add(Convert.ToByte(strArray[6][j]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                //范围大值
                for (int j = 0; j < strArray[7].Length; j++)
                {
                    order_byte.Add(Convert.ToByte(strArray[7][j]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if(j!=3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x12; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 子项目单位13
        public byte[] UnitofSubitem(string[] strArray,int i)
        {
            List<byte> order_byte = new List<byte>();
            if (strArray.Length > 0)
            {
                string str = "[" + (i + 1).ToString() + "]" + "[" + strArray[5] + "]";
                string lenofbyte = System.Text.Encoding.Default.GetByteCount(str).ToString("X2");
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x13);
                order_byte.Add(Convert.ToByte(lenofbyte, 16));//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((i + 1).ToString());//子项序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                byte[] bytes1 = Encoding.Default.GetBytes(strArray[5]);
                for (int l = 0; l < bytes1.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(bytes1[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x13; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 子项目计算参数P1P2P3 14
        public byte[] P1P2P3(string[] strArry,int num)
        {
            List<byte> order_byte = new List<byte>();
            if (strArry[8] != "" )
            {                         
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x14);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((num + 1).ToString());//子项序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);               
                for (int i = 0; i < strArry[8].Length; i++)
                {
                    order_byte.Add(Convert.ToByte(strArry[8][i]));//P1
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                for (int j = 0; j < strArry[9].Length; j++)
                {
                    order_byte.Add(Convert.ToByte(strArry[9][j]));//P2
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                for (int j = 0; j < strArry[10].Length; j++)
                {
                    order_byte.Add(Convert.ToByte(strArry[10][j]));//P3
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x14; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 子项目T/C公式以及双TC功能 15
        public byte[] TCformulaandDoubleTC(string[] strArry,int num)
        {
            List<byte> order_byte = new List<byte>();
            if(strArry[11]!=""&&strArry[12]!="")
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x15);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((num + 1).ToString());//子项序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                byte[] bytes1 = Encoding.Default.GetBytes(strArry[11]);
                for (int i = 0; i < bytes1.Length; i++)
                {
                    order_byte.Add(Convert.ToByte(bytes1[i]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                switch(strArry[12])
                {
                    case"0-禁用":
                        order_byte.Add(Convert.ToByte(0.ToString()));
                        break;
                    case"1-TC1为界分点":
                        order_byte.Add(Convert.ToByte(1.ToString()));
                        break;
                    case "2-TC2为界分点":
                        order_byte.Add(Convert.ToByte(2.ToString()));
                        break;
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x15; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 样本类型、系数/分界值、TC1、TC2 16
        public byte[] SamCoffiTC1TC2(string str1,string str2,string str3,string str4,int num)
        {
            List<byte> order_byte = new List<byte>();
           if(str1!="")
           {
               order_byte.Add(0xEE);
               order_byte.Add(0x23);
               order_byte.Add(0x16);
               order_byte.Add(0x00);//字节长度

               order_byte.Add(0x5B);
               byte[] bytes = System.Text.Encoding.ASCII.GetBytes((num + 1).ToString());//子项序号
               for (int j = 0; j < bytes.Length; j++)
               {
                   order_byte.Add(bytes[j]);
               }
               order_byte.Add(0x5D);

               order_byte.Add(0x5B);
               byte[] samname = convert.StringToBytes(str1);
               for (int i = 0; i < samname.Length; i++)
               {
                   order_byte.Add(samname[i]);//样本类型
               }
               order_byte.Add(0x5D);

               order_byte.Add(0x5B);
               order_byte.Add(Convert.ToByte(Convert.ToInt32(
                   str2).ToString("X2"), 16));//样本系数
               order_byte.Add(0x5D);

               order_byte.Add(0x5B);
               if (str3 == null)
               {
                   order_byte.Add(Convert.ToByte(1.ToString()));
               }
               else
               {
                   order_byte.Add(Convert.ToByte(Convert.ToInt32(
                  str3).ToString("X2"), 16));//TC1
               }
               order_byte.Add(0x5D);

               order_byte.Add(0x5B);
               if (str3 == null)
               {
                   order_byte.Add(Convert.ToByte(1.ToString("X2"), 16));
               }
               else
               {
                   order_byte.Add(Convert.ToByte(Convert.ToInt32(
                  str4).ToString("X2"), 16));//TC2
               }
               order_byte.Add(0x5D);

               order_byte.Add(0xFF);
               byte[] order = new byte[order_byte.Count];
               for (int i = 0; i < order.Length; i++)
               {
                   if (i != 3)
                   {
                       order[i] = order_byte[i];
                   }
                   else
                   {
                       order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                   }
               }
               return order;
           }
           else
           {
               byte[] order = new byte[6];
               order[0] = 0xEE; order[1] = 0x23; order[2] = 0x16; order[3] = 0x00; order[4] = 0xFF;
               return order;
           }
        }
        #endregion
        #region 拟合曲线浓度值以及浓度值小数位数 17
        public byte[] Potency(List<string> potency,string num,int number)
        {
            List<byte> order_byte = new List<byte>();
            if(potency.Count!=0)
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x17);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((number + 1).ToString());//曲线序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(
                  num).ToString("X2"),16));//小数位数
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                for (int i = 0; i < potency.Count; i++)
                {
                    for(int j=0;j<potency[i].Length;j++)
                    {
                        order_byte.Add(Convert.ToByte(potency[i][j]));
                    }
                    order_byte.Add(Convert.ToByte(' '));                     
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x17; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 拟合曲线反应值及反应值小数位数 18
        public byte[] Reponse(List<string> reponse,string num,int number)
        {
            List<byte> order_byte = new List<byte>();
           if(reponse.Count!=0)
           {
               order_byte.Add(0xEE);
               order_byte.Add(0x23);
               order_byte.Add(0x18);
               order_byte.Add(0x00);//字节长度
               order_byte.Add(0x5B);
               byte[] bytes = System.Text.Encoding.ASCII.GetBytes((number + 1).ToString());//曲线序号
               for (int j = 0; j < bytes.Length; j++)
               {
                   order_byte.Add(bytes[j]);
               }
               order_byte.Add(0x5D);
               order_byte.Add(0x5B);
               order_byte.Add(Convert.ToByte(Convert.ToInt32(
                 num).ToString("X2"), 16));//小数位数
               order_byte.Add(0x5D);
               order_byte.Add(0x5B);
               for (int i = 0; i < reponse.Count; i++)
               {
                   for (int j = 0; j < reponse[i].Length; j++)
                   {
                       order_byte.Add(Convert.ToByte(reponse[i][j]));
                   }
                   order_byte.Add(Convert.ToByte(' '));
               }
               order_byte.Add(0x5D);
               order_byte.Add(0xFF);
               byte[] order = new byte[order_byte.Count];
               for (int j = 0; j < order.Length; j++)
               {
                   if (j != 3)
                   {
                       order[j] = order_byte[j];
                   }
                   else
                   {
                       order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                   }
               }
               return order;
           }
           else
           {
               byte[] order = new byte[6];
               order[0] = 0xEE; order[1] = 0x23; order[2] = 0x18; order[3] = 0x00; order[4] = 0xFF;
               return order;
           }
        }
        #endregion
        #region 拟合曲线计算浓度及小数位数默认后4位 19
        public byte[] Cal_Potency(List<string> calpotency,string num,int number)
        {
            List<byte> order_byte = new List<byte>();
            if(calpotency.Count!=0)
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x19);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((number + 1).ToString());//曲线序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(
                  num).ToString("X2"), 16));//小数位数
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                for (int i = 0; i < calpotency.Count; i++)
                {
                    for (int j = 0; j < calpotency[i].Length; j++)
                    {
                        order_byte.Add(Convert.ToByte(calpotency[i][j]));
                    }
                    order_byte.Add(Convert.ToByte(' '));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x19; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 拟合曲线偏差及小数位数默认后4位 1A
        public byte[] Std(List<string> std,string num,int number)
        {
            List<byte> order_byte = new List<byte>();
            if(std.Count!=0)
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x1A);
                order_byte.Add(0x00);
                order_byte.Add(0x5B);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes((number + 1).ToString());//曲线序号
                for (int j = 0; j < bytes.Length; j++)
                {
                    order_byte.Add(bytes[j]);
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(
                  num).ToString("X2"), 16));//小数位数
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                for (int i = 0; i < std.Count; i++)
                {
                    for (int j = 0; j < std[i].Length; j++)
                    {
                        order_byte.Add(Convert.ToByte(std[i][j])); 
                    }
                    order_byte.Add(Convert.ToByte(' '));
                }
                order_byte.Add(0x5D);

                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x1A; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 拟合曲线浓度值以及反应值变换方法以及拟合算法 1B
        public byte[] FitLine(string str1,string str2,string str3,int number)
        {
            if(str1!=""&&str2!="")
            {
                List<byte> order_byte = new List<byte>();
                int numchange = 0;
                int numchange1 = 0;
                int fitmethod = 0;
                switch (str1)
                {
                    case "无变换":
                        numchange = 0;
                        break;
                    case "取对数":
                        numchange = 1;
                        break;
                    case "自然对数":
                        numchange = 2;
                        break;
                    case "底为2对数":
                        numchange = 3;
                        break;
                }
                switch (str2)
                {
                    case "无变换":
                        numchange1 = 0;
                        break;
                    case "取对数":
                        numchange1 = 1;
                        break;
                    case "自然对数":
                        numchange1 = 2;
                        break;
                    case "底为2对数":
                        numchange1 = 3;
                        break;
                }
                switch (str3)
                {
                    case "多项式拟合":
                        fitmethod = 0;
                        break;
                    case "指数拟合":
                        fitmethod = 1;
                        break;
                    case "对数拟合":
                        fitmethod = 2;
                        break;
                    case "幂函数拟合":
                        fitmethod = 3;
                        break;
                    case "直线拟合":
                        fitmethod = 4;
                        break;
                }
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x1B);
                order_byte.Add(0x00);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte((number + 1).ToString("X2"),16));//曲线序号0
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(numchange.ToString("X2"), 16));//浓度1
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(numchange1.ToString("X2"), 16));//反应值2
                order_byte.Add(0x5D);
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(fitmethod.ToString("X2"), 16));//拟合算法3
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x1B; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 峰值位置以及取样点数 1C
        public byte[] PeakPosAndNum(string[] strArry)
        {
            List<byte> order_byte = new List<byte>();
            if(strArry.Length!=0)
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x1C);
                order_byte.Add(0x00);

                order_byte.Add(0x5B);
                for (int l = 0; l < strArry[0].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(strArry[0][l]));//峰序号
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                for (int l = 0; l < strArry[1].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(strArry[1][l]));//起点
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                for (int l = 0; l < strArry[2].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(strArry[2][l]));//终点
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                for (int l = 0; l < strArry[3].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(strArry[3][l]));//点数
                }
                order_byte.Add(0x5D);

                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x1C; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion 
        #region 组合输出 1D
        public byte[] Group(string[] strArry)
        {
            List<byte> order_byte = new List<byte>();
            if(strArry.Length!=0)
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x1D);
                order_byte.Add(0x00);

                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte((Convert.ToInt32(strArry[0]) + 1).ToString("X2"), 16));//曲线序号0
                order_byte.Add(0x5D);
     
                order_byte.Add(0x5B);
                for (int i = 0; i < strArry[1].Length; i++)
                {
                    order_byte.Add(Convert.ToByte(strArry[1][i]));//组合名称1
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                byte[] bytes = (convert.StringToBytes(strArry[2]));//组合单位2
                for (int l = 0; l < bytes.Length; l++)
                {
                    order_byte.Add(bytes[l]);
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                //组合输出小数点后位数
                order_byte.Add(Convert.ToByte(strArry[3]));
                order_byte.Add(0x5D);

                //范围小值
                order_byte.Add(0x5B);
                for (int i = 0; i < strArry[4].Length; i++)
                {
                    order_byte.Add(Convert.ToByte(strArry[4][i]));//组合名称
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                for (int i = 0; i < strArry[5].Length; i++)
                {
                    order_byte.Add(Convert.ToByte(strArry[5][i]));//组合名称
                }
                order_byte.Add(0x5D);

                order_byte.Add(0x5B);
                //范围小数点后位数
                order_byte.Add(Convert.ToByte(strArry[6]));
                order_byte.Add(0x5D);

                //v0常数项
                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(strArry[7]));
                order_byte.Add(0x5D);

                //计算公式
                order_byte.Add(0x5B);              
                for (int l = 0; l < strArry[8].Length; l++)
                {
                    order_byte.Add(Convert.ToByte(strArry[8][l]));
                }
                order_byte.Add(0x5D);

                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int j = 0; j < order.Length; j++)
                {
                    if (j != 3)
                    {
                        order[j] = order_byte[j];
                    }
                    else
                    {
                        order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x1D; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 样本类型、加样量、缓冲液量以及混合液量 1E
        public byte[] Sample(string[] strarry)
        {
            List<byte> order_byte = new List<byte>();
            if(strarry.Length!=0)
            {
                byte[] samname = convert.StringToBytes(strarry[0]);
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x1E);
                order_byte.Add(0x00);//字节长度

                order_byte.Add(0x5B);//[
                for (int i = 0; i < samname.Length; i++)
                {
                    order_byte.Add(samname[i]);//样本类型
                }
                order_byte.Add(0x5D);//]

                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(strarry[1]).ToString("X2"), 16));//加样量
                order_byte.Add(0x5D);//]

                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(strarry[2]).ToString("X2"), 16));//缓冲液量
                order_byte.Add(0x5D);//]

                order_byte.Add(0x5B);
                order_byte.Add(Convert.ToByte(Convert.ToInt32(strarry[3]).ToString("X2"), 16));//混合液量
                order_byte.Add(0x5D);//]

                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x1E; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }   
        }
        #endregion
        #region 拟合曲线个数 1F
        public byte[] LineNum(string str)
        {
            List<byte> order_byte = new List<byte>();
            if (str != "")
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x1F);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);

                for (int l = 0; l < str.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(str[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x06; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
        #region 仪器类型 20
        public byte[] DeviceType(string str)
        {
            List<byte> order_byte = new List<byte>();
            order_byte.Add(0xEE);
            order_byte.Add(0x23);
            order_byte.Add(0x20);
            order_byte.Add(0x00);
            order_byte.Add(0x5B);
            for(int i=0;i<str.Length;i++)
            {
                order_byte.Add(Convert.ToByte(str[i]));
            }
            order_byte.Add(0x5D);
            order_byte.Add(0xFF);
            byte[] order = new byte[order_byte.Count];
            for (int j = 0; j < order.Length; j++)
            {
                if (j != 3)
                {
                    order[j] = order_byte[j];
                }
                else
                {
                    order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                }
            }
            return order;
        }
        #endregion
        #region 项目代码21
        public byte[] Itemnumber(string str)
        {
            List<byte> order_byte = new List<byte>();
            order_byte.Add(0xEE);
            order_byte.Add(0x23);
            order_byte.Add(0x21);
            order_byte.Add(0x00);
            order_byte.Add(0x5B);
            for (int i = 0; i < str.Length; i++)
            {
                order_byte.Add(Convert.ToByte(str[i]));
            }
            order_byte.Add(0x5D);
            order_byte.Add(0xFF);
            byte[] order = new byte[order_byte.Count];
            for (int j = 0; j < order.Length; j++)
            {
                if (j != 3)
                {
                    order[j] = order_byte[j];
                }
                else
                {
                    order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                }
            }
            return order;
        }
        #endregion
        #region 系数小数位 22
        public byte[] SamTimes_de(string str,int num)
        {
            List<byte> order_byte = new List<byte>();
            order_byte.Add(0xEE);
            order_byte.Add(0x23);
            order_byte.Add(0x22);
            order_byte.Add(0x00);

            order_byte.Add(0x5B);
            for (int i = 0; i < num.ToString().Length; i++)
            {
                order_byte.Add(Convert.ToByte(num.ToString()));//子项序号
            }
            order_byte.Add(0x5D);

            order_byte.Add(0x5B);
            for (int i = 0; i < str.Length; i++)
            {
                order_byte.Add(Convert.ToByte(str[i]));//样本系数
            }
            order_byte.Add(0x5D);
            order_byte.Add(0xFF);
            byte[] order = new byte[order_byte.Count];
            for (int j = 0; j < order.Length; j++)
            {
                if (j != 3)
                {
                    order[j] = order_byte[j];
                }
                else
                {
                    order[j] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                }
            }
            return order;
        }
        #endregion
        #region 输出组合个数 23
        public byte[] GroupNum(string str)
        {
            List<byte> order_byte = new List<byte>();
            if (str != "")
            {
                order_byte.Add(0xEE);
                order_byte.Add(0x23);
                order_byte.Add(0x23);
                order_byte.Add(0x00);//字节长度
                order_byte.Add(0x5B);

                for (int l = 0; l < str.Length; l++)
                {
                    order_byte.Add(Convert.ToByte(str[l]));
                }
                order_byte.Add(0x5D);
                order_byte.Add(0xFF);
                byte[] order = new byte[order_byte.Count];
                for (int i = 0; i < order.Length; i++)
                {
                    if (i != 3)
                    {
                        order[i] = order_byte[i];
                    }
                    else
                    {
                        order[i] = Convert.ToByte((order.Length - 5).ToString("X2"), 16);
                    }
                }
                return order;
            }
            else
            {
                byte[] order = new byte[6];
                order[0] = 0xEE; order[1] = 0x23; order[2] = 0x06; order[3] = 0x00; order[4] = 0xFF;
                return order;
            }
        }
        #endregion
    }
}
