package com.example.twentychannelspoct.pages

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentStatePagerAdapter
import androidx.viewpager.widget.ViewPager
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.fragments.*
import com.google.android.material.tabs.TabLayout
import java.util.*

class Debugging : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_debugging)
        bindComponent()
        initView()
    }

    private fun initView() {
        fragments.clear()
        fragments.add(DebugPara1Fragment())
        fragments.add(DebugCurve2Fragment())
        fragments.add(DebugPosition3Fragment())

        val viewPager: ViewPager = findViewById(R.id.view_pager)
        val tabLayout: TabLayout = findViewById(R.id.layout_tab)
        viewPager.adapter = PagerAdapter(supportFragmentManager)
        tabLayout.setupWithViewPager(viewPager)
        for (s in debugTypes) {
            tabLayout.newTab().text = s
        }
    }

    private fun bindComponent() {
        //绑定下方弹窗
        BottomTitle(1, Timer(),this)
    }

    companion object {
        var fragments = ArrayList<BaseFragment>()
        val debugTypes = arrayOf("Parameter", "Curve", "Position")
    }

    private class PagerAdapter(fm: FragmentManager?) : FragmentStatePagerAdapter(fm!!) {
        override fun getItem(position: Int): Fragment {
            return fragments[position]
        }

        override fun getCount(): Int {
            return debugTypes.size
        }

        override fun getPageTitle(position: Int): CharSequence {
            return debugTypes[position]
        }
    }
}