// Decompiled with JetBrains decompiler
// Type: Huion.KeyboardHookStruct
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

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
