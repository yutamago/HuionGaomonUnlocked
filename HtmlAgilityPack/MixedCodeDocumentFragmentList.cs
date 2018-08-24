// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.MixedCodeDocumentFragmentList
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace HtmlAgilityPack
{
    public class MixedCodeDocumentFragmentList : IEnumerable
    {
        private MixedCodeDocument _doc;

        private IList<MixedCodeDocumentFragment> _items =
            (IList<MixedCodeDocumentFragment>) new List<MixedCodeDocumentFragment>();

        internal MixedCodeDocumentFragmentList(MixedCodeDocument doc)
        {
            this._doc = doc;
        }

        public MixedCodeDocument Doc
        {
            get { return this._doc; }
        }

        public int Count
        {
            get { return this._items.Count; }
        }

        public MixedCodeDocumentFragment this[int index]
        {
            get { return this._items[index]; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        public void Append(MixedCodeDocumentFragment newFragment)
        {
            if (newFragment == null)
                throw new ArgumentNullException(nameof(newFragment));
            this._items.Add(newFragment);
        }

        public MixedCodeDocumentFragmentEnumerator GetEnumerator()
        {
            return new MixedCodeDocumentFragmentEnumerator(this._items);
        }

        public void Prepend(MixedCodeDocumentFragment newFragment)
        {
            if (newFragment == null)
                throw new ArgumentNullException(nameof(newFragment));
            this._items.Insert(0, newFragment);
        }

        public void Remove(MixedCodeDocumentFragment fragment)
        {
            if (fragment == null)
                throw new ArgumentNullException(nameof(fragment));
            int fragmentIndex = this.GetFragmentIndex(fragment);
            if (fragmentIndex == -1)
                throw new IndexOutOfRangeException();
            this.RemoveAt(fragmentIndex);
        }

        public void RemoveAll()
        {
            this._items.Clear();
        }

        public void RemoveAt(int index)
        {
            this._items.RemoveAt(index);
        }

        internal void Clear()
        {
            this._items.Clear();
        }

        internal int GetFragmentIndex(MixedCodeDocumentFragment fragment)
        {
            if (fragment == null)
                throw new ArgumentNullException(nameof(fragment));
            for (int index = 0; index < this._items.Count; ++index)
            {
                if (this._items[index] == fragment)
                    return index;
            }

            return -1;
        }

        public class MixedCodeDocumentFragmentEnumerator : IEnumerator
        {
            private int _index;
            private IList<MixedCodeDocumentFragment> _items;

            internal MixedCodeDocumentFragmentEnumerator(IList<MixedCodeDocumentFragment> items)
            {
                this._items = items;
                this._index = -1;
            }

            public MixedCodeDocumentFragment Current
            {
                get { return this._items[this._index]; }
            }

            object IEnumerator.Current
            {
                get { return (object) this.Current; }
            }

            public bool MoveNext()
            {
                ++this._index;
                return this._index < this._items.Count;
            }

            public void Reset()
            {
                this._index = -1;
            }
        }
    }
}