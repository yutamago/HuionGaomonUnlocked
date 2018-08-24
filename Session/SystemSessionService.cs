// Decompiled with JetBrains decompiler
// Type: HuionTablet.Lib.SystemSessionService
// Assembly: Session, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 39273FDA-F068-4F27-97F0-71CFA7804C8D
// Assembly location: D:\Program Files (x86)\Huion Tablet\Session.dll

using Microsoft.Win32;
using System;

namespace HuionTablet.Lib
{
  public class SystemSessionService
  {
    public static SystemSessionService.SystemSessionStateChangedCallback SystemSessionStateChanedListener;
    public static SystemSessionService.SystemDisplayChangedCallback SystemDisplayChangedListener;
    private static SessionSwitchEventHandler mSystemEvents_SessionSwitch;
    private static PowerModeChangedEventHandler mSystemEvents_PowerModeChanged;
    private static EventHandler mSystemEvent_ScreenChanged;

    public static void startListen()
    {
      SystemSessionService.mSystemEvents_SessionSwitch = new SessionSwitchEventHandler(SystemSessionService.SystemEvents_SessionSwitch);
      SystemSessionService.mSystemEvents_PowerModeChanged = new PowerModeChangedEventHandler(SystemSessionService.SystemEvents_PowerModeChanged);
      SystemSessionService.mSystemEvent_ScreenChanged = new EventHandler(SystemSessionService.SystemEvents_DisplaySettingsChanged);
      SystemEvents.SessionSwitch += SystemSessionService.mSystemEvents_SessionSwitch;
      SystemEvents.PowerModeChanged += SystemSessionService.mSystemEvents_PowerModeChanged;
      SystemEvents.DisplaySettingsChanged += SystemSessionService.mSystemEvent_ScreenChanged;
    }

    private static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
    {
      if (SystemSessionService.SystemDisplayChangedListener == null)
        return;
      SystemSessionService.SystemDisplayChangedListener();
    }

    private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
      int category = (int) e.Category;
    }

    public static void stopListen()
    {
      SystemEvents.SessionSwitch -= SystemSessionService.mSystemEvents_SessionSwitch;
      SystemEvents.PowerModeChanged -= SystemSessionService.mSystemEvents_PowerModeChanged;
    }

    private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
      Console.WriteLine(string.Concat((object) e.Mode));
      if (e.Mode == PowerModes.Resume)
      {
        if (SystemSessionService.SystemSessionStateChanedListener == null)
          return;
        SystemSessionService.SystemSessionStateChanedListener(SystemSessionService.SystemSessionState.Resume);
      }
      else
      {
        if (e.Mode != PowerModes.Suspend || SystemSessionService.SystemSessionStateChanedListener == null)
          return;
        SystemSessionService.SystemSessionStateChanedListener(SystemSessionService.SystemSessionState.Suspend);
      }
    }

    private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
    {
      Console.WriteLine(string.Concat((object) e.Reason));
      if (e.Reason == SessionSwitchReason.SessionLock)
      {
        if (SystemSessionService.SystemSessionStateChanedListener == null)
          return;
        SystemSessionService.SystemSessionStateChanedListener(SystemSessionService.SystemSessionState.Lock);
      }
      else if (e.Reason == SessionSwitchReason.SessionUnlock)
      {
        if (SystemSessionService.SystemSessionStateChanedListener == null)
          return;
        SystemSessionService.SystemSessionStateChanedListener(SystemSessionService.SystemSessionState.Unlock);
      }
      else if (e.Reason == SessionSwitchReason.SessionLogon)
      {
        if (SystemSessionService.SystemSessionStateChanedListener == null)
          return;
        SystemSessionService.SystemSessionStateChanedListener(SystemSessionService.SystemSessionState.Logon);
      }
      else
      {
        if (e.Reason != SessionSwitchReason.SessionLogoff || SystemSessionService.SystemSessionStateChanedListener == null)
          return;
        SystemSessionService.SystemSessionStateChanedListener(SystemSessionService.SystemSessionState.Logoff);
      }
    }

    public enum SystemSessionState
    {
      Suspend,
      Resume,
      Lock,
      Unlock,
      Logon,
      Logoff,
    }

    public delegate void SystemSessionStateChangedCallback(SystemSessionService.SystemSessionState state);

    public delegate void SystemDisplayChangedCallback();
  }
}
