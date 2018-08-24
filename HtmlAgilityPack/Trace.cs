// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.Trace
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

namespace HtmlAgilityPack
{
    internal class Trace
    {
        internal static Trace _current;

        internal static Trace Current
        {
            get
            {
                if (_current == null)
                    _current = new Trace();
                return _current;
            }
        }

        private void WriteLineIntern(string message, string category)
        {
        }

        public static void WriteLine(string message, string category)
        {
            Current.WriteLineIntern(message, category);
        }
    }
}