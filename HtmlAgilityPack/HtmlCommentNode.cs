// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlCommentNode
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

namespace HtmlAgilityPack
{
    public class HtmlCommentNode : HtmlNode
    {
        private string _comment;

        internal HtmlCommentNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Comment, ownerdocument, index)
        {
        }

        public string Comment
        {
            get
            {
                if (this._comment == null)
                    return base.InnerHtml;
                return this._comment;
            }
            set { this._comment = value; }
        }

        public override string InnerHtml
        {
            get
            {
                if (this._comment == null)
                    return base.InnerHtml;
                return this._comment;
            }
            set { this._comment = value; }
        }

        public override string OuterHtml
        {
            get
            {
                if (this._comment == null)
                    return base.OuterHtml;
                return "<!--" + this._comment + "-->";
            }
        }
    }
}