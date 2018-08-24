﻿// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.EncodingFoundException
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Text;

namespace HtmlAgilityPack
{
    internal class EncodingFoundException : Exception
    {
        private Encoding _encoding;

        internal EncodingFoundException(Encoding encoding)
        {
            this._encoding = encoding;
        }

        internal Encoding Encoding
        {
            get { return this._encoding; }
        }
    }
}