package com.example.twentychannelspoct.bean

import android.widget.CheckBox

/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Authord
 *创建时间：2023/2/28  8:35
 *用途:
 ***************************
 */

data class QueryBean(
    val checkBox: CheckBox,
    var index: String,
    val sampleID: String,
    val projectName: String,
    val concentrationValue: String,
    val testTime: String,  //测试时间、测试日期
    var page: String   /**页码显示区的占位符 */ )