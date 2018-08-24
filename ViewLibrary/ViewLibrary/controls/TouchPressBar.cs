// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.TouchPressBar
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet.view
{
  public class TouchPressBar : UserControl
  {
    private int mMaxValue = 100;
    private Color mTextColor = Color.Black;
    private TickStyle mTickStyle = TickStyle.TopLeft;
    private Orientation mOrientation;
    private int mMinValue;
    private int mValue;
    private float mBaseHeight;
    private Rectangle mBarRect;
    private Rectangle mValueRect;
    private Pen mPen;
    private PointF[] mMinValueTextPoint;
    private PointF[] mMaxValueTextPoint;
    private SizeF minValueSize;
    private SizeF maxValueSize;
    private IContainer components;

    public Orientation Orientation
    {
      set
      {
        this.mOrientation = value;
        this.measure();
        this.Refresh();
      }
      get
      {
        return this.mOrientation;
      }
    }

    public int MaxValue
    {
      get
      {
        return this.mMaxValue;
      }
      set
      {
        this.mMaxValue = value;
        this.measure();
        this.Refresh();
      }
    }

    public int MinValue
    {
      get
      {
        return this.mMinValue;
      }
      set
      {
        this.mMinValue = value;
        this.measure();
        this.Invalidate();
      }
    }

    public int Value
    {
      set
      {
        if (value == this.mValue)
          return;
        this.mValue = value;
        this.measureValueRect();
        this.Invalidate(this.mBarRect);
      }
      get
      {
        return this.mValue;
      }
    }

    public Color TextColor
    {
      set
      {
        this.mTextColor = value;
        this.Refresh();
      }
      get
      {
        return this.mTextColor;
      }
    }

    public TickStyle TickStyle
    {
      get
      {
        return this.mTickStyle;
      }
      set
      {
        this.mTickStyle = value;
        this.measure();
        this.Refresh();
      }
    }

    public TouchPressBar()
    {
      this.mBaseHeight = 2f * this.Font.Size;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.mPen = new Pen(this.TextColor, 1f);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.mPen.Color = this.TextColor;
      e.Graphics.DrawRectangle(this.mPen, this.mBarRect.X, this.mBarRect.Y, this.mBarRect.Width, this.mBarRect.Height);
      this.mPen.Color = this.TextColor;
      SolidBrush solidBrush1 = new SolidBrush(this.TextColor);
      SolidBrush solidBrush2 = new SolidBrush(this.ForeColor);
      if (this.mMinValueTextPoint != null)
      {
        foreach (PointF point in this.mMinValueTextPoint)
          e.Graphics.DrawString(string.Concat((object) this.MinValue), this.Font, (Brush) solidBrush1, point);
      }
      if (this.mMaxValueTextPoint != null)
      {
        foreach (PointF point in this.mMaxValueTextPoint)
          e.Graphics.DrawString(string.Concat((object) this.MaxValue), this.Font, (Brush) solidBrush1, point);
      }
      e.Graphics.FillRectangle((Brush) solidBrush2, this.mValueRect);
      solidBrush1.Dispose();
      solidBrush2.Dispose();
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
      int num = 0;
      if (this.mTickStyle == TickStyle.BottomRight || this.mTickStyle == TickStyle.TopLeft)
        num = 1;
      else if (this.mTickStyle == TickStyle.Both)
        num = 2;
      if (this.Orientation == Orientation.Horizontal)
        base.SetBoundsCore(x, y, width, (int) ((double) (num + 1) * (double) this.mBaseHeight), specified);
      else
        base.SetBoundsCore(x, y, (int) ((double) num * (double) this.getTextWidth() + (double) this.mBaseHeight), height, specified);
    }

    private void measure()
    {
      this.minValueSize = this.meassureString(string.Concat((object) this.MinValue));
      this.maxValueSize = this.meassureString(string.Concat((object) this.MaxValue));
      this.onMeassure();
    }

    private void onMeassure()
    {
      this.meassureBarRect();
      this.meassureText();
    }

    private void meassureBarRect()
    {
      this.mBarRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      if (this.mTickStyle == TickStyle.None)
        return;
      if (this.Orientation == Orientation.Horizontal)
      {
        this.mBarRect.Height = (int) this.Font.Size;
        this.mBarRect.Width -= (int) ((double) this.minValueSize.Width + (double) this.maxValueSize.Width) / 2;
        this.mBarRect.X = (int) this.minValueSize.Width / 2;
        switch (this.mTickStyle)
        {
          case TickStyle.TopLeft:
            this.mBarRect.Y = this.Height - this.mBarRect.Height - 1;
            break;
          case TickStyle.Both:
            this.mBarRect.Y = (this.Height - this.mBarRect.Height) / 2;
            break;
        }
      }
      else
      {
        this.mBarRect.Width = (int) this.Font.Size;
        this.mBarRect.Height -= (int) ((double) this.minValueSize.Height + (double) this.maxValueSize.Height) / 2;
        this.mBarRect.Y = (int) this.maxValueSize.Height / 2;
        switch (this.mTickStyle)
        {
          case TickStyle.TopLeft:
            this.mBarRect.X = this.Width - this.mBarRect.Width - 1;
            break;
          case TickStyle.Both:
            this.mBarRect.X = (this.Width - this.mBarRect.Width) / 2;
            break;
        }
      }
    }

    private void meassureText()
    {
      this.mMinValueTextPoint = (PointF[]) null;
      this.mMaxValueTextPoint = (PointF[]) null;
      if (this.Orientation == Orientation.Horizontal)
      {
        float y1 = (float) ((double) this.mBarRect.Y - (double) this.minValueSize.Height - 2.0);
        float y2 = (float) (this.mBarRect.Y + this.mBarRect.Height + 5);
        switch (this.mTickStyle)
        {
          case TickStyle.TopLeft:
            this.mMinValueTextPoint = new PointF[1];
            this.mMaxValueTextPoint = new PointF[1];
            this.mMinValueTextPoint[0] = new PointF(0.0f, y1);
            this.mMaxValueTextPoint[0] = new PointF((float) this.Width - this.maxValueSize.Width, y1);
            break;
          case TickStyle.BottomRight:
            this.mMinValueTextPoint = new PointF[1];
            this.mMaxValueTextPoint = new PointF[1];
            this.mMinValueTextPoint[0] = new PointF(0.0f, y2);
            this.mMaxValueTextPoint[0] = new PointF((float) this.Width - this.maxValueSize.Width, y2);
            break;
          case TickStyle.Both:
            this.mMinValueTextPoint = new PointF[2];
            this.mMaxValueTextPoint = new PointF[2];
            this.mMinValueTextPoint[0] = new PointF(0.0f, y1);
            this.mMinValueTextPoint[1] = new PointF(0.0f, y2);
            this.mMaxValueTextPoint[0] = new PointF((float) this.Width - this.maxValueSize.Width, y1);
            this.mMaxValueTextPoint[1] = new PointF((float) this.Width - this.maxValueSize.Width, y2);
            break;
        }
      }
      else
      {
        float x1 = (float) (this.mBarRect.X + this.mBarRect.Width + 5);
        float x2 = (float) ((double) this.mBarRect.X - (double) this.minValueSize.Width - 5.0);
        float x3 = (float) ((double) this.mBarRect.X - (double) this.maxValueSize.Width - 5.0);
        switch (this.mTickStyle)
        {
          case TickStyle.TopLeft:
            this.mMinValueTextPoint = new PointF[1];
            this.mMaxValueTextPoint = new PointF[1];
            this.mMinValueTextPoint[0] = new PointF(x2, (float) this.Height - this.minValueSize.Height);
            this.mMaxValueTextPoint[0] = new PointF(x3, 0.0f);
            break;
          case TickStyle.BottomRight:
            this.mMinValueTextPoint = new PointF[1];
            this.mMaxValueTextPoint = new PointF[1];
            this.mMinValueTextPoint[0] = new PointF(x1, (float) this.Height - this.minValueSize.Height);
            this.mMaxValueTextPoint[0] = new PointF(x1, 0.0f);
            break;
          case TickStyle.Both:
            this.mMinValueTextPoint = new PointF[2];
            this.mMaxValueTextPoint = new PointF[2];
            this.mMinValueTextPoint[0] = new PointF(x2, (float) this.Height - this.minValueSize.Height);
            this.mMinValueTextPoint[1] = new PointF(x1, (float) this.Height - this.minValueSize.Height);
            this.mMaxValueTextPoint[0] = new PointF(x3, 0.0f);
            this.mMaxValueTextPoint[1] = new PointF(x1, 0.0f);
            break;
        }
      }
    }

    private void measureValueRect()
    {
      this.mValueRect = this.mBarRect;
      float num1 = (float) this.Value / (float) this.MaxValue;
      if (this.Orientation == Orientation.Horizontal)
      {
        float num2 = num1 * (float) this.mBarRect.Width;
        if ((double) num2 > (double) this.mBarRect.Width)
          num2 = (float) this.mBarRect.Width;
        this.mValueRect.Width = (int) num2;
        ++this.mValueRect.X;
        ++this.mValueRect.Y;
        --this.mValueRect.Height;
      }
      else
      {
        float num2 = (float) this.mBarRect.Height * num1;
        if ((double) num2 > (double) this.mBarRect.Height)
          num2 = (float) this.mBarRect.Height;
        --this.mValueRect.Width;
        this.mValueRect.Height = (int) num2;
        this.mValueRect.Y += this.mBarRect.Height - (int) num2;
        ++this.mValueRect.X;
      }
    }

    private float getTextWidth()
    {
      this.CreateGraphics();
      SizeF sizeF1 = this.meassureString(string.Concat((object) this.MaxValue));
      SizeF sizeF2 = this.meassureString(string.Concat((object) this.MinValue));
      if ((double) sizeF1.Width <= (double) sizeF2.Width)
        return sizeF2.Width;
      return sizeF1.Width;
    }

    private float getTextWidth(string text)
    {
      return this.meassureString(text).Width;
    }

    private float getTextHeight(string text)
    {
      return this.meassureString(text).Height;
    }

    private SizeF meassureString(string text)
    {
      return this.CreateGraphics().MeasureString(text, this.Font);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }
  }
}
