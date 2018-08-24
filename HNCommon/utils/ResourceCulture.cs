// Decompiled with JetBrains decompiler
// Type: HuionTablet.ResourceCulture
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace HuionTablet
{
    public class ResourceCulture
    {
        private static ResourceManager mResourceManager;

        public static void SetCurrentCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
                name = "en-US";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
        }

        public static void init()
        {
            string letterIsoLanguageName = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            if ("zh".Equals(letterIsoLanguageName) || "ar".Equals(letterIsoLanguageName) ||
                ("de".Equals(letterIsoLanguageName) || "es".Equals(letterIsoLanguageName)) ||
                ("fr".Equals(letterIsoLanguageName) || "it".Equals(letterIsoLanguageName) ||
                 ("ja".Equals(letterIsoLanguageName) || "ko".Equals(letterIsoLanguageName))) ||
                ("pl".Equals(letterIsoLanguageName) || "ru".Equals(letterIsoLanguageName) ||
                 "pt".Equals(letterIsoLanguageName)))
                return;
            SetCurrentCulture((string) null);
        }

        public static string GetString(string id)
        {
            string str;
            try
            {
                if (mResourceManager == null)
                    mResourceManager = new ResourceManager("HuionTablet.Resource", Assembly.GetEntryAssembly());
                CultureInfo culture = new CultureInfo("zh-CN", false);
                str = HNStruct.globalInfo.tabletInfo.devType == 34U || HNStruct.globalInfo.tabletInfo.devType == 37U ||
                      (HNStruct.globalInfo.tabletInfo.devType == 40U || HNStruct.globalInfo.tabletInfo.devType == 41U)
                    ? mResourceManager.GetString(id, culture)
                    : mResourceManager.GetString(id);
            }
            catch
            {
                str = "No id:" + id + ", please add.";
            }

            return str;
        }

        public static void disposeResource()
        {
            if (mResourceManager == null)
                return;
            mResourceManager.ReleaseAllResources();
            mResourceManager = (ResourceManager) null;
        }
    }
}