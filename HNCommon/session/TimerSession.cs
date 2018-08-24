﻿// Decompiled with JetBrains decompiler
// Type: HuionTablet.Lib.TimerSession
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Timers;

namespace HuionTablet.Lib
{
  public class TimerSession
  {
    public static TimerSession.UserLongtimeNoOperationCallback UserLongtimeNoOperationListener;
    public const int IntervalTime = 300000;
    private static Timer timer4UserOperation;
    public static ElapsedEventHandler AutoOperationListener;
    private static Timer timer4AutoConnection;

    public static void startListenUserOperation()
    {
      TimerSession.timer4UserOperation = new Timer(300000.0);
      TimerSession.timer4UserOperation.Elapsed += new ElapsedEventHandler(TimerSession.ElapsedEventHandler4UserOperation);
      TimerSession.timer4UserOperation.AutoReset = true;
      TimerSession.timer4UserOperation.Enabled = true;
      TimerSession.listenAutoConnection();
    }

    public static void userOperation()
    {
      if (TimerSession.timer4UserOperation == null)
        return;
      TimerSession.timer4UserOperation.Stop();
      TimerSession.timer4UserOperation.Start();
    }

    private static void ElapsedEventHandler4UserOperation(object sender, ElapsedEventArgs e)
    {
      if (TimerSession.UserLongtimeNoOperationListener == null)
        return;
      TimerSession.UserLongtimeNoOperationListener();
    }

    public static void listenAutoConnection()
    {
      TimerSession.timer4AutoConnection = new Timer(30000.0);
      TimerSession.timer4AutoConnection.Elapsed += new ElapsedEventHandler(TimerSession.ElapsedEventHandler4AutoOperation);
      TimerSession.timer4AutoConnection.AutoReset = true;
      TimerSession.timer4AutoConnection.Enabled = true;
    }

    public static void startAutoConnection()
    {
      if (TimerSession.timer4AutoConnection == null || TimerSession.timer4AutoConnection.Enabled)
        return;
      TimerSession.timer4AutoConnection.Enabled = true;
    }

    public static void stopAutoConnection()
    {
      if (TimerSession.timer4AutoConnection == null || !TimerSession.timer4AutoConnection.Enabled)
        return;
      TimerSession.timer4AutoConnection.Enabled = false;
    }

    private static void ElapsedEventHandler4AutoOperation(object sender, ElapsedEventArgs e)
    {
      if (TimerSession.AutoOperationListener == null)
        return;
      TimerSession.AutoOperationListener(sender, e);
    }

    public delegate void UserLongtimeNoOperationCallback();
  }
}