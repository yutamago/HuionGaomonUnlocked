// Decompiled with JetBrains decompiler
// Type: HuionTablet.KBTable
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System.Collections.Generic;
using System.Windows.Forms;
using Huion;

namespace HuionTablet
{
    public struct KBTable
    {
        public static readonly KBTable ZERO = new KBTable() {keyCode = 48, keyName = "0"};
        public static readonly KBTable ONE = new KBTable() {keyCode = 49, keyName = "1"};
        public static readonly KBTable TWO = new KBTable() {keyCode = 50, keyName = "2"};
        public static readonly KBTable THREE = new KBTable() {keyCode = 51, keyName = "3"};
        public static readonly KBTable FOUR = new KBTable() {keyCode = 52, keyName = "4"};
        public static readonly KBTable FIVE = new KBTable() {keyCode = 53, keyName = "5"};
        public static readonly KBTable SIX = new KBTable() {keyCode = 54, keyName = "6"};
        public static readonly KBTable SEVEN = new KBTable() {keyCode = 55, keyName = "7"};
        public static readonly KBTable EIGHT = new KBTable() {keyCode = 56, keyName = "8"};
        public static readonly KBTable NINE = new KBTable() {keyCode = 57, keyName = "9"};
        public static readonly KBTable A = new KBTable() {keyCode = 65, keyName = nameof(A)};
        public static readonly KBTable B = new KBTable() {keyCode = 66, keyName = nameof(B)};
        public static readonly KBTable C = new KBTable() {keyCode = 67, keyName = nameof(C)};
        public static readonly KBTable D = new KBTable() {keyCode = 68, keyName = nameof(D)};
        public static readonly KBTable E = new KBTable() {keyCode = 69, keyName = nameof(E)};
        public static readonly KBTable F = new KBTable() {keyCode = 70, keyName = nameof(F)};
        public static readonly KBTable G = new KBTable() {keyCode = 71, keyName = nameof(G)};
        public static readonly KBTable H = new KBTable() {keyCode = 72, keyName = nameof(H)};
        public static readonly KBTable I = new KBTable() {keyCode = 73, keyName = nameof(I)};
        public static readonly KBTable J = new KBTable() {keyCode = 74, keyName = nameof(J)};
        public static readonly KBTable K = new KBTable() {keyCode = 75, keyName = nameof(K)};
        public static readonly KBTable L = new KBTable() {keyCode = 76, keyName = nameof(L)};
        public static readonly KBTable M = new KBTable() {keyCode = 77, keyName = nameof(M)};
        public static readonly KBTable N = new KBTable() {keyCode = 78, keyName = nameof(N)};
        public static readonly KBTable O = new KBTable() {keyCode = 79, keyName = nameof(O)};
        public static readonly KBTable P = new KBTable() {keyCode = 80, keyName = nameof(P)};
        public static readonly KBTable Q = new KBTable() {keyCode = 81, keyName = nameof(Q)};
        public static readonly KBTable R = new KBTable() {keyCode = 82, keyName = nameof(R)};
        public static readonly KBTable S = new KBTable() {keyCode = 83, keyName = nameof(S)};
        public static readonly KBTable T = new KBTable() {keyCode = 84, keyName = nameof(T)};
        public static readonly KBTable U = new KBTable() {keyCode = 85, keyName = nameof(U)};
        public static readonly KBTable V = new KBTable() {keyCode = 86, keyName = nameof(V)};
        public static readonly KBTable W = new KBTable() {keyCode = 87, keyName = nameof(W)};
        public static readonly KBTable X = new KBTable() {keyCode = 88, keyName = nameof(X)};
        public static readonly KBTable Y = new KBTable() {keyCode = 89, keyName = nameof(Y)};
        public static readonly KBTable Z = new KBTable() {keyCode = 90, keyName = nameof(Z)};
        public static readonly KBTable F1 = new KBTable() {keyCode = 112, keyName = nameof(F1)};
        public static readonly KBTable F2 = new KBTable() {keyCode = 113, keyName = nameof(F2)};
        public static readonly KBTable F3 = new KBTable() {keyCode = 114, keyName = nameof(F3)};
        public static readonly KBTable F4 = new KBTable() {keyCode = 115, keyName = nameof(F4)};
        public static readonly KBTable F5 = new KBTable() {keyCode = 116, keyName = nameof(F5)};
        public static readonly KBTable F6 = new KBTable() {keyCode = 117, keyName = nameof(F6)};
        public static readonly KBTable F7 = new KBTable() {keyCode = 118, keyName = nameof(F7)};
        public static readonly KBTable F8 = new KBTable() {keyCode = 119, keyName = nameof(F8)};
        public static readonly KBTable F9 = new KBTable() {keyCode = 120, keyName = nameof(F9)};
        public static readonly KBTable F10 = new KBTable() {keyCode = 121, keyName = nameof(F10)};
        public static readonly KBTable F11 = new KBTable() {keyCode = 122, keyName = nameof(F11)};
        public static readonly KBTable F12 = new KBTable() {keyCode = 123, keyName = nameof(F12)};
        public static readonly KBTable F13 = new KBTable() {keyCode = 124, keyName = nameof(F13)};
        public static readonly KBTable F14 = new KBTable() {keyCode = 125, keyName = nameof(F14)};
        public static readonly KBTable F15 = new KBTable() {keyCode = 126, keyName = nameof(F15)};
        public static readonly KBTable F16 = new KBTable() {keyCode = 127, keyName = nameof(F16)};
        public static readonly KBTable F17 = new KBTable() {keyCode = 128, keyName = nameof(F17)};
        public static readonly KBTable F18 = new KBTable() {keyCode = 129, keyName = nameof(F18)};
        public static readonly KBTable F19 = new KBTable() {keyCode = 130, keyName = nameof(F19)};
        public static readonly KBTable F20 = new KBTable() {keyCode = 131, keyName = nameof(F20)};
        public static readonly KBTable F21 = new KBTable() {keyCode = 132, keyName = nameof(F21)};
        public static readonly KBTable F22 = new KBTable() {keyCode = 133, keyName = nameof(F22)};
        public static readonly KBTable F23 = new KBTable() {keyCode = 134, keyName = nameof(F23)};
        public static readonly KBTable F24 = new KBTable() {keyCode = 135, keyName = nameof(F24)};
        public static readonly KBTable BACKSPACE = new KBTable() {keyCode = 8, keyName = "Backspace"};
        public static readonly KBTable TAB = new KBTable() {keyCode = 9, keyName = "Tab"};
        public static readonly KBTable ENTER = new KBTable() {keyCode = 13, keyName = "Enter"};
        public static readonly KBTable ESC = new KBTable() {keyCode = 27, keyName = "Esc"};
        public static readonly KBTable SPACE = new KBTable() {keyCode = 32, keyName = "Space"};
        public static readonly KBTable PAGEUP = new KBTable() {keyCode = 33, keyName = "PageUp"};
        public static readonly KBTable PAGEDOWN = new KBTable() {keyCode = 34, keyName = "PageDown"};
        public static readonly KBTable END = new KBTable() {keyCode = 35, keyName = "End"};
        public static readonly KBTable HOME = new KBTable() {keyCode = 36, keyName = "Home"};
        public static readonly KBTable LEFT = new KBTable() {keyCode = 37, keyName = "←"};
        public static readonly KBTable RIGHT = new KBTable() {keyCode = 39, keyName = "→"};
        public static readonly KBTable UP = new KBTable() {keyCode = 38, keyName = "↑"};
        public static readonly KBTable DOWN = new KBTable() {keyCode = 40, keyName = "↓"};
        public static readonly KBTable PRINTSCREEN = new KBTable() {keyCode = 44, keyName = "PrintScreen"};
        public static readonly KBTable INSERT = new KBTable() {keyCode = 45, keyName = "Insert"};
        public static readonly KBTable DELETE = new KBTable() {keyCode = 46, keyName = "Delete"};
        public static readonly KBTable NUMPAD_ZERO = new KBTable() {keyCode = 96, keyName = "Numpad0"};
        public static readonly KBTable NUMPAD_ONE = new KBTable() {keyCode = 97, keyName = "Numpad1"};
        public static readonly KBTable NUMPAD_TWO = new KBTable() {keyCode = 98, keyName = "Numpad2"};
        public static readonly KBTable NUMPAD_THREE = new KBTable() {keyCode = 99, keyName = "Numpad3"};
        public static readonly KBTable NUMPAD_FOUR = new KBTable() {keyCode = 100, keyName = "Numpad4"};
        public static readonly KBTable NUMPAD_FIVE = new KBTable() {keyCode = 101, keyName = "Numpad5"};
        public static readonly KBTable NUMPAD_SIX = new KBTable() {keyCode = 102, keyName = "Numpad6"};
        public static readonly KBTable NUMPAD_SEVEN = new KBTable() {keyCode = 103, keyName = "Numpad7"};
        public static readonly KBTable NUMPAD_EIGHT = new KBTable() {keyCode = 104, keyName = "Numpad8"};
        public static readonly KBTable NUMPAD_NINE = new KBTable() {keyCode = 105, keyName = "Numpad9"};
        public static readonly KBTable NUMPAD_MULTIPLY = new KBTable() {keyCode = 106, keyName = "Numpad*"};
        public static readonly KBTable NUMPAD_ADD = new KBTable() {keyCode = 107, keyName = "Numpad+"};
        public static readonly KBTable NUMPAD_MINUS = new KBTable() {keyCode = 109, keyName = "Numpad-"};
        public static readonly KBTable NUMPAD_DIVIDE = new KBTable() {keyCode = 111, keyName = "Numpad/"};
        public static readonly KBTable NUMPAD_DOT = new KBTable() {keyCode = 110, keyName = "Numpad."};
        public static readonly KBTable SEMICOLON = new KBTable() {keyCode = 186, keyName = ";"};
        public static readonly KBTable EQUAL = new KBTable() {keyCode = 187, keyName = "="};
        public static readonly KBTable COMMA = new KBTable() {keyCode = 188, keyName = ","};
        public static readonly KBTable MINUS = new KBTable() {keyCode = 189, keyName = "-"};
        public static readonly KBTable DOT = new KBTable() {keyCode = 190, keyName = "."};
        public static readonly KBTable SLASH = new KBTable() {keyCode = 191, keyName = "/"};
        public static readonly KBTable BACK_QUOTE = new KBTable() {keyCode = 192, keyName = "`"};
        public static readonly KBTable LEFT_BRACKET = new KBTable() {keyCode = 219, keyName = "["};
        public static readonly KBTable BACK_SLASH = new KBTable() {keyCode = 220, keyName = "\\"};
        public static readonly KBTable RIGHT_BRACKET = new KBTable() {keyCode = 221, keyName = "]"};
        public static readonly KBTable QUOTE = new KBTable() {keyCode = 222, keyName = "'"};
        public static readonly KBTable OEM_EXCLAMATION_MARK = new KBTable() {keyCode = 223, keyName = "!"};
        public static readonly KBTable OEM_BACKSLASH = new KBTable() {keyCode = 226, keyName = "<"};
        public static List<KBTable> KeyBoards = new List<KBTable>();
        public byte keyCode;
        public string keyName;

        static KBTable()
        {
            KeyBoards.Add(A);
            KeyBoards.Add(B);
            KeyBoards.Add(C);
            KeyBoards.Add(D);
            KeyBoards.Add(E);
            KeyBoards.Add(F);
            KeyBoards.Add(G);
            KeyBoards.Add(H);
            KeyBoards.Add(I);
            KeyBoards.Add(J);
            KeyBoards.Add(K);
            KeyBoards.Add(L);
            KeyBoards.Add(M);
            KeyBoards.Add(N);
            KeyBoards.Add(O);
            KeyBoards.Add(P);
            KeyBoards.Add(Q);
            KeyBoards.Add(R);
            KeyBoards.Add(S);
            KeyBoards.Add(T);
            KeyBoards.Add(U);
            KeyBoards.Add(V);
            KeyBoards.Add(W);
            KeyBoards.Add(X);
            KeyBoards.Add(Y);
            KeyBoards.Add(Z);
            KeyBoards.Add(ZERO);
            KeyBoards.Add(ONE);
            KeyBoards.Add(TWO);
            KeyBoards.Add(THREE);
            KeyBoards.Add(FOUR);
            KeyBoards.Add(FIVE);
            KeyBoards.Add(SIX);
            KeyBoards.Add(SEVEN);
            KeyBoards.Add(EIGHT);
            KeyBoards.Add(NINE);
            KeyBoards.Add(F1);
            KeyBoards.Add(F2);
            KeyBoards.Add(F3);
            KeyBoards.Add(F4);
            KeyBoards.Add(F5);
            KeyBoards.Add(F6);
            KeyBoards.Add(F7);
            KeyBoards.Add(F8);
            KeyBoards.Add(F9);
            KeyBoards.Add(F10);
            KeyBoards.Add(F11);
            KeyBoards.Add(F12);
            if (DeployConfig.getOemType() == OEMType.HUION)
            {
                KeyBoards.Add(F13);
                KeyBoards.Add(F14);
                KeyBoards.Add(F15);
                KeyBoards.Add(F16);
                KeyBoards.Add(F17);
                KeyBoards.Add(F18);
                KeyBoards.Add(F19);
                KeyBoards.Add(F20);
                KeyBoards.Add(F21);
                KeyBoards.Add(F22);
                KeyBoards.Add(F23);
                KeyBoards.Add(F24);
            }

            KeyBoards.Add(BACKSPACE);
            KeyBoards.Add(TAB);
            KeyBoards.Add(ENTER);
            KeyBoards.Add(ESC);
            KeyBoards.Add(SPACE);
            KeyBoards.Add(PAGEUP);
            KeyBoards.Add(PAGEDOWN);
            KeyBoards.Add(END);
            KeyBoards.Add(HOME);
            KeyBoards.Add(LEFT);
            KeyBoards.Add(RIGHT);
            KeyBoards.Add(UP);
            KeyBoards.Add(DOWN);
            KeyBoards.Add(PRINTSCREEN);
            KeyBoards.Add(INSERT);
            KeyBoards.Add(DELETE);
            KeyBoards.Add(NUMPAD_MULTIPLY);
            KeyBoards.Add(NUMPAD_ADD);
            KeyBoards.Add(NUMPAD_MINUS);
            KeyBoards.Add(NUMPAD_DIVIDE);
            KeyBoards.Add(NUMPAD_DOT);
            if (DeployConfig.getOemType() == OEMType.HUION)
            {
                KeyBoards.Add(NUMPAD_ZERO);
                KeyBoards.Add(NUMPAD_ONE);
                KeyBoards.Add(NUMPAD_TWO);
                KeyBoards.Add(NUMPAD_THREE);
                KeyBoards.Add(NUMPAD_FOUR);
                KeyBoards.Add(NUMPAD_FIVE);
                KeyBoards.Add(NUMPAD_SIX);
                KeyBoards.Add(NUMPAD_SEVEN);
                KeyBoards.Add(NUMPAD_EIGHT);
                KeyBoards.Add(NUMPAD_NINE);
            }

            KeyBoards.Add(SEMICOLON);
            KeyBoards.Add(EQUAL);
            KeyBoards.Add(COMMA);
            KeyBoards.Add(MINUS);
            KeyBoards.Add(DOT);
            KeyBoards.Add(SLASH);
            KeyBoards.Add(BACK_QUOTE);
            KeyBoards.Add(LEFT_BRACKET);
            KeyBoards.Add(BACK_SLASH);
            KeyBoards.Add(RIGHT_BRACKET);
            KeyBoards.Add(QUOTE);
            if (DeployConfig.getOemType() != OEMType.HUION)
                return;
            KeyBoards.Add(OEM_EXCLAMATION_MARK);
            KeyBoards.Add(OEM_BACKSLASH);
        }

        public string KeyName
        {
            get
            {
                Keys keyCode = (Keys) this.keyCode;
                if (DeployConfig.getOemType() == OEMType.HUION &&
                    (KeyCodeUtils.isNumber(keyCode) || KeyCodeUtils.isPunctuation(keyCode) ||
                     KeyCodeUtils.isLetter(keyCode)))
                    return (KeyCodeUtils.convert2Char(keyCode).ToString() ?? "").ToUpper();
                return this.keyName;
            }
        }

        public override string ToString()
        {
            return this.KeyName;
        }

        public static void init_KBTable()
        {
        }

        public static KBTable getKey8Name(string keyName)
        {
            foreach (KBTable keyBoard in KeyBoards)
            {
                if (keyBoard.keyName.Equals(keyName))
                    return keyBoard;
            }

            return new KBTable();
        }

        public static KBTable getKey8Code(byte keyCode)
        {
            foreach (KBTable keyBoard in KeyBoards)
            {
                if ((int) keyBoard.keyCode == (int) keyCode)
                    return keyBoard;
            }

            return new KBTable();
        }

        public static KBTable getKBTable8Keys(Keys key)
        {
            return getKey8Code((byte) key);
        }

        public static Keys getkeys8TableCode(byte code)
        {
            return (Keys) code;
        }
    }
}