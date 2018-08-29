// Decompiled with JetBrains decompiler
// Type: HuionTablet.TabletConfigUtils
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HuionTablet.Lib;

namespace HuionTablet
{
    public class TabletConfigUtils
    {
        public const string TYPE_HUION = "HUION";
        public const string TYPE_GAOMON = "OEM02";
        public const string TYPE_NEUTRAL = "OEM00";
        public const string TYPE_YOUSHANG = "OEM06";
        public const string TYPE_YINENG = "OEM07";
        public const string TYPE_SHIJUN = "OEM08";
        public const string TYPE_KJC = "OEM10";
        public static HNStruct.HNConfigXML config;

        public void readConfig()
        {
            string configFilePath = Application.StartupPath + "\\res\\config_user.xml";

            if (SettingsUtil.isPerAppSettingsEnabled && SettingsUtil.perAppSettingsDefault.HasValue &&
                SettingsUtil.perAppSettingsDefault.Value.enabled)
            {
                configFilePath = Path.GetFullPath(Path.Combine(SettingsUtil.perAppSettingsProfileDir,
                    SettingsUtil.perAppSettingsDefault.Value.profile));
            }

            HNStruct.devTypeString =
                Marshal.PtrToStringAuto(
                    HuionDriverDLL.hnp_get_tablet_image((HnConst.HNTabletType) HNStruct.globalInfo.tabletInfo.devType));
            HNStruct.tabletTextInfo = HNStruct.globalInfo.bOpenedTablet
                ? ResourceCulture.GetString("FormHuionTablet_lbOpenTabletText")
                : ResourceCulture.GetString("FormHuionTablet_lbCloseTabletText");
            if (!HNStruct.globalInfo.bOpenedTablet)
                return;
            IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(configFilePath);
            Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
            IntPtr num = HuionDriverDLL.hnx_read_config(ref HNStruct.globalInfo.tabletInfo, coTaskMemAuto);
            config = (HNStruct.HNConfigXML) Marshal.PtrToStructure(num, typeof(HNStruct.HNConfigXML));
            this.SetLayoutTablet(num);
            Marshal.FreeHGlobal(coTaskMemAuto);
            HuionDriverDLL.hnx_free_PHNConfig(num);
            this.SetLayoutPen();
            this.SetConfig(config);
        }

        public static IntPtr getAddr(object o)
        {
            return GCHandle.ToIntPtr(GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection));
        }

        public void SetConfig(HNStruct.HNConfigXML config)
        {
            HNStruct.globalInfo.hbtns = new HNStruct.HNEkey[(int) HNStruct.globalInfo.layoutTablet.hbtnNum];
            HNStruct.globalInfo.sbtns = new HNStruct.HNEkey[(int) HNStruct.globalInfo.layoutTablet.sbtnNum];
            HNStruct.globalInfo.pbtns = new HNStruct.HNEkey[(int) HNStruct.globalInfo.tabletInfo.pbtnNum];
            HNStruct.globalInfo.mbtns = new HNStruct.HNEkey[16];
            for (int index = 0; (long) index < (long) HNStruct.globalInfo.layoutTablet.hbtnNum; ++index)
                HNStruct.globalInfo.hbtns[index] = config.ctxEkeys[0].hbtns[index];
            for (int index = 0; (long) index < (long) HNStruct.globalInfo.layoutTablet.sbtnNum; ++index)
                HNStruct.globalInfo.sbtns[index] = config.ctxEkeys[0].sbtns[index];
            for (int index = 0; index < 3; ++index)
                HNStruct.globalInfo.pbtns[index] = config.ctxEkeys[0].pbtns[index];
            for (int index1 = 0; index1 < (int) config.ctxEkeys[0].mmeksNum; ++index1)
            {
                config.ctxEkeys[0].ctxMek[index1] = config.ctxEkeys[0].ctxMek[index1];
                config.ctxEkeys[0].ctxMek[index1].eks[0] = config.ctxEkeys[0].ctxMek[index1].eks[0];
                for (int index2 = 0; index2 < (int) config.ctxEkeys[0].ctxMek[index1].eks[0].num; ++index2)
                    HNStruct.globalInfo.mbtns[index2] = config.ctxEkeys[0].ctxMek[index1].eks[0].eks[index2];
            }
        }

        public static int getEKIndexOfCurMEKey(byte mekeyIndex, byte index, byte ekIndex)
        {
            return (int) mekeyIndex * 16 * 16 + (int) index * 16 + (int) ekIndex;
        }

        public void readDisConnectConfig()
        {
            TimerSession.stopAutoConnection();
            HNStruct.tabletTextInfo = HNStruct.globalInfo.bOpenedTablet
                ? ResourceCulture.GetString("FormHuionTablet_lbOpenTabletText")
                : ResourceCulture.GetString("FormHuionTablet_lbCloseTabletText");
        }

        public void SetLayoutTablet(IntPtr pconfig)
        {
            IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(Application.StartupPath + "\\res\\layout_tablet.xml");
            Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNLayoutTablet)));
            IntPtr num1 =
                HuionDriverDLL.hnx_read_layout_tablet(ref HNStruct.globalInfo.tabletInfo, pconfig, coTaskMemAuto);
            HNStruct.HNLayoutTablet hnLayoutTablet = new HNStruct.HNLayoutTablet();
            HNStruct.HNLayoutTablet structure =
                (HNStruct.HNLayoutTablet) Marshal.PtrToStructure(num1, typeof(HNStruct.HNLayoutTablet));
            HNStruct.globalInfo.layoutTablet = structure;
            HNStruct.globalInfo.hbtnLayouts = new HNStruct.HNLayoutEkey[(int) structure.hbtnNum];
            int num2 = 0;
            Console.WriteLine("sizeof(HNLayoutTablet)={0}", (object) Marshal.SizeOf(typeof(HNStruct.HNLayoutTablet)));
            foreach (HNStruct.HNLayoutEkey hnLayoutEkey in MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(
                structure.hbtnLayouts, (int) structure.hbtnNum))
                HNStruct.globalInfo.hbtnLayouts[num2++] = hnLayoutEkey;
            HNStruct.globalInfo.sbtnLayouts = new HNStruct.HNLayoutEkey[(int) structure.sbtnNum];
            int num3 = 0;
            foreach (HNStruct.HNLayoutEkey hnLayoutEkey in MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(
                structure.sbtnLayouts, (int) structure.sbtnNum))
                HNStruct.globalInfo.sbtnLayouts[num3++] = hnLayoutEkey;
            HNStruct.globalInfo.mekeyLayouts = new HNStruct.HNLayoutEkey[(int) structure.mekeyNum];
            int num4 = 0;
            foreach (HNStruct.HNLayoutEkey hnLayoutEkey in MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(
                structure.mekeyLayouts, (int) structure.mekeyNum))
            {
                if (!HNStruct.globalInfo.tabletInfo.sDevType.Equals("HUION_M182"))
                    HNStruct.globalInfo.mekeyLayouts[num4++] = hnLayoutEkey;
                else
                    break;
            }

            HNStruct.globalInfo.mekeyNames = new HNStruct.HNMEkeyName[(int) structure.mekeyNum];
            int index1 = 0;
            foreach (HNStruct.HNMEkeyName hnmEkeyName in MarshalPtrToStructArray<HNStruct.HNMEkeyName>(
                structure.mekeyNames, (int) structure.mekeyNum))
            {
                Console.WriteLine(HNStruct.globalInfo.tabletInfo.sDevType);
                if (!HNStruct.globalInfo.tabletInfo.sDevType.Equals("HUION_M182"))
                {
                    HNStruct.globalInfo.mekeyNames[index1] = hnmEkeyName;
                    HNStruct.globalInfo.names = new string[(int) HNStruct.globalInfo.mekeyNames[index1].num];
                    IntPtr ptr = HNStruct.globalInfo.mekeyNames[index1].names;
                    for (int index2 = 0; (long) index2 < (long) HNStruct.globalInfo.mekeyNames[0].num; ++index2)
                    {
                        switch (IntPtr.Size)
                        {
                            case 4:
                                HNStruct.globalInfo.names[index2] =
                                    Marshal.PtrToStringAuto(new IntPtr((int) Marshal.PtrToStructure(ptr, typeof(int))));
                                ptr = new IntPtr(ptr.ToInt64() + 4L);
                                break;
                            case 8:
                                HNStruct.globalInfo.names[index2] =
                                    Marshal.PtrToStringAuto(
                                        new IntPtr((long) Marshal.PtrToStructure(ptr, typeof(long))));
                                ptr = new IntPtr(ptr.ToInt64() + 8L);
                                break;
                        }
                    }

                    ++index1;
                }
                else
                    break;
            }

            HNStruct.globalInfo.ekLayouts = new HNStruct.HNLayoutEkey[(int) structure.ekNum];
            int num5 = 0;
            foreach (HNStruct.HNLayoutEkey hnLayoutEkey in MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(
                structure.ekLayouts, (int) structure.ekNum))
                HNStruct.globalInfo.ekLayouts[num5++] = hnLayoutEkey;
            HNStruct.HNRect penArea = structure.penArea;
            HNStruct.HNSize size = structure.size;
            HNStruct.HNRectRatio hnRectRatio;
            hnRectRatio.l = (float) penArea.left / (float) size.cx;
            hnRectRatio.r = (float) penArea.right / (float) size.cx;
            hnRectRatio.t = (float) penArea.top / (float) size.cy;
            hnRectRatio.b = (float) penArea.bottom / (float) size.cy;
            HNStruct.globalInfo.penareaRatio = hnRectRatio;
            Marshal.FreeHGlobal(coTaskMemAuto);
            HuionDriverDLL.hnx_free_PHNLayoutTablet(num1);
        }

        public void SetLayoutPen()
        {
            Marshal.PtrToStringAuto(
                HuionDriverDLL.hnp_get_pen_node((HnConst.HNTabletType) HNStruct.globalInfo.tabletInfo.devType));
            IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(Application.StartupPath + "\\res\\layout_pen.xml");
            Marshal.AllocHGlobal(Marshal.SizeOf((object) new HNStruct.HNLayoutPen()));
            IntPtr num1 = HuionDriverDLL.hnx_read_layout_pen(ref HNStruct.globalInfo.tabletInfo, coTaskMemAuto);
            HNStruct.globalInfo.layoutPen =
                (HNStruct.HNLayoutPen) Marshal.PtrToStructure(num1, typeof(HNStruct.HNLayoutPen));
            HNStruct.globalInfo.penLayouts = new HNStruct.HNLayoutEkey[(int) HNStruct.globalInfo.layoutPen.ekNum];
            int num2 = 0;
            foreach (HNStruct.HNLayoutEkey hnLayoutEkey in MarshalPtrToStructArray<HNStruct.HNLayoutEkey>(
                HNStruct.globalInfo.layoutPen.ekLayouts, (int) HNStruct.globalInfo.layoutPen.ekNum))
                HNStruct.globalInfo.penLayouts[num2++] = hnLayoutEkey;
            Marshal.FreeHGlobal(coTaskMemAuto);
            HuionDriverDLL.hnx_free_PHNLayoutPen(num1);
        }

        public static List<T> MarshalPtrToStructArray<T>(IntPtr p, int count)
        {
            List<T> objList = new List<T>();
            switch (IntPtr.Size)
            {
                case 4:
                    int num1 = 0;
                    while (num1 < count)
                    {
                        T structure = (T) Marshal.PtrToStructure(p, typeof(T));
                        objList.Add(structure);
                        ++num1;
                        p = new IntPtr(p.ToInt32() + Marshal.SizeOf(typeof(T)));
                    }

                    break;
                case 8:
                    int num2 = 0;
                    while (num2 < count)
                    {
                        T structure = (T) Marshal.PtrToStructure(p, typeof(T));
                        objList.Add(structure);
                        ++num2;
                        p = new IntPtr(p.ToInt64() + (long) Marshal.SizeOf(typeof(T)));
                    }

                    break;
            }

            return objList;
        }

        public static bool isHuionTablet(uint devType)
        {
            return getTabletType(devType).IndexOf("HUION") != -1;
        }

        public static bool isGaomonTablet(uint devType)
        {
            return getTabletType(devType).IndexOf("OEM02") != -1;
        }

        public static bool isYinengTablet(uint devType)
        {
            return getTabletType(devType).IndexOf("OEM07") != -1;
        }

        public static bool isYoushangTablet(uint devType)
        {
            return getTabletType(devType).IndexOf("OEM06") != -1;
        }

        public static bool isShijunTablet(uint devType)
        {
            return getTabletType(devType).IndexOf("OEM08") != -1;
        }

        public static bool isKJCTablet(uint devType)
        {
            return getTabletType(devType).IndexOf("OEM10") != -1;
        }

        public static string getTabletType(uint devType)
        {
            try
            {
                return ((HnConst.HNTabletType) devType).ToString();
            }
            catch (Exception ex)
            {
                return HnConst.HNTabletType.HNTT_UNKNOW.ToString();
            }
        }

        public static void closeDevice(object obj)
        {
            try
            {
                DeviceStatusUtils.reconnection = false;
                HuionDriverDLL.hnd_close();
            }
            catch (Exception ex)
            {
                HuionLog.printLog("", ex.Message);
                HuionLog.printLog("", ex.StackTrace);
            }
        }
    }
}