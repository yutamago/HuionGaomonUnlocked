// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormSetingBrightness
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormSetingBrightness : BaseForm
  {
    private HNStruct.MONITORINFOEX[] monitorInfos = new HNStruct.MONITORINFOEX[10];
    private int displayNum;
    private static unsafe short* RGBRamp1;
    private static unsafe short* RGBRamp2;
    private static bool initialized;
    private static int hdc;
    private static int a;
    private IContainer components;
    private ComboBox cboxScreenNum;
    private TrackBar tBarBrightness;
    private Label labelBright;
    private TrackBar tBarContract;
    private TrackBar trackBarR;
    private TrackBar trackBarG;
    private TrackBar trackBarB;
    private Label labelContrast;
    private Label labelR;
    private Label labelG;
    private Label labelB;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetDesktopWindow();

    [DllImport("gdi32.dll")]
    private static extern unsafe bool SetDeviceGammaRamp(int hdc, void* ramp);

    [DllImport("gdi32.dll")]
    private static extern unsafe bool GetDeviceGammaRamp(int hdc, void* ramp);

    public FormSetingBrightness(int num, HNStruct.MONITORINFOEX[] infos)
    {
      this.displayNum = num;
      this.monitorInfos = infos;
      this.InitializeComponent();
      this.ControlBox = true;
      this.ShowSettingsIcon = false;
      this.MinimizeBox = false;
      this.AutoScroll = true;
      this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
      this.Text = ResourceCulture.GetString("SettingsTitle");
    }

    private unsafe void FormSetingBrightness_Load(object sender, EventArgs e)
    {
      this.tBarBrightness.Maximum = (int) byte.MaxValue;
      this.tBarContract.Maximum = 100;
      this.tBarContract.Minimum = 0;
      FormSetingBrightness.GetDeviceGammaRamp(FormSetingBrightness.hdc, (void*) FormSetingBrightness.RGBRamp1);
      this.setComboxDisplay();
    }

    private static void initializedClass()
    {
      if (FormSetingBrightness.initialized)
        return;
      FormSetingBrightness.hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();
      FormSetingBrightness.initialized = true;
    }

    private static unsafe bool setBrightness(int brightness)
    {
      FormSetingBrightness.initializedClass();
      if (brightness > (int) byte.MaxValue)
        brightness = (int) byte.MaxValue;
      if (brightness < 0)
        brightness = 0;
      short* numPtr1 = stackalloc short[768];
      short* numPtr2 = numPtr1;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = 0; index2 < 256; ++index2)
        {
          int num = index2 * (brightness + 128);
          if (num > (int) ushort.MaxValue)
            num = (int) ushort.MaxValue;
          *numPtr2 = (short) num;
          numPtr2 += 2;
        }
      }
      return FormSetingBrightness.SetDeviceGammaRamp(FormSetingBrightness.hdc, (void*) numPtr1);
    }

    private static void setContract()
    {
      FormSetingBrightness.initializedClass();
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        int num = 0;
        while (num < (int) byte.MaxValue)
          ++num;
      }
    }

    private void setComboxDisplay()
    {
      if (this.displayNum == 1 && (int) HNStruct.globalInfo.userConfig.curScreenIndex == this.displayNum || (int) HNStruct.globalInfo.userConfig.curScreenIndex > this.displayNum)
        HNStruct.globalInfo.userConfig.curScreenIndex = 0U;
      this.cboxScreenNum.Items.Clear();
      for (int index = 0; index < this.displayNum; ++index)
      {
        int num1 = this.monitorInfos[index].Monitor.Right - this.monitorInfos[index].Monitor.Left;
        int num2 = this.monitorInfos[index].Monitor.Bottom - this.monitorInfos[index].Monitor.Top;
        if (this.monitorInfos[index].Flags == 1U)
          this.cboxScreenNum.Items.Add((object) (ResourceCulture.GetString("FormWorkArea_DisplayText") + (object) (index + 1) + ":" + ResourceCulture.GetString("FormWorkArea_WidthText") + (object) num1 + ResourceCulture.GetString("FormWorkArea_HeightText") + (object) num2 + ResourceCulture.GetString("FormWorkArea_MainDisPlay")));
        else
          this.cboxScreenNum.Items.Add((object) (ResourceCulture.GetString("FormWorkArea_DisplayText") + (object) (index + 1) + ":" + ResourceCulture.GetString("FormWorkArea_WidthText") + (object) num1 + ResourceCulture.GetString("FormWorkArea_HeightText") + (object) num2));
      }
      if (this.displayNum > 1)
        this.cboxScreenNum.Items.Add((object) ResourceCulture.GetString("FormWorkArea_AllScreenText"));
      this.cboxScreenNum.SelectedIndex = (int) HNStruct.globalInfo.userConfig.curScreenIndex;
    }

    private void tBarBrightness_Scroll(object sender, EventArgs e)
    {
      FormSetingBrightness.setBrightness(this.tBarBrightness.Value);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cboxScreenNum = new ComboBox();
      this.tBarBrightness = new TrackBar();
      this.labelBright = new Label();
      this.tBarContract = new TrackBar();
      this.trackBarR = new TrackBar();
      this.trackBarG = new TrackBar();
      this.trackBarB = new TrackBar();
      this.labelContrast = new Label();
      this.labelR = new Label();
      this.labelG = new Label();
      this.labelB = new Label();
      this.tBarBrightness.BeginInit();
      this.tBarContract.BeginInit();
      this.trackBarR.BeginInit();
      this.trackBarG.BeginInit();
      this.trackBarB.BeginInit();
      this.SuspendLayout();
      this.cboxScreenNum.FormattingEnabled = true;
      this.cboxScreenNum.Location = new Point(32, 62);
      this.cboxScreenNum.Name = "cboxScreenNum";
      this.cboxScreenNum.Size = new Size(256, 20);
      this.cboxScreenNum.TabIndex = 0;
      this.tBarBrightness.Location = new Point(90, 102);
      this.tBarBrightness.Name = "tBarBrightness";
      this.tBarBrightness.Size = new Size(172, 45);
      this.tBarBrightness.TabIndex = 1;
      this.tBarBrightness.Scroll += new EventHandler(this.tBarBrightness_Scroll);
      this.labelBright.AutoSize = true;
      this.labelBright.Location = new Point(30, 112);
      this.labelBright.Name = "labelBright";
      this.labelBright.Size = new Size(29, 12);
      this.labelBright.TabIndex = 2;
      this.labelBright.Text = "亮度";
      this.tBarContract.Location = new Point(90, 154);
      this.tBarContract.Name = "tBarContract";
      this.tBarContract.Size = new Size(172, 45);
      this.tBarContract.TabIndex = 3;
      this.trackBarR.Location = new Point(370, 237);
      this.trackBarR.Name = "trackBarR";
      this.trackBarR.Size = new Size(104, 45);
      this.trackBarR.TabIndex = 4;
      this.trackBarG.Location = new Point(370, 288);
      this.trackBarG.Name = "trackBarG";
      this.trackBarG.Size = new Size(104, 45);
      this.trackBarG.TabIndex = 5;
      this.trackBarB.Location = new Point(370, 339);
      this.trackBarB.Name = "trackBarB";
      this.trackBarB.Size = new Size(104, 45);
      this.trackBarB.TabIndex = 6;
      this.labelContrast.AutoSize = true;
      this.labelContrast.Location = new Point(32, 154);
      this.labelContrast.Name = "labelContrast";
      this.labelContrast.Size = new Size(41, 12);
      this.labelContrast.TabIndex = 7;
      this.labelContrast.Text = "对比度";
      this.labelR.AutoSize = true;
      this.labelR.Location = new Point(324, 237);
      this.labelR.Name = "labelR";
      this.labelR.Size = new Size(11, 12);
      this.labelR.TabIndex = 8;
      this.labelR.Text = "R";
      this.labelG.AutoSize = true;
      this.labelG.Location = new Point(326, 288);
      this.labelG.Name = "labelG";
      this.labelG.Size = new Size(11, 12);
      this.labelG.TabIndex = 9;
      this.labelG.Text = "G";
      this.labelB.AutoSize = true;
      this.labelB.Location = new Point(328, 339);
      this.labelB.Name = "labelB";
      this.labelB.Size = new Size(11, 12);
      this.labelB.TabIndex = 10;
      this.labelB.Text = "B";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(603, 410);
      this.Controls.Add((Control) this.labelB);
      this.Controls.Add((Control) this.labelG);
      this.Controls.Add((Control) this.labelR);
      this.Controls.Add((Control) this.labelContrast);
      this.Controls.Add((Control) this.trackBarB);
      this.Controls.Add((Control) this.trackBarG);
      this.Controls.Add((Control) this.trackBarR);
      this.Controls.Add((Control) this.tBarContract);
      this.Controls.Add((Control) this.labelBright);
      this.Controls.Add((Control) this.tBarBrightness);
      this.Controls.Add((Control) this.cboxScreenNum);
      this.Name = nameof (FormSetingBrightness);
      this.ShowSettingsIcon = true;
      this.Text = nameof (FormSetingBrightness);
      this.Load += new EventHandler(this.FormSetingBrightness_Load);
      this.tBarBrightness.EndInit();
      this.tBarContract.EndInit();
      this.trackBarR.EndInit();
      this.trackBarG.EndInit();
      this.trackBarB.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
