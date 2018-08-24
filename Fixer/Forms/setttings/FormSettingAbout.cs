// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormSettingAbout
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using Huion;
using HuionTablet.Lib;
using HuionTablet.utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet
{
  public class FormSettingAbout : Form
  {
    private const int INTERNET_CONNRCTION_MODEM = 1;
    private const int INTERNET_CONNECTION_LAN = 2;
    private const int INTERNET_CONNECTION_PROXY = 3;
    private IContainer components;
    private Label labelCurrentVersion;
    private Button btnCheckVersion;
    private LinkLabel linkLabel1;
    private Label Versionlabel;
    private Panel panel1;
    private ToolTip toolTip1;

    [DllImport("wininet.dll")]
    public static extern bool InternetGetConnectedState(ref int dwFlag, int duReserved);

    public FormSettingAbout()
    {
      this.InitializeComponent();
      this.labelCurrentVersion.Text = "v14.5.0";
      this.Versionlabel.Text = ResourceCulture.GetString("FormInfo_lbVersionText");
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 512)
        return;
      TimerSession.userOperation();
    }

    private void FormSettingAbout_Load(object sender, EventArgs e)
    {
      this.linkLabel1.Visible = false;
      this.labelCurrentVersion.Text = "v14.5.0";
      this.btnCheckVersion.AutoEllipsis = true;
      this.btnCheckVersion.Text = ResourceCulture.GetString("Check_Update");
    }

    private void btnCheckVersion_Click(object sender, EventArgs e)
    {
      new Thread(new ThreadStart(BaseForm.myThread)).Start();
      LinkArea linkArea = new LinkArea(0, 0);
      this.linkLabel1.Text = ResourceCulture.GetString("Checking_Update");
      this.linkLabel1.Visible = true;
      this.linkLabel1.LinkArea = linkArea;
      this.btnCheckVersion.Visible = false;
      HuionDelayRun.delayRun(new Runnable(this.onCheck), 2000, (object) null);
    }

    private void onCheck(object t)
    {
      this.Invoke((Delegate) new Runnable(this.onCheckCompleted), t);
    }

    public void onCheckCompleted(object t)
    {
      this.btnCheckVersion.Visible = false;
      try
      {
        if (BaseForm.IsUpdate)
        {
          this.linkLabel1.Text = ResourceCulture.GetString("Find_NewVersion");
          LinkArea linkArea = new LinkArea(0, this.linkLabel1.Text.Length);
          this.linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
          this.linkLabel1.LinkColor = HuionConst.HuionBlue4;
          this.linkLabel1.LinkArea = linkArea;
          this.linkLabel1.Click += new EventHandler(this.LinkLabel1_Click);
        }
        else
          this.linkLabel1.Text = ResourceCulture.GetString("Already_Updated");
      }
      catch
      {
        int num = (int) MessageBox.Show(ResourceCulture.GetString("Net_Busy"), ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void LinkLabel1_Click(object sender, EventArgs e)
    {
      string str1 = "\\Release\\";
      string currentDirectory = Environment.CurrentDirectory;
      Console.WriteLine(currentDirectory);
      string str2 = "updateFormApp.exe";
      Console.WriteLine(currentDirectory + str1);
      int num1 = FormSettingAbout.IsProcrssRunning("updateFormApp");
      FormSettingAbout.localConnectStatus();
      try
      {
        if (num1 != 0)
        {
          int num2 = (int) MessageBox.Show(ResourceCulture.GetString("Cannot_openAgain"), ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
          FormSettingAbout.procrssStart(currentDirectory + str1 + str2, (string) null);
      }
      catch (Exception ex)
      {
      }
    }

    private static bool localConnectStatus()
    {
      int dwFlag = 0;
      try
      {
        return FormSettingAbout.InternetGetConnectedState(ref dwFlag, 0) && ((dwFlag & 1) != 0 || (dwFlag & 2) != 0 || (dwFlag & 3) != 0);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ResourceCulture.GetString("Net_Busy"), ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        HuionLog.saveLog(nameof (localConnectStatus), ex.Message);
        return false;
      }
    }

    private static int IsProcrssRunning(string processName)
    {
      Process[] processesByName = Process.GetProcessesByName(processName);
      int index = 0;
      if (index < processesByName.Length)
        return processesByName[index].Id;
      return 0;
    }

    public static Process procrssStart(string filename, string command)
    {
      try
      {
        Process process = new Process();
        process.StartInfo.FileName = filename;
        process.StartInfo.Arguments = command;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = false;
        process.Start();
        return process;
      }
      catch (Exception ex)
      {
        throw ex;
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
      this.components = (IContainer) new Container();
      this.labelCurrentVersion = new Label();
      this.btnCheckVersion = new Button();
      this.linkLabel1 = new LinkLabel();
      this.Versionlabel = new Label();
      this.panel1 = new Panel();
      this.toolTip1 = new ToolTip(this.components);
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.labelCurrentVersion.AutoSize = true;
      this.labelCurrentVersion.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelCurrentVersion.Location = new Point(172, 23);
      this.labelCurrentVersion.Name = "labelCurrentVersion";
      this.labelCurrentVersion.Size = new Size(95, 17);
      this.labelCurrentVersion.TabIndex = 1;
      this.labelCurrentVersion.Text = "V14.2.1.180314";
      this.btnCheckVersion.Location = new Point(9, 0);
      this.btnCheckVersion.Name = "btnCheckVersion";
      this.btnCheckVersion.Size = new Size(111, 28);
      this.btnCheckVersion.TabIndex = 3;
      this.btnCheckVersion.Text = "button1";
      this.btnCheckVersion.UseVisualStyleBackColor = true;
      this.btnCheckVersion.Click += new EventHandler(this.btnCheckVersion_Click);
      this.linkLabel1.BackColor = Color.Transparent;
      this.linkLabel1.Dock = DockStyle.Fill;
      this.linkLabel1.Location = new Point(0, 0);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(222, 100);
      this.linkLabel1.TabIndex = 6;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "linkLabel1";
      this.Versionlabel.AutoEllipsis = true;
      this.Versionlabel.AutoSize = true;
      this.Versionlabel.Location = new Point(28, 23);
      this.Versionlabel.Name = "Versionlabel";
      this.Versionlabel.Size = new Size(43, 17);
      this.Versionlabel.TabIndex = 7;
      this.Versionlabel.Text = "label1";
      this.panel1.Controls.Add((Control) this.linkLabel1);
      this.panel1.Controls.Add((Control) this.btnCheckVersion);
      this.panel1.Location = new Point(166, 73);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(222, 100);
      this.panel1.TabIndex = 8;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(400, 249);
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.Versionlabel);
      this.Controls.Add((Control) this.labelCurrentVersion);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Margin = new Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormSettingAbout);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (FormSettingAbout);
      this.Load += new EventHandler(this.FormSettingAbout_Load);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public delegate void localNetStatus();
  }
}
