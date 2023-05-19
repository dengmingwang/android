package com.example.twentychannelspoct.bean

import org.litepal.crud.LitePalSupport


class Book (val name: String,val page:Int) :  LitePalSupport(){
    val id: Long = 0

    override fun toString(): String {
        return "Book(name='$name', page=$page, id=$id)"
    }
}