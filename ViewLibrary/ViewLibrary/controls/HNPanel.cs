// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.HNPanel
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet.view
{
  public class HNPanel : Panel
  {
    private IContainer components;
    private Label labelText;

    public HNPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public virtual Color TextColor
    {
      get
      {
        return this.labelText.ForeColor;
      }
      set
      {
        this.labelText.ForeColor = value;
      }
    }

    public override string Text
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

    public virtual int TextWidth
    {
      get
      {
        return this.labelText.Width;
      }
      set
      {
        this.labelText.Width = value;
      }
    }

    public virtual int TextHeight
    {
      get
      {
        return this.labelText.Height;
      }
      set
      {
        this.labelText.Height = value;
      }
    }

    public virtual DockStyle TextDock
    {
      get
      {
        return this.labelText.Dock;
      }
      set
      {
        this.labelText.Dock = value;
      }
    }

    public virtual ContentAlignment TextAlign
    {
      get
      {
        return this.labelText.TextAlign;
      }
      set
      {
        this.labelText.TextAlign = value;
      }
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
    }

    private void labelText_Click(object sender, EventArgs e)
    {
      this.OnClick(e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.labelText = new Label();
      this.SuspendLayout();
      this.labelText.BackColor = Color.Transparent;
      this.labelText.Dock = DockStyle.Fill;
      this.labelText.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.labelText.Location = new Point(0, 0);
      this.labelText.Name = "labelText";
      this.labelText.Size = new Size(200, 100);
      this.labelText.TabIndex = 0;
      this.labelText.TextAlign = ContentAlignment.MiddleCenter;
      this.labelText.Click += new EventHandler(this.labelText_Click);
      this.Controls.Add((Control) this.labelText);
      this.ResumeLayout(false);
    }
  }
}
