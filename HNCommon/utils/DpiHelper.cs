﻿// Decompiled with JetBrains decompiler
// Type: HuionTablet.DpiHelper
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;

namespace HuionTablet
{
    public class DpiHelper
    {
        private static DpiHelper mInstance;
        private float xDpi = 1f;
        private float yDpi = 1f;

        private DpiHelper(Graphics g)
        {
            this.xDpi = g.DpiX / 96f;
            this.yDpi = g.DpiY / 96f;
        }

        public float XDpi
        {
            get { return this.xDpi; }
        }

        public float YDpi
        {
            get { return this.yDpi; }
        }

        public static DpiHelper createInstance(Graphics g)
        {
            if (mInstance == null)
                mInstance = new DpiHelper(g);
            return mInstance;
        }

        public static DpiHelper getInstance()
        {
            return mInstance;
        }

        public int DpiMatrix(int x)
        {
            return (int) Math.Round((double) x * (double) this.xDpi);
        }

        public void DpiMatrix(ref int x, ref int y)
        {
            x = (int) Math.Round((double) x * (double) this.xDpi);
            y = (int) Math.Round((double) y * (double) this.xDpi);
        }
    }
}