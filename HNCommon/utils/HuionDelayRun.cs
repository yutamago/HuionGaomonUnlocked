// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.HuionDelayRun
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace HuionTablet.utils
{
  public class HuionDelayRun
  {
    private static List<System.Timers.Timer> mTimers = new List<System.Timers.Timer>();
    private int mT;
    private Runnable mRunnable;
    private object mData;
    private static HuionDelayRun mDelay;
    private static Thread mDelayThread;

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
      HuionDelayRun.mDelayThread = (Thread) null;
    }

    public static void delayRun(Runnable r, int t, object d)
    {
      if (HuionDelayRun.mDelayThread != null)
        HuionDelayRun.mDelayThread.Abort();
      HuionDelayRun.mDelay = new HuionDelayRun(t, r, d);
      HuionDelayRun.mDelayThread = new Thread(new ThreadStart(HuionDelayRun.mDelay.run));
      HuionDelayRun.mDelayThread.Start();
    }

    public static void postRun(Runnable r, int t, object d)
    {
      HuionDelayRun.stopAllDelayTask();
      System.Timers.Timer timer = (System.Timers.Timer) new HuionDelayRun.HuionTimer(r, d, (double) t);
      timer.Elapsed += new ElapsedEventHandler(HuionDelayRun.Timer_Elapsed);
      timer.AutoReset = false;
      timer.Enabled = true;
      HuionDelayRun.mTimers.Add(timer);
    }

    private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (sender is HuionDelayRun.HuionTimer)
        ((HuionDelayRun.HuionTimer) sender).postRun();
      HuionDelayRun.stopAllDelayTask();
    }

    public static void stopAllDelayTask()
    {
      foreach (System.Timers.Timer mTimer in HuionDelayRun.mTimers)
      {
        mTimer.Stop();
        mTimer.Dispose();
      }
      HuionDelayRun.mTimers.Clear();
    }

    public class HuionTimer : System.Timers.Timer
    {
      public Runnable Run;
      public object Data;

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
