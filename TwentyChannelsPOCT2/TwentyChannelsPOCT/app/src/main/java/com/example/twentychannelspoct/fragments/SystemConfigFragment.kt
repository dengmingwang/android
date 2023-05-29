package com.example.twentychannelspoct.fragments

import android.annotation.SuppressLint
import android.app.AlertDialog
import android.os.Build
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.view.View
import android.widget.*
import android_serialport_api.ComBean
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.singleton.SerialHelper3
import com.example.twentychannelspoct.utils.ChartUtil
import com.example.twentychannelspoct.utils.PrintfUtil
import com.example.twentychannelspoct.utils.ScreenUtils
import com.example.twentychannelspoct.utils.Tools
import com.github.mikephil.charting.data.Entry
import kotlinx.android.synthetic.main.config_lis2.*
import kotlinx.android.synthetic.main.config_system.*
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

class SystemConfigFragment: BaseFragment(), View.OnClickListener,
    CompoundButton.OnCheckedChangeListener, TimePicker.OnTimeChangedListener,
    DatePicker.OnDateChangedListener {

    var starArray3 = arrayOf("Sec", "Min")
    private var showDateFlag = false
    override fun getLayoutId(): Int {
        return R.layout.config_system
    }

    override fun initView(view: View) {
        showDateFlag=true
        Time_config4.setOnClickListener(this)
        Date_config4.setOnClickListener(this)
        Config4_Save.setOnClickListener(this)

        TurnOffScreen.setOnClickListener(this) //息屏时间设置

        Tools.initSpinner(this.context, UnitSpinner, starArray3)

        val num = (Tools.GetAvailableSize() * 100 / Tools.getTotalInternalMemorySize()).toInt()
        MemorySpace.text = num.toString()

        BuzzerSwitch.setOnCheckedChangeListener(this)
    }

    override fun onClick(p0: View?) {
        when(p0?.id){
            R.id.Time_config4 ->{
                Log.d("当前设置", "Time")
                dialogFlag = true
                showMyTime()
            }
            R.id.Date_config4 ->{
                Log.d("当前设置", "Data")
                dialogFlag = true
                showMyDate()
            }
        }
    }

    override fun onCheckedChanged(p0: CompoundButton?, p1: Boolean) {
        when(p0?.id){
            R.id.BuzzerSwitch ->{
                if (p1) {
                    Log.e("TAG", "开启 蜂鸣器")
                } else {
                    Log.e("TAG", "关闭 蜂鸣器")
                }
            }
        }
    }


    /**定义时间日期组件 */
    private var datePicker1: DatePicker? = null
    private var timePicker1: TimePicker? = null

    /**新建年月日中间变量 */
    private var TYear = 0

    private var TMonth:Int = 0

    private  var TDay:Int = 0

    private var THour:Int = 0

    private  var TMin:Int = 0

    private var TSecond:Int = 0

    /**年月日最终的接收变量 */
    private var ZYear = 0
    /**年月日最终的接收变量 */
    private var ZMonth:Int = 0
    /**年月日最终的接收变量 */
    private  var ZDay:Int = 0
    /**年月日最终的接收变量 */
    private var ZHour:Int = 0
    /**年月日最终的接收变量 */
    private  var ZMin:Int = 0
    private var dialogFlag = false

    /**设置时间小弹窗 */
    private fun showMyTime() {
        val builder = AlertDialog.Builder(this.context)
        val dialog = builder.setCancelable(false).create()
        val dialogView = View.inflate(this.context, R.layout.dialog_time, null)
        //设置对话框布局
        dialog.setView(dialogView)
        //绑定组件，并设置默认参数值：
//        CurStep=(TextView)dialogView.findViewById(R.id.textView);
        timePicker1 = dialogView.findViewById<View>(R.id.TimePicker1) as TimePicker
        timePicker1!!.setIs24HourView(true)
        //设置时间被改变后的监听时间
        timePicker1!!.setOnTimeChangedListener(this)
        val btnConfirm = dialogView.findViewById<View>(R.id.btn_login) as Button
        val btnCancel = dialogView.findViewById<View>(R.id.btn_Confirm) as Button
        btnConfirm.setOnClickListener {
            ZHour = THour
            ZMin = TMin
            Time_config4?.text = "$ZHour:$ZMin:00"
            Log.d("zhour", ZHour.toString())
            dialog.dismiss()
        }
        //点击取消，直接退出当前参数设置对话框，同时将参数设置的中间量清零。
        btnCancel.setOnClickListener { dialog.dismiss() }
        if (dialogFlag) {
            dialog.show()
            dialogFlag = false
        } else {
        }
        //调整dialog 的View 宽度。
        dialog.window!!.setLayout(ScreenUtils.getScreenWidth(this.context) * 2 / 3,
            LinearLayout.LayoutParams.WRAP_CONTENT) //通过此方式来设置dialog 的宽高
    }

    /**设置日期小弹窗 */
    private fun showMyDate() {
        val builder = AlertDialog.Builder(this.context)
        val dialog = builder.setCancelable(false).create()
        val dialogView = View.inflate(this.context, R.layout.dialog_date, null)
        //设置对话框布局
        dialog.setView(dialogView)
        //绑定组件，并设置默认参数值：
        datePicker1 = dialogView.findViewById<View>(R.id.datePicker1) as DatePicker
        //初始化日期，并设置日期被改变后的监听事件
        datePicker1!!.init(2021, 8, 7,this)
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            datePicker1!!.setOnDateChangedListener(this)
        }
        val btnConfirm = dialogView.findViewById<View>(R.id.btn_login) as Button
        val btnCancel = dialogView.findViewById<View>(R.id.btn_Confirm) as Button
        btnConfirm.setOnClickListener {
            ZYear = TYear
            ZMonth = TMonth
            ZDay = TDay
            Date_config4?.text = "$ZYear-$ZMonth-$ZDay"
            dialog.dismiss()
        }
        //点击取消，直接退出当前参数设置对话框，同时将参数设置的中间量清零。
        btnCancel.setOnClickListener { dialog.dismiss() }
        if (dialogFlag) {
            dialog.show()
            dialogFlag = false
        } else {
        }
        //调整dialog 的View 宽度。
        dialog.window!!.setLayout(ScreenUtils.getScreenWidth(this.context) * 2 / 3,
            LinearLayout.LayoutParams.WRAP_CONTENT) //通过此方式来设置dialog 的宽高
    }

    override fun onTimeChanged(p0: TimePicker?, p1: Int, p2: Int) {
        THour = p1
        TMin = p2
    }

    override fun onDateChanged(p0: DatePicker?, p1: Int, p2: Int, p3: Int) {
        TYear = p1
        TMonth = p2 + 1
        TDay = p3

    }




}
