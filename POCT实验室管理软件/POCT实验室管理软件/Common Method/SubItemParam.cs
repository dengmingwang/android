using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCT实验室管理软件.Common_Method
{
    class SubItemParam
    {
        public string segmentcount { get; set; }//分段数 0
        public string decimalplace { get; set; }//范围小数位 1
        public string coefficient_decimalplace { get; set; }//系数小数位 2
        public string subitem_num { get; set; }//子项的序号 3
        public string subitem_name { get; set; }//子项的名称 4
        public string subitem_unit { get; set; }//子项的单位 5
        public string subitem_max { get; set; }//子项的范围大 6
        public string subitem_min { get; set; }//子项的范围小 7
        public string subitem_P1 { get; set; }//子项的P1 8
        public string subitem_P2 { get; set; }//子项的P2 9
        public string subitem_P3 { get; set; }//子项的P3 10
        public string subitem_TCformula { get; set; }//子项的TC公式 11
        public string subitem_doubleTC { get; set; }//子项的双TC功能 12
    }
}
