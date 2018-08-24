// Decompiled with JetBrains decompiler
// Type: HuionTablet.DeviceStatusUtils
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet
{
  public class DeviceStatusUtils
  {
    public static bool reconnection = true;
    public const string STATUS_CONFIGCHANGED = "device_config_changed";
    public const int HD_MSG_VAL_CLOSE = 0;
    public const int HD_MSG_VAL_OPEN = 1;
    public const int HD_MSG_VAL_CONFIG_CHANGE = 2;
    public const int HD_MSG_VAL_MUXKEY0 = 16;
    public const int HD_MSG_VAL_MUXKEY1 = 17;
    public const int HD_MSG_VAL_MUXKEY2 = 18;
    private static DeviceStatusUtils.OpenDeviceCallbcak mConfigCallback;
    public static DeviceStatusUtils.DeviceConfigChanged deviceConfigListener;
    public static DeviceStatusUtils.MekeyIndexChanged mekeyIndexListener;

    public static void deviceConfigCallback(uint msgVal)
    {
      if (msgVal != 2U && msgVal != 16U && (msgVal != 17U && msgVal != 18U) || DeviceStatusUtils.deviceConfigListener == null)
        return;
      DeviceStatusUtils.deviceConfigListener((int) msgVal);
    }

    public static void autoOpenDevice(object obj)
    {
      try
      {
        string s1 = Application.StartupPath + "\\res\\config_user.xml";
        string s2 = Application.StartupPath + "\\res\\layout_tablet.xml";
        IntPtr coTaskMemAuto1 = Marshal.StringToCoTaskMemAuto(s1);
        IntPtr coTaskMemAuto2 = Marshal.StringToCoTaskMemAuto(s2);
        if (DeviceStatusUtils.mConfigCallback == null)
          DeviceStatusUtils.mConfigCallback = new DeviceStatusUtils.OpenDeviceCallbcak(DeviceStatusUtils.deviceConfigCallback);
        int num = (int) HuionDriverDLL.hnd_open(DeviceStatusUtils.mConfigCallback, coTaskMemAuto1, coTaskMemAuto2);
        Marshal.FreeCoTaskMem(coTaskMemAuto1);
        Marshal.FreeCoTaskMem(coTaskMemAuto2);
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("", ex.Message);
        HuionLog.saveLog("", ex.StackTrace);
      }
    }

    public void openDeviceCallback(uint msgVal)
    {
      HuionLog.printLog("openDeviceCallback1", "uint msgVal = " + (object) msgVal);
      if (msgVal == 2U || msgVal == 16U || (msgVal == 17U || msgVal == 18U))
        DeviceStatusUtils.deviceConfigCallback(msgVal);
      else
        this.onDelayCallback((object) msgVal);
    }

    private void onDelayCallback(object o)
    {
      uint num = (uint) o;
      string currentDirectory = Environment.CurrentDirectory;
      try
      {
        IntPtr tabletInfo = HuionDriverDLL.hnd_get_tablet_info();
        HNStruct.globalInfo.tabletInfo = (HNStruct.HNTabletInfo) Marshal.PtrToStructure(tabletInfo, typeof (HNStruct.HNTabletInfo));
        if (HNStruct.globalInfo.tabletInfo.devType != 0U)
        {
          Console.Write(HNStruct.globalInfo.tabletInfo.devType.ToString());
          switch (HNStruct.OemType)
          {
            case OEMType.HUION:
              HNStruct.globalInfo.bOpenedTablet = TabletConfigUtils.isHuionTablet(HNStruct.globalInfo.tabletInfo.devType);
              Console.WriteLine(HNStruct.globalInfo.bOpenedTablet.ToString());
              if (HNStruct.globalInfo.tabletInfo.devType == 19U)
              {
                HNStruct.globalInfo.tabletInfo.maxP = (ushort) 4095;
                break;
              }
              break;
            case OEMType.GAOMON:
              HNStruct.globalInfo.bOpenedTablet = TabletConfigUtils.isGaomonTablet(HNStruct.globalInfo.tabletInfo.devType);
              break;
            case OEMType.YINENG:
              HNStruct.globalInfo.bOpenedTablet = TabletConfigUtils.isYinengTablet(HNStruct.globalInfo.tabletInfo.devType);
              break;
            case OEMType.YOUSHANG:
              HNStruct.globalInfo.bOpenedTablet = TabletConfigUtils.isYoushangTablet(HNStruct.globalInfo.tabletInfo.devType);
              break;
            case OEMType.SHIJUN:
              HNStruct.globalInfo.bOpenedTablet = TabletConfigUtils.isShijunTablet(HNStruct.globalInfo.tabletInfo.devType);
              break;
            case OEMType.KJC:
              HNStruct.globalInfo.bOpenedTablet = TabletConfigUtils.isKJCTablet(HNStruct.globalInfo.tabletInfo.devType);
              break;
            default:
              HNStruct.globalInfo.bOpenedTablet = false;
              break;
          }
          if (HNStruct.globalInfo.bOpenedTablet)
            new TabletConfigUtils().readConfig();
          else
            ThreadPool.QueueUserWorkItem(new WaitCallback(TabletConfigUtils.closeDevice));
          MiddleModule.PostMessage((object) this, (object) num);
        }
        else
        {
          HNStruct.globalInfo.bOpenedTablet = false;
          MiddleModule.PostMessage((object) this, (object) 0);
        }
      }
      catch (Exception ex)
      {
        HuionLog.printSaveLog("open device callback", ex.Message);
        HuionLog.printSaveLog("当前路径", currentDirectory);
        HuionLog.printSaveLog("open device callback", ex.StackTrace);
        HNStruct.globalInfo.bOpenedTablet = false;
        MiddleModule.PostMessage((object) this, (object) 0);
      }
    }

    private void sendMessage(object o)
    {
      if (HNStruct.globalInfo.bOpenedTablet || !USB.hasConnectedDevice())
        return;
      MiddleModule.SendMessage((object) this, (object) HNStruct.globalInfo.bOpenedTablet);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void OpenDeviceCallbcak(uint msgVal);

    public delegate void DeviceConfigChanged(int type);

    public delegate void MekeyIndexChanged(int type);
  }
}
