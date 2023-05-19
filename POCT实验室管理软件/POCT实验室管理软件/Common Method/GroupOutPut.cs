using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCT实验室管理软件.Common_Method
{
    class GroupOutPut
    {
        public string group_num { get; set; }//组合序号
        public string group_name { get; set; }//输出名称
        public string group_unit { get; set; }//计量单位
        public string group_decimalplace { get; set; }//小数位数
        public string group_min { get; set; }//范围小值
        public string group_max { get; set; }//范围大值
        public string rangedecimals { get; set; }//范围小数
        public string v0 { get; set; }//常数项V0
        public string calculationformula { get; set; }//计算公式
    }
}
