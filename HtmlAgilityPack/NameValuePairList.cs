// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.NameValuePairList
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\HtmlAgilityPack.dll

using System;
using System.Collections.Generic;

namespace HtmlAgilityPack
{
    internal class NameValuePairList
    {
        internal readonly string Text;
        private List<KeyValuePair<string, string>> _allPairs;
        private Dictionary<string, List<KeyValuePair<string, string>>> _pairsWithName;

        internal NameValuePairList()
            : this((string) null)
        {
        }

        internal NameValuePairList(string text)
        {
            this.Text = text;
            this._allPairs = new List<KeyValuePair<string, string>>();
            this._pairsWithName = new Dictionary<string, List<KeyValuePair<string, string>>>();
            this.Parse(text);
        }

        internal static string GetNameValuePairsValue(string text, string name)
        {
            return new NameValuePairList(text).GetNameValuePairValue(name);
        }

        internal List<KeyValuePair<string, string>> GetNameValuePairs(string name)
        {
            if (name == null)
                return this._allPairs;
            if (!this._pairsWithName.ContainsKey(name))
                return new List<KeyValuePair<string, string>>();
            return this._pairsWithName[name];
        }

        internal string GetNameValuePairValue(string name)
        {
            if (name == null)
                throw new ArgumentNullException();
            List<KeyValuePair<string, string>> nameValuePairs = this.GetNameValuePairs(name);
            if (nameValuePairs.Count == 0)
                return string.Empty;
            return nameValuePairs[0].Value.Trim();
        }

        private void Parse(string text)
        {
            this._allPairs.Clear();
            this._pairsWithName.Clear();
            if (text == null)
                return;
            string str1 = text;
            char[] chArray = new char[1] {';'};
            foreach (string str2 in str1.Split(chArray))
            {
                if (str2.Length != 0)
                {
                    string[] strArray = str2.Split(new char[1] {'='}, 2);
                    if (strArray.Length != 0)
                    {
                        KeyValuePair<string, string> keyValuePair =
                            new KeyValuePair<string, string>(strArray[0].Trim().ToLower(),
                                strArray.Length < 2 ? "" : strArray[1]);
                        this._allPairs.Add(keyValuePair);
                        List<KeyValuePair<string, string>> keyValuePairList;
                        if (!this._pairsWithName.ContainsKey(keyValuePair.Key))
                        {
                            keyValuePairList = new List<KeyValuePair<string, string>>();
                            this._pairsWithName[keyValuePair.Key] = keyValuePairList;
                        }
                        else
                            keyValuePairList = this._pairsWithName[keyValuePair.Key];

                        keyValuePairList.Add(keyValuePair);
                    }
                }
            }
        }
    }
}