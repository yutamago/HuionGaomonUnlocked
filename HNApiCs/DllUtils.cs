// Decompiled with JetBrains decompiler
// Type: Huion.DllUtils
// Assembly: HNApiCs, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 4957B58E-8324-4AC9-B678-1AA8EE08DC3D
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNApiCs.dll

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
