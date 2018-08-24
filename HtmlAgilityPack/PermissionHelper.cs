// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.PermissionHelper
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System.Net;
using System.Security;
using System.Security.Permissions;

namespace HtmlAgilityPack
{
    public class PermissionHelper : IPermissionHelper
    {
        public bool GetIsRegistryAvailable()
        {
            return SecurityManager.IsGranted((IPermission) new RegistryPermission(PermissionState.Unrestricted));
        }

        public bool GetIsDnsAvailable()
        {
            return SecurityManager.IsGranted((IPermission) new DnsPermission(PermissionState.Unrestricted));
        }
    }
}