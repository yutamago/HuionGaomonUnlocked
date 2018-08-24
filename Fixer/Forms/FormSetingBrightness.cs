// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormSetingBrightness
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormSetingBrightness : Form
  {
    private static List<Monitors> m;
    private static int index;
    private static uint[] brigtnessVal;
    private static uint[] contrastVal;
    private static uint[] gainRVal;
    private static uint[] gainGVal;
    private static uint[] gainBVal;
    private static uint[] blackRVal;
    private static uint[] blackGVal;
    private static uint[] blackBVal;
    private static uint[] factoryResetVal;
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
    private BackgroundWorker backgroundWorker1;
    private ComboBox cBoxcolorPress;
    private Label labelColorPress;
    private Label labelScreen;
    private Button btnReset;

    public FormSetingBrightness()
    {
      this.InitializeComponent();
      this.FormBorderStyle = FormBorderStyle.None;
      this.ControlBox = false;
      this.ShowInTaskbar = false;
      this.FormBorderStyle = FormBorderStyle.None;
      this.ControlBox = false;
    }

    private void FormSetingBrightness_Load(object sender, EventArgs e)
    {
      new Thread(new ThreadStart(this.checkSceenNum)).Start();
      int num1 = this.cboxScreenNum.InvokeRequired ? 1 : 0;
      int num2 = this.InvokeRequired ? 1 : 0;
      this.cboxScreenNum.SelectedText = "请选择";
      this.cboxScreenNum.Enabled = true;
      this.disControl();
    }

    private void addColorPress(List<Monitors> m)
    {
      if (this.cboxScreenNum.InvokeRequired)
      {
        this.Invoke((Delegate) new FormSetingBrightness.myDelegate(this.addColorPress), (object) m);
      }
      else
      {
        int i = 0;
        int num1 = this.cboxScreenNum.InvokeRequired ? 1 : 0;
        int num2 = this.InvokeRequired ? 1 : 0;
        m?.ForEach((Action<Monitors>) (monitor =>
        {
          this.cboxScreenNum.Items.Add((object) ("No" + (object) i + ": " + monitor.PhysicalMonitor.szPhysicalMonitorDescription));
          ++i;
        }));
      }
    }

    public void checkSceenNum()
    {
      try
      {
        FormSetingBrightness.m = VcpFeature.GetAllMonitors();
        this.addColorPress(FormSetingBrightness.m);
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("检测屏幕", ex.Message);
      }
    }

    private void disControl()
    {
      this.tBarBrightness.Enabled = false;
      this.tBarContract.Enabled = false;
      this.cBoxcolorPress.Enabled = false;
      this.trackBarB.Enabled = false;
      this.trackBarG.Enabled = false;
      this.trackBarR.Enabled = false;
      this.btnReset.Enabled = false;
    }

    private void enableContorl()
    {
      this.tBarBrightness.Enabled = true;
      this.tBarContract.Enabled = true;
      this.cBoxcolorPress.Enabled = true;
      this.btnReset.Enabled = true;
    }

    private void trackBarG_Scroll(object sender, EventArgs e)
    {
      this.trackBarG.Maximum = (int) FormSetingBrightness.gainGVal[1];
      uint newVal = (uint) this.trackBarG.Value;
      Console.WriteLine(this.trackBarG.Value);
      Console.WriteLine(FormSetingBrightness.gainGVal[1]);
      VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_GREEN, newVal);
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.RESTORE_FACTORY_DEFAULTS, 1U);
    }

    private void trackBarB_Scroll(object sender, EventArgs e)
    {
      uint newVal = (uint) this.trackBarB.Value;
      this.trackBarB.Maximum = (int) FormSetingBrightness.gainBVal[1];
      Console.WriteLine(FormSetingBrightness.gainBVal[1]);
      VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_BLUE, newVal);
    }

    private void trackBarR_Scroll(object sender, EventArgs e)
    {
      uint newVal = (uint) this.trackBarR.Value;
      this.trackBarR.Maximum = (int) FormSetingBrightness.gainRVal[1];
      Console.WriteLine(FormSetingBrightness.gainRVal[1]);
      VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_RED, newVal);
    }

    private void tBarBrightness_Scroll(object sender, EventArgs e)
    {
      uint newVal = (uint) this.tBarBrightness.Value;
      this.tBarBrightness.Maximum = (int) FormSetingBrightness.brigtnessVal[1];
      Console.WriteLine(FormSetingBrightness.brigtnessVal[1]);
      VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.LUMINANCE, newVal);
    }

    private void tBarContract_Scroll(object sender, EventArgs e)
    {
      this.tBarContract.Maximum = (int) FormSetingBrightness.contrastVal[1];
      uint newVal = (uint) this.tBarContract.Value;
      Console.WriteLine(FormSetingBrightness.contrastVal[1]);
      Console.WriteLine(newVal);
      VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.CONTRAST, newVal);
    }

    private string GetColorPressName(uint colorPreset)
    {
      switch (colorPreset)
      {
        case 5:
          return "6500°K";
        case 8:
          return "9300°K";
        case 11:
          return "User 1";
        default:
          return "Unkown";
      }
    }

    private void cboxScreenNum_SelectionChangeCommitted(object sender, EventArgs e)
    {
      try
      {
        if (!this.cboxScreenNum.SelectedItem.ToString().Contains("Generic Non-PnP Monitor"))
        {
          this.enableContorl();
          this.cBoxcolorPress.Text = "";
          this.cBoxcolorPress.Items.Clear();
          FormSetingBrightness.index = this.cboxScreenNum.SelectedIndex;
          FormSetingBrightness.m[FormSetingBrightness.index].ColorPresets.ForEach((Action<uint>) (cp =>
          {
            if (!(this.GetColorPressName(cp) != "Unkown"))
              return;
            this.cBoxcolorPress.Items.Add((object) this.GetColorPressName(cp));
          }));
          this.cBoxcolorPress.SelectedText = "请选择";
          Console.WriteLine("切换屏幕成功");
          FormSetingBrightness.brigtnessVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.LUMINANCE);
          FormSetingBrightness.contrastVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.CONTRAST);
          FormSetingBrightness.factoryResetVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.RESTORE_FACTORY_DEFAULTS);
          FormSetingBrightness.gainRVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_RED);
          FormSetingBrightness.gainGVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_GREEN);
          FormSetingBrightness.gainBVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_BLUE);
          FormSetingBrightness.blackRVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_BLACK_LEVEL_RED);
          FormSetingBrightness.blackGVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_BLACK_LEVEL_GREEN);
          FormSetingBrightness.blackBVal = VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_BLACK_LEVEL_BLUE);
          this.trackBarR.Maximum = (int) FormSetingBrightness.gainRVal[1];
          this.trackBarB.Maximum = (int) FormSetingBrightness.gainBVal[1];
          this.trackBarG.Maximum = (int) FormSetingBrightness.gainGVal[1];
          this.tBarBrightness.Maximum = (int) FormSetingBrightness.brigtnessVal[1];
          this.tBarContract.Maximum = (int) FormSetingBrightness.contrastVal[1];
          this.tBarBrightness.Value = (int) FormSetingBrightness.brigtnessVal[0];
          this.tBarContract.Value = (int) FormSetingBrightness.contrastVal[0];
        }
        else
          this.disControl();
      }
      catch (Exception ex)
      {
        this.disControl();
        HuionLog.saveLog("change screen", ex.Message);
      }
    }

    private void setRgb()
    {
      this.trackBarR.Value = (int) VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_RED)[0];
      this.trackBarG.Value = (int) VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_GREEN)[0];
      this.trackBarB.Value = (int) VcpFeature.GetVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.VIDEO_GAIN_BLUE)[0];
      this.trackBarB.Enabled = true;
      this.trackBarG.Enabled = true;
      this.trackBarR.Enabled = true;
    }

    private void cBoxcolorPress_SelectedIndexChanged(object sender, EventArgs e)
    {
      int selectedIndex = this.cBoxcolorPress.SelectedIndex;
      uint newVal = FormSetingBrightness.m[FormSetingBrightness.index].ColorPresets[selectedIndex];
      if (this.cBoxcolorPress.SelectedItem.ToString().Equals("6500°K"))
        newVal = 5U;
      else if (this.cBoxcolorPress.SelectedItem.ToString().Equals("9300°K"))
        newVal = 8U;
      else if (this.cBoxcolorPress.SelectedItem.ToString().Equals("User 1"))
        newVal = 11U;
      if (!VcpFeature.setVCPFeature(FormSetingBrightness.m[FormSetingBrightness.index], HNStruct.VCPFeature.COLOR_PRESET, newVal))
        return;
      this.setRgb();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.backgroundWorker1 = new BackgroundWorker();
      this.labelScreen = new Label();
      this.labelColorPress = new Label();
      this.cBoxcolorPress = new ComboBox();
      this.labelB = new Label();
      this.labelG = new Label();
      this.labelR = new Label();
      this.labelContrast = new Label();
      this.trackBarB = new TrackBar();
      this.trackBarG = new TrackBar();
      this.trackBarR = new TrackBar();
      this.tBarContract = new TrackBar();
      this.labelBright = new Label();
      this.tBarBrightness = new TrackBar();
      this.cboxScreenNum = new ComboBox();
      this.btnReset = new Button();
      this.trackBarB.BeginInit();
      this.trackBarG.BeginInit();
      this.trackBarR.BeginInit();
      this.tBarContract.BeginInit();
      this.tBarBrightness.BeginInit();
      this.SuspendLayout();
      this.labelScreen.AutoSize = true;
      this.labelScreen.Location = new Point(30, 32);
      this.labelScreen.Name = "labelScreen";
      this.labelScreen.Size = new Size(29, 12);
      this.labelScreen.TabIndex = 15;
      this.labelScreen.Text = "屏幕";
      this.labelColorPress.AutoSize = true;
      this.labelColorPress.Location = new Point(30, 179);
      this.labelColorPress.Name = "labelColorPress";
      this.labelColorPress.Size = new Size(29, 12);
      this.labelColorPress.TabIndex = 14;
      this.labelColorPress.Text = "色温";
      this.cBoxcolorPress.FormattingEnabled = true;
      this.cBoxcolorPress.Location = new Point(88, 176);
      this.cBoxcolorPress.Name = "cBoxcolorPress";
      this.cBoxcolorPress.Size = new Size(188, 20);
      this.cBoxcolorPress.TabIndex = 11;
      this.cBoxcolorPress.SelectedIndexChanged += new EventHandler(this.cBoxcolorPress_SelectedIndexChanged);
      this.labelB.AutoSize = true;
      this.labelB.Location = new Point(30, 330);
      this.labelB.Name = "labelB";
      this.labelB.Size = new Size(11, 12);
      this.labelB.TabIndex = 10;
      this.labelB.Text = "B";
      this.labelG.AutoSize = true;
      this.labelG.Location = new Point(30, 279);
      this.labelG.Name = "labelG";
      this.labelG.Size = new Size(11, 12);
      this.labelG.TabIndex = 9;
      this.labelG.Text = "G";
      this.labelR.AutoSize = true;
      this.labelR.Location = new Point(30, 228);
      this.labelR.Name = "labelR";
      this.labelR.Size = new Size(11, 12);
      this.labelR.TabIndex = 8;
      this.labelR.Text = "R";
      this.labelContrast.AutoSize = true;
      this.labelContrast.Location = new Point(18, 125);
      this.labelContrast.Name = "labelContrast";
      this.labelContrast.Size = new Size(41, 12);
      this.labelContrast.TabIndex = 7;
      this.labelContrast.Text = "对比度";
      this.trackBarB.Location = new Point(88, 330);
      this.trackBarB.Name = "trackBarB";
      this.trackBarB.Size = new Size(172, 45);
      this.trackBarB.TabIndex = 6;
      this.trackBarB.TickStyle = TickStyle.None;
      this.trackBarB.Scroll += new EventHandler(this.trackBarB_Scroll);
      this.trackBarG.Location = new Point(88, 279);
      this.trackBarG.Name = "trackBarG";
      this.trackBarG.Size = new Size(172, 45);
      this.trackBarG.TabIndex = 5;
      this.trackBarG.TickStyle = TickStyle.None;
      this.trackBarG.Scroll += new EventHandler(this.trackBarG_Scroll);
      this.trackBarR.Location = new Point(88, 228);
      this.trackBarR.Name = "trackBarR";
      this.trackBarR.Size = new Size(172, 45);
      this.trackBarR.TabIndex = 4;
      this.trackBarR.TickStyle = TickStyle.None;
      this.trackBarR.Scroll += new EventHandler(this.trackBarR_Scroll);
      this.tBarContract.Location = new Point(88, 125);
      this.tBarContract.Name = "tBarContract";
      this.tBarContract.Size = new Size(172, 45);
      this.tBarContract.TabIndex = 3;
      this.tBarContract.TickStyle = TickStyle.None;
      this.tBarContract.Scroll += new EventHandler(this.tBarContract_Scroll);
      this.labelBright.AutoSize = true;
      this.labelBright.Location = new Point(30, 74);
      this.labelBright.Name = "labelBright";
      this.labelBright.Size = new Size(29, 12);
      this.labelBright.TabIndex = 2;
      this.labelBright.Text = "亮度";
      this.tBarBrightness.Location = new Point(88, 74);
      this.tBarBrightness.Name = "tBarBrightness";
      this.tBarBrightness.Size = new Size(172, 45);
      this.tBarBrightness.TabIndex = 1;
      this.tBarBrightness.TickStyle = TickStyle.None;
      this.tBarBrightness.Scroll += new EventHandler(this.tBarBrightness_Scroll);
      this.cboxScreenNum.FormattingEnabled = true;
      this.cboxScreenNum.Location = new Point(88, 24);
      this.cboxScreenNum.Name = "cboxScreenNum";
      this.cboxScreenNum.Size = new Size(188, 20);
      this.cboxScreenNum.TabIndex = 0;
      this.cboxScreenNum.SelectionChangeCommitted += new EventHandler(this.cboxScreenNum_SelectionChangeCommitted);
      this.btnReset.Location = new Point(88, 381);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new Size(75, 23);
      this.btnReset.TabIndex = 16;
      this.btnReset.Text = "重置";
      this.btnReset.UseVisualStyleBackColor = true;
      this.btnReset.Click += new EventHandler(this.btnReset_Click);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(323, 423);
      this.Controls.Add((Control) this.btnReset);
      this.Controls.Add((Control) this.labelScreen);
      this.Controls.Add((Control) this.labelColorPress);
      this.Controls.Add((Control) this.cBoxcolorPress);
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
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormSetingBrightness);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.FormSetingBrightness_Load);
      this.trackBarB.EndInit();
      this.trackBarG.EndInit();
      this.trackBarR.EndInit();
      this.tBarContract.EndInit();
      this.tBarBrightness.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void myDelegate(List<Monitors> m);
  }
}
