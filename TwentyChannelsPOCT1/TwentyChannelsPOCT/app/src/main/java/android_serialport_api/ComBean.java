package android_serialport_api;

import android.util.Log;

import java.text.SimpleDateFormat;
import java.util.Date;

public class ComBean {
    public byte[] bReC=null;
    public String sRecTime="";
    public String sComPort="";
    public ComBean(String sPort,byte[] buffer, int size){
        sComPort=sPort;
        bReC=new byte[size];
        for (int i=0;i<size;i++){
            bReC[i]=buffer[i];
        }
        SimpleDateFormat sDateFormat = new SimpleDateFormat("hh:mm:ss");
        sRecTime = sDateFormat.format(new java.util.Date());
    }

}
