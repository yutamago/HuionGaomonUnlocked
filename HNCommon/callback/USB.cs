// Decompiled with JetBrains decompiler
// Type: HuionTablet.USB
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Collections.Generic;
using System.Management;
using System.Text.RegularExpressions;

namespace HuionTablet
{
  public class USB
  {
    private static Dictionary<string, HNStruct.HNDevice> HNDevices = new Dictionary<string, HNStruct.HNDevice>();
    public const string ConnectionEvent = "__InstanceCreationEvent";
    public const string DisconnectionEvent = "__InstanceDeletionEvent";
    private ManagementEventWatcher insertWatcher;
    private ManagementEventWatcher removeWatcher;

    public static void updateHNDeviceStatus(string key, string status)
    {
      if (!USB.HNDevices.ContainsKey(key))
        return;
      HNStruct.HNDevice hnDevice = USB.HNDevices[key];
      hnDevice.Status = status;
      USB.HNDevices.Remove(key);
      USB.HNDevices.Add(key, hnDevice);
    }

    public bool AddUSBEventWatcher(EventArrivedEventHandler usbInsertHandler, EventArrivedEventHandler usbRemoveHandler, TimeSpan withinInterval)
    {
      try
      {
        ManagementScope scope = new ManagementScope("root\\CIMV2");
        scope.Options.EnablePrivileges = true;
        if (usbInsertHandler != null)
        {
          WqlEventQuery wqlEventQuery = new WqlEventQuery("__InstanceCreationEvent", withinInterval, "TargetInstance isa 'Win32_USBControllerDevice'");
          this.insertWatcher = new ManagementEventWatcher(scope, (EventQuery) wqlEventQuery);
          this.insertWatcher.EventArrived += usbInsertHandler;
        }
        if (usbRemoveHandler != null)
        {
          WqlEventQuery wqlEventQuery = new WqlEventQuery("__InstanceDeletionEvent", withinInterval, "TargetInstance isa 'Win32_USBControllerDevice'");
          this.removeWatcher = new ManagementEventWatcher(scope, (EventQuery) wqlEventQuery);
          this.removeWatcher.EventArrived += usbRemoveHandler;
        }
        return true;
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("usb", ex.Message);
        this.RemoveUSBEventWatcher();
        return false;
      }
    }

    public void RemoveUSBEventWatcher()
    {
      if (this.insertWatcher != null)
      {
        this.insertWatcher.Stop();
        this.insertWatcher = (ManagementEventWatcher) null;
      }
      if (this.removeWatcher == null)
        return;
      this.removeWatcher.Stop();
      this.removeWatcher = (ManagementEventWatcher) null;
    }

    public static void loadAllHNDevice()
    {
      ManagementObjectCollection objectCollection1 = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
      if (objectCollection1 == null)
        return;
      foreach (ManagementBaseObject managementBaseObject in objectCollection1)
      {
        string input = (managementBaseObject["Dependent"] as string).Split('=')[1];
        Match match = Regex.Match(input, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
        if (match.Success)
        {
          ushort uint16_1 = Convert.ToUInt16(match.Value.Substring(4, 4), 16);
          ushort uint16_2 = Convert.ToUInt16(match.Value.Substring(13, 4), 16);
          if (HNStruct.HNDevice.isHNDevice(uint16_1, uint16_2))
          {
            ManagementObjectCollection objectCollection2 = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID=" + input).Get();
            if (objectCollection2 != null)
            {
              foreach (ManagementObject managementObject in objectCollection2)
              {
                Guid guid = new Guid(managementObject["ClassGuid"] as string);
                HNStruct.HNDevice hnDevice;
                hnDevice.PNPDeviceID = managementObject["PNPDeviceID"] as string;
                hnDevice.Name = managementObject["Name"] as string;
                hnDevice.Description = managementObject["Description"] as string;
                hnDevice.Service = managementObject["Service"] as string;
                hnDevice.Status = managementObject["Status"] as string;
                hnDevice.VID = uint16_1;
                hnDevice.PID = uint16_2;
                hnDevice.ClassGuid = guid;
                if (!USB.HNDevices.ContainsKey(input.Replace("\"", string.Empty)) && HNStruct.HNDevice.isTabletDevice(hnDevice.Description))
                  USB.HNDevices.Add(input.Replace("\"", string.Empty), hnDevice);
              }
            }
          }
        }
      }
    }

    public static bool hasConnectedDevice()
    {
      ManagementObjectCollection objectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
      if (objectCollection != null)
      {
        foreach (ManagementBaseObject managementBaseObject in objectCollection)
        {
          Match match = Regex.Match((managementBaseObject["Dependent"] as string).Split('=')[1], "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
          if (match.Success && HNStruct.HNDevice.isHNDevice(Convert.ToUInt16(match.Value.Substring(4, 4), 16), Convert.ToUInt16(match.Value.Substring(13, 4), 16)))
            return true;
        }
        objectCollection.Dispose();
      }
      return false;
    }

    public static bool isHNTabletDevice(EventArrivedEventArgs e)
    {
      ManagementBaseObject managementBaseObject = e.NewEvent["TargetInstance"] as ManagementBaseObject;
      if (managementBaseObject != null && managementBaseObject.ClassPath.ClassName == "Win32_USBControllerDevice")
      {
        string input = (managementBaseObject["Dependent"] as string).Split('=')[1];
        Match match = Regex.Match(input, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
        if (match.Success && HNStruct.HNDevice.isHNDevice(Convert.ToUInt16(match.Value.Substring(4, 4), 16), Convert.ToUInt16(match.Value.Substring(13, 4), 16)) && input.ToLower().IndexOf("vid_256c&pid_006e&mi_00") != -1)
          return true;
      }
      return false;
    }

    public static bool isConnectionEvent(EventArrivedEventArgs e)
    {
      return "__InstanceCreationEvent".Equals(e.NewEvent.ClassPath.ClassName);
    }
  }
}
