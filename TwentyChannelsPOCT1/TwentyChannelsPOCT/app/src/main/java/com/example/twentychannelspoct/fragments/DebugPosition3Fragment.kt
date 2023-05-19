package com.example.twentychannelspoct.fragments

import android.annotation.SuppressLint
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.view.View
import android_serialport_api.ComBean
import com.example.twentychannelspoct.R
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
 * * 描    述: 设置界面-公司简介
 * =====================================================
 */

class DebugPosition3Fragment: BaseFragment() ,View.OnClickListener{

    override fun getLayoutId(): Int {
        return R.layout.debug_location3
    }

    override fun initView(view: View) {
    }

    override fun onClick(v: View?) {
    }


}
