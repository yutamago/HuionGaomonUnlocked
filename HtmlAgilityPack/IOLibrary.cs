// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.IOLibrary
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System.IO;
using System.Runtime.InteropServices;

namespace HtmlAgilityPack
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct IOLibrary
    {
        internal static void CopyAlways(string source, string target)
        {
            if (!File.Exists(source))
                return;
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            MakeWritable(target);
            File.Copy(source, target, true);
        }

        internal static void MakeWritable(string path)
        {
            if (!File.Exists(path))
                return;
            File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
        }
    }
}