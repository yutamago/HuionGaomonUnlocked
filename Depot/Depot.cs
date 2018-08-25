// Decompiled with JetBrains decompiler
// Type: HuionTablet.Depot
// Assembly: Depot, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: A3F0FEF4-BBE4-4840-AEFB-765D0FBD07E2
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