package com.example.twentychannelspoct.fragments

import android.R.string
import android.annotation.SuppressLint
import android.os.Build
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.view.View
import android_serialport_api.ComBean
import androidx.annotation.RequiresApi
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.utils.ChartUtil
import com.github.mikephil.charting.data.Entry
import kotlinx.android.synthetic.main.config_system.*
import kotlinx.android.synthetic.main.debug_curve2.*
import kotlinx.android.synthetic.main.debug_params1.*
import kotlinx.android.synthetic.main.debug_params1.view.*
import java.util.*


/**
//                       _ooOoo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                       O\ = /O
//                   ____/`---'\____
//                 .   ' \\| |// `.
//                  / \\||| : |||// \
//                / _||||| -:- |||||- \
//                  | | \\\ - /// | |
//                | \_| ''\---/'' | |
//                 \ .-\__ `-` ___/-. /
//              ______`. .' /--.--\ `. . __
//           ."" '< `.___\_<|>_/___.' >'"".
//          | | : `- \`.;`\ _ /`;.`/ - ` : | |
//            \ \ `-. \_ __\ /__ _/ .-` / /
//    ======`-.____`-.___\_____/___.-`____.-'======
//                       `=---='
//
//    .............................................
//             佛祖保佑             永无BUG
 * =====================================================
 * 作    者：航
 * 创建日期：2023017
 * * 描    述: 设置界面-公司简介
 * =====================================================
 */

class DebugCurve2Fragment: BaseFragment(), View.OnClickListener {

    private val mHandler = object : Handler(Looper.getMainLooper()) {

        @SuppressLint("ClickableViewAccessibility")
        override fun handleMessage(msg: Message) {
            // 在这里可以进行UI操作
            when (msg.what) {
                1 -> {
                    showCurve(2)
                }
            }
        }
    }


    private fun showCurve(Flag: Int) {
        val xDataList: MutableList<String> = ArrayList() // x轴数据源
        val yDataList: MutableList<Entry> = ArrayList() // y轴数据数据源
        if (Flag == 2) {
//            Log.d("Datalist", Datalist1.get(1).toString())
            for (i in Datalist1.indices) {
                // x轴显示的数据
                xDataList.add(i.toString())
                //y轴生成float类型的随机数
                yDataList.add(Entry(Datalist1.get(i).toFloat(), i))
                Log.d("Datalist", Datalist1.get(i).toString())
            }
            ChartUtil.showChart(this.context, lineChart, xDataList, yDataList, "电压峰值图", "电压峰值", "")
            if (Datalist1.size != 0) {
                Log.d("Datalsit", "!0")
            } else {
                Log.d("Datalsit", "0")
            }
        }
        if (Flag == 1) {
//             给上面的X、Y轴数据源做假数据测试
            for (i in 0..379) {
                // x轴显示的数据
                xDataList.add("")
                //y轴生成float类型的随机数
                val value = (Math.random() * 20000).toFloat() + 100
                yDataList.add(Entry(value, i))
            }
            //显示图表,参数（ 上下文，图表对象， X轴数据，Y轴数据，图表标题，曲线图例名称，坐标点击弹出提示框中数字单位）
            ChartUtil.showChart(this.context, lineChart, xDataList, yDataList, "趋势图", "", "")
        }
    }

    override fun getLayoutId(): Int {
        return R.layout.debug_curve2
    }

    @RequiresApi(Build.VERSION_CODES.N)
    override fun initView(view: View) {
        TestCurve?.setOnClickListener(this)
        Thread {
            recThread(receiveMode)
        }.start()
    }


    override fun onClick(p0: View?) {

//        Tools.buzzer(100)
        when (p0?.id) {
            R.id.TestCurve -> {
                showCurve(1)
                sendMessage(1)
            }
        }
    }


    var Datalist1 = DoubleArray(380)
    fun dataProcess1(str: String): DoubleArray? {
        val e = str.slice(str.length - 2..str.length - 1)
        val f = str.slice(2..str.length - 3)
        if (e == "FF") {
            var i = 0
            while (i < f.length) {
                val n = i / 4
                val strX1 = f.substring(i, i + 4)
                Log.d("strX1", strX1)
                val anInt1 = strX1.toInt(16)
                Log.d("anInt1", anInt1.toString())
                val a = anInt1.toDouble()
                Datalist1[n] = a
                i += 4
                Log.d("i", i.toString())
            }

        }
        sendMessage(1);
        return Datalist1

    }


    private val str =StringBuilder()
    private var receiveMode :Int=0
    /**线程接收处理函数*/
    @SuppressLint("SuspiciousIndentation")
    @RequiresApi(Build.VERSION_CODES.N)
    private fun recThread(int:Int) {
        while(true){
            if(SerialHelper1.receiverFlag){
                Log.d("receiveData", SerialHelper1.receiveHexStr)
                var a:String=""
                a=SerialHelper1.receiveHexStr
                val h=a.slice(0..1)
                /*字符串拼接*/
                    if (h == "EE") {
                        str.clear()
                        str.append(a)
                    }
                    str.append(a)
                    Log.d("str", str.toString())
                    val e = str.slice(str.length - 2..str.length - 1)
                    if (e == "FF") {
                        if (str.length == 1524) {
                            dataProcess1(str = String(str))
                            Log.d("str", str.toString())
                            str.clear()
                        } else {
                            str.clear()
                        }
                    }
                SerialHelper1.receiverFlag=false
                when(int){
                    0-> dataProcessing0(SerialHelper1.comBeanRec)
                    1-> dataProcessing1(SerialHelper1.comBeanRec)
                    2-> dataProcessing2(SerialHelper1.comBeanRec)
                }
            }
        }
    }

    private var dataList =ArrayList<Float>()
    /**数据接收处理：字节码*/
    @RequiresApi(Build.VERSION_CODES.N)
    private fun dataProcessing0(comBean: ComBean) {
        val recByte: ByteArray = comBean.bReC
        val size :Int =recByte.size
        val arr = arrayOfNulls<Int>(size)
        for (i in 0 until size){  //byte数组转int数组
            if (recByte[i]<0){
                arr[i]= 256+recByte[i]  //将收到的负字节符号转换成正的
            }else {
                arr[i]= recByte[i]+0
            }
        }
        for (i in 0 until size){
            println(arr[i])
        }

        if ( arr[0]==0xEE&&arr[size-1]==0xFF ) {
            Log.d("TAG","收到正确指令")
            when(arr[1]){
//region   /*条码值*/
//                0x01->{
//                    Log.d("TAG","条码值")
//                    val subArray = arr.slice(3..arr.size-3)
//                    for (i in subArray.indices){
//                        var num : Int =subArray[i]?.toInt()?:0
//                        dataList.add(num.toFloat())
//                        println(num)
//                    }

//                    val ints: IntArray = dataList.stream().mapToInt { i -> i.toInt() }.toArray()
//                    Arrays.stream(ints).forEach(System.out::print)
////                    Log.d("2", dataList.toString())
////                    Receive1= ints
//
////                    a= dataList.toInt()
////                    Log.d("1",a)
////                    Receive1 = a
//
//                    Receive1= 1
//                    println()
////                    Receive1 = ints.toString()
////                    Log.d("TAG",Receive1)
//                    sendMessage(2)
//
//                }
//endregion
                // region              //接收到测试曲线数据
//                0x07-> {
//                    val subArray= arr.slice(2 ..arr.size-2)  //切割数组。
//                    for (i in subArray.indices){
//                        subArray[i!!]?.let { println(it) }
//                    }
//                    for (i in subArray.indices step 2) {
//                        var num:Int = (subArray[i] ?:0 ) *256+ (subArray[i+1]?.toInt() ?: 0)
//                        dataList.add(num.toFloat())
//                        println(num)
//                    }
//                    sendMessage(1);
//                    println(bytesToHex(subArray))
//                    println(subArray.toString())
//                    Log.d("TAG","4")
//                }
                //endregion
                0x03->{
                    Log.d("TAG","1")
                    val subArray= arr.slice(4..arr.size-5)  //切割数组。
                    println(bytesToHex(subArray))
                    Log.d("TAG","4")
                }
            }
        }
    }


    /**将转换后的数值数组转换为16进制数*/
    private fun bytesToHex(bytes: List<Int?>): String {
        val hexArray = "0123456789ABCDEF".toCharArray()
        val hexChars = CharArray(bytes.size * 2)
            for (j in bytes.indices) {
                val v = bytes[j]?.and(0xFF)
                if (v != null) {
                    hexChars[j * 2] = hexArray[v.ushr(4)]
                    hexChars[j * 2 + 1] = hexArray[v.and(0x0F)]
                }
            }
        return String(hexChars)
    }

    /**处理直接HexString*/
    private fun dataProcessing1(comBean: ComBean) {

    }
    /**转换的ASCII文本字符串*/
    private fun dataProcessing2(comBean: ComBean) {

    }

    /**handle消息发送函数 */
    private fun sendMessage(int: Int){
        val message = Message()
        message.what = int
        mHandler.sendMessage(message)
    }
}

