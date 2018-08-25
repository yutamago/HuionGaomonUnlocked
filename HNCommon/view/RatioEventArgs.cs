// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.RatioEventArgs
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;

namespace HuionTablet.view
{
    public class RatioEventArgs : EventArgs
    {
        private HNStruct.HNRectRatio mRatio;

        public HNStruct.HNRectRatio Ratio
        {
            get { return this.mRatio; }
            set { this.mRatio = value; }
        }

        public static RatioEventArgs getInstance(HNStruct.HNRectRatio ratio)
        {
            return new RatioEventArgs() {Ratio = ratio};
        }
    }
}