// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.ScreenHelper
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HuionTablet.utils
{
    public class ScreenHelper
    {
        private static HNStruct.MONITORINFOEX[] mMonitorInfos;
        private static int screenNum;
        private static MonitorInfosCallback listener;

        public static HNStruct.MONITORINFOEX[] MonitorInfos
        {
            get { return mMonitorInfos; }
        }

        public static int ScreenNum
        {
            get { return screenNum; }
        }

        public static void getMonitorInfos()
        {
            screenNum = 0;
            listener = new MonitorInfosCallback(onMonitorInfoCallback);
            mMonitorInfos = new HNStruct.MONITORINFOEX[10];
            HuionDriverDLL.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, listener, IntPtr.Zero);
            int num1 = Marshal.SizeOf(typeof(HNStruct.MONITORINFOEX));
            IntPtr num2 = Marshal.AllocHGlobal(num1 * 10);
            long num3 = 0;
            switch (IntPtr.Size)
            {
                case 4:
                    num3 = (long) num2.ToInt32();
                    break;
                case 8:
                    num3 = num2.ToInt64();
                    break;
            }

            for (int index = 0; index < 10; ++index)
            {
                Marshal.StructureToPtr((object) mMonitorInfos[index], new IntPtr(num3), false);
                num3 += (long) num1;
            }

            HuionDriverDLL.hnd_refresh_monitors(num2, (uint) screenNum);
            Marshal.FreeHGlobal(num2);
        }

        private static bool onMonitorInfoCallback(IntPtr hMonitor, IntPtr hdcMonitor, ref HNStruct.RECT lprcMonitor,
            IntPtr dwData)
        {
            HNStruct.MONITORINFOEX lpmi = new HNStruct.MONITORINFOEX();
            lpmi.Size = Marshal.SizeOf(typeof(HNStruct.MONITORINFOEX));
            HuionDriverDLL.GetMonitorInfo(hMonitor, ref lpmi);
            mMonitorInfos[screenNum] = lpmi;
            ++screenNum;
            return true;
        }

        public static Bitmap getScreenImage(int index, HNStruct.MONITORINFOEX[] monitorinfo)
        {
            HNStruct.RECT screenRect = getScreenRect(index, monitorinfo);
            ref HNStruct.MONITORINFOEX local = ref monitorinfo[index];
            Size blockRegionSize = new Size(screenRect.Right - screenRect.Left, screenRect.Bottom - screenRect.Top);
            Bitmap bitmap = new Bitmap(blockRegionSize.Width, blockRegionSize.Height);
            Graphics graphics = Graphics.FromImage((Image) bitmap);
            graphics.CopyFromScreen(screenRect.Left, screenRect.Top, 0, 0, blockRegionSize);
            graphics.Dispose();
            return bitmap;
        }

        public static HNStruct.RECT getScreenRect(int index, HNStruct.MONITORINFOEX[] monitorinfo)
        {
            HNStruct.MONITORINFOEX monitorinfoex = monitorinfo[index];
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            if (index == screenNum)
            {
                for (int index1 = 0; index1 < screenNum; ++index1)
                {
                    if (num1 > MonitorInfos[index1].Monitor.Left)
                        num1 = MonitorInfos[index1].Monitor.Left;
                    if (num3 > MonitorInfos[index1].Monitor.Top)
                        num3 = MonitorInfos[index1].Monitor.Top;
                    if (num2 < MonitorInfos[index1].Monitor.Right)
                        num2 = MonitorInfos[index1].Monitor.Right;
                    if (num4 < MonitorInfos[index1].Monitor.Bottom)
                        num4 = MonitorInfos[index1].Monitor.Bottom;
                }
            }
            else
            {
                num1 = monitorinfoex.Monitor.Left;
                num3 = monitorinfoex.Monitor.Top;
                num2 = monitorinfoex.Monitor.Right;
                num4 = monitorinfoex.Monitor.Bottom;
            }

            return new HNStruct.RECT()
            {
                Left = num1,
                Bottom = num4,
                Right = num2,
                Top = num3
            };
        }
    }
}