// Decompiled with JetBrains decompiler
// Type: HuionTablet.Fixer4Main
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.Drawing;
using System.Management;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Huion;
using HuionTablet.Lib;
using HuionTablet.utils;

namespace HuionTablet
{
    public class Fixer4Main
    {
        public delegate void FormDelegate(Form f);

        private static Form mainForm;
        public static DeviceStatusUtils.OpenDeviceCallbcak mDeviceCallback;

        public static Form MainForm
        {
            set
            {
                mainForm = value;
                DpiHelper.createInstance(mainForm.CreateGraphics());
                MiddleModule.eventSend += new Send(MiddleModule_eventSend);
            }
            get { return mainForm; }
        }

        public static void openDevice(object obj)
        {
            HuionLog.printLog("", "open device");
            try
            {
                DeviceStatusUtils.reconnection = true;
                if (mDeviceCallback == null)
                    mDeviceCallback =
                        new DeviceStatusUtils.OpenDeviceCallbcak(new DeviceStatusUtils().openDeviceCallback);
                while (true)
                {
                    if (!HNStruct.globalInfo.bOpenedTablet && DeviceStatusUtils.reconnection &&
                        USB.hasConnectedDevice())
                    {
                        int num = (int) HuionDriverDLL.hnd_open(mDeviceCallback);
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                HuionLog.printSaveLog("", ex.Message);
            }
        }

        private static void MiddleModule_eventSend(object sender, object msg)
        {
            HuionLog.printLog(nameof(MiddleModule_eventSend), "reconnect device");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DeviceStatusUtils.autoOpenDevice));
        }

        public static void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(DeviceStatusUtils.autoOpenDevice));
        }

        public static void listenSystemStatus()
        {
            SystemSessionService.SystemSessionStateChanedListener +=
                new SystemSessionService.SystemSessionStateChangedCallback(onSystemSessionChanged);
            SystemSessionService.startListen();
        }

        public static void stopListenSystemStatus()
        {
            SystemSessionService.SystemSessionStateChanedListener =
                (SystemSessionService.SystemSessionStateChangedCallback) null;
            SystemSessionService.stopListen();
        }

        public static void setDisplayChangedCallback(SystemSessionService.SystemDisplayChangedCallback callback)
        {
            SystemSessionService.SystemDisplayChangedListener = callback;
        }

        private static void onSystemSessionChanged(SystemSessionService.SystemSessionState state)
        {
            switch (state)
            {
                case SystemSessionService.SystemSessionState.Resume:
                case SystemSessionService.SystemSessionState.Unlock:
                case SystemSessionService.SystemSessionState.Logon:
                    if (HNStruct.globalInfo.bOpenedTablet || !USB.hasConnectedDevice())
                        break;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(openDevice));
                    break;
            }
        }

        public static void applayClick(object sender, EventArgs e)
        {
            int num = (int) HuionDriverDLL.hnd_save_config(ref HNStruct.globalInfo.userConfig, IntPtr.Zero);
            HuionDriverDLL.hnd_notify_config_changed();
        }

        public static void closeClick(object sender, EventArgs e)
        {
            if (mainForm == null)
                return;
            mainForm.Close();
        }

        public static void okClick(object sender, EventArgs e)
        {
            applayClick(sender, e);
            closeClick(sender, e);
        }

        public static void adminClick(object sender, EventArgs e)
        {
            Utils.runAsAdmin(false);
        }

        public static void onMouseMove(object sender, EventArgs e)
        {
            TimerSession.userOperation();
        }

        public static void FormClosed(object sender, FormClosedEventArgs e)
        {
            KeyboardUtils.unlistenHotKey(((Control) sender).Handle);
            stopListenSystemStatus();
        }

        public static void onDeviceChanged(ref Message m)
        {
        }

        public static void USBEventHandler(object sender, EventArrivedEventArgs e)
        {
            if (!USB.isConnectionEvent(e) || !USB.isHNTabletDevice(e))
                return;
            HuionLog.printSaveLog("usb event", "Huion device was connected");
            int num = HNStruct.globalInfo.bOpenedTablet ? 1 : 0;
        }

        public static string getStatusText(bool enable)
        {
            if (!enable)
                return ResourceCulture.GetString("FormHuionTablet_lbCloseTabletText");
            return ResourceCulture.GetString("FormHuionTablet_lbOpenTabletText");
        }

        public static Icon getNotifyIcon(bool enable)
        {
            if (!enable)
                return ImageHelper.getDllIcon("16_off.ico", HNStruct.OemType);
            return ImageHelper.getDllIcon("16.ico", HNStruct.OemType);
        }

        public static string FormTitle()
        {
            switch (HNStruct.OemType)
            {
                case OEMType.HUION:
                    return ResourceCulture.GetString("FormHuionTablet_Text");
                case OEMType.GAOMON:
                    return ResourceCulture.GetString("Title4Gaomon");
                default:
                    return "TabletDriver";
            }
        }

        public static Icon FormIcon()
        {
            return ImageHelper.getDllIcon("icon.ico", HNStruct.OemType);
        }
    }
}