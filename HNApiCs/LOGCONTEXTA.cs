// Decompiled with JetBrains decompiler
// Type: Huion.LOGCONTEXTA
// Assembly: HNApiCs, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 56F6F7EF-63D4-4942-AD3E-9E758D946ED3
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