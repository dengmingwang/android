using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCT实验室管理软件.Common_Method
{
    class LineImfo
    {
        public string Linenum { get; set; }//曲线序号
        public List<string> potency { get; set; }//浓度
        public List<string> reponse { get; set; }//反应值
        public List<string> calpotency { get; set; }//计算浓度
        public List<string> Std { get; set; }//偏差
        public string result_linefit { get; set; }//拟合结果
        public string method_potency { get; set; }//浓度值变换方法
        public string method_reponse { get; set; }//反应值变换方法
        public string demicalplace_potency { get; set; }//浓度值小数位数
        public string deicalplace_reponse { get; set; }//反应值小数位数
        public string methodLinefit { get; set; }//拟合算法
        
    }
}
