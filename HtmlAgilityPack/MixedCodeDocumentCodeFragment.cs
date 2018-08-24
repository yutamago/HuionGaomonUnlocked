// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.MixedCodeDocumentCodeFragment
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

namespace HtmlAgilityPack
{
    public class MixedCodeDocumentCodeFragment : MixedCodeDocumentFragment
    {
        private string _code;

        internal MixedCodeDocumentCodeFragment(MixedCodeDocument doc)
            : base(doc, MixedCodeDocumentFragmentType.Code)
        {
        }

        public string Code
        {
            get
            {
                if (this._code == null)
                {
                    this._code = this.FragmentText.Substring(this.Doc.TokenCodeStart.Length,
                            this.FragmentText.Length - this.Doc.TokenCodeEnd.Length - this.Doc.TokenCodeStart.Length -
                            1)
                        .Trim();
                    if (this._code.StartsWith("="))
                        this._code = this.Doc.TokenResponseWrite + this._code.Substring(1, this._code.Length - 1);
                }

                return this._code;
            }
            set { this._code = value; }
        }
    }
}