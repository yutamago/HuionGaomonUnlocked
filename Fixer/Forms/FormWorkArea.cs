// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormWorkArea
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using HuionTablet.Lib;
using HuionTablet.utils;
using HuionTablet.view;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormWorkArea : Form, IDestroy
  {
    public int displayNum;
    public HNStruct.MONITORINFOEX[] monitorInfos;
    private IContainer components;
    private Label labelSelectScreen;
    private ComboBox comboBoxSelectScreen;
    private Button buttonWhileBoard;
    private Button buttonScreenRatio;
    private Label labelTop;
    private Label labelBottom;
    private Label labelLeft;
    private Label labelRight;
    private TextBox textBoxTop;
    private TextBox textBoxBottom;
    private TextBox textBoxLeft;
    private TextBox textBoxRight;
    private Button buttonCalibration;
    private GroupBox groupBoxRotate;
    private RadioButton rotateThreeNinety;
    private RadioButton rotateTwoNinety;
    private RadioButton rotateNinety;
    private RadioButton rotateZero;
    private Label label1;
    private Button buttonWhileDisplay;
    private GroupBox groupBoxCustomArea;
    private HuionWorkAreaPictureView huionWorkAreaPictrueView1;
    private HuionWorkAreaPictureRect huionWorkAreaPictureRect1;
    private Button btnIdentify;

    public FormWorkArea(int num, HNStruct.MONITORINFOEX[] infos)
    {
      this.displayNum = num;
      this.monitorInfos = infos;
      this.InitializeComponent();
      this.huionWorkAreaPictrueView1.DeviceImageMaxHeight = DpiHelper.getInstance().DpiMatrix(190);
      this.huionWorkAreaPictrueView1.DeviceImageMaxWidth = DpiHelper.getInstance().DpiMatrix(400);
      this.huionWorkAreaPictrueView1.IntervalSpace = DpiHelper.getInstance().DpiMatrix(5);
      this.huionWorkAreaPictrueView1.ScreenshotMaxHeight = DpiHelper.getInstance().DpiMatrix(290);
      this.huionWorkAreaPictrueView1.ScreenshotMaxWidth = DpiHelper.getInstance().DpiMatrix(640);
    }

    private void buttonWhileDisplay_Click(object sender, EventArgs e)
    {
      TabletConfigUtils.config.screenAreaRatio = HNStruct.HNRectRatio.DEFAULT;
      this.huionWorkAreaPictureRect1.ScreenRectRatio = TabletConfigUtils.config.screenAreaRatio;
      this.huionWorkAreaPictrueView1.Screenshot = ScreenHelper.getScreenImage((int) TabletConfigUtils.config.curScreenIndex, this.monitorInfos);
    }

    private void FormWorkArea_Load(object sender, EventArgs e)
    {
      this.labelSelectScreen.TextAlign = ContentAlignment.MiddleRight;
      this.setViewTextLocale();
      this.btnIdentify.Visible = this.displayNum > 1;
      this.KeyPreview = true;
      this.buttonCalibration.Location = new Point((this.Width - this.buttonCalibration.Width) / 2, this.buttonCalibration.Location.Y);
      this.buttonCalibration.Visible = Convert.ToBoolean(HNStruct.globalInfo.tabletInfo.bMonitor);
      this.buttonCalibration.Click += new EventHandler(this.ButtonCalibration_Click);
      this.SetComboxDisplay();
      this.huionWorkAreaPictureRect1.Parent = (Control) this.huionWorkAreaPictrueView1;
      this.huionWorkAreaPictureRect1.DeviceRectRotate = (int) TabletConfigUtils.config.rotateAngle;
      this.huionWorkAreaPictureRect1.ScreenRectRatio = TabletConfigUtils.config.screenAreaRatio;
      this.huionWorkAreaPictureRect1.DeviceRectRatio = TabletConfigUtils.config.workAreaRatio;
      this.huionWorkAreaPictureRect1.ScreenRatioChanged += new EventHandler(this.HuionWorkAreaPictureRect1_ScreenRatioChanged);
      this.huionWorkAreaPictureRect1.DeviceRatioChanged += new EventHandler(this.HuionWorkAreaPictureRect1_DeviceRatioChanged);
      this.huionWorkAreaPictrueView1.Callback += new EventHandler(this.HuionWorkAreaPictrueView1_Callback);
      this.huionWorkAreaPictrueView1.DeviceImage = HuionRender.blowupImage(ImageHelper.getDllImage(HNStruct.devTypeString), DpiHelper.getInstance().XDpi);
      this.huionWorkAreaPictrueView1.setDeviceInfo(HNStruct.globalInfo.layoutTablet.size, HNStruct.globalInfo.layoutTablet.penArea, (int) TabletConfigUtils.config.rotateAngle);
      this.textBoxLeft.Text = Convert.ToDouble(TabletConfigUtils.config.workAreaRatio.l).ToString("0.000");
      this.textBoxRight.Text = Convert.ToDouble(TabletConfigUtils.config.workAreaRatio.r).ToString("0.000");
      this.textBoxTop.Text = Convert.ToDouble(TabletConfigUtils.config.workAreaRatio.t).ToString("0.000");
      this.textBoxBottom.Text = Convert.ToDouble(TabletConfigUtils.config.workAreaRatio.b).ToString("0.000");
      this.textBoxLeft.Click += new EventHandler(this.onTextFocusEnter);
      this.textBoxRight.Click += new EventHandler(this.onTextFocusEnter);
      this.textBoxTop.Click += new EventHandler(this.onTextFocusEnter);
      this.textBoxBottom.Click += new EventHandler(this.onTextFocusEnter);
      switch (TabletConfigUtils.config.rotateAngle)
      {
        case 0:
          this.rotateZero.Checked = true;
          break;
        case 90:
          this.rotateNinety.Checked = true;
          break;
        case 180:
          this.rotateTwoNinety.Checked = true;
          break;
        case 270:
          this.rotateThreeNinety.Checked = true;
          break;
      }
    }

    private void ButtonCalibration_Click(object sender, EventArgs e)
    {
      if (TabletConfigUtils.config.curScreenIndex < 0U || (long) TabletConfigUtils.config.curScreenIndex >= (long) this.displayNum)
        return;
      FormCalibrate.showForm(this.monitorInfos[(int) TabletConfigUtils.config.curScreenIndex].Monitor);
    }

    private void HuionWorkAreaPictrueView1_Callback(object sender, EventArgs e)
    {
      HuionWorkAreaPictureView workAreaPictureView = (HuionWorkAreaPictureView) sender;
      workAreaPictureView.Location = new Point((this.Width - workAreaPictureView.Width) / 2, workAreaPictureView.Location.Y);
      this.huionWorkAreaPictureRect1.Location = new Point(0, 0);
      this.huionWorkAreaPictureRect1.Width = workAreaPictureView.Width;
      this.huionWorkAreaPictureRect1.Height = workAreaPictureView.Height;
      this.huionWorkAreaPictureRect1.ScreenRect = workAreaPictureView.ScreenshotRect;
      this.huionWorkAreaPictureRect1.DeviceRect = workAreaPictureView.PenWorkRect;
      this.huionWorkAreaPictureRect1.DeviceRectRotate = ((PictureViewEventArgs) e).Rotate;
    }

    private void HuionWorkAreaPictureRect1_ScreenRatioChanged(object sender, EventArgs e)
    {
      RatioEventArgs ratioEventArgs = (RatioEventArgs) e;
      TabletConfigUtils.config.screenAreaRatio = ratioEventArgs.Ratio;
    }

    private void HuionWorkAreaPictureRect1_DeviceRatioChanged(object sender, EventArgs e)
    {
      RatioEventArgs ratioEventArgs = (RatioEventArgs) e;
      TabletConfigUtils.config.workAreaRatio = ratioEventArgs.Ratio;
      this.textBoxLeft.Tag = (object) "1";
      this.textBoxRight.Tag = (object) "1";
      this.textBoxTop.Tag = (object) "1";
      this.textBoxBottom.Tag = (object) "1";
      this.textBoxSetting(this.textBoxLeft, e);
      this.textBoxSetting(this.textBoxRight, e);
      this.textBoxSetting(this.textBoxTop, e);
      this.textBoxSetting(this.textBoxBottom, e);
      this.textBoxLeft.Tag = (object) null;
      this.textBoxRight.Tag = (object) null;
      this.textBoxTop.Tag = (object) null;
      this.textBoxBottom.Tag = (object) null;
    }

    private void textBoxSetting(TextBox textbox, EventArgs e)
    {
      RatioEventArgs ratioEventArgs = (RatioEventArgs) e;
      switch (textbox.Text.Length)
      {
        case 3:
          if (textbox == this.textBoxLeft)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.l).ToString("0.0");
            break;
          }
          if (textbox == this.textBoxRight)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.r).ToString("0.0");
            break;
          }
          if (textbox == this.textBoxTop)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.t).ToString("0.0");
            break;
          }
          if (textbox != this.textBoxBottom)
            break;
          textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.b).ToString("0.0");
          break;
        case 4:
          if (textbox == this.textBoxLeft)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.l).ToString("0.00");
            break;
          }
          if (textbox == this.textBoxRight)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.r).ToString("0.00");
            break;
          }
          if (textbox == this.textBoxTop)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.t).ToString("0.00");
            break;
          }
          if (textbox != this.textBoxBottom)
            break;
          textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.b).ToString("0.00");
          break;
        case 5:
          if (textbox == this.textBoxLeft)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.l).ToString("0.000");
            break;
          }
          if (textbox == this.textBoxRight)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.r).ToString("0.000");
            break;
          }
          if (textbox == this.textBoxTop)
          {
            textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.t).ToString("0.000");
            break;
          }
          if (textbox != this.textBoxBottom)
            break;
          textbox.Text = Convert.ToDouble(ratioEventArgs.Ratio.b).ToString("0.000");
          break;
      }
    }

    private void SetComboxDisplay()
    {
      if (this.displayNum == 1)
        TabletConfigUtils.config.curScreenIndex = 0U;
      if (this.displayNum != 1 && (int) TabletConfigUtils.config.curScreenIndex > this.displayNum)
        TabletConfigUtils.config.curScreenIndex = (uint) this.displayNum;
      this.comboBoxSelectScreen.Items.Clear();
      for (int index = 0; index < this.displayNum; ++index)
      {
        int num1 = this.monitorInfos[index].Monitor.Right - this.monitorInfos[index].Monitor.Left;
        int num2 = this.monitorInfos[index].Monitor.Bottom - this.monitorInfos[index].Monitor.Top;
        if (this.monitorInfos[index].Flags == 1U)
          this.comboBoxSelectScreen.Items.Add((object) (ResourceCulture.GetString("FormWorkArea_DisplayText") + (object) (index + 1) + " : " + ResourceCulture.GetString("FormWorkArea_WidthText") + (object) num1 + ", " + ResourceCulture.GetString("FormWorkArea_HeightText") + (object) num2 + ResourceCulture.GetString("FormWorkArea_MainDisPlay")));
        else
          this.comboBoxSelectScreen.Items.Add((object) (ResourceCulture.GetString("FormWorkArea_DisplayText") + (object) (index + 1) + " : " + ResourceCulture.GetString("FormWorkArea_WidthText") + (object) num1 + ", " + ResourceCulture.GetString("FormWorkArea_HeightText") + (object) num2));
      }
      if (this.displayNum > 1)
        this.comboBoxSelectScreen.Items.Add((object) ResourceCulture.GetString("FormWorkArea_AllScreenText"));
      this.huionWorkAreaPictrueView1.Screenshot = ScreenHelper.getScreenImage((int) TabletConfigUtils.config.curScreenIndex, this.monitorInfos);
      this.comboBoxSelectScreen.SelectedIndex = (int) TabletConfigUtils.config.curScreenIndex;
    }

    private void setViewTextLocale()
    {
      this.buttonWhileDisplay.Text = ResourceCulture.GetString("FormWorkArea_btFullScreenAreaText");
      this.buttonWhileBoard.Text = ResourceCulture.GetString("FormWorkArea_fullAreaText");
      this.buttonScreenRatio.Text = ResourceCulture.GetString("FormWorkArea_ScreenRatioText");
      this.buttonCalibration.Text = ResourceCulture.GetString("FormWoekArea_btCalibrateText");
      this.labelSelectScreen.Text = ResourceCulture.GetString("FormWorkArea_lbSelectScreenText");
      this.labelTop.Text = ResourceCulture.GetString("FormWoekArea_lbTopText");
      this.labelBottom.Text = ResourceCulture.GetString("FormWoekArea_lbBottomText");
      this.labelLeft.Text = ResourceCulture.GetString("FormWoekArea_lbLeftText");
      this.labelRight.Text = ResourceCulture.GetString("FormWoekArea_lbRightText");
      this.groupBoxRotate.Text = ResourceCulture.GetString("FormWoekArea_gbRotateText");
      this.groupBoxCustomArea.Text = ResourceCulture.GetString("FormWorkArea_CustomAreaText");
      this.btnIdentify.Text = ResourceCulture.GetString("Identify");
    }

    private void rotateZero_CheckedChanged(object sender, EventArgs e)
    {
      TabletConfigUtils.config.rotateAngle = 0U;
      this.huionWorkAreaPictrueView1.Rotate = (int) TabletConfigUtils.config.rotateAngle;
    }

    private void rotateNinety_CheckedChanged(object sender, EventArgs e)
    {
      TabletConfigUtils.config.rotateAngle = 90U;
      this.huionWorkAreaPictrueView1.Rotate = (int) TabletConfigUtils.config.rotateAngle;
    }

    private void rotateTwoNinety_CheckedChanged(object sender, EventArgs e)
    {
      TabletConfigUtils.config.rotateAngle = 180U;
      this.huionWorkAreaPictrueView1.Rotate = (int) TabletConfigUtils.config.rotateAngle;
    }

    private void rotateThreeNinety_CheckedChanged(object sender, EventArgs e)
    {
      TabletConfigUtils.config.rotateAngle = 270U;
      this.huionWorkAreaPictrueView1.Rotate = (int) TabletConfigUtils.config.rotateAngle;
    }

    private void comboBoxSelectScreen_SelectIndexCommitted(object sender, EventArgs e)
    {
      if (this.comboBoxSelectScreen.SelectedItem == null || (int) TabletConfigUtils.config.curScreenIndex == this.comboBoxSelectScreen.SelectedIndex)
        return;
      TabletConfigUtils.config.curScreenIndex = (uint) this.comboBoxSelectScreen.SelectedIndex;
      this.huionWorkAreaPictrueView1.Screenshot = ScreenHelper.getScreenImage(this.comboBoxSelectScreen.SelectedIndex, this.monitorInfos);
    }

    private void buttonScreenRatio_Click(object sender, EventArgs e)
    {
      HNStruct.RECT screenRect = ScreenHelper.getScreenRect((int) TabletConfigUtils.config.curScreenIndex, this.monitorInfos);
      float num1 = (float) (screenRect.Right - screenRect.Left) / (float) (screenRect.Bottom - screenRect.Top);
      float num2 = (float) HNStruct.globalInfo.tabletInfo.maxX / (float) HNStruct.globalInfo.tabletInfo.maxY;
      HNStruct.HNRectRatio hnRectRatio = HNStruct.HNRectRatio.DEFAULT;
      if ((double) num1 > (double) num2)
      {
        hnRectRatio.t = (float) (1.0 - (double) num2 / (double) num1) / 2f;
        hnRectRatio.b = (float) (1.0 + (double) num2 / (double) num1) / 2f;
      }
      else
      {
        hnRectRatio.l = (float) (1.0 - (double) num1 / (double) num2) / 2f;
        hnRectRatio.r = (float) (1.0 + (double) num1 / (double) num2) / 2f;
      }
      this.huionWorkAreaPictureRect1.DeviceRectRatio = hnRectRatio;
    }

    private void buttonWhileBoard_Click(object sender, EventArgs e)
    {
      TabletConfigUtils.config.workAreaRatio = HNStruct.HNRectRatio.DEFAULT;
      this.huionWorkAreaPictureRect1.DeviceRectRatio = TabletConfigUtils.config.workAreaRatio;
    }

    private void customer_TextChanged(object sender, EventArgs e)
    {
      HuionDelayRun.delayRun(new Runnable(this.delayCallback), 500, sender);
    }

    private void delayCallback(object sender)
    {
      this.Invoke((Delegate) new Runnable(this.onCustomerValueChanged), sender);
    }

    private void onCustomerValueChanged(object sender)
    {
      Control control = (Control) sender;
      if (control == this.textBoxTop)
        this.textBoxTop_TextChanged(sender, (EventArgs) null);
      else if (control == this.textBoxBottom)
        this.textBoxBottom_TextChanged(sender, (EventArgs) null);
      else if (control == this.textBoxLeft)
      {
        this.textBoxLeft_TextChanged(sender, (EventArgs) null);
      }
      else
      {
        if (control != this.textBoxRight)
          return;
        this.textBoxRight_TextChanged(sender, (EventArgs) null);
      }
    }

    private void textBoxTop_TextChanged(object sender, EventArgs e)
    {
      float num = 0.0f;
      this.textBoxTop.Select(this.textBoxTop.Text.Length, 0);
      if (this.textBoxTop.Text.Trim() != string.Empty && this.textBoxTop.Text.Length != 0)
        num = (float) Convert.ToDouble(this.textBoxTop.Text);
      if ((double) TabletConfigUtils.config.workAreaRatio.t == (double) num)
        return;
      if ((double) num > 1.0 || (double) num < 0.0)
      {
        this.textBoxTop.Text = Convert.ToSingle(0).ToString("0.000");
        float single = Convert.ToSingle(this.textBoxTop.Text);
        TabletConfigUtils.config.workAreaRatio.t = single;
      }
      else
        TabletConfigUtils.config.workAreaRatio.t = num;
      if (this.textBoxTop.Tag != null)
        return;
      this.huionWorkAreaPictureRect1.DeviceRectRatio = TabletConfigUtils.config.workAreaRatio;
    }

    private void textBoxBottom_TextChanged(object sender, EventArgs e)
    {
      float num = 0.0f;
      this.textBoxBottom.Select(this.textBoxBottom.Text.Length, 0);
      if (this.textBoxBottom.Text.Trim() != string.Empty && this.textBoxBottom.Text.Length != 0)
        num = (float) Convert.ToDouble(this.textBoxBottom.Text);
      if ((double) TabletConfigUtils.config.workAreaRatio.b == (double) num)
        return;
      if ((double) num > 1.0 || (double) num < 0.0)
      {
        this.textBoxBottom.Text = Convert.ToSingle(1).ToString("0.000");
        float single = Convert.ToSingle(this.textBoxBottom.Text);
        TabletConfigUtils.config.workAreaRatio.b = single;
      }
      else
      {
        if ((double) num == 0.0)
          return;
        TabletConfigUtils.config.workAreaRatio.b = num;
      }
      if (this.textBoxBottom.Tag != null)
        return;
      this.huionWorkAreaPictureRect1.DeviceRectRatio = TabletConfigUtils.config.workAreaRatio;
    }

    private void textBoxLeft_TextChanged(object sender, EventArgs e)
    {
      float num = 0.0f;
      this.textBoxLeft.Select(this.textBoxLeft.Text.Length, 0);
      if (this.textBoxLeft.Text.Trim() != string.Empty && this.textBoxLeft.Text.Length != 0)
        num = (float) Convert.ToDouble(this.textBoxLeft.Text);
      if ((double) TabletConfigUtils.config.workAreaRatio.l == (double) num)
        return;
      if ((double) num > 1.0 || (double) num < 0.0)
      {
        this.textBoxLeft.Text = Convert.ToSingle(0).ToString("0.000");
        float single = Convert.ToSingle(this.textBoxLeft.Text);
        TabletConfigUtils.config.workAreaRatio.l = single;
      }
      else
        TabletConfigUtils.config.workAreaRatio.l = num;
      if (this.textBoxLeft.Tag != null)
        return;
      this.huionWorkAreaPictureRect1.DeviceRectRatio = TabletConfigUtils.config.workAreaRatio;
    }

    private void textBoxRight_TextChanged(object sender, EventArgs e)
    {
      float num = 0.0f;
      this.textBoxRight.Select(this.textBoxRight.Text.Length, 0);
      if (this.textBoxRight.Text.Trim() != string.Empty && this.textBoxRight.Text.Length != 0)
        num = (float) Convert.ToDouble(this.textBoxRight.Text);
      if ((double) TabletConfigUtils.config.workAreaRatio.r == (double) num)
        return;
      if ((double) num > 1.0 || (double) num < 0.0)
      {
        this.textBoxRight.Text = Convert.ToSingle(1).ToString("0.000");
        float single = Convert.ToSingle(this.textBoxRight.Text);
        TabletConfigUtils.config.workAreaRatio.r = single;
      }
      else
      {
        if ((double) num == 0.0)
          return;
        TabletConfigUtils.config.workAreaRatio.r = num;
      }
      if (this.textBoxRight.Tag == null)
        this.huionWorkAreaPictureRect1.DeviceRectRatio = TabletConfigUtils.config.workAreaRatio;
      Console.WriteLine((object) this.huionWorkAreaPictureRect1.DeviceRectRatio);
    }

    private void inputNumber_Key(object sender, KeyPressEventArgs e)
    {
      int keyChar = (int) e.KeyChar;
      if (keyChar >= 48 && keyChar <= 57 || (keyChar == 8 || keyChar == 46) || keyChar == 13)
      {
        if (sender != null && sender is TextBox && keyChar == 46)
        {
          if (((Control) sender).Text.IndexOf(".") >= 0)
            e.Handled = true;
          else
            e.Handled = false;
        }
        else
          e.Handled = false;
      }
      else
        e.Handled = true;
    }

    private void onTextFocusEnter(object sender, EventArgs e)
    {
      if (!(sender is TextBox))
        return;
      ((TextBoxBase) sender).SelectAll();
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 512)
        return;
      TimerSession.userOperation();
    }

    private void btnIdentify_Click(object sender, EventArgs e)
    {
      if (FormIdentify.ShowCount > 0)
        return;
      for (int index = 0; index < this.displayNum; ++index)
      {
        HNStruct.RECT monitor = this.monitorInfos[index].Monitor;
        new FormIdentify(index + 1, new Point(monitor.Left + 20, monitor.Top + 20), (long) TabletConfigUtils.config.curScreenIndex == (long) index).Show();
      }
      this.Focus();
    }

    public void onDestroy()
    {
      for (int index = 0; index < this.comboBoxSelectScreen.Items.Count; index = index - 1 + 1)
        this.comboBoxSelectScreen.Items.RemoveAt(index);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Return:
        case Keys.Down:
          TextBox textBoxLeft1 = this.textBoxLeft;
          TextBox textBoxLeft2 = this.textBoxLeft;
          float single1 = Convert.ToSingle(this.textBoxLeft.Text);
          string str1;
          string str2 = str1 = single1.ToString("0.000");
          textBoxLeft2.Text = str1;
          string str3 = str2;
          textBoxLeft1.Text = str3;
          TextBox textBoxRight1 = this.textBoxRight;
          TextBox textBoxRight2 = this.textBoxRight;
          float single2 = Convert.ToSingle(this.textBoxRight.Text);
          string str4;
          string str5 = str4 = single2.ToString("0.000");
          textBoxRight2.Text = str4;
          string str6 = str5;
          textBoxRight1.Text = str6;
          TextBox textBoxTop1 = this.textBoxTop;
          TextBox textBoxTop2 = this.textBoxTop;
          float single3 = Convert.ToSingle(this.textBoxTop.Text);
          string str7;
          string str8 = str7 = single3.ToString("0.000");
          textBoxTop2.Text = str7;
          string str9 = str8;
          textBoxTop1.Text = str9;
          TextBox textBoxBottom1 = this.textBoxBottom;
          TextBox textBoxBottom2 = this.textBoxBottom;
          float single4 = Convert.ToSingle(this.textBoxBottom.Text);
          string str10;
          string str11 = str10 = single4.ToString("0.000");
          textBoxBottom2.Text = str10;
          string str12 = str11;
          textBoxBottom1.Text = str12;
          SendKeys.SendWait("{Tab}");
          return true;
        case Keys.Up:
          SendKeys.SendWait("+{TAB}");
          return true;
        default:
          return base.ProcessCmdKey(ref msg, keyData);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.labelSelectScreen = new Label();
      this.comboBoxSelectScreen = new ComboBox();
      this.buttonWhileBoard = new Button();
      this.buttonScreenRatio = new Button();
      this.labelTop = new Label();
      this.labelBottom = new Label();
      this.labelLeft = new Label();
      this.labelRight = new Label();
      this.textBoxTop = new TextBox();
      this.textBoxBottom = new TextBox();
      this.textBoxLeft = new TextBox();
      this.textBoxRight = new TextBox();
      this.buttonCalibration = new Button();
      this.groupBoxRotate = new GroupBox();
      this.rotateThreeNinety = new RadioButton();
      this.rotateTwoNinety = new RadioButton();
      this.rotateNinety = new RadioButton();
      this.rotateZero = new RadioButton();
      this.label1 = new Label();
      this.buttonWhileDisplay = new Button();
      this.groupBoxCustomArea = new GroupBox();
      this.btnIdentify = new Button();
      this.huionWorkAreaPictureRect1 = new HuionWorkAreaPictureRect();
      this.huionWorkAreaPictrueView1 = new HuionWorkAreaPictureView();
      this.groupBoxRotate.SuspendLayout();
      this.groupBoxCustomArea.SuspendLayout();
      ((ISupportInitialize)this.huionWorkAreaPictureRect1).BeginInit();
      ((ISupportInitialize) this.huionWorkAreaPictrueView1).BeginInit();
      this.SuspendLayout();
      this.labelSelectScreen.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelSelectScreen.Location = new Point(3, 7);
      this.labelSelectScreen.Name = "labelSelectScreen";
      this.labelSelectScreen.Size = new Size(179, 20);
      this.labelSelectScreen.TabIndex = 0;
      this.labelSelectScreen.Text = "Select Current Display";
      this.labelSelectScreen.TextAlign = ContentAlignment.MiddleRight;
      this.comboBoxSelectScreen.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxSelectScreen.FormattingEnabled = true;
      this.comboBoxSelectScreen.Location = new Point(183, 5);
      this.comboBoxSelectScreen.Margin = new Padding(3, 4, 3, 4);
      this.comboBoxSelectScreen.Name = "comboBoxSelectScreen";
      this.comboBoxSelectScreen.Size = new Size(385, 25);
      this.comboBoxSelectScreen.TabIndex = 1;
      this.comboBoxSelectScreen.SelectionChangeCommitted += new EventHandler(this.comboBoxSelectScreen_SelectIndexCommitted);
      this.buttonWhileBoard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.buttonWhileBoard.AutoEllipsis = true;
      this.buttonWhileBoard.BackColor = Color.Transparent;
      this.buttonWhileBoard.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonWhileBoard.FlatStyle = FlatStyle.Flat;
      this.buttonWhileBoard.Location = new Point(4, 405);
      this.buttonWhileBoard.Margin = new Padding(3, 4, 3, 4);
      this.buttonWhileBoard.Name = "buttonWhileBoard";
      this.buttonWhileBoard.Size = new Size(181, 25);
      this.buttonWhileBoard.TabIndex = 3;
      this.buttonWhileBoard.Text = "全局区";
      this.buttonWhileBoard.UseVisualStyleBackColor = false;
      this.buttonWhileBoard.Click += new EventHandler(this.buttonWhileBoard_Click);
      this.buttonScreenRatio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.buttonScreenRatio.AutoEllipsis = true;
      this.buttonScreenRatio.BackColor = Color.Transparent;
      this.buttonScreenRatio.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonScreenRatio.FlatStyle = FlatStyle.Flat;
      this.buttonScreenRatio.Location = new Point(4, 438);
      this.buttonScreenRatio.Margin = new Padding(3, 4, 3, 4);
      this.buttonScreenRatio.Name = "buttonScreenRatio";
      this.buttonScreenRatio.Size = new Size(181, 25);
      this.buttonScreenRatio.TabIndex = 4;
      this.buttonScreenRatio.Text = "与屏幕等比例";
      this.buttonScreenRatio.UseVisualStyleBackColor = false;
      this.buttonScreenRatio.Click += new EventHandler(this.buttonScreenRatio_Click);
      this.labelTop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelTop.AutoEllipsis = true;
      this.labelTop.FlatStyle = FlatStyle.Flat;
      this.labelTop.Location = new Point(2, 55);
      this.labelTop.Name = "labelTop";
      this.labelTop.Size = new Size(55, 23);
      this.labelTop.TabIndex = 7;
      this.labelTop.Text = "上";
      this.labelTop.TextAlign = ContentAlignment.MiddleRight;
      this.labelBottom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelBottom.AutoEllipsis = true;
      this.labelBottom.FlatStyle = FlatStyle.Flat;
      this.labelBottom.Location = new Point(103, 55);
      this.labelBottom.Name = "labelBottom";
      this.labelBottom.Size = new Size(55, 23);
      this.labelBottom.TabIndex = 8;
      this.labelBottom.Text = "下";
      this.labelBottom.TextAlign = ContentAlignment.MiddleRight;
      this.labelLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelLeft.AutoEllipsis = true;
      this.labelLeft.FlatStyle = FlatStyle.Flat;
      this.labelLeft.Location = new Point(2, 26);
      this.labelLeft.Name = "labelLeft";
      this.labelLeft.Size = new Size(55, 23);
      this.labelLeft.TabIndex = 9;
      this.labelLeft.Text = "左";
      this.labelLeft.TextAlign = ContentAlignment.MiddleRight;
      this.labelRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelRight.AutoEllipsis = true;
      this.labelRight.FlatStyle = FlatStyle.Flat;
      this.labelRight.Location = new Point(103, 26);
      this.labelRight.Name = "labelRight";
      this.labelRight.Size = new Size(55, 23);
      this.labelRight.TabIndex = 10;
      this.labelRight.Text = "右";
      this.labelRight.TextAlign = ContentAlignment.MiddleRight;
      this.textBoxTop.Location = new Point(59, 55);
      this.textBoxTop.Margin = new Padding(3, 4, 3, 4);
      this.textBoxTop.MaxLength = 7;
      this.textBoxTop.Name = "textBoxTop";
      this.textBoxTop.Size = new Size(42, 23);
      this.textBoxTop.TabIndex = 11;
      this.textBoxTop.TextChanged += new EventHandler(this.customer_TextChanged);
      this.textBoxTop.KeyPress += new KeyPressEventHandler(this.inputNumber_Key);
      this.textBoxBottom.Location = new Point(161, 55);
      this.textBoxBottom.Margin = new Padding(3, 4, 3, 4);
      this.textBoxBottom.Name = "textBoxBottom";
      this.textBoxBottom.Size = new Size(42, 23);
      this.textBoxBottom.TabIndex = 12;
      this.textBoxBottom.TextChanged += new EventHandler(this.customer_TextChanged);
      this.textBoxBottom.KeyPress += new KeyPressEventHandler(this.inputNumber_Key);
      this.textBoxLeft.CharacterCasing = CharacterCasing.Upper;
      this.textBoxLeft.Location = new Point(59, 26);
      this.textBoxLeft.Margin = new Padding(3, 4, 3, 4);
      this.textBoxLeft.Name = "textBoxLeft";
      this.textBoxLeft.Size = new Size(42, 23);
      this.textBoxLeft.TabIndex = 9;
      this.textBoxLeft.TextChanged += new EventHandler(this.customer_TextChanged);
      this.textBoxLeft.KeyPress += new KeyPressEventHandler(this.inputNumber_Key);
      this.textBoxRight.Location = new Point(161, 26);
      this.textBoxRight.Margin = new Padding(3, 4, 3, 4);
      this.textBoxRight.Name = "textBoxRight";
      this.textBoxRight.Size = new Size(42, 23);
      this.textBoxRight.TabIndex = 10;
      this.textBoxRight.TextChanged += new EventHandler(this.customer_TextChanged);
      this.textBoxRight.KeyPress += new KeyPressEventHandler(this.inputNumber_Key);
      this.buttonCalibration.Anchor = AnchorStyles.Bottom;
      this.buttonCalibration.AutoEllipsis = true;
      this.buttonCalibration.BackColor = Color.Transparent;
      this.buttonCalibration.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonCalibration.FlatStyle = FlatStyle.Flat;
      this.buttonCalibration.Location = new Point(352, 535);
      this.buttonCalibration.Margin = new Padding(3, 4, 3, 4);
      this.buttonCalibration.Name = "buttonCalibration";
      this.buttonCalibration.Size = new Size(160, 25);
      this.buttonCalibration.TabIndex = 13;
      this.buttonCalibration.Text = "数位屏校准";
      this.buttonCalibration.UseVisualStyleBackColor = false;
      this.buttonCalibration.Visible = false;
      this.groupBoxRotate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.groupBoxRotate.BackColor = SystemColors.Control;
      this.groupBoxRotate.Controls.Add((Control) this.rotateThreeNinety);
      this.groupBoxRotate.Controls.Add((Control) this.rotateTwoNinety);
      this.groupBoxRotate.Controls.Add((Control) this.rotateNinety);
      this.groupBoxRotate.Controls.Add((Control) this.rotateZero);
      this.groupBoxRotate.Location = new Point(691, 425);
      this.groupBoxRotate.Margin = new Padding(3, 4, 3, 4);
      this.groupBoxRotate.Name = "groupBoxRotate";
      this.groupBoxRotate.Padding = new Padding(3, 4, 3, 4);
      this.groupBoxRotate.Size = new Size(143, 134);
      this.groupBoxRotate.TabIndex = 17;
      this.groupBoxRotate.TabStop = false;
      this.groupBoxRotate.Text = "旋转";
      this.rotateThreeNinety.AutoSize = true;
      this.rotateThreeNinety.Location = new Point(17, 104);
      this.rotateThreeNinety.Margin = new Padding(3, 4, 3, 4);
      this.rotateThreeNinety.Name = "rotateThreeNinety";
      this.rotateThreeNinety.Size = new Size(52, 21);
      this.rotateThreeNinety.TabIndex = 8;
      this.rotateThreeNinety.TabStop = true;
      this.rotateThreeNinety.Text = "270°";
      this.rotateThreeNinety.UseVisualStyleBackColor = true;
      this.rotateThreeNinety.CheckedChanged += new EventHandler(this.rotateThreeNinety_CheckedChanged);
      this.rotateTwoNinety.AutoSize = true;
      this.rotateTwoNinety.Location = new Point(17, 77);
      this.rotateTwoNinety.Margin = new Padding(3, 4, 3, 4);
      this.rotateTwoNinety.Name = "rotateTwoNinety";
      this.rotateTwoNinety.Size = new Size(52, 21);
      this.rotateTwoNinety.TabIndex = 7;
      this.rotateTwoNinety.TabStop = true;
      this.rotateTwoNinety.Text = "180°";
      this.rotateTwoNinety.UseVisualStyleBackColor = true;
      this.rotateTwoNinety.CheckedChanged += new EventHandler(this.rotateTwoNinety_CheckedChanged);
      this.rotateNinety.AutoSize = true;
      this.rotateNinety.Location = new Point(17, 50);
      this.rotateNinety.Margin = new Padding(3, 4, 3, 4);
      this.rotateNinety.Name = "rotateNinety";
      this.rotateNinety.Size = new Size(45, 21);
      this.rotateNinety.TabIndex = 6;
      this.rotateNinety.TabStop = true;
      this.rotateNinety.Text = "90°";
      this.rotateNinety.UseVisualStyleBackColor = true;
      this.rotateNinety.CheckedChanged += new EventHandler(this.rotateNinety_CheckedChanged);
      this.rotateZero.AutoSize = true;
      this.rotateZero.Location = new Point(17, 23);
      this.rotateZero.Margin = new Padding(3, 4, 3, 4);
      this.rotateZero.Name = "rotateZero";
      this.rotateZero.Size = new Size(38, 21);
      this.rotateZero.TabIndex = 5;
      this.rotateZero.Text = "0°";
      this.rotateZero.UseVisualStyleBackColor = true;
      this.rotateZero.CheckedChanged += new EventHandler(this.rotateZero_CheckedChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(629, 455);
      this.label1.Name = "label1";
      this.label1.Size = new Size(0, 17);
      this.label1.TabIndex = 18;
      this.buttonWhileDisplay.AutoEllipsis = true;
      this.buttonWhileDisplay.BackColor = Color.Transparent;
      this.buttonWhileDisplay.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonWhileDisplay.FlatStyle = FlatStyle.Flat;
      this.buttonWhileDisplay.Location = new Point(571, 5);
      this.buttonWhileDisplay.Margin = new Padding(3, 4, 3, 4);
      this.buttonWhileDisplay.Name = "buttonWhileDisplay";
      this.buttonWhileDisplay.Size = new Size(155, 25);
      this.buttonWhileDisplay.TabIndex = 2;
      this.buttonWhileDisplay.Text = "全屏幕区域";
      this.buttonWhileDisplay.UseVisualStyleBackColor = false;
      this.buttonWhileDisplay.Click += new EventHandler(this.buttonWhileDisplay_Click);
      this.groupBoxCustomArea.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.groupBoxCustomArea.BackColor = SystemColors.Control;
      this.groupBoxCustomArea.Controls.Add((Control) this.labelLeft);
      this.groupBoxCustomArea.Controls.Add((Control) this.textBoxLeft);
      this.groupBoxCustomArea.Controls.Add((Control) this.textBoxTop);
      this.groupBoxCustomArea.Controls.Add((Control) this.labelTop);
      this.groupBoxCustomArea.Controls.Add((Control) this.labelRight);
      this.groupBoxCustomArea.Controls.Add((Control) this.textBoxRight);
      this.groupBoxCustomArea.Controls.Add((Control) this.textBoxBottom);
      this.groupBoxCustomArea.Controls.Add((Control) this.labelBottom);
      this.groupBoxCustomArea.Location = new Point(4, 470);
      this.groupBoxCustomArea.Margin = new Padding(3, 4, 3, 4);
      this.groupBoxCustomArea.Name = "groupBoxCustomArea";
      this.groupBoxCustomArea.Padding = new Padding(3, 4, 3, 4);
      this.groupBoxCustomArea.Size = new Size(207, 89);
      this.groupBoxCustomArea.TabIndex = 20;
      this.groupBoxCustomArea.TabStop = false;
      this.groupBoxCustomArea.Text = "自定义区域";
      this.btnIdentify.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnIdentify.AutoEllipsis = true;
      this.btnIdentify.BackColor = Color.Transparent;
      this.btnIdentify.FlatAppearance.BorderColor = Color.DarkGray;
      this.btnIdentify.FlatStyle = FlatStyle.Flat;
      this.btnIdentify.Location = new Point(729, 5);
      this.btnIdentify.Margin = new Padding(3, 4, 3, 4);
      this.btnIdentify.Name = "btnIdentify";
      this.btnIdentify.Size = new Size(106, 25);
      this.btnIdentify.TabIndex = 23;
      this.btnIdentify.Text = "button1";
      this.btnIdentify.UseVisualStyleBackColor = false;
      this.btnIdentify.Click += new EventHandler(this.btnIdentify_Click);
      this.huionWorkAreaPictureRect1.BackColor = Color.Transparent;
      this.huionWorkAreaPictureRect1.DeviceRect = new Rectangle(0, 0, 0, 0);
      this.huionWorkAreaPictureRect1.DeviceRectRotate = 0;
      this.huionWorkAreaPictureRect1.Location = new Point(146, (int) sbyte.MaxValue);
      this.huionWorkAreaPictureRect1.Margin = new Padding(3, 4, 3, 4);
      this.huionWorkAreaPictureRect1.Name = "huionWorkAreaPictureRect1";
      this.huionWorkAreaPictureRect1.ScreenRect = new Rectangle(0, 0, 0, 0);
      this.huionWorkAreaPictureRect1.Size = new Size(87, 57);
      this.huionWorkAreaPictureRect1.TabIndex = 22;
      this.huionWorkAreaPictureRect1.TabStop = false;
      this.huionWorkAreaPictrueView1.DeviceImage = (Bitmap) null;
      this.huionWorkAreaPictrueView1.DeviceImageMaxHeight = 190;
      this.huionWorkAreaPictrueView1.DeviceImageMaxWidth = 400;
      this.huionWorkAreaPictrueView1.IntervalSpace = 5;
      this.huionWorkAreaPictrueView1.Location = new Point(117, 36);
      this.huionWorkAreaPictrueView1.Margin = new Padding(5, 6, 5, 6);
      this.huionWorkAreaPictrueView1.Name = "huionWorkAreaPictrueView1";
      this.huionWorkAreaPictrueView1.Rotate = 0;
      this.huionWorkAreaPictrueView1.Screenshot = (Bitmap) null;
      this.huionWorkAreaPictrueView1.ScreenshotMaxHeight = 300;
      this.huionWorkAreaPictrueView1.ScreenshotMaxWidth = 640;
      this.huionWorkAreaPictrueView1.Size = new Size(38, 25);
      this.huionWorkAreaPictrueView1.TabIndex = 21;
      this.huionWorkAreaPictrueView1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(840, 560);
      this.Controls.Add((Control) this.btnIdentify);
      this.Controls.Add((Control) this.huionWorkAreaPictureRect1);
      this.Controls.Add((Control) this.groupBoxCustomArea);
      this.Controls.Add((Control) this.buttonWhileDisplay);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.groupBoxRotate);
      this.Controls.Add((Control) this.buttonCalibration);
      this.Controls.Add((Control) this.buttonScreenRatio);
      this.Controls.Add((Control) this.buttonWhileBoard);
      this.Controls.Add((Control) this.comboBoxSelectScreen);
      this.Controls.Add((Control) this.labelSelectScreen);
      this.Controls.Add((Control) this.huionWorkAreaPictrueView1);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.KeyPreview = true;
      this.Margin = new Padding(3, 4, 3, 4);
      this.Name = nameof (FormWorkArea);
      this.Text = nameof (FormWorkArea);
      this.Load += new EventHandler(this.FormWorkArea_Load);
      this.groupBoxRotate.ResumeLayout(false);
      this.groupBoxRotate.PerformLayout();
      this.groupBoxCustomArea.ResumeLayout(false);
      this.groupBoxCustomArea.PerformLayout();
      ((ISupportInitialize) this.huionWorkAreaPictureRect1).EndInit();
      ((ISupportInitialize) this.huionWorkAreaPictrueView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
