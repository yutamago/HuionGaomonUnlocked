﻿// Decompiled with JetBrains decompiler
// Type: Huion.KeyBoardHook
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Huion
{
  public class KeyBoardHook
  {
    private static int hKeyboardHook = 0;
    private static KeyBoardHook.HookProc KeyboardHookProcedure;
    public const int WH_KEYBOARD_LL = 13;
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    public const int WM_SYSKEYDOWN = 260;
    public const int WM_SYSKEYUP = 261;
    private static bool isControl;
    private static bool isAlt;
    private static bool isShift;
    private static bool isWin;
    private static HuionKeyEventHandler mOnKeyDown;
    private static HuionKeyEventHandler mOnKeyUp;

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int SetWindowsHookEx(int idHook, KeyBoardHook.HookProc lpfn, IntPtr hInstance, int threadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern bool UnhookWindowsHookEx(int idHook);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string name);

    private static int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
    {
      KeyEventArgs keyEventArgs = new KeyEventArgs((Keys) ((KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof (KeyboardHookStruct))).vkCode);
      if (260 == wParam)
        wParam = 256;
      if (wParam == 256)
      {
        if (keyEventArgs.KeyCode == Keys.LControlKey || keyEventArgs.KeyCode == Keys.RControlKey)
          KeyBoardHook.isControl = true;
        if (keyEventArgs.KeyCode == Keys.LShiftKey || keyEventArgs.KeyCode == Keys.RShiftKey)
          KeyBoardHook.isShift = true;
        if (keyEventArgs.KeyCode == Keys.LMenu || keyEventArgs.KeyCode == Keys.RMenu)
          KeyBoardHook.isAlt = true;
        if (keyEventArgs.KeyCode == Keys.LWin || keyEventArgs.KeyCode == Keys.RWin)
          KeyBoardHook.isWin = true;
        HuionKeyEventArgs e = new HuionKeyEventArgs(keyEventArgs.KeyCode, KeyBoardHook.isControl, KeyBoardHook.isAlt, KeyBoardHook.isShift, KeyBoardHook.isWin);
        if (KeyBoardHook.mOnKeyDown != null)
          KeyBoardHook.mOnKeyDown((object) null, e);
        if (e.Handled)
          return 1;
      }
      else
      {
        if (keyEventArgs.KeyCode == Keys.LControlKey || keyEventArgs.KeyCode == Keys.RControlKey)
          KeyBoardHook.isControl = false;
        if (keyEventArgs.KeyCode == Keys.LShiftKey || keyEventArgs.KeyCode == Keys.RShiftKey)
          KeyBoardHook.isShift = false;
        if (keyEventArgs.KeyCode == Keys.LMenu || keyEventArgs.KeyCode == Keys.RMenu)
          KeyBoardHook.isAlt = false;
        if (keyEventArgs.KeyCode == Keys.LWin || keyEventArgs.KeyCode == Keys.RWin)
          KeyBoardHook.isWin = false;
        HuionKeyEventArgs e = new HuionKeyEventArgs(keyEventArgs.KeyCode, KeyBoardHook.isControl, KeyBoardHook.isAlt, KeyBoardHook.isShift, KeyBoardHook.isWin);
        if (KeyBoardHook.mOnKeyUp != null)
          KeyBoardHook.mOnKeyUp((object) null, e);
      }
      return KeyBoardHook.CallNextHookEx(KeyBoardHook.hKeyboardHook, nCode, wParam, lParam);
    }

    public static void HookAll(HuionKeyEventHandler onKeyDown, HuionKeyEventHandler onKeyUp)
    {
      KeyBoardHook.mOnKeyDown = onKeyDown;
      KeyBoardHook.mOnKeyUp = onKeyUp;
      KeyBoardHook.hookKey();
    }

    public static void StartHook(Keys key)
    {
      KeyBoardHook.hookKey();
    }

    private static void hookKey()
    {
      if (KeyBoardHook.hKeyboardHook != 0)
        return;
      KeyBoardHook.KeyboardHookProcedure = new KeyBoardHook.HookProc(KeyBoardHook.KeyboardHookProc);
      KeyBoardHook.hKeyboardHook = KeyBoardHook.SetWindowsHookEx(13, KeyBoardHook.KeyboardHookProcedure, KeyBoardHook.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
      if (KeyBoardHook.hKeyboardHook == 0)
      {
        KeyBoardHook.StopHook();
        throw new Exception("SetWindowsHookEx ist failed.");
      }
    }

    public static void StopHook()
    {
      KeyBoardHook.isAlt = false;
      KeyBoardHook.isControl = false;
      KeyBoardHook.isShift = false;
      KeyBoardHook.isWin = false;
      KeyBoardHook.mOnKeyDown = (HuionKeyEventHandler) null;
      bool flag = true;
      if (KeyBoardHook.hKeyboardHook != 0)
      {
        flag = KeyBoardHook.UnhookWindowsHookEx(KeyBoardHook.hKeyboardHook);
        KeyBoardHook.hKeyboardHook = 0;
        KeyBoardHook.KeyboardHookProcedure = (KeyBoardHook.HookProc) null;
      }
      int num = flag ? 1 : 0;
    }

    public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
  }
}
