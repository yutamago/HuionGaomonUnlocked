// Decompiled with JetBrains decompiler
// Type: Huion.DllUtils
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System.Runtime.InteropServices;

namespace Huion
{
    public class DllUtils
    {
        public const int SM_XVIRTUALSCREEN = 76;
        public const int SM_YVIRTUALSCREEN = 77;
        public const int SM_CXVIRTUALSCREEN = 78;
        public const int SM_CYVIRTUALSCREEN = 79;

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSystemMetrics(int index);
    }
}