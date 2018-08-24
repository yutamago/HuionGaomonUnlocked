// Decompiled with JetBrains decompiler
// Type: HuionTablet.Entity.PointGravity
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Runtime.InteropServices;

namespace HuionTablet.Entity
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct PointGravity
    {
        public const int LEFT = 1;
        public const int RIGHT = 2;
        public const int TOP = 4;
        public const int BOTTOM = 8;
        public const int CENTER = 16;
    }
}