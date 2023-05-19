package com.example.twentychannelspoct.utils;

import android.content.Context;
import android.os.Build;
import android.os.storage.StorageManager;
import android.util.Log;
import android.widget.Toast;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.IOException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.List;

public class TFCardUtil {
    private static final String TAG = "TFCardUtil";
    private static Context mContext;
    private static ArrayList<String> tfCardPaths=new ArrayList<>();
    public static List<String> getMountPaths() {
        List<String> resultList = new ArrayList<>();
        StorageManager storageManager = (StorageManager)
                mContext.getSystemService(Context.STORAGE_SERVICE);
        Class<?> volumeInfoClazz = null;
        Class<?> diskInfoClazz = null;
        try {
            volumeInfoClazz = Class.forName("android.os.storage.VolumeInfo");
            diskInfoClazz = Class.forName("android.os.storage.DiskInfo");
            Method getVolumes =
                    storageManager.getClass().getMethod("getVolumes");
            Method getDisk = volumeInfoClazz.getMethod("getDisk");
            Method getPath = volumeInfoClazz.getMethod("getPath");
            Method isSd = diskInfoClazz.getMethod("isSd");
            List<Object> result = (List<Object>)
                    getVolumes.invoke(storageManager);
            for (Object volume : result) {
                File file = (File) getPath.invoke(volume);
                if (file != null) {
                    Object diskInfo = getDisk.invoke(volume);
                    if (diskInfo != null) {
                        boolean isASd = (boolean) isSd.invoke(diskInfo);
                        if (isASd) {
                            resultList.add(file.getAbsolutePath());
                        }
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        for (int i = 0, count = resultList.size(); i < count; i++) {
            resultList.set(i, resultList.get(i).replace("storage",
                    "mnt/media_rw"));
        }
        return resultList;
    }
    public static void getTfCardPaths(Context context) {
        mContext = context;
        if (Build.VERSION.SDK_INT >= 23) {
            tfCardPaths = (ArrayList<String>) getMountPaths();
            if (tfCardPaths.size() == 0) {
                Toast.makeText(context,"未检出SD卡",Toast.LENGTH_SHORT).show();
                return;
            }
        } else if (Build.VERSION.SDK_INT >= 21) {
            tfCardPaths.add("/mnt/external_sd");
            if (!TFCardUtil.isTFCardExist()) {
                Toast.makeText(context,"未检出SD卡",Toast.LENGTH_SHORT).show();
                return;
            }
        } else if (Build.VERSION.SDK_INT >= 19) {
            tfCardPaths.add("/mnt/extsd");
            if (!TFCardUtil.isTFCardExist()) {
                Toast.makeText(context,"未检出SD卡",Toast.LENGTH_SHORT).show();
                return;
            }
        }
        Log.d(TAG, "tfCardPaths:" + tfCardPaths.toString());
    }
    public static boolean isTFCardExist(){
        String result = execRootCmd("ls /dev/block/mmcblk0*");
        if (result==null||result.isEmpty()){
            return false;
        }
        return true;
    }
    public static String execRootCmd(String cmd) {
        String result = "";
        DataOutputStream dos = null;
        DataInputStream dis = null;
        try {
            Process p = Runtime.getRuntime().exec("su");
            dos = new DataOutputStream(p.getOutputStream());
            dis = new DataInputStream(p.getInputStream());
            dos.writeBytes(cmd + "\n");
            dos.flush();
            dos.writeBytes("exit\n");
            dos.flush();
            for(String line = null; (line = dis.readLine()) != null; result =
                    result + line) {
            }
            p.waitFor();
        } catch (Exception var18) {
            var18.printStackTrace();
        } finally {
            if (dos != null) {
                try {
                    dos.close();
                } catch (IOException var17) {
                    var17.printStackTrace();
                }
            }
            if (dis != null) {
                try {
                    dis.close();
                } catch (IOException var16) {
                    var16.printStackTrace();
                }
            }
        }
        return result;
    }
}
