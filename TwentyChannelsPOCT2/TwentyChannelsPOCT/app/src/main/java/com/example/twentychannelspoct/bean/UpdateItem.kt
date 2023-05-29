package com.example.twentychannelspoct.bean

/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author
 *创建时间：2023/1/6  9:04
 *用途: 数据上传子项目，用于将测试结果的每一个子项目上传到后台服务。
 ***************************
 */

data class UpdateItem(val jigoumingcheng :String, //检测机构、仪器类型、仪器编号、批次名称
                      val yiqileixing: String,
                      val yiqibianhao: String,
                      val picimingcheng:String,
                      val zhengduanbumen:String,   //诊断部门、操作员、详细地址
                      val caozuoyuan:String,
                      val xiangxidizhi:String,

                      val xiangmumingcheng:String, //项目名称、子项目、样本类型、样本号、流水号、条码
                      val zixiangmu:String,
                      val yangbenleixing:String,
                      val yangbenhao: String,
                      val liushuihao:String,
                      val tiaoma:String,

                      val xingming:String, //姓名、年龄、性别
                      val nianling: String,
                      val xingbie:String,

                      val jiancejieguo:String, //检测结果、反应值、单位、测试范围、参考范围、结论、时间
                      val fanyingzhi:String,
                      val danwei:String,
                      val ceshifanwei:String,
                      val cankaofenwei:String,
                      val jielun:String,
                      val shijian:String,

                      val fengzhi:String,    //峰值、C值、t1值、检测电压值
                      val c:String,
                      val t1:String,
                      val jiancedianyazhi:String)