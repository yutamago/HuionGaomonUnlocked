// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlAttribute
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HtmlAgilityPack
{
  [DebuggerDisplay("Name: {OriginalName}, Value: {Value}")]
  public class HtmlAttribute : IComparable
  {
    private AttributeValueQuote _quoteType = AttributeValueQuote.DoubleQuote;
    private int _line;
    internal int _lineposition;
    internal string _name;
    internal int _namelength;
    internal int _namestartindex;
    internal HtmlDocument _ownerdocument;
    internal HtmlNode _ownernode;
    internal int _streamposition;
    internal string _value;
    internal int _valuelength;
    internal int _valuestartindex;

    internal HtmlAttribute(HtmlDocument ownerdocument)
    {
      this._ownerdocument = ownerdocument;
    }

    public int Line
    {
      get
      {
        return this._line;
      }
      internal set
      {
        this._line = value;
      }
    }

    public int LinePosition
    {
      get
      {
        return this._lineposition;
      }
    }

    public string Name
    {
      get
      {
        if (this._name == null)
          this._name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
        return this._name.ToLower();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this._name = value;
        if (this._ownernode == null)
          return;
        this._ownernode._innerchanged = true;
        this._ownernode._outerchanged = true;
      }
    }

    public string OriginalName
    {
      get
      {
        return this._name;
      }
    }

    public HtmlDocument OwnerDocument
    {
      get
      {
        return this._ownerdocument;
      }
    }

    public HtmlNode OwnerNode
    {
      get
      {
        return this._ownernode;
      }
    }

    public AttributeValueQuote QuoteType
    {
      get
      {
        return this._quoteType;
      }
      set
      {
        this._quoteType = value;
      }
    }

    public int StreamPosition
    {
      get
      {
        return this._streamposition;
      }
    }

    public string Value
    {
      get
      {
        if (this._value == null)
          this._value = this._ownerdocument.Text.Substring(this._valuestartindex, this._valuelength);
        return this._value;
      }
      set
      {
        this._value = value;
        if (this._ownernode == null)
          return;
        this._ownernode._innerchanged = true;
        this._ownernode._outerchanged = true;
      }
    }

    internal string XmlName
    {
      get
      {
        return HtmlDocument.GetXmlName(this.Name);
      }
    }

    internal string XmlValue
    {
      get
      {
        return this.Value;
      }
    }

    public string XPath
    {
      get
      {
        return (this.OwnerNode == null ? "/" : this.OwnerNode.XPath + "/") + this.GetRelativeXpath();
      }
    }

    public int CompareTo(object obj)
    {
      HtmlAttribute htmlAttribute = obj as HtmlAttribute;
      if (htmlAttribute == null)
        throw new ArgumentException(nameof (obj));
      return this.Name.CompareTo(htmlAttribute.Name);
    }

    public HtmlAttribute Clone()
    {
      return new HtmlAttribute(this._ownerdocument)
      {
        Name = this.Name,
        Value = this.Value
      };
    }

    public void Remove()
    {
      this._ownernode.Attributes.Remove(this);
    }

    private string GetRelativeXpath()
    {
      if (this.OwnerNode == null)
        return this.Name;
      int num = 1;
      foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this.OwnerNode.Attributes)
      {
        if (!(attribute.Name != this.Name))
        {
          if (attribute != this)
            ++num;
          else
            break;
        }
      }
      return "@" + this.Name + "[" + (object) num + "]";
    }
  }
}
