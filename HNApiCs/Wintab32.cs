// Decompiled with JetBrains decompiler
// Type: Huion.Wintab32
// Assembly: HNApiCs, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 4957B58E-8324-4AC9-B678-1AA8EE08DC3D
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNApiCs.dll

using System;
using System.Runtime.InteropServices;

namespace Huion
{
  public class Wintab32
  {
    public const string DLL_NAME = "Wintab32.dll";
    public static readonly uint PACKETDATA;
    public const int LCNAMELEN = 40;
    public const int LC_NAMELEN = 40;
    public const int WT_DEFBASE = 32752;
    public const int WT_MAXOFFSET = 15;
    public const int WT_PACKET = 32752;
    public const int WT_CTXOPEN = 32753;
    public const int WT_CTXCLOSE = 32754;
    public const int WT_CTXUPDATE = 32755;
    public const int WT_CTXOVERLAP = 32756;
    public const int WT_PROXIMITY = 32757;
    public const int WT_INFOCHANGE = 32758;
    public const int WT_CSRCHANGE = 32759;
    public const int WT_PACKETEXT = 32760;
    public const int WT_MAX = 32767;
    public const int WTI_DEFCONTEXT = 3;
    public const int WTI_DEFSYSCTX = 4;
    public const int WTI_DDCTXS = 400;
    public const int WTI_DSCTXS = 500;
    public const int PK_CONTEXT = 1;
    public const int PK_STATUS = 2;
    public const int PK_TIME = 4;
    public const int PK_CHANGED = 8;
    public const int PK_SERIAL_NUMBER = 16;
    public const int PK_CURSOR = 32;
    public const int PK_BUTTONS = 64;
    public const int PK_X = 128;
    public const int PK_Y = 256;
    public const int PK_Z = 512;
    public const int PK_NORMAL_PRESSURE = 1024;
    public const int PK_TANGENT_PRESSURE = 2048;
    public const int PK_ORIENTATION = 4096;
    public const int PK_ROTATION = 8192;
    public const int CXO_SYSTEM = 1;
    public const int CXO_PEN = 2;
    public const int CXO_MESSAGES = 4;
    public const int CXO_MARGIN = 32768;
    public const int CXO_MGNINSIDE = 16384;
    public const int CXO_CSRMESSAGES = 8;
    public const int CXS_DISABLED = 1;
    public const int CXS_OBSCURED = 2;
    public const int CXS_ONTOP = 4;
    public const int CXL_INSIZE = 1;
    public const int CXL_INASPECT = 2;
    public const int CXL_SENSITIVITY = 4;
    public const int CXL_MARGIN = 8;
    public const int CXL_SYSOUT = 16;

    static Wintab32()
    {
      Wintab32.PACKETDATA |= 64U;
      Wintab32.PACKETDATA |= 128U;
      Wintab32.PACKETDATA |= 256U;
      Wintab32.PACKETDATA |= 1024U;
    }

    [DllImport("Wintab32.dll")]
    public static extern int WTInfoA(uint wCategory, uint nIndex, IntPtr lpOutput);

    [DllImport("Wintab32.dll")]
    public static extern IntPtr WTOpenA(IntPtr hWnd, IntPtr lpLogCtx, bool fEnable);

    [DllImport("Wintab32.dll")]
    public static extern bool WTPacket(IntPtr hCtx, int wSerial, IntPtr lpPkt);

    [DllImport("Wintab32.dll")]
    public static extern bool WTClose(IntPtr hCtx);
  }
}
