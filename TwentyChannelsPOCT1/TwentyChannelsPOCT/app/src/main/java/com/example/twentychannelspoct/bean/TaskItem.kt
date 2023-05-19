package com.example.twentychannelspoct.bean

/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author D
 *创建时间：2023/3/3  8:53
 *用途:任务执行是需要的参数
 *@param isHave :用来表示孔位是否有卡状态
 *@param sendHex: 待发送的HEX指令
 *@param counter: 倒计时计数器，用来对试剂卡的孵育时间进行计时。
 *@param id :试剂卡测试卡位ID
 *@param Status :当前卡槽的状态  2种：1检测完成，检测中
 *@param sampleId:样本ID
 *@param sampleType:样本类型
 ***************************
 */

class TaskItem (var id:Int,var counter:Int,var isHave:Boolean,
                var sendHex:String, var Status:taskStatus, var sampleId:String, var sampleType:String) {
}