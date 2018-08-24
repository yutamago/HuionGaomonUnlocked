// Decompiled with JetBrains decompiler
// Type: Huion.DeployConfig
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System.Threading;

namespace Huion
{
  public class DeployConfig
  {
    private static bool newUI;
    public const string CopyrightName = "Shenzhen Huion Animation Technology Copyright";
    public const string PID = "{0F07496F-7E4A-49BE-BDB7-C7194EC6C358}";
    public const string VersionCode4Display = "v14.7.4";
    public const string VersionCode = "14.4.7.4";
    public const string UpdateVersion = "HuionTablet_WinDriver_v14.7.4.exe";
    public const string downlaodFile = "";
    public const string CompanyYear = "2011-2018";
    public const string CompanyName = "Shenzhen Huion Animation Technology Co., Ltd.";
    public const string ServiceName = "HuionService";
    public const string ExeName = "\\Huion Tablet.exe";
    public const string ResourceName = "HuionTablet.Resource";
    public const string RegistryValue = "HuionTablet";
    public const string WebSiteCN = "http://www.huion.cn";
    public const string WebSiteEN = "http://www.huion.com";
    public const string WebSiteUpdate = "http://driver.huion.com/win/Public/html/update.html";
    public const string ProductName = "HuionTablet";

    public static OEMType getOemType()
    {
      return OEMType.HUION;
    }

    public static bool isNewUI
    {
      get
      {
        return DeployConfig.newUI;
      }
      set
      {
        DeployConfig.newUI = value;
      }
    }

    public static string Website
    {
      get
      {
        return !DeployConfig.isChinese() ? "http://www.huion.com" : "http://www.huion.cn";
      }
    }

    private static bool isChinese()
    {
      return "zh-CN".Equals(Thread.CurrentThread.CurrentCulture.Name);
    }
  }
}
