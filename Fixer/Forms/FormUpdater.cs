// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormUpdater
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormUpdater : Form
  {
    private static FormUpdater formUpdater = new FormUpdater();
    private IContainer components;
    private ProgressBar progressBar1;
    private LinkLabel linkLabel1;
    private Label label1;
    private LinkLabel linkLabel2;

    public static void showForm()
    {
      FormUpdater.showForm((string) null);
    }

    public static void showForm(string url)
    {
      if (FormUpdater.formUpdater.IsDisposed)
        FormUpdater.formUpdater = new FormUpdater();
      FormUpdater.formUpdater.Show();
      FormUpdater.formUpdater.Focus();
    }

    public FormUpdater()
    {
      this.InitializeComponent();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.Dispose();
    }

    private void FormUpdater_Load(object sender, EventArgs e)
    {
      this.Text = "在线更新";
      this.Icon = Fixer4Main.FormIcon();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.progressBar1 = new ProgressBar();
      this.linkLabel1 = new LinkLabel();
      this.label1 = new Label();
      this.linkLabel2 = new LinkLabel();
      this.SuspendLayout();
      this.progressBar1.Location = new Point(9, 43);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(432, 23);
      this.progressBar1.TabIndex = 0;
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.LinkArea = new LinkArea(5, 6);
      this.linkLabel1.Location = new Point(13, 102);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(153, 21);
      this.linkLabel1.TabIndex = 1;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "下载成功，立即完成更新。";
      this.linkLabel1.UseCompatibleTextRendering = true;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(448, 46);
      this.label1.Name = "label1";
      this.label1.Size = new Size(33, 17);
      this.label1.TabIndex = 2;
      this.label1.Text = "50%";
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.LinkArea = new LinkArea(6, 2);
      this.linkLabel2.Location = new Point(13, (int) sbyte.MaxValue);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new Size(116, 21);
      this.linkLabel2.TabIndex = 3;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "下载失败，请重试。";
      this.linkLabel2.UseCompatibleTextRendering = true;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(497, 178);
      this.Controls.Add((Control) this.linkLabel2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.progressBar1);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Margin = new Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.Name = nameof (FormUpdater);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (FormUpdater);
      this.Load += new EventHandler(this.FormUpdater_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
