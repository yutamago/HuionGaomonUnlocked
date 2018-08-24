// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlNode
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace HtmlAgilityPack
{
    [DebuggerDisplay("Name: {OriginalName}}")]
    public class HtmlNode : IXPathNavigable
    {
        public static readonly string HtmlNodeTypeNameComment = "#comment";
        public static readonly string HtmlNodeTypeNameDocument = "#document";
        public static readonly string HtmlNodeTypeNameText = "#text";
        public static Dictionary<string, HtmlElementFlag> ElementsFlags = new Dictionary<string, HtmlElementFlag>();
        internal HtmlAttributeCollection _attributes;
        internal HtmlNodeCollection _childnodes;
        internal HtmlNode _endnode;
        internal bool _innerchanged;
        internal string _innerhtml;
        internal int _innerlength;
        internal int _innerstartindex;
        internal int _line;
        internal int _lineposition;
        private string _name;
        internal int _namelength;
        internal int _namestartindex;
        internal HtmlNode _nextnode;
        internal HtmlNodeType _nodetype;
        private string _optimizedName;
        internal bool _outerchanged;
        internal string _outerhtml;
        internal int _outerlength;
        internal int _outerstartindex;
        internal HtmlDocument _ownerdocument;
        internal HtmlNode _parentnode;
        internal HtmlNode _prevnode;
        internal HtmlNode _prevwithsamename;
        internal bool _starttag;
        internal int _streamposition;

        static HtmlNode()
        {
            ElementsFlags.Add("script", HtmlElementFlag.CData);
            ElementsFlags.Add("style", HtmlElementFlag.CData);
            ElementsFlags.Add("noxhtml", HtmlElementFlag.CData);
            ElementsFlags.Add("base", HtmlElementFlag.Empty);
            ElementsFlags.Add("link", HtmlElementFlag.Empty);
            ElementsFlags.Add("meta", HtmlElementFlag.Empty);
            ElementsFlags.Add("isindex", HtmlElementFlag.Empty);
            ElementsFlags.Add("hr", HtmlElementFlag.Empty);
            ElementsFlags.Add("col", HtmlElementFlag.Empty);
            ElementsFlags.Add("img", HtmlElementFlag.Empty);
            ElementsFlags.Add("param", HtmlElementFlag.Empty);
            ElementsFlags.Add("embed", HtmlElementFlag.Empty);
            ElementsFlags.Add("frame", HtmlElementFlag.Empty);
            ElementsFlags.Add("wbr", HtmlElementFlag.Empty);
            ElementsFlags.Add("bgsound", HtmlElementFlag.Empty);
            ElementsFlags.Add("spacer", HtmlElementFlag.Empty);
            ElementsFlags.Add("keygen", HtmlElementFlag.Empty);
            ElementsFlags.Add("area", HtmlElementFlag.Empty);
            ElementsFlags.Add("input", HtmlElementFlag.Empty);
            ElementsFlags.Add("basefont", HtmlElementFlag.Empty);
            ElementsFlags.Add("form", HtmlElementFlag.Empty | HtmlElementFlag.CanOverlap);
            ElementsFlags.Add("option", HtmlElementFlag.Empty);
            ElementsFlags.Add("br", HtmlElementFlag.Empty | HtmlElementFlag.Closed);
            ElementsFlags.Add("p", HtmlElementFlag.Empty | HtmlElementFlag.Closed);
        }

        public HtmlNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
        {
            this._nodetype = type;
            this._ownerdocument = ownerdocument;
            this._outerstartindex = index;
            switch (type)
            {
                case HtmlNodeType.Document:
                    this.Name = HtmlNodeTypeNameDocument;
                    this._endnode = this;
                    break;
                case HtmlNodeType.Comment:
                    this.Name = HtmlNodeTypeNameComment;
                    this._endnode = this;
                    break;
                case HtmlNodeType.Text:
                    this.Name = HtmlNodeTypeNameText;
                    this._endnode = this;
                    break;
            }

            if (this._ownerdocument.Openednodes != null && !this.Closed && -1 != index)
                this._ownerdocument.Openednodes.Add(index, this);
            if (-1 != index || type == HtmlNodeType.Comment || type == HtmlNodeType.Text)
                return;
            this._outerchanged = true;
            this._innerchanged = true;
        }

        public HtmlAttributeCollection Attributes
        {
            get
            {
                if (!this.HasAttributes)
                    this._attributes = new HtmlAttributeCollection(this);
                return this._attributes;
            }
            internal set { this._attributes = value; }
        }

        public HtmlNodeCollection ChildNodes
        {
            get { return this._childnodes ?? (this._childnodes = new HtmlNodeCollection(this)); }
            internal set { this._childnodes = value; }
        }

        public bool Closed
        {
            get { return this._endnode != null; }
        }

        public HtmlAttributeCollection ClosingAttributes
        {
            get
            {
                if (this.HasClosingAttributes)
                    return this._endnode.Attributes;
                return new HtmlAttributeCollection(this);
            }
        }

        internal HtmlNode EndNode
        {
            get { return this._endnode; }
        }

        public HtmlNode FirstChild
        {
            get
            {
                if (this.HasChildNodes)
                    return this._childnodes[0];
                return (HtmlNode) null;
            }
        }

        public bool HasAttributes
        {
            get { return this._attributes != null && this._attributes.Count > 0; }
        }

        public bool HasChildNodes
        {
            get { return this._childnodes != null && this._childnodes.Count > 0; }
        }

        public bool HasClosingAttributes
        {
            get
            {
                return this._endnode != null && this._endnode != this &&
                       (this._endnode._attributes != null && this._endnode._attributes.Count > 0);
            }
        }

        public string Id
        {
            get
            {
                if (this._ownerdocument.Nodesid == null)
                    throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
                return this.GetId();
            }
            set
            {
                if (this._ownerdocument.Nodesid == null)
                    throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.SetId(value);
            }
        }

        public virtual string InnerHtml
        {
            get
            {
                if (this._innerchanged)
                {
                    this._innerhtml = this.WriteContentTo();
                    this._innerchanged = false;
                    return this._innerhtml;
                }

                if (this._innerhtml != null)
                    return this._innerhtml;
                if (this._innerstartindex < 0)
                    return string.Empty;
                return this._ownerdocument.Text.Substring(this._innerstartindex, this._innerlength);
            }
            set
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(value);
                this.RemoveAllChildren();
                this.AppendChildren(htmlDocument.DocumentNode.ChildNodes);
            }
        }

        public virtual string InnerText
        {
            get
            {
                if (this._nodetype == HtmlNodeType.Text)
                    return ((HtmlTextNode) this).Text;
                if (this._nodetype == HtmlNodeType.Comment)
                    return ((HtmlCommentNode) this).Comment;
                if (!this.HasChildNodes)
                    return string.Empty;
                string str = (string) null;
                foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
                    str += childNode.InnerText;
                return str;
            }
        }

        public HtmlNode LastChild
        {
            get
            {
                if (this.HasChildNodes)
                    return this._childnodes[this._childnodes.Count - 1];
                return (HtmlNode) null;
            }
        }

        public int Line
        {
            get { return this._line; }
            internal set { this._line = value; }
        }

        public int LinePosition
        {
            get { return this._lineposition; }
            internal set { this._lineposition = value; }
        }

        public string Name
        {
            get
            {
                if (this._optimizedName == null)
                {
                    if (this._name == null)
                        this.Name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
                    this._optimizedName = this._name != null ? this._name.ToLower() : string.Empty;
                }

                return this._optimizedName;
            }
            set
            {
                this._name = value;
                this._optimizedName = (string) null;
            }
        }

        public HtmlNode NextSibling
        {
            get { return this._nextnode; }
            internal set { this._nextnode = value; }
        }

        public HtmlNodeType NodeType
        {
            get { return this._nodetype; }
            internal set { this._nodetype = value; }
        }

        public string OriginalName
        {
            get { return this._name; }
        }

        public virtual string OuterHtml
        {
            get
            {
                if (this._outerchanged)
                {
                    this._outerhtml = this.WriteTo();
                    this._outerchanged = false;
                    return this._outerhtml;
                }

                if (this._outerhtml != null)
                    return this._outerhtml;
                if (this._outerstartindex < 0)
                    return string.Empty;
                return this._ownerdocument.Text.Substring(this._outerstartindex, this._outerlength);
            }
        }

        public HtmlDocument OwnerDocument
        {
            get { return this._ownerdocument; }
            internal set { this._ownerdocument = value; }
        }

        public HtmlNode ParentNode
        {
            get { return this._parentnode; }
            internal set { this._parentnode = value; }
        }

        public HtmlNode PreviousSibling
        {
            get { return this._prevnode; }
            internal set { this._prevnode = value; }
        }

        public int StreamPosition
        {
            get { return this._streamposition; }
        }

        public string XPath
        {
            get
            {
                return (this.ParentNode == null || this.ParentNode.NodeType == HtmlNodeType.Document
                           ? "/"
                           : this.ParentNode.XPath + "/") + this.GetRelativeXpath();
            }
        }

        public XPathNavigator CreateNavigator()
        {
            return (XPathNavigator) new HtmlNodeNavigator(this.OwnerDocument, this);
        }

        public XPathNavigator CreateRootNavigator()
        {
            return (XPathNavigator) new HtmlNodeNavigator(this.OwnerDocument, this.OwnerDocument.DocumentNode);
        }

        public HtmlNodeCollection SelectNodes(string xpath)
        {
            HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection((HtmlNode) null);
            XPathNodeIterator xpathNodeIterator = new HtmlNodeNavigator(this.OwnerDocument, this).Select(xpath);
            while (xpathNodeIterator.MoveNext())
            {
                HtmlNodeNavigator current = (HtmlNodeNavigator) xpathNodeIterator.Current;
                htmlNodeCollection.Add(current.CurrentNode);
            }

            if (htmlNodeCollection.Count == 0)
                return (HtmlNodeCollection) null;
            return htmlNodeCollection;
        }

        public HtmlNode SelectSingleNode(string xpath)
        {
            if (xpath == null)
                throw new ArgumentNullException(nameof(xpath));
            XPathNodeIterator xpathNodeIterator = new HtmlNodeNavigator(this.OwnerDocument, this).Select(xpath);
            if (!xpathNodeIterator.MoveNext())
                return (HtmlNode) null;
            return ((HtmlNodeNavigator) xpathNodeIterator.Current).CurrentNode;
        }

        public static bool CanOverlapElement(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (!ElementsFlags.ContainsKey(name.ToLower()))
                return false;
            return (ElementsFlags[name.ToLower()] & HtmlElementFlag.CanOverlap) != (HtmlElementFlag) 0;
        }

        public static HtmlNode CreateNode(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument.DocumentNode.FirstChild;
        }

        public static bool IsCDataElement(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (!ElementsFlags.ContainsKey(name.ToLower()))
                return false;
            return (ElementsFlags[name.ToLower()] & HtmlElementFlag.CData) != (HtmlElementFlag) 0;
        }

        public static bool IsClosedElement(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (!ElementsFlags.ContainsKey(name.ToLower()))
                return false;
            return (ElementsFlags[name.ToLower()] & HtmlElementFlag.Closed) != (HtmlElementFlag) 0;
        }

        public static bool IsEmptyElement(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (name.Length == 0 || '!' == name[0] || '?' == name[0])
                return true;
            if (!ElementsFlags.ContainsKey(name.ToLower()))
                return false;
            return (ElementsFlags[name.ToLower()] & HtmlElementFlag.Empty) != (HtmlElementFlag) 0;
        }

        public static bool IsOverlappedClosingElement(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (text.Length <= 4 || text[0] != '<' || (text[text.Length - 1] != '>' || text[1] != '/'))
                return false;
            return CanOverlapElement(text.Substring(2, text.Length - 3));
        }

        public IEnumerable<HtmlNode> Ancestors()
        {
            for (HtmlNode node = this.ParentNode; node.ParentNode != null; node = node.ParentNode)
                yield return node.ParentNode;
        }

        public IEnumerable<HtmlNode> Ancestors(string name)
        {
            for (HtmlNode n = this.ParentNode; n != null; n = n.ParentNode)
            {
                if (n.Name == name)
                    yield return n;
            }
        }

        public IEnumerable<HtmlNode> AncestorsAndSelf()
        {
            for (HtmlNode n = this; n != null; n = n.ParentNode)
                yield return n;
        }

        public IEnumerable<HtmlNode> AncestorsAndSelf(string name)
        {
            for (HtmlNode n = this; n != null; n = n.ParentNode)
            {
                if (n.Name == name)
                    yield return n;
            }
        }

        public HtmlNode AppendChild(HtmlNode newChild)
        {
            if (newChild == null)
                throw new ArgumentNullException(nameof(newChild));
            this.ChildNodes.Append(newChild);
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public void AppendChildren(HtmlNodeCollection newChildren)
        {
            if (newChildren == null)
                throw new ArgumentNullException(nameof(newChildren));
            foreach (HtmlNode newChild in (IEnumerable<HtmlNode>) newChildren)
                this.AppendChild(newChild);
        }

        public IEnumerable<HtmlAttribute> ChildAttributes(string name)
        {
            return this.Attributes.AttributesWithName(name);
        }

        public HtmlNode Clone()
        {
            return this.CloneNode(true);
        }

        public HtmlNode CloneNode(string newName)
        {
            return this.CloneNode(newName, true);
        }

        public HtmlNode CloneNode(string newName, bool deep)
        {
            if (newName == null)
                throw new ArgumentNullException(nameof(newName));
            HtmlNode htmlNode = this.CloneNode(deep);
            htmlNode.Name = newName;
            return htmlNode;
        }

        public HtmlNode CloneNode(bool deep)
        {
            HtmlNode node = this._ownerdocument.CreateNode(this._nodetype);
            node.Name = this.Name;
            switch (this._nodetype)
            {
                case HtmlNodeType.Comment:
                    ((HtmlCommentNode) node).Comment = ((HtmlCommentNode) this).Comment;
                    return node;
                case HtmlNodeType.Text:
                    ((HtmlTextNode) node).Text = ((HtmlTextNode) this).Text;
                    return node;
                default:
                    if (this.HasAttributes)
                    {
                        foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._attributes)
                        {
                            HtmlAttribute newAttribute = attribute.Clone();
                            node.Attributes.Append(newAttribute);
                        }
                    }

                    if (this.HasClosingAttributes)
                    {
                        node._endnode = this._endnode.CloneNode(false);
                        foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._endnode._attributes)
                        {
                            HtmlAttribute newAttribute = attribute.Clone();
                            node._endnode._attributes.Append(newAttribute);
                        }
                    }

                    if (!deep || !this.HasChildNodes)
                        return node;
                    foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
                    {
                        HtmlNode newChild = childnode.Clone();
                        node.AppendChild(newChild);
                    }

                    return node;
            }
        }

        public void CopyFrom(HtmlNode node)
        {
            this.CopyFrom(node, true);
        }

        public void CopyFrom(HtmlNode node, bool deep)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            this.Attributes.RemoveAll();
            if (node.HasAttributes)
            {
                foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) node.Attributes)
                    this.SetAttributeValue(attribute.Name, attribute.Value);
            }

            if (deep)
                return;
            this.RemoveAllChildren();
            if (!node.HasChildNodes)
                return;
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) node.ChildNodes)
                this.AppendChild(childNode.CloneNode(true));
        }

        [Obsolete("Use Descendants() instead, the results of this function will change in a future version")]
        public IEnumerable<HtmlNode> DescendantNodes()
        {
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
            {
                yield return childNode;
                foreach (HtmlNode descendantNode in childNode.DescendantNodes())
                    yield return descendantNode;
            }
        }

        [Obsolete("Use DescendantsAndSelf() instead, the results of this function will change in a future version")]
        public IEnumerable<HtmlNode> DescendantNodesAndSelf()
        {
            return this.DescendantsAndSelf();
        }

        public IEnumerable<HtmlNode> Descendants()
        {
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
            {
                yield return childNode;
                foreach (HtmlNode descendant in childNode.Descendants())
                    yield return descendant;
            }
        }

        public IEnumerable<HtmlNode> Descendants(string name)
        {
            name = name.ToLowerInvariant();
            foreach (HtmlNode descendant in this.Descendants())
            {
                if (descendant.Name.Equals(name))
                    yield return descendant;
            }
        }

        public IEnumerable<HtmlNode> DescendantsAndSelf()
        {
            yield return this;
            foreach (HtmlNode descendant in this.Descendants())
            {
                HtmlNode el = descendant;
                if (el != null)
                    yield return el;
            }
        }

        public IEnumerable<HtmlNode> DescendantsAndSelf(string name)
        {
            yield return this;
            foreach (HtmlNode descendant in this.Descendants())
            {
                if (descendant.Name == name)
                    yield return descendant;
            }
        }

        public HtmlNode Element(string name)
        {
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
            {
                if (childNode.Name == name)
                    return childNode;
            }

            return (HtmlNode) null;
        }

        public IEnumerable<HtmlNode> Elements(string name)
        {
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
            {
                if (childNode.Name == name)
                    yield return childNode;
            }
        }

        public string GetAttributeValue(string name, string def)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (!this.HasAttributes)
                return def;
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
                return def;
            return attribute.Value;
        }

        public int GetAttributeValue(string name, int def)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (!this.HasAttributes)
                return def;
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
                return def;
            try
            {
                return Convert.ToInt32(attribute.Value);
            }
            catch
            {
                return def;
            }
        }

        public bool GetAttributeValue(string name, bool def)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (!this.HasAttributes)
                return def;
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
                return def;
            try
            {
                return Convert.ToBoolean(attribute.Value);
            }
            catch
            {
                return def;
            }
        }

        public HtmlNode InsertAfter(HtmlNode newChild, HtmlNode refChild)
        {
            if (newChild == null)
                throw new ArgumentNullException(nameof(newChild));
            if (refChild == null)
                return this.PrependChild(newChild);
            if (newChild == refChild)
                return newChild;
            int num = -1;
            if (this._childnodes != null)
                num = this._childnodes[refChild];
            if (num == -1)
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            if (this._childnodes != null)
                this._childnodes.Insert(num + 1, newChild);
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public HtmlNode InsertBefore(HtmlNode newChild, HtmlNode refChild)
        {
            if (newChild == null)
                throw new ArgumentNullException(nameof(newChild));
            if (refChild == null)
                return this.AppendChild(newChild);
            if (newChild == refChild)
                return newChild;
            int index = -1;
            if (this._childnodes != null)
                index = this._childnodes[refChild];
            if (index == -1)
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            if (this._childnodes != null)
                this._childnodes.Insert(index, newChild);
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public HtmlNode PrependChild(HtmlNode newChild)
        {
            if (newChild == null)
                throw new ArgumentNullException(nameof(newChild));
            this.ChildNodes.Prepend(newChild);
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public void PrependChildren(HtmlNodeCollection newChildren)
        {
            if (newChildren == null)
                throw new ArgumentNullException(nameof(newChildren));
            foreach (HtmlNode newChild in (IEnumerable<HtmlNode>) newChildren)
                this.PrependChild(newChild);
        }

        public void Remove()
        {
            if (this.ParentNode == null)
                return;
            this.ParentNode.ChildNodes.Remove(this);
        }

        public void RemoveAll()
        {
            this.RemoveAllChildren();
            if (this.HasAttributes)
                this._attributes.Clear();
            if (this._endnode != null && this._endnode != this && this._endnode._attributes != null)
                this._endnode._attributes.Clear();
            this._outerchanged = true;
            this._innerchanged = true;
        }

        public void RemoveAllChildren()
        {
            if (!this.HasChildNodes)
                return;
            if (this._ownerdocument.OptionUseIdAttribute)
            {
                foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
                    this._ownerdocument.SetIdForNode((HtmlNode) null, childnode.GetId());
            }

            this._childnodes.Clear();
            this._outerchanged = true;
            this._innerchanged = true;
        }

        public HtmlNode RemoveChild(HtmlNode oldChild)
        {
            if (oldChild == null)
                throw new ArgumentNullException(nameof(oldChild));
            int index = -1;
            if (this._childnodes != null)
                index = this._childnodes[oldChild];
            if (index == -1)
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            if (this._childnodes != null)
                this._childnodes.Remove(index);
            this._ownerdocument.SetIdForNode((HtmlNode) null, oldChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return oldChild;
        }

        public HtmlNode RemoveChild(HtmlNode oldChild, bool keepGrandChildren)
        {
            if (oldChild == null)
                throw new ArgumentNullException(nameof(oldChild));
            if (oldChild._childnodes != null && keepGrandChildren)
            {
                HtmlNode previousSibling = oldChild.PreviousSibling;
                foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) oldChild._childnodes)
                    this.InsertAfter(childnode, previousSibling);
            }

            this.RemoveChild(oldChild);
            this._outerchanged = true;
            this._innerchanged = true;
            return oldChild;
        }

        public HtmlNode ReplaceChild(HtmlNode newChild, HtmlNode oldChild)
        {
            if (newChild == null)
                return this.RemoveChild(oldChild);
            if (oldChild == null)
                return this.AppendChild(newChild);
            int index = -1;
            if (this._childnodes != null)
                index = this._childnodes[oldChild];
            if (index == -1)
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            if (this._childnodes != null)
                this._childnodes.Replace(index, newChild);
            this._ownerdocument.SetIdForNode((HtmlNode) null, oldChild.GetId());
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public HtmlAttribute SetAttributeValue(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
                return this.Attributes.Append(this._ownerdocument.CreateAttribute(name, value));
            attribute.Value = value;
            return attribute;
        }

        public void WriteContentTo(TextWriter outText)
        {
            if (this._childnodes == null)
                return;
            foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
                childnode.WriteTo(outText);
        }

        public string WriteContentTo()
        {
            StringWriter stringWriter = new StringWriter();
            this.WriteContentTo((TextWriter) stringWriter);
            stringWriter.Flush();
            return stringWriter.ToString();
        }

        public void WriteTo(TextWriter outText)
        {
            switch (this._nodetype)
            {
                case HtmlNodeType.Document:
                    if (this._ownerdocument.OptionOutputAsXml)
                    {
                        outText.Write("<?xml version=\"1.0\" encoding=\"" +
                                      this._ownerdocument.GetOutEncoding().BodyName + "\"?>");
                        if (this._ownerdocument.DocumentNode.HasChildNodes)
                        {
                            int count = this._ownerdocument.DocumentNode._childnodes.Count;
                            if (count > 0)
                            {
                                if (this._ownerdocument.GetXmlDeclaration() != null)
                                    --count;
                                if (count > 1)
                                {
                                    if (this._ownerdocument.OptionOutputUpperCase)
                                    {
                                        outText.Write("<SPAN>");
                                        this.WriteContentTo(outText);
                                        outText.Write("</SPAN>");
                                        break;
                                    }

                                    outText.Write("<span>");
                                    this.WriteContentTo(outText);
                                    outText.Write("</span>");
                                    break;
                                }
                            }
                        }
                    }

                    this.WriteContentTo(outText);
                    break;
                case HtmlNodeType.Element:
                    string name = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpper() : this.Name;
                    if (this._ownerdocument.OptionOutputOriginalCase)
                        name = this.OriginalName;
                    if (this._ownerdocument.OptionOutputAsXml)
                    {
                        if (name.Length <= 0 || name[0] == '?' || name.Trim().Length == 0)
                            break;
                        name = HtmlDocument.GetXmlName(name);
                    }

                    outText.Write("<" + name);
                    this.WriteAttributes(outText, false);
                    if (this.HasChildNodes)
                    {
                        outText.Write(">");
                        bool flag = false;
                        if (this._ownerdocument.OptionOutputAsXml && IsCDataElement(this.Name))
                        {
                            flag = true;
                            outText.Write("\r\n//<![CDATA[\r\n");
                        }

                        if (flag)
                        {
                            if (this.HasChildNodes)
                                this.ChildNodes[0].WriteTo(outText);
                            outText.Write("\r\n//]]>//\r\n");
                        }
                        else
                            this.WriteContentTo(outText);

                        outText.Write("</" + name);
                        if (!this._ownerdocument.OptionOutputAsXml)
                            this.WriteAttributes(outText, true);
                        outText.Write(">");
                        break;
                    }

                    if (IsEmptyElement(this.Name))
                    {
                        if (this._ownerdocument.OptionWriteEmptyNodes || this._ownerdocument.OptionOutputAsXml)
                        {
                            outText.Write(" />");
                            break;
                        }

                        if (this.Name.Length > 0 && this.Name[0] == '?')
                            outText.Write("?");
                        outText.Write(">");
                        break;
                    }

                    outText.Write("></" + name + ">");
                    break;
                case HtmlNodeType.Comment:
                    string comment = ((HtmlCommentNode) this).Comment;
                    if (this._ownerdocument.OptionOutputAsXml)
                    {
                        outText.Write("<!--" + GetXmlComment((HtmlCommentNode) this) + " -->");
                        break;
                    }

                    outText.Write(comment);
                    break;
                case HtmlNodeType.Text:
                    string text = ((HtmlTextNode) this).Text;
                    outText.Write(this._ownerdocument.OptionOutputAsXml ? HtmlDocument.HtmlEncode(text) : text);
                    break;
            }
        }

        public void WriteTo(XmlWriter writer)
        {
            switch (this._nodetype)
            {
                case HtmlNodeType.Document:
                    writer.WriteProcessingInstruction("xml",
                        "version=\"1.0\" encoding=\"" + this._ownerdocument.GetOutEncoding().BodyName + "\"");
                    if (!this.HasChildNodes)
                        break;
                    using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>) this.ChildNodes).GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                            enumerator.Current.WriteTo(writer);
                        break;
                    }
                case HtmlNodeType.Element:
                    string localName = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpper() : this.Name;
                    if (this._ownerdocument.OptionOutputOriginalCase)
                        localName = this.OriginalName;
                    writer.WriteStartElement(localName);
                    WriteAttributes(writer, this);
                    if (this.HasChildNodes)
                    {
                        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
                            childNode.WriteTo(writer);
                    }

                    writer.WriteEndElement();
                    break;
                case HtmlNodeType.Comment:
                    writer.WriteComment(GetXmlComment((HtmlCommentNode) this));
                    break;
                case HtmlNodeType.Text:
                    string text = ((HtmlTextNode) this).Text;
                    writer.WriteString(text);
                    break;
            }
        }

        public string WriteTo()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                this.WriteTo((TextWriter) stringWriter);
                stringWriter.Flush();
                return stringWriter.ToString();
            }
        }

        internal static string GetXmlComment(HtmlCommentNode comment)
        {
            string comment1 = comment.Comment;
            return comment1.Substring(4, comment1.Length - 7).Replace("--", " - -");
        }

        internal static void WriteAttributes(XmlWriter writer, HtmlNode node)
        {
            if (!node.HasAttributes)
                return;
            foreach (HtmlAttribute htmlAttribute in node.Attributes.Hashitems.Values)
                writer.WriteAttributeString(htmlAttribute.XmlName, htmlAttribute.Value);
        }

        internal void CloseNode(HtmlNode endnode)
        {
            if (!this._ownerdocument.OptionAutoCloseOnEnd && this._childnodes != null)
            {
                foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
                {
                    if (!childnode.Closed)
                    {
                        HtmlNode endnode1 = new HtmlNode(this.NodeType, this._ownerdocument, -1);
                        endnode1._endnode = endnode1;
                        childnode.CloseNode(endnode1);
                    }
                }
            }

            if (this.Closed)
                return;
            this._endnode = endnode;
            if (this._ownerdocument.Openednodes != null)
                this._ownerdocument.Openednodes.Remove(this._outerstartindex);
            if (Utilities.GetDictionaryValueOrNull<string, HtmlNode>(this._ownerdocument.Lastnodes, this.Name) == this)
            {
                this._ownerdocument.Lastnodes.Remove(this.Name);
                this._ownerdocument.UpdateLastParentNode();
            }

            if (endnode == this)
                return;
            this._innerstartindex = this._outerstartindex + this._outerlength;
            this._innerlength = endnode._outerstartindex - this._innerstartindex;
            this._outerlength = endnode._outerstartindex + endnode._outerlength - this._outerstartindex;
        }

        internal string GetId()
        {
            HtmlAttribute attribute = this.Attributes["id"];
            if (attribute != null)
                return attribute.Value;
            return string.Empty;
        }

        internal void SetId(string id)
        {
            HtmlAttribute htmlAttribute =
                this.Attributes[nameof(id)] ?? this._ownerdocument.CreateAttribute(nameof(id));
            htmlAttribute.Value = id;
            this._ownerdocument.SetIdForNode(this, htmlAttribute.Value);
            this._outerchanged = true;
        }

        internal void WriteAttribute(TextWriter outText, HtmlAttribute att)
        {
            string str1 = att.QuoteType == AttributeValueQuote.DoubleQuote ? "\"" : "'";
            if (this._ownerdocument.OptionOutputAsXml)
            {
                string str2 = this._ownerdocument.OptionOutputUpperCase ? att.XmlName.ToUpper() : att.XmlName;
                if (this._ownerdocument.OptionOutputOriginalCase)
                    str2 = att.OriginalName;
                outText.Write(" " + str2 + "=" + str1 + HtmlDocument.HtmlEncode(att.XmlValue) + str1);
            }
            else
            {
                string str2 = this._ownerdocument.OptionOutputUpperCase ? att.Name.ToUpper() : att.Name;
                if (att.Name.Length >= 4 && att.Name[0] == '<' &&
                    (att.Name[1] == '%' && att.Name[att.Name.Length - 1] == '>') &&
                    att.Name[att.Name.Length - 2] == '%')
                    outText.Write(" " + str2);
                else if (this._ownerdocument.OptionOutputOptimizeAttributeValues)
                {
                    if (att.Value.IndexOfAny(new char[4] {'\n', '\r', '\t', ' '}) < 0)
                        outText.Write(" " + str2 + "=" + att.Value);
                    else
                        outText.Write(" " + str2 + "=" + str1 + att.Value + str1);
                }
                else
                    outText.Write(" " + str2 + "=" + str1 + att.Value + str1);
            }
        }

        internal void WriteAttributes(TextWriter outText, bool closing)
        {
            if (this._ownerdocument.OptionOutputAsXml)
            {
                if (this._attributes == null)
                    return;
                foreach (HtmlAttribute att in this._attributes.Hashitems.Values)
                    this.WriteAttribute(outText, att);
            }
            else if (!closing)
            {
                if (this._attributes != null)
                {
                    foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._attributes)
                        this.WriteAttribute(outText, attribute);
                }

                if (!this._ownerdocument.OptionAddDebuggingAttributes)
                    return;
                this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_closed", this.Closed.ToString()));
                this.WriteAttribute(outText,
                    this._ownerdocument.CreateAttribute("_children", this.ChildNodes.Count.ToString()));
                int num = 0;
                foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
                {
                    this.WriteAttribute(outText,
                        this._ownerdocument.CreateAttribute("_child_" + (object) num, childNode.Name));
                    ++num;
                }
            }
            else
            {
                if (this._endnode == null || this._endnode._attributes == null || this._endnode == this)
                    return;
                foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._endnode._attributes)
                    this.WriteAttribute(outText, attribute);
                if (!this._ownerdocument.OptionAddDebuggingAttributes)
                    return;
                this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_closed", this.Closed.ToString()));
                this.WriteAttribute(outText,
                    this._ownerdocument.CreateAttribute("_children", this.ChildNodes.Count.ToString()));
            }
        }

        private string GetRelativeXpath()
        {
            if (this.ParentNode == null)
                return this.Name;
            if (this.NodeType == HtmlNodeType.Document)
                return string.Empty;
            int num = 1;
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ParentNode.ChildNodes)
            {
                if (!(childNode.Name != this.Name))
                {
                    if (childNode != this)
                        ++num;
                    else
                        break;
                }
            }

            return this.Name + "[" + (object) num + "]";
        }
    }
}