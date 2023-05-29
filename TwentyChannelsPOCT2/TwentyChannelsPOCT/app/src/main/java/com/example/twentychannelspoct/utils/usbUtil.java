package com.example.twentychannelspoct.utils;

import android.content.Context;
import android.os.Build;
import android.os.storage.StorageManager;
import android.util.Log;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class usbUtil {
    private static final String TAG = "UsbUtil";
    public static List<String> getMountPaths(Context context) {
        List<String> resultList = new ArrayList<>();
        if (Build.VERSION.SDK_INT < 23) {
            try {
                Thread.sleep(1000);
                Runtime runtime = Runtime.getRuntime();
                Process proc = runtime.exec("mount");
                InputStream in = proc.getInputStream();
                InputStreamReader isr = new InputStreamReader(in);
                String line;
                BufferedReader br = new BufferedReader(isr);
                while ((line = br.readLine()) != null) {
                    if (line.contains("proc") || line.contains("tmpfs") ||
                            line.contains("media")) {
                        continue;
                    }
                    if (line.contains("vold")) {
                        String items[] = line.split(" ");
                        if (items != null && items.length > 1) {
                            String path =
                                    items[1].toLowerCase(Locale.getDefault());
                            if (path != null && !resultList.contains(path) &&
                                    path.contains("usb")) {
                                resultList.add(items[1]);
                            }
                        }
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
            return resultList;
        }
        StorageManager storageManager = (StorageManager)
                context.getSystemService(Context.STORAGE_SERVICE);
        Class<?> volumeInfoClazz = null;
        Class<?> diskInfoClazz = null;
        try {
            volumeInfoClazz = Class.forName("android.os.storage.VolumeInfo");
            diskInfoClazz = Class.forName("android.os.storage.DiskInfo");
            Method getVolumes =
                    storageManager.getClass().getMethod("getVolumes");
            Method getDisk = volumeInfoClazz.getMethod("getDisk");
            Method getPath = volumeInfoClazz.getMethod("getPath");
            Method isUsb = diskInfoClazz.getMethod("isUsb");
            List<Object> result = (List<Object>) getVolumes.invoke(storageManager);
            for (Object volume : result) {
                File file = (File) getPath.invoke(volume);
                if (file != null) {
                    Object diskInfo = getDisk.invoke(volume);
                    if (diskInfo != null) {
                        boolean isAUsb = (boolean) isUsb.invoke(diskInfo);
                        if (isAUsb) {
                            resultList.add(file.getAbsolutePath());
                        }
                    }
                }
            }
        } catch (ClassNotFoundException | NoSuchMethodException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        for (int i = 0, count = resultList.size(); i < count; i++) {
            resultList.set(i, resultList.get(i).replace("storage",
                    "mnt/media_rw"));
        }
        return resultList;
    }

    public static ArrayList getUsbPaths(Context context) {
        ArrayList<String> usbPaths = (ArrayList<String>)
                usbUtil.getMountPaths(context);
        if (usbPaths.size() == 0) {return null;
        }
        for (String path : usbPaths) {
            Log.i(TAG, "usbPath : " + path);
        }
        return usbPaths;
    }
}
