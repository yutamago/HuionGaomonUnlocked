// Decompiled with JetBrains decompiler
// Type: HuionTablet.Program
// Assembly: Huion Tablet, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 89741050-C3D9-47F7-9E24-1E7879C81D96
// Assembly location: D:\Program Files (x86)\Huion Tablet\app.publish\Huion Tablet.exe

using Huion;
using HuionTablet.Lib;
using HuionTablet.utils;
using System;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      HNStruct.OemType = DeployConfig.getOemType();
      HuionLog.listenGlobalCrashLog();
      ResourceCulture.init();
      TimerSession.startListenUserOperation();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      bool flag = false;
      try
      {
        flag = Utils.isAppRunning();
      }
      catch
      {
        Utils.runAsAdmin(true);
        return;
      }
      if (new Mutex(true, "OnlyRun").WaitOne(0, false))
      {
        Application.Run((Form) new HuionTalbet());
      }
      else
      {
        if (Utils.isStartup(args) || HuionDriverDLL.PostMessage(HuionDriverDLL.FindWindow((string) null, Fixer4Main.FormTitle()), 1054, IntPtr.Zero, IntPtr.Zero) != 0)
          return;
        Utils.runAsAdmin(true);
      }
    }
  }
}
