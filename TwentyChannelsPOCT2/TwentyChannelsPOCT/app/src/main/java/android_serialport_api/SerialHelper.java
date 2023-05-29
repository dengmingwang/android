package android_serialport_api;

import android.util.Log;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.security.InvalidParameterException;

public abstract class SerialHelper {
    private SerialPort mSerialPort;
    private OutputStream mOutputStream;
    private InputStream mInputStream;
    private ReadThread mReadThread;
    private SendThread mSendThread;
    private String sPort;
    private int iBaudRate;
    private boolean _isOpen;
    private byte[] _bLoopData;
    private int iDelay;

    public SerialHelper(String sPort, int iBaudRate) {
//        this.sPort = "/dev/ttyS1";
//        this.iBaudRate = 115200;
        this._isOpen = false;
        this._bLoopData = new byte[]{48};
        this.iDelay = 500;
        this.sPort = sPort;
        this.iBaudRate = iBaudRate;
    }

    public SerialHelper() {
//        this("/dev/ttyS1", 115200);
//        this.sPort = "/dev/ttyS1";
//        this.iBaudRate = 9600;
////        this._isOpen = true;
    }


    public SerialHelper(String sPort) {
        this(sPort, 115200);
    }

    public SerialHelper(String sPort, String sBaudRate) {
        this(sPort, Integer.parseInt(sBaudRate));
    }

    public void open() throws SecurityException, IOException, InvalidParameterException {
        this.mSerialPort = new SerialPort(new File(this.sPort), this.iBaudRate, 0);
        this.mOutputStream = this.mSerialPort.getOutputStream();
        this.mInputStream = this.mSerialPort.getInputStream();
        this.mReadThread = new ReadThread();
        this.mReadThread.start();
        this.mSendThread = new SendThread();
        this.mSendThread.setSuspendFlag();
        this.mSendThread.start();
        this._isOpen = true;
    }

    public void close() {
        if (this.mReadThread != null) {
            this.mReadThread.interrupt();
        }

        if (this.mSerialPort != null) {
            this.mSerialPort.close();
            this.mSerialPort = null;
        }

        this._isOpen = false;
    }

    public void send(byte[] bOutArray) {
        try {
            this.mOutputStream.write(bOutArray);
        } catch (IOException var3) {
            var3.printStackTrace();
        }

    }

    public void sendHex(String sHex) {
        byte[] bOutArray = Myfunc.HexToByteArr(sHex);
        this.send(bOutArray);
    }

    public void sendTxt(String sTxt) {
//        byte[] bOutArray = sTxt.getBytes("GBK");
//        this.send(bOutArray);
        byte[] bOutArray = new byte[0];
        try {
            bOutArray = sTxt.getBytes("GBK");
//       Log.d("SerialHelp",sTxt);
            send(bOutArray);
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

    }

    public int getBaudRate() {
        return this.iBaudRate;
    }

    public boolean setBaudRate(int iBaud) {
        if (this._isOpen) {
            return false;
        } else {
            this.iBaudRate = iBaud;
            return true;
        }
    }

    public boolean setBaudRate(String sBaud) {
        int iBaud = Integer.parseInt(sBaud);
        return this.setBaudRate(iBaud);
    }

    public String getPort() {
        return this.sPort;
    }

    public boolean setPort(String sPort) {
        if (this._isOpen) {
            return false;
        } else {
            this.sPort = sPort;
            return true;
        }
    }

    public boolean isOpen() {
        return this._isOpen;
    }

    public byte[] getbLoopData() {
        return this._bLoopData;
    }

    public void setbLoopData(byte[] bLoopData) {
        this._bLoopData = bLoopData;
    }

    public void setTxtLoopData(String sTxt) {
        this._bLoopData = sTxt.getBytes();
    }

    public void setHexLoopData(String sHex) {
        this._bLoopData = Myfunc.HexToByteArr(sHex);
    }

    public int getiDelay() {
        return this.iDelay;
    }

    public void setiDelay(int iDelay) {
        this.iDelay = iDelay;
    }

    public void startSend() {
        if (this.mSendThread != null) {
            this.mSendThread.setResume();
        }
    }

    public void stopSend() {
        if (this.mSendThread != null) {
            this.mSendThread.setSuspendFlag();
        }

    }
    protected abstract void onDataReceived(ComBean var1);

    private class SendThread extends Thread {
        public boolean suspendFlag;

        private SendThread() {
            this.suspendFlag = true;
        }

        @Override
        public void run() {
            super.run();

            while(!this.isInterrupted()) {
                synchronized(this) {
                    while(this.suspendFlag) {
                        try {
                            this.wait();
                        } catch (InterruptedException var5) {
                            var5.printStackTrace();
                        }
                    }
                }

                SerialHelper.this.send(SerialHelper.this.getbLoopData());

                try {
                    Thread.sleep((long)SerialHelper.this.iDelay);
                } catch (InterruptedException var4) {
                    var4.printStackTrace();
                }
            }

        }

        public void setSuspendFlag() {
            this.suspendFlag = true;
        }

        public synchronized void setResume() {
            this.suspendFlag = false;
            this.notify();
        }
    }

    private class ReadThread extends Thread {
        private ReadThread() {
        }

        @Override
        public void run() {
            super.run();

            while(!this.isInterrupted()) {
                try {
                    if (SerialHelper.this.mInputStream == null) {
                        return;
                    }
                    byte[] buffer = new byte[1024];
                    int size = SerialHelper.this.mInputStream.read(buffer);
                    if (size > 0) {
                        ComBean ComRecData = new ComBean(SerialHelper.this.sPort, buffer, size);
                        SerialHelper.this.onDataReceived(ComRecData);
                    }
                } catch (Throwable var4) {
                    Log.e("error", var4.getMessage());
                    return;
                }
            }

        }
    }

}
