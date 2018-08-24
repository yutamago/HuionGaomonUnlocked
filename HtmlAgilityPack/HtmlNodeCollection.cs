// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlNodeCollection
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace HtmlAgilityPack
{
  public class HtmlNodeCollection : IList<HtmlNode>, ICollection<HtmlNode>, IEnumerable<HtmlNode>, IEnumerable
  {
    private readonly List<HtmlNode> _items = new List<HtmlNode>();
    private readonly HtmlNode _parentnode;

    public HtmlNodeCollection(HtmlNode parentnode)
    {
      this._parentnode = parentnode;
    }

    public int this[HtmlNode node]
    {
      get
      {
        int nodeIndex = this.GetNodeIndex(node);
        if (nodeIndex == -1)
          throw new ArgumentOutOfRangeException(nameof (node), "Node \"" + node.CloneNode(false).OuterHtml + "\" was not found in the collection");
        return nodeIndex;
      }
    }

    public HtmlNode this[string nodeName]
    {
      get
      {
        nodeName = nodeName.ToLower();
        for (int index = 0; index < this._items.Count; ++index)
        {
          if (this._items[index].Name.Equals(nodeName))
            return this._items[index];
        }
        return (HtmlNode) null;
      }
    }

    public int Count
    {
      get
      {
        return this._items.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public HtmlNode this[int index]
    {
      get
      {
        return this._items[index];
      }
      set
      {
        this._items[index] = value;
      }
    }

    public void Add(HtmlNode node)
    {
      this._items.Add(node);
    }

    public void Clear()
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        htmlNode.ParentNode = (HtmlNode) null;
        htmlNode.NextSibling = (HtmlNode) null;
        htmlNode.PreviousSibling = (HtmlNode) null;
      }
      this._items.Clear();
    }

    public bool Contains(HtmlNode item)
    {
      return this._items.Contains(item);
    }

    public void CopyTo(HtmlNode[] array, int arrayIndex)
    {
      this._items.CopyTo(array, arrayIndex);
    }

    IEnumerator<HtmlNode> IEnumerable<HtmlNode>.GetEnumerator()
    {
      return (IEnumerator<HtmlNode>) this._items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this._items.GetEnumerator();
    }

    public int IndexOf(HtmlNode item)
    {
      return this._items.IndexOf(item);
    }

    public void Insert(int index, HtmlNode node)
    {
      HtmlNode htmlNode1 = (HtmlNode) null;
      HtmlNode htmlNode2 = (HtmlNode) null;
      if (index > 0)
        htmlNode2 = this._items[index - 1];
      if (index < this._items.Count)
        htmlNode1 = this._items[index];
      this._items.Insert(index, node);
      if (htmlNode2 != null)
      {
        if (node == htmlNode2)
          throw new InvalidProgramException("Unexpected error.");
        htmlNode2._nextnode = node;
      }
      if (htmlNode1 != null)
        htmlNode1._prevnode = node;
      node._prevnode = htmlNode2;
      if (htmlNode1 == node)
        throw new InvalidProgramException("Unexpected error.");
      node._nextnode = htmlNode1;
      node._parentnode = this._parentnode;
    }

    public bool Remove(HtmlNode item)
    {
      this.RemoveAt(this._items.IndexOf(item));
      return true;
    }

    public void RemoveAt(int index)
    {
      HtmlNode htmlNode1 = (HtmlNode) null;
      HtmlNode htmlNode2 = (HtmlNode) null;
      HtmlNode htmlNode3 = this._items[index];
      if (index > 0)
        htmlNode2 = this._items[index - 1];
      if (index < this._items.Count - 1)
        htmlNode1 = this._items[index + 1];
      this._items.RemoveAt(index);
      if (htmlNode2 != null)
      {
        if (htmlNode1 == htmlNode2)
          throw new InvalidProgramException("Unexpected error.");
        htmlNode2._nextnode = htmlNode1;
      }
      if (htmlNode1 != null)
        htmlNode1._prevnode = htmlNode2;
      htmlNode3._prevnode = (HtmlNode) null;
      htmlNode3._nextnode = (HtmlNode) null;
      htmlNode3._parentnode = (HtmlNode) null;
    }

    public static HtmlNode FindFirst(HtmlNodeCollection items, string name)
    {
      foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>) items)
      {
        if (htmlNode.Name.ToLower().Contains(name))
          return htmlNode;
        if (htmlNode.HasChildNodes)
        {
          HtmlNode first = HtmlNodeCollection.FindFirst(htmlNode.ChildNodes, name);
          if (first != null)
            return first;
        }
      }
      return (HtmlNode) null;
    }

    public void Append(HtmlNode node)
    {
      HtmlNode htmlNode = (HtmlNode) null;
      if (this._items.Count > 0)
        htmlNode = this._items[this._items.Count - 1];
      this._items.Add(node);
      node._prevnode = htmlNode;
      node._nextnode = (HtmlNode) null;
      node._parentnode = this._parentnode;
      if (htmlNode == null)
        return;
      if (htmlNode == node)
        throw new InvalidProgramException("Unexpected error.");
      htmlNode._nextnode = node;
    }

    public HtmlNode FindFirst(string name)
    {
      return HtmlNodeCollection.FindFirst(this, name);
    }

    public int GetNodeIndex(HtmlNode node)
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        if (node == this._items[index])
          return index;
      }
      return -1;
    }

    public void Prepend(HtmlNode node)
    {
      HtmlNode htmlNode = (HtmlNode) null;
      if (this._items.Count > 0)
        htmlNode = this._items[0];
      this._items.Insert(0, node);
      if (node == htmlNode)
        throw new InvalidProgramException("Unexpected error.");
      node._nextnode = htmlNode;
      node._prevnode = (HtmlNode) null;
      node._parentnode = this._parentnode;
      if (htmlNode == null)
        return;
      htmlNode._prevnode = node;
    }

    public bool Remove(int index)
    {
      this.RemoveAt(index);
      return true;
    }

    public void Replace(int index, HtmlNode node)
    {
      HtmlNode htmlNode1 = (HtmlNode) null;
      HtmlNode htmlNode2 = (HtmlNode) null;
      HtmlNode htmlNode3 = this._items[index];
      if (index > 0)
        htmlNode2 = this._items[index - 1];
      if (index < this._items.Count - 1)
        htmlNode1 = this._items[index + 1];
      this._items[index] = node;
      if (htmlNode2 != null)
      {
        if (node == htmlNode2)
          throw new InvalidProgramException("Unexpected error.");
        htmlNode2._nextnode = node;
      }
      if (htmlNode1 != null)
        htmlNode1._prevnode = node;
      node._prevnode = htmlNode2;
      if (htmlNode1 == node)
        throw new InvalidProgramException("Unexpected error.");
      node._nextnode = htmlNode1;
      node._parentnode = this._parentnode;
      htmlNode3._prevnode = (HtmlNode) null;
      htmlNode3._nextnode = (HtmlNode) null;
      htmlNode3._parentnode = (HtmlNode) null;
    }

    public IEnumerable<HtmlNode> Descendants()
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        foreach (HtmlNode descendant in htmlNode.Descendants())
          yield return descendant;
      }
    }

    public IEnumerable<HtmlNode> Descendants(string name)
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        foreach (HtmlNode descendant in htmlNode.Descendants(name))
          yield return descendant;
      }
    }

    public IEnumerable<HtmlNode> Elements()
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) htmlNode.ChildNodes)
          yield return childNode;
      }
    }

    public IEnumerable<HtmlNode> Elements(string name)
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        foreach (HtmlNode element in htmlNode.Elements(name))
          yield return element;
      }
    }

    public IEnumerable<HtmlNode> Nodes()
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) htmlNode.ChildNodes)
          yield return childNode;
      }
    }
  }
}
