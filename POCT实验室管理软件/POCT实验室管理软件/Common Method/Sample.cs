using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCT实验室管理软件.Common_Method
{
    class Sample
    {
        public string samtype { get; set; }//样本类型
        public string samvalue { get; set; }//加样量
        public string buffer_value { get; set; }//缓冲液量
        public string mixture_value { get; set; }//混合液量
    }
}
