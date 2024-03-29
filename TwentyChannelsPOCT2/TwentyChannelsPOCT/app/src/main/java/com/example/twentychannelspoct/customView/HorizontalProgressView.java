package com.example.twentychannelspoct.customView;

import android.animation.ValueAnimator;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.RectF;
import android.util.AttributeSet;
import android.util.Log;
import android.util.TypedValue;
import android.view.View;
import android.view.animation.DecelerateInterpolator;

import com.example.twentychannelspoct.R;

import androidx.annotation.Nullable;

/**
 * **************************
 * 项目名称：TwentyChannelsPOCT
 *
 * @Author BigHang
 * 创建时间：2022/7/15  13:22
 * 用途: 自定义进度条
 * **************************
 */

public class HorizontalProgressView extends View {
    private Paint mPaint;
    private Paint mPaintRoundRect;
    private Paint mPaintText;
    private int mWidth;
    private int mHeight;
    private int padding = 5;
    private int strokeWidth = 8;
    private int textSize = 15;
    private long duration = 3500;
    private int round;
    private float process;

    public HorizontalProgressView(Context context) {
        super(context);
        init();
    }

    public HorizontalProgressView(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    public HorizontalProgressView(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        init();
    }

    //初始化画笔
    private void init(){
        mPaintRoundRect = new Paint();//圆角矩形
        mPaintRoundRect.setColor(getResources().getColor(R.color.back));//圆角矩形颜色
        mPaintRoundRect.setAntiAlias(true);// 抗锯齿效果
        mPaintRoundRect.setStyle(Paint.Style.STROKE);//设置画笔样式
        mPaintRoundRect.setStrokeWidth(strokeWidth);//设置画笔宽度

        mPaint = new Paint();
        mPaint.setColor(getResources().getColor(R.color.inner));
        mPaint.setStyle(Paint.Style.FILL_AND_STROKE);
        mPaint.setAntiAlias(true);
        mPaint.setStrokeWidth(strokeWidth);

        mPaintText = new Paint();
        mPaintText.setAntiAlias(true);
        mPaintText.setStyle(Paint.Style.FILL);
        mPaintText.setColor(getResources().getColor(R.color.back));
        mPaintText.setTextSize(sp2px(textSize));
    }

    public void setPadding(int padding) {
        this.padding = padding;
    }

    public void setStrokeWidth(int strokeWidth) {
        this.strokeWidth = strokeWidth;
    }

    public void setTextSize(int textSize) {
        this.textSize = textSize;
    }

    public void setDuration(long duration) {this.duration = duration;
    }


    @Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
        super.onMeasure(widthMeasureSpec, heightMeasureSpec);
        int widthSpecMode = MeasureSpec.getMode(widthMeasureSpec);
        int heightSpecMode = MeasureSpec.getMode(heightMeasureSpec);
        int widthSpecSize = MeasureSpec.getSize(widthMeasureSpec);
        int heightSpecSize = MeasureSpec.getSize(heightMeasureSpec);
        Log.d("widthSpa", String.valueOf(widthSpecSize));
        //MeasureSpec.EXACTLY，精确尺寸
        if (widthSpecMode == MeasureSpec.EXACTLY || widthSpecMode == MeasureSpec.AT_MOST) {
            mWidth = widthSpecSize;
            Log.d("成功获取mWidth", String.valueOf(mWidth));
        } else {
            mWidth = 0;
        }
        //MeasureSpec.AT_MOST，最大尺寸，只要不超过父控件允许的最大尺寸即可，MeasureSpec.UNSPECIFIED未指定尺寸
        if (heightSpecMode == MeasureSpec.AT_MOST || heightSpecMode == MeasureSpec.UNSPECIFIED) {
            mHeight = defaultHeight();
        } else {
            mHeight = heightSpecSize;
        }
        //设置控件实际大小
        round = mHeight / 2;//圆角半径
        setMeasuredDimension(mWidth, mHeight);
    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        drawBackground(canvas);//绘制背景矩形
        drawProgress(canvas);//绘制进度
        updateText(canvas);//处理文字
    }

    private void updateText(Canvas canvas) {
        String finishedText ="完成";
        String defaultText = "0%";
        int percent = (int) (process / (mWidth - padding - strokeWidth) * 100);
        Paint.FontMetrics fm = mPaintText.getFontMetrics();
        int mTxtWidth = (int) mPaintText.measureText(finishedText, 0, defaultText.length());
        int mTxtHeight = (int) Math.ceil(fm.descent - fm.ascent);
        int x = getWidth() / 2 - mTxtWidth / 2; //文字在画布中的x坐标
        int y = getHeight() / 2 + mTxtHeight / 4; //文字在画布中的y坐标
        mPaintText.setTextSize(25);
        if (percent < 100) {
            canvas.drawText(percent + "%", x, y, mPaintText);
        } else {
            canvas.drawText(finishedText, x, y, mPaintText);
        }
    }

    private void drawProgress(Canvas canvas) {
        if (process!=0){
            RectF rectProgress = new RectF(padding + strokeWidth, padding + strokeWidth, process, mHeight - padding - strokeWidth);//内部进度条
            canvas.drawRoundRect(rectProgress, round, round, mPaint);
        }
    }

    private void drawBackground(Canvas canvas) {
        RectF rectF = new RectF(padding, padding, mWidth - padding, mHeight - padding);//圆角矩形
        canvas.drawRoundRect(rectF, round, round, mPaintRoundRect);
    }

    //属性动画
    public void start() {
//        mWidth=1000;
        ValueAnimator valueAnimator = ValueAnimator.ofFloat(0, mWidth - padding - strokeWidth);
        valueAnimator.setDuration(duration);
        Log.d("mWidth", String.valueOf(mWidth));
        Log.d("duration", String.valueOf(duration));
        valueAnimator.setInterpolator(new DecelerateInterpolator());
        valueAnimator.addUpdateListener(animation -> {
            process = (float) animation.getAnimatedValue();
            Log.d("process", String.valueOf(process));
            invalidate();
        });
        valueAnimator.start();
    }

    //进度条默认高度，未设置高度时使用
    private int defaultHeight() {
        float scale = getContext().getResources().getDisplayMetrics().density;
        return (int) (20 * scale + 0.5f * (20 >= 0 ? 1 : -1));
    }

    private int sp2px(int sp) {
        return (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_SP, sp,
                getResources().getDisplayMetrics());
    }
    
    
}
