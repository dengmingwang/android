package com.example.twentychannelspoct.fragments

import android.util.Log
import android.view.View
import android.widget.Spinner
import androidx.core.view.size
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.bean.TaskItem
import com.example.twentychannelspoct.bean.taskStatus
import com.example.twentychannelspoct.pages.Test
import com.example.twentychannelspoct.pages.Test.Companion.taskList
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.singleton.SerialHelper3
import com.example.twentychannelspoct.utils.PrintfUtil
import com.example.twentychannelspoct.utils.Tools
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

class DebugPara1Fragment: BaseFragment(), View.OnClickListener {

    override fun getLayoutId(): Int {
        return R.layout.debug_params1

    }


    override fun initView(view: View) {
        //扫描向前、向后、转盘初始化
        Btn_Turntable.setOnClickListener(this)
        Btn_Moveforward.setOnClickListener(this)
//        Btn_Moveback.setOnClickListener(this)
        //设置检测窗起始位置、退卡定位移动
        Btn_Testbegin.setOnClickListener(this)
//        Btn_Returncardlocation.setOnClickListener(this)
        //读取电压、条码值
        Btn_ReadVoltage.setOnClickListener(this)
        Btn_Readcode.setOnClickListener(this)
        //丢卡、丢卡复位
        Btn_Lostcard.setOnClickListener(this)
//        Btn_Pushcard.setOnClickListener(this)
        //系统复位
        Btn_Reset.setOnClickListener(this)
        //测试
        Btn_Test.setOnClickListener(this)
        //获取当前设置的扫描位置&试剂卡测试位置
        Btn_GetPosition.setOnClickListener(this)
        //设定
        Btn_Lostcardto.setOnClickListener(this)
        Btn_ScanID.setOnClickListener(this)
        Btn_Scantest.setOnClickListener(this)
        Btn_Turntableto.setOnClickListener(this)
        //插卡状态
        Btn_Cardin.setOnClickListener(this)
    }


    override fun onClick(p0: View?) {
//        Tools.buzzer(100)
        when (p0?.id) {
            R.id.Btn_Turntable -> SerialHelper1.sendHex("EE0100043030303030FF")
            R.id.Btn_Moveforward -> SerialHelper1.sendHex("EE0100013030303030FF")
            R.id.Btn_Moveback -> SerialHelper1.sendHex("EE0100023030303030FF")
            R.id.Btn_Lostcard -> SerialHelper1.sendHex("EE0100033030303030FF")
//            R.id.Btn_Pushcard -> SerialHelper1.sendHex("EEB104FF")
            R.id.Btn_Reset -> SerialHelper1.sendHex("EE0100003030303030FF")
            R.id.Btn_ReadVoltage -> SerialHelper1.sendHex("EE0202003030303030FF")
//            R.id.Btn_Readcode -> SerialHelper1.sendHex("EEA102FF")

            R.id.Btn_Cardin->{
                    SerialHelper1.sendHex("EE0103043131313131FF")
                    Log.d("1","插卡")
            }
            R.id.Btn_Testbegin -> {
//                val str1: String = Tools.StringToHex2(Edi_Testbegin.text.toString(), true)
//                Log.d("str1", str1)
//                SerialHelper1.sendHex(str1)
            }
            R.id.Btn_Lostcardto->{
//                val str2: String = Tools.StringToHex1(Edi_Lostcardto.text.toString(),true)
//                Log.d("str2",str2)
                SerialHelper1.sendHex("EE0402003032303030FF")
            }
            R.id.Btn_ScanID->{
                val str3:String = Tools.StringToHex3(Edi_ScanID.text.toString(),true)
                Log.d("str3",str3)
//                SerialHelper1.sendHex(str3)
            }
            R.id.Btn_Turntableto->{
                SerialHelper1.sendHex("EE0402003031303030FF")
                Log.d("2","EE0401003031303030FF")
//                val str4:String = Tools.StringToHex4(Edi_Turntableto.text.toString(),true)
//                Log.d("str4",str4)
//                SerialHelper1.sendHex(str4)
            }
            R.id.Btn_Scantest->{
              SerialHelper1.sendHex("EE0102033030323435FF")

//                SerialHelper1.sendHex(str5)
            }
//            R.id.Btn_Returncardlocation -> {
//                val str2: String = Tools.StringToHex2(Edi_Returncardlocation.text.toString(), false)
//                Log.d("str2", str2)
//                SerialHelper1.sendHex(str2)
//            }
            R.id.Btn_Test -> {
                    testThread()
                }
             //这里测试指令： 运行测试的指令待定。
            R.id.Btn_Printf -> {
                Thread{
                    val printfUtil = PrintfUtil()
                    printfUtil.serialHelper = SerialHelper3
                    printfUtil.Printer()
                }.start()
                Tools.exportExcel(this.context)
            }
            R.id.Btn_GetPosition -> SerialHelper1.sendHex("EEB105FF")
        }
    }
    private val starArray6= arrayOf("0","1", "2", "3", "4", "5", "6", "7", "8", "9")
    private fun testThread() {
        for (i in 0..19) {
            if (i < 9) {
                 Log.d("teststr","EE040100303"+starArray6[i + 1]+"303030FF")
                SerialHelper1.sendHex("EE040100303"+starArray6[i + 1]+"303030FF")
            } else if (i in 9 until 19) {
                Log.d("teststr","EE040100313"+starArray6[i -9]+"303030FF")
                SerialHelper1.sendHex("EE040100313"+starArray6[i -9]+"303030FF")
            } else {
                Log.d("teststr","EE0401003230303030FF")
                SerialHelper1.sendHex("EE0401003230303030FF")
            }
            Thread.sleep(41000)
        }
    }

}
