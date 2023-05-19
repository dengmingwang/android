package com.example.twentychannelspoct.bean

import android.util.Log
import java.io.UnsupportedEncodingException
import java.nio.charset.Charset
import java.nio.charset.StandardCharsets


/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author D
 *创建时间：2023/2/28  8:30
 *用途:定义卡信息
 ***************************
 */

class IDReadBean (
    var companyName: String="",
    var projectName: String="",
    var numOfSubItem: String="",
    var nameOfSubItem: String="",
    var idOfSubItem: String="",
    var minOfReference: String="",
    var maxOfReference: String="",
    var minOfPoint: String="",
    /**最小值的小数点位置*/
    var maxOfPoint: String="",
    /**最大值的小数点位置*/
    var Unit: String="",
    var formulaTrans: String="",
    /**公式-数值变换*/
    var methodOfFitting: String="",
    /**公式-拟合方法*/

    var parasA: String="",
    var pointA: String="",

    var parasB: String="",
    var pointB: String="",

    var parasC: String = "",   //非必须，暂时先设置成可变变量
    var pointC: String = "",

    var softwareVersion: String="",
    var hardwareVersion: String="",
    var IDSerialNumber: String="",
    var dateOfManufacture: String="",
    var warrantDate: String="",
    var referencePeak: String="",
){
    /**
     * @Author BigHang
     * @Time 2022/11/23 9:37
     * @Description 用于解析下位机回传的ID卡的数据信息。 不采用字符分割的方式了，直接处理字节数组。
     * @param  ReceiveDate :收到的16进制字符串。
     */

    fun readIDCard(ReceiveDate: ByteArray) {
        val ReceiveFlag = "TAG"
        var TextType = 0 //文本类型变量   0：为数字， 1：为 中文。
        var DateLength = 0
        DateLength = ReceiveDate[3].toInt()
        Log.d("DateLength", DateLength.toString())
        if (ReceiveDate[DateLength + 3] == (0x5D).toByte()) {  //根据数据指定长度检测结束符是否为正确的”5D“ 即”]“
            if (ReceiveDate[2] == (0x01).toByte()) { //到公司名
                TextType = 1
                val buf = ByteArray(DateLength - 2) //数据内容字段：
                for (i in buf.indices) {
                    buf[i] = ReceiveDate[i + 5]
                }
                var companyNameStr = ""
                try {
                    if (TextType == 1) {
                        companyNameStr = String(buf, Charset.forName("gb2312"))
                    }
                } catch (e: UnsupportedEncodingException) {
                    e.printStackTrace()
                }
//                setCompanyName(companyname)
                this.companyName=companyNameStr
                Log.d(ReceiveFlag, companyNameStr)
            } else if (ReceiveDate[2] == (0x02).toByte()) { //项目名
                var ProjectName = ""
                var SubProjectNum: Int = ReceiveDate[DateLength + 2].toInt()
                if (SubProjectNum < 0) {
                    SubProjectNum = 256 + SubProjectNum
                }
//                setSubProjectNum(SubProjectNum.toString()) //设置子项目数
                this.numOfSubItem=SubProjectNum.toString()
                val buf1 = ByteArray(DateLength - 5) //数据内容字段：
                for (i in buf1.indices) {
                    buf1[i] = ReceiveDate[i + 5]
                }
                try {
                    ProjectName = String(buf1, Charset.forName("gb2312"))
                } catch (e: UnsupportedEncodingException) {
                    e.printStackTrace()
                }
//                setProjectName(ProjectName) //设置项目名
                this.projectName=ProjectName
            } else if (ReceiveDate[2] == (0x03).toByte()) { //子项目名
                var SubProjectName = ""
//                setSubItemId(ReceiveDate[5].toString()) //确定子项目代码
                this.idOfSubItem=ReceiveDate[5].toString()
                val buf1 = ByteArray(DateLength - 5)
                for (i in buf1.indices) {
                    buf1[i] = ReceiveDate[i + 8]
                }
                try {
                    SubProjectName = String(buf1,  Charset.forName("gb2312"))
                } catch (e: UnsupportedEncodingException) {
                    e.printStackTrace()
                }
//                setSubItemName(SubProjectName) //设置子项目名
                this.nameOfSubItem=SubProjectName
            } else if (ReceiveDate[2] == (0x04).toByte()) {  //参考范围     参考值数值范围都为2个字节， 即从0~65535  最小值是第8，9字节，最大值为12,13；
                var subItemId = 0
                var minValueS: Int
                var minValueE: Int //最小值高低位
                var maxValueS: Int
                var maxValueE: Int //最大值高低位
                var minValue = 0
                var maxValue = 0
                var miniPoint = 0
                var maxPoint = 0 //最小值和最大值的小数点位置。
                var a = 0
                for (i in ReceiveDate.indices) {
                    if (ReceiveDate[i] == (0x5B).toByte()) {
                        for (j in i + 1 until i + 4) {
                            if (ReceiveDate[j] == (0x5D).toByte()) {
                                a = a + 1
                                if (a == 1) {  //第一个[]为子项目代码id
                                    subItemId = ReceiveDate[j - 1].toInt()
                                    break
                                } else if (a == 2) { //取到第二个[] 中的数据 ：即最小值
                                    if (j - i == 3) {
                                        if (ReceiveDate[j - 2] < 0) {
                                            minValueS = 256 + ReceiveDate[j - 2]
                                        } else {
                                            minValueS = ReceiveDate[j - 2].toInt()
                                        }
                                        if (ReceiveDate[j - 1] < 0) {
                                            minValueE = 256 + ReceiveDate[j - 1]
                                        } else {
                                            minValueE = ReceiveDate[j - 1].toInt()
                                            Log.d("minValueE", minValueE.toString())
                                        }
                                        minValue = minValueS * 256 + minValueE
                                    } else if (j - i == 2) {
                                        minValue = ReceiveDate[j - 1].toInt()
                                    }
                                    break
                                } else if (a == 3) { //第三个[] ,即最小值的小数点
                                    miniPoint = ReceiveDate[j - 1].toInt()
                                    break
                                } else if (a == 4) { //第四个[] ,即最大值。
                                    if (j - i == 3) {
                                        if (ReceiveDate[j - 2] < 0) {
                                            maxValueS = 256 + ReceiveDate[j - 2]
                                            Log.d("maxValueE1111", maxValueS.toString())
                                        } else {
                                            maxValueS = ReceiveDate[j - 2].toInt()
                                            Log.d("maxValueE22222", maxValueS.toString())
                                        }
                                        if (ReceiveDate[j - 1] < 0) {
                                            maxValueE = 256 + ReceiveDate[j - 1]
                                        } else {
                                            maxValueE = ReceiveDate[j - 1].toInt()
                                        }
                                        maxValue = maxValueS * 256 + maxValueE
                                    } else if (j - i == 2) {
                                        maxValue = ReceiveDate[j - 1].toInt()
                                    }
                                    break
                                } else if (a == 5) { //第五个[] ,即最大值的小数点
                                    maxPoint = ReceiveDate[j - 1].toInt()
                                    break
                                }
                            }
                        }
                    }
                }
//                setMiniValue(minValue.toString())
                this.minOfReference=minValue.toString()
                Log.d("minValue2", minValue.toString())
//                setMinPoint(miniPoint.toString())
                this.minOfPoint=miniPoint.toString()
                Log.d("minPoint2", miniPoint.toString())


//                setMaxValue(maxValue.toString())
                this.maxOfReference=maxValue.toString()
                Log.d("maxValue2", maxValue.toString())

//                setMaxPoint(maxPoint.toString())
                this.maxOfPoint=maxPoint.toString()
            } else if (ReceiveDate[2] == (0x05).toByte()) { //单位
                val buf = ByteArray(1)
                buf[0] = ReceiveDate[5]
                val SubprojectId = String(buf, StandardCharsets.UTF_8)
                var UnitStr = ""
                val buf1 = ByteArray(DateLength - 5)
                for (i in buf1.indices) {
                    buf1[i] = ReceiveDate[i + 8]
                }
                try {
                    UnitStr = String(buf1,  Charset.forName("gb2312"))
                } catch (e: UnsupportedEncodingException) {
                    e.printStackTrace()
                }
//                setUnit(Unit)
                this.Unit=UnitStr.toString()
            } else if (ReceiveDate[2] == (0x06).toByte()) {  //公式  : 这里面得解析出7个参数：分别是 ①数值变换 ②拟合方法③参数 a,b 和a,b 的小数点位 ③子项目代码
                var subItemId3 = 0 //子项目id
                var numTransform = 0 // 数值变换
                var fittingmethod = 0 //拟合方式
                var AValue = 0 //参数 a；
                var aValueS = 0
                var aValueE = 0
                var BValue = 0 // 参数b;
                var bValueS = 0
                var bValueE = 0
                var paramsAPoint = 0 //参数的小数点位；
                var paramsBPoint = 0 //参数的小数点位；
                var a = 0
                for (i in ReceiveDate.indices) {
                    if (ReceiveDate[i] == (0x5B).toByte()) {
                        for (j in i + 1 until i + 4) {
                            if (ReceiveDate[j] == (0x5D).toByte()) {
                                a = a + 1
                                if (a == 1) {  //第一个[]为子项目代码id
                                    subItemId3 = ReceiveDate[j - 1].toInt()
                                    break
                                } else if (a == 2) {  //第二个[]数值变换方法
                                    numTransform = ReceiveDate[j - 1].toInt()
                                    break
                                } else if (a == 3) {  //第二个[]  拟合方法
                                    fittingmethod = ReceiveDate[j - 1].toInt()
                                    break
                                } else if (a == 4) { //取到第四个[]   参数a;
                                    if (j - i == 3) {
                                        if (ReceiveDate[j - 2] < 0) {
                                            aValueS = 256 + ReceiveDate[j - 2]
                                        } else {
                                            aValueS = ReceiveDate[j - 2].toInt()
                                        }
                                        if (ReceiveDate[j - 1] < 0) {
                                            aValueE = 256 + ReceiveDate[j - 1]
                                        } else {
                                            aValueE = ReceiveDate[j - 1].toInt()
                                            Log.d("minValueE", aValueE.toString())
                                        }
                                        AValue = aValueS * 256 + aValueE
                                    } else if (j - i == 2) {
                                        AValue = ReceiveDate[j - 1].toInt()
                                    }
                                    break
                                } else if (a == 5) { //第五个[] ,即第一个参数的小数点
                                    paramsAPoint = ReceiveDate[j - 1].toInt()
                                    break
                                } else if (a == 6) { //第6个[] ,即最大值。
                                    if (j - i == 3) {
                                        if (ReceiveDate[j - 2] < 0) {
                                            bValueS = 256 + ReceiveDate[j - 2]
                                            Log.d("maxValueE1111", bValueS.toString())
                                        } else {
                                            bValueS = ReceiveDate[j - 2].toInt()
                                            Log.d("maxValueE22222", bValueS.toString())
                                        }
                                        if (ReceiveDate[j - 1] < 0) {
                                            bValueE = 256 + ReceiveDate[j - 1]
                                        } else {
                                            bValueE = ReceiveDate[j - 1].toInt()
                                        }
                                        BValue = bValueS * 256 + bValueE
                                    } else if (j - i == 2) {
                                        BValue = ReceiveDate[j - 1].toInt()
                                    }
                                    break
                                } else if (a == 7) { //第7个[] ,即最大值的小数点
                                    paramsBPoint = ReceiveDate[j - 1].toInt()
                                    break
                                }
                            }
                        }
                    }
                }
                //将得到的参数进行赋值。
//                setSubItemid3(subItemId3.toString())
//                setNumTransform(numTransform.toString())
//                setFittingmethod(fittingmethod.toString())
//                setParametersA(AValue.toString())
//                setParametersB(BValue.toString())
//                setParaAPoint(paramsAPoint.toString())
//                setParaBPoint(paramsBPoint.toString())

                this.idOfSubItem=subItemId3.toString()
                this.formulaTrans=numTransform.toString()
                this.methodOfFitting=fittingmethod.toString()
                this.parasA=AValue.toString()
                this.pointA=paramsAPoint.toString()
                this.parasB=BValue.toString()
                this.pointB=paramsBPoint.toString()

            } else if (ReceiveDate[2] == (0x07).toByte()) {  //软件版本号
                val buf = ByteArray(DateLength - 2)
                for (i in buf.indices) {
                    buf[i] = ReceiveDate[i + 5]
                }
                val id = String(buf, StandardCharsets.UTF_8)
                Log.d("softwarid", id)
//                setSoftwareVersion(id)
                this.softwareVersion=id
            } else if (ReceiveDate[2] == (0x08).toByte()) {  //硬件版本号
                val buf = ByteArray(DateLength - 2)
                for (i in buf.indices) {
                    buf[i] = ReceiveDate[i + 5]
                }
                val id = String(buf, StandardCharsets.UTF_8)
//                setHardwareVersion(id)
                this.hardwareVersion=id
            } else if (ReceiveDate[2] == (0x09).toByte()) {  //ID卡序列号
                val buf = ByteArray(DateLength - 2)
                for (i in buf.indices) {
                    buf[i] = ReceiveDate[i + 5]
                }
                val id = String(buf, StandardCharsets.UTF_8)
//                setIDSerialNumber(id)
                this.IDSerialNumber=id
            } else if (ReceiveDate[2] == (0x0A).toByte()) {   //出厂日期
                val buf = ByteArray(DateLength - 2)
                for (i in buf.indices) {
                    buf[i] = ReceiveDate[i + 5]
                }
                val dateStr = String(buf, StandardCharsets.UTF_8)
//                setDateOfManufacture(dateStr)
                this.dateOfManufacture=dateStr
            } else if (ReceiveDate[2] == (0x0B).toByte()) {   //有效月数
//                byte[] buf =new byte[DateLength-2];
//                for(int i=0;i<buf.length;i++){
//                    buf[i]=ReceiveDate[i+5];
//                }
//                String DataStr = new String(buf, StandardCharsets.UTF_8);
                val num: Int = ReceiveDate[5].toInt()
//                setWarrantyDate(num.toString())
                this.warrantDate= num.toString()
            } else if (ReceiveDate[2] == (0x0C).toByte()) {   //基准峰
                val buf = ByteArray(DateLength - 2)
                for (i in buf.indices) {
                    buf[i] = ReceiveDate[i + 5]
                }
                val DataStr = String(buf, StandardCharsets.UTF_8)
//                setReferencePeak(DataStr)
                this.referencePeak=DataStr
            }
        } else {
            Log.d(ReceiveFlag, "数据接收错误")
        }
    }
}
