// Decompiled with JetBrains decompiler
// Type: HuionTablet.HNStruct
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using System;
using System.Runtime.InteropServices;

namespace HuionTablet
{
  public class HNStruct
  {
    public static readonly int REQUEST_TIMEOUT = 200;
    public static readonly int REQUEST_REPEATS = 4;
    public static HNStruct.HNGlobalInfo globalInfo;
    public static string tabletTextInfo;
    public static string devTypeString;
    public static OEMType OemType;

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNRect
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNSize
    {
      public int cx;
      public int cy;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNRectRatio
    {
      public static HNStruct.HNRectRatio DEFAULT = new HNStruct.HNRectRatio() { l = 0.0f, t = 0.0f, b = 1f, r = 1f };
      public float l;
      public float t;
      public float r;
      public float b;

      public override string ToString()
      {
        return "l：" + (object) this.l + ",t:" + (object) this.t + ",r:" + (object) this.r + ",b:" + (object) this.b;
      }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
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
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
      public string sDevType;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
      public string sPenType;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string sDevVer;

      public string getFirmwareVersion()
      {
        return ResourceCulture.GetString("FirmwareVersion") + this.sDevVer;
      }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
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

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNEkey
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.U2)]
      public char[] ekid;
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

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNMEkey
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.U2)]
      public char[] mekid;
      public byte num;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNEkey[] eks;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNMuxMEkey
    {
      public byte ekNum;
      public byte muxNum;
      public byte muxIndex;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNMEkey[] eks;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNContextEkeys
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260, ArraySubType = UnmanagedType.U2)]
      public char[] pid;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNEkey[] hbtns;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNEkey[] sbtns;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNEkey[] pbtns;
      public byte mmeksNum;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNMuxMEkey[] ctxMek;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct HNConfigXML
    {
      public byte bGame;
      public byte bImproveLinearity;
      public byte bTabletpc;
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
      public HNStruct.HNRectRatio workAreaRatio;
      public HNStruct.HNRectRatio screenAreaRatio;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
      public HNStruct.HNContextEkeys[] ctxEkeys;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNLayoutEkey
    {
      public HNStruct.HNRect rect;
      public HNStruct.HNRect inRect;
      public HNStruct.HNPoint center;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNMEkeyName
    {
      public uint num;
      public IntPtr names;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
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

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HNLayoutPen
    {
      public HNStruct.HNSize size;
      public uint ekNum;
      public IntPtr ekLayouts;
    }

    public struct HNGlobalInfo
    {
      public HNStruct.HNTabletInfo tabletInfo;
      public HNStruct.HNLayoutTablet layoutTablet;
      public HNStruct.HNRectRatio penareaRatio;
      public HNStruct.HNLayoutPen layoutPen;
      public bool bOpenedTablet;
      public bool? isDeviceConnected;
      public HNStruct.HNEkey[] hbtns;
      public HNStruct.HNEkey[] pbtns;
      public HNStruct.HNEkey[] sbtns;
      public HNStruct.HNContextEkeys[] eKeys;
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

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MONITORINFOEX
    {
      public int Size;
      public HNStruct.RECT Monitor;
      public HNStruct.RECT WorkArea;
      public uint Flags;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U2)]
      public char[] DeviceName;
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

    public struct VCPRect
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PHYSICAL_MONITOR
    {
      public IntPtr hphysicalMonitor;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public string szPhysicalMonitorDescription;
    }

    public enum VCPFeature
    {
      RESTORE_FACTORY_DEFAULTS = 4,
      RESTORE_FACTORY_LUMINANCE_DEFAULTS = 5,
      RESTORE_FACTORY_COLOR_DEFAULTS = 8,
      LUMINANCE = 16, // 0x00000010
      CONTRAST = 18, // 0x00000012
      COLOR_PRESET = 20, // 0x00000014
      VIDEO_GAIN_RED = 22, // 0x00000016
      VIDEO_GAIN_GREEN = 24, // 0x00000018
      VIDEO_GAIN_BLUE = 26, // 0x0000001A
      INPUT_SOURCE = 96, // 0x00000060
      SPEAKER_VOLUME = 98, // 0x00000062
      VIDEO_BLACK_LEVEL_RED = 108, // 0x0000006C
      VIDEO_BLACK_LEVEL_GREEN = 110, // 0x0000006E
      VIDEO_BLACK_LEVEL_BLUE = 112, // 0x00000070
      STORE_RESTORE_SETTINGS = 176, // 0x000000B0
    }

    [Flags]
    public enum ColorPreset
    {
      SRGB = 1,
      DISPLAY_NATIVE = 2,
      _4000K = DISPLAY_NATIVE | SRGB, // 0x00000003
      _5000K = 4,
      _6500K = _5000K | SRGB, // 0x00000005
      _7500K = _5000K | DISPLAY_NATIVE, // 0x00000006
      _8200K = _7500K | SRGB, // 0x00000007
      _9300K = 8,
      _10000K = _9300K | SRGB, // 0x00000009
      _11500K = _9300K | DISPLAY_NATIVE, // 0x0000000A
      USER1 = _11500K | SRGB, // 0x0000000B
      USER2 = _9300K | _5000K, // 0x0000000C
      USER3 = USER2 | SRGB, // 0x0000000D
    }

    [Flags]
    public enum InputSource
    {
      ANALOG_VIDEO1 = 1,
      ANALOG_VIDEO2 = 2,
      DVI1 = ANALOG_VIDEO2 | ANALOG_VIDEO1, // 0x00000003
      DVI2 = 4,
      HDMI1 = 17, // 0x00000011
      HDMI2 = 18, // 0x00000012
      DISPLAY_PORT1 = 15, // 0x0000000F
      DISPLAY_PORT2 = 16, // 0x00000010
    }

    public enum ParseState
    {
      DEFAULT,
      VCP,
      PROT,
      TYPE,
      CMDS,
      MODEL,
      SKIP,
      MSWHQL,
      ASSET_EEP,
      MCCS_VER,
      ERROR,
    }

    public enum VcpState
    {
      DEFAULT,
      INPUT_SOURCE,
      COLOR_PRESET,
      SKIP,
    }
  }
}
