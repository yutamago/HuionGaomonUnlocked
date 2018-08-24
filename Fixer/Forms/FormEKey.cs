// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormEKey
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using HuionTablet.Lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormEKey : Form
  {
    private object mHolder;
    private HNStruct.HNEkey m_Ekey;
    public const int KeyType_Combo = 1;
    public const int KeyType_Mouse = 2;
    public const int KeyType_Switch = 3;
    public const int KeyType_Execute = 4;
    public const char SplitChar = ' ';
    public const string SplitString = " ";
    private IContainer components;
    private CheckBox CheckCtrl;
    private CheckBox checkAlt;
    private CheckBox checkShift;
    private CheckBox checkWindows;
    private RadioButton radioChar;
    private ComboBox comboBoxStr;
    private Button buttonClearStr;
    private RadioButton radioStr;
    private Label label1;
    private Label labelStr;
    private Label labelMaxStr;
    private RadioButton radioMouseLeft;
    private RadioButton radioMouseMid;
    private RadioButton radioMouseRight;
    private RadioButton radioSwitchScreen;
    private RadioButton radioSwithBrush;
    private Label labelProgressPath;
    private Label labelPath;
    private Button buttonBrowse;
    private Button buttonOk;
    private Button buttonCancel;
    private Button buttonJoinStr;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private CheckBox checkBoxFunKb;
    private CheckBox checkBoxFunMouse;
    private CheckBox checkBoxFunSwitch;
    private CheckBox checkBoxFunRun;
    private RadioButton radioMouseWheelBackWard;
    private RadioButton radioMouseWheelForward;

    public event EKeyCallback callback;

    public FormEKey(object holder, HNStruct.HNEkey value)
    {
      this.mHolder = holder;
      this.m_Ekey = value;
      this.InitializeComponent();
      MiddleModule.eventPost += new Post(this.MiddleModule_eventSend);
    }

    private void MiddleModule_eventSend(object sender, object msg)
    {
      if (Convert.ToBoolean(msg))
        return;
      this.Invoke((Delegate) new HuionTablet.utils.Void(((Form) this).Close));
    }

    private void FormEKey_Load(object sender, EventArgs e)
    {
      this.Icon = Fixer4Main.FormIcon();
      this.labelPath.Text = new string(this.m_Ekey.cmdPath);
      this.setViewText8Locale();
      this.BringToFront();
      this.comboBoxStr.Items.Clear();
      this.comboBoxStr.MeasureItem += new MeasureItemEventHandler(this.comboBoxStr_MeasureItem);
      for (int index = 0; index < KBTable.KeyBoards.Count; ++index)
      {
        KBTable keyBoard = KBTable.KeyBoards[index];
        this.comboBoxStr.Items.Add((object) keyBoard);
        if ((int) this.m_Ekey.kbtn.kbKeys[0] == (int) keyBoard.keyCode)
          this.comboBoxStr.Text = keyBoard.ToString();
      }
      this.updateHNConfigToView();
    }

    private void setViewText8Locale()
    {
      this.radioChar.Text = ResourceCulture.GetString("FormEkey_rdCharText");
      this.radioStr.Text = ResourceCulture.GetString("FormEkey_rdStrText");
      this.radioMouseLeft.Text = ResourceCulture.GetString("FormEkey_rdMouseLeftText");
      this.radioMouseRight.Text = ResourceCulture.GetString("FormEkey_rdMouseRightText");
      this.radioMouseMid.Text = ResourceCulture.GetString("FormEkey_rdMouseMidText");
      this.radioMouseWheelForward.Text = ResourceCulture.GetString("FormEkey_rdMouseWheelForwardText");
      this.radioMouseWheelBackWard.Text = ResourceCulture.GetString("FormEkey_rdMouseWheelBackwardText");
      this.radioSwitchScreen.Text = ResourceCulture.GetString("FormEkey_rdSwitchScreenText");
      this.radioSwithBrush.Text = ResourceCulture.GetString("FormEkey_rdSwitchBrushText");
      this.buttonClearStr.Text = ResourceCulture.GetString("FormEkey_btJoinClearText");
      this.buttonBrowse.Text = ResourceCulture.GetString("FormEkey_btBrowseText");
      this.buttonJoinStr.Text = ResourceCulture.GetString("FormEkey_btJoinStrText");
      this.buttonOk.Text = ResourceCulture.GetString("FormEkey_btOKText");
      this.buttonCancel.Text = ResourceCulture.GetString("FormEkey_btCannelText");
      this.labelMaxStr.Text = ResourceCulture.GetString("FormEkey_lbMaxStrText");
      this.labelProgressPath.Text = ResourceCulture.GetString("FormEkey_lbProgressPathText");
      this.Text = ResourceCulture.GetString("FormEKey_Text");
      this.checkBoxFunKb.Text = ResourceCulture.GetString("FormEkey_ckFunKbText");
      this.checkBoxFunMouse.Text = ResourceCulture.GetString("FormEkey_ckFunMouseText");
      this.checkBoxFunRun.Text = ResourceCulture.GetString("FormEkey_ckFunRunText");
      this.checkBoxFunSwitch.Text = ResourceCulture.GetString("FormEkey_ckFunSwitchText");
    }

    private void comboBoxStr_MeasureItem(object sender, MeasureItemEventArgs e)
    {
      e.ItemHeight = 50;
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
      HNStruct.HNEkey hnEkey = new HNStruct.HNEkey();
      hnEkey.cmdPath = new char[260];
      hnEkey.kbtn.kbKeys = new byte[16];
      if (this.checkBoxFunKb.Checked)
      {
        if (this.CheckCtrl.Checked)
          hnEkey.kbtn.bCtrl = (byte) 1;
        if (this.checkAlt.Checked)
          hnEkey.kbtn.bAlt = (byte) 1;
        if (this.checkShift.Checked)
          hnEkey.kbtn.bShift = (byte) 1;
        if (this.checkWindows.Checked)
          hnEkey.kbtn.bWin = (byte) 1;
        if (this.radioChar.Checked && this.comboBoxStr.SelectedIndex != -1)
        {
          KBTable selectedItem = (KBTable) this.comboBoxStr.SelectedItem;
          hnEkey.kbtn.kbKeys[0] = selectedItem.keyCode;
        }
        else if (this.radioStr.Checked && !string.IsNullOrEmpty(this.labelStr.Text.Trim()))
        {
          string[] strArray = this.labelStr.Text.Trim().Split(' ');
          for (int index = 0; index < strArray.Length; ++index)
            hnEkey.kbtn.kbKeys[index] = KBTable.getKey8Name(strArray[index]).keyCode;
        }
        if (hnEkey.kbtn.bCtrl == (byte) 1 || hnEkey.kbtn.bAlt == (byte) 1 || (hnEkey.kbtn.bShift == (byte) 1 || hnEkey.kbtn.bWin == (byte) 1) || hnEkey.kbtn.kbKeys.Length != 0)
          hnEkey.funcBit |= 4U;
      }
      if (this.checkBoxFunMouse.Checked)
      {
        if (this.radioMouseLeft.Checked)
          hnEkey.funcBit |= 16U;
        else if (this.radioMouseRight.Checked)
          hnEkey.funcBit |= 64U;
        else if (this.radioMouseMid.Checked)
          hnEkey.funcBit |= 32U;
        else if (this.radioMouseWheelForward.Checked)
          hnEkey.funcBit |= 128U;
        else if (this.radioMouseWheelBackWard.Checked)
          hnEkey.funcBit |= 256U;
      }
      if (this.checkBoxFunSwitch.Checked)
      {
        if (this.radioSwitchScreen.Checked)
          hnEkey.funcBit |= 1U;
        else if (this.radioSwithBrush.Checked)
          hnEkey.funcBit |= 2U;
      }
      if (this.checkBoxFunRun.Checked && !string.IsNullOrEmpty(this.labelPath.Text.Trim()))
      {
        hnEkey.funcBit |= 8U;
        this.labelPath.Text.ToCharArray().CopyTo((Array) hnEkey.cmdPath, 0);
      }
      // ISSUE: reference to a compiler-generated field
      this.callback(this.mHolder, hnEkey);
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void updateHNConfigToView()
    {
      if (((int) this.m_Ekey.funcBit & 4) == 4)
      {
        this.checkAlt.Checked = this.m_Ekey.kbtn.bAlt > (byte) 0;
        this.checkWindows.Checked = this.m_Ekey.kbtn.bWin > (byte) 0;
        this.CheckCtrl.Checked = this.m_Ekey.kbtn.bCtrl > (byte) 0;
        this.checkShift.Checked = this.m_Ekey.kbtn.bShift > (byte) 0;
        this.checkBoxFunKb.Checked = this.CheckCtrl.Checked || this.checkAlt.Checked || (this.checkWindows.Checked || this.checkShift.Checked) || (this.radioChar.Checked || this.radioStr.Checked);
        if (Convert.ToBoolean(Convert.ToInt32(this.m_Ekey.kbtn.kbKeys[1])))
        {
          this.checkBoxFunKb.Checked = true;
          this.radioStr.Checked = true;
          for (int index = 0; index < 16 && Convert.ToBoolean(this.m_Ekey.kbtn.kbKeys[index]); ++index)
          {
            Label labelStr = this.labelStr;
            labelStr.Text = labelStr.Text + (object) KBTable.getKey8Code(this.m_Ekey.kbtn.kbKeys[index]) + " ";
          }
        }
        else
        {
          this.radioChar.Checked = true;
          this.checkBoxFunKb.Checked = true;
          this.comboBoxStr.Text += (string) (object) KBTable.getKey8Code(this.m_Ekey.kbtn.kbKeys[0]);
        }
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 16U))
      {
        this.checkBoxFunMouse.Checked = true;
        this.radioMouseLeft.Checked = true;
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 32U))
      {
        this.checkBoxFunMouse.Checked = true;
        this.radioMouseMid.Checked = true;
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 64U))
      {
        this.checkBoxFunMouse.Checked = true;
        this.radioMouseRight.Checked = true;
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 128U))
      {
        this.checkBoxFunMouse.Checked = true;
        this.radioMouseWheelForward.Checked = true;
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 256U))
      {
        this.checkBoxFunMouse.Checked = true;
        this.radioMouseWheelBackWard.Checked = true;
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 2U))
      {
        this.checkBoxFunSwitch.Checked = true;
        this.radioSwithBrush.Checked = true;
      }
      if (Convert.ToBoolean(this.m_Ekey.funcBit & 1U))
      {
        this.checkBoxFunSwitch.Checked = true;
        this.radioSwitchScreen.Checked = true;
      }
      if (!Convert.ToBoolean(this.m_Ekey.funcBit & 8U))
        return;
      this.checkBoxFunRun.Checked = true;
    }

    private void buttonJoinStr_Click(object sender, EventArgs e)
    {
      this.checkBoxFunKb.Checked = true;
      this.radioStr.Checked = true;
      if (this.labelStr.Text.Split(' ').Length > 16)
        return;
      this.labelStr.Text += (string) this.comboBoxStr.SelectedItem;
      this.labelStr.Text += " ";
    }

    private void buttonClearStr_Click(object sender, EventArgs e)
    {
      this.checkBoxFunKb.Checked = true;
      this.radioStr.Checked = true;
      this.labelStr.Text = (string) null;
    }

    private void comboBoxStr_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.radioChar.Checked = true;
    }

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = ResourceCulture.GetString("ExecutableFile") + "|*.exe";
      openFileDialog.RestoreDirectory = true;
      openFileDialog.FilterIndex = 1;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.labelPath.Text = openFileDialog.FileName;
      openFileDialog.Dispose();
    }

    private void TypeChanged(object sender, EventArgs e)
    {
      if (!(sender is CheckBox))
        return;
      CheckBox checkBox = (CheckBox) sender;
      switch (Convert.ToInt32(checkBox.Tag))
      {
        case 1:
          this.groupBox1.Enabled = checkBox.Checked;
          break;
        case 2:
          this.groupBox2.Enabled = checkBox.Checked;
          break;
        case 3:
          this.groupBox3.Enabled = checkBox.Checked;
          break;
        case 4:
          this.groupBox4.Enabled = checkBox.Checked;
          break;
      }
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 512)
        return;
      TimerSession.userOperation();
    }

    private void FormEKey_FormClosed(object sender, FormClosedEventArgs e)
    {
      MiddleModule.eventPost -= new Post(this.MiddleModule_eventSend);
      this.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormEKey));
      this.CheckCtrl = new CheckBox();
      this.checkAlt = new CheckBox();
      this.checkShift = new CheckBox();
      this.checkWindows = new CheckBox();
      this.radioChar = new RadioButton();
      this.comboBoxStr = new ComboBox();
      this.buttonClearStr = new Button();
      this.radioStr = new RadioButton();
      this.label1 = new Label();
      this.labelStr = new Label();
      this.labelMaxStr = new Label();
      this.radioMouseLeft = new RadioButton();
      this.radioMouseMid = new RadioButton();
      this.radioMouseRight = new RadioButton();
      this.radioSwitchScreen = new RadioButton();
      this.radioSwithBrush = new RadioButton();
      this.labelProgressPath = new Label();
      this.labelPath = new Label();
      this.buttonBrowse = new Button();
      this.buttonOk = new Button();
      this.buttonCancel = new Button();
      this.buttonJoinStr = new Button();
      this.groupBox1 = new GroupBox();
      this.checkBoxFunKb = new CheckBox();
      this.groupBox2 = new GroupBox();
      this.radioMouseWheelBackWard = new RadioButton();
      this.radioMouseWheelForward = new RadioButton();
      this.checkBoxFunMouse = new CheckBox();
      this.groupBox3 = new GroupBox();
      this.checkBoxFunSwitch = new CheckBox();
      this.groupBox4 = new GroupBox();
      this.checkBoxFunRun = new CheckBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      this.CheckCtrl.AutoSize = true;
      this.CheckCtrl.Location = new Point(18, 15);
      this.CheckCtrl.Margin = new Padding(3, 4, 3, 4);
      this.CheckCtrl.Name = "CheckCtrl";
      this.CheckCtrl.Size = new Size(47, 21);
      this.CheckCtrl.TabIndex = 2;
      this.CheckCtrl.Text = "Ctrl";
      this.CheckCtrl.UseVisualStyleBackColor = true;
      this.checkAlt.AutoSize = true;
      this.checkAlt.Location = new Point(232, 15);
      this.checkAlt.Margin = new Padding(3, 4, 3, 4);
      this.checkAlt.Name = "checkAlt";
      this.checkAlt.Size = new Size(42, 21);
      this.checkAlt.TabIndex = 5;
      this.checkAlt.Text = "Alt";
      this.checkAlt.UseVisualStyleBackColor = true;
      this.checkShift.AutoSize = true;
      this.checkShift.Location = new Point(122, 15);
      this.checkShift.Margin = new Padding(3, 4, 3, 4);
      this.checkShift.Name = "checkShift";
      this.checkShift.Size = new Size(52, 21);
      this.checkShift.TabIndex = 3;
      this.checkShift.Text = "Shift";
      this.checkShift.UseVisualStyleBackColor = true;
      this.checkWindows.AutoSize = true;
      this.checkWindows.Location = new Point(330, 15);
      this.checkWindows.Margin = new Padding(3, 4, 3, 4);
      this.checkWindows.Name = "checkWindows";
      this.checkWindows.Size = new Size(80, 21);
      this.checkWindows.TabIndex = 6;
      this.checkWindows.Text = "Windows";
      this.checkWindows.UseVisualStyleBackColor = true;
      this.radioChar.AutoSize = true;
      this.radioChar.Location = new Point(18, 46);
      this.radioChar.Margin = new Padding(3, 4, 3, 4);
      this.radioChar.Name = "radioChar";
      this.radioChar.Size = new Size(74, 21);
      this.radioChar.TabIndex = 7;
      this.radioChar.TabStop = true;
      this.radioChar.Text = "键盘按键";
      this.radioChar.UseVisualStyleBackColor = true;
      this.comboBoxStr.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxStr.FormattingEnabled = true;
      this.comboBoxStr.ItemHeight = 17;
      this.comboBoxStr.Location = new Point(39, 70);
      this.comboBoxStr.Margin = new Padding(3, 4, 3, 4);
      this.comboBoxStr.Name = "comboBoxStr";
      this.comboBoxStr.Size = new Size(306, 25);
      this.comboBoxStr.TabIndex = 8;
      this.comboBoxStr.SelectedIndexChanged += new EventHandler(this.comboBoxStr_SelectedIndexChanged);
      this.buttonClearStr.AutoSize = true;
      this.buttonClearStr.Location = new Point(617, 126);
      this.buttonClearStr.Margin = new Padding(3, 4, 3, 4);
      this.buttonClearStr.Name = "buttonClearStr";
      this.buttonClearStr.Size = new Size(107, 27);
      this.buttonClearStr.TabIndex = 12;
      this.buttonClearStr.Text = "清空字符串";
      this.buttonClearStr.UseVisualStyleBackColor = true;
      this.buttonClearStr.Click += new EventHandler(this.buttonClearStr_Click);
      this.radioStr.AutoSize = true;
      this.radioStr.Location = new Point(18, 102);
      this.radioStr.Margin = new Padding(3, 4, 3, 4);
      this.radioStr.Name = "radioStr";
      this.radioStr.Size = new Size(74, 21);
      this.radioStr.TabIndex = 10;
      this.radioStr.TabStop = true;
      this.radioStr.Text = "字符串键";
      this.radioStr.UseVisualStyleBackColor = true;
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.White;
      this.label1.Location = new Point(170, 218);
      this.label1.Name = "label1";
      this.label1.Size = new Size(0, 17);
      this.label1.TabIndex = 10;
      this.labelStr.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelStr.BackColor = Color.Transparent;
      this.labelStr.BorderStyle = BorderStyle.FixedSingle;
      this.labelStr.ForeColor = SystemColors.ControlText;
      this.labelStr.Location = new Point(39, 126);
      this.labelStr.Name = "labelStr";
      this.labelStr.Size = new Size(432, 27);
      this.labelStr.TabIndex = 11;
      this.labelStr.TextAlign = ContentAlignment.MiddleLeft;
      this.labelMaxStr.AutoSize = true;
      this.labelMaxStr.ForeColor = SystemColors.ControlDarkDark;
      this.labelMaxStr.Location = new Point(474, 131);
      this.labelMaxStr.Name = "labelMaxStr";
      this.labelMaxStr.Size = new Size(58, 17);
      this.labelMaxStr.TabIndex = 0;
      this.labelMaxStr.Text = "最多16个";
      this.radioMouseLeft.AutoSize = true;
      this.radioMouseLeft.Location = new Point(20, 16);
      this.radioMouseLeft.Margin = new Padding(3, 4, 3, 4);
      this.radioMouseLeft.Name = "radioMouseLeft";
      this.radioMouseLeft.Size = new Size(74, 21);
      this.radioMouseLeft.TabIndex = 14;
      this.radioMouseLeft.TabStop = true;
      this.radioMouseLeft.Text = "鼠标左键";
      this.radioMouseLeft.UseVisualStyleBackColor = true;
      this.radioMouseMid.AutoSize = true;
      this.radioMouseMid.Location = new Point(266, 16);
      this.radioMouseMid.Margin = new Padding(3, 4, 3, 4);
      this.radioMouseMid.Name = "radioMouseMid";
      this.radioMouseMid.Size = new Size(74, 21);
      this.radioMouseMid.TabIndex = 15;
      this.radioMouseMid.TabStop = true;
      this.radioMouseMid.Text = "鼠标中键";
      this.radioMouseMid.UseVisualStyleBackColor = true;
      this.radioMouseRight.AutoSize = true;
      this.radioMouseRight.Location = new Point(518, 16);
      this.radioMouseRight.Margin = new Padding(3, 4, 3, 4);
      this.radioMouseRight.Name = "radioMouseRight";
      this.radioMouseRight.Size = new Size(74, 21);
      this.radioMouseRight.TabIndex = 16;
      this.radioMouseRight.TabStop = true;
      this.radioMouseRight.Text = "鼠标右键";
      this.radioMouseRight.UseVisualStyleBackColor = true;
      this.radioSwitchScreen.AutoSize = true;
      this.radioSwitchScreen.Location = new Point(20, 14);
      this.radioSwitchScreen.Margin = new Padding(3, 4, 3, 4);
      this.radioSwitchScreen.Name = "radioSwitchScreen";
      this.radioSwitchScreen.Size = new Size(74, 21);
      this.radioSwitchScreen.TabIndex = 20;
      this.radioSwitchScreen.TabStop = true;
      this.radioSwitchScreen.Text = "切换屏幕";
      this.radioSwitchScreen.UseVisualStyleBackColor = true;
      this.radioSwithBrush.AutoSize = true;
      this.radioSwithBrush.Location = new Point(266, 14);
      this.radioSwithBrush.Margin = new Padding(3, 4, 3, 4);
      this.radioSwithBrush.Name = "radioSwithBrush";
      this.radioSwithBrush.Size = new Size(74, 21);
      this.radioSwithBrush.TabIndex = 20;
      this.radioSwithBrush.TabStop = true;
      this.radioSwithBrush.Text = "切换笔刷";
      this.radioSwithBrush.UseVisualStyleBackColor = true;
      this.labelProgressPath.AutoSize = true;
      this.labelProgressPath.Location = new Point(20, 13);
      this.labelProgressPath.Name = "labelProgressPath";
      this.labelProgressPath.Size = new Size(56, 17);
      this.labelProgressPath.TabIndex = 0;
      this.labelProgressPath.Text = "程序路径";
      this.labelProgressPath.TextAlign = ContentAlignment.MiddleRight;
      this.labelPath.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.labelPath.BorderStyle = BorderStyle.FixedSingle;
      this.labelPath.Location = new Point(23, 33);
      this.labelPath.Name = "labelPath";
      this.labelPath.Size = new Size(542, 27);
      this.labelPath.TabIndex = 22;
      this.buttonBrowse.AutoSize = true;
      this.buttonBrowse.Location = new Point(597, 33);
      this.buttonBrowse.Margin = new Padding(3, 4, 3, 4);
      this.buttonBrowse.Name = "buttonBrowse";
      this.buttonBrowse.Size = new Size(101, 27);
      this.buttonBrowse.TabIndex = 23;
      this.buttonBrowse.Text = "浏览";
      this.buttonBrowse.UseVisualStyleBackColor = true;
      this.buttonBrowse.Click += new EventHandler(this.buttonBrowse_Click);
      this.buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonOk.ForeColor = SystemColors.ControlText;
      this.buttonOk.Location = new Point(443, 478);
      this.buttonOk.Margin = new Padding(3, 4, 3, 4);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new Size(149, 27);
      this.buttonOk.TabIndex = 24;
      this.buttonOk.Text = "确定";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
      this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonCancel.ForeColor = SystemColors.ControlText;
      this.buttonCancel.Location = new Point(611, 478);
      this.buttonCancel.Margin = new Padding(3, 4, 3, 4);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(147, 27);
      this.buttonCancel.TabIndex = 25;
      this.buttonCancel.Text = "取消";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonJoinStr.AutoSize = true;
      this.buttonJoinStr.Location = new Point(369, 69);
      this.buttonJoinStr.Margin = new Padding(3, 4, 3, 4);
      this.buttonJoinStr.Name = "buttonJoinStr";
      this.buttonJoinStr.Size = new Size(107, 27);
      this.buttonJoinStr.TabIndex = 9;
      this.buttonJoinStr.Text = "加入字符串";
      this.buttonJoinStr.UseVisualStyleBackColor = true;
      this.buttonJoinStr.Click += new EventHandler(this.buttonJoinStr_Click);
      this.groupBox1.BackColor = Color.Transparent;
      this.groupBox1.Controls.Add((Control) this.buttonJoinStr);
      this.groupBox1.Controls.Add((Control) this.checkWindows);
      this.groupBox1.Controls.Add((Control) this.CheckCtrl);
      this.groupBox1.Controls.Add((Control) this.checkAlt);
      this.groupBox1.Controls.Add((Control) this.checkShift);
      this.groupBox1.Controls.Add((Control) this.buttonClearStr);
      this.groupBox1.Controls.Add((Control) this.radioChar);
      this.groupBox1.Controls.Add((Control) this.comboBoxStr);
      this.groupBox1.Controls.Add((Control) this.labelStr);
      this.groupBox1.Controls.Add((Control) this.radioStr);
      this.groupBox1.Controls.Add((Control) this.labelMaxStr);
      this.groupBox1.Enabled = false;
      this.groupBox1.FlatStyle = FlatStyle.Flat;
      this.groupBox1.Location = new Point(17, 27);
      this.groupBox1.Margin = new Padding(3, 4, 3, 4);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new Padding(3, 4, 3, 4);
      this.groupBox1.Size = new Size(739, 160);
      this.groupBox1.TabIndex = 27;
      this.groupBox1.TabStop = false;
      this.checkBoxFunKb.AutoSize = true;
      this.checkBoxFunKb.Location = new Point(17, 12);
      this.checkBoxFunKb.Margin = new Padding(3, 4, 3, 4);
      this.checkBoxFunKb.Name = "checkBoxFunKb";
      this.checkBoxFunKb.Size = new Size(111, 21);
      this.checkBoxFunKb.TabIndex = 1;
      this.checkBoxFunKb.Tag = (object) "1";
      this.checkBoxFunKb.Text = "键盘组合键功能";
      this.checkBoxFunKb.UseVisualStyleBackColor = true;
      this.checkBoxFunKb.CheckStateChanged += new EventHandler(this.TypeChanged);
      this.groupBox2.Controls.Add((Control) this.radioMouseWheelBackWard);
      this.groupBox2.Controls.Add((Control) this.radioMouseWheelForward);
      this.groupBox2.Controls.Add((Control) this.radioMouseMid);
      this.groupBox2.Controls.Add((Control) this.radioMouseLeft);
      this.groupBox2.Controls.Add((Control) this.radioMouseRight);
      this.groupBox2.Enabled = false;
      this.groupBox2.Location = new Point(17, 219);
      this.groupBox2.Margin = new Padding(3, 4, 3, 4);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new Padding(3, 4, 3, 4);
      this.groupBox2.Size = new Size(739, 75);
      this.groupBox2.TabIndex = 28;
      this.groupBox2.TabStop = false;
      this.radioMouseWheelBackWard.AutoSize = true;
      this.radioMouseWheelBackWard.Location = new Point(266, 45);
      this.radioMouseWheelBackWard.Margin = new Padding(3, 4, 3, 4);
      this.radioMouseWheelBackWard.Name = "radioMouseWheelBackWard";
      this.radioMouseWheelBackWard.Size = new Size(74, 21);
      this.radioMouseWheelBackWard.TabIndex = 18;
      this.radioMouseWheelBackWard.TabStop = true;
      this.radioMouseWheelBackWard.Text = "后向滚轮";
      this.radioMouseWheelBackWard.UseVisualStyleBackColor = true;
      this.radioMouseWheelForward.AutoSize = true;
      this.radioMouseWheelForward.Location = new Point(20, 45);
      this.radioMouseWheelForward.Margin = new Padding(3, 4, 3, 4);
      this.radioMouseWheelForward.Name = "radioMouseWheelForward";
      this.radioMouseWheelForward.Size = new Size(74, 21);
      this.radioMouseWheelForward.TabIndex = 17;
      this.radioMouseWheelForward.TabStop = true;
      this.radioMouseWheelForward.Text = "前向滚轮";
      this.radioMouseWheelForward.UseVisualStyleBackColor = true;
      this.checkBoxFunMouse.AutoSize = true;
      this.checkBoxFunMouse.Location = new Point(17, 203);
      this.checkBoxFunMouse.Margin = new Padding(3, 4, 3, 4);
      this.checkBoxFunMouse.Name = "checkBoxFunMouse";
      this.checkBoxFunMouse.Size = new Size(99, 21);
      this.checkBoxFunMouse.TabIndex = 13;
      this.checkBoxFunMouse.Tag = (object) "2";
      this.checkBoxFunMouse.Text = "鼠标按键功能";
      this.checkBoxFunMouse.UseVisualStyleBackColor = true;
      this.checkBoxFunMouse.CheckStateChanged += new EventHandler(this.TypeChanged);
      this.groupBox3.Controls.Add((Control) this.radioSwitchScreen);
      this.groupBox3.Controls.Add((Control) this.radioSwithBrush);
      this.groupBox3.Enabled = false;
      this.groupBox3.Location = new Point(17, 328);
      this.groupBox3.Margin = new Padding(3, 4, 3, 4);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Padding = new Padding(3, 4, 3, 4);
      this.groupBox3.Size = new Size(739, 42);
      this.groupBox3.TabIndex = 29;
      this.groupBox3.TabStop = false;
      this.checkBoxFunSwitch.AutoSize = true;
      this.checkBoxFunSwitch.Location = new Point(17, 312);
      this.checkBoxFunSwitch.Margin = new Padding(3, 4, 3, 4);
      this.checkBoxFunSwitch.Name = "checkBoxFunSwitch";
      this.checkBoxFunSwitch.Size = new Size(75, 21);
      this.checkBoxFunSwitch.TabIndex = 19;
      this.checkBoxFunSwitch.Tag = (object) "3";
      this.checkBoxFunSwitch.Text = "切换功能";
      this.checkBoxFunSwitch.UseVisualStyleBackColor = true;
      this.checkBoxFunSwitch.CheckStateChanged += new EventHandler(this.TypeChanged);
      this.groupBox4.Controls.Add((Control) this.labelProgressPath);
      this.groupBox4.Controls.Add((Control) this.labelPath);
      this.groupBox4.Controls.Add((Control) this.buttonBrowse);
      this.groupBox4.Enabled = false;
      this.groupBox4.Location = new Point(17, 404);
      this.groupBox4.Margin = new Padding(3, 4, 3, 4);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Padding = new Padding(3, 4, 3, 4);
      this.groupBox4.Size = new Size(739, 70);
      this.groupBox4.TabIndex = 30;
      this.groupBox4.TabStop = false;
      this.checkBoxFunRun.AutoSize = true;
      this.checkBoxFunRun.Location = new Point(17, 389);
      this.checkBoxFunRun.Margin = new Padding(3, 4, 3, 4);
      this.checkBoxFunRun.Name = "checkBoxFunRun";
      this.checkBoxFunRun.Size = new Size(99, 21);
      this.checkBoxFunRun.TabIndex = 21;
      this.checkBoxFunRun.Tag = (object) "4";
      this.checkBoxFunRun.Text = "程序运行功能";
      this.checkBoxFunRun.UseVisualStyleBackColor = true;
      this.checkBoxFunRun.CheckStateChanged += new EventHandler(this.TypeChanged);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(770, 508);
      this.Controls.Add((Control) this.checkBoxFunKb);
      this.Controls.Add((Control) this.checkBoxFunSwitch);
      this.Controls.Add((Control) this.checkBoxFunRun);
      this.Controls.Add((Control) this.checkBoxFunMouse);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOk);
      this.Controls.Add((Control) this.label1);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormEKey);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (FormEKey);
      this.FormClosed += new FormClosedEventHandler(this.FormEKey_FormClosed);
      this.Load += new EventHandler(this.FormEKey_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
