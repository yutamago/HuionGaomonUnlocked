// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.MixedCodeDocument
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.IO;
using System.Text;

namespace HtmlAgilityPack
{
  public class MixedCodeDocument
  {
    public string TokenCodeEnd = "%>";
    public string TokenCodeStart = "<%";
    public string TokenDirective = "@";
    public string TokenResponseWrite = "Response.Write ";
    private string TokenTextBlock = "TextBlock({0})";
    private int _c;
    internal MixedCodeDocumentFragmentList _codefragments;
    private MixedCodeDocumentFragment _currentfragment;
    internal MixedCodeDocumentFragmentList _fragments;
    private int _index;
    private int _line;
    private int _lineposition;
    private MixedCodeDocument.ParseState _state;
    private Encoding _streamencoding;
    internal string _text;
    internal MixedCodeDocumentFragmentList _textfragments;

    public MixedCodeDocument()
    {
      this._codefragments = new MixedCodeDocumentFragmentList(this);
      this._textfragments = new MixedCodeDocumentFragmentList(this);
      this._fragments = new MixedCodeDocumentFragmentList(this);
    }

    public string Code
    {
      get
      {
        string str = "";
        int num = 0;
        foreach (MixedCodeDocumentFragment fragment in this._fragments)
        {
          switch (fragment._type)
          {
            case MixedCodeDocumentFragmentType.Code:
              str = str + ((MixedCodeDocumentCodeFragment) fragment).Code + "\n";
              continue;
            case MixedCodeDocumentFragmentType.Text:
              str = str + this.TokenResponseWrite + string.Format(this.TokenTextBlock, (object) num) + "\n";
              ++num;
              continue;
            default:
              continue;
          }
        }
        return str;
      }
    }

    public MixedCodeDocumentFragmentList CodeFragments
    {
      get
      {
        return this._codefragments;
      }
    }

    public MixedCodeDocumentFragmentList Fragments
    {
      get
      {
        return this._fragments;
      }
    }

    public Encoding StreamEncoding
    {
      get
      {
        return this._streamencoding;
      }
    }

    public MixedCodeDocumentFragmentList TextFragments
    {
      get
      {
        return this._textfragments;
      }
    }

    public MixedCodeDocumentCodeFragment CreateCodeFragment()
    {
      return (MixedCodeDocumentCodeFragment) this.CreateFragment(MixedCodeDocumentFragmentType.Code);
    }

    public MixedCodeDocumentTextFragment CreateTextFragment()
    {
      return (MixedCodeDocumentTextFragment) this.CreateFragment(MixedCodeDocumentFragmentType.Text);
    }

    public void Load(Stream stream)
    {
      this.Load((TextReader) new StreamReader(stream));
    }

    public void Load(Stream stream, bool detectEncodingFromByteOrderMarks)
    {
      this.Load((TextReader) new StreamReader(stream, detectEncodingFromByteOrderMarks));
    }

    public void Load(Stream stream, Encoding encoding)
    {
      this.Load((TextReader) new StreamReader(stream, encoding));
    }

    public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
      this.Load((TextReader) new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks));
    }

    public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
    {
      this.Load((TextReader) new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, buffersize));
    }

    public void Load(string path)
    {
      this.Load((TextReader) new StreamReader(path));
    }

    public void Load(string path, bool detectEncodingFromByteOrderMarks)
    {
      this.Load((TextReader) new StreamReader(path, detectEncodingFromByteOrderMarks));
    }

    public void Load(string path, Encoding encoding)
    {
      this.Load((TextReader) new StreamReader(path, encoding));
    }

    public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
      this.Load((TextReader) new StreamReader(path, encoding, detectEncodingFromByteOrderMarks));
    }

    public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
    {
      this.Load((TextReader) new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, buffersize));
    }

    public void Load(TextReader reader)
    {
      this._codefragments.Clear();
      this._textfragments.Clear();
      StreamReader streamReader = reader as StreamReader;
      if (streamReader != null)
        this._streamencoding = streamReader.CurrentEncoding;
      this._text = reader.ReadToEnd();
      reader.Close();
      this.Parse();
    }

    public void LoadHtml(string html)
    {
      this.Load((TextReader) new StringReader(html));
    }

    public void Save(Stream outStream)
    {
      this.Save(new StreamWriter(outStream, this.GetOutEncoding()));
    }

    public void Save(Stream outStream, Encoding encoding)
    {
      this.Save(new StreamWriter(outStream, encoding));
    }

    public void Save(string filename)
    {
      this.Save(new StreamWriter(filename, false, this.GetOutEncoding()));
    }

    public void Save(string filename, Encoding encoding)
    {
      this.Save(new StreamWriter(filename, false, encoding));
    }

    public void Save(StreamWriter writer)
    {
      this.Save((TextWriter) writer);
    }

    public void Save(TextWriter writer)
    {
      writer.Flush();
    }

    internal MixedCodeDocumentFragment CreateFragment(MixedCodeDocumentFragmentType type)
    {
      switch (type)
      {
        case MixedCodeDocumentFragmentType.Code:
          return (MixedCodeDocumentFragment) new MixedCodeDocumentCodeFragment(this);
        case MixedCodeDocumentFragmentType.Text:
          return (MixedCodeDocumentFragment) new MixedCodeDocumentTextFragment(this);
        default:
          throw new NotSupportedException();
      }
    }

    internal Encoding GetOutEncoding()
    {
      if (this._streamencoding != null)
        return this._streamencoding;
      return Encoding.UTF8;
    }

    private void IncrementPosition()
    {
      ++this._index;
      if (this._c == 10)
      {
        this._lineposition = 1;
        ++this._line;
      }
      else
        ++this._lineposition;
    }

    private void Parse()
    {
      this._state = MixedCodeDocument.ParseState.Text;
      this._index = 0;
      this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
      while (this._index < this._text.Length)
      {
        this._c = (int) this._text[this._index];
        this.IncrementPosition();
        switch (this._state)
        {
          case MixedCodeDocument.ParseState.Text:
            if (this._index + this.TokenCodeStart.Length < this._text.Length && this._text.Substring(this._index - 1, this.TokenCodeStart.Length) == this.TokenCodeStart)
            {
              this._state = MixedCodeDocument.ParseState.Code;
              this._currentfragment.Length = this._index - 1 - this._currentfragment.Index;
              this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Code);
              this.SetPosition();
              continue;
            }
            continue;
          case MixedCodeDocument.ParseState.Code:
            if (this._index + this.TokenCodeEnd.Length < this._text.Length && this._text.Substring(this._index - 1, this.TokenCodeEnd.Length) == this.TokenCodeEnd)
            {
              this._state = MixedCodeDocument.ParseState.Text;
              this._currentfragment.Length = this._index + this.TokenCodeEnd.Length - this._currentfragment.Index;
              this._index += this.TokenCodeEnd.Length;
              this._lineposition += this.TokenCodeEnd.Length;
              this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
              this.SetPosition();
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      this._currentfragment.Length = this._index - this._currentfragment.Index;
    }

    private void SetPosition()
    {
      this._currentfragment.Line = this._line;
      this._currentfragment._lineposition = this._lineposition;
      this._currentfragment.Index = this._index - 1;
      this._currentfragment.Length = 0;
    }

    private enum ParseState
    {
      Text,
      Code,
    }
  }
}
