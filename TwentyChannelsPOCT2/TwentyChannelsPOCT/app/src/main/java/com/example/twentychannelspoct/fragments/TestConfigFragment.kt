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
import androidx.appcompat.app.AppCompatActivity
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.singleton.SerialHelper3
import com.example.twentychannelspoct.utils.ChartUtil
import com.example.twentychannelspoct.utils.PrintfUtil
import com.example.twentychannelspoct.utils.Tools
import com.github.mikephil.charting.data.Entry
import kotlinx.android.synthetic.main.config_test.*
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
 * * 描    述: 设置界面-结果输出参数项设置
 * =====================================================
 */

class TestConfigFragment(activity: AppCompatActivity): BaseFragment(), CompoundButton.OnCheckedChangeListener{

    override fun getLayoutId(): Int {
        return R.layout.config_test
    }

    var saveDays: Int=0
    var alignFlag:Int=0
    var instantPrintFlag:Boolean=false
    var autoTestFlag:Boolean=false
    var cardDetectFlag:Boolean=false
    var setFlag:Boolean=false

    lateinit var prefs: SharedPreferences
    override fun initView(view: View) {
        SwitchAlign_Config3.setOnCheckedChangeListener(this)
        SwitchPrint_Config3.setOnCheckedChangeListener(this)
        SwitchAutoTest_Config3.setOnCheckedChangeListener(this)
        SwitchJuge_Config3.setOnCheckedChangeListener(this)
        prefs = activity?.getSharedPreferences("data1", Context.MODE_PRIVATE)!!
        var setFlag=prefs?.getBoolean("setFlag3",false)
        if(setFlag == true){//如果获取当前参数
            SwitchAlign_Config3.isChecked= prefs?.getBoolean("alignFlag",false) == true
            SwitchPrint_Config3.isChecked=prefs?.getBoolean("instantPrintFlag",false)==true
            SwitchAutoTest_Config3.isChecked=prefs?.getBoolean("autoTestFlag",false)==true
            SwitchJuge_Config3.isChecked=prefs?.getBoolean("cardDetectFlag",false)==true
        }else{

        }
    }

    override fun onCheckedChanged(p0: CompoundButton?, p1: Boolean) {
        val editor=prefs.edit()
        when(p0?.id){
            R.id.SwitchAlign_Config3 ->{
                if (p1) {
                    editor.putBoolean("alignFlag", true)
                }  else {
                    editor.putBoolean("alignFlag", false)
                }
            }
            R.id.SwitchPrint_Config3 ->{
                if (p1) {
                    editor.putBoolean("instantPrintFlag", true)
                } else {
                    editor.putBoolean("instantPrintFlag", false)
                }
            }
            R.id.SwitchAutoTest_Config3 ->{
                if (p1) {
                    editor.putBoolean("autoTestFlag", true)
                } else {
                    editor.putBoolean("autoTestFlag", false)
                }
            }
            R.id.SwitchJuge_Config3 ->{
                if (p1) {
                    editor.putBoolean("cardDetectFlag", true)
                } else {
                    editor.putBoolean("cardDetectFlag", false)
                }
            }
        }
        editor.putBoolean("setFlag",true)
        editor.apply()
    }



}
