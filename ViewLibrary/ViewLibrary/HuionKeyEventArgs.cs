// Decompiled with JetBrains decompiler
// Type: Huion.HuionKeyEventArgs
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System;
using System.Windows.Forms;

namespace Huion
{
  public class HuionKeyEventArgs : EventArgs
  {
    private bool isControl;
    private bool isAlt;
    private bool isShift;
    private bool isWin;
    private bool mHandled;
    private Keys key;
    private string mKeyText;

    public HuionKeyEventArgs(Keys keyData, bool control, bool alt, bool shift, bool win)
    {
      this.key = keyData;
      this.isControl = control;
      this.isAlt = alt;
      this.isShift = shift;
      this.isWin = win;
    }

    public bool Alt
    {
      get
      {
        return this.isAlt;
      }
    }

    public bool Control
    {
      get
      {
        return this.isControl;
      }
    }

    public Keys KeyCode
    {
      get
      {
        return this.key;
      }
      set
      {
        this.key = value;
      }
    }

    public int KeyValue
    {
      get
      {
        return (int) this.key;
      }
    }

    public bool Shift
    {
      get
      {
        return this.isShift;
      }
    }

    public bool Window
    {
      get
      {
        return this.isWin;
      }
    }

    public bool Handled
    {
      get
      {
        return this.mHandled;
      }
      set
      {
        this.mHandled = value;
      }
    }

    public bool HasControls
    {
      get
      {
        if (!this.isControl && !this.isAlt && !this.isShift)
          return this.isWin;
        return true;
      }
    }

    public string KeyText
    {
      get
      {
        return this.mKeyText;
      }
      set
      {
        this.mKeyText = value;
      }
    }
  }
}
