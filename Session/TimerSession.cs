// Decompiled with JetBrains decompiler
// Type: HuionTablet.Lib.TimerSession
// Assembly: Session, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: FD4A05A9-AB6B-45B7-AB2F-2850B74E1F56
// Assembly location: D:\Program Files (x86)\Huion Tablet\Session.dll

using System.Timers;

namespace HuionTablet.Lib
{
  public class TimerSession
  {
    public static TimerSession.UserLongtimeNoOperationCallback UserLongtimeNoOperationListener;
    public const int IntervalTime = 600000;
    private static Timer timer4UserOperation;
    public static ElapsedEventHandler AutoOperationListener;
    private static Timer timer4AutoConnection;

    public static void startListenUserOperation()
    {
      TimerSession.timer4UserOperation = new Timer(600000.0);
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
