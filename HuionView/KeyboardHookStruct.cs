// Decompiled with JetBrains decompiler
// Type: Huion.KeyboardHookStruct
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

using System.Runtime.InteropServices;

namespace Huion
{
  [StructLayout(LayoutKind.Sequential)]
  public class KeyboardHookStruct
  {
    public int vkCode;
    public int scanCode;
    public int flags;
    public int time;
    public int dwExtraInfo;
  }
}
