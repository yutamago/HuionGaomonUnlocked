// Decompiled with JetBrains decompiler
// Type: Huion.ViewUtils
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Huion
{
  public class ViewUtils
  {
    public static ToolTip CreateTip(Control v, string text)
    {
      ToolTip toolTip = new ToolTip();
      toolTip.AutoPopDelay = 5000;
      toolTip.InitialDelay = 500;
      toolTip.ReshowDelay = 500;
      toolTip.ShowAlways = true;
      toolTip.SetToolTip(v, text);
      return toolTip;
    }

    public static IntPtr CreateCursor()
    {
      Bitmap bitmap = new Bitmap(8, 8);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.DrawEllipse(Pens.Black, new Rectangle(0, 0, bitmap.Width - 1, bitmap.Height - 1));
      graphics.Dispose();
      return bitmap.GetHicon();
    }
  }
}
