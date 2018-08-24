// Decompiled with JetBrains decompiler
// Type: HuionTablet.Lib.TimerSession
// Assembly: Session, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 39273FDA-F068-4F27-97F0-71CFA7804C8D
// Assembly location: D:\Program Files (x86)\Huion Tablet\Session.dll

using System.Timers;

namespace HuionTablet.Lib
{
    public class TimerSession
    {
        public delegate void UserLongtimeNoOperationCallback();

        public const int IntervalTime = 600000;
        public static UserLongtimeNoOperationCallback UserLongtimeNoOperationListener;
        private static Timer timer4UserOperation;
        public static ElapsedEventHandler AutoOperationListener;
        private static Timer timer4AutoConnection;

        public static void startListenUserOperation()
        {
            timer4UserOperation = new Timer(600000.0);
            timer4UserOperation.Elapsed += new ElapsedEventHandler(ElapsedEventHandler4UserOperation);
            timer4UserOperation.AutoReset = true;
            timer4UserOperation.Enabled = true;
            listenAutoConnection();
        }

        public static void userOperation()
        {
            if (timer4UserOperation == null)
                return;
            timer4UserOperation.Stop();
            timer4UserOperation.Start();
        }

        private static void ElapsedEventHandler4UserOperation(object sender, ElapsedEventArgs e)
        {
            if (UserLongtimeNoOperationListener == null)
                return;
            UserLongtimeNoOperationListener();
        }

        public static void listenAutoConnection()
        {
            timer4AutoConnection = new Timer(30000.0);
            timer4AutoConnection.Elapsed += new ElapsedEventHandler(ElapsedEventHandler4AutoOperation);
            timer4AutoConnection.AutoReset = true;
        }

        public static void startAutoConnection()
        {
            if (timer4AutoConnection == null || timer4AutoConnection.Enabled)
                return;
            timer4AutoConnection.Enabled = true;
        }

        public static void stopAutoConnection()
        {
            if (timer4AutoConnection == null || !timer4AutoConnection.Enabled)
                return;
            timer4AutoConnection.Enabled = false;
        }

        private static void ElapsedEventHandler4AutoOperation(object sender, ElapsedEventArgs e)
        {
            if (AutoOperationListener == null)
                return;
            AutoOperationListener(sender, e);
        }
    }
}