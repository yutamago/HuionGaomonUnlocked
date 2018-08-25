// Decompiled with JetBrains decompiler
// Type: HuionTablet.TouchInfoReader
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet
{
    public class TouchInfoReader
    {
        public delegate void ReadTouchInfoCallback(HNStruct.HNPenData data);

        public const int HM_PACKET = 1064;
        private static IntPtr mHandle = IntPtr.Zero;
        public static ReadTouchInfoCallback touchInfoListener;
        private static Thread mReadThread;
        private static bool reading;

        public static void startListen(IntPtr handle)
        {
            mHandle = handle;
            reading = true;
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

            mReadThread = new Thread(new ThreadStart(onRead));
            mReadThread.Start();
        }

        private static void onRead()
        {
            int num1 = (int) HuionDriverDLL.timeBeginPeriod(1U);
            while (reading)
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
                    if (touchInfoListener != null)
                        touchInfoListener(p);
                    IntPtr num2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNPenData)));
                    Marshal.StructureToPtr((object) p, num2, false);
                    HuionDriverDLL.PostMessage(mHandle, 1064, num2, IntPtr.Zero);
                    Marshal.FreeHGlobal(num2);
                }

                Thread.Sleep(1);
            }

            int num3 = (int) HuionDriverDLL.timeEndPeriod(1U);
        }

        public static void stopListen()
        {
            reading = false;
            touchInfoListener = (ReadTouchInfoCallback) null;
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
            return (int) getTouchInfo().ps;
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
            return (HNStruct.HNPenData) Marshal.PtrToStructure(m.WParam, typeof(HNStruct.HNPenData));
        }
    }
}