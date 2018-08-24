// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormUpdateMessage
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormUpdateMessage : Form
  {
    private static FormUpdateMessage formUpdateMessage = new FormUpdateMessage();
    private IContainer components;
    private Button button1;
    private Label label1;

    public static void showForm()
    {
      FormUpdateMessage.showForm((string) null);
    }

    public static void showForm(string url)
    {
      if (FormUpdateMessage.formUpdateMessage.IsDisposed)
        FormUpdateMessage.formUpdateMessage = new FormUpdateMessage();
      FormUpdateMessage.formUpdateMessage.Show();
      FormUpdateMessage.formUpdateMessage.Focus();
    }

    public static void downloadScessce()
    {
      FormUpdateMessage formUpdateMessage = new FormUpdateMessage();
      formUpdateMessage.label1.Text = ResourceCulture.GetString("waitForUpdate");
      formUpdateMessage.button1.Text = ResourceCulture.GetString("installNow");
      formUpdateMessage.button1.Click += new EventHandler(FormUpdateMessage.buttonClick1);
      formUpdateMessage.Show();
    }

    public static void downloadFail()
    {
      FormUpdateMessage formUpdateMessage = new FormUpdateMessage();
      formUpdateMessage.label1.Text = ResourceCulture.GetString("failDownload");
      formUpdateMessage.button1.Text = ResourceCulture.GetString("downloadAgain");
      formUpdateMessage.button1.Click += new EventHandler(FormUpdateMessage.buttonClick2);
      formUpdateMessage.Show();
    }

    public static void downloaded()
    {
      FormUpdateMessage formUpdateMessage = new FormUpdateMessage();
      formUpdateMessage.label1.Text = "您已下载！";
      formUpdateMessage.button1.Text = "立即更新";
      formUpdateMessage.button1.Click += new EventHandler(FormUpdateMessage.buttonClick1);
      formUpdateMessage.Show();
    }

    public FormUpdateMessage()
    {
      this.InitializeComponent();
      this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
      this.Text = Fixer4Main.FormTitle();
      this.CenterToScreen();
    }

    private void FormUpdateMessage_Load(object sender, EventArgs e)
    {
    }

    public static void openFile(string file)
    {
      Process.Start(file);
    }

    private static void buttonClick1(object sender, EventArgs e)
    {
      FormUpdateMessage.openFile("D:\\");
    }

    private static void buttonClick2(object sender, EventArgs e)
    {
      ((Control) sender).FindForm().Close();
      HuionMessageBox.changeForm();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.button1 = new Button();
      this.label1 = new Label();
      this.SuspendLayout();
      this.button1.Location = new Point(213, 79);
      this.button1.Name = "button1";
      this.button1.Size = new Size(109, 34);
      this.button1.TabIndex = 1;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(78, 42);
      this.label1.Name = "label1";
      this.label1.Size = new Size(41, 12);
      this.label1.TabIndex = 2;
      this.label1.Text = "label1";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(371, 140);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button1);
      this.Name = nameof (FormUpdateMessage);
      this.Text = nameof (FormUpdateMessage);
      this.Load += new EventHandler(this.FormUpdateMessage_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
