// Decompiled with JetBrains decompiler
// Type: HuionTablet.Fixer4Info
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
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
            IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(saveFileDialog.FileName);
            int num = (int) HuionDriverDLL.hnd_save_config(ref HNStruct.globalInfo.userConfig, coTaskMemAuto);
            HuionDriverDLL.hnd_notify_config_changed();
            saveFileDialog.Dispose();
        }

        public static void importConfigClick(object sender, EventArgs e)
        {
            if ((MessageBox.Show(ResourceCulture.GetString("FormInfo_RemindImportMessageText"),
                    ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)) == DialogResult.OK)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = ResourceCulture.GetString("FormInfo_TextFile") + "|*.xml";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                HNStruct.HNConfig cfg = new HNStruct.HNConfig();
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(Path.GetFullPath(openFileDialog.FileName));
                StringBuilder stringBuilder = new StringBuilder(Path.GetFullPath(openFileDialog.FileName));
                uint result = HuionDriverDLL.hnd_read_config(ref cfg, coTaskMemAuto);
                HNStruct.globalInfo.userConfig = cfg;
                new TabletConfigUtils().SetConfig(result);
                openFileDialog.Dispose();
            }
        }

        public static void defaultConfigClick(object sender, EventArgs e)
        {
            if ((MessageBox.Show(ResourceCulture.GetString("FormInfo_RemindMessageText"),
                    ResourceCulture.GetString("FormInfo_remindText"), MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)) == DialogResult.OK)
                new TabletConfigUtils().SetConfig(
                    HuionDriverDLL.hnd_restore_config(ref HNStruct.globalInfo.userConfig));
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