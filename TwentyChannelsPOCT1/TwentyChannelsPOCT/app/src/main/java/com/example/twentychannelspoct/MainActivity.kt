package com.example.twentychannelspoct

import android.content.ContentValues
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android_serialport_api.ComBean
import android_serialport_api.SerialHelper
import com.example.twentychannelspoct.bean.Book
import com.example.twentychannelspoct.bean.fruit
import com.example.twentychannelspoct.singleton.SerialHelper1.bytesToHex
import kotlinx.android.synthetic.main.activity_main.*
import org.litepal.LitePal
import org.litepal.extension.count
import org.litepal.extension.find
import org.litepal.extension.findAll
import org.litepal.tablemanager.callback.DatabaseListener

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        LitePal.registerDatabaseListener(object : DatabaseListener {
            override fun onCreate() {
            }

            override fun onUpgrade(oldVersion: Int, newVersion: Int) {
            }
        })

        SendHexBtn?.setOnClickListener {
//            serialHelper.sendHex("010203")
            val book = Book("第一行代码",552)
            val result=book.save()
            Log.d("TAG", "save result is $result,book id is ${book.id}")
            val fruit = fruit("第一行代码",125)
            val result2=fruit.save()
            Log.d("TAG", "save result is $result2,book id is ${fruit.id}")
        }

        update?.setOnClickListener {
            val cv = ContentValues()
            cv.put("name", "第二行代码")
            cv.put("page", 570)
            LitePal.update(Book::class.java, cv, 1)
        }

        Delete?.setOnClickListener {
            LitePal.deleteAll(Book::class.java,"page>?","500")
        }

        Query?.setOnClickListener {
//            LitePal.findAll(Book::class.java).forEach {
//                Log.d("TAG","book name is ${it.name},book page is ${it.page},book id is ${it.id}")
//            }
            LitePal.where("name like?","第_行代码")
                .order("page desc")
                .limit(5)
                .find(Book::class.java).forEach {
                    Log.d("TAG","book name is ${it.name},book page is ${it.page},book id is ${it.id}")
                }
//                        LitePal.findAll()<Book>(1).forEach {
//                Log.d("TAG","book name is ${it.name},book page is ${it.page},book id is ${it.id}")

            val book: Book? =LitePal.find(1)
            Log.d("TAG","book name is ${book?.name},book page is ${book?.page},book id is ${book?.id}")
            Log.d("TAG",book.toString())


            val list =LitePal.where("page>?","100").find<Book>().forEach {
                Log.d("TAG","book name is ${it.name},book page is ${it.page},book id is ${it.id}")
            }

            val count =LitePal.count<Book>()
            Log.d("TaG", count.toString())
            LitePal.findAll<Book>().forEach {
                Log.d("TAG","book name is ${it.name},book page is ${it.page},book id is ${it.id}")
            }
        }
//        LitePal.runInTransaction {
//            val result1 = LitePal.find<Book>(1)// 数据库操作1
//            val result2 = LitePal.find<Book>(2)// 数据库操作2
//            val result3 = LitePal.find<Book>(10)// 数据库操作3
//                result1 && result2 && result3
//        }
    }
    var Receive1 = ""
    /**串口连接类： */
    private lateinit var serialHelper: SerialHelper
    private val barcodeFlag = 0
    //串口初始化
    private fun initUart() {
        serialHelper = object : SerialHelper() {
            override fun onDataReceived(ComRecData: ComBean) {
                runOnUiThread(Runnable {
                    val sMsg = StringBuilder()
                    val receiveData = String(ComRecData.bReC)
                    Log.d("ASCII码", receiveData)
                    val BytesToStr: String = bytesToHex(ComRecData.bReC) //将 接收的16进制数转换成：16进制字符串
                    Log.d("msg：串口1接受数格式为：HEX", BytesToStr)
                    Log.d("msg：串口1接受数格式为：ASCLL", receiveData)
                    val data = receiveData.toCharArray() //2054.
                    if (receiveData == "0xE1") { // 判断：1.测试时判断下是否正确扫描到试剂卡的条形吗，确认当前卡时正常放置。；正确，执行测试，错误弹窗提示。
                    } else if (receiveData == "0xE2") {  //判断是否正确插入ID卡。
                    } else if (receiveData == "0xE3") {  //判断是否打测试完成，可以进行打印操作。
                    }
                })
            }
        }
        serialHelper.baudRate=9600
        serialHelper.port="/dev/ttyS1"
        Log.d("Baud", serialHelper.getBaudRate().toString())
        Log.d("setPort", serialHelper.getPort())
        serialHelper.open()
    }

    /**
     * bytes转换成十六进制字符串
     * @param   b byte数组
     * @return String 每个Byte值之间空格分隔
     */
    fun bytesToHex(bytes: ByteArray): String {
        return bytes.joinToString("") { String.format("%02x", it) }
    }
}