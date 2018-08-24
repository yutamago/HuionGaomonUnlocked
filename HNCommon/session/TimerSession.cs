// Decompiled with JetBrains decompiler
// Type: HuionTablet.Lib.TimerSession
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Timers;

namespace HuionTablet.Lib
{
    public class TimerSession
    {
        public delegate void UserLongtimeNoOperationCallback();

        public const int IntervalTime = 300000;
        public static UserLongtimeNoOperationCallback UserLongtimeNoOperationListener;
        private static Timer timer4UserOperation;
        public static ElapsedEventHandler AutoOperationListener;
        private static Timer timer4AutoConnection;

        public static void startListenUserOperation()
        {
            timer4UserOperation = new Timer(300000.0);
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
            timer4AutoConnection.Enabled = true;
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