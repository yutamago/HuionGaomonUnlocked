// Decompiled with JetBrains decompiler
// Type: HuionTablet.Depot
// Assembly: Depot, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 7A5EDBC9-3259-4706-97F7-C6DF31A213B5
// Assembly location: D:\Program Files (x86)\Huion Tablet\Depot.dll

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
