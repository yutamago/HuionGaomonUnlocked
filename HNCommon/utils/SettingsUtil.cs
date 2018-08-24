// Decompiled with JetBrains decompiler
// Type: HuionTablet.SettingsUtil
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using Huion;
using HuionTablet.utils;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml;

namespace HuionTablet
{
  public class SettingsUtil
  {
    public const string SettingsFileName = "SettingsFile.xml";

    public static bool isAutorun()
    {
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
      {
        int num = registryKey.GetValue("TabletDriver") != null ? 1 : 0;
        registryKey.Close();
        return num != 0;
      }
    }

    public static bool setAutorun(bool value)
    {
      try
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
        {
          if (value)
            registryKey.SetValue("TabletDriver", (object) Utils.ExecutablePath, RegistryValueKind.String);
          else
            registryKey.DeleteValue("TabletDriver");
          registryKey.Close();
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static bool isCommonStartup
    {
      get
      {
        return System.IO.File.Exists(Utils.CommonStartupLinkPath);
      }
      set
      {
        if (value)
        {
          IWshShortcut shortcut = (IWshShortcut)new WshShellClass().CreateShortcut(Utils.CommonStartupLinkPath);
          shortcut.TargetPath = Utils.ExecutablePath;
          shortcut.WindowStyle = 7;
          shortcut.Save();
        }
        else if (Utils.isWin10)
        {
          System.IO.File.Delete(Utils.CommonStartupLinkPath);
        }
        else
        {
          try
          {
            if (!System.IO.File.Exists(Utils.CommonStartupLinkPath))
              return;
            string commonStartupLinkPath = Utils.CommonStartupLinkPath;
            FileInfo fileInfo = new FileInfo(commonStartupLinkPath);
            if (fileInfo.IsReadOnly)
            {
              FileSecurity accessControl = fileInfo.GetAccessControl();
              accessControl.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
              accessControl.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
              fileInfo.SetAccessControl(accessControl);
              System.IO.File.Delete(commonStartupLinkPath);
            }
            else
              System.IO.File.Delete(Utils.CommonStartupLinkPath);
          }
          catch (Exception ex)
          {
            HuionLog.saveLog("删除win7启动快捷方式", ex.Message);
          }
        }
      }
    }

    public static HuionKeyEventArgs DefaultUIShortcut
    {
      get
      {
        return new HuionKeyEventArgs(Keys.H, true, true, false, false);
      }
    }

    public static void CreateSettingsXml()
    {
      if (System.IO.File.Exists("SettingsFile.xml"))
        return;
      XmlDocument xmlDocument = new XmlDocument();
      HuionKeyEventArgs defaultUiShortcut = SettingsUtil.DefaultUIShortcut;
      try
      {
        XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", (string) null);
        xmlDocument.AppendChild((XmlNode) xmlDeclaration);
        XmlElement element1 = xmlDocument.CreateElement("Settings");
        xmlDocument.AppendChild((XmlNode) element1);
        XmlElement element2 = xmlDocument.CreateElement("Autorun");
        element2.SetAttribute("value", "1");
        element1.AppendChild((XmlNode) element2);
        XmlElement element3 = xmlDocument.CreateElement("Shortcuts");
        element1.AppendChild((XmlNode) element3);
        XmlElement element4 = xmlDocument.CreateElement("ShowUI");
        element3.AppendChild((XmlNode) element4);
        element4.SetAttribute("ctrl", defaultUiShortcut.Control ? "1" : "0");
        element4.SetAttribute("alt", defaultUiShortcut.Alt ? "1" : "0");
        element4.SetAttribute("win", defaultUiShortcut.Window ? "1" : "0");
        element4.SetAttribute("shift", defaultUiShortcut.Shift ? "1" : "0");
        element4.SetAttribute("key", string.Concat((object) defaultUiShortcut.KeyCode));
        xmlDocument.Save("SettingsFile.xml");
      }
      catch (Exception ex)
      {
        HuionLog.printSaveLog("Read UI Shortcut", ex.Message);
        HuionLog.printSaveLog("Read UI Shortcut", ex.StackTrace);
      }
    }

    public static HuionKeyEventArgs ShowUIShortcut
    {
      get
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          xmlDocument.Load("SettingsFile.xml");
          XmlAttributeCollection attributes = xmlDocument.SelectSingleNode("Settings").SelectSingleNode("Shortcuts").SelectSingleNode("ShowUI").Attributes;
          return new HuionKeyEventArgs((Keys) Convert.ToInt32(attributes["key"].Value), Convert.ToBoolean(Convert.ToInt32(attributes["ctrl"].Value)), Convert.ToBoolean(Convert.ToInt32(attributes["alt"].Value)), Convert.ToBoolean(Convert.ToInt32(attributes["shift"].Value)), Convert.ToBoolean(Convert.ToInt32(attributes["win"].Value)));
        }
        catch (Exception ex)
        {
          HuionLog.printSaveLog("Read UI Shortcut", ex.Message);
          HuionLog.printSaveLog("Read UI Shortcut", ex.StackTrace);
        }
        return SettingsUtil.DefaultUIShortcut;
      }
      set
      {
        SettingsUtil.CreateSettingsXml();
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          xmlDocument.Load("SettingsFile.xml");
          XmlAttributeCollection attributes = xmlDocument.SelectSingleNode("Settings").SelectSingleNode("Shortcuts").SelectSingleNode("ShowUI").Attributes;
          attributes["key"].Value = string.Concat((object) value.KeyCode);
          attributes["ctrl"].Value = value.Control ? "1" : "0";
          attributes["alt"].Value = value.Alt ? "1" : "0";
          attributes["win"].Value = value.Window ? "1" : "0";
          attributes["shift"].Value = value.Shift ? "1" : "0";
          xmlDocument.Save("SettingsFile.xml");
        }
        catch (Exception ex)
        {
          HuionLog.printSaveLog("Read UI Shortcut", ex.Message);
          HuionLog.printSaveLog("Read UI Shortcut", ex.StackTrace);
        }
      }
    }

    public static bool CheckHotkey(IntPtr handle)
    {
      return KeyboardUtils.CheckHotkey(handle, SettingsUtil.ShowUIShortcut);
    }
  }
}
