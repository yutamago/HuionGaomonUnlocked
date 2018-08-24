// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormShortcut
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using Huion;
using HuionTablet.Lib;
using HuionTablet.utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormShortcut : Form
  {
    private IntPtr mMainFormHandle;
    private IContainer components;
    private Label labelOpenMainForm;
    private HuionTextView tvOpenMainForm;
    private Button btnReset;
    private PictureBox pbOpenMainForm;

    public FormShortcut(IntPtr mainFormHandle)
    {
      this.InitializeComponent();
      this.mMainFormHandle = mainFormHandle;
      this.pbOpenMainForm.Image = (Image) ImageHelper.getDllImage("warnning.png");
      this.pbOpenMainForm.Visible = false;
      ViewUtils.CreateTip((Control) this.pbOpenMainForm, ResourceCulture.GetString("SettingsHotkeyRegistered"));
      this.labelOpenMainForm.Text = ResourceCulture.GetString("SettingsOpenDriverUI");
      this.btnReset.Text = ResourceCulture.GetString("SettingsResetHotkey");
    }

    private bool IsRegisterCallback(object sender, HuionKeyEventArgs hotKey)
    {
      bool flag = KeyboardUtils.CheckHotkey(Fixer4Main.MainForm.Handle, hotKey);
      if (sender == this.tvOpenMainForm)
        this.pbOpenMainForm.Visible = !flag;
      return flag;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.tvOpenMainForm.mKeyChangedListener += new HuionTextView.KeyChangedListener(this.onKeyChangedListener);
      this.tvOpenMainForm.isRegisterCallback += new HuionTextView.IsRegisterCallback(this.IsRegisterCallback);
      this.tvOpenMainForm.addKeyEvent(SettingsUtil.ShowUIShortcut);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 512)
        return;
      TimerSession.userOperation();
    }

    private string onKeyChangedListener(HuionKeyEventArgs keyEvent)
    {
      SettingsUtil.ShowUIShortcut = keyEvent;
      HuionDriverDLL.PostMessage(this.mMainFormHandle, 1064, IntPtr.Zero, IntPtr.Zero);
      return KBTable.getKBTable8Keys(keyEvent.KeyCode).KeyName;
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      this.tvOpenMainForm.clearKeyEvents();
      this.tvOpenMainForm.addKeyEvent(SettingsUtil.DefaultUIShortcut);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.labelOpenMainForm = new Label();
      this.btnReset = new Button();
      this.pbOpenMainForm = new PictureBox();
      this.tvOpenMainForm = new HuionTextView();
      ((ISupportInitialize) this.pbOpenMainForm).BeginInit();
      this.SuspendLayout();
      this.labelOpenMainForm.AutoSize = true;
      this.labelOpenMainForm.Location = new Point(23, 13);
      this.labelOpenMainForm.Name = "labelOpenMainForm";
      this.labelOpenMainForm.Size = new Size(79, 17);
      this.labelOpenMainForm.TabIndex = 0;
      this.labelOpenMainForm.Text = "Open Driver";
      this.labelOpenMainForm.TextAlign = ContentAlignment.MiddleLeft;
      this.btnReset.AutoEllipsis = true;
      this.btnReset.FlatAppearance.BorderColor = SystemColors.Control;
      this.btnReset.Location = new Point(282, 93);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new Size(106, 29);
      this.btnReset.TabIndex = 2;
      this.btnReset.Text = "重置为默认值";
      this.btnReset.UseVisualStyleBackColor = true;
      this.btnReset.Click += new EventHandler(this.btnReset_Click);
      this.pbOpenMainForm.Location = new Point(271, 36);
      this.pbOpenMainForm.Name = "pbOpenMainForm";
      this.pbOpenMainForm.Size = new Size(20, 20);
      this.pbOpenMainForm.SizeMode = PictureBoxSizeMode.Zoom;
      this.pbOpenMainForm.TabIndex = 3;
      this.pbOpenMainForm.TabStop = false;
      this.tvOpenMainForm.BorderStyle = BorderStyle.FixedSingle;
      this.tvOpenMainForm.ImeMode = ImeMode.Disable;
      this.tvOpenMainForm.IsAltChecked = false;
      this.tvOpenMainForm.IsControlChecked = false;
      this.tvOpenMainForm.IsShiftChecked = false;
      this.tvOpenMainForm.IsSingleKeys = true;
      this.tvOpenMainForm.IsWinChecked = false;
      this.tvOpenMainForm.Location = new Point(27, 35);
      this.tvOpenMainForm.Name = "tvOpenMainForm";
      this.tvOpenMainForm.Size = new Size(238, 23);
      this.tvOpenMainForm.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(400, 261);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pbOpenMainForm);
      this.Controls.Add((Control) this.btnReset);
      this.Controls.Add((Control) this.tvOpenMainForm);
      this.Controls.Add((Control) this.labelOpenMainForm);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormShortcut);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "FormHotkey";
      ((ISupportInitialize) this.pbOpenMainForm).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
