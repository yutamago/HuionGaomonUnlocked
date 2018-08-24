// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlNameTable
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System.Xml;

namespace HtmlAgilityPack
{
  internal class HtmlNameTable : XmlNameTable
  {
    private NameTable _nametable = new NameTable();

    public override string Add(string array)
    {
      return this._nametable.Add(array);
    }

    public override string Add(char[] array, int offset, int length)
    {
      return this._nametable.Add(array, offset, length);
    }

    public override string Get(string array)
    {
      return this._nametable.Get(array);
    }

    public override string Get(char[] array, int offset, int length)
    {
      return this._nametable.Get(array, offset, length);
    }

    internal string GetOrAdd(string array)
    {
      return this.Get(array) ?? this.Add(array);
    }
  }
}
