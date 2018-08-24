// Decompiled with JetBrains decompiler
// Type: HuionTablet.HNStruct
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using System;
using System.Runtime.InteropServices;

namespace HuionTablet
{
  public class HNStruct
  {
    public static HNStruct.HNGlobalInfo globalInfo;
    public static string tabletTextInfo;
    public static string devTypeString;
    public static OEMType OemType;

    public struct HNRect
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    public struct HNSize
    {
      public int cx;
      public int cy;
    }

    public struct HNRectRatio
    {
      public static HNStruct.HNRectRatio DEFAULT = new HNStruct.HNRectRatio()
      {
        l = 0.0f,
        t = 0.0f,
        b = 1f,
        r = 1f
      };
      public float l;
      public float t;
      public float r;
      public float b;

      public override string ToString()
      {
        return "l：" + (object) this.l + ",t:" + (object) this.t + ",r:" + (object) this.r + ",b:" + (object) this.b;
      }
    }

    public struct HNPoint
    {
      public int x;
      public int y;
    }

    public struct HNPtRatio
    {
      public float x;
      public float y;
    }

    public struct HNPenData
    {
      public HNStruct.HNPoint pt;
      public HNStruct.HNPtRatio pr;
      public uint ps;
      public char ax;
      public char ay;
      public byte btn;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct HNTabletInfo
    {
      public uint devType;
      public uint oemType;
      public uint maxX;
      public uint maxY;
      public ushort maxP;
      public ushort lpi;
      public ushort rate;
      public byte hbtnNum;
      public byte sbtnNum;
      public byte pbtnNum;
      public byte bDebug;
      public byte bPassive;
      public byte bMonitor;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string sDevType;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
      public string companyName;

      public string getFirmwareVersion()
      {
        return ResourceCulture.GetString("FirmwareVersion") + this.sDevType;
      }
    }

    public struct HNKbtn
    {
      public byte bCtrl;
      public byte bAlt;
      public byte bShift;
      public byte bWin;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
      public byte[] kbKeys;
    }

    public enum HNEkeyType
    {
      HNEKT_NONE,
      HNEKT_MOUSE_LEFT,
      HNEKT_MOUSE_MID,
      HNEKT_MOUSE_RIGHT,
      HNEKT_KBTN,
      HNEKT_EXEC,
      HNEKT_SWITCH_DISPLAY,
      HNEKT_SWITCH_BRUSH,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct AnonymousUnion
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 520)]
      [FieldOffset(0)]
      public string cmdPath;
      [FieldOffset(0)]
      public HNStruct.HNKbtn kbtn;
    }

    public struct HNEkey
    {
      public uint funcBit;
      public ulong funcEvent;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260, ArraySubType = UnmanagedType.U2)]
      public char[] cmdPath;
      public HNStruct.HNKbtn kbtn;

      public override string ToString()
      {
        if (this.funcBit == 0U)
          return ResourceCulture.GetString("FormEkey_btUndefinedText");
        string str = (string) null;
        if (Convert.ToBoolean(this.funcBit & 4U))
        {
          if (this.kbtn.bCtrl != (byte) 0)
            str += "Ctrl+";
          if (this.kbtn.bAlt != (byte) 0)
            str += "Alt+";
          if (this.kbtn.bShift != (byte) 0)
            str += "Shift+";
          if (this.kbtn.bWin != (byte) 0)
            str += "Win+";
          for (int index = 0; index < 16 && this.kbtn.kbKeys[index] != (byte) 0; ++index)
            str += (string) (object) KBTable.getKey8Code(this.kbtn.kbKeys[index]);
        }
        if (Convert.ToBoolean(this.funcBit & 16U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdMouseLeftText");
        }
        if (Convert.ToBoolean(this.funcBit & 32U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdMouseMidText");
        }
        if (Convert.ToBoolean(this.funcBit & 64U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdMouseRightText");
        }
        if (Convert.ToBoolean(this.funcBit & 128U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdMouseWheelForwardText");
        }
        if (Convert.ToBoolean(this.funcBit & 256U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdMouseWheelBackwardText");
        }
        if (Convert.ToBoolean(this.funcBit & 1U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdSwitchScreenText");
        }
        if (Convert.ToBoolean(this.funcBit & 2U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += ResourceCulture.GetString("FormEkey_rdSwitchBrushText");
        }
        if (Convert.ToBoolean(this.funcBit & 8U))
        {
          if (!string.IsNullOrEmpty(str))
            str += ";";
          str += new string(this.cmdPath).Trim(new char[1]);
        }
        return str ?? ResourceCulture.GetString("FormEkey_btUndefinedText");
      }
    }

    public struct HNMEkey
    {
      public uint num;
      public IntPtr ekeys;
    }

    public struct HNConfig
    {
      public byte bGame;
      public byte bImproveLinearity;
      public byte bTabletpc;
      public byte bWintab;
      public byte bEnableHBtn;
      public byte bEnableSBtn;
      public byte bEnableMBtn;
      public byte bCtrlCursor;
      public byte bCalibrated;
      public byte bAbsoluteMode;
      public uint curScreenIndex;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I4)]
      public int[] calibrateFactors;
      public int pressFactor;
      public uint rotateAngle;
      public uint cursor;
      public uint mekeyNum;
      public IntPtr pbtns;
      public IntPtr hbtns;
      public IntPtr sbtns;
      public IntPtr mekeys;
      public HNStruct.HNRectRatio workAreaRatio;
      public HNStruct.HNRectRatio screenAreaRatio;
    }

    public struct HNLayoutEkey
    {
      public HNStruct.HNRect rect;
      public HNStruct.HNRect inRect;
      public HNStruct.HNPoint center;
    }

    public struct HNMEkeyName
    {
      public uint num;
      public IntPtr names;
    }

    public struct HNLayoutTablet
    {
      public HNStruct.HNSize size;
      public HNStruct.HNRect penArea;
      public uint hbtnNum;
      public IntPtr hbtnLayouts;
      public uint sbtnNum;
      public IntPtr sbtnLayouts;
      public HNStruct.HNRect sbtnInRect;
      public uint mekeyNum;
      public IntPtr mekeyLayouts;
      public IntPtr mekeyNames;
      public uint ekNum;
      public IntPtr ekLayouts;
    }

    public struct HNLayoutPen
    {
      public HNStruct.HNSize size;
      public uint ekNum;
      public IntPtr ekLayouts;
    }

    public struct HNGlobalInfo
    {
      public HNStruct.HNTabletInfo tabletInfo;
      public HNStruct.HNConfig userConfig;
      public HNStruct.HNLayoutTablet layoutTablet;
      public HNStruct.HNRectRatio penareaRatio;
      public HNStruct.HNLayoutPen layoutPen;
      public bool bOpenedTablet;
      public bool? isDeviceConnected;
      public HNStruct.HNEkey[] hbtns;
      public HNStruct.HNEkey[] pbtns;
      public HNStruct.HNEkey[] sbtns;
      public HNStruct.HNMEkey[] meKeys;
      public HNStruct.HNEkey[] mbtns;
      public HNStruct.HNLayoutEkey[] hbtnLayouts;
      public HNStruct.HNLayoutEkey[] sbtnLayouts;
      public HNStruct.HNLayoutEkey[] mekeyLayouts;
      public HNStruct.HNMEkeyName[] mekeyNames;
      public string[] names;
      public HNStruct.HNLayoutEkey[] ekLayouts;
      public HNStruct.HNLayoutEkey[] penLayouts;
      public HNStruct.HNPenData penData;
    }

    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MONITORINFOEX
    {
      public int Size;
      public HNStruct.RECT Monitor;
      public HNStruct.RECT WorkArea;
      public uint Flags;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string DeviceName;
    }

    public struct HNDevice
    {
      public ushort VID;
      public ushort PID;
      public string Status;
      public string PNPDeviceID;
      public string Name;
      public string Description;
      public string Service;
      public Guid ClassGuid;

      public bool isHNDevice()
      {
        return HNStruct.HNDevice.isHNDevice(this.VID, this.PID);
      }

      public static bool isHNDevice(ushort VID, ushort PID)
      {
        if (VID == (ushort) 9580)
          return PID == (ushort) 110;
        return false;
      }

      public static bool isTabletDevice(string description)
      {
        return "Graphics Tablet Device".Equals(description);
      }
    }

    public struct COPYDATASTRUCT
    {
      public IntPtr dwData;
      public int cbData;
      public IntPtr lpData;
    }
  }
}
