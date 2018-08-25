// Decompiled with JetBrains decompiler
// Type: HuionTablet.Fixer4DrawLine
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Huion;
using HuionTablet.Lib;

namespace HuionTablet
{
    public class Fixer4DrawLine
    {
        private bool isDrawLine;
        private bool isListenPressValue;
        private bool isMouseDown;
        private Graphics mGraphics;
        private Graphics mGraphics1;
        private Pen mPen;
        private Point mStartPoint = new Point(-1, -1);
        private Control mView;
        public PressValueChanged onPressValueChangedListener;

        public Fixer4DrawLine(Control view, Image image)
        {
            this.mView = view;
            this.mView.MouseDown += new MouseEventHandler(this.onMouseDown);
            this.mView.MouseMove += new MouseEventHandler(this.onMouseMove);
            this.mView.MouseUp += new MouseEventHandler(this.onMouseUp);
            this.mGraphics = Graphics.FromImage(image);
            this.mGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.mGraphics1 = view.CreateGraphics();
            this.mGraphics1.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public bool IsDrawLine
        {
            get { return this.isDrawLine; }
            set { this.isDrawLine = value; }
        }

        public bool IsListenPressValue
        {
            get { return this.isListenPressValue; }
            set { this.isListenPressValue = value; }
        }

        public void startDrawLine(Form form)
        {
            this.initPen();
            HuionApi.listenDeviceInfo(form.Handle);
        }

        public void stopDrawLine()
        {
            HuionApi.stopListenDeviceInfo();
            this.releasePen();
        }

        public void clearDrawLine()
        {
            this.mGraphics.Clear(Color.Transparent);
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            TimerSession.userOperation();
            this.isMouseDown = true;
            if (!this.IsDrawLine)
                return;
            this.mStartPoint = e.Location;
            this.onDrawline(e.Location);
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            TimerSession.userOperation();
            this.isMouseDown = false;
            this.onDrawline(e.Location);
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
            this.mStartPoint = new Point(-1, -1);
            if (this.onPressValueChangedListener == null)
                return;
            this.onPressValueChangedListener(0);
        }

        public void onDrawline()
        {
            this.onDrawline(this.mView.PointToClient(Control.MousePosition));
        }

        public void onDrawline(Point mouseLocation)
        {
            if (this.mStartPoint.X == -1)
                return;
            this.mPen.Width = this.getPenWidth();
            if (this.isMouseDown)
            {
                SolidBrush solidBrush = new SolidBrush(this.mPen.Color);
                this.mGraphics.FillEllipse((Brush) solidBrush, (float) this.mStartPoint.X - this.mPen.Width / 2f,
                    (float) this.mStartPoint.Y - this.mPen.Width / 2f, this.mPen.Width, this.mPen.Width);
                this.mGraphics1.FillEllipse((Brush) solidBrush, (float) this.mStartPoint.X - this.mPen.Width / 2f,
                    (float) this.mStartPoint.Y - this.mPen.Width / 2f, this.mPen.Width, this.mPen.Width);
                solidBrush.Dispose();
            }
            else
            {
                this.mGraphics.DrawLine(this.mPen, this.mStartPoint, mouseLocation);
                this.mGraphics1.DrawLine(this.mPen, this.mStartPoint, mouseLocation);
                this.mStartPoint = mouseLocation;
            }
        }

        private float getPenWidth()
        {
            return (float) HNStruct.globalInfo.penData.ps / 384f;
        }

        public static int getPenPressValue()
        {
            try
            {
                HuionDriverDLL.hndh_get_cursor(ref HNStruct.globalInfo.penData);
            }
            catch (Exception ex)
            {
                HuionLog.saveLog("get press value", ex.Message);
                HuionLog.saveLog("get press value", ex.StackTrace);
            }

            return (int) HNStruct.globalInfo.penData.ps;
        }

        private void initPen()
        {
            this.mPen = new Pen(HuionConst.HuionDrawLine, 2f);
            this.mPen.StartCap = LineCap.Round;
            this.mPen.EndCap = LineCap.Round;
            this.mPen.LineJoin = LineJoin.Round;
        }

        private void releasePen()
        {
            if (this.mPen == null)
                return;
            this.mPen.Dispose();
            this.mPen = (Pen) null;
        }

        public void releaseGraphics()
        {
            if (this.mGraphics != null)
            {
                this.mGraphics.Dispose();
                this.mGraphics = (Graphics) null;
            }

            if (this.mGraphics1 == null)
                return;
            this.mGraphics1.Dispose();
            this.mGraphics1 = (Graphics) null;
        }

        public static void calcBezier2ControlPoint(PointF pStart, PointF pEnd, PointF p1, out PointF p2)
        {
            p2 = new PointF();
            p2.X = (float) ((double) p1.X * 2.0 - ((double) pStart.X + (double) pEnd.X) / 2.0);
            p2.Y = (float) ((double) p1.Y * 2.0 - ((double) pStart.Y + (double) pEnd.Y) / 2.0);
        }
    }
}