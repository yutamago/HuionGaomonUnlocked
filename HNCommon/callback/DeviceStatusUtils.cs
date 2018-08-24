// Decompiled with JetBrains decompiler
// Type: HuionTablet.DeviceStatusUtils
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HuionTablet
{
  public class DeviceStatusUtils
  {
    public static bool reconnection = true;
    public const string STATUS_CONFIGCHANGED = "device_config_changed";
    public const int HD_MSG_VAL_CLOSE = 0;
    public const int HD_MSG_VAL_OPEN = 1;
    public const int HD_MSG_VAL_CONFIG_CHANGE = 2;
    private static DeviceStatusUtils.OpenDeviceCallbcak mConfigCallback;
    public static DeviceStatusUtils.DeviceConfigChanged deviceConfigListener;

    public static void deviceConfigCallback(uint msgVal)
    {
      if (msgVal != 2U || DeviceStatusUtils.deviceConfigListener == null)
        return;
      DeviceStatusUtils.deviceConfigListener(2);
    }

    public static void autoOpenDevice(object obj)
    {
      try
      {
        if (DeviceStatusUtils.mConfigCallback == null)
          DeviceStatusUtils.mConfigCallback = new DeviceStatusUtils.OpenDeviceCallbcak(DeviceStatusUtils.deviceConfigCallback);
        int num = (int) HuionDriverDLL.hnd_open(DeviceStatusUtils.mConfigCallback);
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("", ex.Message);
        HuionLog.saveLog("", ex.StackTrace);
      }
    }

    public void openDeviceCallback(uint msgVal)
    {
      if (msgVal == 2U)
      {
        DeviceStatusUtils.deviceConfigCallback(2U);
      }
      else
      {
        HuionLog.printLog("openDeviceCallback1", "uint msgVal = " + (object) msgVal);
        this.onDelayCallback((object) msgVal);
      }
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
  }
}
