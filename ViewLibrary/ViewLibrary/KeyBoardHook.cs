// Decompiled with JetBrains decompiler
// Type: Huion.KeyBoardHook
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Huion
{
    public class KeyBoardHook
    {
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        public const int WH_KEYBOARD_LL = 13;
        public const int WH_KEYBOARD = 2;
        public const int WM_KEYDOWN = 256;
        public const int WM_KEYUP = 257;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 261;
        private static int hKeyboardHook = 0;
        private static HookProc KeyboardHookProcedure;
        private static bool isControl;
        private static bool isAlt;
        private static bool isShift;
        private static bool isWin;
        private static HuionKeyEventHandler mOnKeyDown;
        private static HuionKeyEventHandler mOnKeyUp;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey,
            int fuState);

        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        public event KeyEventHandler KeyDownEvent;

        public event KeyPressEventHandler KeyPressEvent;

        public event KeyEventHandler KeyUpEvent;

        private static int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            KeyEventArgs keyEventArgs =
                new KeyEventArgs(
                    (Keys) ((KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct))).vkCode);
            if (260 == wParam)
                wParam = 256;
            if (wParam == 256)
            {
                if (keyEventArgs.KeyCode == Keys.LControlKey || keyEventArgs.KeyCode == Keys.RControlKey)
                    isControl = true;
                if (keyEventArgs.KeyCode == Keys.LShiftKey || keyEventArgs.KeyCode == Keys.RShiftKey)
                    isShift = true;
                if (keyEventArgs.KeyCode == Keys.LMenu || keyEventArgs.KeyCode == Keys.RMenu)
                    isAlt = true;
                if (keyEventArgs.KeyCode == Keys.LWin || keyEventArgs.KeyCode == Keys.RWin)
                    isWin = true;
                HuionKeyEventArgs e = new HuionKeyEventArgs(keyEventArgs.KeyCode, isControl, isAlt, isShift, isWin);
                if (mOnKeyDown != null)
                    mOnKeyDown((object) null, e);
                if (e.Handled)
                    return 1;
            }
            else
            {
                if (keyEventArgs.KeyCode == Keys.LControlKey || keyEventArgs.KeyCode == Keys.RControlKey)
                    isControl = false;
                if (keyEventArgs.KeyCode == Keys.LShiftKey || keyEventArgs.KeyCode == Keys.RShiftKey)
                    isShift = false;
                if (keyEventArgs.KeyCode == Keys.LMenu || keyEventArgs.KeyCode == Keys.RMenu)
                    isAlt = false;
                if (keyEventArgs.KeyCode == Keys.LWin || keyEventArgs.KeyCode == Keys.RWin)
                    isWin = false;
                HuionKeyEventArgs e = new HuionKeyEventArgs(keyEventArgs.KeyCode, isControl, isAlt, isShift, isWin);
                if (mOnKeyUp != null)
                    mOnKeyUp((object) null, e);
            }

            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        public static void HookAll(HuionKeyEventHandler onKeyDown, HuionKeyEventHandler onKeyUp)
        {
            mOnKeyDown = onKeyDown;
            mOnKeyUp = onKeyUp;
            hookKey();
        }

        public static void StartHook(Keys key)
        {
            hookKey();
        }

        public static void hookKey()
        {
            if (hKeyboardHook != 0)
                return;
            KeyboardHookProcedure = new HookProc(KeyboardHookProc);
            hKeyboardHook = SetWindowsHookEx(13, KeyboardHookProcedure,
                GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
            if (hKeyboardHook == 0)
            {
                StopHook();
                int num = (int) MessageBox.Show("SetWindowsHookEx ist failed.");
                throw new Exception("SetWindowsHookEx ist failed.");
            }
        }

        public static void StopHook()
        {
            isAlt = false;
            isControl = false;
            isShift = false;
            isWin = false;
            mOnKeyDown = (HuionKeyEventHandler) null;
            bool flag = true;
            if (hKeyboardHook != 0)
            {
                flag = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
                KeyboardHookProcedure = (HookProc) null;
            }

            int num = flag ? 1 : 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int dwExtraInfo;
            public int flags;
            public int scanCode;
            public int time;
            public int vkCode;
        }
    }
}