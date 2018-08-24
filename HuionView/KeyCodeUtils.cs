// Decompiled with JetBrains decompiler
// Type: Huion.KeyCodeUtils
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Huion
{
    public class KeyCodeUtils
    {
        public static bool isLegalKey(Keys k)
        {
            return isNumber(k) || isLetter(k) || (isFunction(k) || isOperator(k)) ||
                   (isDirection(k) || isOperation(k) || (isPunctuation(k) || isControls(k)));
        }

        public static bool isNumber(Keys k)
        {
            return k >= Keys.D0 && k <= Keys.D9 || k >= Keys.NumPad0 && k <= Keys.NumPad9;
        }

        public static bool isLetter(Keys k)
        {
            return k >= Keys.A && k <= Keys.Z;
        }

        public static bool isFunction(Keys k)
        {
            return k >= Keys.F1 && k <= Keys.F12;
        }

        public static bool isOperator(Keys k)
        {
            return k >= Keys.Multiply && k <= Keys.Divide;
        }

        public static bool isDirection(Keys k)
        {
            return k >= Keys.Prior && k <= Keys.Down;
        }

        public static bool isOperation(Keys k)
        {
            return k >= Keys.Snapshot && k <= Keys.Delete || (k == Keys.Space || k == Keys.Return) ||
                   (k == Keys.Escape || k >= Keys.Back && k <= Keys.Tab);
        }

        public static bool isPunctuation(Keys k)
        {
            return k >= Keys.OemSemicolon && k <= Keys.OemBackslash;
        }

        public static bool isControls(Keys k)
        {
            return k >= Keys.ShiftKey && k <= Keys.Menu || k >= Keys.LShiftKey && k <= Keys.RMenu ||
                   k >= Keys.LWin && k <= Keys.RWin;
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState,
            [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder receivingBuffer, int bufferSize, uint flags);

        public static char convert2Char(Keys k)
        {
            StringBuilder receivingBuffer = new StringBuilder(2);
            byte[] keyboardState = new byte[2];
            ToUnicode((uint) k, 0U, keyboardState, receivingBuffer, 2, 0U);
            try
            {
                return Convert.ToChar(receivingBuffer[0]);
            }
            catch
            {
                return char.MinValue;
            }
        }

        public static string convert2Text(Keys k)
        {
            return new KeysConverter().ConvertToString((object) k);
        }
    }
}