﻿// Decompiled with JetBrains decompiler
// Type: HuionTablet.PnPEntityInfo
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
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