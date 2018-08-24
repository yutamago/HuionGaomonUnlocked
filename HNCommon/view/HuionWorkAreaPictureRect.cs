// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.HuionWorkAreaPictureRect
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HuionTablet.Entity;
using HuionTablet.Lib;
using HuionTablet.utils;

namespace HuionTablet.view
{
    public class HuionWorkAreaPictureRect : PictureBox
    {
        public EventHandler DeviceRatioChanged;
        private MouseLocationType locationType;
        private Rectangle mDeviceDrawRect;
        private HNStruct.HNRectRatio mDeviceDrawRectRatio;
        private Rectangle mDeviceRect;

        private HNStruct.HNRectRatio mDeviceRectRatio = new HNStruct.HNRectRatio()
        {
            l = 0.0f,
            t = 0.0f,
            r = 1f,
            b = 1f
        };

        private int mDeviceRectRotate;
        private MouseButtons mMouseButton;
        private Rectangle mMouseInRect;
        private Point mMouseLocation;
        private Rectangle mScreenDrawRect;
        private Rectangle mScreenRect;

        private HNStruct.HNRectRatio mScreenRectRatio = new HNStruct.HNRectRatio()
        {
            l = 0.0f,
            t = 0.0f,
            r = 1f,
            b = 1f
        };

        public EventHandler ScreenRatioChanged;

        public Rectangle ScreenRect
        {
            get { return this.mScreenRect; }
            set
            {
                this.mScreenRect = value;
                this.measureScreenRect();
            }
        }

        public Rectangle DeviceRect
        {
            get { return this.mDeviceRect; }
            set
            {
                this.mDeviceRect = value;
                this.measureDeviceRect();
            }
        }

        public int DeviceRectRotate
        {
            set
            {
                this.mDeviceDrawRectRatio = Utils.rotateRatio(this.mDeviceDrawRectRatio, -this.mDeviceRectRotate);
                this.mDeviceRectRotate = value;
                this.mDeviceDrawRectRatio = Utils.rotateRatio(this.mDeviceDrawRectRatio, this.mDeviceRectRotate);
                this.measureDeviceRect();
            }
            get { return this.mDeviceRectRotate; }
        }

        public HNStruct.HNRectRatio ScreenRectRatio
        {
            set
            {
                this.mScreenRectRatio = value;
                this.measureScreenRect();
            }
            get { return this.mScreenRectRatio; }
        }

        public HNStruct.HNRectRatio DeviceRectRatio
        {
            set
            {
                this.mDeviceRectRatio = value;
                this.mDeviceDrawRectRatio = Utils.rotateRatio(this.mDeviceRectRatio, this.mDeviceRectRotate);
                this.measureDeviceRect();
            }
            get { return this.mDeviceRectRatio; }
        }

        private void measureScreenRect()
        {
            this.mScreenDrawRect.X = (int) ((double) this.mScreenRect.X +
                                            (double) this.mScreenRect.Width * (double) this.mScreenRectRatio.l);
            this.mScreenDrawRect.Y = (int) ((double) this.mScreenRect.Y +
                                            (double) this.mScreenRect.Height * (double) this.mScreenRectRatio.t);
            this.mScreenDrawRect.Width = (int) ((double) this.mScreenRect.Width *
                                                ((double) this.mScreenRectRatio.r - (double) this.mScreenRectRatio.l));
            this.mScreenDrawRect.Height = (int) ((double) this.mScreenRect.Height *
                                                 ((double) this.mScreenRectRatio.b - (double) this.mScreenRectRatio.t));
            if (this.ScreenRatioChanged != null)
                this.ScreenRatioChanged((object) this, (EventArgs) RatioEventArgs.getInstance(this.ScreenRectRatio));
            this.Refresh();
        }

        private void measureDeviceRect()
        {
            this.mDeviceDrawRect.X = (int) ((double) this.mDeviceRect.X +
                                            (double) this.mDeviceRect.Width * (double) this.mDeviceDrawRectRatio.l);
            this.mDeviceDrawRect.Y = (int) ((double) this.mDeviceRect.Y +
                                            (double) this.mDeviceRect.Height * (double) this.mDeviceDrawRectRatio.t);
            this.mDeviceDrawRect.Width = (int) ((double) this.mDeviceRect.Width *
                                                ((double) this.mDeviceDrawRectRatio.r -
                                                 (double) this.mDeviceDrawRectRatio.l));
            this.mDeviceDrawRect.Height = (int) ((double) this.mDeviceRect.Height *
                                                 ((double) this.mDeviceDrawRectRatio.b -
                                                  (double) this.mDeviceDrawRectRatio.t));
            if (this.DeviceRatioChanged != null)
                this.DeviceRatioChanged((object) this,
                    (EventArgs) RatioEventArgs.getInstance(Utils.rotateRatio(this.mDeviceDrawRectRatio,
                        -this.mDeviceRectRotate)));
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(Color.Green, 1f), this.mScreenDrawRect);
            e.Graphics.DrawRectangle(new Pen(Color.Green, 1f), this.mDeviceDrawRect);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(this.ForeColor, 1f);
            pen.DashStyle = DashStyle.Custom;
            pen.DashPattern = new float[2] {4f, 4f};
            e.Graphics.DrawLine(pen, this.mScreenDrawRect.Left, this.mScreenDrawRect.Top, this.mDeviceDrawRect.Left,
                this.mDeviceDrawRect.Top);
            e.Graphics.DrawLine(pen, this.mScreenDrawRect.Left, this.mScreenDrawRect.Bottom, this.mDeviceDrawRect.Left,
                this.mDeviceDrawRect.Bottom);
            e.Graphics.DrawLine(pen, this.mScreenDrawRect.Right, this.mScreenDrawRect.Top, this.mDeviceDrawRect.Right,
                this.mDeviceDrawRect.Top);
            e.Graphics.DrawLine(pen, this.mScreenDrawRect.Right, this.mScreenDrawRect.Bottom,
                this.mDeviceDrawRect.Right, this.mDeviceDrawRect.Bottom);
            pen.Dispose();
            Brush brush = (Brush) new SolidBrush(Color.Green);
            int width = 5;
            int height = 5;
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mScreenDrawRect.Left, this.mScreenDrawRect.Top, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mScreenDrawRect.Left, this.mScreenDrawRect.Bottom - height, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mScreenDrawRect.Right - width, this.mScreenDrawRect.Top, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mScreenDrawRect.Right - width, this.mScreenDrawRect.Bottom - height, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mDeviceDrawRect.Left, this.mDeviceDrawRect.Top, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mDeviceDrawRect.Left, this.mDeviceDrawRect.Bottom - height, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mDeviceDrawRect.Right - width, this.mDeviceDrawRect.Top, width, height));
            e.Graphics.FillRectangle(brush,
                new Rectangle(this.mDeviceDrawRect.Right - width, this.mDeviceDrawRect.Bottom - height, width, height));
            brush.Dispose();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.measureDeviceRect();
            this.measureScreenRect();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            TimerSession.userOperation();
            this.mMouseButton = e.Button;
            this.mMouseLocation = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            TimerSession.userOperation();
            if (this.mMouseButton == MouseButtons.Left)
            {
                Point location = e.Location;
                int num1 = location.X - this.mMouseLocation.X;
                location = e.Location;
                int num2 = location.Y - this.mMouseLocation.Y;
                float ratioX = (float) Math.Round((double) num1 / (double) this.mScreenRect.Width, 3);
                float ratioY = (float) Math.Round((double) num2 / (double) this.mScreenRect.Height, 3);
                if (this.mMouseInRect == this.mScreenDrawRect)
                    onRectChanged(ref this.mScreenRectRatio, this.locationType, ratioX, ratioY);
                else if (this.mMouseInRect == this.mDeviceDrawRect)
                    onRectChanged(ref this.mDeviceDrawRectRatio, this.locationType, ratioX, ratioY);
                this.mMouseLocation = e.Location;
                if (this.mMouseInRect == this.mScreenDrawRect)
                {
                    this.measureScreenRect();
                    this.mMouseInRect = this.mScreenDrawRect;
                }
                else
                {
                    if (!(this.mMouseInRect == this.mDeviceDrawRect))
                        return;
                    this.measureDeviceRect();
                    this.mMouseInRect = this.mDeviceDrawRect;
                }
            }
            else
            {
                if (this.mMouseInRect != Rectangle.Empty)
                {
                    this.locationType = ViewUtils.locationType(this.mMouseInRect, e.Location);
                    if (this.locationType == MouseLocationType.EXTERNAL)
                    {
                        this.mMouseInRect = Rectangle.Empty;
                    }
                    else
                    {
                        this.Cursor = ViewUtils.getCursor8LocationType(this.locationType, this.Cursor);
                        return;
                    }
                }

                this.locationType = ViewUtils.locationType(this.mScreenDrawRect, e.Location);
                if (this.locationType == MouseLocationType.EXTERNAL)
                {
                    this.mMouseInRect = Rectangle.Empty;
                    this.locationType = ViewUtils.locationType(this.mDeviceDrawRect, e.Location);
                    if (this.locationType == MouseLocationType.EXTERNAL)
                    {
                        this.Cursor = Cursors.Default;
                        this.mMouseInRect = Rectangle.Empty;
                    }
                    else
                    {
                        this.mMouseInRect = this.mDeviceDrawRect;
                        this.Cursor = ViewUtils.getCursor8LocationType(this.locationType, this.Cursor);
                    }
                }
                else
                {
                    this.mMouseInRect = this.mScreenDrawRect;
                    this.Cursor = ViewUtils.getCursor8LocationType(this.locationType, this.Cursor);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.mMouseButton = MouseButtons.None;
            this.locationType = MouseLocationType.EXTERNAL;
        }

        private static void onRectChanged(ref HNStruct.HNRectRatio rectRatio, MouseLocationType mouseType, float ratioX,
            float ratioY)
        {
            if (mouseType == MouseLocationType.INNER)
            {
                if ((double) rectRatio.l + (double) ratioX >= 0.0 && (double) rectRatio.r + (double) ratioX <= 1.0)
                {
                    rectRatio.l += ratioX;
                    rectRatio.r += ratioX;
                }

                if ((double) rectRatio.t + (double) ratioY >= 0.0 && (double) rectRatio.b + (double) ratioY <= 1.0)
                {
                    rectRatio.t += ratioY;
                    rectRatio.b += ratioY;
                }
            }
            else
            {
                if ((mouseType & MouseLocationType.LEFT) == MouseLocationType.LEFT && (double) ratioX != 0.0)
                {
                    if ((double) rectRatio.l + (double) ratioX < 0.0)
                        rectRatio.l = 0.0f;
                    else if ((double) rectRatio.l + (double) ratioX > (double) rectRatio.r)
                        rectRatio.l = rectRatio.r;
                    else
                        rectRatio.l += ratioX;
                }

                if ((mouseType & MouseLocationType.RIGHT) == MouseLocationType.RIGHT && (double) ratioX != 0.0)
                {
                    if ((double) rectRatio.r + (double) ratioX > 1.0)
                        rectRatio.r = 1f;
                    else if ((double) rectRatio.r + (double) ratioX < (double) rectRatio.l)
                        rectRatio.r = rectRatio.l;
                    else
                        rectRatio.r += ratioX;
                }

                if ((mouseType & MouseLocationType.TOP) == MouseLocationType.TOP && (double) ratioY != 0.0)
                {
                    if ((double) rectRatio.t + (double) ratioY < 0.0)
                        rectRatio.t = 0.0f;
                    else if ((double) rectRatio.t + (double) ratioY > (double) rectRatio.b)
                        rectRatio.t = rectRatio.b;
                    else
                        rectRatio.t += ratioY;
                }

                if ((mouseType & MouseLocationType.BOTTOM) == MouseLocationType.BOTTOM && (double) ratioY != 0.0)
                {
                    if ((double) rectRatio.b + (double) ratioY > 1.0)
                        rectRatio.b = 1f;
                    else if ((double) rectRatio.b + (double) ratioY < (double) rectRatio.t)
                        rectRatio.b = rectRatio.t;
                    else
                        rectRatio.b += ratioY;
                }
            }

            rectRatio.l = (float) Math.Round((double) rectRatio.l, 3);
            rectRatio.t = (float) Math.Round((double) rectRatio.t, 3);
            rectRatio.r = (float) Math.Round((double) rectRatio.r, 3);
            rectRatio.b = (float) Math.Round((double) rectRatio.b, 3);
        }
    }
}