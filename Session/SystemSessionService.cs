﻿// Decompiled with JetBrains decompiler
// Type: HuionTablet.Lib.SystemSessionService
// Assembly: Session, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: FD4A05A9-AB6B-45B7-AB2F-2850B74E1F56
// Assembly location: D:\Program Files (x86)\Huion Tablet\Session.dll

using System;
using Microsoft.Win32;

namespace HuionTablet.Lib
{
    public class SystemSessionService
    {
        public delegate void SystemDisplayChangedCallback();

        public delegate void SystemSessionStateChangedCallback(SystemSessionState state);

        public enum SystemSessionState
        {
            Suspend,
            Resume,
            Lock,
            Unlock,
            Logon,
            Logoff,
        }

        public static SystemSessionStateChangedCallback SystemSessionStateChanedListener;
        public static SystemDisplayChangedCallback SystemDisplayChangedListener;
        private static SessionSwitchEventHandler mSystemEvents_SessionSwitch;
        private static PowerModeChangedEventHandler mSystemEvents_PowerModeChanged;
        private static EventHandler mSystemEvent_ScreenChanged;

        public static void startListen()
        {
            mSystemEvents_SessionSwitch = new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            mSystemEvents_PowerModeChanged = new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            mSystemEvent_ScreenChanged = new EventHandler(SystemEvents_DisplaySettingsChanged);
            SystemEvents.SessionSwitch += mSystemEvents_SessionSwitch;
            SystemEvents.PowerModeChanged += mSystemEvents_PowerModeChanged;
            SystemEvents.DisplaySettingsChanged += mSystemEvent_ScreenChanged;
        }

        private static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            if (SystemDisplayChangedListener == null)
                return;
            SystemDisplayChangedListener();
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            int category = (int) e.Category;
        }

        public static void stopListen()
        {
            SystemEvents.SessionSwitch -= mSystemEvents_SessionSwitch;
            SystemEvents.PowerModeChanged -= mSystemEvents_PowerModeChanged;
        }

        private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            Console.WriteLine(string.Concat((object) e.Mode));
            if (e.Mode == PowerModes.Resume)
            {
                if (SystemSessionStateChanedListener == null)
                    return;
                SystemSessionStateChanedListener(SystemSessionState.Resume);
            }
            else
            {
                if (e.Mode != PowerModes.Suspend || SystemSessionStateChanedListener == null)
                    return;
                SystemSessionStateChanedListener(SystemSessionState.Suspend);
            }
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            Console.WriteLine(string.Concat((object) e.Reason));
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                if (SystemSessionStateChanedListener == null)
                    return;
                SystemSessionStateChanedListener(SystemSessionState.Lock);
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                if (SystemSessionStateChanedListener == null)
                    return;
                SystemSessionStateChanedListener(SystemSessionState.Unlock);
            }
            else if (e.Reason == SessionSwitchReason.SessionLogon)
            {
                if (SystemSessionStateChanedListener == null)
                    return;
                SystemSessionStateChanedListener(SystemSessionState.Logon);
            }
            else
            {
                if (e.Reason != SessionSwitchReason.SessionLogoff || SystemSessionStateChanedListener == null)
                    return;
                SystemSessionStateChanedListener(SystemSessionState.Logoff);
            }
        }
    }
}