// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormCalibrate
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HuionTablet.utils;

namespace HuionTablet
{
    public class FormCalibrate : Form
    {
        private static string sourcePath = Application.StartupPath + "\\res\\config_user.xml";
        private Button btnDefault;
        private Button btnStart;
        private Button btnStop;
        private IContainer components;
        private Label labelHint;
        private int mCurrentPoint;
        private Point[] mDisplayPoints;
        private Size mPointSize = new Size(10, 10);
        private HNStruct.RECT mScreenRect;
        private HNStruct.HNPoint[] mUserPoints;

        public FormCalibrate(HNStruct.RECT rect)
        {
            this.InitializeComponent();
            this.mScreenRect = rect;
        }

        public static void showForm(HNStruct.RECT screenRect)
        {
            FormCalibrate formCalibrate = new FormCalibrate(screenRect);
            formCalibrate.Focus();
            int num = (int) formCalibrate.ShowDialog();
        }

        private void FormCalibrate_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.mScreenRect.Left, this.mScreenRect.Top);
            this.Size = new Size(this.mScreenRect.Right - this.mScreenRect.Left,
                this.mScreenRect.Bottom - this.mScreenRect.Top);
            this.btnDefault.Text = ResourceCulture.GetString("CalibrationDefault");
            this.btnStart.Text = ResourceCulture.GetString("CalibrationStart");
            this.btnStop.Text = ResourceCulture.GetString("CalibrationStop");
            this.labelHint.Text = ResourceCulture.GetString("CalibrationHint");
            this.btnDefault.Location = new Point((this.Width - this.btnDefault.Width) / 2, this.btnDefault.Location.Y);
            this.btnStart.Location = new Point((this.Width - this.btnStart.Width) / 2, this.btnStart.Location.Y);
            this.btnStop.Location = new Point((this.Width - this.btnStop.Width) / 2, this.btnStop.Location.Y);
            this.labelHint.Location = new Point((this.Width - this.labelHint.Width) / 2,
                (this.Height / 2 - this.labelHint.Height) / 2);
            this.mUserPoints = new HNStruct.HNPoint[9];
            this.mDisplayPoints = new Point[9];
            this.mDisplayPoints[0].X = this.Width / 20;
            this.mDisplayPoints[0].Y = this.Height / 20;
            this.mDisplayPoints[1].X = this.Width * 10 / 20;
            this.mDisplayPoints[1].Y = this.mDisplayPoints[0].Y;
            this.mDisplayPoints[2].X = this.Width * 19 / 20;
            this.mDisplayPoints[2].Y = this.mDisplayPoints[0].Y;
            this.mDisplayPoints[3].X = this.mDisplayPoints[0].X;
            this.mDisplayPoints[3].Y = this.Height * 10 / 20;
            this.mDisplayPoints[4].X = this.mDisplayPoints[1].X;
            this.mDisplayPoints[4].Y = this.mDisplayPoints[3].Y;
            this.mDisplayPoints[5].X = this.mDisplayPoints[2].X;
            this.mDisplayPoints[5].Y = this.mDisplayPoints[3].Y;
            this.mDisplayPoints[6].X = this.mDisplayPoints[0].X;
            this.mDisplayPoints[6].Y = this.Height * 19 / 20;
            this.mDisplayPoints[7].X = this.mDisplayPoints[1].X;
            this.mDisplayPoints[7].Y = this.mDisplayPoints[6].Y;
            this.mDisplayPoints[8].X = this.mDisplayPoints[2].X;
            this.mDisplayPoints[8].Y = this.mDisplayPoints[6].Y;
            HuionDriverDLL.hnd_start_calibrate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SolidBrush solidBrush1 = new SolidBrush(Color.Black);
            SolidBrush solidBrush2 = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Black);
            pen.Width = 5f;
            int num = this.Width / 60;
            for (int index = 0; index < this.mDisplayPoints.Length; ++index)
            {
                Point mDisplayPoint = this.mDisplayPoints[index];
                if (index == this.mCurrentPoint)
                    e.Graphics.FillEllipse((Brush) solidBrush2, new Rectangle(mDisplayPoint, this.mPointSize));
                else if (index < this.mCurrentPoint)
                    e.Graphics.FillEllipse((Brush) solidBrush1, new Rectangle(mDisplayPoint, this.mPointSize));
                else
                    break;
                Point point = new Point(mDisplayPoint.X + this.mPointSize.Width / 2,
                    mDisplayPoint.Y + this.mPointSize.Height / 2);
                e.Graphics.DrawLine(pen, point - new Size(10, 0), new Point(point.X - num, point.Y));
                e.Graphics.DrawLine(pen, point + new Size(10, 0), new Point(point.X + num, point.Y));
                e.Graphics.DrawLine(pen, point - new Size(0, 10), new Point(point.X, point.Y - num));
                e.Graphics.DrawLine(pen, point + new Size(0, 10), new Point(point.X, point.Y + num));
            }

            solidBrush1.Dispose();
            solidBrush2.Dispose();
            pen.Dispose();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != 74)
                return;
            this.mUserPoints[this.mCurrentPoint] = (HNStruct.HNPoint) Marshal.PtrToStructure(
                ((HNStruct.COPYDATASTRUCT) Marshal.PtrToStructure(m.LParam, typeof(HNStruct.COPYDATASTRUCT))).lpData,
                typeof(HNStruct.HNPoint));
            ++this.mCurrentPoint;
            if (this.mCurrentPoint >= this.mUserPoints.Length)
            {
                IntPtr address = Utils.getAddress<HNStruct.HNPoint>(this.mUserPoints);
                this.saveCalibrate(address);
                Marshal.FreeHGlobal(address);
                this.Close();
                this.Dispose();
            }
            else
                this.Refresh();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            TabletConfigUtils.config.bCalibrated = (byte) 0;
            IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
            Marshal.StructureToPtr((object) TabletConfigUtils.config, num1, true);
            IntPtr num2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNTabletInfo)));
            Marshal.StructureToPtr((object) HNStruct.globalInfo.tabletInfo, num2, true);
            IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(sourcePath);
            int num3 = (int) HuionDriverDLL.hnx_save_config(num1, num2, coTaskMemAuto, coTaskMemAuto);
            HuionDriverDLL.hnd_notify_config_changed();
            Marshal.FreeHGlobal(num2);
            Marshal.FreeHGlobal(coTaskMemAuto);
            Marshal.FreeHGlobal(num1);
            this.Close();
            this.Dispose();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.mUserPoints = new HNStruct.HNPoint[9];
            this.mCurrentPoint = 0;
            this.Refresh();
        }

        private void saveCalibrate(IntPtr ptr)
        {
            try
            {
                IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
                Marshal.StructureToPtr((object) TabletConfigUtils.config, num1, true);
                IntPtr num2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNTabletInfo)));
                Marshal.StructureToPtr((object) HNStruct.globalInfo.tabletInfo, num2, true);
                IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(sourcePath);
                HuionDriverDLL.hnc_calibrate_monitor(num2, num1, ptr);
                int num3 = (int) HuionDriverDLL.hnx_save_config(num1, num2, coTaskMemAuto, coTaskMemAuto);
                HuionDriverDLL.hnd_notify_config_changed();
                Marshal.FreeHGlobal(coTaskMemAuto);
                Marshal.FreeHGlobal(num2);
                Marshal.FreeHGlobal(num1);
            }
            catch (Exception ex)
            {
                HuionLog.printSaveLog("", ex.Message);
                HuionLog.printSaveLog("", ex.StackTrace);
            }
        }

        private void FormCalibrate_FormClosed(object sender, FormClosedEventArgs e)
        {
            HuionDriverDLL.hnd_end_calibrate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStop = new Button();
            this.btnStart = new Button();
            this.btnDefault = new Button();
            this.labelHint = new Label();
            this.SuspendLayout();
            this.btnStop.Anchor = AnchorStyles.Bottom;
            this.btnStop.FlatStyle = FlatStyle.Flat;
            this.btnStop.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.btnStop.Location = new Point(390, 536);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(254, 31);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "button1";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.btnStart.Anchor = AnchorStyles.Bottom;
            this.btnStart.FlatStyle = FlatStyle.Flat;
            this.btnStart.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.btnStart.Location = new Point(390, 493);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(254, 31);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "button2";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.btnDefault.Anchor = AnchorStyles.Bottom;
            this.btnDefault.FlatStyle = FlatStyle.Flat;
            this.btnDefault.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.btnDefault.Location = new Point(390, 430);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new Size(254, 31);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "button3";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
            this.labelHint.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.labelHint.Location = new Point(322, 150);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new Size(368, 82);
            this.labelHint.TabIndex = 3;
            this.labelHint.Text = "label1";
            this.labelHint.TextAlign = ContentAlignment.TopCenter;
            this.AutoScaleDimensions = new SizeF(96f, 96f);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(1068, 706);
            this.Controls.Add((Control) this.labelHint);
            this.Controls.Add((Control) this.btnDefault);
            this.Controls.Add((Control) this.btnStart);
            this.Controls.Add((Control) this.btnStop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(FormCalibrate);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PenMontiorCalibration";
            this.TopMost = true;
            this.FormClosed += new FormClosedEventHandler(this.FormCalibrate_FormClosed);
            this.Load += new EventHandler(this.FormCalibrate_Load);
            this.ResumeLayout(false);
        }
    }
}