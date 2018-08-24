// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlTextNode
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

namespace HtmlAgilityPack
{
    public class HtmlTextNode : HtmlNode
    {
        private string _text;

        internal HtmlTextNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Text, ownerdocument, index)
        {
        }

        public override string InnerHtml
        {
            get { return this.OuterHtml; }
            set { this._text = value; }
        }

        public override string OuterHtml
        {
            get
            {
                if (this._text == null)
                    return base.OuterHtml;
                return this._text;
            }
        }

        public string Text
        {
            get
            {
                if (this._text == null)
                    return base.OuterHtml;
                return this._text;
            }
            set { this._text = value; }
        }
    }
}