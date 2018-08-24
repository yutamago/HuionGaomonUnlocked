// Decompiled with JetBrains decompiler
// Type: HuionTablet.Monitors
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Collections.Generic;

namespace HuionTablet
{
  public class Monitors
  {
    public HNStruct.PHYSICAL_MONITOR PhysicalMonitor { get; set; }

    public string Model { get; set; }

    public List<uint> Capabilitys { get; set; }

    public List<uint> InputSources { get; set; }

    public List<uint> ColorPresets { get; set; }
  }
}
