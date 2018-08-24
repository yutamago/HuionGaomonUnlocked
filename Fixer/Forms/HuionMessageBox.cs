// Decompiled with JetBrains decompiler
// Type: HuionTablet.HuionMessageBox
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace HuionTablet
{
  public class HuionMessageBox : BaseForm
  {
    private IContainer components;
    private Panel panelContent;
    private Button btnOK;
    private Label labelText;
    private Panel panel1;

    public static void HotkeyConflict()
    {
      HuionMessageBox huionMessageBox = new HuionMessageBox();
      huionMessageBox.HintText = ResourceCulture.GetString("SettingsHotkeyRegistered");
      huionMessageBox.btnOK.Click += new EventHandler(HuionMessageBox.HotkeyConflictOKClick);
      huionMessageBox.btnOK.Text = ResourceCulture.GetString("ChangeNow");
      huionMessageBox.Show();
    }

    private static void HotkeyConflictOKClick(object sender, EventArgs e)
    {
      ((Control) sender).FindForm().Close();
      FormSettings.showForm(Fixer4Main.MainForm.Handle, true, 1);
    }

    public static void UpdateOnline()
    {
      if (HuionMessageBox.compareVersion())
        return;
      HuionMessageBox huionMessageBox = new HuionMessageBox();
      huionMessageBox.HintText = "发现新版本，是否立即更新？";
      huionMessageBox.btnOK.Click += new EventHandler(HuionMessageBox.UpdateOnlineOKClick);
      huionMessageBox.btnOK.Text = "立即更新";
      huionMessageBox.Show();
    }

    private static string getHtml(string url)
    {
      Stream stream = new WebClient().OpenRead(url);
      string end = new StreamReader(stream, Encoding.GetEncoding("utf-8")).ReadToEnd();
      stream.Close();
      return end;
    }

    public static bool compareVersion()
    {
      bool flag = false;
      string html = HuionMessageBox.getHtml("http://driver.huion.com/win/Public/html/update.html");
      HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
      htmlDocument.LoadHtml(html);
      string strA = htmlDocument.GetElementbyId("LayuiWin").InnerText.Trim().Split(new char[1]{ '_' }, StringSplitOptions.RemoveEmptyEntries)[2];
      string strB = "HuionTablet_WinDriver_v14.7.4.exe".Split(new char[1]{ '_' }, StringSplitOptions.RemoveEmptyEntries)[2];
      Console.WriteLine(strA);
      Console.WriteLine(strB);
      if (string.Compare(strA, strB, true) > 0)
        flag = true;
      Console.WriteLine(flag.ToString());
      return flag;
    }

    private static int htmlDown(string url, string localFile)
    {
      if (System.IO.File.Exists(localFile))
        return 0;
      FileStream fileStream = new FileStream(localFile, FileMode.Create);
      try
      {
        Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
        byte[] buffer = new byte[1024];
        for (int count = responseStream.Read(buffer, 0, buffer.Length); count > 0; count = responseStream.Read(buffer, 0, buffer.Length))
          fileStream.Write(buffer, 0, count);
        fileStream.Close();
        responseStream.Close();
        return 1;
      }
      catch (Exception ex)
      {
        fileStream.Close();
        return 2;
      }
    }

    private static void UpdateOnlineOKClick(object sender, EventArgs e)
    {
      ((Control) sender).FindForm().Close();
      HuionMessageBox.changeForm();
    }

    public static int download
    {
      get
      {
        return HuionMessageBox.htmlDown("http://driver.huion.com/win/Public/driver/HUION_TabletDriverInstaller_v14.3.5.180524.exe", "d:\\HUION_TabletDriverInstaller_v14.3.5.180524.exe");
      }
    }

    public static void changeForm()
    {
      switch (HuionMessageBox.download)
      {
        case 0:
          FormUpdateMessage.downloaded();
          break;
        case 1:
          FormUpdateMessage.downloadScessce();
          break;
        case 2:
          FormUpdateMessage.downloadFail();
          break;
      }
    }

    private static void UpdateNow()
    {
      HuionMessageBox huionMessageBox = new HuionMessageBox();
      huionMessageBox.HintText = "发现新版本，是否立即更新？";
      huionMessageBox.btnOK.Click += new EventHandler(HuionMessageBox.UpdateOnlineOKClick);
      huionMessageBox.btnOK.Text = "立即完成更新";
      huionMessageBox.Show();
    }

    private static void UpdateNowOKClick(object sender, EventArgs e)
    {
      ((Control) sender).FindForm().Close();
    }

    public string HintText
    {
      get
      {
        return this.labelText.Text;
      }
      set
      {
        this.labelText.Text = value;
      }
    }

    public HuionMessageBox()
    {
      this.InitializeComponent();
      this.ShowSettingsIcon = false;
      this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
      this.Text = Fixer4Main.FormTitle();
      this.CenterToScreen();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panelContent = new Panel();
      this.labelText = new Label();
      this.btnOK = new Button();
      this.panel1 = new Panel();
      this.panelContent.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.panelContent.BackColor = Color.Transparent;
      this.panelContent.Controls.Add((Control) this.panel1);
      this.panelContent.Controls.Add((Control) this.btnOK);
      this.panelContent.Dock = DockStyle.Fill;
      this.panelContent.Location = new Point(1, 1);
      this.panelContent.Name = "panelContent";
      this.panelContent.Padding = new Padding(10);
      this.panelContent.Size = new Size(368, 151);
      this.panelContent.TabIndex = 0;
      this.labelText.Dock = DockStyle.Fill;
      this.labelText.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelText.Location = new Point(0, 0);
      this.labelText.Name = "labelText";
      this.labelText.Size = new Size(200, 100);
      this.labelText.TabIndex = 0;
      this.labelText.Text = "label1";
      this.btnOK.AutoEllipsis = true;
      this.btnOK.Location = new Point((int) byte.MaxValue, 65);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 27);
      this.btnOK.TabIndex = 1;
      this.btnOK.UseVisualStyleBackColor = true;
      this.panel1.Controls.Add((Control) this.labelText);
      this.panel1.Location = new Point(13, 38);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(200, 100);
      this.panel1.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(370, 153);
      this.Controls.Add((Control) this.panelContent);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (HuionMessageBox);
      this.ShowSettingsIcon = true;
      this.Text = nameof (HuionMessageBox);
      this.panelContent.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
