package com.example.twentychannelspoct.pages

import android.annotation.SuppressLint
import android.app.AlertDialog
import android.content.ContentValues.TAG
import android.content.Context
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.os.Message
import android.util.Log
import android.view.MotionEvent
import android.view.View
import android.view.inputmethod.InputMethodManager
import android.widget.*
import android_serialport_api.ComBean
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.size
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.adapter.ItemAdapter1
import com.example.twentychannelspoct.bean.*
import com.example.twentychannelspoct.fragments.BottomTitle
import com.example.twentychannelspoct.singleton.SerialHelper1
import com.example.twentychannelspoct.singleton.SerialHelper2
import com.example.twentychannelspoct.singleton.SerialHelper3
import com.example.twentychannelspoct.utils.*
import com.github.mikephil.charting.data.Entry
import kotlinx.android.synthetic.main.activity_test.*
import kotlinx.android.synthetic.main.activity_test2.*
import kotlinx.android.synthetic.main.activity_test2.RecyclerTest
import kotlinx.android.synthetic.main.activity_test2.patientinfoBtn
import kotlinx.android.synthetic.main.activity_test2.pauseBtn
import kotlinx.android.synthetic.main.activity_test2.printBtn
import kotlinx.android.synthetic.main.activity_test2.readcardBtn
import kotlinx.android.synthetic.main.activity_test2.testBtn
import kotlinx.android.synthetic.main.activity_test2.view.*
import kotlinx.android.synthetic.main.config_system.*
import kotlinx.android.synthetic.main.dialog_date.*
import kotlinx.android.synthetic.main.dialog_graph.*
import kotlinx.android.synthetic.main.dialog_time.*
import kotlinx.android.synthetic.main.item_list1.*
import kotlinx.android.synthetic.main.item_list1.view.*
import java.text.SimpleDateFormat
import java.util.*
import kotlin.concurrent.thread
import kotlin.math.roundToInt


class Test: AppCompatActivity() ,View.OnClickListener, CompoundButton.OnCheckedChangeListener {
    companion object{
        /*执行任务列表*/
        val taskList=ArrayList<TaskItem>()
        val taskList2=ArrayList<TaskItem>()

        val starArray1 = arrayOf("未选择", "全血", "血清", "血浆", "血清/血浆", "末梢血", "质控", "其它")//用来表示孔位字符串数组     孔位的数字中间是不留空格。

        /*血样下拉菜单*/
        @SuppressLint("StaticFieldLeak")
        private lateinit var  SampleType:Spinner

        /*样本号下拉菜单*/
        @SuppressLint("StaticFieldLeak")
        private lateinit var IntNo:EditText

    }


    private val editTextList=ArrayList<EditText>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_test2)
        binComponent()
        initTaskList()
//        initTaskList2()
        initResultList()
        timer1run()
        initialization()
        Thread { recThread(receiverMode) } .start()
    }

    private fun  initialization(){
        SerialHelper1.sendHex("EE0100003030303030FF")
    }
//region/*handler*/
    private val updateStatus :Int=1
    val handler=object :Handler(Looper.getMainLooper()){
        override fun handleMessage(msg: Message) {
            // 在这里可以进行UI操作
            when (msg.what) {
                updateStatus -> {
                    for (i in 0 until taskList.size){
                        when(taskList[i].Status){
                            taskStatus.WaitForCard->{
                                imglist[i].setImageResource(R.drawable.indicate_bar_white)
                            }
//                            taskStatus.WaitForCounter->{
//                                imglist[i].setImageResource(R.drawable.indicate_bar_blue)
//                            }
                            taskStatus.WaitForTesting->{
                                imglist[i].setImageResource(R.drawable.indicate_bar_red2)
                            }
//                            taskStatus.Testing->{
//                                imglist[i].setImageResource(R.drawable.indicate_bar_yellow)
//                            }
                            taskStatus.Complete->{
                                imglist[i].setImageResource(R.drawable.indicate_bar_green)
                            }
                        }
                    }
//                    editHideInput()
                }
                2->{
                    Text_receive?.setText(receive1)
                    cardnum.setText(receive1)
                }
                3->{
                    resultText?.setText(receive1)
                    resulttext.setText(receive1)
                }
                4->{
                    renewRecycleView(idFlag)
                }
            }
        }
    }
//endregion

    private lateinit var sampleindex:Spinner
    private lateinit var steaknum:Spinner
    private var Text_receive: TextView? = null
    private var resultText:TextView?=null

    /**界面底部的测试状态提示图标集合*/
    private val imglist = ArrayList<ImageView>()

//region/*binComponent*/
    private fun binComponent() {
        //绑定下方弹窗
        BottomTitle(1,Timer(),this)
        imglist.add(image1)
        imglist.add(image2)
        imglist.add(image3)
        imglist.add(image4)
        imglist.add(image5)
        imglist.add(image6)
        imglist.add(image7)
        imglist.add(image8)
        imglist.add(image9)
        imglist.add(image10)
        imglist.add(image11)
        imglist.add(image12)
        imglist.add(image13)
        imglist.add(image14)
        imglist.add(image15)
        imglist.add(image16)
        imglist.add(image17)
        imglist.add(image18)
        imglist.add(image19)
        imglist.add(image20)

        //结果表单
//        val layoutManager=LinearLayoutManager(this)
//        RecyclerTest?.layoutManager = layoutManager
//        val adapter1 = ItemAdapter1(resultList)
//        RecyclerTest?.adapter = adapter1

    /*结果显示Text*/
        resultText = findViewById<TextView>(R.id.resulttext)
    /*卡ID号显示Text*/
        Text_receive = findViewById<TextView>(R.id.cardnum)
        /*模式选择*/
        switch_model?.setOnCheckedChangeListener(this)
        /*开始测试*/
        testBtn?.setOnClickListener(this)
        /*初始化*/
        initializationBtn?.setOnClickListener(this)
        /*读卡*/
        readcardBtn?.setOnClickListener(this)
        /*患者信息*/
        patientinfoBtn?.setOnClickListener(this)
        /*条码模式*/
        Btn_Testmodel?.setOnClickListener(this)
       /*暂停测试*/
        pauseBtn?.setOnClickListener (this)
    /*打印*/
        printBtn?.setOnClickListener(this)
        /*继续测试*/
//        continueBtn?.setOnClickListener(this)
        /*插卡*/
        CardinBtn?.setOnClickListener(this)
        /*测试曲线*/
//        GraphBtn?.setOnClickListener (this)
        /*血样下拉菜单*/
        SampleType=findViewById(R.id.spinner_type)
        initSpinner(this,SampleType,starArray1)

//region        /*卡位下拉菜单*/
//        val starArray2 = arrayOf("1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
//            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20")
//        sampleindex=findViewById(R.id.spinner_IDnum)
//        initSpinner(this,sampleindex,starArray2)
        //endregionn

        /*线条数*/
        val starArrays3 = arrayOf("0","1","2","3")
        steaknum = findViewById(R.id.Streak)
        initSpinner(this,steaknum,starArrays3)

        /*样本号输入*/
        IntNo=findViewById(R.id.edit_intNo)
        editTextList.add(IntNo)

//        editHideInput()
    }
    //endregionn

    private var cardinFlag:Boolean=false
    private var istestflag:Boolean=false
    private var testindex:Int =0
    /*检测模式,false:连续测试，true:定时孵育*/
    private var testmodel:Boolean=false

   //region /*按钮*/
    @SuppressLint("SuspiciousIndentation")
    override fun onClick(v: View?) {
//        Tools.buzzer(100)
        when(v?.id){
            R.id.initializationBtn->{
                SerialHelper1.sendHex("EE0103043232323232FF")
                Log.d("TAG","initializationBtn")
                SerialHelper1.sendHex("EE0100003030303030FF")
            }
            R.id.CardinBtn->{
                cardinFlag = !cardinFlag
                if (cardinFlag) {
                    SerialHelper1.sendHex("EE0103043131313131FF")
                    Toast.makeText(this, "开始插入试剂卡",Toast.LENGTH_SHORT).show()
                    Log.d("TAG", "Carding")
                }else if(!cardinFlag){
                    SerialHelper1.sendHex("EE0103043232323232FF")
                    slotnumList.clone()
                    Toast.makeText(this, "插卡完成，请选择检测模式",Toast.LENGTH_SHORT).show()
                    Log.d("TAG", "Card in Finish")
                }
            }
            R.id.testBtn-> {
                SerialHelper1.sendHex("EE0103043232323232FF")
                if (!testmodel){
                    Log.d("TAG","Touch this TestButton")
//                    if (!istestflag){
//                        istestflag = !istestflag
                        thread {
                                for (i in 0 until slotnumList.size){
                                    timer1run()
                                    while(countTime==0) {
                                        Log.d("TAG", "Finish Hatch")
                                        SerialHelper1.sendHex(taskList[i].sendHex)
                                        Thread.sleep(500)
                                        Log.d("SendHexStr", taskList[i].sendHex)
                                        break
                                    }
                                    while(taskList[i].Status!= taskStatus.Complete) {
                                        Log.d("", i.toString())
                                        Log.d("taskLis[i].Status++$i", taskList[i].Status.toString())
                                        Thread.sleep(500)
                                        break
                                    }
                                    taskList[i].Status=taskStatus.WaitForCard
                                    Thread.sleep(500)
                                    taskList[i].isHave=false
                                    slotnumList.clear()
                                }
                        }
                }
                else{//定时检测
                    if (testmodel){
                        Log.d("TAG","Touch this TestButton")
                        thread {
                            for (i in 0 until slotnumList.size){
//                            Thread { recThread1(receiverMode) } .start()
                                if (getcardflag) {
                                    timer1run()
                                    while (countTime == 0) {
                                        Log.d("TAG", "Finish Hatch")
                                        SerialHelper1.sendHex(taskList[i].sendHex)
                                        Log.d("SendHexStr", taskList[i].sendHex)
                                        Thread.sleep(25000)
                                        break
                                    }
                                    while(taskList[i].Status!= taskStatus.Complete) {
                                        Log.d("", i.toString())
                                        Log.d("taskLis[i].Status++$i", taskList[i].Status.toString())
                                        Thread.sleep(500)
                                        break
                                    }
                                    taskList[i].Status=taskStatus.WaitForCard
                                    Thread.sleep(500)
                                    taskList[i].isHave=false
                                    slotnumList.clear()
                                }
                            }
                        }
                    }
                }
            }

            R.id.Btn_Testmodel->{
                for (i in 0 ..steaknum.size){
                    dataProcessing1(dataStr = dataStr)
                }
            }
            R.id.testmodel->{
                testmodel=!testmodel
                if (testmodel==true){
                    Toast.makeText(this, "单卡检测模式",Toast.LENGTH_SHORT).show()
                }else if(testmodel==false) {
                    Toast.makeText(this, "连续检测模式",Toast.LENGTH_SHORT).show()
                }
                Log.d("TAG","Touch test_model")
            }
            R.id.patientinfoBtn->{
                patiDialogFlag=true
                patiDialog()
            }
            R.id.printBtn->{
                Log.d("TAG","printf")
                printf()
            }


            R.id.pauseBtn->{
                Log.d("TAG","Touch pauseBtn")
                pauseThread()
            }
//region            /*暂停检测和继续检测*/
//            R.id.continueBtn->{
////                Continue=!Continue
////                testflag=1
//                while (pause) {
//                    resumeThread()
//                Log.d("TAG","Continue Test")
//            }
            //endregion

            R.id.readcardBtn -> {
                Log.d("TAG", "readcardBtn")
                Thread { recThread1(receiverMode) } .start()
            }
//            R.id.GraphBtn->{
//                graphDialogFlag =true
//                graphDialog()
//            }
        }
    }
    //endregion

    //region/*打印功能*/
     private fun printf() {
         var printfUtil = PrintfUtil()

         //设置打印信息
         printfUtil.printfTime = TimeStr//打印时间

         printfUtil.age = age  //年龄

         printfUtil.name = nameStr  //名字

         printfUtil.gender = genderStr  //性别

         printfUtil.reportType = 0  //报告类型

         printfUtil.ageUnit = ageUnit  //年龄单位

         printfUtil.setSampleNumber(edit_intNo?.text.toString())//样本号
//         for (i in testItemList.indices) {
//             printfUtil.subProjectName = testItemList.get(i).getItem_Name() //子项目名称
//             printfUtil.resultValue = testItemList.get(i).getItem_ConcentrationValue() //结果值
//         }

         printfUtil.serialNumber = TimeStr    //流水号
         printfUtil.isBarcodeSwitch = true

         //printf
         //执行打印操作
         printfUtil.serialHelper = SerialHelper3
         printfUtil.Printer()
     }
//endregion

//region    /*患者信息的弹窗显示*/
     /**当前的姓名选项 */
    private var nameStr = ""
    private var age = ""
    private var ageUnit = ""
    private var genderStr = ""
    private var patiDialogFlag=false
    /**
     * @Author
     * @Time 2023/2/14 13：35
     * @Description 患者信息的弹窗显示：
     */
    @SuppressLint("ClickableViewAccessibility")
    private fun patiDialog(){
        val builder = AlertDialog.Builder(this)
        val dialog = builder.setCancelable(false).create()
        val dialogView = View.inflate(this, R.layout.dialog_pati_information, null)
        //设置对话框布局
        dialog.setView(dialogView)
        //确认和取消
        val btnConfirm = dialogView.findViewById<View>(R.id.btn_Confirm) as Button
        val btnCancel = dialogView.findViewById<View>(R.id.btn_Cancel) as Button
        val nameEdit = dialogView.findViewById<EditText>(R.id.Name)
        nameEdit.setText(nameStr)
        //在Dialog中，点击空白处隐藏EditText弹出的输入键盘。
        dialogView.setOnTouchListener { v, event ->
            if (event.action == MotionEvent.ACTION_DOWN) {
                println(event.rawX)
                println(nameEdit.left)
                println(nameEdit.right)
                if (event.rawX < nameEdit.left || event.rawX > nameEdit.right) {
                    // User clicked outside the EditText, hide the keyboard
                    val inputMethodManager = getSystemService(INPUT_METHOD_SERVICE) as InputMethodManager
                    inputMethodManager.hideSoftInputFromWindow(nameEdit.windowToken, InputMethodManager.HIDE_NOT_ALWAYS)
                    return@setOnTouchListener true
                }
            }
            false
        }
        val age = dialogView.findViewById<EditText>(R.id.Age)
        age.setText(this.age)
        val ageUnit = dialogView.findViewById<Spinner>(R.id.AgeUint)
        val gender = dialogView.findViewById<Spinner>(R.id.Gender)
        val starArray3 = arrayOf("岁", "周") //用来表示孔位字符串数组     孔位的数字中间是不留空格。
        val starArray4 = arrayOf("男", "女") //用来表示孔位字符串数组     孔位的数字中间是不留空格。
        Tools.initSpinner(this, ageUnit, starArray3)
        Tools.initSpinner(this, gender, starArray4)
        if (ageUnit.selectedItem.toString() == "周") {
            ageUnit.setSelection(1)
        }
        if (gender.selectedItem.toString() == "女") {
            gender.setSelection(1)
        }
        btnConfirm.setOnClickListener(View.OnClickListener {
            if (nameEdit.text.toString() == "" || age.text.toString() == "") {
//                    Toast.makeText(TestPage1.this, "请补全高级信息参数", Toast.LENGTH_SHORT).show();
                Toast.makeText(this, "请补全高级信息参数", Toast.LENGTH_SHORT).show()
                Log.d("请补全参数", ".....")
                return@OnClickListener
            }
            //1.姓名：
            nameStr = nameEdit.text.toString()
            //2.年龄
            val a = age.text.toString().toInt()
            if (a in 1..99) {
                this.age = a.toString()
            } else {
                Toast.makeText(this, "年龄参数有误", Toast.LENGTH_SHORT).show()
                return@OnClickListener
            }
            //3.年龄单位
            this.ageUnit = ageUnit.selectedItem.toString()
            //4.性别
            genderStr = gender.selectedItem.toString()
            dialog.dismiss()
        })
        btnCancel.setOnClickListener { dialog.dismiss() }
        if (patiDialogFlag) {
            dialog.show()
            patiDialogFlag = false
        }
        //调整dialog 的View 宽度。
        dialog.window!!.setLayout(
            LinearLayout.LayoutParams.WRAP_CONTENT,
            LinearLayout.LayoutParams.WRAP_CONTENT
        ) //通过此方式来设置dialog 的宽高
        val mWindow = dialog.window
        val lp = mWindow!!.attributes
        lp.x = -200 //新位置X坐标
        lp.y = -150 //新位置Y坐标
        dialog.onWindowAttributesChanged(lp)
    }

//endregion



//region结果列表
    lateinit var adapter: ItemAdapter1
    fun renewRecycleView(n:Int){
        val layoutManager = LinearLayoutManager(this)
        RecyclerTest?.layoutManager = layoutManager
        for (i in 1..20) {

                adapter = ItemAdapter1(resultTotalList[i-1].list)

        }
        RecyclerTest?.adapter = adapter

//        if(n==1){
//             adapter = ItemAdapter1(resultList)
//        }else if (n==2){
//             adapter = ItemAdapter1(resultList2)
//        }

    }

    //子菜单结果显示表单数据集合
//    private val resultList= ArrayList<ResultItem>()
//    //初始化结果集合
//    private fun initResult() {
//        var id =0
//        repeat(20){
//            id++
//            val calendar=Calendar.getInstance()
//            resultList.add(ResultItem(id,"PG1",2.5,getCurrentTime().run { Str_Time }.toString(),getCurrentTime().run { Str_Time }.toString(),calendar.time.toString()))
//        }
//    }

//    //子菜单结果显示表单数据集合
//    private val resultList2 = ArrayList<ResultItem>()
//    //初始化结果合集
//    private fun initResult2(){
//        var id=0
//        repeat(20){}
//        id++
//        val calendar = Calendar.getInstance()
//        resultList2.add(ResultItem(id,"PG1",2.3,"2.5~10",calendar.time.toString()))
//    }
    //endregion

    private val resultTotalList = ArrayList<ResultItemList>()
     private var resultItem1= ArrayList<ResultItem>()
//    lateinit var resultItem2: ResultItem
     private var idFlag:Int=0
    @SuppressLint("SuspiciousIndentation")
    private fun initResultList(){
        for (i in 1..20) {
            val calendar = Calendar.getInstance()

                    resultItem1 .add(ResultItem(i, "PG1", 2.5, getCurrentTime().run { Str_Time }.toString(), getCurrentTime().run { Str_Time }.toString(), calendar.time.toString()))

//                val subresultItem = ArrayList<ResultItem>()
//                subresultItem.add(resultItem1)
//            subresultItem.add(resultItem2)
            val resultList1 = ResultItemList(resultItem1)
            resultTotalList.add(resultList1)
            }

//        for (i in 1..20){
//        val calendar = Calendar.getInstance()
//            when(idFlag){
//                0->{
//                    resultTotalList.add(ResultItem(i,"PG1",2.5,getCurrentTime().run { Str_Time }.toString(),getCurrentTime().run { Str_Time }.toString(),calendar.time.toString()))
//                }
//                1->{
//                    resultTotalList.add(ResultItem(i,"CRPTest",2.5,getCurrentTime().run { Str_Time }.toString(),getCurrentTime().run { Str_Time }.toString(),calendar.time.toString()))
//                }
//            }
//        }
    }

    private val starArray5= arrayOf("0","1", "2", "3", "4", "5", "6", "7", "8", "9")

    /*初始化孔位*/
    /*无条码*/
    private fun initTaskList() {
        for (i in 0..19) {
            if (i< 9) {
                taskList.add(TaskItem(1, 100, false, "EE040100303"+starArray5[i + 1]+"303030FF", taskStatus.WaitForCard, i.toString(), starArray1[0]))
            }
            else if (i in 9 until 19) {
                taskList.add(TaskItem(1, 100, false, "EE040100313"+starArray5[i -9]+"303030FF", taskStatus.WaitForCard, i.toString(), starArray1[0]))
            }
            else  {
                taskList.add(TaskItem(1, 100, false, "EE0401003230303030FF", taskStatus.WaitForCard, i.toString(), starArray1[0]))
            }

                for (i in 0 until taskList.size) {
                    Log.d("sendHex", taskList[i].sendHex)
                }
            }
    }
    /*有条码*/
    private fun initTaskList2() {
        for (i in 0..19) {
            if (i< 9) {
                taskList2.add(TaskItem(1, 100, false, "EE040101303"+starArray5[i + 1]+"303030FF", taskStatus.WaitForCard, i.toString(), starArray1[0]))
            }
            else if (i in 9 until 19) {
                taskList2.add(TaskItem(1, 100, false, "EE040101313"+starArray5[i -9]+"303030FF", taskStatus.WaitForCard, i.toString(), starArray1[0]))
            }
            else  {
                taskList2.add(TaskItem(1, 100, false, "EE0401013230303030FF", taskStatus.WaitForCard, i.toString(), starArray1[0]))
            }

            for (i in 0 until taskList2.size) {
                Log.d("sendHex", taskList2[i].sendHex)
            }
        }
    }


   private lateinit var Insertiontime: TextView
   private lateinit var  Scantime:TextView

    /**用来扫描下位机的插卡状态*/
    private val timer1 = Timer()
    /**孵育时间 */
    var getInsertiontimeFlag:Boolean=false
    var getScantimeFlag: Boolean= false
    private val incubationTime : Int=100  //现阶段设为固定
    var recCardFlag:Boolean = false
    var countTime:Int=0     //孵育的剩余时间
    /**定时器：用来给各个试剂卡槽中的卡进行计时*/
    private fun timer1run() {
        timer1.scheduleAtFixedRate(object:TimerTask(){
            @SuppressLint("ResourceType", "SetTextI18n")
            override fun run() {
                val dialogView = View.inflate(this@Test, R.layout.item_list1, null)
                val calendar = Calendar.getInstance()
                Insertiontime = dialogView.findViewById(R.id.Insertiontime)
                Scantime = dialogView.findViewById(R.id.Scantime)

                var insertiontime: Int = 0
                var scantime: Int = 0

                for (i in 0 until slotnumList.size)
                    if (getInsertiontimeFlag) {
                        val date1 = Calendar.getInstance()
                        val a = date1.time
                        val timeInMills1 = date1.timeInMillis
                        insertiontime = timeInMills1.toInt()
                        resultItem1.add(ResultItem(i, "PG1", Result, a.toString(), getCurrentTime().run { Str_Time }.toString(), calendar.time.toString()))
//                        resultTotalList[testindex].list.add(ResultItem(i, "PG1", Result, a.toString(), getCurrentTime().run { Str_Time }.toString(), calendar.time.toString()))
                        if (getScantimeFlag) {
                            val date2 = Calendar.getInstance()
                            val b = date2.time
                            val timeInMills2 = date2.timeInMillis
                            scantime = timeInMills2.toInt()
                            resultItem1.add(ResultItem(i, "PG1", Result, a.toString(), b.toString(), calendar.time.toString()))
//                            resultTotalList[testindex].list.add(ResultItem(i, "PG1", Result, a.toString(), b.toString(), calendar.time.toString()))
                        }
                Thread.sleep(1000)
            }

                if(recCardFlag==true) {
                var t =scantime - insertiontime                       //插卡时间和孵育时间的时间差
                countTime = incubationTime - t

                    for (i in 0 until taskList.size){
                        if (countTime>0){
                            if (taskList[i].Status==taskStatus.WaitForTesting){
                                countTime--
                            }
                        }
                    }
                    countTime=0
                }

//region  从插卡开始孵育计时
//                if (!recCardFlag) {
//                    for (i in 0 until taskList.size) {
//                        countTime++
//                        if (countTime > 0) {
//                            if (taskList[i].Status == taskStatus.WaitForTesting) {
//                                taskList[i].counter--
//                                if (taskList[i].counter == 0) {
//                                    taskList[i].Status = taskStatus.Complete//还有丢卡动作未进行
////                                    Log.d("TAG", taskList[i].sendHex)
////                                    SerialHelper1.sendHex(taskList[i].sendHex)
//                                }
//                            }
//                        }
//                    }
//                }else{
//                    for (i in 0 until taskList.size){
//                        taskList[i].Status = taskStatus.WaitForCard
//                    }
//                }
//                countTime=0

/*计时*/
//                countTime+=50
//                if (countTime==1000){
//                    for (i in 0 until taskList.size){
//                        if (taskList[i].Status==taskStatus.WaitForCard) {
//                            taskList[i].counter--
//                            if (taskList[i].counter==0)
//                                taskList[i].Status=taskStatus.WaitForTesting
//                        }
//                    }
//                    countTime=0
//                }
                //endregion

                val msg=Message()
                msg.what=updateStatus
                handler.sendMessage(msg)
            }

        },0,50)
    }



    private var setCmdFlag:Boolean=false
    private var executeCmdFlag:Boolean=false

    private fun taskExecution(str: String){
        while (setCmdFlag){ //指令下达成功
            sendHexCmd1=str
            SerialHelper1.sendHex(str)
            Thread.sleep(2000)
        }
        while (executeCmdFlag){//指令执行完成
            Thread.sleep(1000)
        }
    }

   /**线程锁*/
    private val lock = Object()
    /*线程暂停变量*/
    private var pause=false
   /*调用这个方法实现暂停线程*/
    private fun pauseThread(){
        pause=true
    }
    private fun onPauseOfThrea(){
        synchronized (lock) {
            lock.wait()
        }
    }
    private fun resumeThread() {
        pause = false
        synchronized (lock) {
            lock.notifyAll()
        }
    }

//region/*曲线弹窗*/
/*截取字符串*/
    private val str =StringBuilder()
    var Datalist = DoubleArray(380)
    fun dataProcess1(str: String): DoubleArray? {
        val e = str.slice(str.length - 2..str.length - 1)
        val f = str.slice(2..str.length - 3)
        if (e == "FF") {
            var i = 0
            while (i < f.length) {
                val n = i / 4
                val strX1 = f.substring(i, i + 4)
                Log.d("strX1", strX1)
                val anInt1 = strX1.toInt(16)
                Log.d("anInt1", anInt1.toString())
                val a = anInt1.toDouble()
                Datalist[n] = a
                i += 4
                Log.d("i", i.toString())
            }

        }
        return Datalist
    }

    /*曲线弹窗*/
    private var graphDialogFlag = false
    private fun graphDialog(){
        val builder = AlertDialog.Builder(this)
        val dialog = builder.setCancelable(false).create()
        val dialogView = View.inflate(this, R.layout.dialog_graph, null)
        /*设置对话框*/
        dialog.setView(dialogView)
        val Back = dialogView.findViewById<View>(R.id.Confirm1) as Button
        val TestCurve = dialogView.findViewById<View>(R.id.TestCurve) as Button
        TestCurve.setOnClickListener(View.OnClickListener {
            showCurve(1)
            })

        Back.setOnClickListener { dialog.dismiss() }
        if (graphDialogFlag){
            dialog.show()
            graphDialogFlag = false
        }
        //调整dialog 的View 宽度。
//        dialog.window!!.setLayout(
//            LinearLayout.LayoutParams.WRAP_CONTENT ,
//            LinearLayout.LayoutParams.WRAP_CONTENT
//        ) //通过此方式来设置dialog 的宽高
        val mWindow = dialog.window
        val lp = mWindow!!.attributes
        lp.x = 0 //新位置X坐标
        lp.y = 0 //新位置Y坐标
        lp.height = 650
        lp.width = 850
        dialog.onWindowAttributesChanged(lp)
    }

/*曲线表*/
    private fun showCurve(Flag: Int) {
        val xDataList: MutableList<String> = ArrayList() // x轴数据源
        val yDataList: MutableList<Entry> = ArrayList() // y轴数据数据源
        if (Flag == 2) {
//            Log.d("Datalist", Datalist1.get(1).toString())
            for (i in Datalist.indices) {
                // x轴显示的数据
                xDataList.add(i.toString())
                //y轴生成float类型的随机数
                yDataList.add(Entry(Datalist.get(i).toFloat(), i))
                Log.d("Datalist", Datalist.get(i).toString())
            }
            ChartUtil.showChart(this, lineChart, xDataList, yDataList, "电压峰值图", "电压峰值", "")
            if (Datalist.size != 0) {
                Log.d("Datalsit", "!0")
            } else {
                Log.d("Datalsit", "0")
            }
        }
        if (Flag == 1) {
//             给上面的X、Y轴数据源做假数据测试
            for (i in 0..379) {
                // x轴显示的数据
                xDataList.add("")
                //y轴生成float类型的随机数
                val value = (Math.random() * 20000).toFloat() + 100
                yDataList.add(Entry(value, i))
            }
            //显示图表,参数（ 上下文，图表对象， X轴数据，Y轴数据，图表标题，曲线图例名称，坐标点击弹出提示框中数字单位）
            ChartUtil.showChart( this@Test , lineChart, xDataList, yDataList, "趋势图", "", "")
        }
    }
//endregion


//region/*读取是否有卡*/
    var getcardflag:Boolean=true
    private fun querycard(){
        if (!getcardflag){
            for (i in 0 until taskList.size){
                taskList[i].Status=taskStatus.WaitForTesting
                SerialHelper1.sendHex("EE0103043030303030FF")
            }
        }else{
            for (i in 0 until taskList.size){
                taskList[i].Status = taskStatus.WaitForCard
            }
        }
//        getcardflag=true
//        while (getcardflag){
//            Thread.sleep(500)
//        }
        Thread.sleep(1000)
        Log.d("跳出LOOP","0000")
    }
//endregion

    /*子项目集合*/
    private val IDitemList= ArrayList<IDReadBean>()
    /**下位机返回的ID信息的应答码 */
    private var receiveIdFlag = false

    /**下位机返回的字节数组 */
    lateinit var receiveIDByte:ByteArray

   //region 完整的id卡信息
    /**
     * 建表信息是一条子项目信息对应一条存储数据表信息，要获取一条id卡的完整信息，
     * 也就通过获取id卡中每个子项目中共有的整体项目名称，通过它来获取所有该条ID所包含
     * 的全部子项目信息，即为完整的id卡信息
     * */
    private fun queryID1(){
        IDitemList.clear()
        var numofSub=0//子项目数据
        SerialHelper1.sendHex("EE2402FF")
        while (!receiveIdFlag){//先获取子项目数
            try {
                Thread.sleep(200)
            }catch (e:InterruptedException){
                e.printStackTrace()
            }
        }
        receiveIdFlag=false
        numofSub=receiveIDByte[receiveIDByte.size-2].toInt()
        for (n in 0 until numofSub){
            val idReadBean=IDReadBean()
            for (i in 1..12){
                val sendHex="EE24"+Tools.byteToHex(i).toString()+"FF"
                SerialHelper1.sendHex(sendHex)
                Log.d("sendhex",sendHex)
                while (!receiveIdFlag){
                    try {
                        Thread.sleep(200)
                    }catch (e:InterruptedException){
                        e.printStackTrace()
                    }
                }
                receiveIdFlag=false
                idReadBean.readIDCard(receiveIDByte)
            }
            IDitemList.add(idReadBean)
        }
        //整体保存idReadBean.save()
        for (n in IDitemList.indices){
            Log.d("子项目信息",IDitemList[n].toString())
        }
    }
 //endregion

    private var IDnum:String=""
    private var receive1: String=""
    /**串口1指令下达指令 */
    private var sendHexCmd1:String=""
    private var receiverMode :Int= 0

//region/线程接受处理函数
    private fun recThread(int: Int) {
        while(true){
            if(SerialHelper1.receiverFlag) {
                Log.d("receiveData", SerialHelper1.receiveHexStr)

                var a: String = ""
                a = SerialHelper1.receiveHexStr
                val h = a.slice(a.length-1..a.length-1)
                /*字符串拼接*/
                if (h == "FF") {
                    voltageflag
                }

                SerialHelper1.receiverFlag=false
                when (SerialHelper1.receiveHexStr) {
                    sendHexCmd1 -> {
                        setCmdFlag=true
                        Log.d("SetCmd","Success")//指令下达成功
                    }
                    sendHexCmd1+"BB" -> {
                        executeCmdFlag=true
                        Log.d("executeCmd","Success")//指令执行成功
                    }
                    else -> {
                        Log.d("SendHex","Test")//指令下达成功
                    }
                }
                when(int){
                    0-> dataProcessing0(SerialHelper1.comBeanRec)
//                    1-> dataProcessing1(SerialHelper1.comBeanRec)
//                    2-> dataProcessing2(SerialHelper1.comBeanRec)
                }
            }
        }
    }

/*扫码枪串口ttyS2*/
    private fun recThread1(int: Int) {
        while(true){
            if(SerialHelper2.receiverFlag){
                Log.d("receiveData",SerialHelper2.receiveHexStr)
                /*接收ID卡号*/
                IDnum = SerialHelper2.receiveAsciiStr
                sendMessage(2)
                receive1 = IDnum

                getcardflag = true
                getScantimeFlag = true
                SerialHelper2.receiverFlag=false
            }
        }
    }
    //endregion

    private var Result:Double = 0.0
    val dataStr:String=""
    /**处理直接HexString*/
    private fun dataProcessing1(dataStr: String) {
        //处理接收到的电压曲线字符串：
//        val dataStr:String=SerialHelper1.receiveHexStr;
        for (i in 0..slotnumList.size) {
            println(dataStr)
            val peak = ResultAnalysis()
//            if (i==0){
//                val peakValue1=peak.getPeak(dataStr)[0]
//                Log.d("Peak1",peakValue1.toString())
//                var result1=peakValue1
//                Result=(result1*100).roundToInt().toDouble()/100
//                println(Result)
//           }
//            else if (i==1) {
                val peakValue1 = peak.getPeak(dataStr)[0]
                val peakValue2 = peak.getPeak(dataStr)[1]
                Log.d("Peak1", peakValue1.toString())
                Log.d("Peak2", peakValue2.toString())
                var result= peakValue2 / peakValue1
                Result = (result * 100).roundToInt().toDouble() / 100
                println(Result)
            receive1 = Result.toString()
            sendMessage(3)
//            }else if (i==2){
//                val peakValue1 = peak.getPeak(dataStr)[0]
//                val peakValue2 = peak.getPeak(dataStr)[1]
//                val peakValue3 = peak.getPeak(dataStr)[5]
//                Log.d("Peak1", peakValue1.toString())
//                Log.d("Peak2", peakValue2.toString())
//                Log.d("Peak3",peakValue3.toString())
//                var result= peakValue2 / peakValue1
//                Result = (result * 100).roundToInt().toDouble() / 100
//                println(Result)
//            }
            val calendar = Calendar.getInstance()
            Log.d("taskList.size", taskList.size.toString())
            Log.d("testIndex", (testindex ).toString())
            taskList.get(testindex).Status = taskStatus.Complete
            Log.d("taskList222", taskList.get(testindex).Status.toString())
            resultTotalList[testindex].list.add(ResultItem(i,"PG1", result,getCurrentTime().run { Str_Time }.toString(),getCurrentTime().run { Str_Time }.toString(),calendar.time.toString()))
//            resultTotalList.add(ResultItem(i,"CRPTest",result,getCurrentTime().run { Str_Time }.toString(),getCurrentTime().run { Str_Time }.toString(),calendar.time.toString()))
        }
    }


//    /**转换的ASCII文本字符串*/
//    private fun dataProcessing2(comBeanRec: ComBean) {
//
//    }

//region/*字节码接收和解析*/
/*
* slotnumList ：插入卡的卡槽号列表
* slotnum：拼接接收的卡槽号
* slotnum1：对拼接后的slotnum及逆行输出，使用
* */
    private var voltageflag:Boolean = false
    private val slotnumList = arrayListOf<Int>()
    private val voltage = arrayListOf<Int>()
    private val  slotnum = StringBuilder()
    private var slotnum1:Int=0
    /**数据接收处理：字节码*/
    private fun dataProcessing0(comBean: ComBean) {
        val recByte: ByteArray = comBean.bReC
        val size :Int =recByte.size
        var arr = arrayListOf<Int>()
        for (i in 0 until size){  //byte数组转int数组
            if (recByte[i]<0){
//                arr[i]= 256+recByte[i]
                arr.add(256+recByte[i])
            }else {
//                arr[i]= recByte[i]+0
                arr.add(recByte[i]+0)
            }
        }
        for (i in 0 until size){
            println(arr[i])
            Log.d("arr",arr.toString())
        }
        if (arr.size>11) {
            if (arr[0] == 0xEE) {
                voltage.clear()
            }
            for (i in 0..arr.size-1){
                    voltage.add(arr[i])
            }
            if (voltage[voltage.size-1]== 0xFF){
                when(voltage[1]){
                    0x07->{
//                    接收到电压曲线
                        val subArray= voltage.slice(2..voltage.size-2)  //切割数组。
                        println(subArray)//换行输出
                        Log.d("TAG1",bytesToHex(subArray))
                        val length =SerialHelper1.receiveHexStr.length
                        Log.d("TAG2",SerialHelper1.receiveHexStr.substring(2,length-2))
                        dataProcessing1(bytesToHex(subArray))

                    }
                }
            }
        }

        if (arr[0]==0xEE&&arr[size-1]==0xFF){
            Log.d("TAG","收到正确指令")
            when(arr[1]){
                0x01->{
                    Log.d("TAG","条码值")
                    val subArray = arr.slice(2..arr.size-2)
                    println(subArray.toString())
                    Log.d("TAG",subArray.toString())
                }
                0x02->{
                    Log.d("TAG","2")
                }
                0x03-> {//检测卡槽状态
                    if (cardinFlag==true) {
                        getInsertiontimeFlag = true
                        Log.d("TAG", "卡位信息")
                        val subArray = arr.slice(4..arr.size - 5)
                        println(bytesToHex(subArray))
                        println(subArray)
                        val num = bytesToHex(subArray)
                        Log.d("slotnum", slotnum.toString())
                        val num2 = num.slice(1..1)
                        val num3 = num.substring(3)
                        slotnum.append(num2)
                        slotnum.append(num3)
                        slotnum1 = slotnum.toString().toInt()
                        Log.d("slotnum1", slotnum1.toString())

                        for (i in 0 until taskList.size) {
                            if (slotnum1  == i) {
                                taskList[i-1].isHave = true
                                taskList[i-1].counter = incubationTime
                                taskList[i-1].Status = taskStatus.WaitForTesting
                                getcardflag = true
                            }
                        }

                        if (slotnum1==1){
                            idFlag=0
                            sendMessage(4)
                        }
                        if (slotnum1==2){
                            idFlag=1
                            sendMessage(4)
                        }

                        slotnumList.add(slotnum1)
                        Log.d("slotnumList", slotnumList.toString())
                        slotnum.clear()
                        Log.d("TAG", "3")
                    }
                }
                0x04->{
         }

                0x05->{
                    val subArray = arr.slice(4..5)
                    Log.d("TAG","5")
                }
                0x06->{
                    Log.d("TAG","6")
                }
            }
        }
    }
//endregion

//region/* bytesToHex*/
    private fun bytesToHex(bytes: List<Int?>): String {
        val hexArray = "0123456789ABCDEF".toCharArray()
        val hexChars = CharArray(bytes.size * 2)
        for (j in bytes.indices) {
            val v = bytes[j]?.and(0xFF)
            if (v != null) {
                hexChars[j * 2] = hexArray[v.ushr(4)]
                hexChars[j * 2 + 1] = hexArray[v.and(0x0F)]
            }
        }
        return String(hexChars)
    }
    //endregion



    override fun onCheckedChanged(p0: CompoundButton?, p1: Boolean) {
        when(p0?.id){
            R.id.switch_model ->{
                testmodel = p1
                if (testmodel){
                    Log.d("testMode","ture")
                }else
                    Log.d("testMode","false")
            }
        }
    }

    /*下拉列表配置*/
    @SuppressWarnings("ResourceType")
    private fun initSpinner(context: Context?, sp:Spinner, Strs:Array<String>){
        //声明一个下拉列表的数组适配器
        val starAdapter = ArrayAdapter(context!!, R.layout.item_select, Strs)
        //设置数组适配器的布局样式
        starAdapter.setDropDownViewResource(R.layout.item_drapdown)
        //从布局中获取名为sp_dialog的下拉框
//        Spinner sp=findViewById(R.id.spinner)
        //设置下拉框的标题，不设置就没有难看的标题了
        sp.prompt="动作名称"
        //设置下拉框的数组适配器
        sp.adapter=starAdapter
        //设置下拉框的默认的第一选项
        sp.setSelection(0)
        //给下拉框设置选择监听器，一旦用户选中某一项，就触发监听器onItemSelect方法
        if ("1"==Strs[0]){ //用来判断当前下拉框的目标对象。
            sp.onItemSelectedListener = MySelectedListener()
        }
    }

    internal class MySelectedListener():AdapterView.OnItemSelectedListener{
        override fun onItemSelected(adapterView: AdapterView<*>?, view: View?, i: Int, l: Long) {
            for(n in starArray1.indices){
                if (taskList[i].sampleType== starArray1[n]){
                    SampleType.setSelection(n)
                }
            }
            IntNo.setText(taskList[i].sampleId)
        }

        override fun onNothingSelected(p0: AdapterView<*>?) {
        }
    }

    @SuppressLint("ClickableViewAccessibility")
    private fun editHideInput(){
        for (i in 0 until editTextList.size){
            editTextList[i].setOnTouchListener { v, event ->
                if (event.action == MotionEvent.ACTION_UP) {
                    if (event.rawX < editTextList[i].left || event.rawX > editTextList[i].right) {
                        // User clicked outside the EditText, hide the keyboard
                        val inputMethodManager = getSystemService(INPUT_METHOD_SERVICE) as InputMethodManager
                        inputMethodManager.hideSoftInputFromWindow(editTextList[i].windowToken, 0)
                        return@setOnTouchListener true
                    }
                }
                false
            }
        }
    }

    override fun dispatchTouchEvent(ev: MotionEvent?): Boolean {
        if (ev?.action == MotionEvent.ACTION_DOWN) {
            val view = currentFocus
            if (KeyboardsUtils.isShouldHideKeyBord(view, ev)) {
                KeyboardsUtils.hintKeyBoards(view)
            }
        }
        return super.dispatchTouchEvent(ev)
    }

    /**handle消息发送函数 */
    private fun sendMessage(int: Int){
        val message = Message()
        message.what = int
        handler.sendMessage(message)
    }


    override fun onDestroy() {
        super.onDestroy()
        pauseThread()
        taskList.clear()
        timer1.cancel()
        println("退出测试线程")
    }


    /**这个表示 完整的日期时间：例如2022-10-13 14:23 */
    private var TimeStr = ""
    private var testDay = 0
    private var Str_Date: String? = null
    private  var Str_Time:String? = null
    private fun getCurrentTime() {
        val calendar = Calendar.getInstance() //取得当前时间的年月日 时分秒
        val year = calendar[Calendar.YEAR]
        val month = calendar[Calendar.MONTH] + 1
        val day = calendar[Calendar.DAY_OF_MONTH]
        val hour = calendar[Calendar.HOUR_OF_DAY]
        val minute = calendar[Calendar.MINUTE]
        val second = calendar[Calendar.SECOND]
//        Str_Date = year.toString() + "年" + month + "月" + day + "日"
//        val Month_Str: String
//        val Day_Str: String
//        val date_Str: String
//        Day_Str = if (day < 10) {
//            "0$day"
//        } else {
//            "" + day
//        }
//        Month_Str = if (month < 10) {
//            "0$month"
//        } else {
//            "" + month
//        }
//        date_Str = year.toString() + Month_Str + Day_Str //整形的日期时间。
//        testDay = date_Str.toInt()
        val StrMinute: String
        val StrSecond: String
        StrMinute = if (minute < 10) {
            ":0$minute"
        } else {
            ":$minute"
        }
        StrSecond = if (second < 10) {
            ":0$second"
        } else {
            ":$second"
        }
         Str_Time = hour.toString()+ StrMinute + StrSecond + ""

//        TimeStr = "$year-$month-$day $Str_Time"
    }


    @SuppressLint("SimpleDateFormat")
    fun Date(Strs: Array<String>){
        // 格式化日期
        val sdf = SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS")
        val time = Date() // 获取当前时间
        val format: String = sdf.format(time) // 格式化时间

        // 时间转换为时间戳
        // getTime返回自1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总毫秒数.
        // 时间戳是指自1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数
        // 单位换算：1秒=1000毫秒
        val timestamp = time.time / 1000L
        println("当前时间：$time")
        println("当前时间（格式化）：$format")
        println("当前时间戳：$timestamp")


        // 时间戳转化为时间
        val time2 = Date(timestamp * 1000L)
        val time3: String = sdf.format(time2)
        println("时间戳转换来的时间：$time2")
        // 这里会有精度损失，是因为时间戳是秒数
        println("格式化后的转换时间：$time3")
    }

    /*时分秒转换成秒*/
    fun formatTurnSecond(time: String):Long {
        val index1 = time.indexOf(":")
        val index2 = time.indexOf(":", index1 + 1)
        val hh = time.substring(0, index1).toInt()
        val mi = time.substring(index1 + 1, index2).toInt()
        val ss = time.substring(index2 + 1).toInt()
        Log.e(TAG, "formatTurnSecond: 时间== " + hh * 60 *60 + mi * 60 + ss)
        return (hh * 60 * 60 + mi * 60 + ss).toLong();
    }


    fun a(args: Array<String>) {
        //获取当前日期时间
        val date = Date()
        //实例化日期对象
        val calendar = Calendar.getInstance()
        //使用给定的Date设置此Calendar的时间
        calendar.time = date
        //以毫秒为单位返回Calendar的时间值
        val timeInMillis = calendar.timeInMillis
        //输出验证
        println(timeInMillis)
    }

    }




