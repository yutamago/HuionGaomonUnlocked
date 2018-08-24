// Decompiled with JetBrains decompiler
// Type: HuionTablet.HuionDriverDLL
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HuionTablet.utils;

namespace HuionTablet
{
    [StructLayout(LayoutKind.Sequential)]
    public class HuionDriverDLL
    {
        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8,
        }

        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 1,
            LeftDown = 2,
            LeftUp = 4,
            RightDown = 8,
            RightUp = 16, // 0x00000010
            MiddleDown = 32, // 0x00000020
            MiddleUp = 64, // 0x00000040
            XDown = 128, // 0x00000080
            XUp = 256, // 0x00000100
            Wheel = 2048, // 0x00000800
            VirtualDesk = 16384, // 0x00004000
            Absolute = 32768, // 0x00008000
        }

        public const string DLLNAME_USER32 = "User32.dll";
        public static readonly bool isX64 = IntPtr.Size == 8;

        public static uint hnd_open(DeviceStatusUtils.OpenDeviceCallbcak callback, IntPtr xmlPathConfig,
            IntPtr xmlPathLayout)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnd_open(callback, xmlPathConfig, xmlPathLayout);
            return HuionDriverDLL_X86.hnd_open(callback, xmlPathConfig, xmlPathLayout);
        }

        public static void hnd_close()
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_close();
            else
                HuionDriverDLL_X86.hnd_close();
        }

        public static void hnd_set_nonlicence()
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_set_nonlicence();
            else
                HuionDriverDLL_X86.hnd_set_nonlicence();
        }

        public static IntPtr hnd_get_tablet_info()
        {
            if (isX64)
                return HuionDriverDLL_X64.hnd_get_tablet_info();
            return HuionDriverDLL_X86.hnd_get_tablet_info();
        }

        public static void hnx_free_PHNConfig(IntPtr cfg)
        {
            if (isX64)
                HuionDriverDLL_X64.hnx_free_PHNConfig(cfg);
            else
                HuionDriverDLL_X86.hnx_free_PHNConfig(cfg);
        }

        public static void hnx_free_PHNLayoutTablet(IntPtr cfg)
        {
            if (isX64)
                HuionDriverDLL_X64.hnx_free_PHNLayoutTablet(cfg);
            else
                HuionDriverDLL_X86.hnx_free_PHNLayoutTablet(cfg);
        }

        public static void hnx_free_PHNLayoutPen(IntPtr cfg)
        {
            if (isX64)
                HuionDriverDLL_X64.hnx_free_PHNLayoutPen(cfg);
            else
                HuionDriverDLL_X86.hnx_free_PHNLayoutPen(cfg);
        }

        public static IntPtr hnx_read_layout_tablet(ref HNStruct.HNTabletInfo tabletinfo, IntPtr cfg, IntPtr path)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnx_read_layout_tablet(ref tabletinfo, cfg, path);
            return HuionDriverDLL_X86.hnx_read_layout_tablet(ref tabletinfo, cfg, path);
        }

        public static IntPtr hnx_read_config(ref HNStruct.HNTabletInfo tabletinfo, IntPtr path)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnx_read_config(ref tabletinfo, path);
            return HuionDriverDLL_X86.hnx_read_config(ref tabletinfo, path);
        }

        public static IntPtr hnx_read_layout_pen(ref HNStruct.HNTabletInfo tabletinfo, IntPtr path)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnx_read_layout_pen(ref tabletinfo, path);
            return HuionDriverDLL_X86.hnx_read_layout_pen(ref tabletinfo, path);
        }

        public static char hnx_save_config(IntPtr cfg, IntPtr tabletinfo, IntPtr sourcePath, IntPtr savePath)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnx_save_config(cfg, tabletinfo, sourcePath, savePath);
            return HuionDriverDLL_X86.hnx_save_config(cfg, tabletinfo, sourcePath, savePath);
        }

        public static void hnd_notify_config_changed()
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_notify_config_changed();
            else
                HuionDriverDLL_X86.hnd_notify_config_changed();
        }

        public static void hnd_refresh_monitors(IntPtr mi, uint num)
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_refresh_monitors(mi, num);
            else
                HuionDriverDLL_X86.hnd_refresh_monitors(mi, num);
        }

        public static void hnd_start_calibrate()
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_start_calibrate();
            else
                HuionDriverDLL_X86.hnd_start_calibrate();
        }

        public static void hnd_end_calibrate()
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_end_calibrate();
            else
                HuionDriverDLL_X86.hnd_end_calibrate();
        }

        public static void hnd_set_screenSaverRunning(int isRunning)
        {
            if (isX64)
                HuionDriverDLL_X64.hnd_set_screenSaverRunning(isRunning);
            else
                HuionDriverDLL_X86.hnd_set_screenSaverRunning(isRunning);
        }

        public static uint hnc_calibrate_press_tablet(IntPtr pconfig, ref HNStruct.HNTabletInfo tabletinfo, uint psVal,
            uint maxP)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnc_calibrate_press_tablet(pconfig, ref tabletinfo, psVal, maxP);
            return HuionDriverDLL_X86.hnc_calibrate_press_tablet(pconfig, ref tabletinfo, psVal, maxP);
        }

        public static uint hnc_equation_power(double pv, double x, double maxP)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnc_equation_power(pv, x, maxP);
            return HuionDriverDLL_X86.hnc_equation_power(pv, x, maxP);
        }

        public static uint hnc_equation_circle(double pv, double r, double maxP, int bArcAboveB)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnc_equation_circle(pv, r, maxP, bArcAboveB);
            return HuionDriverDLL_X86.hnc_equation_circle(pv, r, maxP, bArcAboveB);
        }

        public static uint hnc_equation_circle1(double pv, double r, double maxP)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnc_equation_circle1(pv, r, maxP);
            return HuionDriverDLL_X86.hnc_equation_circle1(pv, r, maxP);
        }

        public static IntPtr hnc_get_oem_type(HnConst.HNOEMType t)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnc_get_oem_type(t);
            return HuionDriverDLL_X86.hnc_get_oem_type(t);
        }

        public static IntPtr hnc_get_dev_type(HnConst.HNTabletType t)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnc_get_dev_type(t);
            return HuionDriverDLL_X86.hnc_get_dev_type(t);
        }

        public static IntPtr hnp_get_pen_image(HnConst.HNTabletType t)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnp_get_pen_image(t);
            return HuionDriverDLL_X86.hnp_get_pen_image(t);
        }

        public static IntPtr hnp_get_pen_node(HnConst.HNTabletType t)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnp_get_pen_node(t);
            return HuionDriverDLL_X86.hnp_get_pen_node(t);
        }

        public static IntPtr hnp_get_tablet_image(HnConst.HNTabletType t)
        {
            if (isX64)
                return HuionDriverDLL_X64.hnp_get_tablet_image(t);
            return HuionDriverDLL_X86.hnp_get_tablet_image(t);
        }

        public static void hnc_calibrate_monitor(IntPtr di, IntPtr cfg, IntPtr pt)
        {
            if (isX64)
                HuionDriverDLL_X64.hnc_calibrate_monitor(di, cfg, pt);
            else
                HuionDriverDLL_X86.hnc_calibrate_monitor(di, cfg, pt);
        }

        public static int hndh_get_cursor(ref HNStruct.HNPenData p)
        {
            if (isX64)
                return HuionDriverDLL_X64.hndh_get_cursor(ref p);
            return HuionDriverDLL_X86.hndh_get_cursor(ref p);
        }

        public static int hndh_init_cursor(ref HNStruct.HNRect r)
        {
            if (isX64)
                return HuionDriverDLL_X64.hndh_init_cursor(ref r);
            return HuionDriverDLL_X86.hndh_init_cursor(ref r);
        }

        public static void hndh_uninit_cursor()
        {
            if (isX64)
                HuionDriverDLL_X64.hndh_uninit_cursor();
            else
                HuionDriverDLL_X86.hndh_uninit_cursor();
        }

        [DllImport("User32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool SystemParametersInfo(int action, int param, ref int retval, int updini);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref HNStruct.MONITORINFOEX lpmi);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr MonitorFromRect(ref Rectangle rect, uint dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        public static extern IntPtr CursorFromFile(string fileName);

        [DllImport("User32.dll")]
        public static extern IntPtr SetCursor(IntPtr cursorHandle);

        [DllImport("User32.dll")]
        public static extern uint DestroyCursor(IntPtr cursorHandle);

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string lpSpecifiedClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);

        [DllImport("User32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorInfosCallback lpfnEnum,
            IntPtr dwData);

        [DllImport("User32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr Hwnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll")]
        public static extern bool SetProcessDPIAware();

        [DllImport("User32.dll")]
        public static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);

        [DllImport("User32.dll")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("User32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("winmm.dll")]
        internal static extern uint timeBeginPeriod(uint period);

        [DllImport("winmm.dll")]
        internal static extern uint timeEndPeriod(uint period);
    }
}