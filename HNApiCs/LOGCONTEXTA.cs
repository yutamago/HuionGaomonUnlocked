// Decompiled with JetBrains decompiler
// Type: Huion.LOGCONTEXTA
// Assembly: HNApiCs, Version=14.4.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 4957B58E-8324-4AC9-B678-1AA8EE08DC3D
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNApiCs.dll

using System.Runtime.InteropServices;

namespace Huion
{
    public struct LOGCONTEXTA
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string lcName;

        public uint lcOptions;
        public uint lcStatus;
        public uint lcLocks;
        public uint lcMsgBase;
        public uint lcDevice;
        public uint lcPktRate;
        public uint lcPktData;
        public uint lcPktMode;
        public uint lcMoveMask;
        public uint lcBtnDnMask;
        public uint lcBtnUpMask;
        public uint lcInOrgX;
        public uint lcInOrgY;
        public uint lcInOrgZ;
        public uint lcInExtX;
        public uint lcInExtY;
        public uint lcInExtZ;
        public uint lcOutOrgX;
        public uint lcOutOrgY;
        public uint lcOutOrgZ;
        public uint lcOutExtX;
        public uint lcOutExtY;
        public uint lcOutExtZ;
        public uint lcSensX;
        public uint lcSensY;
        public uint lcSensZ;
        public bool lcSysMode;
        public uint lcSysOrgX;
        public uint lcSysOrgY;
        public uint lcSysExtX;
        public uint lcSysExtY;
        public uint lcSysSensX;
        public uint lcSysSensY;
    }
}