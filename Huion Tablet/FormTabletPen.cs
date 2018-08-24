// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormTabletPen
// Assembly: Huion Tablet, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: E9BBED94-79CD-4774-8A97-2E0171DB986F
// Assembly location: D:\Program Files (x86)\Huion Tablet\app.publish\Huion Tablet.exe

using Huion;
using HuionTablet.Entity;
using HuionTablet.Lib;
using HuionTablet.utils;
using HuionTablet.view;
using HuionTablet.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormTabletPen : Form, IDestroy
  {
    public bool bTest = false;
    private IContainer components = (IContainer) null;
    private Fixer4DrawLine drawLineHelper;
    private HuionKeyLayout[] penLayouts;
    private PictureBox imgPen;
    private Button btnAbove;
    private Button btnBelow;
    private PictureBox pictureBoxPressCurve;
    private Label labelSetPress;
    private TrackBar trackBar1;
    private Button buttonTest;
    private Button buttonClear;
    private PictureBox pictureBoxDrawTest;
    private CheckBox checkInk;
    private CheckBox checkTab;
    private TouchPressBar touchPressBar1;
    private HuionPictureBox pictureBoxDrawLine;
    private Label labelMaxPressValue;
    private Label labelMinPressValue;
    private Label label1;
    private Label label2;
    private CheckBox checkBoxMouseMode;
    private CheckBox checkBgame;

    public FormTabletPen()
    {
      this.InitializeComponent();
      this.pictureBoxDrawTest.Paint += new PaintEventHandler(this.PictureBoxDrawTest_Paint);
      this.pictureBoxPressCurve.Paint += new PaintEventHandler(this.pictureBoxPressCurve_Paint);
      this.pictureBoxPressCurve.Size = new Size(DpiHelper.getInstance().DpiMatrix(222), DpiHelper.getInstance().DpiMatrix(222));
      this.checkInk.CheckedChanged += new EventHandler(Fixer4TabletPen.inkCheckedChanged);
      this.checkTab.CheckedChanged += new EventHandler(Fixer4TabletPen.wintabCheckedChanged);
      this.checkBoxMouseMode.CheckedChanged += new EventHandler(Fixer4TabletPen.onMouseModeChanged);
      this.checkBgame.CheckedChanged += new EventHandler(Fixer4TabletPen.bGameCheckedChanged);
    }

    private void pictureBoxPressCurve_Paint(object sender, PaintEventArgs e)
    {
      int num1 = 0;
      int num2 = this.pictureBoxPressCurve.Height;
      Graphics graphics = e.Graphics;
      Pen pen1 = new Pen(Color.DarkGray, 2f);
      Pen pen2 = new Pen(Color.DarkGray, 1f);
      graphics.DrawRectangle(pen1, 0, 0, this.pictureBoxPressCurve.Width, this.pictureBoxPressCurve.Height);
      uint width = (uint) this.pictureBoxPressCurve.Width;
      for (int index = 0; index < this.pictureBoxPressCurve.Width; ++index)
      {
        graphics.DrawLine(pen2, (float) num1, (float) num2, (float) index, (float) ((long) this.pictureBoxPressCurve.Height - (long) Fixer4TabletPen.calibratePressVal(HNStruct.globalInfo.userConfig, (uint) index, width)));
        num1 = index;
        num2 = (int) ((long) this.pictureBoxPressCurve.Height - (long) Fixer4TabletPen.calibratePressVal(HNStruct.globalInfo.userConfig, (uint) index, width));
      }
      pen1.Dispose();
      pen2.Dispose();
    }

    private void PictureBoxDrawTest_Paint(object sender, PaintEventArgs e)
    {
      Pen pen = new Pen(Color.DarkGray, 2f);
      e.Graphics.DrawRectangle(pen, 0, 0, this.pictureBoxDrawTest.Width, this.pictureBoxDrawTest.Height);
      pen.Dispose();
    }

    private void PictureBoxDrawLine_Paint(object sender, PaintEventArgs e)
    {
      if (this.penLayouts == null)
        return;
      Pen pen = new Pen(Color.DarkGray, 2f);
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.DrawLine(pen, Utils.getViewCenter((Control) this.btnAbove, 17), Utils.getRectCenter(this.penLayouts[2].Rect));
      e.Graphics.DrawLine(pen, Utils.getViewCenter((Control) this.btnBelow, 17), Utils.getRectCenter(this.penLayouts[1].Rect));
      pen.Dispose();
    }

    private void FormTabletPen_Load(object sender, EventArgs e)
    {
      this.pictureBoxDrawLine.Size = new Size(this.Width, this.Height);
      this.pictureBoxDrawTest.Image = (Image) new Bitmap(this.pictureBoxDrawTest.Width, this.pictureBoxDrawTest.Height);
      this.pictureBoxDrawLine.Paint += new PaintEventHandler(this.PictureBoxDrawLine_Paint);
      this.btnAbove.Parent = (Control) this.pictureBoxDrawLine;
      this.btnBelow.Parent = (Control) this.pictureBoxDrawLine;
      this.btnAbove.UseMnemonic = false;
      this.btnBelow.UseMnemonic = false;
      this.pictureBoxPressCurve.Parent = (Control) this.pictureBoxDrawLine;
      this.buttonClear.Parent = (Control) this.pictureBoxDrawLine;
      this.buttonTest.Parent = (Control) this.pictureBoxDrawLine;
      this.touchPressBar1.Parent = (Control) this.pictureBoxDrawLine;
      this.labelSetPress.Parent = (Control) this.pictureBoxDrawLine;
      this.checkInk.Parent = (Control) this.pictureBoxDrawLine;
      this.checkTab.Parent = (Control) this.pictureBoxDrawLine;
      this.pictureBoxDrawTest.Parent = (Control) this.pictureBoxDrawLine;
      this.checkTab.Visible = false;
      this.drawLineHelper = new Fixer4DrawLine((Control) this.pictureBoxDrawTest, this.pictureBoxDrawTest.Image);
      this.drawLineHelper.onPressValueChangedListener += new PressValueChanged(this.onPressValueChanged);
      this.touchPressBar1.MaxValue = (int) HNStruct.globalInfo.tabletInfo.maxP;
      this.setViewText8Locale();
      this.imgPen.Image = Fixer4TabletPen.getPenImage();
      int num = (this.Height - this.buttonClear.Height * 2 - this.imgPen.Image.Size.Height) / 2;
      this.imgPen.Location = new Point(this.imgPen.Location.X, num < 0 ? 0 : num);
      this.pictureBoxDrawLine.setPictureInfo(this.imgPen);
      this.imgPen.Visible = false;
      int length = HNStruct.globalInfo.penLayouts.Length;
      this.penLayouts = new HuionKeyLayout[length];
      for (int index = 0; index < length; ++index)
      {
        this.penLayouts[index] = Utils.swapLayout(this.imgPen.Image.Size, HNStruct.globalInfo.layoutPen.size, HNStruct.globalInfo.penLayouts[index]);
        this.penLayouts[index].Rect.X += this.imgPen.Left;
        this.penLayouts[index].Rect.Y += this.imgPen.Top;
        this.penLayouts[index].KeyType = HuionKeyType.PENKEY;
      }
      if (HNStruct.OemType != OEMType.HUION || Convert.ToBoolean(HNStruct.globalInfo.tabletInfo.bMonitor))
        this.checkBoxMouseMode.Hide();
      this.trackBar1.Top = this.pictureBoxPressCurve.Top - 5;
      this.trackBar1.Height = this.pictureBoxPressCurve.Height + 10;
      this.labelMaxPressValue.Text = string.Concat((object) this.trackBar1.Maximum);
      this.labelMinPressValue.Text = string.Concat((object) this.trackBar1.Minimum);
      this.btnAbove.Text = HNStruct.globalInfo.pbtns[2].ToString();
      this.btnBelow.Text = HNStruct.globalInfo.pbtns[1].ToString();
      this.trackBar1.Value = HNStruct.globalInfo.userConfig.pressFactor;
      this.checkInk.Checked = Convert.ToBoolean(HNStruct.globalInfo.userConfig.bTabletpc);
      this.checkTab.Checked = Convert.ToBoolean(HNStruct.globalInfo.userConfig.bImproveLinearity);
      this.checkBoxMouseMode.Checked = HNStruct.globalInfo.userConfig.bAbsoluteMode == (byte) 0;
      this.checkBgame.Checked = HNStruct.globalInfo.userConfig.bGame == (byte) 1;
      this.labelMaxPressValue.Location = new Point(this.trackBar1.Right - this.labelMaxPressValue.Width, this.trackBar1.Top);
      this.label1.Location = new Point(this.labelMaxPressValue.Left, this.trackBar1.Top + (this.trackBar1.Height - this.label1.Height) / 2);
      this.labelMinPressValue.Location = new Point(this.labelMaxPressValue.Left, this.trackBar1.Bottom - this.labelMinPressValue.Height);
      this.pictureBoxDrawTest.Enabled = true;
      this.pictureBoxDrawTest.MouseEnter += new EventHandler(this.PictureBoxDrawTest_MouseEnter);
      this.pictureBoxDrawTest.MouseLeave += new EventHandler(this.PictureBoxDrawTest_MouseLeave);
      if (DeployConfig.isNewUI)
      {
        this.buttonTest_Click((object) null, (EventArgs) null);
      }
      else
      {
        this.label2.Visible = false;
        this.buttonTest.Visible = true;
        this.buttonTest.Enabled = true;
        this.buttonTest.TabStop = true;
        this.pictureBoxDrawTest.Top += 5;
        this.touchPressBar1.Top += 5;
        this.buttonClear.Left = this.buttonTest.Right + 10;
        this.buttonClear.Top = this.buttonTest.Top;
      }
    }

    private void PictureBoxDrawTest_MouseLeave(object sender, EventArgs e)
    {
      this.Cursor = Cursors.Default;
    }

    private void PictureBoxDrawTest_MouseEnter(object sender, EventArgs e)
    {
      this.Cursor = new Cursor(ViewUtils.CreateCursor());
    }

    private void setViewText8Locale()
    {
      this.labelSetPress.Text = ResourceCulture.GetString("FormTabletPen_lbSetPressSensiveText");
      this.buttonTest.Text = ResourceCulture.GetString("FormTabletPen_btStartPressTestText");
      this.buttonClear.Text = ResourceCulture.GetString("FormTabletPen_btClearPressTestText");
      this.checkInk.Text = ResourceCulture.GetString("FormTabletPen_btnWindowsInk");
      this.checkTab.Text = ResourceCulture.GetString("FormTabletPen_btnWindowsTab");
      this.label2.Text = ResourceCulture.GetString("PressureTest");
      this.checkBoxMouseMode.Text = ResourceCulture.GetString("TextMouseMode");
      this.checkBgame.Text = ResourceCulture.GetString("BGametext");
    }

    private void buttonTest_Click(object sender, EventArgs e)
    {
      this.bTest = !this.bTest;
      this.buttonTest.Text = this.bTest ? ResourceCulture.GetString("FormTabletPen_btCanncelPressTestText") : ResourceCulture.GetString("FormTabletPen_btStartPressTestText");
      this.drawLineHelper.IsDrawLine = this.bTest;
      if (this.bTest)
        this.drawLineHelper.startDrawLine((Form) this);
      else
        this.drawLineHelper.stopDrawLine();
      this.touchPressBar1.Value = 0;
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      HNStruct.globalInfo.penData.ps = 0U;
      this.onPressValueHandle((int) HNStruct.globalInfo.penData.ps);
      this.drawLineHelper.clearDrawLine();
      this.Invalidate();
    }

    private void btnPenKey_Click(object sender, EventArgs e)
    {
      if (DeployConfig.isNewUI)
      {
        if (sender == this.btnAbove)
        {
          FormEKey_New formEkeyNew = new FormEKey_New(sender, HNStruct.globalInfo.pbtns[2]);
          formEkeyNew.callback += new EKeyCallback(this.form_TransfEvent);
          int num = (int) formEkeyNew.ShowDialog();
        }
        else
        {
          if (sender != this.btnBelow)
            return;
          FormEKey_New formEkeyNew = new FormEKey_New(sender, HNStruct.globalInfo.pbtns[1]);
          formEkeyNew.callback += new EKeyCallback(this.form_TransfEvent);
          int num = (int) formEkeyNew.ShowDialog();
        }
      }
      else if (sender == this.btnAbove)
      {
        FormEKey formEkey = new FormEKey(sender, HNStruct.globalInfo.pbtns[2]);
        formEkey.callback += new EKeyCallback(this.form_TransfEvent);
        int num = (int) formEkey.ShowDialog();
      }
      else if (sender == this.btnBelow)
      {
        FormEKey formEkey = new FormEKey(sender, HNStruct.globalInfo.pbtns[1]);
        formEkey.callback += new EKeyCallback(this.form_TransfEvent);
        int num = (int) formEkey.ShowDialog();
      }
    }

    private void form_TransfEvent(object holder, HNStruct.HNEkey value)
    {
      int index = 1;
      if (holder == this.btnAbove)
        index = 2;
      Fixer4TabletPen.savePenButtonValue(index, value);
      if (index == 2)
      {
        this.btnAbove.Text = value.ToString();
      }
      else
      {
        if (index != 1)
          return;
        this.btnBelow.Text = value.ToString();
      }
    }

    private void onPressValueChanged(int value)
    {
      this.Invoke((Delegate) new PressValueChanged(this.onPressValueHandle), (object) value);
    }

    private void onPressValueHandle(int value)
    {
      Point client = this.pictureBoxDrawLine.PointToClient(Control.MousePosition);
      if (client.X > this.ClientSize.Width || client.X < 0 || client.Y > this.ClientSize.Height || client.Y < 0)
        value = 0;
      if (value != this.touchPressBar1.Value)
        this.touchPressBar1.Value = value;
      if (value <= 0)
        return;
      if (Utils.mouseInView((Control) this.pictureBoxPressCurve, client))
        this.pictureBoxPressCurve.Invalidate();
      else if (Utils.mouseInView((Control) this.pictureBoxDrawTest, client))
        this.pictureBoxDrawTest.Invalidate();
      else if (Utils.mouseInView((Control) this.btnAbove, client))
        this.btnAbove.Invalidate();
      else if (Utils.mouseInView((Control) this.btnBelow, client))
        this.btnBelow.Invalidate();
      else if (Utils.mouseInView((Control) this.buttonClear, client))
        this.buttonClear.Invalidate();
      else if (Utils.mouseInView((Control) this.buttonTest, client))
        this.buttonTest.Invalidate();
      else if (Utils.mouseInView((Control) this.checkInk, client))
        this.checkInk.Invalidate();
      else if (Utils.mouseInView((Control) this.checkTab, client))
        this.checkTab.Invalidate();
      else if (Utils.mouseInView((Control) this.touchPressBar1, client))
        this.touchPressBar1.Invalidate();
      else if (Utils.mouseInView((Control) this.labelSetPress, client))
        this.labelSetPress.Invalidate();
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      HNStruct.globalInfo.userConfig.pressFactor = this.trackBar1.Value;
      this.pictureBoxPressCurve.Refresh();
      if (!DeployConfig.isNewUI)
        return;
      Fixer4Main.applayClick((object) null, (EventArgs) null);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg == 512)
        TimerSession.userOperation();
      if (m.Msg == 32752)
      {
        HNStruct.globalInfo.penData.ps = HuionApi.Msg2Packet(m).pkNormalPressure;
        this.onPressValueHandle((int) HNStruct.globalInfo.penData.ps);
        this.drawLineHelper.onDrawline();
      }
      else
      {
        if (m.Msg != 1064)
          return;
        HNStruct.globalInfo.penData = TouchInfoReader.Msg2Packet(m);
        this.onPressValueHandle((int) HNStruct.globalInfo.penData.ps);
        this.drawLineHelper.onDrawline();
      }
    }

    public void onDestroy()
    {
      this.drawLineHelper.stopDrawLine();
      this.drawLineHelper.releaseGraphics();
    }

    private void FormTabletPen_Deactivate(object sender, EventArgs e)
    {
      this.touchPressBar1.Value = 0;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.imgPen = new PictureBox();
      this.btnAbove = new Button();
      this.btnBelow = new Button();
      this.pictureBoxPressCurve = new PictureBox();
      this.labelSetPress = new Label();
      this.trackBar1 = new TrackBar();
      this.buttonTest = new Button();
      this.buttonClear = new Button();
      this.pictureBoxDrawTest = new PictureBox();
      this.checkInk = new CheckBox();
      this.checkTab = new CheckBox();
      this.labelMaxPressValue = new Label();
      this.labelMinPressValue = new Label();
      this.label1 = new Label();
      this.label2 = new Label();
      this.checkBoxMouseMode = new CheckBox();
      this.checkBgame = new CheckBox();
      this.touchPressBar1 = new TouchPressBar();
      this.pictureBoxDrawLine = new HuionPictureBox();
      ((ISupportInitialize) this.imgPen).BeginInit();
      ((ISupportInitialize) this.pictureBoxPressCurve).BeginInit();
      this.trackBar1.BeginInit();
      ((ISupportInitialize) this.pictureBoxDrawTest).BeginInit();
      ((ISupportInitialize) this.pictureBoxDrawLine).BeginInit();
      this.SuspendLayout();
      this.imgPen.BackColor = Color.Transparent;
      this.imgPen.BackgroundImageLayout = ImageLayout.None;
      this.imgPen.Enabled = false;
      this.imgPen.Location = new Point(60, 5);
      this.imgPen.Margin = new Padding(3, 4, 3, 4);
      this.imgPen.Name = "imgPen";
      this.imgPen.Size = new Size(38, 500);
      this.imgPen.SizeMode = PictureBoxSizeMode.AutoSize;
      this.imgPen.TabIndex = 0;
      this.imgPen.TabStop = false;
      this.btnAbove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnAbove.AutoEllipsis = true;
      this.btnAbove.BackColor = Color.Transparent;
      this.btnAbove.FlatAppearance.BorderColor = Color.DarkGray;
      this.btnAbove.FlatStyle = FlatStyle.Flat;
      this.btnAbove.Location = new Point(193, 342);
      this.btnAbove.Margin = new Padding(3, 4, 3, 4);
      this.btnAbove.Name = "btnAbove";
      this.btnAbove.Size = new Size(171, 25);
      this.btnAbove.TabIndex = 5;
      this.btnAbove.Text = "button1";
      this.btnAbove.UseVisualStyleBackColor = false;
      this.btnAbove.Click += new EventHandler(this.btnPenKey_Click);
      this.btnBelow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnBelow.AutoEllipsis = true;
      this.btnBelow.BackColor = Color.Transparent;
      this.btnBelow.FlatAppearance.BorderColor = Color.DarkGray;
      this.btnBelow.FlatStyle = FlatStyle.Flat;
      this.btnBelow.Location = new Point(193, 385);
      this.btnBelow.Margin = new Padding(3, 4, 3, 4);
      this.btnBelow.Name = "btnBelow";
      this.btnBelow.Size = new Size(171, 25);
      this.btnBelow.TabIndex = 6;
      this.btnBelow.Text = "button2";
      this.btnBelow.UseVisualStyleBackColor = false;
      this.btnBelow.Click += new EventHandler(this.btnPenKey_Click);
      this.pictureBoxPressCurve.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.pictureBoxPressCurve.BackColor = Color.Transparent;
      this.pictureBoxPressCurve.Enabled = false;
      this.pictureBoxPressCurve.Location = new Point(537, 27);
      this.pictureBoxPressCurve.Margin = new Padding(3, 4, 3, 4);
      this.pictureBoxPressCurve.Name = "pictureBoxPressCurve";
      this.pictureBoxPressCurve.Size = new Size(222, 222);
      this.pictureBoxPressCurve.TabIndex = 3;
      this.pictureBoxPressCurve.TabStop = false;
      this.labelSetPress.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.labelSetPress.AutoSize = true;
      this.labelSetPress.BackColor = Color.Transparent;
      this.labelSetPress.Location = new Point(538, 8);
      this.labelSetPress.Name = "labelSetPress";
      this.labelSetPress.Size = new Size(43, 17);
      this.labelSetPress.TabIndex = 4;
      this.labelSetPress.Text = "label1";
      this.trackBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.trackBar1.LargeChange = 1;
      this.trackBar1.Location = new Point(771, 17);
      this.trackBar1.Margin = new Padding(3, 4, 3, 4);
      this.trackBar1.Maximum = 4;
      this.trackBar1.Minimum = -4;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Orientation = Orientation.Vertical;
      this.trackBar1.Size = new Size(45, 235);
      this.trackBar1.TabIndex = 7;
      this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
      this.buttonTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonTest.AutoEllipsis = true;
      this.buttonTest.Enabled = false;
      this.buttonTest.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonTest.FlatStyle = FlatStyle.Flat;
      this.buttonTest.Location = new Point(537, 272);
      this.buttonTest.Margin = new Padding(3, 4, 3, 4);
      this.buttonTest.Name = "buttonTest";
      this.buttonTest.Size = new Size(137, 25);
      this.buttonTest.TabIndex = 2;
      this.buttonTest.TabStop = false;
      this.buttonTest.Text = "button1";
      this.buttonTest.UseVisualStyleBackColor = true;
      this.buttonTest.Visible = false;
      this.buttonTest.Click += new EventHandler(this.buttonTest_Click);
      this.buttonClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonClear.AutoEllipsis = true;
      this.buttonClear.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonClear.FlatStyle = FlatStyle.Flat;
      this.buttonClear.Location = new Point(680, 272);
      this.buttonClear.Margin = new Padding(3, 4, 3, 4);
      this.buttonClear.Name = "buttonClear";
      this.buttonClear.Size = new Size(79, 24);
      this.buttonClear.TabIndex = 1;
      this.buttonClear.Text = "button2";
      this.buttonClear.UseVisualStyleBackColor = true;
      this.buttonClear.Click += new EventHandler(this.buttonClear_Click);
      this.pictureBoxDrawTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.pictureBoxDrawTest.BackColor = Color.Transparent;
      this.pictureBoxDrawTest.Enabled = false;
      this.pictureBoxDrawTest.Location = new Point(537, 297);
      this.pictureBoxDrawTest.Margin = new Padding(3, 4, 3, 4);
      this.pictureBoxDrawTest.Name = "pictureBoxDrawTest";
      this.pictureBoxDrawTest.Size = new Size(222, 230);
      this.pictureBoxDrawTest.TabIndex = 8;
      this.pictureBoxDrawTest.TabStop = false;
      this.checkInk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkInk.AutoSize = true;
      this.checkInk.BackColor = Color.Transparent;
      this.checkInk.Location = new Point(40, 526);
      this.checkInk.Margin = new Padding(3, 4, 3, 4);
      this.checkInk.Name = "checkInk";
      this.checkInk.Size = new Size(89, 21);
      this.checkInk.TabIndex = 3;
      this.checkInk.Text = "checkBox1";
      this.checkInk.UseVisualStyleBackColor = false;
      this.checkTab.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkTab.AutoSize = true;
      this.checkTab.BackColor = Color.Transparent;
      this.checkTab.Location = new Point(135, 526);
      this.checkTab.Margin = new Padding(3, 4, 3, 4);
      this.checkTab.Name = "checkTab";
      this.checkTab.Size = new Size(89, 21);
      this.checkTab.TabIndex = 4;
      this.checkTab.Text = "checkBox2";
      this.checkTab.UseVisualStyleBackColor = false;
      this.labelMaxPressValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.labelMaxPressValue.AutoSize = true;
      this.labelMaxPressValue.BackColor = Color.Transparent;
      this.labelMaxPressValue.Location = new Point(796, 24);
      this.labelMaxPressValue.Name = "labelMaxPressValue";
      this.labelMaxPressValue.Size = new Size(15, 17);
      this.labelMaxPressValue.TabIndex = 16;
      this.labelMaxPressValue.Text = "4";
      this.labelMinPressValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.labelMinPressValue.AutoSize = true;
      this.labelMinPressValue.BackColor = Color.Transparent;
      this.labelMinPressValue.Location = new Point(796, 252);
      this.labelMinPressValue.Name = "labelMinPressValue";
      this.labelMinPressValue.Size = new Size(20, 17);
      this.labelMinPressValue.TabIndex = 17;
      this.labelMinPressValue.Text = "-4";
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.Transparent;
      this.label1.Location = new Point(796, 138);
      this.label1.Name = "label1";
      this.label1.Size = new Size(15, 17);
      this.label1.TabIndex = 18;
      this.label1.Text = "0";
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Location = new Point(539, 277);
      this.label2.Name = "label2";
      this.label2.Size = new Size(43, 17);
      this.label2.TabIndex = 19;
      this.label2.Text = "label2";
      this.checkBoxMouseMode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkBoxMouseMode.AutoSize = true;
      this.checkBoxMouseMode.BackColor = Color.Transparent;
      this.checkBoxMouseMode.Location = new Point(245, 526);
      this.checkBoxMouseMode.Margin = new Padding(3, 4, 3, 4);
      this.checkBoxMouseMode.Name = "checkBoxMouseMode";
      this.checkBoxMouseMode.Size = new Size(106, 21);
      this.checkBoxMouseMode.TabIndex = 20;
      this.checkBoxMouseMode.Text = "Mouse Mode";
      this.checkBoxMouseMode.UseVisualStyleBackColor = false;
      this.checkBgame.AutoSize = true;
      this.checkBgame.Location = new Point(388, 526);
      this.checkBgame.Name = "checkBgame";
      this.checkBgame.Size = new Size(89, 21);
      this.checkBgame.TabIndex = 21;
      this.checkBgame.Text = "checkBox1";
      this.checkBgame.UseVisualStyleBackColor = true;
      this.touchPressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.touchPressBar1.BackColor = Color.Transparent;
      this.touchPressBar1.ForeColor = Color.CornflowerBlue;
      this.touchPressBar1.Location = new Point(771, 297);
      this.touchPressBar1.Margin = new Padding(3, 4, 3, 4);
      this.touchPressBar1.MaxValue = 100;
      this.touchPressBar1.MinValue = 0;
      this.touchPressBar1.Name = "touchPressBar1";
      this.touchPressBar1.Orientation = Orientation.Vertical;
      this.touchPressBar1.Size = new Size(43, 229);
      this.touchPressBar1.TabIndex = 14;
      this.touchPressBar1.TextColor = Color.Black;
      this.touchPressBar1.TickStyle = TickStyle.BottomRight;
      this.touchPressBar1.Value = 0;
      this.pictureBoxDrawLine.BackColor = Color.Transparent;
      this.pictureBoxDrawLine.Location = new Point(0, 0);
      this.pictureBoxDrawLine.Margin = new Padding(3, 4, 3, 4);
      this.pictureBoxDrawLine.Name = "pictureBoxDrawLine";
      this.pictureBoxDrawLine.Pictrue = (Bitmap) null;
      this.pictureBoxDrawLine.PictrueRect = new Rectangle(0, 0, 0, 0);
      this.pictureBoxDrawLine.Size = new Size(100, 65);
      this.pictureBoxDrawLine.TabIndex = 15;
      this.pictureBoxDrawLine.TabStop = false;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(840, 560);
      this.Controls.Add((Control) this.checkBgame);
      this.Controls.Add((Control) this.checkBoxMouseMode);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.labelMinPressValue);
      this.Controls.Add((Control) this.labelMaxPressValue);
      this.Controls.Add((Control) this.checkInk);
      this.Controls.Add((Control) this.checkTab);
      this.Controls.Add((Control) this.touchPressBar1);
      this.Controls.Add((Control) this.buttonClear);
      this.Controls.Add((Control) this.buttonTest);
      this.Controls.Add((Control) this.labelSetPress);
      this.Controls.Add((Control) this.trackBar1);
      this.Controls.Add((Control) this.btnBelow);
      this.Controls.Add((Control) this.btnAbove);
      this.Controls.Add((Control) this.imgPen);
      this.Controls.Add((Control) this.pictureBoxDrawLine);
      this.Controls.Add((Control) this.pictureBoxPressCurve);
      this.Controls.Add((Control) this.pictureBoxDrawTest);
      this.Cursor = Cursors.Default;
      this.DoubleBuffered = true;
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Margin = new Padding(3, 4, 3, 4);
      this.Name = nameof (FormTabletPen);
      this.Text = "Form1";
      this.TransparencyKey = SystemColors.Control;
      this.Deactivate += new EventHandler(this.FormTabletPen_Deactivate);
      this.Load += new EventHandler(this.FormTabletPen_Load);
      ((ISupportInitialize) this.imgPen).EndInit();
      ((ISupportInitialize) this.pictureBoxPressCurve).EndInit();
      this.trackBar1.EndInit();
      ((ISupportInitialize) this.pictureBoxDrawTest).EndInit();
      ((ISupportInitialize)this.pictureBoxDrawLine).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
