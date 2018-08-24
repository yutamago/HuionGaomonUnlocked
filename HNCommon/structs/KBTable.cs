// Decompiled with JetBrains decompiler
// Type: HuionTablet.KBTable
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HuionTablet
{
  public struct KBTable
  {
    public static readonly KBTable ZERO = new KBTable()
    {
      keyCode = 48,
      keyName = "0"
    };
    public static readonly KBTable ONE = new KBTable()
    {
      keyCode = 49,
      keyName = "1"
    };
    public static readonly KBTable TWO = new KBTable()
    {
      keyCode = 50,
      keyName = "2"
    };
    public static readonly KBTable THREE = new KBTable()
    {
      keyCode = 51,
      keyName = "3"
    };
    public static readonly KBTable FOUR = new KBTable()
    {
      keyCode = 52,
      keyName = "4"
    };
    public static readonly KBTable FIVE = new KBTable()
    {
      keyCode = 53,
      keyName = "5"
    };
    public static readonly KBTable SIX = new KBTable()
    {
      keyCode = 54,
      keyName = "6"
    };
    public static readonly KBTable SEVEN = new KBTable()
    {
      keyCode = 55,
      keyName = "7"
    };
    public static readonly KBTable EIGHT = new KBTable()
    {
      keyCode = 56,
      keyName = "8"
    };
    public static readonly KBTable NINE = new KBTable()
    {
      keyCode = 57,
      keyName = "9"
    };
    public static readonly KBTable A = new KBTable()
    {
      keyCode = 65,
      keyName = nameof (A)
    };
    public static readonly KBTable B = new KBTable()
    {
      keyCode = 66,
      keyName = nameof (B)
    };
    public static readonly KBTable C = new KBTable()
    {
      keyCode = 67,
      keyName = nameof (C)
    };
    public static readonly KBTable D = new KBTable()
    {
      keyCode = 68,
      keyName = nameof (D)
    };
    public static readonly KBTable E = new KBTable()
    {
      keyCode = 69,
      keyName = nameof (E)
    };
    public static readonly KBTable F = new KBTable()
    {
      keyCode = 70,
      keyName = nameof (F)
    };
    public static readonly KBTable G = new KBTable()
    {
      keyCode = 71,
      keyName = nameof (G)
    };
    public static readonly KBTable H = new KBTable()
    {
      keyCode = 72,
      keyName = nameof (H)
    };
    public static readonly KBTable I = new KBTable()
    {
      keyCode = 73,
      keyName = nameof (I)
    };
    public static readonly KBTable J = new KBTable()
    {
      keyCode = 74,
      keyName = nameof (J)
    };
    public static readonly KBTable K = new KBTable()
    {
      keyCode = 75,
      keyName = nameof (K)
    };
    public static readonly KBTable L = new KBTable()
    {
      keyCode = 76,
      keyName = nameof (L)
    };
    public static readonly KBTable M = new KBTable()
    {
      keyCode = 77,
      keyName = nameof (M)
    };
    public static readonly KBTable N = new KBTable()
    {
      keyCode = 78,
      keyName = nameof (N)
    };
    public static readonly KBTable O = new KBTable()
    {
      keyCode = 79,
      keyName = nameof (O)
    };
    public static readonly KBTable P = new KBTable()
    {
      keyCode = 80,
      keyName = nameof (P)
    };
    public static readonly KBTable Q = new KBTable()
    {
      keyCode = 81,
      keyName = nameof (Q)
    };
    public static readonly KBTable R = new KBTable()
    {
      keyCode = 82,
      keyName = nameof (R)
    };
    public static readonly KBTable S = new KBTable()
    {
      keyCode = 83,
      keyName = nameof (S)
    };
    public static readonly KBTable T = new KBTable()
    {
      keyCode = 84,
      keyName = nameof (T)
    };
    public static readonly KBTable U = new KBTable()
    {
      keyCode = 85,
      keyName = nameof (U)
    };
    public static readonly KBTable V = new KBTable()
    {
      keyCode = 86,
      keyName = nameof (V)
    };
    public static readonly KBTable W = new KBTable()
    {
      keyCode = 87,
      keyName = nameof (W)
    };
    public static readonly KBTable X = new KBTable()
    {
      keyCode = 88,
      keyName = nameof (X)
    };
    public static readonly KBTable Y = new KBTable()
    {
      keyCode = 89,
      keyName = nameof (Y)
    };
    public static readonly KBTable Z = new KBTable()
    {
      keyCode = 90,
      keyName = nameof (Z)
    };
    public static readonly KBTable F1 = new KBTable()
    {
      keyCode = 112,
      keyName = nameof (F1)
    };
    public static readonly KBTable F2 = new KBTable()
    {
      keyCode = 113,
      keyName = nameof (F2)
    };
    public static readonly KBTable F3 = new KBTable()
    {
      keyCode = 114,
      keyName = nameof (F3)
    };
    public static readonly KBTable F4 = new KBTable()
    {
      keyCode = 115,
      keyName = nameof (F4)
    };
    public static readonly KBTable F5 = new KBTable()
    {
      keyCode = 116,
      keyName = nameof (F5)
    };
    public static readonly KBTable F6 = new KBTable()
    {
      keyCode = 117,
      keyName = nameof (F6)
    };
    public static readonly KBTable F7 = new KBTable()
    {
      keyCode = 118,
      keyName = nameof (F7)
    };
    public static readonly KBTable F8 = new KBTable()
    {
      keyCode = 119,
      keyName = nameof (F8)
    };
    public static readonly KBTable F9 = new KBTable()
    {
      keyCode = 120,
      keyName = nameof (F9)
    };
    public static readonly KBTable F10 = new KBTable()
    {
      keyCode = 121,
      keyName = nameof (F10)
    };
    public static readonly KBTable F11 = new KBTable()
    {
      keyCode = 122,
      keyName = nameof (F11)
    };
    public static readonly KBTable F12 = new KBTable()
    {
      keyCode = 123,
      keyName = nameof (F12)
    };
    public static readonly KBTable F13 = new KBTable()
    {
      keyCode = 124,
      keyName = nameof (F13)
    };
    public static readonly KBTable F14 = new KBTable()
    {
      keyCode = 125,
      keyName = nameof (F14)
    };
    public static readonly KBTable F15 = new KBTable()
    {
      keyCode = 126,
      keyName = nameof (F15)
    };
    public static readonly KBTable F16 = new KBTable()
    {
      keyCode = 127,
      keyName = nameof (F16)
    };
    public static readonly KBTable F17 = new KBTable()
    {
      keyCode = 128,
      keyName = nameof (F17)
    };
    public static readonly KBTable F18 = new KBTable()
    {
      keyCode = 129,
      keyName = nameof (F18)
    };
    public static readonly KBTable F19 = new KBTable()
    {
      keyCode = 130,
      keyName = nameof (F19)
    };
    public static readonly KBTable F20 = new KBTable()
    {
      keyCode = 131,
      keyName = nameof (F20)
    };
    public static readonly KBTable F21 = new KBTable()
    {
      keyCode = 132,
      keyName = nameof (F21)
    };
    public static readonly KBTable F22 = new KBTable()
    {
      keyCode = 133,
      keyName = nameof (F22)
    };
    public static readonly KBTable F23 = new KBTable()
    {
      keyCode = 134,
      keyName = nameof (F23)
    };
    public static readonly KBTable F24 = new KBTable()
    {
      keyCode = 135,
      keyName = nameof (F24)
    };
    public static readonly KBTable BACKSPACE = new KBTable()
    {
      keyCode = 8,
      keyName = "Backspace"
    };
    public static readonly KBTable TAB = new KBTable()
    {
      keyCode = 9,
      keyName = "Tab"
    };
    public static readonly KBTable ENTER = new KBTable()
    {
      keyCode = 13,
      keyName = "Enter"
    };
    public static readonly KBTable ESC = new KBTable()
    {
      keyCode = 27,
      keyName = "Esc"
    };
    public static readonly KBTable SPACE = new KBTable()
    {
      keyCode = 32,
      keyName = "Space"
    };
    public static readonly KBTable PAGEUP = new KBTable()
    {
      keyCode = 33,
      keyName = "PageUp"
    };
    public static readonly KBTable PAGEDOWN = new KBTable()
    {
      keyCode = 34,
      keyName = "PageDown"
    };
    public static readonly KBTable END = new KBTable()
    {
      keyCode = 35,
      keyName = "End"
    };
    public static readonly KBTable HOME = new KBTable()
    {
      keyCode = 36,
      keyName = "Home"
    };
    public static readonly KBTable LEFT = new KBTable()
    {
      keyCode = 37,
      keyName = "←"
    };
    public static readonly KBTable RIGHT = new KBTable()
    {
      keyCode = 39,
      keyName = "→"
    };
    public static readonly KBTable UP = new KBTable()
    {
      keyCode = 38,
      keyName = "↑"
    };
    public static readonly KBTable DOWN = new KBTable()
    {
      keyCode = 40,
      keyName = "↓"
    };
    public static readonly KBTable PRINTSCREEN = new KBTable()
    {
      keyCode = 44,
      keyName = "PrintScreen"
    };
    public static readonly KBTable INSERT = new KBTable()
    {
      keyCode = 45,
      keyName = "Insert"
    };
    public static readonly KBTable DELETE = new KBTable()
    {
      keyCode = 46,
      keyName = "Delete"
    };
    public static readonly KBTable NUMPAD_ZERO = new KBTable()
    {
      keyCode = 96,
      keyName = "Numpad0"
    };
    public static readonly KBTable NUMPAD_ONE = new KBTable()
    {
      keyCode = 97,
      keyName = "Numpad1"
    };
    public static readonly KBTable NUMPAD_TWO = new KBTable()
    {
      keyCode = 98,
      keyName = "Numpad2"
    };
    public static readonly KBTable NUMPAD_THREE = new KBTable()
    {
      keyCode = 99,
      keyName = "Numpad3"
    };
    public static readonly KBTable NUMPAD_FOUR = new KBTable()
    {
      keyCode = 100,
      keyName = "Numpad4"
    };
    public static readonly KBTable NUMPAD_FIVE = new KBTable()
    {
      keyCode = 101,
      keyName = "Numpad5"
    };
    public static readonly KBTable NUMPAD_SIX = new KBTable()
    {
      keyCode = 102,
      keyName = "Numpad6"
    };
    public static readonly KBTable NUMPAD_SEVEN = new KBTable()
    {
      keyCode = 103,
      keyName = "Numpad7"
    };
    public static readonly KBTable NUMPAD_EIGHT = new KBTable()
    {
      keyCode = 104,
      keyName = "Numpad8"
    };
    public static readonly KBTable NUMPAD_NINE = new KBTable()
    {
      keyCode = 105,
      keyName = "Numpad9"
    };
    public static readonly KBTable NUMPAD_MULTIPLY = new KBTable()
    {
      keyCode = 106,
      keyName = "Numpad*"
    };
    public static readonly KBTable NUMPAD_ADD = new KBTable()
    {
      keyCode = 107,
      keyName = "Numpad+"
    };
    public static readonly KBTable NUMPAD_MINUS = new KBTable()
    {
      keyCode = 109,
      keyName = "Numpad-"
    };
    public static readonly KBTable NUMPAD_DIVIDE = new KBTable()
    {
      keyCode = 111,
      keyName = "Numpad/"
    };
    public static readonly KBTable NUMPAD_DOT = new KBTable()
    {
      keyCode = 110,
      keyName = "Numpad."
    };
    public static readonly KBTable SEMICOLON = new KBTable()
    {
      keyCode = 186,
      keyName = ";"
    };
    public static readonly KBTable EQUAL = new KBTable()
    {
      keyCode = 187,
      keyName = "="
    };
    public static readonly KBTable COMMA = new KBTable()
    {
      keyCode = 188,
      keyName = ","
    };
    public static readonly KBTable MINUS = new KBTable()
    {
      keyCode = 189,
      keyName = "-"
    };
    public static readonly KBTable DOT = new KBTable()
    {
      keyCode = 190,
      keyName = "."
    };
    public static readonly KBTable SLASH = new KBTable()
    {
      keyCode = 191,
      keyName = "/"
    };
    public static readonly KBTable BACK_QUOTE = new KBTable()
    {
      keyCode = 192,
      keyName = "`"
    };
    public static readonly KBTable LEFT_BRACKET = new KBTable()
    {
      keyCode = 219,
      keyName = "["
    };
    public static readonly KBTable BACK_SLASH = new KBTable()
    {
      keyCode = 220,
      keyName = "\\"
    };
    public static readonly KBTable RIGHT_BRACKET = new KBTable()
    {
      keyCode = 221,
      keyName = "]"
    };
    public static readonly KBTable QUOTE = new KBTable()
    {
      keyCode = 222,
      keyName = "'"
    };
    public static readonly KBTable OEM_EXCLAMATION_MARK = new KBTable()
    {
      keyCode = 223,
      keyName = "!"
    };
    public static readonly KBTable OEM_BACKSLASH = new KBTable()
    {
      keyCode = 226,
      keyName = "<"
    };
    public static List<KBTable> KeyBoards = new List<KBTable>();
    public byte keyCode;
    public string keyName;

    static KBTable()
    {
      KBTable.KeyBoards.Add(KBTable.A);
      KBTable.KeyBoards.Add(KBTable.B);
      KBTable.KeyBoards.Add(KBTable.C);
      KBTable.KeyBoards.Add(KBTable.D);
      KBTable.KeyBoards.Add(KBTable.E);
      KBTable.KeyBoards.Add(KBTable.F);
      KBTable.KeyBoards.Add(KBTable.G);
      KBTable.KeyBoards.Add(KBTable.H);
      KBTable.KeyBoards.Add(KBTable.I);
      KBTable.KeyBoards.Add(KBTable.J);
      KBTable.KeyBoards.Add(KBTable.K);
      KBTable.KeyBoards.Add(KBTable.L);
      KBTable.KeyBoards.Add(KBTable.M);
      KBTable.KeyBoards.Add(KBTable.N);
      KBTable.KeyBoards.Add(KBTable.O);
      KBTable.KeyBoards.Add(KBTable.P);
      KBTable.KeyBoards.Add(KBTable.Q);
      KBTable.KeyBoards.Add(KBTable.R);
      KBTable.KeyBoards.Add(KBTable.S);
      KBTable.KeyBoards.Add(KBTable.T);
      KBTable.KeyBoards.Add(KBTable.U);
      KBTable.KeyBoards.Add(KBTable.V);
      KBTable.KeyBoards.Add(KBTable.W);
      KBTable.KeyBoards.Add(KBTable.X);
      KBTable.KeyBoards.Add(KBTable.Y);
      KBTable.KeyBoards.Add(KBTable.Z);
      KBTable.KeyBoards.Add(KBTable.ZERO);
      KBTable.KeyBoards.Add(KBTable.ONE);
      KBTable.KeyBoards.Add(KBTable.TWO);
      KBTable.KeyBoards.Add(KBTable.THREE);
      KBTable.KeyBoards.Add(KBTable.FOUR);
      KBTable.KeyBoards.Add(KBTable.FIVE);
      KBTable.KeyBoards.Add(KBTable.SIX);
      KBTable.KeyBoards.Add(KBTable.SEVEN);
      KBTable.KeyBoards.Add(KBTable.EIGHT);
      KBTable.KeyBoards.Add(KBTable.NINE);
      KBTable.KeyBoards.Add(KBTable.F1);
      KBTable.KeyBoards.Add(KBTable.F2);
      KBTable.KeyBoards.Add(KBTable.F3);
      KBTable.KeyBoards.Add(KBTable.F4);
      KBTable.KeyBoards.Add(KBTable.F5);
      KBTable.KeyBoards.Add(KBTable.F6);
      KBTable.KeyBoards.Add(KBTable.F7);
      KBTable.KeyBoards.Add(KBTable.F8);
      KBTable.KeyBoards.Add(KBTable.F9);
      KBTable.KeyBoards.Add(KBTable.F10);
      KBTable.KeyBoards.Add(KBTable.F11);
      KBTable.KeyBoards.Add(KBTable.F12);
      if (DeployConfig.getOemType() == OEMType.HUION)
      {
        KBTable.KeyBoards.Add(KBTable.F13);
        KBTable.KeyBoards.Add(KBTable.F14);
        KBTable.KeyBoards.Add(KBTable.F15);
        KBTable.KeyBoards.Add(KBTable.F16);
        KBTable.KeyBoards.Add(KBTable.F17);
        KBTable.KeyBoards.Add(KBTable.F18);
        KBTable.KeyBoards.Add(KBTable.F19);
        KBTable.KeyBoards.Add(KBTable.F20);
        KBTable.KeyBoards.Add(KBTable.F21);
        KBTable.KeyBoards.Add(KBTable.F22);
        KBTable.KeyBoards.Add(KBTable.F23);
        KBTable.KeyBoards.Add(KBTable.F24);
      }
      KBTable.KeyBoards.Add(KBTable.BACKSPACE);
      KBTable.KeyBoards.Add(KBTable.TAB);
      KBTable.KeyBoards.Add(KBTable.ENTER);
      KBTable.KeyBoards.Add(KBTable.ESC);
      KBTable.KeyBoards.Add(KBTable.SPACE);
      KBTable.KeyBoards.Add(KBTable.PAGEUP);
      KBTable.KeyBoards.Add(KBTable.PAGEDOWN);
      KBTable.KeyBoards.Add(KBTable.END);
      KBTable.KeyBoards.Add(KBTable.HOME);
      KBTable.KeyBoards.Add(KBTable.LEFT);
      KBTable.KeyBoards.Add(KBTable.RIGHT);
      KBTable.KeyBoards.Add(KBTable.UP);
      KBTable.KeyBoards.Add(KBTable.DOWN);
      KBTable.KeyBoards.Add(KBTable.PRINTSCREEN);
      KBTable.KeyBoards.Add(KBTable.INSERT);
      KBTable.KeyBoards.Add(KBTable.DELETE);
      KBTable.KeyBoards.Add(KBTable.NUMPAD_MULTIPLY);
      KBTable.KeyBoards.Add(KBTable.NUMPAD_ADD);
      KBTable.KeyBoards.Add(KBTable.NUMPAD_MINUS);
      KBTable.KeyBoards.Add(KBTable.NUMPAD_DIVIDE);
      KBTable.KeyBoards.Add(KBTable.NUMPAD_DOT);
      if (DeployConfig.getOemType() == OEMType.HUION)
      {
        KBTable.KeyBoards.Add(KBTable.NUMPAD_ZERO);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_ONE);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_TWO);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_THREE);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_FOUR);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_FIVE);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_SIX);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_SEVEN);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_EIGHT);
        KBTable.KeyBoards.Add(KBTable.NUMPAD_NINE);
      }
      KBTable.KeyBoards.Add(KBTable.SEMICOLON);
      KBTable.KeyBoards.Add(KBTable.EQUAL);
      KBTable.KeyBoards.Add(KBTable.COMMA);
      KBTable.KeyBoards.Add(KBTable.MINUS);
      KBTable.KeyBoards.Add(KBTable.DOT);
      KBTable.KeyBoards.Add(KBTable.SLASH);
      KBTable.KeyBoards.Add(KBTable.BACK_QUOTE);
      KBTable.KeyBoards.Add(KBTable.LEFT_BRACKET);
      KBTable.KeyBoards.Add(KBTable.BACK_SLASH);
      KBTable.KeyBoards.Add(KBTable.RIGHT_BRACKET);
      KBTable.KeyBoards.Add(KBTable.QUOTE);
      if (DeployConfig.getOemType() != OEMType.HUION)
        return;
      KBTable.KeyBoards.Add(KBTable.OEM_EXCLAMATION_MARK);
      KBTable.KeyBoards.Add(KBTable.OEM_BACKSLASH);
    }

    public string KeyName
    {
      get
      {
        Keys keyCode = (Keys) this.keyCode;
        if (DeployConfig.getOemType() == OEMType.HUION && (KeyCodeUtils.isNumber(keyCode) || KeyCodeUtils.isPunctuation(keyCode) || KeyCodeUtils.isLetter(keyCode)))
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
      foreach (KBTable keyBoard in KBTable.KeyBoards)
      {
        if (keyBoard.keyName.Equals(keyName))
          return keyBoard;
      }
      return new KBTable();
    }

    public static KBTable getKey8Code(byte keyCode)
    {
      foreach (KBTable keyBoard in KBTable.KeyBoards)
      {
        if ((int) keyBoard.keyCode == (int) keyCode)
          return keyBoard;
      }
      return new KBTable();
    }

    public static KBTable getKBTable8Keys(Keys key)
    {
      return KBTable.getKey8Code((byte) key);
    }

    public static Keys getkeys8TableCode(byte code)
    {
      return (Keys) code;
    }
  }
}
