package com.example.twentychannelspoct.utils;


import android.util.Log;

import java.io.UnsupportedEncodingException;

import android_serialport_api.SerialHelper;

/**
 * **************************
 * 项目名称：SinglePOCT
 *
 * @Author BigHang
 * 创建时间：2022/8/2  10:26
 * 用途: 用来封装打印相关的数据信息：
 * **************************
 */

public class PrintfUtil {
    SerialHelper serialHelper;
    /**样本号*/
    private String SampleNumber;
    /**流水号*/
    private String SerialNumber;
    /**姓名*/
    private String Name;
    /**年龄*/
    private String age;
    /**年龄单位*/
    private String ageUnit;
    /**性别*/
    private String gender;
    /**测试时间*/
    private String testTime;
    /**打印时间*/
    private String printfTime;
    /**血清、血浆类的样本类型*/
    private int sampleType;
    /**测试结果*/
    private String ResultValue;
    /**结果单位*/
    private String ResultUnit;

    /**测试子项目名称*/
    private String SubProjectName;


    /**报告单类型对应的应该是测试项目*/
    private int ReportType;
    /**条码开关标志位：*/
    private boolean BarcodeSwitch;



    public String getSerialNumber() {
        return SerialNumber;
    }

    public void setSerialNumber(String serialNumber) {
        SerialNumber = serialNumber;
    }

    public String getResultValue() {
        return ResultValue;
    }

    public void setResultValue(String resultValue) {
        ResultValue = resultValue;
    }

    public String getResultUnit() {
        return ResultUnit;
    }

    public void setResultUnit(String resultUnit) {
        ResultUnit = resultUnit;
    }

    public SerialHelper getSerialHelper() {
        return serialHelper;
    }
    public void setSerialHelper(SerialHelper serialHelper) {
        this.serialHelper = serialHelper;
    }

    public String getSampleNumber() {
        return SampleNumber;
    }

    public void setSampleNumber(String sampleNumber) {
        SampleNumber = sampleNumber;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getAge() {
        return age;
    }

    public void setAge(String age) {
        this.age = age;
    }

    public String getAgeUnit() {
        return ageUnit;
    }

    public void setAgeUnit(String ageUnit) {
        this.ageUnit = ageUnit;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public boolean isBarcodeSwitch() {
        return BarcodeSwitch;
    }

    public void setBarcodeSwitch(boolean barcodeSwitch) {
        BarcodeSwitch = barcodeSwitch;
    }

    public String getTestTime() {
        return testTime;
    }

    public void setTestTime(String testTime) {
        this.testTime = testTime;
    }

    public String getPrintfTime() {
        return printfTime;
    }

    public void setPrintfTime(String printfTime) {
        this.printfTime = printfTime;
    }

    public int getSampleType() {
        return sampleType;
    }

    public void setSampleType(int sampleType) {
        this.sampleType = sampleType;
    }

    public int getReportType() {
        return ReportType;
    }

    public void setReportType(int reportType) {
        ReportType = reportType;
    }

    public String getSubProjectName() {
        return SubProjectName;
    }

    public void setSubProjectName(String subProjectName) {
        SubProjectName = subProjectName;
    }

    public void Print_testItem(){

    }
    //打印公司抬头：
    private void Print_CompanyTitle(){
        SendHex(enUnicode("重庆新三亚生物制药有限公司"));
        newline();
    }
    private void Print_Report(int flag){
        switch (flag){
            case 0:
                String HexStr =enUnicode("C反应蛋白");
                SendHex(enUnicode("C反应蛋白"));
                break;
            default:
                break;
        }
        for (int a=0;a<3;a++){
//            SendHex("7f");
            SendHex(enUnicode(" "));
        }
        SendHex(enUnicode("报告单"));
        newline();
    }
    //流水号：
    private void Print_SerialNumber(){
        //当前获取的日期和上次实验的流水号不相同时，更新流水编号。
        SendHex(enUnicode("流 水 号:"));
        //获取流水号：
        SendHex(enUnicode(SerialNumber));
        newline();
    }
    //    private String[] starArray1 = { "全血", "血清" ,"血浆","血清/血浆","末梢血","质控","其它"};
    //样本类型：
    private void Print_SampleType(int  type){
        SendHex(enUnicode("样本类型： "));
        switch (type){
            case 0:
                SendHex(enUnicode("全血"));
                break;
            case 1:
                SendHex(enUnicode("血清"));
                break;
            case 2:
                SendHex(enUnicode("血浆"));
                break;
            case 3:
                SendHex(enUnicode("血清/血浆"));
                break;
            case 4:
                SendHex(enUnicode("末梢血"));
                break;
            case 5:
                SendHex(enUnicode("质控"));
                break;
            case 6:
                SendHex(enUnicode("其它"));
                break;
            default:
                break;
        }
        newline();
    }
    //  检测时间
    private void Print_DetectionTime(){
        SendHex(enUnicode("检测时间： "));
        if(testTime!=null){
            SendHex(enUnicode(testTime));
        }else {
            Log.d("当前未设置","gender");
        }
        newline();
    }
    //  分割线
    private void Print_Separator(){
        for(int a=0;a<30;a++){
            SendHex(enUnicode("-"));
        }
        newline();
    }

    private void Print_TestItem(){  //打印   检测项目    结果    单位
        SendHex(enUnicode("检测项目:"));
        for(int a=0;a<5;a++){
            SendHex(enUnicode(" "));
        }
        SendHex(enUnicode("结果"));
        for(int a=0;a<5;a++){
            SendHex(enUnicode(" "));
        }
        SendHex(enUnicode("单位"));
        newline();
    }

    private void Print_Reference(int range){//打印     "检测项目   结果   参考范围"
        SendHex(enUnicode("(参考范围： "));
        SendHex(enUnicode("< "+range+" "));
        SendHex(enUnicode("mg/L： )"));
        newline();
    }

    private void Print_Warning(){//打印  本结果只对本份标本负责！
        SendHex(enUnicode("本结果只对本份标本负责！"));
    }

    private void Print_SET(){////////////////打印机设置//////////////////
        SendHex("1F2D550105");
        try {
            Thread.sleep(0x0A);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    //发送一次测试结果
    private void Print_TestResult(String Data) {
        for(int a=0;a<7;a++){
            SendHex(enUnicode(" "));
        }
        //get 检测数据。
        SendHex(enUnicode(Data));
        for(int a=0;a<5;a++){
            SendHex(enUnicode(" "));
        }
    }

    //打印项目结果：
    /**
     * @Author
     * @Time 2022/10/26 19:02
     * @Description 根据设定的参数，确认输出的数据结果值。
     * 这个好像是 根据曲线的数量判断输出结果。  flag ：子项目名称   num:测试结果。
     */
    private void Print_Item(String flag,String num,float a1,float a2,float a3){
        SendHex(enUnicode("   "));
        switch (flag){
            case "CRP":
                SendHex(enUnicode("CRP:"));
                Print_TestResult(num);
                SendHex(enUnicode("mmol/L "));
                break;
            case "PG1":
                SendHex(enUnicode("PG1:"));
                Print_TestResult(num);
                SendHex(enUnicode(" mmol/L "));
                break;
            case "PG2":
                SendHex(enUnicode("PG2:  "));
                Print_TestResult(num);
                SendHex(enUnicode(" mmol/L "));
                break;
        }
        newline();
    }

    //打印样本号：
    private  void Print_SampleNum(){
        SendHex(enUnicode("样 本 号: "));
        //判断当前是否设置样本号：
        if(SampleNumber!=null){
            SendHex(enUnicode(SampleNumber));
        }else {
            Log.d("当前未设置","SampleNumber");
        }
        newline();
    }
    //打印  姓名
    void Print_Name(){
        SendHex(enUnicode("姓 名: "));
        //判断当前是否设置姓名：
        if(Name!=null){
            SendHex(enUnicode(Name));
        }else {
            Log.d("当前未设置","Name");
        }
        newline();
    }
    //打印  性别
    void Print_Sex(){
        SendHex(enUnicode("性 别: "));
        //判断性别输入
        if(gender!=null){
            SendHex(enUnicode(gender));
        }else {
            Log.d("当前未设置","gender");
        }
        newline();
    }
    //打印  年龄
    void Print_Age() {
        SendHex(enUnicode("年 龄: "));
        //判断年龄设置：
        if(age!=null){
            SendHex(enUnicode(age));
        }else {
            Log.d("当前未设置","gender");
        }
        if(ageUnit!=null){
            SendHex(enUnicode(ageUnit));
        }else {
            Log.d("当前未设置","gender");
        }
        newline();
    }
    //打印  空行
    void Print_Null(){
        for (int i=0;i<2;i++){
            SendHex(enUnicode(""));
        }
        newline();
    }
    //打印当前时间
    void Print_Time(){
        if(printfTime!=null){
            SendHex(enUnicode("打印时间"+":"+printfTime));
            //获取当前时间
        }
        newline();
    }







    private void SendHex(String str) {
        serialHelper.sendHex(str);
    }

    private void newline(){
        SendHex("0A");
        try {
            Thread.sleep(100);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void Printer()
    {
  /*  1.  重庆新赛亚生物科技有限公司
  2.  C反应蛋白 报告单
  3.  流 水 号：201103049
  4.  样 本 号：
  5.  样本类型：血浆
  6.  检测时间：2020-11-03 11:01：05
  7.  -------------------------------
  8.  检测项目    结果     单位
  9.  hs-CRP      >5       mg/L
  10. CRP         15.47    mg/L
  11. (参考范围:<10 mg/L)
  12. -------------------------------
  13. 本结果只对本份标本负责！
  14. 空行
  15. 打印时间: 2020-11-03 11：01：12
  16. 空行
  */

        try {
            Thread.sleep(100);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        Print_CompanyTitle();               //1.  公司名头
        Print_Report(ReportType);                    //2.  C反应蛋白 报告单
        Print_SerialNumber();               //3.  流 水 号：201103049     (具体数字后续)
        Print_SampleNum();		              //4.  样 本 号：
        Print_SampleType(sampleType);	      //5.  样本类型：  这个需要在test界面判断一下。
        Print_Name();                       //6.  姓名：张三   (如果有)
        Print_Sex();                        //7.  性别：男     (如果有)
        Print_Age();                        //8.  年龄：30     (如果有)
        Print_DetectionTime();              //9.  检测时间：2020-11-03 11:01：05
        Print_Separator();	                //10.  -------------------------------
        Print_TestItem();                   //11.  检测项目    结果     单位
        //12.  hs-CRP      >5       mg/L
        if(BarcodeSwitch)  //条码 开
        {
            Print_Item(SubProjectName,ResultValue,1,0, 1.1f);	          //13. CRP         15.47    mg/L
        }
        else  //条码关
        {
            Print_Item(SubProjectName,ResultValue,1,0, 1.1f);	          //13. CRP         15.47    mg/L
        }
        Print_Reference(1);              //14. (参考范围:<10 mg/L)
        Print_Separator();                    //15. -------------------------------
        Print_Warning();                    //16. 本结果只对本份标本负责！
        Print_Null();                       //空行
        Print_Time();                         //18. 打印时间: 2020-11-03 11：01：12
        Print_Null();                       //空行
        Print_Null();                       //空行
        Print_Null();                       //空行
    }

    public static String enUnicode(String str) {// 将汉字转换为16进制字符串
        String st = "";
        byte[] by = new byte[0];
        try {
            by = str.getBytes("GB2312");
        } catch (UnsupportedEncodingException e) {
            throw new RuntimeException(e);
        }
        for (int i = 0; i < by.length; i++) {
            String strs = Integer.toHexString(by[i]);
            if (strs.length() > 2) {
                strs = strs.substring(strs.length() - 2);
            }
            st += strs;
        }
        return st;
    }
    //发送字符串字间延迟；
    private void ByteDelay(int n){


        try {
            Thread.sleep(n);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

}
