// Decompiled with JetBrains decompiler
// Type: HuionTablet.HuionDriverDLL_X64
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Runtime.InteropServices;

namespace HuionTablet
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class HuionDriverDLL_X64
    {
        public const string DLLNAME_DRIVER = "\\amd64\\HuionDriver.dll";
        public const string DLLNAME_CROSS = "\\amd64\\HuionCross.dll";
        public const string DLLNAME_DRIVERHOOK = "\\amd64\\HuionDriverHook.dll";
        public const string DLLNAME_PARSE = "\\amd64\\HuionParse.dll";

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_open(DeviceStatusUtils.OpenDeviceCallbcak callback);

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnd_close();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnd_set_nonlicence();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnd_get_tablet_info();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_init_config();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnd_get_tablet_layout();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_read_config(ref HNStruct.HNConfig cfg, IntPtr path);

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_read_layout_tablet(ref HNStruct.HNLayoutTablet layoutTablet,
            ref HNStruct.HNConfig cfg);

        [DllImport("\\amd64\\HuionDriver.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_read_layout_pen(ref HNStruct.HNLayoutPen layoutPen, string sPenNode);

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_save_config(ref HNStruct.HNConfig cfg, IntPtr path);

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnd_notify_config_changed();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnd_restore_config(ref HNStruct.HNConfig cfg);

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnd_refresh_monitors(IntPtr mi, uint num);

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnd_start_calibrate();

        [DllImport("\\amd64\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnd_end_calibrate();

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnc_equation_power(double pv, double x, double maxP);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnc_equation_circle(double pv, double r, double maxP, int bArcAboveB);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint hnc_equation_circle1(double pv, double r, double maxP);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnc_get_oem_type(HnConst.HNOEMType t);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnc_get_dev_type(HnConst.HNTabletType t);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnc_get_pen_image(HnConst.HNTabletType t);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnc_get_pen_node(HnConst.HNTabletType t);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnc_get_tablet_image(HnConst.HNTabletType t);

        [DllImport("\\amd64\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hnc_calibrate_monitor(ref HNStruct.HNTabletInfo di, ref HNStruct.HNConfig cfg,
            IntPtr pt);

        [DllImport("\\amd64\\HuionDriverHook.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int hndh_get_cursor(ref HNStruct.HNPenData p);

        [DllImport("\\amd64\\HuionDriverHook.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int hndh_init_cursor(ref HNStruct.HNRect r);

        [DllImport("\\amd64\\HuionDriverHook.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void hndh_uninit_cursor();

        [DllImport("\\amd64\\HuionParse.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnp_get_pen_image(HnConst.HNTabletType t);

        [DllImport("\\amd64\\HuionParse.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnp_get_pen_node(HnConst.HNTabletType t);

        [DllImport("\\amd64\\HuionParse.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr hnp_get_tablet_image(HnConst.HNTabletType t);
    }
}