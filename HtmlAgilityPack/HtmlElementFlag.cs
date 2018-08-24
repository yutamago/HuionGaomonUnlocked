// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlElementFlag
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

using System;

namespace HtmlAgilityPack
{
    [Flags]
    public enum HtmlElementFlag
    {
        CData = 1,
        Empty = 2,
        Closed = 4,
        CanOverlap = 8,
    }
}