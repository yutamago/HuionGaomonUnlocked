﻿// Decompiled with JetBrains decompiler
// Type: HuionTablet.Entity.HuionKeyLayout
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Drawing;

namespace HuionTablet.Entity
{
    public class HuionKeyLayout
    {
        public Point Center;
        public Rectangle InnerRect;
        public HNStruct.HNEkey Key;
        public int KeyIndex;
        public HuionKeyType KeyType;
        public HNStruct.HNEkey[] MutliKeys;
        public Rectangle Rect;

        public bool mouseEnter(int x, int y)
        {
            return x >= this.Rect.X && x <= this.Rect.X + this.Rect.Width &&
                   (y >= this.Rect.Y && y <= this.Rect.Y + this.Rect.Height) &&
                   (!(this.InnerRect != Rectangle.Empty) || x < this.InnerRect.X ||
                    (x > this.InnerRect.X + this.InnerRect.Width || y < this.InnerRect.Y) ||
                    y > this.InnerRect.Y + this.InnerRect.Height);
        }

        public static bool operator +(HuionKeyLayout left, int value)
        {
            if (left != null)
            {
                left.Rect.X += value;
                left.Rect.Y += value;
                left.Rect.Width += value;
                left.Rect.Height += value;
            }

            return false;
        }

        public static bool operator -(HuionKeyLayout left, int value)
        {
            if (left != null)
            {
                left.Rect.X -= value;
                left.Rect.Y -= value;
                left.Rect.Width -= value;
                left.Rect.Height -= value;
            }

            return false;
        }
    }
}