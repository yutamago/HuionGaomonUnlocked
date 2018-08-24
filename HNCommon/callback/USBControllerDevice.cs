// Decompiled with JetBrains decompiler
// Type: HuionTablet.USBControllerDevice
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

namespace HuionTablet
{
  public struct USBControllerDevice
  {
    public string Antecedent;
    public string Dependent;

    public override string ToString()
    {
      return this.Antecedent + "\n" + this.Dependent;
    }
  }
}
