// Decompiled with JetBrains decompiler
// Type: HuionTablet.Forms.keyDisplay
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet.Forms
{
  public class keyDisplay : Form
  {
    public static keyDisplay keydisplay;
    public static int keyDisplayText;
    private IContainer components;
    private System.Windows.Forms.Timer timer1;
    private Label label1;
    private Label label2;
    private Label label3;
    private System.Windows.Forms.Timer timer2;
    private System.Windows.Forms.Timer timer3;

    private static event keyDisplay.keyDisplayTextChanged keydisplaytextchanged;

    public keyDisplay()
    {
      this.InitializeComponent();
    }

    public static int keydisplayText
    {
      get
      {
        return keyDisplay.keyDisplayText;
      }
      set
      {
        if (keyDisplay.keyDisplayText != value)
          keyDisplay.WhenMyValueChange();
        keyDisplay.keyDisplayText = value;
      }
    }

    private static void WhenMyValueChange()
    {
      // ISSUE: reference to a compiler-generated field
      if (keyDisplay.keydisplaytextchanged == null)
        return;
      // ISSUE: reference to a compiler-generated field
      keyDisplay.keydisplaytextchanged();
    }

    public void setText(int text)
    {
      switch (text)
      {
        case 1:
          this.setStyle((Control) this.label1);
          this.setStyleDefault((Control) this.label2);
          this.setStyleDefault((Control) this.label3);
          break;
        case 2:
          this.setStyle((Control) this.label2);
          this.setStyleDefault((Control) this.label1);
          this.setStyleDefault((Control) this.label3);
          this.label3.Top = 117;
          break;
        case 3:
          this.setStyle((Control) this.label3);
          this.setStyleDefault((Control) this.label1);
          this.setStyleDefault((Control) this.label2);
          this.label3.Top = 105;
          break;
      }
    }

    public void setStyle(Control sender)
    {
      sender.Font = new Font("微软雅黑", 16f, FontStyle.Bold);
      sender.ForeColor = Color.FromArgb(34, 0, 162, 232);
    }

    public void setStyleDefault(Control sender)
    {
      sender.Font = new Font("微软雅黑", 12f, FontStyle.Regular);
      sender.ForeColor = Color.Transparent;
    }

    private void keyDisplay_Load(object sender, EventArgs e)
    {
      string name = Thread.CurrentThread.CurrentCulture.Name;
      this.label1.Text = ResourceCulture.GetString("Canvas_scaling");
      this.label2.Text = ResourceCulture.GetString("Brush_scaling");
      this.label3.Text = ResourceCulture.GetString("Rolling");
      int num1 = 187;
      using (Graphics graphics = this.CreateGraphics())
      {
        Font font = new Font("微软雅黑", 16f, FontStyle.Bold);
        float num2 = graphics.DpiX / 96f;
        SizeF sizeF1 = graphics.MeasureString(this.label1.Text, font);
        SizeF sizeF2 = graphics.MeasureString(this.label2.Text, font);
        SizeF sizeF3 = graphics.MeasureString(this.label3.Text, font);
        num1 = (int) Math.Max(sizeF1.Width * num2, Math.Max(sizeF2.Width * num2, sizeF3.Width * num2)) + 50;
      }
      this.Width = num1;
      int x = Screen.PrimaryScreen.WorkingArea.Size.Width / 2 - this.Width / 2;
      int y = Screen.PrimaryScreen.WorkingArea.Size.Height - 170;
      this.AutoScaleMode = AutoScaleMode.None;
      this.label1.Left = (this.Width - this.label1.Width) / 2;
      this.label2.Left = (this.Width - this.label2.Width) / 2;
      this.label3.Left = (this.Width - this.label3.Width) / 2;
      this.SetDesktopLocation(x, y);
      keyDisplay.keydisplaytextchanged += new keyDisplay.keyDisplayTextChanged(this.a);
      this.DrawRoundRect((Control) this, 2);
      this.timer1.Interval = 30;
      this.timer3.Interval = 1500;
      this.timer3.Enabled = true;
      this.timer3.Start();
      this.timer2.Interval = 1;
      this.timer2.Enabled = true;
      this.timer2.Start();
    }

    private void a()
    {
      if (this.InvokeRequired)
        this.Invoke((Delegate) new keyDisplay.aDelegeat(this.a));
      else if (this.timer3.Enabled)
      {
        this.timer3.Enabled = false;
        this.timer3.Stop();
        this.timer3.Enabled = true;
        this.timer3.Start();
      }
      Console.WriteLine("返回值发生变化");
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (this.Opacity > 0.1)
        this.Opacity -= 0.02;
      else if (this.Opacity < 0.0)
        this.timer1.Enabled = false;
      else
        this.Close();
    }

    private void keyDisplay_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.timer1.Dispose();
      this.timer2.Dispose();
      this.timer3.Dispose();
      // ISSUE: reference to a compiler-generated field
      keyDisplay.keydisplaytextchanged = (keyDisplay.keyDisplayTextChanged) null;
      keyDisplay.keydisplay = (keyDisplay) null;
    }

    private void DrawRoundRect(Control label, int t)
    {
      float width = (float) label.Width;
      float height = (float) label.Height;
      PointF[] points = new PointF[8]{ new PointF((float) t, 0.0f), new PointF(width - (float) t, 0.0f), new PointF(width, (float) t), new PointF(width, height - (float) t), new PointF(width - (float) t, height), new PointF((float) t, height), new PointF(0.0f, height - (float) t), new PointF(0.0f, (float) t) };
      GraphicsPath path = new GraphicsPath();
      path.AddLines(points);
      label.Region = new Region(path);
    }

    private void timer2_Tick(object sender, EventArgs e)
    {
      this.setText(keyDisplay.keyDisplayText);
      this.label1.Left = (this.Width - this.label1.Width) / 2;
      this.label2.Left = (this.Width - this.label2.Width) / 2;
      this.label3.Left = (this.Width - this.label3.Width) / 2;
      int top1 = this.label2.Top;
      int top2 = this.label1.Top;
      int height = this.label1.Height;
      this.timer3.Enabled = true;
      this.timer3.Start();
    }

    private void timer3_Tick_1(object sender, EventArgs e)
    {
      this.timer1.Enabled = true;
      this.timer1.Start();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (keyDisplay));
      this.label1 = new Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.label2 = new Label();
      this.label3 = new Label();
      this.timer2 = new System.Windows.Forms.Timer(this.components);
      this.timer3 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.Transparent;
      this.label1.FlatStyle = FlatStyle.Flat;
      this.label1.Font = new Font("Verdana", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.ForeColor = Color.Transparent;
      this.label1.Location = new Point(49, 9);
      this.label1.Margin = new Padding(5, 0, 5, 0);
      this.label1.Name = "label1";
      this.label1.RightToLeft = RightToLeft.No;
      this.label1.Size = new Size(61, 29);
      this.label1.TabIndex = 0;
      this.label1.Text = "放大";
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.label2.AutoSize = true;
      this.label2.Font = new Font("新宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label2.Location = new Point(37, 59);
      this.label2.Name = "label2";
      this.label2.Size = new Size(82, 24);
      this.label2.TabIndex = 1;
      this.label2.Text = "label2";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("新宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label3.Location = new Point(37, 107);
      this.label3.Name = "label3";
      this.label3.Size = new Size(82, 24);
      this.label3.TabIndex = 2;
      this.label3.Text = "label3";
      this.timer2.Tick += new EventHandler(this.timer2_Tick);
      this.timer3.Enabled = true;
      this.timer3.Tick += new EventHandler(this.timer3_Tick_1);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.Black;
      this.BackgroundImageLayout = ImageLayout.Zoom;
      this.ClientSize = new Size(187, 162);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Font = new Font("新宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.ForeColor = Color.Transparent;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.ImeMode = ImeMode.Disable;
      this.Margin = new Padding(5, 6, 5, 6);
      this.Name = nameof (keyDisplay);
      this.Opacity = 0.5;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (keyDisplay);
      this.TopMost = true;
      this.TransparencyKey = Color.WhiteSmoke;
      this.FormClosing += new FormClosingEventHandler(this.keyDisplay_FormClosing);
      this.Load += new EventHandler(this.keyDisplay_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void keyDisplayTextChanged();

    private delegate void aDelegeat();
  }
}
