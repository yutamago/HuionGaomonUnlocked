// Decompiled with JetBrains decompiler
// Type: HuionTablet.Program
// Assembly: Huion Tablet, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: E9BBED94-79CD-4774-8A97-2E0171DB986F
// Assembly location: D:\Program Files (x86)\Huion Tablet\app.publish\Huion Tablet.exe

using Huion;
using HuionTablet.Lib;
using HuionTablet.utils;
using System;
using System.Windows.Forms;

namespace HuionTablet
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      if (!Utils.isAdmin() && !Utils.isRunas(args))
      {
        Utils.runAsAdmin(true);
      }
      else
      {
        HNStruct.OemType = DeployConfig.getOemType();
        HuionLog.listenGlobalCrashLog();
        HuionDriverDLL.hnd_init_config();
        ResourceCulture.init();
        TimerSession.startListenUserOperation();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        bool flag;
        try
        {
          flag = Utils.isAppRunning();
        }
        catch
        {
          Utils.runAsAdmin(true);
          return;
        }
        if (!flag)
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
}
