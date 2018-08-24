// Decompiled with JetBrains decompiler
// Type: HuionTablet.PnPEntityInfo
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;

namespace HuionTablet
{
  public struct PnPEntityInfo
  {
    public string PNPDeviceID;
    public string Name;
    public string Description;
    public string Service;
    public string Status;
    public ushort VendorID;
    public ushort ProductID;
    public Guid ClassGuid;
  }
}
