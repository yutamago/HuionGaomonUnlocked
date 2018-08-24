// Decompiled with JetBrains decompiler
// Type: Huion.TitleBar
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using Huion.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Huion
{
  public class TitleBar : UserControl
  {
    private string curPath = Environment.CurrentDirectory;
    private Bitmap mIcon;
    private Point mDownPoint;
    public EventHandler OnSettingsButtonClick;
    private IContainer components;
    private PictureBox btnClose;
    private PictureBox btnMinimun;
    private PictureBox btnSettings;
    private PictureBox btnIcon;
    private Label labelTitle;
    private Panel panelIcon;

    public TitleBar()
    {
      this.InitializeComponent();
    }

    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        this.labelTitle.Text = this.Text;
      }
    }

    public bool IsActived
    {
      set
      {
        this.labelTitle.Enabled = value;
      }
    }

    public bool ShowSettingsIcon
    {
      get
      {
        return this.btnSettings.Visible;
      }
      set
      {
        this.btnSettings.Visible = value;
      }
    }

    public Image SettingsIcon
    {
      get
      {
        return this.btnSettings.Image;
      }
      set
      {
        this.btnSettings.Image = value;
      }
    }

    public bool ShowMinimizeBox
    {
      get
      {
        return this.btnMinimun.Visible;
      }
      set
      {
        this.btnMinimun.Visible = value;
      }
    }

    public Bitmap Icon
    {
      get
      {
        return this.mIcon;
      }
      set
      {
        this.mIcon = value;
        this.btnIcon.Image = (Image) this.mIcon;
      }
    }

    private void btn_OnClick(object sender, EventArgs e)
    {
      if (sender == this.btnMinimun)
        this.FindForm().WindowState = FormWindowState.Minimized;
      else if (sender == this.btnClose)
      {
        this.FindForm().Close();
      }
      else
      {
        if (sender != this.btnSettings || this.OnSettingsButtonClick == null)
          return;
        this.OnSettingsButtonClick(sender, e);
      }
    }

    private void btn_OnMouseDown(object sender, MouseEventArgs e)
    {
      if (sender == this.btnClose)
        this.btnClose.BackColor = Color.Pink;
      else
        ((Control) sender).BackColor = Color.Gray;
    }

    private void btn_OnMouseUp(object sender, MouseEventArgs e)
    {
      ((Control) sender).BackColor = Color.Transparent;
    }

    private void btn_OnMouseEnter(object sender, EventArgs e)
    {
      if (sender == this.btnClose)
        this.btnClose.BackColor = Color.Red;
      else
        ((Control) sender).BackColor = Color.LightGray;
    }

    private void btn_OnMouseLeave(object sender, EventArgs e)
    {
      ((Control) sender).BackColor = Color.Transparent;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.mDownPoint = e.Location;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (e.Button != MouseButtons.Left)
        return;
      Form form = this.FindForm();
      form.Location = new Point(form.Location.X + e.X - this.mDownPoint.X, form.Location.Y + e.Y - this.mDownPoint.Y);
    }

    private void labelTitle_MouseDown(object sender, MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    private void labelTitle_MouseMove(object sender, MouseEventArgs e)
    {
      this.OnMouseMove(e);
    }

    private void btn_OnMouseHover(object sender, EventArgs e)
    {
      if (sender == this.btnClose)
        this.btnClose.BackColor = Color.Red;
      else
        ((Control) sender).BackColor = Color.LightGray;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TitleBar));
      this.labelTitle = new Label();
      this.panelIcon = new Panel();
      this.btnIcon = new PictureBox();
      this.btnSettings = new PictureBox();
      this.btnMinimun = new PictureBox();
      this.btnClose = new PictureBox();
      this.panelIcon.SuspendLayout();
      ((ISupportInitialize) this.btnIcon).BeginInit();
      ((ISupportInitialize) this.btnSettings).BeginInit();
      ((ISupportInitialize) this.btnMinimun).BeginInit();
      ((ISupportInitialize) this.btnClose).BeginInit();
      this.SuspendLayout();
      this.labelTitle.Dock = DockStyle.Fill;
      this.labelTitle.Location = new Point(45, 0);
      this.labelTitle.Margin = new Padding(1, 0, 3, 0);
      this.labelTitle.Name = "labelTitle";
      this.labelTitle.Size = new Size(452, 91);
      this.labelTitle.TabIndex = 5;
      this.labelTitle.TextAlign = ContentAlignment.MiddleLeft;
      this.labelTitle.MouseDown += new MouseEventHandler(this.labelTitle_MouseDown);
      this.labelTitle.MouseMove += new MouseEventHandler(this.labelTitle_MouseMove);
      this.panelIcon.Controls.Add((Control) this.btnIcon);
      this.panelIcon.Dock = DockStyle.Left;
      this.panelIcon.Enabled = false;
      this.panelIcon.Location = new Point(0, 0);
      this.panelIcon.Name = "panelIcon";
      this.panelIcon.Padding = new Padding(5);
      this.panelIcon.Size = new Size(45, 91);
      this.panelIcon.TabIndex = 6;
      this.btnIcon.BackColor = Color.Transparent;
      this.btnIcon.Dock = DockStyle.Fill;
      this.btnIcon.Enabled = false;
      this.btnIcon.Location = new Point(5, 5);
      this.btnIcon.Margin = new Padding(0);
      this.btnIcon.Name = "btnIcon";
      this.btnIcon.Size = new Size(35, 81);
      this.btnIcon.SizeMode = PictureBoxSizeMode.Zoom;
      this.btnIcon.TabIndex = 4;
      this.btnIcon.TabStop = false;
      this.btnSettings.BackColor = Color.Transparent;
      this.btnSettings.Dock = DockStyle.Right;
      this.btnSettings.ErrorImage = (Image) Resources.settings;
      this.btnSettings.Image = (Image) Resources.settings;
      this.btnSettings.InitialImage = (Image) Resources.settings;
      this.btnSettings.Location = new Point(497, 0);
      this.btnSettings.Margin = new Padding(3, 4, 3, 4);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Size = new Size(34, 91);
      this.btnSettings.SizeMode = PictureBoxSizeMode.Zoom;
      this.btnSettings.TabIndex = 3;
      this.btnSettings.TabStop = false;
      this.btnSettings.Click += new EventHandler(this.btn_OnClick);
      this.btnSettings.MouseDown += new MouseEventHandler(this.btn_OnMouseDown);
      this.btnSettings.MouseEnter += new EventHandler(this.btn_OnMouseEnter);
      this.btnSettings.MouseLeave += new EventHandler(this.btn_OnMouseLeave);
      this.btnSettings.MouseUp += new MouseEventHandler(this.btn_OnMouseUp);
      this.btnMinimun.BackColor = Color.Transparent;
      this.btnMinimun.Dock = DockStyle.Right;
      this.btnMinimun.Image = (Image) componentResourceManager.GetObject("btnMinimun.Image");
      this.btnMinimun.Location = new Point(531, 0);
      this.btnMinimun.Margin = new Padding(3, 4, 4, 4);
      this.btnMinimun.Name = "btnMinimun";
      this.btnMinimun.Padding = new Padding(11, 11, 0, 0);
      this.btnMinimun.Size = new Size(31, 91);
      this.btnMinimun.SizeMode = PictureBoxSizeMode.Zoom;
      this.btnMinimun.TabIndex = 2;
      this.btnMinimun.TabStop = false;
      this.btnMinimun.Click += new EventHandler(this.btn_OnClick);
      this.btnMinimun.MouseDown += new MouseEventHandler(this.btn_OnMouseDown);
      this.btnMinimun.MouseEnter += new EventHandler(this.btn_OnMouseEnter);
      this.btnMinimun.MouseLeave += new EventHandler(this.btn_OnMouseLeave);
      this.btnMinimun.MouseHover += new EventHandler(this.btn_OnMouseHover);
      this.btnMinimun.MouseUp += new MouseEventHandler(this.btn_OnMouseUp);
      this.btnClose.BackColor = Color.Transparent;
      this.btnClose.Dock = DockStyle.Right;
      this.btnClose.Image = (Image) componentResourceManager.GetObject("btnClose.Image");
      this.btnClose.Location = new Point(562, 0);
      this.btnClose.Margin = new Padding(3, 4, 3, 4);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(34, 91);
      this.btnClose.SizeMode = PictureBoxSizeMode.Zoom;
      this.btnClose.TabIndex = 0;
      this.btnClose.TabStop = false;
      this.btnClose.Click += new EventHandler(this.btn_OnClick);
      this.btnClose.MouseDown += new MouseEventHandler(this.btn_OnMouseDown);
      this.btnClose.MouseEnter += new EventHandler(this.btn_OnMouseEnter);
      this.btnClose.MouseLeave += new EventHandler(this.btn_OnMouseLeave);
      this.btnClose.MouseHover += new EventHandler(this.btn_OnMouseHover);
      this.btnClose.MouseUp += new MouseEventHandler(this.btn_OnMouseUp);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.labelTitle);
      this.Controls.Add((Control) this.panelIcon);
      this.Controls.Add((Control) this.btnSettings);
      this.Controls.Add((Control) this.btnMinimun);
      this.Controls.Add((Control) this.btnClose);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.Margin = new Padding(3, 4, 3, 4);
      this.Name = nameof (TitleBar);
      this.Size = new Size(596, 91);
      this.panelIcon.ResumeLayout(false);
      ((ISupportInitialize) this.btnIcon).EndInit();
      ((ISupportInitialize) this.btnSettings).EndInit();
      ((ISupportInitialize) this.btnMinimun).EndInit();
      ((ISupportInitialize) this.btnClose).EndInit();
      this.ResumeLayout(false);
    }
  }
}
