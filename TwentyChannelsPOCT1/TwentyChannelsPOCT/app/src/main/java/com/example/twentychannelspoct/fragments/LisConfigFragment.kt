package com.example.twentychannelspoct.fragments

import android.annotation.SuppressLint
import android.content.Context
import android.content.SharedPreferences
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.view.View
import android.widget.CompoundButton
import android_serialport_api.ComBean
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.singleton.SerialHelper3
import com.example.twentychannelspoct.utils.ChartUtil
import com.example.twentychannelspoct.utils.PrintfUtil
import com.example.twentychannelspoct.utils.Tools
import com.github.mikephil.charting.data.Entry
import kotlinx.android.synthetic.main.config_lis2.*
import kotlinx.android.synthetic.main.debug_curve2.*
import kotlinx.android.synthetic.main.debug_params1.*

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

class LisConfigFragment: BaseFragment(), View.OnClickListener,
    CompoundButton.OnCheckedChangeListener {

    private val starArray1 = arrayOf("2400",
        "4800",
        "19200",
        "38400",
        "57600",
        "115200") //用来表示孔位字符串数组     孔位的数字中间是不留空格。

    private val starArray2 = arrayOf("IP通信", "串口通信") //用来表示孔位字符串数组     孔位的数字中间是不留空格。

    override fun getLayoutId(): Int {
        return R.layout.config_lis2
    }

    lateinit var prefs: SharedPreferences
    var lisSwitchFlag:Boolean=false
    var baudRate:String=""
    var transmissionMode=""
    var localIP1=""
    var localIP2=""
    var localIP3=""
    var localIP4=""

    var remoteIP1=""
    var remoteIP2=""
    var remoteIP3=""
    var remoteIP4=""
    var port=""

    override fun initView(view: View) {
        //串口设置：
        Tools.initSpinner(this.context, BaudRate_Select, starArray1)
        //端口选择
        Tools.initSpinner(this.context, Interface_Select, starArray2)

        Save_Config2.setOnClickListener(this)
        NextItem.setOnClickListener(this)
        LIS_Switch.setOnCheckedChangeListener(this)
        prefs = activity?.getSharedPreferences("data1", Context.MODE_PRIVATE)!!
        var setFlag=prefs?.getBoolean("setFlag2",false)
        if(setFlag == true){//如果获取当前参数

            Local_IP1?.setText(prefs.getString("localIP1", ""))
            Local_IP2?.setText(prefs.getString("localIP2", ""))
            Local_IP3?.setText(prefs.getString("localIP3", ""))
            Local_IP4?.setText(prefs.getString("localIP4", ""))

            Remote_IP1?.setText(prefs.getString("remoteIP1", ""))
            Remote_IP2?.setText(prefs.getString("remoteIP2", ""))
            Remote_IP3?.setText(prefs.getString("remoteIP3", ""))
            Remote_IP4?.setText(prefs.getString("remoteIP4", ""))

            Port_Select?.setText(prefs.getString("port", ""))
            LIS_Switch.isChecked=prefs.getBoolean("port", false)
            Log.d("starArry1.size",starArray1.size.toString())
            for (i in starArray1.indices){
                if(starArray1[i]==prefs.getString("baudRate", "")){
                    BaudRate_Select.setSelection(i)
                }
            }

            for (i in starArray2.indices){
                if (starArray2[i]==prefs.getString("transmissionMode", ""))
                    Interface_Select.setSelection(i)
            }
        }else{

        }
    }

    override fun onClick(p0: View?) {
        when(p0?.id){
            R.id.Save_Config2 ->{
                saveConfig2()
            }
            R.id.NextItem ->{
                toNextItem()
            }
            R.id.LIS_Switch ->{

            }
        }

    }

    private fun toNextItem() {
        Log.d("当前点击", "NextItem")
        val str: String = BaudRate_Select.selectedItem.toString()
        Log.d("str", str)
        for (i in starArray1.indices) {
            if (starArray1.get(i) == BaudRate_Select.selectedItem.toString()) {
                Log.d("i", i.toString())
                if (i == starArray1.size - 1) {
                    BaudRate_Select.setSelection(0)
                } else {
                    BaudRate_Select.setSelection(i + 1)
                }
                break
            }
        }
    }

    private fun saveConfig2() {
        Log.d("TAG","保存参数")
        val editor=prefs.edit()
        editor.putString("localIP1",Local_IP1.text.toString())
        editor.putString("localIP2",Local_IP2.text.toString())
        editor.putString("localIP3",Local_IP3.text.toString())
        editor.putString("localIP4",Local_IP4.text.toString())

        editor.putString("remoteIP1",Remote_IP1.text.toString())
        editor.putString("remoteIP2",Remote_IP2.text.toString())
        editor.putString("remoteIP3",Remote_IP3.text.toString())
        editor.putString("remoteIP4",Remote_IP4.text.toString())

        editor.putString("baudRate",BaudRate_Select.selectedItem.toString())
        editor.putString("transmissionMode",Interface_Select.selectedItem.toString())
        editor.putBoolean("setFlag2",true)
        editor.apply()
    }

    override fun onCheckedChanged(p0: CompoundButton?, p1: Boolean) {
        val editor=prefs.edit()
        when(p0?.id){
            R.id.LIS_Switch ->{
                if (p1) {
                    editor.putBoolean("lisSwitchFlag",true)
                } else {
                    editor.putBoolean("lisSwitchFlag",false)
                }
            }
        }
        editor.apply()
    }




}
