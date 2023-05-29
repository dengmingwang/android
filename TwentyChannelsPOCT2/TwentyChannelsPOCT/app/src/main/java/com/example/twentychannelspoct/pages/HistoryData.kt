package com.example.twentychannelspoct.pages

import android.annotation.SuppressLint
import android.app.AlertDialog
import android.app.DatePickerDialog
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.util.TypedValue
import android.view.*
import android.widget.*
import androidx.constraintlayout.widget.ConstraintLayout
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.bean.QueryBean
import com.example.twentychannelspoct.bean.SaveItem
import com.example.twentychannelspoct.fragments.BottomTitle
import com.example.twentychannelspoct.utils.Tools
import com.lingber.mycontrol.datagridview.DataGridView
import kotlinx.android.synthetic.main.activity_history_data.*
import org.litepal.LitePal
import java.util.*

open class HistoryData : AppCompatActivity() , View.OnClickListener {

    lateinit var totalCheckBox: CheckBox

    private val mHandler = object : Handler(Looper.getMainLooper()) {
        @SuppressLint("ClickableViewAccessibility")
        override fun handleMessage(msg: Message) {
            // 在这里可以进行UI操作
            when (msg.what) {
                3->{   //用来判断页面所有子项是否被选中
                    var a = 0
                    val curListDatas: List<QueryBean> = mDataGridView.pageDatas as List<QueryBean>
                    for (i in curListDatas.indices) {
                        val queryBean = mDataGridView.getRowData(i) as QueryBean
                        Log.d("queryBean", queryBean.toString())
                        if (queryBean.checkBox.isChecked) {
                            val checkBox = mDataGridView.getItemCellContentView(i, 0) as CheckBox // 获取指定单元格内部View
                            checkBox.isChecked = true
                            a++
                        } else {
                            val checkBox = mDataGridView.getItemCellContentView(i, 0) as CheckBox // 获取指定单元格内部View
                            checkBox.isChecked = false
                            a--
                        }
                    }
                    Log.d("a值", a.toString())
                    //进入页面，判断下全选框状态。
                    totalCheckBox.isChecked = a == curListDatas.size
                    Log.d("33333", "3333")
                }
                4->{
                    val layout = findViewById<ConstraintLayout>(R.id.historyConstraintLayout)
                    val textView = TextView(this@HistoryData)
                    textView.setTextSize(TypedValue.COMPLEX_UNIT_SP, 22f)
                    textView.gravity = Gravity.CENTER
                    totalCheckBox = CheckBox(this@HistoryData)
                    totalCheckBox.scaleX = 2f
                    totalCheckBox.scaleY = 2f
                    val params = ConstraintLayout.LayoutParams(
                        ConstraintLayout.LayoutParams.WRAP_CONTENT,
                        ConstraintLayout.LayoutParams.WRAP_CONTENT)
                    params.startToStart = R.id.textView18
                    params.topToBottom = R.id.textView18
                    params.setMargins(0, 40, 0, 0)
                    layout.addView(totalCheckBox, params)

                    //判断当前totalbox是否是被手点击按下。
                    totalCheckBox.setOnTouchListener { v, event ->
                        if (event.action == MotionEvent.ACTION_DOWN) {
                            println(event.rawX)
                            println(totalCheckBox.left)
                            println(totalCheckBox.right)

                            if (event.rawX > totalCheckBox.left && event.rawX < totalCheckBox.right) {
                                totalCheckBox.isChecked=! totalCheckBox.isChecked
//
                                //根据上面赋值状态，更改子项选中状态。
                                val curListData: List<QueryBean> = mDataGridView.pageDatas as List<QueryBean>

                                val curPage:Int=mDataGridView.currentPageNumber
                                val itemsOfPage:Int=mDataGridView.pageItems
                                Log.d("curListDatas", curListData.size.toString())
                                for (i in curListData.indices) {
                                    if (totalCheckBox.isChecked) {
                                        val checkBox = mDataGridView.getItemCellContentView(i, 0) as CheckBox // 获取指定单元格内部View
                                        dataSource[i+(curPage-1)*itemsOfPage].checkBox.isChecked=true
                                        checkBox.isChecked = true
                                    } else {
                                        val checkBox = mDataGridView.getItemCellContentView(i, 0) as CheckBox // 获取指定单元格内部View
                                        checkBox.isChecked = false
                                        dataSource[i+(curPage-1)*5].checkBox.isChecked=false
                                    }
                                }
                                return@setOnTouchListener true
                            }
                        }
                        false
                    }
                }
            }
        }
    }

    private lateinit var mDataGridView : DataGridView<QueryBean>
    private var dataSource = ArrayList<QueryBean>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_history_data)
        bindComponent()
    }


    private fun initData() {
        LitePal.deleteAll(SaveItem::class.java)
        for (i in 0 .. 10 ){
            SaveItem(i.toString(),"123+${i}","血浆","1234","血液分析","C反应蛋白","20",
                "mmol",Tools.getDate()[0]+Tools.getDate()[1], Tools.getDate()[2],"kobe1","女","20","岁").save()
        }
        for (i in 0 .. 10 ){
            SaveItem(i.toString(),"456+${i}","血浆","1234","血液分析","C反应蛋白","20",
                "mmol",Tools.getDate()[0]+Tools.getDate()[1], Tools.getDate()[2],"kobe2","女","20","岁").save()
        }
        for (i in 0 .. 10 ){
            SaveItem(i.toString(),"789+${i}","血浆","1234","血液分析","C反应蛋白","20",
                "mmol",Tools.getDate()[0]+Tools.getDate()[1], Tools.getDate()[2],"kobe3","女","20","岁").save()
        }
        for (i in 0 .. 10 ){
            SaveItem(i.toString(),"1010+${i}","血浆","1234","血液分析","C反应蛋白","20",
                "mmol",Tools.getDate()[0]+Tools.getDate()[1], Tools.getDate()[2],"kobe4","女","20","岁").save()
        }
        for(i in 0..10){
            SaveItem(age="19", sampleID = "686", ageUnit = "周", name = "Lebron", gender = "男").save()
        }


    }


    var viewShow: View? = null
    private lateinit var linnearLayout : LinearLayout
    private fun bindComponent() {
        //绑定下方弹窗
        BottomTitle(1, Timer(),this)
        //绑定显示下方数据表单
        linnearLayout = findViewById(R.id.LinearLayout)
        viewShow = LayoutInflater.from(this).inflate(R.layout.history_page1, linnearLayout, true)
        showHistory()
        //右侧点击按钮
        UpData.setOnClickListener(this)
        Printf.setOnClickListener(this)
        Delete.setOnClickListener(this)
        Export.setOnClickListener(this)
        //屏幕上方操作按钮
        HistoryView.setOnClickListener(this)
        Classification.setOnClickListener(this)
        FuzzyQuery.setOnClickListener(this)
        //日期
        date1.setOnClickListener(this)
        date2.setOnClickListener(this)
        Confirm.setOnClickListener(this)
    }


    /**
     * 用于显示查询页面的表单插件
     */
    private fun showHistory() {
        mDataGridView = findViewById(R.id.datagridview)
        // 设置数据源
        val ordinaryDatas: List<SaveItem> = LitePal.findAll(SaveItem::class.java)
        for (i in ordinaryDatas.indices) { //得倒着排序，最新的在最前面：
            val checkBox = CheckBox(this)
            val index = ordinaryDatas.size - 1 - i //倒着排序的index；
            dataSource.add(QueryBean(checkBox,
                ordinaryDatas[index].index,
                ordinaryDatas[index].sampleID,
                ordinaryDatas[index].projectName,
                ordinaryDatas[index].concentrationValue,
                ordinaryDatas[index].testTime,
                ""))
        }

        // 设置列数
        mDataGridView.setColunms(7)
        // 设置表头内容
        //基本字段：流水号、样本号、项目、检测结果、检测时间、" page";
        mDataGridView.setHeaderContentByStringId(intArrayOf(R.string.history1_page,
            R.string.history1_index,
            R.string.history1_sampleNumber,
            R.string.history1_projectName,
            R.string.history1_concentration,
            R.string.history1_time,
            R.string.history1_page))

        // 绑定字段
        mDataGridView.setFieldNames(arrayOf(
            "checkbox",
            "index",
            "sampleID",
            "projectName",
            "concentrationValue",
            "testTime",
            "page"))
        // 每个column占比
        mDataGridView.setColunmWeight(floatArrayOf(1f, 3f, 3f, 3f, 2f, 3f, 1f))
        // 每个单元格包含控件
        mDataGridView.setCellContentView(arrayOf<Class<*>>(CheckBox::class.java,
            TextView::class.java,
            TextView::class.java,
            TextView::class.java,
            TextView::class.java,
            TextView::class.java,
            TextView::class.java))
        // 设置数据源
        mDataGridView.setDataSource(dataSource)
        // 单行选中模式
        // mDataGridView.setSelectedMode(2);
        // 启用翻页
        mDataGridView.setFlipOverEnable(true, 5, fragmentManager)
        // 初始化表格
        mDataGridView.initDataGridView()
        // 单元格点击事件
        mDataGridView.setOnItemCellClickListener { _, row, column ->
            val checkBox = mDataGridView.getItemCellContentView(row, 0) as CheckBox // 获取指定单元格内部View
            val curSelectIndex: Int = (mDataGridView.currentPageNumber - 1) * mDataGridView.pageItems + row
            Log.d("当前点击的是第几行", row.toString())
            if (column < 5) {
                if (column == 0) {
                    if (checkBox.isChecked) {
                        checkBox.isChecked = true
                        dataSource[curSelectIndex].checkBox.isChecked = true
                        Log.d("改变checkbox 状态2", "改为true")
                    } else {
                        checkBox.isChecked = false
                        Log.d("改变checkbox 状态", "改为false")
                        dataSource[curSelectIndex].checkBox.isChecked = false
                    }
                    sendMessage(3)  //每当勾选一个check ,判断一次，目前是否处于全选状态。
                }else{


                }
            }
            Log.d("每页数据条数", mDataGridView.pageItems.toString())
            val list1: List<QueryBean> = mDataGridView.pageDatas as List<QueryBean>
            for (i in list1.indices) {
                if (list1[i].checkBox.isChecked) {
                    Log.d("第" + i + "个", "true")
                } else {
                    Log.d("第" + i + "个", "False")
                }
            }
        }
        //点击单元格内内容触发
        mDataGridView.setOnItemCellContentClickListener { _, row, column ->
            val checkBox = mDataGridView.getItemCellContentView(row, 0) as CheckBox // 获取指定单元格内部View
            Log.d("当前点击的是第几行", row.toString())
            Log.d("当前点击的是第几列", column.toString())
            val curSelectIndex: Int = (mDataGridView.currentPageNumber - 1) * mDataGridView.pageItems + row
            if (column < 5) {
                if (column == 0) {
                    if (checkBox.isChecked) {
                        checkBox.isChecked = true
                        dataSource[curSelectIndex].checkBox.isChecked = true
                        Log.d("改变checkbox 状态2", "改为true")
                    } else {
                        checkBox.isChecked = false
                        Log.d("改变checkbox 状态", "改为false")
                        dataSource[curSelectIndex].checkBox.isChecked = false
                    }
                    sendMessage(3)
                }
            }
        }
        mDataGridView.setOnSwitchPageNumberListener { //翻页之后，判断在翻页页面中的方框勾选状态。
            sendMessage(3)
            Log.d("翻页", "翻页")
        }
        sendMessage(4)
    }


    /**handle消息发送函数 */
    private fun sendMessage(int: Int) {
        when(int){
            3-> Log.d(tag,"刷新当前页面子项的勾选情况")
            4-> Log.d(tag,"生成全选框，并绑定点击事件")
        }
        val message = Message()
        message.what = int
        mHandler.sendMessage(message)
    }


    override fun onClick(p0: View?) {
        when(p0?.id){
            R.id.UpData ->{
                Log.d(tag,"上传")
            }
            R.id.Printf ->{
                Log.d(tag,"打印")
            }
            R.id.Delete ->{
                Log.d(tag,"Delete")
                deleteItem()
            }
            R.id.Export ->{
                Log.d(tag,"Export")
            }
            R.id.date1 ->{
                showDatePickDlg(1)
            }
            R.id.date2 ->{
                showDatePickDlg(2);
            }
            R.id.Confirm ->{
                displayByTime()
            }

            R.id.HistoryView ->{
                linnearLayout.removeAllViews()
                viewShow = LayoutInflater.from(this).inflate(R.layout.history_page1, linnearLayout, true)
                showHistory()
            }
            R.id.Classification ->{
                linnearLayout.removeAllViews()
                viewShow = LayoutInflater.from(this).inflate(R.layout.history_page2, linnearLayout, true)
            }
            R.id.FuzzyQuery ->{
                //新建弹窗：
                advDialogFlag = true
                fuzzyQuery()
            }
            R.id.TestTime ->{
                showDatePickDlg(3)
            }

        }
    }

    val tag="TAG"


    /**
     * @Author
     * @Time 2022/10/27 15:30
     * @Description
     * @param  num:表示将当前获取时间复制给特定的显示文本框，1：是主界面2个日期的前一个，2.是主界面2个日期的后一个。
     * 3：查询弹窗的时间设置窗。
     */
    @SuppressLint("SetTextI18n")
    private fun showDatePickDlg(num: Int) {
        val calendar = Calendar.getInstance()
        val datePickerDialog = DatePickerDialog(this,
            { _, year, monthOfYear, dayOfMonth ->
                var moth=monthOfYear+1;

                var monthStr: String
                var dayStr: String
                var dateStr: String
                when (num) {
                    1 -> {
                        monthStr = if (moth < 10) {
                            "0$moth"
                        } else {
                            "" + moth
                        }
                        dayStr = if (dayOfMonth < 10) {
                            "0$dayOfMonth"
                        } else {
                            "" + dayOfMonth
                        }
                        dateStr = year.toString() + monthStr + dayStr
                        dateStart = dateStr.toInt()
                        this.date1.text = "$year-$monthStr-$dayStr"
                    }
                    2 -> {
                        monthStr = if (moth < 10) {
                            "0$moth"
                        } else {
                            "" + moth
                        }
                        dayStr = if (dayOfMonth < 10) {
                            "0$dayOfMonth"
                        } else {
                            "" + dayOfMonth
                        }
                        dateStr = year.toString() + monthStr + dayStr
                        dateEnd = dateStr.toInt()
                        Log.d(tag,dateEnd.toString())
                        this.date2.text = "$year-$monthStr-$dayStr"
                    }
                    3 -> {
                        monthStr = if (moth < 10) {
                            "0$moth"
                        } else {
                            "" + moth
                        }
                        dayStr = if (dayOfMonth < 10) {
                            "0$dayOfMonth"
                        } else {
                            "" + dayOfMonth
                        }
                        dateStr = year.toString() + monthStr + dayStr
                        dateEnd = dateStr.toInt()
                        testTime?.text = "$year-$monthStr-$dayStr"
                    }
                }
            }, calendar[Calendar.YEAR], calendar[Calendar.MONTH], calendar[Calendar.DAY_OF_MONTH])
        datePickerDialog.show()

    }

    /**查询起始日期： 例： 20230107  即不包括短横显示的日期数字格式*/
    private var dateStart = 0
    /**查询结束日期：*/
    private var dateEnd = 0


    /**
     *  fun(): 根据左侧时间段来重新更新页面数据
     */
    private fun displayByTime() {
        val splitDate1: Array<String> = date1.text.toString().split("-").toTypedArray()
        dateStart = (splitDate1[0] + splitDate1[1] + splitDate1[2]).toInt()
        val splitDate2: Array<String> = date2.text.toString().split("-").toTypedArray()
        dateEnd = (splitDate2[0] + splitDate2[1] + splitDate2[2]).toInt()
        if (dateStart < dateEnd) { //按日期查询测试记录：
            dataSource.clear()
            val ordinaryData: List<SaveItem> = LitePal.where("testDay between ?  and  ? ", "" + (dateStart - 1), "" + dateEnd)
                .find(SaveItem::class.java)
            for (i in ordinaryData.indices) { //得倒着排序，最新的在最前面：
                val checkBox = CheckBox(this)
                val index = ordinaryData.size - 1 - i //倒着排序的index；
                dataSource.add(QueryBean(checkBox,
                    ordinaryData[index].index,
                    ordinaryData[index].sampleID,
                    ordinaryData[index].ItemName,
                    ordinaryData[index].concentrationValue ,
                    ordinaryData[index].testTime,
                    ""))
            }
            mDataGridView.setDataSource(dataSource) //重新装载数据。
            mDataGridView.updateAll() // 更新所有数据。
        } else { //弹窗提示：重新设置日期。
            Toast.makeText(this, "当前日期范围设置有误，请重设", Toast.LENGTH_SHORT).show()
        }
    }

    private fun deleteItem() {
        var dataSource2 = ArrayList<QueryBean>()
        //删除选中数据条目： 1.获取到选中项目的流水号
        for (i in dataSource.indices) {
            if (!dataSource[i].checkBox.isChecked) {
                dataSource2.add(dataSource[i])
            }else{
                //                        dataSource.get(i).delete(); //数据库删除
                Log.d(tag, LitePal.findAll(SaveItem::class.java).size.toString())
                LitePal.deleteAll(SaveItem::class.java, "index=?", dataSource[i].index) //好使
                Log.d(tag, LitePal.findAll(SaveItem::class.java).size.toString())
            }
        }
        dataSource.clear()
        Log.d("dataSize", dataSource.size.toString())
        for (i in dataSource2.indices){
            dataSource.add(dataSource2[i])
        }
        Log.d("dataSize", dataSource.size.toString())
        mDataGridView.setDataSource(dataSource) //重新装载数据。
        mDataGridView.updateAll() // 更新所有数据。
    }


    /**当前的姓名选项 */
    private val nameStr = ""
    private var age = 0
    private val ageUnit: String? = null
    private val gender: String? = null
    private lateinit var dialogView: View
    private var testTime: TextView? = null

    private var advDialogFlag = false

    /**
     * 模糊查询 ：根据人名信息查询对应数据
     */
    private fun fuzzyQuery() {
        val builder = AlertDialog.Builder(this)
        val dialog = builder.setCancelable(false).create()
        dialogView = View.inflate(this, R.layout.dialog_fuzzy_query, null)
        //设置对话框布局
        dialog.setView(dialogView)
        //确认和取消
        val btnConfirm = dialogView.findViewById<View>(R.id.btn_Confirm) as Button
        val btnCancel = dialogView.findViewById<View>(R.id.btn_Cancel) as Button

        val name = dialogView.findViewById<EditText>(R.id.Name)
        val age = dialogView.findViewById<EditText>(R.id.Age)
        val ageUnit = dialogView.findViewById<Spinner>(R.id.AgeUint)

        val gender = dialogView.findViewById<Spinner>(R.id.Gender)

        val starArray2 = arrayOf("岁", "周") //用来表示孔位字符串数组     孔位的数字中间是不留空格。
        val starArray3 = arrayOf("男", "女") //用来表示孔位字符串数组     孔位的数字中间是不留空格。
        Tools.initSpinner(this, ageUnit, starArray2)
        Tools.initSpinner(this, gender, starArray3)

        btnConfirm.setOnClickListener(View.OnClickListener {
            dataSource.clear()
            var nameStr: String=""
            var ageStr = ""
            var genderStr: String=""
            var ageUnitStr: String=""
            nameStr = if (name.text.toString() != "") {
                name.text.toString()
            } else {
                Toast.makeText(this, "请输入姓名", Toast.LENGTH_SHORT).show()
                return@OnClickListener
            }
            if (age.text.toString() != "") {
                if (Tools.isInteger(age.text.toString())) {
                    ageStr = age.text.toString()
                }
            } else {
                Toast.makeText(this, "请输入年龄", Toast.LENGTH_SHORT).show()
                return@OnClickListener
            }
            genderStr = gender.selectedItem.toString()
            ageUnitStr=ageUnit.selectedItem.toString()
            var saveItemList = LitePal.where("name = ? and age = ? and ageUnit=? and gender = ?", nameStr, ageStr, ageUnitStr,genderStr).limit(1000).
            find(SaveItem::class.java) as ArrayList<SaveItem>

            for (i in saveItemList.indices) { //得倒着排序，最新的在最前面：
                val checkBox = CheckBox(this)
                val index = saveItemList.size - 1 - i //倒着排序的index；
                dataSource.add(QueryBean(checkBox,
                    saveItemList[index].index,
                    saveItemList[index].sampleID,
                    saveItemList[index].ItemName,
                    saveItemList[index].concentrationValue ,
                    saveItemList[index].testTime,
                    ""))
            }
            mDataGridView.setDataSource(dataSource) //重新装载数据。
            mDataGridView.updateAll() // 更新所有数据。
            dialog.dismiss()
        })
        btnCancel.setOnClickListener { dialog.dismiss() }
        if (advDialogFlag) {
            dialog.show()
            advDialogFlag = false
        }
        //调整dialog 的View 宽度。
        dialog.window!!.setLayout(
            ViewGroup.LayoutParams.WRAP_CONTENT,
            ViewGroup.LayoutParams.WRAP_CONTENT
        ) //通过此方式来设置dialog 的宽高
        val mWindow = dialog.window
        val lp = mWindow!!.attributes
        lp.x = -200 //新位置X坐标
        lp.y = -150 //新位置Y坐标
        dialog.onWindowAttributesChanged(lp)
    }

}