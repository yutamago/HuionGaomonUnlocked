// Decompiled with JetBrains decompiler
// Type: HuionTablet.Properties.Resources
// Assembly: Huion Tablet, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: E9BBED94-79CD-4774-8A97-2E0171DB986F
// Assembly location: D:\Program Files (x86)\Huion Tablet\app.publish\Huion Tablet.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace HuionTablet.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (HuionTablet.Properties.Resources.resourceMan == null)
          HuionTablet.Properties.Resources.resourceMan = new ResourceManager("HuionTablet.Properties.Resources", typeof (HuionTablet.Properties.Resources).Assembly);
        return HuionTablet.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return HuionTablet.Properties.Resources.resourceCulture;
      }
      set
      {
        HuionTablet.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap bg
    {
      get
      {
        return (Bitmap) HuionTablet.Properties.Resources.ResourceManager.GetObject(nameof (bg), HuionTablet.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap icon0
    {
      get
      {
        return (Bitmap) HuionTablet.Properties.Resources.ResourceManager.GetObject(nameof (icon0), HuionTablet.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap icon1
    {
      get
      {
        return (Bitmap) HuionTablet.Properties.Resources.ResourceManager.GetObject(nameof (icon1), HuionTablet.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap icon2
    {
      get
      {
        return (Bitmap) HuionTablet.Properties.Resources.ResourceManager.GetObject(nameof (icon2), HuionTablet.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap icon3
    {
      get
      {
        return (Bitmap) HuionTablet.Properties.Resources.ResourceManager.GetObject(nameof (icon3), HuionTablet.Properties.Resources.resourceCulture);
      }
    }
  }
}
