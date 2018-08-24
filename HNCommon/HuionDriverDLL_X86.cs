// Decompiled with JetBrains decompiler
// Type: HuionTablet.HuionDriverDLL_X86
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Runtime.InteropServices;

namespace HuionTablet
{
  [StructLayout(LayoutKind.Sequential)]
  public class HuionDriverDLL_X86
  {
    public const string DLLNAME_DRIVER = "\\i386\\HuionDriver.dll";
    public const string DLLNAME_CROSS = "\\i386\\HuionCross.dll";
    public const string DLLNAME_DRIVERHOOK = "\\i386\\HuionDriverHook.dll";
    public const string DLLNAME_PARSE = "\\amd64\\HuionParse.dll";

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_open(DeviceStatusUtils.OpenDeviceCallbcak callback);

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnd_close();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnd_set_nonlicence();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnd_get_tablet_info();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_init_config();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnd_get_tablet_layout();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_read_config(ref HNStruct.HNConfig cfg, IntPtr path);

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_read_layout_tablet(ref HNStruct.HNLayoutTablet layoutTablet, ref HNStruct.HNConfig cfg);

    [DllImport("\\i386\\HuionDriver.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_read_layout_pen(ref HNStruct.HNLayoutPen layoutPen, string sPenNode);

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_save_config(ref HNStruct.HNConfig cfg, IntPtr path);

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnd_notify_config_changed();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnd_restore_config(ref HNStruct.HNConfig cfg);

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnd_refresh_monitors(IntPtr mi, uint num);

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnd_start_calibrate();

    [DllImport("\\i386\\HuionDriver.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnd_end_calibrate();

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnc_equation_power(double pv, double x, double maxP);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnc_equation_circle(double pv, double r, double maxP, int bArcAboveB);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern uint hnc_equation_circle1(double pv, double r, double maxP);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnc_get_oem_type(HnConst.HNOEMType t);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnc_get_dev_type(HnConst.HNTabletType t);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnc_get_pen_image(HnConst.HNTabletType t);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnc_get_pen_node(HnConst.HNTabletType t);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnc_get_tablet_image(HnConst.HNTabletType t);

    [DllImport("\\i386\\HuionCross.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hnc_calibrate_monitor(ref HNStruct.HNTabletInfo di, ref HNStruct.HNConfig cfg, IntPtr pt);

    [DllImport("\\i386\\HuionDriverHook.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int hndh_get_cursor(ref HNStruct.HNPenData p);

    [DllImport("\\i386\\HuionDriverHook.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int hndh_init_cursor(ref HNStruct.HNRect r);

    [DllImport("\\i386\\HuionDriverHook.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern void hndh_uninit_cursor();

    [DllImport("\\amd64\\HuionParse.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnp_get_pen_image(HnConst.HNTabletType t);

    [DllImport("\\amd64\\HuionParse.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnp_get_pen_node(HnConst.HNTabletType t);

    [DllImport("\\amd64\\HuionParse.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr hnp_get_tablet_image(HnConst.HNTabletType t);
  }
}
