// Decompiled with JetBrains decompiler
// Type: HuionTablet.SettingsUtil
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.IO;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml;
using Huion;
using HuionTablet.utils;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using static HuionTablet.HNStruct;
using File = System.IO.File;

namespace HuionTablet
{
    public class SettingsUtil
    {
        public const string SettingsFileName = "SettingsFile.xml";

        public static HashSet<string> excludedApplications =
            new HashSet<string>(new string[] {"huion tablet.exe", "explorer.exe", "devenv.exe"});

        public static bool isCommonStartup
        {
            get { return File.Exists(Utils.CommonStartupLinkPath); }
            set
            {
                if (value)
                {
                    IWshShortcut shortcut =
                        (IWshShortcut) new WshShellClass().CreateShortcut(Utils.CommonStartupLinkPath);
                    shortcut.TargetPath = Utils.ExecutablePath;
                    shortcut.WindowStyle = 7;
                    shortcut.Save();
                }
                else if (Utils.isWin10)
                {
                    File.Delete(Utils.CommonStartupLinkPath);
                }
                else
                {
                    try
                    {
                        if (!File.Exists(Utils.CommonStartupLinkPath))
                            return;
                        string commonStartupLinkPath = Utils.CommonStartupLinkPath;
                        FileInfo fileInfo = new FileInfo(commonStartupLinkPath);
                        if (fileInfo.IsReadOnly)
                        {
                            FileSecurity accessControl = fileInfo.GetAccessControl();
                            accessControl.AddAccessRule(new FileSystemAccessRule("Everyone",
                                FileSystemRights.FullControl, AccessControlType.Allow));
                            accessControl.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl,
                                AccessControlType.Allow));
                            fileInfo.SetAccessControl(accessControl);
                            File.Delete(commonStartupLinkPath);
                        }
                        else
                            File.Delete(Utils.CommonStartupLinkPath);
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
            get { return new HuionKeyEventArgs(Keys.H, true, true, false, false); }
        }

        public static PerAppSetting? perAppSettingsDefault
        {
            get
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlNode perAppSettingsNode = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("PerAppSettings");

                    if ("1".Equals(perAppSettingsNode?
                        .Attributes["manualMode"]
                        .Value))
                    {
                        XmlNode defaultNode = perAppSettingsNode?.SelectSingleNode("Default");
                        if (defaultNode == null)
                        {
                            return null;
                        }

                        XmlAttributeCollection attributes = defaultNode.Attributes;
                        return new PerAppSetting("__default__",
                            attributes["profile"].Value,
                            Convert.ToBoolean(Convert.ToInt32(attributes["enabled"].Value)));
                    }
                    else
                    {
                        string defaultPath = Path.Combine(perAppSettingsProfileDir, "default.xml");
                        if (File.Exists(defaultPath))
                        {
                            return new PerAppSetting("__default__", defaultPath, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings Apps Default", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings Apps Default", ex.StackTrace);
                }

                return null;
            }
            set
            {
                CreateSettingsXml();
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlNode perAppSettings =
                        xmlDocument.SelectSingleNode("Settings").SelectSingleNode("PerAppSettings");
                    if (perAppSettings.SelectSingleNode("Default") != null)
                    {
                        perAppSettings.RemoveChild(perAppSettings.SelectSingleNode("Default"));
                    }

                    if (!value.HasValue)
                    {
                        return;
                    }

                    XmlElement perAppSettingDefaultElement = xmlDocument.CreateElement("Default");
                    perAppSettingDefaultElement.SetAttribute("enabled", value.Value.enabled ? "1" : "0");
                    perAppSettingDefaultElement.SetAttribute("profile", value.Value.profile);
                    perAppSettings.AppendChild(perAppSettingDefaultElement);

                    xmlDocument.Save("SettingsFile.xml");
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings Apps Default", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings Apps Default", ex.StackTrace);
                }
            }
        }

        public static Dictionary<string, PerAppSetting> perAppSettings
        {
            get
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlNode perAppSettingsNode = xmlDocument.SelectSingleNode("Settings")?
                        .SelectSingleNode("PerAppSettings");
                    XmlNodeList applications = perAppSettingsNode?.ChildNodes;

                    Dictionary<string, PerAppSetting> applicationsMap = new Dictionary<string, PerAppSetting>();
                    if ("1".Equals(perAppSettingsNode.Attributes["manualMode"].Value))
                    {
                        foreach (XmlNode app in applications)
                        {
                            if (!app.Name.Equals("App"))
                            {
                                continue;
                            }

                            applicationsMap.Add(
                                app.Attributes["processFileName"].Value.ToLower(),
                                new PerAppSetting(
                                    app.Attributes["processFileName"].Value,
                                    app.Attributes["profile"].Value,
                                    Convert.ToBoolean(Convert.ToInt32(app.Attributes["enabled"].Value))
                                )
                            );
                        }
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(Path.GetFullPath(perAppSettingsProfileDir), "*.xml",
                            SearchOption.AllDirectories))
                        {
                            applicationsMap.Add(
                                Path.GetFileNameWithoutExtension(file).ToLower() + ".exe",
                                new PerAppSetting(
                                    Path.GetFileNameWithoutExtension(file).ToLower() + ".exe",
                                    Path.GetFileName(file),
                                    true
                                )
                            );
                        }
                    }


                    return applicationsMap;
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings Apps", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings Apps", ex.StackTrace);
                }

                return new Dictionary<string, PerAppSetting>();
            }
            set
            {
                CreateSettingsXml();
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlNode perAppSettings = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("PerAppSettings");

                    foreach (XmlNode child in perAppSettings.ChildNodes)
                    {
                        if (!child.Name.Equals("App"))
                        {
                            continue;
                        }

                        perAppSettings.RemoveChild(child);
                    }

                    foreach (PerAppSetting app in value.Values)
                    {
                        XmlElement appElement = xmlDocument.CreateElement("App");
                        appElement.SetAttribute("enabled", app.enabled ? "1" : "0");
                        appElement.SetAttribute("processFileName", app.processFileName);
                        appElement.SetAttribute("profile", app.profile);
                        perAppSettings.AppendChild(appElement);
                    }

                    xmlDocument.Save("SettingsFile.xml");
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings Apps", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings Apps", ex.StackTrace);
                }
            }
        }

        public static string perAppSettingsProfileDir
        {
            get
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("PerAppSettings")?
                        .Attributes;
                    return Path.GetFullPath(attributes["profileDir"].Value);
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings profileDir", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings profileDir", ex.StackTrace);
                }

                return "";
            }
            set
            {
                CreateSettingsXml();
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("PerAppSettings")?
                        .Attributes;
                    attributes["profileDir"].Value = string.Concat(value);
                    xmlDocument.Save("SettingsFile.xml");
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings profileDir", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings profileDir", ex.StackTrace);
                }
            }
        }

        public static bool isPerAppSettingsEnabled
        {
            get
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("PerAppSettings")?
                        .Attributes;
                    return Convert.ToBoolean(Convert.ToInt32(attributes["enabled"].Value));
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings enabled", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings enabled", ex.StackTrace);
                }

                return false;
            }
            set
            {
                CreateSettingsXml();
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument.SelectSingleNode("Settings")
                        .SelectSingleNode("PerAppSettings").Attributes;
                    attributes["enabled"].Value = value ? "1" : "0";
                    xmlDocument.Save("SettingsFile.xml");
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read PerAppSettings enabled", ex.Message);
                    HuionLog.printSaveLog("Read PerAppSettings enabled", ex.StackTrace);
                }
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
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("Shortcuts")?
                        .SelectSingleNode("ShowUI")?
                        .Attributes;
                    return new HuionKeyEventArgs((Keys) Enum.Parse(typeof(Keys), attributes["key"].Value),
                        Convert.ToBoolean(Convert.ToInt32(attributes["ctrl"].Value)),
                        Convert.ToBoolean(Convert.ToInt32(attributes["alt"].Value)),
                        Convert.ToBoolean(Convert.ToInt32(attributes["shift"].Value)),
                        Convert.ToBoolean(Convert.ToInt32(attributes["win"].Value)));
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read UI Shortcut", ex.Message);
                    HuionLog.printSaveLog("Read UI Shortcut", ex.StackTrace);
                }

                return DefaultUIShortcut;
            }
            set
            {
                CreateSettingsXml();
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("Shortcuts")?
                        .SelectSingleNode("ShowUI")?
                        .Attributes;
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

        public static bool isAutorun()
        {
            using (RegistryKey registryKey =
                Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
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
                using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadWriteSubTree,
                    RegistryRights.FullControl))
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

        public static bool isUpdateReminderEnabled
        {
            get
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("UpdateReminder")?
                        .Attributes;
                    return Convert.ToBoolean(Convert.ToInt32(attributes["enabled"]?.Value));
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Read UpdateReminder", ex.Message);
                    HuionLog.printSaveLog("Read UpdateReminder", ex.StackTrace);
                }

                return true;
            }
            set
            {
                CreateSettingsXml();
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load("SettingsFile.xml");
                    XmlAttributeCollection attributes = xmlDocument
                        .SelectSingleNode("Settings")?
                        .SelectSingleNode("UpdateReminder")?
                        .Attributes;
                    attributes["enabled"].Value = value ? "1" : "0";
                    xmlDocument.Save("SettingsFile.xml");
                }
                catch (Exception ex)
                {
                    HuionLog.printSaveLog("Write UpdateReminder", ex.Message);
                    HuionLog.printSaveLog("Write UpdateReminder", ex.StackTrace);
                }
            }
        }

        public static void CreateSettingsXml()
        {
            if (File.Exists("SettingsFile.xml"))
                return;
            XmlDocument xmlDocument = new XmlDocument();
            HuionKeyEventArgs defaultUiShortcut = DefaultUIShortcut;
            try
            {
                XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", (string) null);
                xmlDocument.AppendChild((XmlNode) xmlDeclaration);
                XmlElement element1 = xmlDocument.CreateElement("Settings");
                xmlDocument.AppendChild((XmlNode) element1);
                XmlElement element2 = xmlDocument.CreateElement("Autorun");
                element2.SetAttribute("value", "1");
                element1.AppendChild((XmlNode) element2);

                XmlElement updateReminder = xmlDocument.CreateElement("UpdateReminder");
                updateReminder.SetAttribute("enabled", "0");
                element1.AppendChild((XmlNode) updateReminder);

                XmlElement perAppSettingsElement = xmlDocument.CreateElement("PerAppSettings");
                perAppSettingsElement.SetAttribute("enabled", "1");


                string defaultProfileDir = Path.GetFullPath(Environment.ExpandEnvironmentVariables("%userprofile%\\.Huion Tablet\\profiles"));

                Directory.CreateDirectory(defaultProfileDir);
                perAppSettingsElement.SetAttribute("profileDir", defaultProfileDir);
                perAppSettingsElement.SetAttribute("manualMode", "0");

                XmlComment comment = xmlDocument.CreateComment(" App and Default tags will be ignored if manualMode=\"0\" ");
                perAppSettingsElement.AppendChild(comment);
                
                XmlElement perAppSettingSampleElement = xmlDocument.CreateElement("App");
                perAppSettingSampleElement.SetAttribute("enabled", "0");
                perAppSettingSampleElement.SetAttribute("processFileName", "sample.exe");
                perAppSettingSampleElement.SetAttribute("profile", "sample.xml");
                perAppSettingsElement.AppendChild((XmlNode) perAppSettingSampleElement);
                XmlElement perAppSettingDefaultElement = xmlDocument.CreateElement("Default");
                perAppSettingDefaultElement.SetAttribute("enabled", "0");
                perAppSettingDefaultElement.SetAttribute("profile", "sample.xml");
                perAppSettingsElement.AppendChild((XmlNode) perAppSettingDefaultElement);

                element1.AppendChild((XmlNode) perAppSettingsElement);

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

        public static bool CheckHotkey(IntPtr handle)
        {
            return KeyboardUtils.CheckHotkey(handle, ShowUIShortcut);
        }
    }
}