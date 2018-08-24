// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.RatioEventArgs
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
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