// Decompiled with JetBrains decompiler
// Type: HuionTablet.TouchInfoReader
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet
{
  public class TouchInfoReader
  {
    private static IntPtr mHandle = IntPtr.Zero;
    public const int HM_PACKET = 1064;
    public static TouchInfoReader.ReadTouchInfoCallback touchInfoListener;
    private static Thread mReadThread;
    private static bool reading;

    public static void startListen(IntPtr handle)
    {
      TouchInfoReader.mHandle = handle;
      TouchInfoReader.reading = true;
      HNStruct.HNRect r = new HNStruct.HNRect();
      try
      {
        HuionDriverDLL.hndh_init_cursor(ref r);
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("start draw line", ex.Message);
        HuionLog.saveLog("start draw line", ex.StackTrace);
      }
      TouchInfoReader.mReadThread = new Thread(new ThreadStart(TouchInfoReader.onRead));
      TouchInfoReader.mReadThread.Start();
    }

    private static void onRead()
    {
      int num1 = (int) HuionDriverDLL.timeBeginPeriod(1U);
      while (TouchInfoReader.reading)
      {
        HNStruct.HNPenData p = new HNStruct.HNPenData();
        try
        {
          HuionDriverDLL.hndh_get_cursor(ref p);
        }
        catch (Exception ex)
        {
          HuionLog.saveLog("Get Touch Info", ex.Message);
          HuionLog.saveLog("Get Touch Info", ex.StackTrace);
        }
        if (p.btn > (byte) 0)
        {
          if (TouchInfoReader.touchInfoListener != null)
            TouchInfoReader.touchInfoListener(p);
          IntPtr num2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (HNStruct.HNPenData)));
          Marshal.StructureToPtr((object) p, num2, false);
          HuionDriverDLL.PostMessage(TouchInfoReader.mHandle, 1064, num2, IntPtr.Zero);
          Marshal.FreeHGlobal(num2);
        }
        Thread.Sleep(1);
      }
      int num3 = (int) HuionDriverDLL.timeEndPeriod(1U);
    }

    public static void stopListen()
    {
      TouchInfoReader.reading = false;
      TouchInfoReader.touchInfoListener = (TouchInfoReader.ReadTouchInfoCallback) null;
      try
      {
        HuionDriverDLL.hndh_uninit_cursor();
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("stop draw line", ex.Message);
        HuionLog.saveLog("stop draw line", ex.StackTrace);
      }
    }

    public static int getPenPressValue()
    {
      return (int) TouchInfoReader.getTouchInfo().ps;
    }

    public static HNStruct.HNPenData getTouchInfo()
    {
      HNStruct.HNPenData p = new HNStruct.HNPenData();
      try
      {
        HuionDriverDLL.hndh_get_cursor(ref p);
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("Get Touch Info", ex.Message);
        HuionLog.saveLog("Get Touch Info", ex.StackTrace);
      }
      return p;
    }

    public static HNStruct.HNPenData Msg2Packet(Message m)
    {
      return (HNStruct.HNPenData) Marshal.PtrToStructure(m.WParam, typeof (HNStruct.HNPenData));
    }

    public delegate void ReadTouchInfoCallback(HNStruct.HNPenData data);
  }
}
