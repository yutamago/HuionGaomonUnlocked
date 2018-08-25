// Decompiled with JetBrains decompiler
// Type: HuionTablet.VcpFeature
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace HuionTablet
{
    public class VcpFeature
    {
        [DllImport("dxva2.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CapabilitiesRequestAndCapabilitiesReply(IntPtr hMonitor,
            [Out] char[] pszASCIICapabilitiesString, uint dwCapabilitiesStringLengthInCharacters);

        [DllImport("dxva2.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCapabilitiesStringLength(IntPtr hMonitor,
            ref uint pdwCapabilitiesStringLengthInCharacters);

        [DllImport("dxva2.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint dwPhysicalMonitorArraySize,
            [Out] HNStruct.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor,
            ref uint pdwNumberOfPhysicalMonitors);

        [DllImport("user32")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        [DllImport("dxva2.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetVCPFeatureAndVCPFeatureReply(IntPtr hMonitor, byte bVCPCode, ref uint pvct,
            ref uint pdwCurrentValue, ref uint pdwMaximumValue);

        [DllImport("dxva2.dll")]
        private static extern bool SetVCPFeature(IntPtr hMonitor, byte bVCPCode, uint dwNewValue);

        public static List<Monitors> GetAllMonitors()
        {
            List<Monitors> physicalMonitors = new List<Monitors>();
            MonitorEnumProc callback =
                (MonitorEnumProc) ((IntPtr hDesktop, IntPtr hdc, ref HNStruct.VCPRect r, int d) =>
                {
                    uint pdwNumberOfPhysicalMonitors = 0;
                    if (!GetNumberOfPhysicalMonitorsFromHMONITOR(hDesktop, ref pdwNumberOfPhysicalMonitors))
                        throw new Exception("无法获取显示器个数");
                    HNStruct.PHYSICAL_MONITOR[] pPhysicalMonitorArray =
                        new HNStruct.PHYSICAL_MONITOR[(int) pdwNumberOfPhysicalMonitors];
                    if (!GetPhysicalMonitorsFromHMONITOR(hDesktop, pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray))
                        throw new Exception("无法获取显示器句柄");
                    physicalMonitors.Add(GetMonitorCapabilities(new Monitors()
                    {
                        PhysicalMonitor = pPhysicalMonitorArray[0]
                    }));
                    return true;
                });
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0);
            return physicalMonitors;
        }

        public static Monitors GetMonitorCapabilities(Monitors m)
        {
            uint pdwCapabilitiesStringLengthInCharacters = 0;
            bool flag = GetCapabilitiesStringLength(m.PhysicalMonitor.hphysicalMonitor,
                ref pdwCapabilitiesStringLengthInCharacters);
            for (int index = 0; index < 4 && !flag; ++index)
            {
                Thread.Sleep(200);
                flag = GetCapabilitiesStringLength(m.PhysicalMonitor.hphysicalMonitor,
                    ref pdwCapabilitiesStringLengthInCharacters);
            }

            Console.WriteLine("GetCapabilitiesStringLength  :" + flag.ToString());
            char[] pszASCIICapabilitiesString = new char[(int) pdwCapabilitiesStringLengthInCharacters];
            flag = CapabilitiesRequestAndCapabilitiesReply(m.PhysicalMonitor.hphysicalMonitor,
                pszASCIICapabilitiesString, pdwCapabilitiesStringLengthInCharacters);
            for (int index = 0; index < 4 && !flag; ++index)
            {
                Thread.Sleep(200);
                flag = CapabilitiesRequestAndCapabilitiesReply(m.PhysicalMonitor.hphysicalMonitor,
                    pszASCIICapabilitiesString, pdwCapabilitiesStringLengthInCharacters);
            }

            return ParseVcp(new string(pszASCIICapabilitiesString), m);
        }

        public static Monitors ParseVcp(string capabilityString, Monitors m)
        {
            capabilityString = PrepareCapabilityString(capabilityString);
            List<uint> uintList1 = new List<uint>();
            List<uint> uintList2 = new List<uint>();
            List<uint> uintList3 = new List<uint>();
            HNStruct.ParseState parseState1 = HNStruct.ParseState.DEFAULT;
            HNStruct.VcpState vcpState = HNStruct.VcpState.DEFAULT;
            uint? nullable = new uint?();
            string str1 = "";
            string str2 = capabilityString;
            char[] chArray = new char[1] {' '};
            foreach (string hex in str2.Split(chArray))
            {
                HNStruct.ParseState parseState2 = parseState1;
                switch (hex)
                {
                    case "asset_eep":
                        parseState1 = HNStruct.ParseState.ASSET_EEP;
                        break;
                    case "cmds":
                        parseState1 = HNStruct.ParseState.CMDS;
                        break;
                    case "mccs_ver":
                        parseState1 = HNStruct.ParseState.MCCS_VER;
                        break;
                    case "model":
                        parseState1 = HNStruct.ParseState.MODEL;
                        break;
                    case "mswhql":
                        parseState1 = HNStruct.ParseState.MSWHQL;
                        break;
                    case "prot":
                        parseState1 = HNStruct.ParseState.PROT;
                        break;
                    case "type":
                        parseState1 = HNStruct.ParseState.TYPE;
                        break;
                    case "vcp":
                        parseState1 = HNStruct.ParseState.VCP;
                        break;
                }

                if (parseState1 == parseState2)
                {
                    switch (parseState1)
                    {
                        case HNStruct.ParseState.VCP:
                            if (hex == "(" && nullable.HasValue)
                            {
                                switch (nullable.Value)
                                {
                                    case 20:
                                        vcpState = HNStruct.VcpState.COLOR_PRESET;
                                        continue;
                                    case 96:
                                        vcpState = HNStruct.VcpState.INPUT_SOURCE;
                                        continue;
                                    default:
                                        vcpState = HNStruct.VcpState.SKIP;
                                        continue;
                                }
                            }
                            else
                            {
                                if (hex == ")")
                                {
                                    vcpState = HNStruct.VcpState.DEFAULT;
                                    continue;
                                }

                                if (hex != ")" && hex != "(")
                                {
                                    if (hex != "")
                                    {
                                        try
                                        {
                                            nullable = new uint?((uint) HexToDecConverter(hex));
                                            switch (vcpState)
                                            {
                                                case HNStruct.VcpState.DEFAULT:
                                                    uintList1.Add(nullable.Value);
                                                    continue;
                                                case HNStruct.VcpState.INPUT_SOURCE:
                                                    uintList2.Add(nullable.Value);
                                                    continue;
                                                case HNStruct.VcpState.COLOR_PRESET:
                                                    uintList3.Add(nullable.Value);
                                                    continue;
                                                default:
                                                    continue;
                                            }
                                        }
                                        catch (FormatException ex)
                                        {
                                            parseState1 = HNStruct.ParseState.ERROR;
                                            continue;
                                        }
                                    }
                                    else
                                        continue;
                                }
                                else
                                    continue;
                            }
                        case HNStruct.ParseState.MODEL:
                            if (hex != "(" && hex != ")")
                            {
                                str1 += hex;
                                continue;
                            }

                            continue;
                        default:
                            continue;
                    }
                }
            }

            m.Capabilitys = uintList1;
            m.InputSources = uintList2;
            m.Model = str1;
            m.ColorPresets = uintList3;
            return m;
        }

        private static string PrepareCapabilityString(string capabilityString)
        {
            capabilityString = capabilityString.Replace(" (", "(");
            capabilityString = capabilityString.Replace("( ", "(");
            capabilityString = capabilityString.Replace("(", " ( ");
            capabilityString = capabilityString.Replace(" )", ")");
            capabilityString = capabilityString.Replace(")", " ) ");
            Console.WriteLine(capabilityString.ToString());
            return capabilityString;
        }

        private static int HexToDecConverter(string hex)
        {
            return int.Parse(hex, NumberStyles.HexNumber);
        }

        public static uint[] GetVCPFeature(Monitors m, HNStruct.VCPFeature vcpfeature)
        {
            uint pvct = 0;
            uint pdwCurrentValue = 0;
            uint pdwMaximumValue = 0;
            bool andVcpFeatureReply = GetVCPFeatureAndVCPFeatureReply(m.PhysicalMonitor.hphysicalMonitor,
                (byte) vcpfeature, ref pvct, ref pdwCurrentValue, ref pdwMaximumValue);
            for (int index = 0; index < 4 && !andVcpFeatureReply; ++index)
            {
                Thread.Sleep(200);
                andVcpFeatureReply = GetVCPFeatureAndVCPFeatureReply(m.PhysicalMonitor.hphysicalMonitor,
                    (byte) vcpfeature, ref pvct, ref pdwCurrentValue, ref pdwMaximumValue);
            }

            return new uint[2] {pdwCurrentValue, pdwMaximumValue};
        }

        public static bool setVCPFeature(Monitors m, HNStruct.VCPFeature vcpfeature, uint newVal)
        {
            bool flag = SetVCPFeature(m.PhysicalMonitor.hphysicalMonitor, (byte) vcpfeature, newVal);
            for (int index = 0; index < 4 && !flag; ++index)
            {
                Thread.Sleep(200);
                flag = SetVCPFeature(m.PhysicalMonitor.hphysicalMonitor, (byte) vcpfeature, newVal);
            }

            return flag;
        }

        private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref HNStruct.VCPRect pRect, int dwData);
    }
}