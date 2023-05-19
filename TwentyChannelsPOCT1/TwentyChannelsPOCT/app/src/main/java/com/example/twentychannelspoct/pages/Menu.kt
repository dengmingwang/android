package com.example.twentychannelspoct.pages

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.fragments.BottomTitle
import kotlinx.android.synthetic.main.activity_menu.*
import java.util.*

class Menu : AppCompatActivity(), View.OnClickListener {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_menu)
        bindComponent()
    }

    private fun bindComponent() {
        BottomTitle(0, Timer(),this)

        //菜单按钮
        SingTest.setOnClickListener(this)
        SingTest?.setBackgroundResource(R.drawable.btn_menu_selet1)

        BatchTesting.setBackgroundResource(R.drawable.btn_menu_selet2)
        BatchTesting.setOnClickListener(this)

        HistoricalData.setBackgroundResource(R.drawable.btn_menu_selet3)
        HistoricalData.setOnClickListener(this)

//
//        ProjectManagement.setBackgroundResource(R.drawable.btn_menu_selet4)
//        ProjectManagement.setOnClickListener(this)

        SystemSetting.setBackgroundResource(R.drawable.btn_menu_selet5)
        SystemSetting.setOnClickListener(this)
    }

    override fun onClick(v: View?) {
        when (v?.id){
            R.id.SingTest->{  //跳转测试界面
                Log.d("onclick","SingTest")
                val intent = Intent(this,Test::class.java)
                startActivity(intent)
            }
            R.id.HistoricalData->{ //数据查询界面
                val intent = Intent(this,HistoryData::class.java)
                startActivity(intent)
            }
            R.id.SystemSetting->{ // 系统设置界面
                val intent = Intent(this,SystemSettings::class.java)
                startActivity(intent)
            }
            R.id.BatchTesting ->{
                val intent = Intent(this,Debugging::class.java)
                startActivity(intent)
            }


        }
    }

    override fun onDestroy() {
        super.onDestroy()
        Log.d("Menu","Destroy")
    }
}