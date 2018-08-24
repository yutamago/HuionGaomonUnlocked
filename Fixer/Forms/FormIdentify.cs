// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormIdentify
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace HuionTablet
{
  public class FormIdentify : Form
  {
    private object[] SetControlStyleArgs = new object[2]{ (object) ControlStyles.Selectable, (object) false };
    public static volatile int ShowCount;
    private int mIdentifyNumber;
    private Point mLocation;
    private bool mScreenIsSelected;
    private const int WM_MOUSEACTIVATE = 33;
    private const int MA_NOACTIVATE = 3;
    private MethodInfo SetControlStyleMethod;
    private IContainer components;
    private Label labelIdentify;

    public FormIdentify(int identifyNumber, Point location, bool isSelected)
    {
      this.InitializeComponent();
      this.mIdentifyNumber = identifyNumber;
      this.mLocation = location;
      this.mScreenIsSelected = isSelected;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 18, 18, 18);
      this.SetControlStyleMethod = typeof (Button).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
      this.SetChildControlNoFocus((Control) this);
      this.DoubleBuffered = true;
      this.SetStyle(ControlStyles.Selectable, false);
    }

    private void FormIdentify_Load(object sender, EventArgs e)
    {
      this.labelIdentify.Size = this.Size;
      this.labelIdentify.Text = this.mIdentifyNumber.ToString();
      this.labelIdentify.BackColor = Color.Transparent;
      this.labelIdentify.ForeColor = Color.White;
      if (this.mScreenIsSelected)
        this.labelIdentify.ForeColor = Color.Red;
      this.Location = this.mLocation;
      this.InvokeLostFocus((Control) this, e);
      ++FormIdentify.ShowCount;
      new Thread(new ThreadStart(this.run)).Start();
    }

    private void run()
    {
      Thread.Sleep(2000);
      this.Invoke((Delegate) new HuionTablet.utils.Void(this.close));
    }

    private void close()
    {
      --FormIdentify.ShowCount;
      this.Close();
      this.Dispose();
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 33)
        m.Result = new IntPtr(3);
      else
        base.WndProc(ref m);
    }

    protected override bool ShowWithoutActivation
    {
      get
      {
        return false;
      }
    }

    protected void SetChildControlNoFocus(Control ctrl)
    {
      if (!ctrl.HasChildren)
        return;
      foreach (Control control in (ArrangedElementCollection) ctrl.Controls)
        this.SetControlNoFocus(control);
    }

    private void SetControlNoFocus(Control ctrl)
    {
      this.SetControlStyleMethod.Invoke((object) ctrl, this.SetControlStyleArgs);
      this.SetChildControlNoFocus(ctrl);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.labelIdentify = new Label();
      this.SuspendLayout();
      this.labelIdentify.BackColor = Color.Transparent;
      this.labelIdentify.Font = new Font("微软雅黑", 125.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelIdentify.ForeColor = Color.White;
      this.labelIdentify.Location = new Point(0, 0);
      this.labelIdentify.Name = "labelIdentify";
      this.labelIdentify.Size = new Size(250, 250);
      this.labelIdentify.TabIndex = 0;
      this.labelIdentify.Text = "1";
      this.labelIdentify.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.BackColor = Color.Black;
      this.ClientSize = new Size(250, 250);
      this.Controls.Add((Control) this.labelIdentify);
      this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Margin = new Padding(3, 4, 3, 4);
      this.Name = nameof (FormIdentify);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (FormIdentify);
      this.TopMost = true;
      this.Load += new EventHandler(this.FormIdentify_Load);
      this.ResumeLayout(false);
    }
  }
}
