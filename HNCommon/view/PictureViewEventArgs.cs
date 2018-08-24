// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.PictureViewEventArgs
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;

namespace HuionTablet.view
{
  public class PictureViewEventArgs : EventArgs
  {
    private int mRotate;

    public static PictureViewEventArgs getInstance(int rotate)
    {
      return new PictureViewEventArgs() { Rotate = rotate };
    }

    public int Rotate
    {
      set
      {
        this.mRotate = value;
      }
      get
      {
        return this.mRotate;
      }
    }
  }
}
