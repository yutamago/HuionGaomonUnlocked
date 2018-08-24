// Decompiled with JetBrains decompiler
// Type: HuionTablet.Properties.Resources
// Assembly: Huion Tablet, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 89741050-C3D9-47F7-9E24-1E7879C81D96
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
                if (resourceMan == null)
                    resourceMan = new ResourceManager("HuionTablet.Properties.Resources", typeof(Resources).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        internal static Bitmap bg
        {
            get { return (Bitmap) ResourceManager.GetObject(nameof(bg), resourceCulture); }
        }

        internal static Bitmap icon0
        {
            get { return (Bitmap) ResourceManager.GetObject(nameof(icon0), resourceCulture); }
        }

        internal static Bitmap icon1
        {
            get { return (Bitmap) ResourceManager.GetObject(nameof(icon1), resourceCulture); }
        }

        internal static Bitmap icon2
        {
            get { return (Bitmap) ResourceManager.GetObject(nameof(icon2), resourceCulture); }
        }

        internal static Bitmap icon3
        {
            get { return (Bitmap) ResourceManager.GetObject(nameof(icon3), resourceCulture); }
        }
    }
}