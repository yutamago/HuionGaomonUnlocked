// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.PictureViewEventArgs
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
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
