package com.example.twentychannelspoct.pages

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.os.Message
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.fragments.BottomTitle
import kotlinx.android.synthetic.main.activity_initialization.*
import java.util.*
//初始化
class initialization : AppCompatActivity() {

    private val handler = object : Handler(Looper.getMainLooper()) {
        override fun handleMessage(msg: Message) {
            // 在这里可以进行UI操作
            when (msg.what) {
                1 ->progressBar?.start()
            }
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_initialization)
        bindComponent()
        Thread{ progressRun() }.start()
    }

    private fun progressRun() {
        ShowUI(false)
        try {
            Thread.sleep(1000)
        } catch (e: InterruptedException) {
            e.printStackTrace()
        }
        val msg = Message()
        msg.what = 1

        handler.sendMessage(msg) //发送处理码
        try {
            Thread.sleep(4000)
        } catch (e: InterruptedException) {
            e.printStackTrace()
        }
        val intent = Intent(this, Menu::class.java)
        startActivity(intent)
        finish()
    }

    private fun bindComponent() {
        BottomTitle(2, Timer(),this)
    }

    fun ShowUI(Flag: Boolean) {
        val intent = Intent()
        intent.action = "marvsmart_bar"
        if (Flag) {
            intent.putExtra("marvsmart_swich", true)
            intent.putExtra("marvsmart_toast", "") //Toast,如果传入数据不为空且长度大于0，就以Toast弹出
        } else {
            intent.putExtra("marvsmart_swich", false)
            intent.putExtra("marvsmart_toast", "") //Toast,如果传入数据不为空且长度大于0，就以Toast弹出
        }
        sendBroadcast(intent)
    }

}


