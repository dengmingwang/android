package com.example.twentychannelspoct.bean

/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author D
 *创建时间：2023/1/4  20:13
 *用途:卡槽状态信息
 ***************************
 */

enum class taskStatus(val type:Int) {
    WaitForCard(1),//等卡
//    WaitForCounter(2),//等待孵育
    WaitForTesting(3),//等待测试
//    Testing(4),//测试
    Complete(5)//完成
}
