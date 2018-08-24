// Decompiled with JetBrains decompiler
// Type: Huion.HuionApi
// Assembly: HNApiCs, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 4957B58E-8324-4AC9-B678-1AA8EE08DC3D
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNApiCs.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Huion
{
  public class HuionApi
  {
    private static IntPtr hCtx = IntPtr.Zero;
    public const int PACKETMODE = 0;

    public static void listenDeviceInfo(IntPtr handle)
    {
      IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (LOGCONTEXTA)));
      try
      {
        Wintab32.WTInfoA(3U, 0U, num1);
      }
      catch
      {
        Marshal.FreeHGlobal(num1);
        return;
      }
      LOGCONTEXTA logcontexta = new LOGCONTEXTA();
      LOGCONTEXTA structure = (LOGCONTEXTA) Marshal.PtrToStructure(num1, typeof (LOGCONTEXTA));
      structure.lcPktData = Wintab32.PACKETDATA;
      structure.lcPktMode = 0U;
      structure.lcOptions = 2U;
      IntPtr num2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (LOGCONTEXTA)));
      Marshal.StructureToPtr((object) structure, num2, false);
      HuionApi.hCtx = Wintab32.WTOpenA(handle, num2, true);
      Marshal.FreeHGlobal(num1);
      Marshal.FreeHGlobal(num2);
    }

    public static void stopListenDeviceInfo()
    {
      if (!(HuionApi.hCtx != IntPtr.Zero))
        return;
      Wintab32.WTClose(HuionApi.hCtx);
      HuionApi.hCtx = IntPtr.Zero;
    }

    public static PACKET Msg2Packet(Message m)
    {
      if (m.Msg != 32752)
        return new PACKET();
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (PACKET)));
      Wintab32.WTPacket(m.LParam, (int) m.WParam, num);
      PACKET structure = (PACKET) Marshal.PtrToStructure(num, typeof (PACKET));
      Marshal.FreeHGlobal(num);
      return structure;
    }

    public static int getScreenTotalWidth()
    {
      return DllUtils.GetSystemMetrics(78);
    }

    public static int getScreenTotalHeight()
    {
      return DllUtils.GetSystemMetrics(79);
    }

    public static Bitmap getScreenShot(Rectangle rect)
    {
      Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
      graphics.Dispose();
      return bitmap;
    }
  }
}
