// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormInfo
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
  public class FormInfo : Form, IDestroy
  {
    private OEMType mOemType;
    private IContainer components;
    private Button buttonWeb;
    private Button buttonLOGO;
    private Label labelCompany;
    private Label labelVersion;
    private Label labelRight;
    private Button buttonExport;
    private Button buttonImport;
    private Button buttonDefault;
    private Label HuionTypeLabel;
    private Panel panelButton;
    private Panel panelInfo;
    private Panel panel1;

    public FormInfo(OEMType oemType)
    {
      this.InitializeComponent();
      this.mOemType = oemType;
      if (this.mOemType != OEMType.HUION)
        this.labelRight.AutoSize = false;
      this.setViewText8Locale();
      this.buttonExport.Click += new EventHandler(Fixer4Info.exportConfigClick);
      this.buttonImport.Click += new EventHandler(Fixer4Info.importConfigClick);
      this.buttonDefault.Click += new EventHandler(Fixer4Info.defaultConfigClick);
      this.buttonLOGO.Click += new EventHandler(Fixer4Info.logoConfigClick);
      this.buttonWeb.Click += new EventHandler(Fixer4Info.logoConfigClick);
    }

    private void FormInfo_Load(object sender, EventArgs e)
    {
      this.labelRight.AutoSize = false;
      this.panelInfo.Width = this.Width;
      this.buttonDefault.Parent = (Control) this.panelButton;
      this.buttonExport.Parent = (Control) this.panelButton;
      this.buttonImport.Parent = (Control) this.panelButton;
      this.labelCompany.Parent = (Control) this.panelInfo;
      this.labelRight.Parent = (Control) this.panel1;
      this.labelVersion.Parent = (Control) this.panelInfo;
      this.HuionTypeLabel.Parent = (Control) this.panelInfo;
      this.HuionTypeLabel.Text = "";
      this.SetAllControlImage();
      this.updateStatus();
      KeyboardUtils.sendMsgEvent += new HuionTablet.utils.Void(this.HuionTalbet_sendMsgEvent);
      KeyboardUtils.sendHuionTypeEvent += new HuionTablet.utils.Void(this.HuionTalbet_sendHuionTypeEvent);
    }

    public void updateStatus()
    {
      this.buttonExport.Enabled = HNStruct.globalInfo.bOpenedTablet;
      this.buttonImport.Enabled = HNStruct.globalInfo.bOpenedTablet;
      this.buttonDefault.Enabled = HNStruct.globalInfo.bOpenedTablet;
    }

    private void HuionTalbet_sendMsgEvent()
    {
      this.labelCompany.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
      this.labelCompany.Text = "Shenzhen Huion Animation Technology Co., Ltd.";
    }

    private void HuionTalbet_sendHuionTypeEvent()
    {
      this.HuionTypeLabel.Text = HNStruct.globalInfo.tabletInfo.getFirmwareVersion();
    }

    private void SetAllControlImage()
    {
      Image image;
      if (this.mOemType == OEMType.HUION)
      {
        image = (Image) HuionRender.blowupImage(ImageHelper.getDllImage("logo.png", OEMType.HUION), DpiHelper.getInstance().XDpi);
        this.buttonWeb.BackgroundImage = (Image) ImageHelper.getDllImage("BANNER.jpg", OEMType.HUION);
        this.buttonLOGO.Size = image.Size;
        int x = this.panelButton.Right / 2 + (this.Width - this.buttonLOGO.Width) / 2;
        if (x < 0)
          x = 0;
        this.buttonLOGO.Location = new Point(x, this.panelButton.Bottom - this.buttonLOGO.Height);
      }
      else
      {
        image = (Image) HuionRender.blowupImage(ImageHelper.getDllImage("logo.png", this.mOemType), DpiHelper.getInstance().XDpi);
        this.buttonWeb.Visible = false;
        this.buttonLOGO.SetBounds((this.Width - image.Width) / 2, 100, image.Width, image.Height);
      }
      this.buttonLOGO.BackgroundImage = image;
    }

    private void setViewText8Locale()
    {
      if (this.mOemType == OEMType.GAOMON)
        this.labelCompany.Text = ResourceCulture.GetString("CompanyName4Gaomon");
      else if (this.mOemType == OEMType.HUION)
        this.labelCompany.Text = ResourceCulture.GetString("FormInfo_lbCommpanyText");
      else
        this.labelCompany.Text = "Company Name";
      this.labelVersion.Text = ResourceCulture.GetString("FormInfo_lbVersionText") + "v14.5.0";
      this.labelRight.Text = ResourceCulture.GetString("FormInfo_lbReservedText") + "2011-2018";
      this.buttonExport.Text = ResourceCulture.GetString("FormInfo_btEmportText");
      this.buttonImport.Text = ResourceCulture.GetString("FormInfo_btImportText");
      this.buttonDefault.Text = ResourceCulture.GetString("FormInfo_btDefaultText");
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 512)
        TimerSession.userOperation();
      base.WndProc(ref m);
    }

    public void onDestroy()
    {
      KeyboardUtils.sendMsgEvent -= new HuionTablet.utils.Void(this.HuionTalbet_sendMsgEvent);
      KeyboardUtils.sendHuionTypeEvent -= new HuionTablet.utils.Void(this.HuionTalbet_sendHuionTypeEvent);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.buttonLOGO = new Button();
      this.buttonWeb = new Button();
      this.labelCompany = new Label();
      this.labelVersion = new Label();
      this.labelRight = new Label();
      this.buttonExport = new Button();
      this.buttonImport = new Button();
      this.buttonDefault = new Button();
      this.HuionTypeLabel = new Label();
      this.panelButton = new Panel();
      this.panelInfo = new Panel();
      this.panel1 = new Panel();
      this.panelButton.SuspendLayout();
      this.panelInfo.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.buttonLOGO.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonLOGO.BackColor = SystemColors.Control;
      this.buttonLOGO.BackgroundImageLayout = ImageLayout.Zoom;
      this.buttonLOGO.Cursor = Cursors.Hand;
      this.buttonLOGO.FlatAppearance.BorderSize = 0;
      this.buttonLOGO.FlatStyle = FlatStyle.Flat;
      this.buttonLOGO.Location = new Point(747, 467);
      this.buttonLOGO.Margin = new Padding(3, 4, 3, 4);
      this.buttonLOGO.Name = "buttonLOGO";
      this.buttonLOGO.Size = new Size(70, 61);
      this.buttonLOGO.TabIndex = 0;
      this.buttonLOGO.TabStop = false;
      this.buttonLOGO.UseVisualStyleBackColor = false;
      this.buttonWeb.BackgroundImageLayout = ImageLayout.Stretch;
      this.buttonWeb.Cursor = Cursors.Hand;
      this.buttonWeb.FlatAppearance.BorderSize = 0;
      this.buttonWeb.FlatStyle = FlatStyle.Flat;
      this.buttonWeb.Location = new Point(0, 32);
      this.buttonWeb.Margin = new Padding(3, 4, 3, 4);
      this.buttonWeb.Name = "buttonWeb";
      this.buttonWeb.Size = new Size(840, 236);
      this.buttonWeb.TabIndex = 0;
      this.buttonWeb.TabStop = false;
      this.buttonWeb.UseVisualStyleBackColor = true;
      this.labelCompany.AutoSize = true;
      this.labelCompany.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelCompany.Location = new Point(0, 58);
      this.labelCompany.Name = "labelCompany";
      this.labelCompany.Size = new Size(278, 17);
      this.labelCompany.TabIndex = 0;
      this.labelCompany.Text = "Shenzhen Huion Animation Technology Co.,Ltd";
      this.labelVersion.AutoSize = true;
      this.labelVersion.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelVersion.Location = new Point(0, 28);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new Size(189, 17);
      this.labelVersion.TabIndex = 0;
      this.labelVersion.Text = "Version Number:v13.3.0151010";
      this.labelRight.Dock = DockStyle.Fill;
      this.labelRight.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelRight.Location = new Point(0, 0);
      this.labelRight.Name = "labelRight";
      this.labelRight.Size = new Size(209, 49);
      this.labelRight.TabIndex = 0;
      this.labelRight.Text = "All Rights Reserved 2011-2016";
      this.buttonExport.AutoEllipsis = true;
      this.buttonExport.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonExport.FlatStyle = FlatStyle.Flat;
      this.buttonExport.Location = new Point(4, 9);
      this.buttonExport.Margin = new Padding(3, 4, 3, 4);
      this.buttonExport.Name = "buttonExport";
      this.buttonExport.Size = new Size(165, 27);
      this.buttonExport.TabIndex = 1;
      this.buttonExport.Text = "Export Config";
      this.buttonExport.UseVisualStyleBackColor = true;
      this.buttonImport.AutoEllipsis = true;
      this.buttonImport.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonImport.FlatStyle = FlatStyle.Flat;
      this.buttonImport.Location = new Point(172, 9);
      this.buttonImport.Margin = new Padding(3, 4, 3, 4);
      this.buttonImport.Name = "buttonImport";
      this.buttonImport.Size = new Size(165, 27);
      this.buttonImport.TabIndex = 2;
      this.buttonImport.Text = "Import Config";
      this.buttonImport.UseVisualStyleBackColor = true;
      this.buttonDefault.AutoEllipsis = true;
      this.buttonDefault.FlatAppearance.BorderColor = Color.DarkGray;
      this.buttonDefault.FlatStyle = FlatStyle.Flat;
      this.buttonDefault.Location = new Point(340, 9);
      this.buttonDefault.Margin = new Padding(3, 4, 3, 4);
      this.buttonDefault.Name = "buttonDefault";
      this.buttonDefault.Size = new Size(165, 27);
      this.buttonDefault.TabIndex = 3;
      this.buttonDefault.Text = "Default Config";
      this.buttonDefault.UseVisualStyleBackColor = true;
      this.HuionTypeLabel.AutoSize = true;
      this.HuionTypeLabel.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.HuionTypeLabel.Location = new Point(404, 58);
      this.HuionTypeLabel.Name = "HuionTypeLabel";
      this.HuionTypeLabel.Size = new Size(15, 17);
      this.HuionTypeLabel.TabIndex = 0;
      this.HuionTypeLabel.Text = "1";
      this.panelButton.BackColor = Color.Transparent;
      this.panelButton.Controls.Add((Control) this.buttonExport);
      this.panelButton.Controls.Add((Control) this.buttonDefault);
      this.panelButton.Controls.Add((Control) this.buttonImport);
      this.panelButton.Location = new Point(228, 505);
      this.panelButton.Name = "panelButton";
      this.panelButton.Size = new Size(509, 36);
      this.panelButton.TabIndex = 4;
      this.panelInfo.BackColor = Color.Transparent;
      this.panelInfo.Controls.Add((Control) this.HuionTypeLabel);
      this.panelInfo.Controls.Add((Control) this.labelVersion);
      this.panelInfo.Controls.Add((Control) this.labelCompany);
      this.panelInfo.Location = new Point(17, 418);
      this.panelInfo.Name = "panelInfo";
      this.panelInfo.Size = new Size(712, 81);
      this.panelInfo.TabIndex = 5;
      this.panel1.Controls.Add((Control) this.labelRight);
      this.panel1.Location = new Point(17, 505);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(209, 49);
      this.panel1.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(840, 566);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.buttonLOGO);
      this.Controls.Add((Control) this.buttonWeb);
      this.Controls.Add((Control) this.panelButton);
      this.Controls.Add((Control) this.panelInfo);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Margin = new Padding(3, 4, 3, 4);
      this.Name = nameof (FormInfo);
      this.ShowIcon = false;
      this.Text = nameof (FormInfo);
      this.Load += new EventHandler(this.FormInfo_Load);
      this.panelButton.ResumeLayout(false);
      this.panelInfo.ResumeLayout(false);
      this.panelInfo.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
