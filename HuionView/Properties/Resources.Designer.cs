// Decompiled with JetBrains decompiler
// Type: Huion.Properties.Resources
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Huion.Properties
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
        if (Huion.Properties.Resources.resourceMan == null)
          Huion.Properties.Resources.resourceMan = new ResourceManager("Huion.Properties.Resources", typeof (Huion.Properties.Resources).Assembly);
        return Huion.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Huion.Properties.Resources.resourceCulture;
      }
      set
      {
        Huion.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap close
    {
      get
      {
        return (Bitmap) Huion.Properties.Resources.ResourceManager.GetObject(nameof (close), Huion.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap minimum
    {
      get
      {
        return (Bitmap) Huion.Properties.Resources.ResourceManager.GetObject(nameof (minimum), Huion.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap settings
    {
      get
      {
        return (Bitmap) Huion.Properties.Resources.ResourceManager.GetObject(nameof (settings), Huion.Properties.Resources.resourceCulture);
      }
    }
  }
}
