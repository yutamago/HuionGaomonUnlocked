// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.HuionClickableButton
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System.Windows.Forms;

namespace HuionTablet.view
{
    public class HuionClickableButton : Button, IClickable
    {
        private bool isClickable = true;

        public bool Clickable
        {
            get { return this.isClickable; }
            set { this.isClickable = value; }
        }
    }
}