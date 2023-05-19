using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCT实验室管理软件.Common_Method
{
    class itemparam
    {
        public string Itemname { get; set; }//项目名称
        public string Itemnum { get; set; }//项目代码
        public string devicetype { get; set; }//仪器类型
        public string subitemnum { get; set; }//子项目数
        public string pre_readtime { get; set; }//预读时间
        public string testtime { get; set; }//测试时间

        public string customsecddilution { get; set; }//自定义二次稀释
        public string judgeaddsam { get; set; }//判定未加样
        public string peakvalue_num{get;set;}//峰值个数
        public string secondary_buffer { get; set; }//二次缓冲液量
        public string judgeaddsam_value { get; set; }//判断是否加样的阈值
        public string referencepeak { get; set; }//基准峰
        public string secondary_mixture { get; set; }//二次混合液量
        public string peaknumber { get; set; }//峰序号
        public string methodofgetpeak { get; set; }//取峰算法     
    }
}
