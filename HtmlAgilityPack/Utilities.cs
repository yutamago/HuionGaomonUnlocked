// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.Utilities
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

using System.Collections.Generic;

namespace HtmlAgilityPack
{
    internal static class Utilities
    {
        public static TValue GetDictionaryValueOrNull<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key)
            where TKey : class
        {
            if (!dict.ContainsKey(key))
                return default(TValue);
            return dict[key];
        }
    }
}