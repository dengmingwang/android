using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace POCT实验室管理软件.Common_Method
{
    class ComPort
    {
        ///<summary>
        ///自动获取可用串口
        ///</summary>
        ///<returns></returns>
        public static int GetComNum()
        {
            int comNum = -1;
            string[] strArr = GetHarewareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            foreach (string s in strArr)
            {
                //Debug.WriteLine(s);
                if (s.Length >= 23 && s.Contains("CH340"))
                {
                    int start = s.IndexOf("(") + 3;
                    int end = s.IndexOf(")");
                    comNum = Convert.ToInt32(s.Substring(start + 1, end - start - 1));
                }
            }
            return comNum;
        }
        ///<summary>
        ///获取系统硬件设置
        ///</summary>
        ///<param name="hardType">设备类型</param>
        ///<param name="propKey">设备属性</param>
        ///<returns></returns>
        private static string[] GetHarewareInfo(HardwareEnum hardType, string propKey)
        {
            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity"))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties["Name"].Value != null && hardInfo.Properties["Name"].Value.ToString().Contains("(COM"))
                        {
                            string strComName = hardInfo.Properties["Name"].Value.ToString();
                            strs.Add(strComName);
                        }
                    }
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                strs = null;
            }
        }
        ///<summary>
        ///枚举win32 api
        ///</summary>
        public enum HardwareEnum
        {
            //硬件
            Win32_Processor,//CPU处理器
            Win32_PhysicalMemory,//物理内存
            Win32_Keyboard,//键盘
            Win32_PointingDevice,//点输入设备,包括鼠标
            Win32_FloppyDrive,//软盘驱动器
            Win32_DiskDrive,//硬盘驱动器
            Win32_CDROMDrive,//光盘驱动器
            Win32_BaseBoard,//主板
            Win32_BIOS,//BIOS芯片
            Win32_ParallelPort,//并口
            Win32_SerialPort,//串口
            Win32_SerialPortConfiguration,//串口配置
            Win32_SoundDevice,//多媒体设置，一般指声卡
            Win32_SystemSlot,//主板插槽（ISA&PCI&AGP）
            Win32_USBController,//USB控制器
            Win32_NetworkAdapter,//网络适配器
            Win32_NetworkAdapterConfiguration,//网络适配器设置
            Win32_Printer,//打印机
            Win32_PrinterConfiguration,//打印机设置
            Win32_PrintJob,//打印机任务
            Win32_TCPIPPrinterPort,//打印机端口
            Win32_POTSModem,//MODEM
            Win32_POTSModemToSerialPort,//MODEM端口
            Win32_DesktopMonitor,//显示器
            Win32_DisplayConfiguration,//显卡
            Win32_DisplayControllerConfiguration,//显卡设置
            Win32_VideoController,//显卡细节
            Win32_VideoSettings,//显卡支持的显示模式
            //操作系统
            Win32_TimeZone,//时区
            Win32_SystemDriver,//驱动程序
            Win32_DiskPartition,//磁盘分区
            Win32_LogicalDisk,//逻辑磁盘
            Win32_LogicalDiskToPartition,//逻辑磁盘所在分区及始末位置
            Win32_LogicalMemoryConfiguration,//逻辑内存配置
            Win32_PageFile,//系统页文件信息
            Win32_PageFileSetting,//页文件设置
            Win32_BootConfiguration,//系统启动设置
            Win32_ComputerSystem,//计算机信息简要
            Win32_OperatingSystem,//操作系统信息
            Win32_StartupCommand,//系统自动启动程序
            Win32_Service,//系统安装的服务
            Win32_Group,//系统管理组
            Win32_GroupUser,//系统组账号
            Win32_UserAccount,//用户账号
            Win32_Process,//系统进程
            Win32_Thread,//系统线程
            Win32_Share,//共享
            Win32_NetworkClient,//已安装的网络客户端
            Win32_NetworkProtocol,//已安装的网络协议
            Win32_PnPEntity,//所有的设备
        }
        public byte[] Test()
        {
            byte[] order = new byte[8];
            order[1] = 0xee; order[2] = 0x11;
            order[3] = 0x00; order[4] = 0x00;
            order[5] = 0x00; order[6] = 0x00; 
            order[7] = 0xff; //不卸载试纸条
            return order;
        }
        public byte[] TestofThrowd()
        {
            byte[] order = new byte[8];
            order[1] = 0xee; order[2] = 0x11;
            order[3] = 0x01; order[4] = 0x00;
            order[5] = 0x00; order[6] = 0x00; 
            order[7] = 0xff; //卸载试纸条
            return order;
        }
    }
}
