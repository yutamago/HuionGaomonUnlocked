// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.HuionDelayRun
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace HuionTablet.utils
{
    public class HuionDelayRun
    {
        private static List<Timer> mTimers = new List<Timer>();
        private static HuionDelayRun mDelay;
        private static Thread mDelayThread;
        private object mData;
        private Runnable mRunnable;
        private int mT;

        public HuionDelayRun(int t, Runnable r, object d)
        {
            this.mT = t;
            this.mRunnable = r;
            this.mData = d;
        }

        public void run()
        {
            Thread.Sleep(this.mT);
            if (this.mRunnable != null)
                this.mRunnable(this.mData);
            mDelayThread = (Thread) null;
        }

        public static void delayRun(Runnable r, int t, object d)
        {
            if (mDelayThread != null)
                mDelayThread.Abort();
            mDelay = new HuionDelayRun(t, r, d);
            mDelayThread = new Thread(new ThreadStart(mDelay.run));
            mDelayThread.Start();
        }

        public static void postRun(Runnable r, int t, object d)
        {
            stopAllDelayTask();
            Timer timer = (Timer) new HuionTimer(r, d, (double) t);
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            timer.AutoReset = false;
            timer.Enabled = true;
            mTimers.Add(timer);
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sender is HuionTimer)
                ((HuionTimer) sender).postRun();
            stopAllDelayTask();
        }

        public static void stopAllDelayTask()
        {
            foreach (Timer mTimer in mTimers)
            {
                mTimer.Stop();
                mTimer.Dispose();
            }

            mTimers.Clear();
        }

        public class HuionTimer : Timer
        {
            public object Data;
            public Runnable Run;

            public HuionTimer(Runnable r, object d, double i)
                : base(i)
            {
                this.Run = r;
                this.Data = d;
            }

            public void postRun()
            {
                this.Run(this.Data);
            }
        }
    }
}