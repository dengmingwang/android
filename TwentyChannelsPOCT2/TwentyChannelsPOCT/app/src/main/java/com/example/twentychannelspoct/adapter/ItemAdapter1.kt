package com.example.twentychannelspoct.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.bean.ResultItem
import com.example.twentychannelspoct.utils.Tools
import java.util.*
import kotlin.concurrent.timer
/**
 ***************************
 *项目名称：TwentyChannelsPOCT
 *@Author
 *创建时间：2023/5/23  9:19
 *用途:  RecycleView适配器
 ***************************
 */
class ItemAdapter1 (private val ItemList: List<ResultItem>): RecyclerView.Adapter<ItemAdapter1.ViewHolder>(){
    inner class ViewHolder(view: View) : RecyclerView.ViewHolder(view) {

        val itemId: TextView =view.findViewById(R.id.subItemId)
        val subName: TextView = view.findViewById(R.id.subItemName)
        val subResult : TextView =view.findViewById(R.id.subResult)
//        val subRange : TextView = view.findViewById(R.id.subReferenceRange)
        val testTime : TextView = view.findViewById(R.id.TestTime)
        val Insertiontime:TextView = view.findViewById(R.id.Insertiontime)
        val Scantime:TextView = view.findViewById(R.id.Scantime)

    }
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.item_list1, parent, false)
        val viewHolder = ViewHolder(view)
        viewHolder.itemView.setOnClickListener {
            val position = viewHolder.adapterPosition
            val item = ItemList[position]
            Toast.makeText(parent.context, "you clicked view ${item.name}",
                Toast.LENGTH_SHORT).show()
        }
        return viewHolder
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = ItemList[position]
        holder.itemId.text=item.id.toString()
        holder.subName.text=item.name
        holder.subResult.text=item.result.toString()+"mmol/L"
//        holder.subRange.text=item.range
        holder.testTime.text=item.testtime
        holder.Insertiontime.text = item.insertiontime
        holder.Scantime.text = item.scantime
    }
    override fun getItemCount() = ItemList.size
}