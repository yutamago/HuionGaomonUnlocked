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

    private void WriteLineIntern(string message, string category)
    {
    }

    internal static Trace Current
    {
      get
      {
        if (Trace._current == null)
          Trace._current = new Trace();
        return Trace._current;
      }
    }

    public static void WriteLine(string message, string category)
    {
      Trace.Current.WriteLineIntern(message, category);
    }
  }
}
