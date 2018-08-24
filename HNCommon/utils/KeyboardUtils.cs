// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.KeyboardUtils
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Windows.Forms;
using Huion;

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
                if (keyValue == 72 && correct == 0)
                    ++correct;
                else if (keyValue == 85 && correct == 1)
                    ++correct;
                else if (keyValue == 73 && correct == 2)
                    ++correct;
                else if (keyValue == 79 && correct == 3)
                    ++correct;
                else if (keyValue == 78 && correct == 4)
                    ++correct;
                else
                    correct = 0;
                if (keyValue == 86 && correctforType == 0)
                    ++correctforType;
                else if (keyValue == 69 && correctforType == 1)
                    ++correctforType;
                else if (keyValue == 82 && correctforType == 2)
                    ++correctforType;
                else
                    correctforType = 0;
            }
            else
            {
                correct = 0;
                correctforType = 0;
            }

            if (correct == 5)
            {
                // ISSUE: reference to a compiler-generated field
                if (sendMsgEvent != null)
                {
                    // ISSUE: reference to a compiler-generated field
                    sendMsgEvent();
                }

                correct = 0;
            }

            // ISSUE: reference to a compiler-generated field
            if (correctforType != 3 || sendHuionTypeEvent == null)
                return;
            // ISSUE: reference to a compiler-generated field
            sendHuionTypeEvent();
        }

        public static void listenHotKey(IntPtr handle, Void callback, HuionKeyEventArgs hotKey)
        {
            showFormEvent += callback;
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
            showFormEvent = (Void) null;
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
            if (m.Msg != 786 || (m.WParam.ToInt32() != 1000 || showFormEvent == null))
                return;
            // ISSUE: reference to a compiler-generated field
            showFormEvent();
        }
    }
}