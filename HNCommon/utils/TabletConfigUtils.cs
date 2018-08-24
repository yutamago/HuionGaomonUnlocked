// Decompiled with JetBrains decompiler
// Type: HuionTablet.TabletConfigUtils
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using HuionTablet.Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HuionTablet
{
  public class TabletConfigUtils
  {
    public const string TYPE_HUION = "HUION";
    public const string TYPE_GAOMON = "OEM02";
    public const string TYPE_NEUTRAL = "OEM00";
    public const string TYPE_YOUSHANG = "OEM06";
    public const string TYPE_YINENG = "OEM07";
    public const string TYPE_SHIJUN = "OEM08";
    public const string TYPE_KJC = "OEM10";

    public void readConfig()
    {
      HNStruct.globalInfo.isDeviceConnected = new bool?(HNStruct.globalInfo.bOpenedTablet);
      HNStruct.devTypeString = Marshal.PtrToStringAuto(HuionDriverDLL.hnc_get_tablet_image((HnConst.HNTabletType) HNStruct.globalInfo.tabletInfo.devType));
      HNStruct.tabletTextInfo = HNStruct.globalInfo.bOpenedTablet ? ResourceCulture.GetString("FormHuionTablet_lbOpenTabletText") : ResourceCulture.GetString("FormHuionTablet_lbCloseTabletText");
      if (!HNStruct.globalInfo.bOpenedTablet)
        return;
      this.SetLayoutTablet();
      KBTable.init_KBTable();
      this.SetLayoutPen();
      this.SetConfig(HuionDriverDLL.hnd_read_config(ref HNStruct.globalInfo.userConfig, IntPtr.Zero));
    }

    public void SetConfig(uint result)
    {
      if (result == 0U)
        return;
      HNStruct.HNGlobalInfo globalInfo = HNStruct.globalInfo;
      HNStruct.globalInfo.hbtns = new HNStruct.HNEkey[(int) HNStruct.globalInfo.layoutTablet.hbtnNum];
      int num1 = 0;
      foreach (HNStruct.HNEkey hnEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNEkey>(HNStruct.globalInfo.userConfig.hbtns, (int) HNStruct.globalInfo.layoutTablet.hbtnNum))
        HNStruct.globalInfo.hbtns[num1++] = hnEkey;
      HNStruct.globalInfo.sbtns = new HNStruct.HNEkey[(int) HNStruct.globalInfo.layoutTablet.sbtnNum];
      int num2 = 0;
      foreach (HNStruct.HNEkey hnEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNEkey>(HNStruct.globalInfo.userConfig.sbtns, (int) HNStruct.globalInfo.layoutTablet.sbtnNum))
        HNStruct.globalInfo.sbtns[num2++] = hnEkey;
      HNStruct.globalInfo.pbtns = new HNStruct.HNEkey[(int) HNStruct.globalInfo.layoutPen.ekNum];
      int num3 = 0;
      foreach (HNStruct.HNEkey hnEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNEkey>(HNStruct.globalInfo.userConfig.pbtns, (int) HNStruct.globalInfo.layoutPen.ekNum))
        HNStruct.globalInfo.pbtns[num3++] = hnEkey;
      HNStruct.globalInfo.meKeys = new HNStruct.HNMEkey[(int) HNStruct.globalInfo.userConfig.mekeyNum];
      int num4 = 0;
      foreach (HNStruct.HNMEkey hnmEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNMEkey>(HNStruct.globalInfo.userConfig.mekeys, (int) HNStruct.globalInfo.layoutTablet.mekeyNum))
      {
        HNStruct.globalInfo.meKeys[num4++] = hnmEkey;
        HNStruct.globalInfo.mbtns = new HNStruct.HNEkey[(int) hnmEkey.num];
        int num5 = 0;
        foreach (HNStruct.HNEkey hnEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNEkey>(hnmEkey.ekeys, (int) hnmEkey.num))
          HNStruct.globalInfo.mbtns[num5++] = hnEkey;
      }
    }

    public void readDisConnectConfig()
    {
      TimerSession.stopAutoConnection();
      HNStruct.tabletTextInfo = HNStruct.globalInfo.bOpenedTablet ? ResourceCulture.GetString("FormHuionTablet_lbOpenTabletText") : ResourceCulture.GetString("FormHuionTablet_lbCloseTabletText");
    }

    public void SetLayoutTablet()
    {
      IntPtr tabletLayout = HuionDriverDLL.hnd_get_tablet_layout();
      HNStruct.globalInfo.layoutTablet = (HNStruct.HNLayoutTablet) Marshal.PtrToStructure(tabletLayout, typeof (HNStruct.HNLayoutTablet));
      HNStruct.globalInfo.hbtnLayouts = new HNStruct.HNLayoutEkey[(int) HNStruct.globalInfo.layoutTablet.hbtnNum];
      int num1 = 0;
      foreach (HNStruct.HNLayoutEkey hnLayoutEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(HNStruct.globalInfo.layoutTablet.hbtnLayouts, (int) HNStruct.globalInfo.layoutTablet.hbtnNum))
        HNStruct.globalInfo.hbtnLayouts[num1++] = hnLayoutEkey;
      HNStruct.globalInfo.sbtnLayouts = new HNStruct.HNLayoutEkey[(int) HNStruct.globalInfo.layoutTablet.sbtnNum];
      int num2 = 0;
      foreach (HNStruct.HNLayoutEkey hnLayoutEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(HNStruct.globalInfo.layoutTablet.sbtnLayouts, (int) HNStruct.globalInfo.layoutTablet.sbtnNum))
        HNStruct.globalInfo.sbtnLayouts[num2++] = hnLayoutEkey;
      HNStruct.globalInfo.mekeyLayouts = new HNStruct.HNLayoutEkey[(int) HNStruct.globalInfo.layoutTablet.mekeyNum];
      int num3 = 0;
      foreach (HNStruct.HNLayoutEkey hnLayoutEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(HNStruct.globalInfo.layoutTablet.mekeyLayouts, (int) HNStruct.globalInfo.layoutTablet.mekeyNum))
        HNStruct.globalInfo.mekeyLayouts[num3++] = hnLayoutEkey;
      HNStruct.globalInfo.mekeyNames = new HNStruct.HNMEkeyName[(int) HNStruct.globalInfo.layoutTablet.mekeyNum];
      int index1 = 0;
      foreach (HNStruct.HNMEkeyName hnmEkeyName in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNMEkeyName>(HNStruct.globalInfo.layoutTablet.mekeyNames, (int) HNStruct.globalInfo.layoutTablet.mekeyNum))
      {
        HNStruct.globalInfo.mekeyNames[index1] = hnmEkeyName;
        HNStruct.globalInfo.names = new string[(int) HNStruct.globalInfo.mekeyNames[index1].num];
        IntPtr ptr = HNStruct.globalInfo.mekeyNames[index1].names;
        for (int index2 = 0; (long) index2 < (long) HNStruct.globalInfo.mekeyNames[0].num; ++index2)
        {
          switch (IntPtr.Size)
          {
            case 4:
              HNStruct.globalInfo.names[index2] = Marshal.PtrToStringAuto(new IntPtr((int) Marshal.PtrToStructure(ptr, typeof (int))));
              ptr = new IntPtr(ptr.ToInt64() + 4L);
              break;
            case 8:
              HNStruct.globalInfo.names[index2] = Marshal.PtrToStringAuto(new IntPtr((long) Marshal.PtrToStructure(ptr, typeof (long))));
              ptr = new IntPtr(ptr.ToInt64() + 8L);
              break;
          }
        }
        ++index1;
      }
      HNStruct.globalInfo.ekLayouts = new HNStruct.HNLayoutEkey[(int) HNStruct.globalInfo.layoutTablet.ekNum];
      int num4 = 0;
      foreach (HNStruct.HNLayoutEkey hnLayoutEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(HNStruct.globalInfo.layoutTablet.ekLayouts, (int) HNStruct.globalInfo.layoutTablet.ekNum))
        HNStruct.globalInfo.ekLayouts[num4++] = hnLayoutEkey;
      HNStruct.HNRect penArea = HNStruct.globalInfo.layoutTablet.penArea;
      HNStruct.HNSize size = HNStruct.globalInfo.layoutTablet.size;
      HNStruct.HNRectRatio hnRectRatio;
      hnRectRatio.l = (float) penArea.left / (float) size.cx;
      hnRectRatio.r = (float) penArea.right / (float) size.cx;
      hnRectRatio.t = (float) penArea.top / (float) size.cy;
      hnRectRatio.b = (float) penArea.bottom / (float) size.cy;
      HNStruct.globalInfo.penareaRatio = hnRectRatio;
    }

    public void SetLayoutPen()
    {
      string stringAuto = Marshal.PtrToStringAuto(HuionDriverDLL.hnc_get_pen_node((HnConst.HNTabletType) HNStruct.globalInfo.tabletInfo.devType));
      int num1 = (int) HuionDriverDLL.hnd_read_layout_pen(ref HNStruct.globalInfo.layoutPen, stringAuto);
      HNStruct.globalInfo.penLayouts = new HNStruct.HNLayoutEkey[(int) HNStruct.globalInfo.layoutPen.ekNum];
      int num2 = 0;
      foreach (HNStruct.HNLayoutEkey hnLayoutEkey in TabletConfigUtils.MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(HNStruct.globalInfo.layoutPen.ekLayouts, (int) HNStruct.globalInfo.layoutPen.ekNum))
        HNStruct.globalInfo.penLayouts[num2++] = hnLayoutEkey;
    }

    public static List<T> MarshalPtrToStructArray<T>(IntPtr p, int count)
    {
      List<T> objList = new List<T>();
      switch (IntPtr.Size)
      {
        case 4:
          int num1 = 0;
          while (num1 < count)
          {
            T structure = (T) Marshal.PtrToStructure(p, typeof (T));
            objList.Add(structure);
            ++num1;
            p = new IntPtr(p.ToInt32() + Marshal.SizeOf(typeof (T)));
          }
          break;
        case 8:
          int num2 = 0;
          while (num2 < count)
          {
            T structure = (T) Marshal.PtrToStructure(p, typeof (T));
            objList.Add(structure);
            ++num2;
            p = new IntPtr(p.ToInt64() + (long) Marshal.SizeOf(typeof (T)));
          }
          break;
      }
      return objList;
    }

    public static bool isHuionTablet(uint devType)
    {
      return TabletConfigUtils.getTabletType(devType).IndexOf("HUION") != -1;
    }

    public static bool isGaomonTablet(uint devType)
    {
      return TabletConfigUtils.getTabletType(devType).IndexOf("OEM02") != -1;
    }

    public static bool isYinengTablet(uint devType)
    {
      return TabletConfigUtils.getTabletType(devType).IndexOf("OEM07") != -1;
    }

    public static bool isYoushangTablet(uint devType)
    {
      return TabletConfigUtils.getTabletType(devType).IndexOf("OEM06") != -1;
    }

    public static bool isShijunTablet(uint devType)
    {
      return TabletConfigUtils.getTabletType(devType).IndexOf("OEM08") != -1;
    }

    public static bool isKJCTablet(uint devType)
    {
      return TabletConfigUtils.getTabletType(devType).IndexOf("OEM10") != -1;
    }

    public static string getTabletType(uint devType)
    {
      try
      {
        return ((HnConst.HNTabletType) devType).ToString();
      }
      catch (Exception ex)
      {
        return HnConst.HNTabletType.HNTT_UNKNOW.ToString();
      }
    }

    public static void closeDevice(object obj)
    {
      try
      {
        DeviceStatusUtils.reconnection = false;
        HuionDriverDLL.hnd_close();
      }
      catch (Exception ex)
      {
        HuionLog.printLog("", ex.Message);
        HuionLog.printLog("", ex.StackTrace);
      }
    }
  }
}
