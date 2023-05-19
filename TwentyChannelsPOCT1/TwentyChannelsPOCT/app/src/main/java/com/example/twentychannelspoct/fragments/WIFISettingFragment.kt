package com.example.twentychannelspoct.fragments

import android.annotation.SuppressLint
import android.app.AlertDialog
import android.content.Context
import android.net.wifi.WifiConfiguration
import android.net.wifi.WifiManager
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.text.InputType
import android.util.Log
import android.view.View
import android.widget.AdapterView
import android.widget.EditText
import android.widget.ListView
import android_serialport_api.ComBean
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.adapter.WiFiListAdapter
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.singleton.SerialHelper3
import com.example.twentychannelspoct.utils.ChartUtil
import com.example.twentychannelspoct.utils.PrintfUtil
import com.example.twentychannelspoct.utils.Tools
import com.github.mikephil.charting.data.Entry
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
 * * 描    述: 设置界面
 * =====================================================
 */

class WIFISettingFragment: BaseFragment() {

    override fun getLayoutId(): Int {
        return R.layout.config_wifi
    }

    override fun initView(view: View) {
//		val wifiManager = view.getApplicationContext().getSystemService(Context.WIFI_SERVICE)
//		val wifiManager = this.context?.applicationContext?.getSystemService(Context.WIFI_SERVICE) as WifiManager
        val wifiManager =this.context?.applicationContext?.getSystemService(Context.WIFI_SERVICE) as WifiManager
        wifiManager.startScan()
        val scanResults = wifiManager.scanResults
        for (scanResult in scanResults) {
            val ssid = scanResult.SSID
            val bssid = scanResult.BSSID
            val level = scanResult.level
            // 处理扫描结果...
        }

        val listView: ListView = view.findViewById(R.id.listView)
        listView.adapter = this.context?.let { WiFiListAdapter(it, scanResults) }
        listView.onItemClickListener =
            AdapterView.OnItemClickListener { parent, view, position, id ->
                val scanResult = scanResults[position]
                val ssid = scanResult.SSID
                val editText = EditText(this.context)
                editText.inputType =
                    InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_PASSWORD
                AlertDialog.Builder(this.context)
                    .setTitle("Enter WiFi Password")
                    .setMessage(String.format("Connect to WiFi network %s?", ssid))
                    .setView(editText)
                    .setPositiveButton(
                        "Connect"
                    ) { dialog, which ->
                        val password = editText.text.toString()
                        // 连接 WiFi 热点...

                        //2.创建WIFI配置：
                        val wifiConfig = WifiConfiguration()
                        wifiConfig.SSID = String.format("\"%s\"", ssid)
                        wifiConfig.preSharedKey = String.format("\"%s\"", password)

                        // 其他可选的 WiFi 配置
                        // wifiConfig.hiddenSSID = true;
                        // wifiConfig.status = WifiConfiguration.Status.ENABLED;
                        // wifiConfig.priority = 40;
                        val netId = wifiManager.addNetwork(wifiConfig)
                        if (netId != -1) {
                            // 成功添加 WiFi 配置
                            wifiManager.disconnect()
                            wifiManager.enableNetwork(netId, true)
                            wifiManager.reconnect()
                            Log.d("当前WIFI连接成功", "ok")
                            // WiFi 连接成功
                        } else {
                            // WiFi 连接失败
                        }
                    }
                    .setNegativeButton("Cancel", null)
                    .show()
            }
    }




}
