package com.example.twentychannelspoct.utils;


import android.app.Activity;
import android.content.Context;
import android.content.res.Configuration;
import android.os.Environment;
import android.os.StatFs;
import android.os.storage.StorageManager;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;

import com.example.twentychannelspoct.R;
import com.example.twentychannelspoct.bean.DemoBean;

import java.io.DataOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Array;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.regex.Pattern;

import androidx.appcompat.app.AppCompatActivity;

/**
 * **************************
 * 项目名称：SinglePOCT
 *
 * @Author BigHang
 * 创建时间：2022/7/5  15:26
 * 用途:   系统功能类，目前完成了①下拉菜单选中的控制逻辑  ②完成了系统内存的查询读取。
 * **************************
 */

public class Tools {


    public Tools() {
    }
    /**
     * @Author
     * @Time 2022/7/19 18:25
     * @Description 这里使用Java中的重载，来兼容参数类型。
     */

    public static void initSpinner(Context context, Spinner sp, ArrayList<String> Strs) {
        //声明一个下拉列表的数组适配器
        ArrayAdapter<String> starAdapter = new ArrayAdapter<String>(context, R.layout.item_select, Strs);
        //设置数组适配器的布局样式
        starAdapter.setDropDownViewResource(R.layout.item_drapdown);
        //从布局文件中获取名叫sp_dialog的下拉框
//        Spinner sp = findViewById(R.id.spinner);
        //设置下拉框的标题，不设置就没有难看的标题了
        sp.setPrompt("动作名称");
        //设置下拉框的数组适配器
        sp.setAdapter(starAdapter);
        //设置下拉框默认的显示第一项
        sp.setSelection(0);
        //给下拉框设置选择监听器，一旦用户选中某一项，就触发监听器的onItemSelected方法
        starArray4=Strs;
        sp.setOnItemSelectedListener(new MySelectedListener4());
    }

    public static void initSpinner(Context context,Spinner sp, String[] Strs) {
        //声明一个下拉列表的数组适配器
        ArrayAdapter<String> starAdapter = new ArrayAdapter<>(context, R.layout.item_select, Strs);
        //设置数组适配器的布局样式
        starAdapter.setDropDownViewResource(R.layout.item_drapdown);
        //从布局文件中获取名叫sp_dialog的下拉框
//        Spinner sp = findViewById(R.id.spinner);
        //设置下拉框的标题，不设置就没有难看的标题了
        sp.setPrompt("动作名称");
        //设置下拉框的数组适配器
        sp.setAdapter(starAdapter);
        //设置下拉框默认的显示第一项
        sp.setSelection(0);
        //给下拉框设置选择监听器，一旦用户选中某一项，就触发监听器的onItemSelected方法
        if("115200".equals(Strs[Strs.length-1])){    //用来判断当前下拉框的目标对象。
            starArray1=Strs;
            sp.setOnItemSelectedListener(new MySelectedListener1());
        }
        else if("串口通信".equals(Strs[Strs.length-1])){
            starArray2=Strs;
            sp.setOnItemSelectedListener(new MySelectedListener2());
        }else if("Min".equals(Strs[Strs.length-1])){
            starArray3=Strs;
            sp.setOnItemSelectedListener(new MySelectedListener3());
        }
    }

    private static ArrayList<String> starArray4;

    private static String[] starArray1;  //这个是全局变量
    private static String[] starArray2;  //这个是全局变量
    private static String[] starArray3;  //这个是全局变量
    static class MySelectedListener1 implements AdapterView.OnItemSelectedListener {
        @Override
        public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
            //Toast.makeText(TestPage1.this, "您选择孔位：" + starArray1[i], Toast.LENGTH_SHORT).show();
            Log.d("你选择的波特率为：",starArray1[i]+"");
        }

        @Override
        public void onNothingSelected(AdapterView<?> adapterView) {

        }
    }

    static class MySelectedListener2 implements AdapterView.OnItemSelectedListener {
        @Override
        public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
            Log.d("选择的通信串口为：",starArray2[i]+"");
        }

        @Override
        public void onNothingSelected(AdapterView<?> adapterView) {

        }
    }
    static class MySelectedListener3 implements AdapterView.OnItemSelectedListener {
        @Override
        public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
            Log.d("屏幕休眠时间单位：",starArray3[i]+"");
        }

        @Override
        public void onNothingSelected(AdapterView<?> adapterView) {

        }
    }


    static class MySelectedListener4 implements AdapterView.OnItemSelectedListener {
        @Override
        public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
            Log.d("当前查询的项目为：",starArray4.get(i)+"");
        }

        @Override
        public void onNothingSelected(AdapterView<?> adapterView) {

        }
    }

    /**
     * @Author
     * @Time 2022/7/19 14:05
     * @Description 设置sprinner 指定显示特定项*/
    public static int ugid = 1;
    public  void setSpinnerItemSelectedByValue(Spinner spinner,String value){
        SpinnerAdapter apsAdapter= spinner.getAdapter();
        int k= apsAdapter.getCount();
        for(int i=0;i<k;i++){
            if(value.equals(apsAdapter.getItem(i).toString())){
                spinner.setSelection(i);// 默认选中项
                break;
            }
        }
    }
    /**
     * @Author
     * @Time 2022/7/19 14:04
     * @Description 获取系统剩余内存：
     */
    public static long  GetAvailableSize()
    {
        File path = Environment.getDataDirectory();
        StatFs stat = new StatFs(path.getPath());
        long availableBytes = 0;
        if(android.os.Build.VERSION.SDK_INT >= 18)
        {
            availableBytes = stat.getAvailableBytes();
        }
        else
        {
            long blockSize = stat.getBlockSize();
            long totalBlocks = stat.getBlockCount();
            availableBytes =  totalBlocks * blockSize;
        }
        return availableBytes;
    }
    /**
     * @Author
     * @Time 2022/7/19 14:04
     * @Description 获取系统的整体内存。
     */
    public static long getTotalInternalMemorySize() {

        File path = Environment.getDataDirectory();

        StatFs stat = new StatFs(path.getPath());

        long blockSize = stat.getBlockSize();

        long totalBlocks = stat.getBlockCount();

        return totalBlocks * blockSize;

    }

//    public void ShowUI(Boolean Flag){
//        Intent intent = new Intent();
//        intent.setAction("marvsmart_bar");
//        if(Flag){
//            intent.putExtra("marvsmart_swich", true);
//            intent.putExtra("marvsmart_toast", "");//Toast,如果传入数据不为空且长度大于0，就以Toast弹出
//        }else{
//            intent.putExtra("marvsmart_swich", false);
//            intent.putExtra("marvsmart_toast", "");//Toast,如果传入数据不为空且长度大于0，就以Toast弹出
//        }
//        sendBroadcast(intent);
//    }

    /**
     * @Author
     * @Time 2022/8/4 20:05
     * @Description 开启系统蜂鸣器。
     */
    public static void execShell(String cmd){
        try{
//权限设置
            Process p = Runtime.getRuntime().exec("su");
//获取输出流
            OutputStream outputStream = p.getOutputStream();
            DataOutputStream dataOutputStream=new
                    DataOutputStream(outputStream);
//将命令写入
            dataOutputStream.writeBytes(cmd);
//提交命令
            dataOutputStream.flush();
//关闭流操作
            dataOutputStream.close();
            outputStream.close();
        } catch(Throwable t)
        {
            t.printStackTrace();
        }
    }
    //    public static void newBuzzer(boolean flag) throws IOException, InterruptedException {
//        if (flag){
//            execShell("echo 1 > /sys/class/leds/beep/brightness");
//        }else {
//            execShell("echo 0 > /sys/class/leds/beep/brightness");
//        }
//    }
//3288——7inch产品使用此方法
    public static void newBuzzer(boolean flag) throws IOException, InterruptedException {
        if (flag) {
            execShell("echo 1 > /sys/class/leds/beep/brightness");
        } else {
            execShell("echo 0 > /sys/class/leds/beep/brightness");
        }
    }
    //------------------16进制转字符串（GBK2312编码）-------------------
    /**
     * 16进制直接转换成为字符串(无需Unicode解码)
     * @param hex Byte字符串(Byte之间无分隔符
     * @author xxs
     * @return 对应的字符串
     */
    public static String hexStr2Str(String hex,String charSet) {
        String hexStr = "";
        String str = "0123456789ABCDEF"; //16进制能用到的所有字符 0-15
        for(int i=0;i<hex.length();i++){
            String s = hex.substring(i, i+1);
            if(s.equals("a")||s.equals("b")||s.equals("c")||s.equals("d")||s.equals("e")||s.equals("f")){
                s=s.toUpperCase().substring(0, 1);
            }
            hexStr+=s;
        }

        char[] hexs = hexStr.toCharArray();//toCharArray() 方法将字符串转换为字符数组。
        int length = (hexStr.length() / 2);//1个byte数值 -> 两个16进制字符
        byte[] bytes = new byte[length];
        int n;
        for (int i = 0; i < bytes.length; i++) {
            int position = i * 2;//两个16进制字符 -> 1个byte数值
            n = str.indexOf(hexs[position]) * 16;
            n += str.indexOf(hexs[position + 1]);
            // 保持二进制补码的一致性 因为byte类型字符是8bit的  而int为32bit 会自动补齐高位1  所以与上0xFF之后可以保持高位一致性
            //当byte要转化为int的时候，高的24位必然会补1，这样，其二进制补码其实已经不一致了，&0xff可以将高的24位置为0，低8位保持原样，这样做的目的就是为了保证二进制数据的一致性。
            bytes[i] = (byte) (n & 0xff);
        }
        String name = "";
        try {
            name = new String(bytes,charSet);
        } catch (UnsupportedEncodingException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return name;
    }

    //------------------转字符串（GBK2312编码）转16进制-------------------

    /**字节数组转换成为十六进制字符串
     * @param str
     * @return
     */
    public static String bytes_String16(String str) {
        byte[] b= new byte[0];
        try {
            b = str.getBytes("GB2312");
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        StringBuilder sb = new StringBuilder();
        for(int i=0;i<b.length;i++) {
            sb.append(String.format("%02x", b[i]));
        }
        return sb.toString();
    }
//    //---------------------------------------手动设置系统时间-------------------------------------
//    public static void setDate(int year ,int month ,int day,int hour ,int minute) throws
//            Settings.SettingNotFoundException {
////关闭自动确定日期和时间
//        int autoTime = Settings.Global.getInt(getContext().getContentResolver(),
//                Settings.Global.AUTO_TIME);
//        if (autoTime == 1) {
//            Settings.Global.putInt(getContext().getContentResolver(),
//                    Settings.Global.AUTO_TIME, 0);
//        }
////关闭自动确定时区
//        int autoZoneEnable = Settings.Global.getInt(getContext().getContentResolver(),
//                Settings.Global.AUTO_TIME_ZONE);
//        if (autoZoneEnable == 1) {
//            Settings.Global.putInt(getContext().getContentResolver(),
//                    Settings.Global.AUTO_TIME_ZONE, 0);
//        }
////设置24小时制
//        if(!DateFormat.is24HourFormat(getContext())) {
//            Settings.System.putString(getContext().getContentResolver(),
//                    Settings.System.TIME_12_24, "24");
//        } Calendar c = Calendar.getInstance();
//        c.set(Calendar.YEAR, year);
//        c.set(Calendar.MONTH, month);
//        c.set(Calendar.DAY_OF_MONTH, day);
//        c.set(Calendar.HOUR_OF_DAY, hour);
//        c.set(Calendar.MINUTE, minute);
//        c.set(Calendar.SECOND, 00);
//        long when = c.getTimeInMillis();
//        ((AlarmManager) getContext().getSystemService(Context.ALARM_SERVICE)).setTime(when);
//    }


    //---------------------------------------获取系统时间-------------------------------------
    public static List<String> getDate(){
        List<String> dataList=new ArrayList<>();
        Calendar calendar = Calendar.getInstance();//取得当前时间的年月日 时分秒
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH)+1;
        int day = calendar.get(Calendar.DAY_OF_MONTH);
        int hour = calendar.get(Calendar.HOUR_OF_DAY);
        int minute = calendar.get(Calendar.MINUTE);
        int second = calendar.get(Calendar.SECOND);
        String  Str_Date=year+"年"+month+"月"+day+"日";   //日期格式1：2022年9月19日：
        String StrMinute;
        String StrSecond;
        if(minute<10){
            StrMinute=":0"+minute;
        }else {
            StrMinute=":"+minute;
        }
        if(second<10){
            StrSecond=":0"+second;
        }else {
            StrSecond=":"+second;
        }
        String  Str_Time=hour+StrMinute+StrSecond+""; // 时间格式：15：14：30

        dataList.add(Str_Date);
        dataList.add(Str_Time);

//        String  Str_Date2="";
//        String StrMonth;
//        String StrDay;
//        if (month<10){
//            StrMonth="0"+month;
//
//        }else {
//            StrMonth=""+month;
//        }
//        if(day<10){
//            StrDay="0"+day;
//        }else {
//            StrDay=""+day;
//        }
//        Str_Date2=year+StrMonth+StrDay;
        SimpleDateFormat simpleDateFormat1 = new SimpleDateFormat("yyyyMMdd");
        String CurMonth1=simpleDateFormat1.format(calendar.getTime());
        dataList.add(CurMonth1);     //20221025  中间没有"-"的数字格式。


        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd");
        String CurMonth=simpleDateFormat.format(calendar.getTime());
        dataList.add(CurMonth);    //添加日期格式2：2022-09-01
        calendar.setTime(new Date());
        calendar.add(Calendar.MONTH,-1);
        String monthAgo= simpleDateFormat.format(calendar.getTime());

        dataList.add(monthAgo);
        return dataList;
    }
    //---------------------------------------------
//    判断整数（int）
    public static boolean isInteger(String str) {
        if (null == str || "".equals(str)) {
            return false;
        }
        Pattern pattern = Pattern.compile("^[-\\+]?[\\d]*$");
        return pattern.matcher(str).matches();
    }

    //判断浮点数（double和float）
    public static boolean isDouble(String str) {
        if (null == str || "".equals(str)) {
            return false;
        }
        Pattern pattern = Pattern.compile("^[-\\+]?\\d*[.]\\d+$"); // 之前这里正则表达式错误，现更正
        return pattern.matcher(str).matches();
    }



    /**
     * bytes转换成十六进制字符串
     * @param   b byte数组
     * @return String 每个Byte值之间空格分隔
     */
    public static String byteToHexStr(byte[] b)
    {
        String stmp="";
        StringBuilder sb = new StringBuilder("");
        for (int n=0;n<b.length;n++)
        {
            stmp = Integer.toHexString(b[n] & 0xFF);
            sb.append((stmp.length()==1)? "0"+stmp : stmp);
//            sb.append(" ");
        }
        return sb.toString().toUpperCase().trim();

    }


    /**
     * 16进制表示的字符串转换为字节数组
     *
     * @param hexString 16进制表示的字符串
     * @return byte[] 字节数组
     */
    public static byte[] hexStringToByteArray(String hexString) {
        hexString = hexString.replaceAll(" ", "");
        int len = hexString.length();
        byte[] bytes = new byte[len / 2];
        for (int i = 0; i < len; i += 2) {
            // 两位一组，表示一个字节,把这样表示的16进制字符串，还原成一个字节
            bytes[i / 2] = (byte) ((Character.digit(hexString.charAt(i), 16) << 4) + Character
                    .digit(hexString.charAt(i+1), 16));
        }
        return bytes;
    }



    /**
     * 16进制的字符串表示转成字节数组
     *
     * @param hexString 16进制格式的字符串
     * @return 转换后的字节数组
     **/
    public static byte[] toByteArray(String hexString) {
        hexString = hexString.replaceAll(" ", "");
        final byte[] byteArray = new byte[hexString.length() / 2];
        int k = 0;
        for (int i = 0; i < byteArray.length; i++) {//因为是16进制，最多只会占用4位，转换成字节需要两个16进制的字符，高位在先
            byte high = (byte) (Character.digit(hexString.charAt(k), 16) & 0xff);
            byte low = (byte) (Character.digit(hexString.charAt(k + 1), 16) & 0xff);
            byteArray[i] = (byte) (high << 4 | low);
            k += 2;
        }
        return byteArray;
    }

    /**
     * 将一个整形化为十六进制，并以字符串的形式返回
     */
    private final static String[] hexArray = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"};

    public static String byteToHex(int n) {
        if (n < 0) {
            n = n + 256;
        }
        int d1 = n / 16;
        int d2 = n % 16;
        return hexArray[d1] + hexArray[d2];
    }








    /**
     * @Author
     * @param   serialNumber :最近一次使用过的系统流水号。 是通过数据库进行读取。 20221019001
     * @Time 2022/9/19 11:07
     * @Description 生成流水号：
     */
    public static  String CreateSerialNumber(String serialNumber){
        //流水号变量
        String NumberStr;
        String NumberEndStr;
        String LastNumb;
        //1.截取最新最近使用的流水号日期：20220919001
        if(serialNumber.length()==11){
            LastNumb= serialNumber.substring(0,8);
        }else {
            LastNumb="20220901";//这个任意设置的默认初始值，即当前程序第一次测试的流水号的默认比较值。
        }
        String CurTime=getDate().get(2); //获取当前日期 20220919.
        Log.d(CurTime,CurTime);
        //2.判断当前系统已使用的流水号中的日期，当前日期是否一致。
        if(LastNumb.equals(CurTime)){
            //上一个流水号尾缀：
            String LastNumbEnd= serialNumber.substring(8);
            int a= Integer.parseInt(LastNumbEnd)+1;
            if(a<10){
                NumberEndStr="00"+a;
            }else if(a<100){
                NumberEndStr="0"+a;
            }else {
                NumberEndStr=""+a;
            }
            NumberStr=LastNumb+NumberEndStr;
        }else{  //不一致，则以当前日期,重新开始计数，例如：20220919001.
            NumberStr=CurTime+"001";
        }
        return NumberStr;
    }



    /**
     * @Author
     * @Time 2022/10/8 14:21
     * @Description 用来设置当前页面字符显示选择中文还是英文。
     */
    public static void LanguageUtil(AppCompatActivity activity, boolean isEnglish){
        Configuration configuration = activity.getResources().getConfiguration();
        DisplayMetrics displayMetrics = activity.getResources().getDisplayMetrics();
        if (isEnglish) {
            //设置英文
            configuration.locale = Locale.US;
        } else {
            //设置中文
            configuration.locale = Locale.CHINESE;
            Log.d("设置为：","中文");
        }
        //更新配置
        activity.getResources().updateConfiguration(configuration, displayMetrics);
    }

    public static String getUPath(Context context){
        StorageManager mStorageManager = (StorageManager)
                context.getSystemService(Activity.STORAGE_SERVICE);
        Class<?> volumeInfoClazz = null;
        Method getVolumes = null;
        Method isMountedReadable = null;
        Method getType = null;
        Method getPath = null;
        List<?> volumes = null;
        try {
            volumeInfoClazz = Class.forName("android.os.storage.VolumeInfo");
            getVolumes = StorageManager.class.getMethod("getVolumes");
            isMountedReadable = volumeInfoClazz.getMethod("isMountedReadable");
            getType = volumeInfoClazz.getMethod("getType");
            getPath = volumeInfoClazz.getMethod("getPath");
            volumes = (List<?>) getVolumes.invoke(mStorageManager);
            if (volumes.size()==0){
                return null;
            }
            for (Object vol : volumes) {
                if (vol != null && (boolean) isMountedReadable.invoke(vol) && (int)
                        getType.invoke(vol) == 0) {
                    File path2 = (File) getPath.invoke(vol);
                    String p2 = path2.getPath();
                    return p2;
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return null;
    }



    public static String getStoragePath(Context mContext, boolean is_removale) {

        StorageManager mStorageManager = (StorageManager) mContext.getSystemService(Context.STORAGE_SERVICE);
        Class<?> storageVolumeClazz = null;
        try {
            storageVolumeClazz = Class.forName("android.os.storage.StorageVolume");
            Method getVolumeList = mStorageManager.getClass().getMethod("getVolumeList");
            Method getPath = storageVolumeClazz.getMethod("getPath");
            Method isRemovable = storageVolumeClazz.getMethod("isRemovable");
            Object result = getVolumeList.invoke(mStorageManager);
            final int length = Array.getLength(result);
            for (int i = 0; i < length; i++) {
                Object storageVolumeElement = Array.get(result, i);
                String path = (String) getPath.invoke(storageVolumeElement);
                boolean removable = (Boolean) isRemovable.invoke(storageVolumeElement);
                if (is_removale == removable) {
                    return path;
                }
            }
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        return null;
    }

    /**
     * @Author
     * @Time 2023/1/7 11:38
     * @Description 蜂鸣器滴答函数
     * @param n 设置的蜂鸣器蜂鸣时间： ms
     */
    public static void buzzer(int n){
        try {
            newBuzzer(true);
            Thread.sleep(n);
            Tools.newBuzzer(false);
            Thread.sleep(n);
            Tools.newBuzzer(false);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }


    /**
     * @Author
     * @Time 2023/1/7 20:57
     * @Description
     */
    public static String StringToHex2(String str,boolean flag){
        String SendStr="";
        int a = Integer.parseInt(str);
        String HexStr=String.format("%04x",a);
        if(flag){
            SendStr="EEB10700"+HexStr+"FF";
        }else {
            SendStr="EEB10700"+HexStr+"FF";
        }
        return SendStr;


    }
    public static String StringToHex1(String str,boolean flag){
        String SendStr="";
        int a = Integer.parseInt(str);
        String HexStr=String.format("%04x",a);
        if(flag){
            SendStr="EEB11500"+HexStr+"FF";
        }else {
            SendStr="EEB11500"+HexStr+"FF";
        }
        return SendStr;
    }
    public static String StringToHex3(String str,boolean flag){
        String SendStr="";
        int a = Integer.parseInt(str);
        String HexStr=String.format("%04x",a);
        if(flag){
            SendStr="EEB10600"+HexStr+"FF";
        }else {
            SendStr="EEB10600"+HexStr+"FF";
        }
        return SendStr;
    }

    public static String StringToHex4(String str,boolean flag){
        String SendStr="";
        int a = Integer.parseInt(str);
        String HexStr=String.format("%04x",a);
        if(flag){
            SendStr="EEB11300"+HexStr+"FF";
        }else {
            SendStr="EEB11300"+HexStr+"FF";
        }
        return SendStr;
    }
    public static String StringToHex5(String str,boolean flag){
        String SendStr="";
        int a = Integer.parseInt(str);
        String HexStr=String.format("%04x",a);
        if(flag){
            SendStr="EEB11400"+HexStr+"FF";
        }else {
            SendStr="EEB11400"+HexStr+"FF";
        }
        return SendStr;
    }



    public static void exportExcel(Context context) {
        ArrayList<String> resultList;
        resultList= usbUtil.getUsbPaths(context);
        TFCardUtil.getTfCardPaths(context);
        String dirPath=resultList.get(0)+"/testApp";
        File file = new File(dirPath);
        if (!file.exists()) {
            if(file.mkdirs()){
                Log.d("Flag1","新建文件成功");
            }else {
            }
        }else {
            Log.d("Flag1","已存在当前要建立的文件夹");
        }
        String excelFileName = "/UsbTest.xls";
        String[] title = {"姓名", "年龄", "男孩"};
        String sheetName = "demoSheetName";


        List<DemoBean> demoBeanList = new ArrayList<>();
        for (int i=0;i<2000;i++){
            DemoBean demoBean1 = new DemoBean("张三", 10, true);
            demoBeanList.add(demoBean1);
        }
        DemoBean demoBean1 = new DemoBean("张三", 10, true);
        DemoBean demoBean2 = new DemoBean("小红", 12, false);
        DemoBean demoBean3 = new DemoBean("李四", 18, true);
        DemoBean demoBean4 = new DemoBean("王香", 13, false);
        demoBeanList.add(demoBean1);
        demoBeanList.add(demoBean2);
        demoBeanList.add(demoBean3);
        demoBeanList.add(demoBean4);

        dirPath = dirPath + excelFileName;
        ExcelUtil.initExcel(dirPath, sheetName, title);
        ExcelUtil.writeObjListToExcel(demoBeanList, dirPath, context);
    }
}
