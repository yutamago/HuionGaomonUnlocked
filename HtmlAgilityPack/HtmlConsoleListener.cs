// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlConsoleListener
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

using System;
using System.Diagnostics;

namespace HtmlAgilityPack
{
    internal class HtmlConsoleListener : TraceListener
    {
        public override void Write(string Message)
        {
            this.Write(Message, "");
        }

        public override void Write(string Message, string Category)
        {
            Console.Write("T:" + Category + ": " + Message);
        }

        public override void WriteLine(string Message)
        {
            this.Write(Message + "\n");
        }

        public override void WriteLine(string Message, string Category)
        {
            this.Write(Message + "\n", Category);
        }
    }
}