package com.example.twentychannelspoct.adapter;

import android.content.Context;
import android.net.wifi.ScanResult;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import com.example.twentychannelspoct.R;

import java.util.List;

public class WiFiListAdapter extends ArrayAdapter<ScanResult> {

    private LayoutInflater inflater;
    private List<android.net.wifi.ScanResult> scanResults;

    public WiFiListAdapter(@NonNull Context context, List<android.net.wifi.ScanResult> scanResults) {
        super(context, R.layout.list_item_wifi, scanResults);
        this.inflater = LayoutInflater.from(context);
        this.scanResults = scanResults;
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        ViewHolder holder;
        if (convertView == null) {
            convertView = inflater.inflate(R.layout.list_item_wifi, parent, false);
            holder = new ViewHolder();
            holder.textViewSsid = convertView.findViewById(R.id.textViewSsid);
            holder.textViewBssid = convertView.findViewById(R.id.textViewBssid);
            holder.textViewLevel = convertView.findViewById(R.id.textViewLevel);
            convertView.setTag(holder);
        } else {
            holder = (ViewHolder) convertView.getTag();
        }

        android.net.wifi.ScanResult scanResult = scanResults.get(position);
        holder.textViewSsid.setText(scanResult.SSID);
        holder.textViewBssid.setText(scanResult.BSSID);
        holder.textViewLevel.setText(String.valueOf(scanResult.level));

        return convertView;
    }

    private static class ViewHolder {
        TextView textViewSsid;
        TextView textViewBssid;
        TextView textViewLevel;
    }


}
