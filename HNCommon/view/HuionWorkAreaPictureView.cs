// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.HuionWorkAreaPictureView
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using HuionTablet.utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HuionTablet.view
{
  public class HuionWorkAreaPictureView : PictureBox
  {
    private int mScreenshotMaxWidth = 640;
    private int mScreenshotMaxHeight = 300;
    private int mDeviceImageMaxWidth = 400;
    private int mDeviceImageMaxHeight = 190;
    private int mIntervalSpace = 5;
    public EventHandler Callback;
    private const int mRefreshTop = 1;
    private const int mRefreshBottom = 2;
    private Bitmap mScreenshot;
    private Rectangle mScreenshotRect;
    private Bitmap mDeviceImage;
    private Bitmap mDeviceImage4Draw;
    private Rectangle mDeviceImageRect;
    private int mRotate;
    private HNStruct.HNSize mDeviceSize;
    private HNStruct.HNRect mPenWorkarea;
    private Rectangle mPenWorkRect;

    public int ScreenshotMaxWidth
    {
      get
      {
        return this.mScreenshotMaxWidth;
      }
      set
      {
        this.mScreenshotMaxWidth = value;
        this.meassure(0);
      }
    }

    public int ScreenshotMaxHeight
    {
      get
      {
        return this.mScreenshotMaxHeight;
      }
      set
      {
        this.mScreenshotMaxHeight = value;
        this.meassure(0);
      }
    }

    public int DeviceImageMaxWidth
    {
      set
      {
        this.mDeviceImageMaxWidth = value;
        this.meassure(2);
      }
      get
      {
        return this.mDeviceImageMaxWidth;
      }
    }

    public int DeviceImageMaxHeight
    {
      set
      {
        this.mDeviceImageMaxHeight = value;
        this.meassure(2);
      }
      get
      {
        return this.mDeviceImageMaxHeight;
      }
    }

    public Bitmap Screenshot
    {
      set
      {
        this.mScreenshot = value;
        this.meassure(1);
      }
      get
      {
        return this.mScreenshot;
      }
    }

    public Rectangle ScreenshotRect
    {
      get
      {
        return this.mScreenshotRect;
      }
    }

    public Bitmap DeviceImage
    {
      set
      {
        this.mDeviceImage = value;
        this.mDeviceImage = HuionRender.compressImageWithRate(this.mDeviceImage, this.DeviceImageMaxWidth, this.DeviceImageMaxHeight);
        this.meassure(2);
      }
      get
      {
        return this.mDeviceImage;
      }
    }

    public Rectangle DeviceImageRect
    {
      get
      {
        return this.mDeviceImageRect;
      }
    }

    public int IntervalSpace
    {
      set
      {
        this.mIntervalSpace = value;
        this.meassure(0);
      }
      get
      {
        return this.mIntervalSpace;
      }
    }

    public int Rotate
    {
      set
      {
        this.mRotate = value;
        this.meassure(2);
      }
      get
      {
        return this.mRotate;
      }
    }

    public HNStruct.HNSize DeviceSize
    {
      set
      {
        this.mDeviceSize = value;
        this.meassure(2);
      }
      get
      {
        return this.mDeviceSize;
      }
    }

    public HNStruct.HNRect PenWorkarea
    {
      set
      {
        this.mPenWorkarea = value;
        this.meassure(0);
      }
      get
      {
        return this.mPenWorkarea;
      }
    }

    public Rectangle PenWorkRect
    {
      get
      {
        return this.mPenWorkRect;
      }
    }

    public void setDeviceInfo(HNStruct.HNSize deviceSize, HNStruct.HNRect penWorkarea, int rotate)
    {
      this.mDeviceSize = deviceSize;
      this.mPenWorkarea = penWorkarea;
      this.mRotate = rotate;
      this.meassure(0);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Pen pen = new Pen(Color.FromArgb(-586018245), 1f);
      pen.DashPattern = new float[2]{ 8f, 8f };
      pen.DashStyle = DashStyle.Custom;
      if (this.mScreenshot != null)
      {
        e.Graphics.DrawImage((Image) this.mScreenshot, this.mScreenshotRect);
        e.Graphics.DrawRectangle(pen, new Rectangle(this.mScreenshotRect.X + 1, this.mScreenshotRect.Y + 1, this.mScreenshotRect.Width - 2, this.mScreenshotRect.Height - 2));
      }
      if (this.mDeviceImage4Draw == null)
        return;
      e.Graphics.DrawImage((Image) this.mDeviceImage4Draw, this.mDeviceImageRect);
      e.Graphics.DrawRectangle(pen, new Rectangle(this.mDeviceImageRect.X + 1, this.mDeviceImageRect.Y + 1, this.mDeviceImageRect.Width - 2, this.mDeviceImageRect.Height - 2));
    }

    private void meassure(int refreshPart)
    {
      this.mScreenshot = HuionRender.compressImageWithRate(this.mScreenshot, this.ScreenshotMaxWidth, this.ScreenshotMaxHeight);
      if (this.mScreenshot != null)
      {
        this.mScreenshotRect = new Rectangle();
        this.mScreenshotRect.Width = this.mScreenshot.Width;
        this.mScreenshotRect.Height = this.mScreenshot.Height;
        this.mScreenshotRect.X = 0;
        this.mScreenshotRect.Y = 0;
      }
      this.mDeviceImage4Draw = HuionRender.rotateImageCenter(this.mDeviceImage, (float) this.mRotate, Color.Transparent);
      this.mDeviceImage4Draw = HuionRender.compressImageWithRate(this.mDeviceImage4Draw, this.DeviceImageMaxWidth, this.DeviceImageMaxHeight);
      if (this.mDeviceImage4Draw != null)
      {
        this.mDeviceImageRect = new Rectangle();
        this.mDeviceImageRect.Width = this.mDeviceImage4Draw.Width;
        this.mDeviceImageRect.Height = this.mDeviceImage4Draw.Height;
        this.mDeviceImageRect.X = (this.mScreenshotRect.Width - this.mDeviceImage4Draw.Width) / 2;
        this.mDeviceImageRect.Y = this.mScreenshotRect.Y + this.mScreenshotRect.Height + this.mIntervalSpace;
      }
      if (this.mDeviceImageRect.X < 0)
        this.mDeviceImageRect.X = 0;
      this.Width = this.mDeviceImageRect.Width > this.mScreenshotRect.Width ? this.mDeviceImageRect.Width : this.mScreenshotRect.Width;
      this.Height = this.mDeviceImageRect.Bottom;
      if (this.Width > this.mScreenshotRect.Width)
        this.mScreenshotRect.X = (this.Width - this.mScreenshotRect.Width) / 2;
      if (this.mDeviceImage4Draw != null && this.mDeviceSize.cx > 0 && this.mDeviceSize.cy > 0)
      {
        this.mPenWorkRect = Utils.swapLayout(this.mDeviceImage4Draw.Size, Utils.rotateSize(this.mDeviceSize, this.mRotate), Utils.rotateRect(this.mPenWorkarea, this.mDeviceSize, this.mRotate));
        this.mPenWorkRect.X += this.mDeviceImageRect.X;
        this.mPenWorkRect.Y += this.mDeviceImageRect.Y;
      }
      if ((refreshPart & 1) == 1)
        this.Invalidate(new Rectangle(0, 0, this.Width, this.mScreenshotRect.Bottom));
      else if ((refreshPart & 2) == 2)
        this.Invalidate(new Rectangle(0, this.mDeviceImageRect.Y, this.Width, this.mDeviceImageRect.Height));
      else
        this.Invalidate();
      if (this.Callback == null)
        return;
      this.Callback((object) this, (EventArgs) PictureViewEventArgs.getInstance(this.Rotate));
    }
  }
}
