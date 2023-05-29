package com.example.twentychannelspoct.singleton

import android_serialport_api.ComBean
import android_serialport_api.SerialHelper


/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author D
 *创建时间：2023/3/1
 *用途:
 ***************************
 */

object SerialHelper3 : SerialHelper(){
    var receiverFlag : Boolean=false
    lateinit var comBeanRec:ComBean
    var receiveHexStr :String=""
    var receiveAsciiStr :String=""
    init {
        SerialHelper3.baudRate=115200
        SerialHelper3.port="/dev/ttyS3"
        SerialHelper3.open()
    }
    override fun onDataReceived(comBean: ComBean?) {
        if (comBean != null) {
            comBeanRec=comBean
            receiveHexStr = bytesToHex(comBean.bReC)
            receiveAsciiStr = String(comBean.bReC)
            println("receiveHexStr=$receiveHexStr,receiveAsciiStr=$receiveAsciiStr")
            receiverFlag=true;
        }
    }
    //获取comBeanRec 转换后的字符串。
    fun getComBean(int: Int):String{
        val receiveData:String
        if (int==0){
            //接收16进制数据：
            receiveData = bytesToHex(comBeanRec.bReC)

        }else{
            //接收ASCII码字符串：
            receiveData= String(comBeanRec.bReC)

        }
        return receiveData
    }



    fun bytesToHex(bytes: ByteArray): String {
        val hexArray = "0123456789ABCDEF".toCharArray()
        val hexChars = CharArray(bytes.size * 2)
        for (j in bytes.indices) {
            val v = bytes[j].toInt() and 0xFF
            hexChars[j * 2] = hexArray[v.ushr(4)]
            hexChars[j * 2 + 1] = hexArray[v and 0x0F]
        }
        return String(hexChars)
    }


}