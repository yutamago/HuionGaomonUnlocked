// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.MonitorInfosCallback
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;

namespace HuionTablet.utils
{
    public delegate bool MonitorInfosCallback(IntPtr hMonitor, IntPtr hdcMonitor, ref HNStruct.RECT lprcMonitor,
        IntPtr dwData);
}