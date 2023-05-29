package com.example.twentychannelspoct.fragments

import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.utils.Tools
import kotlinx.android.synthetic.main.bottom_fragment.*
import java.util.*

/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author
 *创建时间：2022/12/30  13:15
 *用途:  用于显示底部的日期时间
 ***************************
 */

open class BottomTitle(private val activityFlag:Int, var Curtimer: Timer, val CurActivity: AppCompatActivity) : BaseFragment() {
    private val handler = object : Handler(Looper.getMainLooper()) {
        override fun handleMessage(msg: Message) {
            // 在这里可以进行UI操作
            when (msg.what) {
                0 -> {
                    Text_Date?.text= Tools.getDate().get(0);
                    Text_Time?.text = Tools.getDate().get(1)
                }
            }
        }
    }
    init {    //此处添加主构造函数的控制逻辑
        val fragmentManager = CurActivity.supportFragmentManager
        val transaction = fragmentManager.beginTransaction()
        when(activityFlag){
            0->transaction.replace(R.id.bottom_Menu_title, this)
            1->transaction.replace(R.id.bottom_Test_Title, this)
            2->transaction.replace(R.id.bottom_initialization_title, this) //初始化界面
        }
        transaction.commit()
    }
    override fun getLayoutId(): Int {
        return if(activityFlag==1){
            R.layout.bottom_fragment
        }else{
            R.layout.bottom_men_fragment
        }
    }

    private fun timerRun() {
        Curtimer?.scheduleAtFixedRate(object : TimerTask() {
            override fun run() {
                val message: Message = handler.obtainMessage()
                message.what = 0
                handler.sendMessage(message)
//                Log.d("Timer2","Timer2")
            }
        }, 0, 1000)
    }
    override fun initView(view: View) {
        Text_Date?.text=Tools.getDate().get(0);
        Text_Time?.text = Tools.getDate().get(1)
        timerRun()
        Menu_Back?.setOnClickListener {
//            Tools.buzzer(100)
            CurActivity.finish()
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        Log.d("BottomFragment","Destroy")
        Curtimer.cancel()
    }
}