// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.HuionClickableButton
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

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