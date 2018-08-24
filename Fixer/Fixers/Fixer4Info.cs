// Decompiled with JetBrains decompiler
// Type: HuionTablet.Fixer4Info
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Huion;

namespace HuionTablet
{
    public class Fixer4Info
    {
        public static void exportConfigClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ResourceCulture.GetString("FormInfo_TextFile") + "|*.xml";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string fileName = saveFileDialog.FileName;
            string s = Application.StartupPath + "\\res\\config_user.xml";
            IntPtr coTaskMemAuto1 = Marshal.StringToCoTaskMemAuto(fileName);
            IntPtr coTaskMemAuto2 = Marshal.StringToCoTaskMemAuto(s);
            IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
            Marshal.StructureToPtr((object) TabletConfigUtils.config, num1, true);
            IntPtr num2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNTabletInfo)));
            Marshal.StructureToPtr((object) HNStruct.globalInfo.tabletInfo, num2, true);
            int num3 = (int) HuionDriverDLL.hnx_save_config(num1, num2, coTaskMemAuto2, coTaskMemAuto1);
            HuionDriverDLL.hnd_notify_config_changed();
            Marshal.FreeHGlobal(coTaskMemAuto1);
            Marshal.FreeHGlobal(coTaskMemAuto2);
            Marshal.FreeHGlobal(num2);
            Marshal.FreeHGlobal(num1);
            saveFileDialog.Dispose();
        }

        public static void importConfigClick(object sender, EventArgs e)
        {
            if ((HNStruct.OemType != OEMType.GAOMON
                    ? MessageBox.Show(ResourceCulture.GetString("FormInfo_RemindImportMessageText"),
                        ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question)
                    : buttonForm.importConfig()) == DialogResult.OK)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = ResourceCulture.GetString("FormInfo_TextFile") + "|*.xml";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                HNStruct.HNConfigXML hnConfigXml = new HNStruct.HNConfigXML();
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(Path.GetFullPath(openFileDialog.FileName));
                StringBuilder stringBuilder = new StringBuilder(Path.GetFullPath(openFileDialog.FileName));
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
                IntPtr num = HuionDriverDLL.hnx_read_config(ref HNStruct.globalInfo.tabletInfo, coTaskMemAuto);
                TabletConfigUtils.config = hnConfigXml;
                TabletConfigUtils.config =
                    (HNStruct.HNConfigXML) Marshal.PtrToStructure(num, typeof(HNStruct.HNConfigXML));
                new TabletConfigUtils().SetConfig(TabletConfigUtils.config);
                Marshal.FreeHGlobal(coTaskMemAuto);
                Marshal.FreeHGlobal(num);
                openFileDialog.Dispose();
            }
        }

        public static void defaultConfigClick(object sender, EventArgs e)
        {
            if ((HNStruct.OemType != OEMType.GAOMON
                    ? MessageBox.Show(ResourceCulture.GetString("FormInfo_RemindMessageText"),
                        ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question)
                    : buttonForm.defaultConfig()) == DialogResult.OK)
            {
                try
                {
                    IntPtr coTaskMemAuto =
                        Marshal.StringToCoTaskMemAuto(Application.StartupPath + "\\res\\config_default.xml");
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
                    IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNTabletInfo)));
                    Marshal.StructureToPtr((object) HNStruct.globalInfo.tabletInfo, num1, false);
                    IntPtr num2 = HuionDriverDLL.hnx_read_config(ref HNStruct.globalInfo.tabletInfo, coTaskMemAuto);
                    TabletConfigUtils.config =
                        (HNStruct.HNConfigXML) Marshal.PtrToStructure(num2, typeof(HNStruct.HNConfigXML));
                    new TabletConfigUtils().SetConfig(TabletConfigUtils.config);
                    Marshal.FreeHGlobal(coTaskMemAuto);
                    Marshal.FreeHGlobal(num1);
                    HuionDriverDLL.hnx_free_PHNConfig(num2);
                }
                catch (Exception ex)
                {
                    HuionLog.saveLog("默认配置", ex.Message);
                }
            }
        }

        public static void logoConfigClick(object sender, EventArgs e)
        {
            if (HNStruct.globalInfo.tabletInfo.devType == 34U || HNStruct.globalInfo.tabletInfo.devType == 37U ||
                (HNStruct.globalInfo.tabletInfo.devType == 40U || HNStruct.globalInfo.tabletInfo.devType == 41U))
                Process.Start("http://www.huion.cn");
            else
                Process.Start(DeployConfig.Website);
        }
    }
}