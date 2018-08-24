// Decompiled with JetBrains decompiler
// Type: HuionTablet.Depot
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.IO;

namespace HuionTablet
{
  public class Depot
  {
    public Stream loadImage(string imageName)
    {
      return this.GetType().Assembly.GetManifestResourceStream("HuionTablet.res." + imageName);
    }

    public Stream loadHuionImage(string imageName)
    {
      return this.GetType().Assembly.GetManifestResourceStream("HuionTablet.res.Huion." + imageName);
    }

    public Stream loadGaomonImage(string imageName)
    {
      return this.GetType().Assembly.GetManifestResourceStream("HuionTablet.res.Gaomon." + imageName);
    }

    public Stream loadTalbetDriverImage(string imageName)
    {
      return this.GetType().Assembly.GetManifestResourceStream("HuionTablet.res.TabletDriver." + imageName);
    }
  }
}
