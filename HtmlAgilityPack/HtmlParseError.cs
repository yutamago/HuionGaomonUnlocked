// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlParseError
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

namespace HtmlAgilityPack
{
    public class HtmlParseError
    {
        private HtmlParseErrorCode _code;
        private int _line;
        private int _linePosition;
        private string _reason;
        private string _sourceText;
        private int _streamPosition;

        internal HtmlParseError(HtmlParseErrorCode code, int line, int linePosition, int streamPosition,
            string sourceText, string reason)
        {
            this._code = code;
            this._line = line;
            this._linePosition = linePosition;
            this._streamPosition = streamPosition;
            this._sourceText = sourceText;
            this._reason = reason;
        }

        public HtmlParseErrorCode Code
        {
            get { return this._code; }
        }

        public int Line
        {
            get { return this._line; }
        }

        public int LinePosition
        {
            get { return this._linePosition; }
        }

        public string Reason
        {
            get { return this._reason; }
        }

        public string SourceText
        {
            get { return this._sourceText; }
        }

        public int StreamPosition
        {
            get { return this._streamPosition; }
        }
    }
}