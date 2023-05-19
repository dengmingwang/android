package com.example.twentychannelspoct.bean

import org.litepal.crud.LitePalSupport

/*
 ********************************
 * 项目名称：TwentyChannelsPOCT
 * @Author： D
 * 创建时间：2023/2/28 8:41
 * 用途：卡测试信息定义
 ********************************
*/


data class SaveItem (
 var index:String="",   //流水号、样本号、样本类型、条码值、
 val sampleID:String="",
 val sampleType:String="",
 val barCodeValue:String="",

 val projectName:String="", //项目名称、子项目名、浓度值、单位
 val ItemName:String="",
 val concentrationValue:String="",
 val unit:String="",

 val testTime:String="",  //测试时间、测试日期
 val testDay:String="",

 val name:String="", //年龄、性别、年纪、年纪单位
 val gender:String="",
 val age:String="",
 val ageUnit:String=""
): LitePalSupport()