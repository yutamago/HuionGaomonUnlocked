// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.Utils
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using HuionTablet.Entity;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace HuionTablet.utils
{
  public class Utils
  {
    public const string HKLM_ARGS = "huion-hklm";
    public const string HKCU_ARGS = "huion-hkcu";
    public const string RUNAS_ARGS = "huion-luwu";

    public static HuionKeyLayout swapLayout(Size imageSize, HNStruct.HNSize keyPanelSize, HNStruct.HNLayoutEkey keyLayout)
    {
      HuionKeyLayout huionKeyLayout = new HuionKeyLayout();
      float num1 = (float) imageSize.Width / (float) keyPanelSize.cx;
      float num2 = (float) imageSize.Height / (float) keyPanelSize.cy;
      Rectangle rectangle1 = Utils.swapLayout(imageSize, keyPanelSize, keyLayout.rect);
      Rectangle rectangle2 = Utils.swapLayout(imageSize, keyPanelSize, keyLayout.inRect);
      huionKeyLayout.Center = new Point()
      {
        X = (int) Math.Round((double) keyLayout.center.x * (double) num1),
        Y = (int) Math.Round((double) keyLayout.center.y * (double) num2)
      };
      huionKeyLayout.InnerRect = rectangle2;
      huionKeyLayout.Rect = rectangle1;
      return huionKeyLayout;
    }

    public static Rectangle swapLayout(Size imageSize, HNStruct.HNSize keyPanelSize, HNStruct.HNRect keyRect)
    {
      float num1 = (float) imageSize.Width / (float) keyPanelSize.cx;
      float num2 = (float) imageSize.Height / (float) keyPanelSize.cy;
      return new Rectangle()
      {
        X = (int) Math.Round((double) keyRect.left * (double) num1),
        Y = (int) Math.Round((double) keyRect.top * (double) num2),
        Width = (int) Math.Round((double) (keyRect.right - keyRect.left) * (double) num1),
        Height = (int) Math.Round((double) (keyRect.bottom - keyRect.top) * (double) num2)
      };
    }

    public static Rectangle rotateRatioRect(Rectangle src, HNStruct.HNRectRatio ratio, int angle)
    {
      Rectangle rectangle = src;
      switch (angle)
      {
        case 90:
          rectangle.X = (int) Math.Round((double) src.X + (double) src.Width * (1.0 - (double) ratio.b));
          rectangle.Y = (int) Math.Round((double) src.Y + (double) src.Height * (double) ratio.l);
          rectangle.Width = (int) Math.Round((double) src.Width * ((double) ratio.r - (double) ratio.l));
          rectangle.Height = (int) Math.Round((double) src.Height * ((double) ratio.b - (double) ratio.t));
          break;
        case 180:
          rectangle.X = (int) Math.Round((double) src.X + (double) src.Width * (1.0 - (double) ratio.r));
          rectangle.Y = (int) Math.Round((double) src.Y + (double) src.Height * (1.0 - (double) ratio.b));
          rectangle.Width = (int) Math.Round((double) src.Width * ((double) ratio.r - (double) ratio.l));
          rectangle.Height = (int) Math.Round((double) src.Height * ((double) ratio.b - (double) ratio.t));
          break;
        case 270:
          rectangle.X = (int) Math.Round((double) src.X + (double) src.Width * (double) ratio.l);
          rectangle.Y = (int) Math.Round((double) src.Y + (double) src.Height * (1.0 - (double) ratio.r));
          rectangle.Width = (int) Math.Round((double) src.Width * ((double) ratio.r - (double) ratio.l));
          rectangle.Height = (int) Math.Round((double) src.Height * ((double) ratio.b - (double) ratio.t));
          break;
        default:
          rectangle.X = (int) Math.Round((double) src.X + (double) src.Width * (double) ratio.l);
          rectangle.Y = (int) Math.Round((double) src.Y + (double) src.Height * (double) ratio.t);
          rectangle.Width = (int) Math.Round((double) src.Width * ((double) ratio.r - (double) ratio.l));
          rectangle.Height = (int) Math.Round((double) src.Height * ((double) ratio.b - (double) ratio.t));
          break;
      }
      return rectangle;
    }

    public static HNStruct.HNRect rotateRect(HNStruct.HNRect src, HNStruct.HNSize size, int angle)
    {
      HNStruct.HNRect hnRect = src;
      int num1 = src.right - src.left;
      int num2 = src.bottom - src.top;
      switch (angle)
      {
        case 90:
          hnRect.left = size.cy - src.bottom;
          hnRect.top = src.left;
          hnRect.right = hnRect.left + num2;
          hnRect.bottom = hnRect.top + num1;
          break;
        case 180:
          hnRect.left = size.cx - src.right;
          hnRect.top = size.cy - src.bottom;
          hnRect.right = hnRect.left + num1;
          hnRect.bottom = hnRect.top + num2;
          break;
        case 270:
          hnRect.left = src.top;
          hnRect.top = size.cx - src.right;
          hnRect.right = hnRect.left + num2;
          hnRect.bottom = hnRect.top + num1;
          break;
      }
      return hnRect;
    }

    public static HNStruct.HNRectRatio rotateRatio(HNStruct.HNRectRatio src, int angle)
    {
      HNStruct.HNRectRatio hnRectRatio = src;
      switch (angle)
      {
        case -270:
          hnRectRatio.l = 1f - src.b;
          hnRectRatio.t = src.l;
          hnRectRatio.r = 1f - src.t;
          hnRectRatio.b = src.r;
          break;
        case -180:
          hnRectRatio.l = 1f - src.r;
          hnRectRatio.t = 1f - src.b;
          hnRectRatio.r = 1f - src.l;
          hnRectRatio.b = 1f - src.t;
          break;
        case -90:
          hnRectRatio.l = src.t;
          hnRectRatio.t = 1f - src.r;
          hnRectRatio.r = src.b;
          hnRectRatio.b = 1f - src.l;
          break;
        case 90:
          hnRectRatio.t = src.l;
          hnRectRatio.r = 1f - src.t;
          hnRectRatio.b = src.r;
          hnRectRatio.l = 1f - src.b;
          break;
        case 180:
          hnRectRatio.r = 1f - src.l;
          hnRectRatio.b = 1f - src.t;
          hnRectRatio.l = 1f - src.r;
          hnRectRatio.t = 1f - src.b;
          break;
        case 270:
          hnRectRatio.b = 1f - src.l;
          hnRectRatio.l = src.t;
          hnRectRatio.t = 1f - src.r;
          hnRectRatio.r = src.b;
          break;
      }
      return hnRectRatio;
    }

    public static HNStruct.HNSize rotateSize(HNStruct.HNSize size, int angle)
    {
      HNStruct.HNSize hnSize = size;
      if (angle == 90 || angle == 270)
      {
        hnSize.cx = size.cy;
        hnSize.cy = size.cx;
      }
      return hnSize;
    }

    public static Point getViewCenter(Control v)
    {
      return Utils.getViewCenter(v, 16);
    }

    public static Point getViewCenter(Control v, int gravity)
    {
      return Utils.getRectCenter(v.Bounds, gravity);
    }

    public static Point getRectCenter(Rectangle rect)
    {
      return Utils.getRectCenter(rect, 16);
    }

    public static bool mouseInView(Control v, Point p)
    {
      if (p.X >= v.Left && p.X <= v.Right && p.Y >= v.Top)
        return p.Y <= v.Bottom;
      return false;
    }

    public static Point getRectCenter(Rectangle rect, int gravity)
    {
      if (16 == gravity)
        return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
      bool flag1 = (1 & gravity) == 1;
      bool flag2 = (2 & gravity) == 2;
      bool flag3 = (4 & gravity) == 4;
      bool flag4 = (8 & gravity) == 8;
      bool flag5 = (16 & gravity) == 16;
      if (flag1 & flag3)
        return new Point(rect.X, rect.Y);
      if (flag1 & flag4)
        return new Point(rect.X, rect.Y + rect.Height);
      if (flag1 & flag5)
        return new Point(rect.X, rect.Y + rect.Height / 2);
      if (flag2 & flag3)
        return new Point(rect.X + rect.Width, rect.Y);
      if (flag2 & flag4)
        return new Point(rect.X + rect.Width, rect.Y + rect.Height);
      if (flag2 & flag5)
        return new Point(rect.X + rect.Width, rect.Y + rect.Height / 2);
      if (flag3 & flag5)
        return new Point(rect.X + rect.Width / 2, rect.Y);
      if (flag4 & flag5)
        return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height);
      return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
    }

    public static IntPtr getAddress<T>(T[] data)
    {
      int num1 = Marshal.SizeOf(typeof (T));
      IntPtr num2 = Marshal.AllocHGlobal(data.Length * num1);
      long num3 = !HuionDriverDLL.isX64 ? (long) num2.ToInt32() : num2.ToInt64();
      for (int index = 0; index < data.Length; ++index)
      {
        IntPtr ptr = new IntPtr(num3);
        Marshal.StructureToPtr((object) data[index], ptr, false);
        num3 += (long) num1;
      }
      return num2;
    }

    public static bool showMainWindow(IntPtr handle)
    {
      if (HuionDriverDLL.IsWindowVisible(handle))
      {
        HuionDriverDLL.SetForegroundWindow(handle);
        return true;
      }
      HuionDriverDLL.ShowWindow(handle, 5);
      return !HuionDriverDLL.SetForegroundWindow(handle);
    }

    public static bool isWin10
    {
      get
      {
        return Environment.OSVersion.Version.Major == 10;
      }
    }

    public static bool isAdmin()
    {
      return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }

    public static bool isChinese()
    {
      return "zh-CN".Equals(Thread.CurrentThread.CurrentCulture.Name);
    }

    public static void runAsAdmin(bool single)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = true;
      startInfo.WorkingDirectory = Environment.CurrentDirectory;
      startInfo.FileName = Application.ExecutablePath;
      startInfo.Verb = "runas";
      if (!single)
        startInfo.Arguments = "huion-luwu";
      try
      {
        Process.Start(startInfo);
        Application.Exit();
      }
      catch
      {
      }
    }

    public static void runAsAdmin(string args)
    {
      Process.Start(new ProcessStartInfo()
      {
        UseShellExecute = true,
        WorkingDirectory = Environment.CurrentDirectory,
        FileName = Application.ExecutablePath,
        Verb = "runas",
        Arguments = args
      });
      Application.Exit();
    }

    public static bool isAppRunning()
    {
      bool createdNew;
      Mutex mutex = new Mutex(true, Application.ProductName, out createdNew);
      if (!createdNew)
        return true;
      Process currentProcess = Process.GetCurrentProcess();
      Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
      if (processesByName != null && processesByName.Length != 0 && currentProcess != null)
      {
        foreach (Process process in processesByName)
        {
          if (currentProcess.Id != process.Id && process.MainModule != null && (currentProcess.MainModule != null && !string.IsNullOrEmpty(currentProcess.MainModule.FileName)) && (!string.IsNullOrEmpty(process.MainModule.FileName) && currentProcess.MainModule.FileName.Equals(process.MainModule.FileName)))
            return true;
        }
      }
      return false;
    }

    public static bool isRunas(string[] args)
    {
      return "huion-luwu".Equals(Utils.getArg(args));
    }

    public static bool isStartup(string[] args)
    {
      if (!"huion-hkcu".Equals(Utils.getArg(args)))
        return "huion-hklm".Equals(Utils.getArg(args));
      return true;
    }

    public static bool isStartup(string arg)
    {
      if (!"huion-hkcu".Equals(arg))
        return "huion-hklm".Equals(arg);
      return true;
    }

    public static string getArg(string[] args)
    {
      if (args != null && args.Length != 0)
        return args[0];
      return (string) null;
    }

    public static string ExecutablePath
    {
      get
      {
        return Application.ExecutablePath;
      }
    }

    public static string CommonStartupPath
    {
      get
      {
        return Path.GetPathRoot(Environment.SystemDirectory) + "ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\";
      }
    }

    public static string CommonStartupLinkPath
    {
      get
      {
        return Utils.CommonStartupPath + Path.GetFileNameWithoutExtension(Utils.ExecutablePath) + ".lnk";
      }
    }

    public static string DesktopLinkPath
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Path.GetFileNameWithoutExtension(Utils.ExecutablePath) + ".lnk";
      }
    }
  }
}
