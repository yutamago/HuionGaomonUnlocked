// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.MixedCodeDocumentFragment
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

namespace HtmlAgilityPack
{
  public abstract class MixedCodeDocumentFragment
  {
    internal MixedCodeDocument Doc;
    private string _fragmentText;
    internal int Index;
    internal int Length;
    private int _line;
    internal int _lineposition;
    internal MixedCodeDocumentFragmentType _type;

    internal MixedCodeDocumentFragment(MixedCodeDocument doc, MixedCodeDocumentFragmentType type)
    {
      this.Doc = doc;
      this._type = type;
      switch (type)
      {
        case MixedCodeDocumentFragmentType.Code:
          this.Doc._codefragments.Append(this);
          break;
        case MixedCodeDocumentFragmentType.Text:
          this.Doc._textfragments.Append(this);
          break;
      }
      this.Doc._fragments.Append(this);
    }

    public string FragmentText
    {
      get
      {
        if (this._fragmentText == null)
          this._fragmentText = this.Doc._text.Substring(this.Index, this.Length);
        return this.FragmentText;
      }
      internal set
      {
        this._fragmentText = value;
      }
    }

    public MixedCodeDocumentFragmentType FragmentType
    {
      get
      {
        return this._type;
      }
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

    public int StreamPosition
    {
      get
      {
        return this.Index;
      }
    }
  }
}
