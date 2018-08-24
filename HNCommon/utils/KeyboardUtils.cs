// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.KeyboardUtils
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using System;
using System.Windows.Forms;

namespace HuionTablet.utils
{
  public class KeyboardUtils
  {
    public const int ID_SHOWFORM = 1000;
    private static int correct;
    private static int correctforType;

    public static event Void sendMsgEvent;

    public static event Void sendHuionTypeEvent;

    public static event Void showFormEvent;

    public static void onKeyDown(object sender, KeyEventArgs e)
    {
      int keyValue = e.KeyValue;
      if (e.Modifiers == Keys.Control)
      {
        if (keyValue == 72 && KeyboardUtils.correct == 0)
          ++KeyboardUtils.correct;
        else if (keyValue == 85 && KeyboardUtils.correct == 1)
          ++KeyboardUtils.correct;
        else if (keyValue == 73 && KeyboardUtils.correct == 2)
          ++KeyboardUtils.correct;
        else if (keyValue == 79 && KeyboardUtils.correct == 3)
          ++KeyboardUtils.correct;
        else if (keyValue == 78 && KeyboardUtils.correct == 4)
          ++KeyboardUtils.correct;
        else
          KeyboardUtils.correct = 0;
        if (keyValue == 86 && KeyboardUtils.correctforType == 0)
          ++KeyboardUtils.correctforType;
        else if (keyValue == 69 && KeyboardUtils.correctforType == 1)
          ++KeyboardUtils.correctforType;
        else if (keyValue == 82 && KeyboardUtils.correctforType == 2)
          ++KeyboardUtils.correctforType;
        else
          KeyboardUtils.correctforType = 0;
      }
      else
      {
        KeyboardUtils.correct = 0;
        KeyboardUtils.correctforType = 0;
      }
      if (KeyboardUtils.correct == 5)
      {
        // ISSUE: reference to a compiler-generated field
        if (KeyboardUtils.sendMsgEvent != null)
        {
          // ISSUE: reference to a compiler-generated field
          KeyboardUtils.sendMsgEvent();
        }
        KeyboardUtils.correct = 0;
      }
      // ISSUE: reference to a compiler-generated field
      if (KeyboardUtils.correctforType != 3 || KeyboardUtils.sendHuionTypeEvent == null)
        return;
      // ISSUE: reference to a compiler-generated field
      KeyboardUtils.sendHuionTypeEvent();
    }

    public static void listenHotKey(IntPtr handle, Void callback, HuionKeyEventArgs hotKey)
    {
      KeyboardUtils.showFormEvent += callback;
      HuionDriverDLL.KeyModifiers fsModifiers = HuionDriverDLL.KeyModifiers.None;
      if (hotKey.Control)
        fsModifiers |= HuionDriverDLL.KeyModifiers.Ctrl;
      if (hotKey.Alt)
        fsModifiers |= HuionDriverDLL.KeyModifiers.Alt;
      if (hotKey.Window)
        fsModifiers |= HuionDriverDLL.KeyModifiers.WindowsKey;
      if (hotKey.Shift)
        fsModifiers |= HuionDriverDLL.KeyModifiers.Shift;
      HuionDriverDLL.RegisterHotKey(handle, 1000, fsModifiers, hotKey.KeyCode);
    }

    public static void unlistenHotKey(IntPtr handle)
    {
      // ISSUE: reference to a compiler-generated field
      KeyboardUtils.showFormEvent = (Void) null;
      HuionDriverDLL.UnregisterHotKey(handle, 1000);
    }

    public static bool CheckHotkey(IntPtr handle, HuionKeyEventArgs hotKey)
    {
      HuionDriverDLL.KeyModifiers fsModifiers = HuionDriverDLL.KeyModifiers.None;
      if (hotKey.Control)
        fsModifiers |= HuionDriverDLL.KeyModifiers.Ctrl;
      if (hotKey.Alt)
        fsModifiers |= HuionDriverDLL.KeyModifiers.Alt;
      if (hotKey.Window)
        fsModifiers |= HuionDriverDLL.KeyModifiers.WindowsKey;
      if (hotKey.Shift)
        fsModifiers |= HuionDriverDLL.KeyModifiers.Shift;
      if (!HuionDriverDLL.RegisterHotKey(handle, 1000, fsModifiers, hotKey.KeyCode))
        return false;
      HuionDriverDLL.UnregisterHotKey(handle, 1000);
      return true;
    }

    public static void onHotKey(ref Message m)
    {
      // ISSUE: reference to a compiler-generated field
      if (m.Msg != 786 || (m.WParam.ToInt32() != 1000 || KeyboardUtils.showFormEvent == null))
        return;
      // ISSUE: reference to a compiler-generated field
      KeyboardUtils.showFormEvent();
    }
  }
}
