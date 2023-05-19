package com.example.twentychannelspoct.utils;

import android.util.Log;

/**
 * **************************
 * 项目名称：SinglePOCT
 *
 * @Author BigHang
 * 创建时间：2022/11/14  13:36
 * 用途:用于分析处理下位机回传的数据。
 *      处理方法暂定：目前下位机的处理方法。
 * **************************
 */

public class ResultAnalysis {

    //1.数据缓存
    public String[] TestMode = {"最大面积法","峰值比较法","","其他"};
    String ResultStr = "";
    //第一个峰值的起点和终点
    int Peak1_RangeS,Peak1_RangeE;
    /**第二个峰值的起点和终点*/
    int Peak2_RangeS,Peak2_RangeE;
    /**第三个峰值的起点和终点*/
    int Peak3_RangeS,Peak3_RangeE;
    /**第四个峰值的起点和终点*/
    int Peak4_RangeS,Peak4_RangeE;
    /**第五个峰值的起点和终点*/
    int Peak5_RangeS,Peak5_RangeE;

    /**
     * @Author
     * @Time 2022/11/14 14:26
     * @Description 获取测试结果值：
     * @param  i:表示有几个检测结果
     *         receiveData: 检测的结果值：
     */

    public String getResult(int i,byte[] receiveData){

        //电压峰值应该是一个整数
        int a[] = {0,0,0,0,0};
        int[] Peakdata = new int[10];//标记各个峰值的起、落点位置
        int[] MaxX = new int[5];//标记各个峰值的X坐标
        float[] MaxV = new float[5];//标记各个峰值数据
        switch (i){
            case 1://

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                break;
        }
        return ResultStr;
    }

    String[] Datalist = new String[380];
    //    List<String> Datalist= new ArrayList<>();
    //1.将接收到的字符串变成字节数数组
    //2.然后再以120、240为测试基准点，前后扩展40的点位范围，取最大值。
    public String[] dataProcess(String DataStr){
        for (int i = 0;i < DataStr.length();i = i+4){
           int n=i/4;
           String strX1 = DataStr.substring(i,i+4);
            Log.d("strX1",strX1);
            int anInt1 = Integer.parseInt(strX1,16);
            Log.d("anInt1",String.valueOf(anInt1));
//            Datalist.add((float)anInt1);
            Double a = (double)anInt1;
            Datalist[n] = String.valueOf(a);
        }
        //        for(String a:Datalist){
//
//        }
        return Datalist;
    }

    double[] Datalist1 = new double[380];
    public double[] dataProcess1(String DataStr) {
        for (int i = 0; i < DataStr.length(); i = i + 4) {
            int n = i / 4;
            String strX1 = DataStr.substring(i, i + 4);
            Log.d("strX1", strX1);
            int anInt1 = Integer.parseInt(strX1, 16);
            Log.d("anInt1", String.valueOf(anInt1));
//            Datalist.add((float)anInt1);
            Double a = (double) anInt1;
            Datalist1[n] = a;
        }
        //        for(String a:Datalist){`
//
//        }
        return Datalist1;
    }

    public static double getMaxValue(double[] array){
        double maxValue = Double.MAX_VALUE;
        for (double d:array){
            if (d>maxValue){
                maxValue = d;
            }
        }
        return maxValue;
    }
    //分别在120~220中间值最值，240~340之间最值

      /*两峰*/
    public double[] getPeak(String dataStr){
        double[] peak = new double[4];
        double[]list = dataProcess1(dataStr);
        int start1 = 120;
        int end1 = 220;
        double max1 = Double.MIN_VALUE;
        for (int i = start1;i<=end1;i++){
            max1 = Math.max(max1,list[i]);
        }
        for (int i= start1;i<end1;i++){
            if (list[i]==max1){
                peak[2]=i;
                Log.d("peak1峰值横坐标",String.valueOf(i));
            }
        }
        peak[0] = max1;
        int start2 = 240;
        int end2 = 340;
        double max2 = Double.MIN_VALUE;
        for (int i = start2;i <= end2;i++){
            max2 = Math.max(max2,list[i]);
        }
        peak[1]=max2;
        for (int i=start2;i<end2;i++){
            if (list[i]==max2){
                peak[3]=i;
                Log.d("peak2峰值横坐标",String.valueOf(i));
            }
        }
        return peak;
    }
    /*三峰*/
    public double[] getPeak1(String dataStr){
       double[] peak = new double[6] ;
       double[]list = dataProcess1(dataStr);
       int start1 = 120;
       int end1 = 220;
       double max1 = Double.MIN_VALUE;
       for (int i = start1; i<=end1;i++){
           max1 = Math.max(max1,list[i]);
       }
       for (int i = start1; i<end1;i++){
           if (list[i] == max1){
               peak[2]=i;
               Log.d("peak1峰值横坐标",String.valueOf(i));
           }
       }
       peak[0] = max1;
        int start2 = 240;
        int end2 = 340;
        double max2 = Double.MIN_VALUE;
        for (int i = start2;i <= end2;i++){
            max2 = Math.max(max2,list[i]);
        }
        peak[1]=max2;
        for (int i=start2;i<end2;i++) {
            if (list[i] == max2) {
                peak[3] = i;
                Log.d("peak2峰值横坐标", String.valueOf(i));
            }
        }
        int start3 =360;
        int end3 = 460;
        double max3 = Double.MIN_VALUE;
        for (int i = start3;i <= end3;i++){
            max3 = Math.max(max3,list[i]);
        }
        peak[5]=max3;
        for (int i = start3;i<end3;i++){
            if (list[i]==max3){
                peak[4]=i;
                Log.d("peak3峰值横坐标", String.valueOf(i));
            }
        }

        return peak;
    }
}
