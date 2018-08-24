// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormCommonSettings
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using HuionTablet.Lib;
using HuionTablet.utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormCommonSettings : Form
  {
    private IContainer components;
    private CheckBox checkAutorun;

    public FormCommonSettings()
    {
      this.InitializeComponent();
      this.checkAutorun.Enabled = Utils.isAdmin();
      this.checkAutorun.Checked = !Utils.isWin10 ? SettingsUtil.isAutorun() : SettingsUtil.isCommonStartup;
      this.checkAutorun.Text = ResourceCulture.GetString("SettingsAutorun");
    }

    private void checkAutorun_Click(object sender, EventArgs e)
    {
      if (Utils.isWin10)
      {
        SettingsUtil.isCommonStartup = !this.checkAutorun.Checked;
        this.checkAutorun.Checked = !this.checkAutorun.Checked;
      }
      else
      {
        if (!SettingsUtil.setAutorun(!this.checkAutorun.Checked))
          return;
        this.checkAutorun.Checked = !this.checkAutorun.Checked;
      }
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 512)
        return;
      TimerSession.userOperation();
    }

    private void FormCommonSettings_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.checkAutorun = new CheckBox();
      this.SuspendLayout();
      this.checkAutorun.AutoCheck = false;
      this.checkAutorun.AutoSize = true;
      this.checkAutorun.Location = new Point(25, 22);
      this.checkAutorun.Name = "checkAutorun";
      this.checkAutorun.RightToLeft = RightToLeft.No;
      this.checkAutorun.Size = new Size(99, 21);
      this.checkAutorun.TabIndex = 0;
      this.checkAutorun.Text = "开机自动运行";
      this.checkAutorun.TextAlign = ContentAlignment.TopLeft;
      this.checkAutorun.UseVisualStyleBackColor = true;
      this.checkAutorun.Click += new EventHandler(this.checkAutorun_Click);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(400, 162);
      this.ControlBox = false;
      this.Controls.Add((Control) this.checkAutorun);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Margin = new Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormCommonSettings);
      this.RightToLeft = RightToLeft.Yes;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (FormCommonSettings);
      this.Load += new EventHandler(this.FormCommonSettings_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
