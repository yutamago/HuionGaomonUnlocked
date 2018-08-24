// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlAttributeCollection
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace HtmlAgilityPack
{
  public class HtmlAttributeCollection : IList<HtmlAttribute>, ICollection<HtmlAttribute>, IEnumerable<HtmlAttribute>, IEnumerable
  {
    internal Dictionary<string, HtmlAttribute> Hashitems = new Dictionary<string, HtmlAttribute>();
    private List<HtmlAttribute> items = new List<HtmlAttribute>();
    private HtmlNode _ownernode;

    internal HtmlAttributeCollection(HtmlNode ownernode)
    {
      this._ownernode = ownernode;
    }

    public HtmlAttribute this[string name]
    {
      get
      {
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        HtmlAttribute htmlAttribute;
        if (!this.Hashitems.TryGetValue(name.ToLower(), out htmlAttribute))
          return (HtmlAttribute) null;
        return htmlAttribute;
      }
      set
      {
        this.Append(value);
      }
    }

    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public HtmlAttribute this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        this.items[index] = value;
      }
    }

    public void Add(HtmlAttribute item)
    {
      this.Append(item);
    }

    void ICollection<HtmlAttribute>.Clear()
    {
      this.items.Clear();
    }

    public bool Contains(HtmlAttribute item)
    {
      return this.items.Contains(item);
    }

    public void CopyTo(HtmlAttribute[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    IEnumerator<HtmlAttribute> IEnumerable<HtmlAttribute>.GetEnumerator()
    {
      return (IEnumerator<HtmlAttribute>) this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    public int IndexOf(HtmlAttribute item)
    {
      return this.items.IndexOf(item);
    }

    public void Insert(int index, HtmlAttribute item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      this.Hashitems[item.Name] = item;
      item._ownernode = this._ownernode;
      this.items.Insert(index, item);
      this._ownernode._innerchanged = true;
      this._ownernode._outerchanged = true;
    }

    bool ICollection<HtmlAttribute>.Remove(HtmlAttribute item)
    {
      return this.items.Remove(item);
    }

    public void RemoveAt(int index)
    {
      this.Hashitems.Remove(this.items[index].Name);
      this.items.RemoveAt(index);
      this._ownernode._innerchanged = true;
      this._ownernode._outerchanged = true;
    }

    public void Add(string name, string value)
    {
      this.Append(name, value);
    }

    public HtmlAttribute Append(HtmlAttribute newAttribute)
    {
      if (newAttribute == null)
        throw new ArgumentNullException(nameof (newAttribute));
      this.Hashitems[newAttribute.Name] = newAttribute;
      newAttribute._ownernode = this._ownernode;
      this.items.Add(newAttribute);
      this._ownernode._innerchanged = true;
      this._ownernode._outerchanged = true;
      return newAttribute;
    }

    public HtmlAttribute Append(string name)
    {
      return this.Append(this._ownernode._ownerdocument.CreateAttribute(name));
    }

    public HtmlAttribute Append(string name, string value)
    {
      return this.Append(this._ownernode._ownerdocument.CreateAttribute(name, value));
    }

    public bool Contains(string name)
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].Name.Equals(name.ToLower()))
          return true;
      }
      return false;
    }

    public HtmlAttribute Prepend(HtmlAttribute newAttribute)
    {
      this.Insert(0, newAttribute);
      return newAttribute;
    }

    public void Remove(HtmlAttribute attribute)
    {
      if (attribute == null)
        throw new ArgumentNullException(nameof (attribute));
      int attributeIndex = this.GetAttributeIndex(attribute);
      if (attributeIndex == -1)
        throw new IndexOutOfRangeException();
      this.RemoveAt(attributeIndex);
    }

    public void Remove(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      string lower = name.ToLower();
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].Name == lower)
          this.RemoveAt(index);
      }
    }

    public void RemoveAll()
    {
      this.Hashitems.Clear();
      this.items.Clear();
      this._ownernode._innerchanged = true;
      this._ownernode._outerchanged = true;
    }

    public IEnumerable<HtmlAttribute> AttributesWithName(string attributeName)
    {
      attributeName = attributeName.ToLower();
      for (int i = 0; i < this.items.Count; ++i)
      {
        if (this.items[i].Name.Equals(attributeName))
          yield return this.items[i];
      }
    }

    public void Remove()
    {
      foreach (HtmlAttribute htmlAttribute in this.items)
        htmlAttribute.Remove();
    }

    internal void Clear()
    {
      this.Hashitems.Clear();
      this.items.Clear();
    }

    internal int GetAttributeIndex(HtmlAttribute attribute)
    {
      if (attribute == null)
        throw new ArgumentNullException(nameof (attribute));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index] == attribute)
          return index;
      }
      return -1;
    }

    internal int GetAttributeIndex(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      string lower = name.ToLower();
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].Name == lower)
          return index;
      }
      return -1;
    }
  }
}
