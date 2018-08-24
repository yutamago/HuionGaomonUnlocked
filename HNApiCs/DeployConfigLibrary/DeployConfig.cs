// Decompiled with JetBrains decompiler
// Type: Huion.DeployConfig
// Assembly: HNApiCs, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: E7997FEE-85D1-421E-99F3-E43695282E60
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNApiCs.dll

using System.Threading;

namespace Huion
{
  public class DeployConfig
  {
    private static bool newUI;
    public const string CopyrightName = "Shenzhen Huion Animation Technology Copyright";
    public const string PID = "{0F07496F-7E4A-49BE-BDB7-C7194EC6C358}";
    public const string VersionCode4Display = "v14.5.0";
    public const string VersionCode = "14.4.5.0";
    public const string UpdateVersion = "HuionTablet_WinDriver_v14.5.0.exe";
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
