using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 干式荧光免疫实验管理软件.Common_Method;

namespace POCT实验室管理软件.Common_Method
{
    class CreatLineOrder
    {
        Order or = new Order();
        public List<byte[]> GetLineOrder( List<LineImfo> lineimf)
        {
            List<byte[]> order = new List<byte[]>();
            for(int i=0;i<lineimf.Count;i++)
            {
                order.Add(or.Potency(lineimf[i].potency, lineimf[i].demicalplace_potency, i));//浓度 17
                order.Add(or.Reponse(lineimf[i].reponse, lineimf[i].deicalplace_reponse, i));//反应值 18           
                order.Add(or.FitLine(lineimf[i].method_potency, lineimf[i].method_reponse, lineimf[i].methodLinefit,i));//浓度值以及反应值变换方法以及拟合方法 1B
            }
            order.Add(or.LineNum(lineimf.Count.ToString()));//拟合曲线个数 1F
            return order;
        }
    }
}
