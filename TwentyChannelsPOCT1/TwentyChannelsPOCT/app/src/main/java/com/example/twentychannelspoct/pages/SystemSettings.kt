package com.example.twentychannelspoct.pages

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentStatePagerAdapter
import androidx.viewpager.widget.ViewPager
import com.example.twentychannelspoct.R
import com.example.twentychannelspoct.fragments.*
import com.google.android.material.tabs.TabLayout
import java.util.*

class SystemSettings : AppCompatActivity() , View.OnClickListener{
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_system_setting)
        bindComponent()
        initView()
    }

    private fun initView() {
        fragments.clear()
        fragments.add(CompanyFragment())
        fragments.add(LisConfigFragment())
        fragments.add(TestConfigFragment(this))
        fragments.add(SystemConfigFragment())
        fragments.add(OutputSettingFragment())
        fragments.add(WIFISettingFragment())
        fragments.add(VersionInfoFragment())

        val viewPager: ViewPager = findViewById(R.id.view_pager)
        val tabLayout: TabLayout = findViewById(R.id.layout_tab)
        viewPager.adapter = PagerAdapter(supportFragmentManager)
        tabLayout.setupWithViewPager(viewPager)
        for (s in types) {
            tabLayout.newTab().text = s
        }
    }

    private fun bindComponent() {
        //绑定下方弹窗
        BottomTitle(1, Timer(),this)
    }

    companion object {
        var fragments = ArrayList<BaseFragment>()
        private val types = arrayOf("Company", "LIS", "Test", "System", "Output", "WIFI", "Version")
    }

    override fun onClick(p0: View?) {

    }

    private class PagerAdapter(fm: FragmentManager?) : FragmentStatePagerAdapter(fm!!) {
        override fun getItem(position: Int): Fragment {
            return fragments[position]
        }

        override fun getCount(): Int {
            return types.size
        }

        override fun getPageTitle(position: Int): CharSequence {
            return types[position]
        }
    }


}