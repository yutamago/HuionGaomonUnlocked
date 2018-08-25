// Decompiled with JetBrains decompiler
// Type: HuionTablet.HuionTalbet
// Assembly: Huion Tablet, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 89741050-C3D9-47F7-9E24-1E7879C81D96
// Assembly location: D:\Program Files (x86)\Huion Tablet\app.publish\Huion Tablet.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Huion;
using HuionTablet.Forms;
using HuionTablet.Lib;
using HuionTablet.Properties;
using HuionTablet.utils;
using HuionTablet.view;
using Boolean = HuionTablet.utils.Boolean;
using Timer = System.Windows.Forms.Timer;
using Void = HuionTablet.utils.Void;

namespace HuionTablet
{
    public class HuionTalbet : BaseForm
    {
        public static bool IsReminder;
        private Button btnAdmin;
        private Button btnApplay;
        private Button btnClose;
        private Button btnOK;
        private IContainer components = (IContainer) null;
        private ContextMenuStrip contextMenuStrip1;
        private string currentSettings = "";
        private bool curEnabled = false;
        private int curIsRunning;
        private USB ezUSB = new USB();
        private int isRunning;
        private Label labelTabletState;
        private List<Form> mForms;
        private TabType mTabType;
        private NotifyIcon notifyIcon1;
        private NotifyIcon notifyIcon2;
        private int openNum = 0;
        private HNPanel panel1;
        private Panel panelBottom;
        private Panel panelContent;
        private HNPanel panelHotKey;
        private HNPanel panelInfo;
        private Panel panelTabControl;
        private HNPanel panelTabletPen;
        private Panel panelWindow;
        private HNPanel panelWorkArea;
        private string lastForegroundApplication;
        private Dictionary<string, HNStruct.PerAppSetting> perAppSettings;
        private bool perAppSettingsActive;
        private HNStruct.PerAppSetting? perAppSettingsRest;
        private string perAppSettingsWorkspace;
        private Timer timer1;
        private Timer timer2;
        private Timer timer3;
        private Timer checkForegroundWindowInterval;
        private ToolStripMenuItem tsmiExit;
        private ToolStripMenuItem tsmiSettings;

        public HuionTalbet()
        {
            this.InitializeComponent();
            this.ControlBox = true;
            DeployConfig.isNewUI = true;
            SettingsUtil.CreateSettingsXml();
            this.setViewText8Locale();
            this.mForms = new List<Form>();
            ThreadPool.SetMaxThreads(5, 5);
            this.notifyIcon1.Icon = Fixer4Main.getNotifyIcon(false);
            this.checkForegroundWindowInterval = new Timer();
            this.checkForegroundWindowInterval.Interval = 500;
            this.checkForegroundWindowInterval.Tick += checkForegroundWindowTick;

            if (Utils.isWin10)
                SettingsUtil.setAutorun(false);
            else
                SettingsUtil.isCommonStartup = false;

            this.perAppSettingsActive = SettingsUtil.isPerAppSettingsActive;
            this.perAppSettings = SettingsUtil.perAppSettings;
            this.perAppSettingsRest = SettingsUtil.perAppSettingsRest;
            this.perAppSettingsWorkspace = SettingsUtil.perAppSettingsWorkspace;

            if (this.perAppSettingsActive)
            {
                this.checkForegroundWindowInterval.Start();
            }
        }

        public static bool isReminder
        {
            get { return IsReminder; }
            set { IsReminder = value; }
        }

        private void checkForegroundWindowTick(object sender, EventArgs e)
        {
            IntPtr hwnd = GetForegroundWindow();
            int processId = GetWindowProcessID(hwnd);
            if (processId == 1)
            {
                return;
            }

            Process process = Process.GetProcessById(processId);
            string processFileName = Path.GetFileName(process.MainModule.FileName);
            if (!processFileName.Equals(lastForegroundApplication))
            {
                lastForegroundApplication = processFileName;

                HuionLog.printLog("FocusChanged", processFileName.ToLower());
                handleFocusChange(processFileName.ToLower());
            }
        }

        private void handleFocusChange(string processName)
        {
            if (processName.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName.ToLower())) ||
                SettingsUtil.excludedApplications.Contains(processName))
            {
                // Excluded app
                return;
            }

            HNStruct.PerAppSetting appSetting;
            if (this.perAppSettings.ContainsKey(processName))
            {
                appSetting = this.perAppSettings[processName];
            }
            else if (this.perAppSettingsRest.HasValue)
            {
                appSetting = this.perAppSettingsRest.Value;
            }
            else
            {
                // No setting found
                return;
            }

            if (this.currentSettings == appSetting.settingName)
            {
                HuionLog.printLog("FocusChanged", "Error: already active");
                // already active
                return;
            }
            else if (!appSetting.active)
            {
                HuionLog.printLog("FocusChanged", "Error: not enabled");
                // setting not enabled
                return;
            }

            string path = Path.GetFullPath(Path.Combine(this.perAppSettingsWorkspace, appSetting.settingName));
            if (!File.Exists(path))
            {
                HuionLog.printLog("FocusChanged", "Error: file not found");
                return;
            }

            HuionLog.printLog("FocusChanged", "Info: loading profile");
            
            IntPtr coTaskMemAuto = Marshal.StringToCoTaskMemAuto(path);
            Marshal.AllocHGlobal(Marshal.SizeOf(typeof(HNStruct.HNConfigXML)));
            IntPtr num = HuionDriverDLL.hnx_read_config(ref HNStruct.globalInfo.tabletInfo, coTaskMemAuto);
            TabletConfigUtils.config = (HNStruct.HNConfigXML) Marshal.PtrToStructure(num, typeof(HNStruct.HNConfigXML));
            new TabletConfigUtils().SetConfig(TabletConfigUtils.config);
            Marshal.FreeHGlobal(coTaskMemAuto);
            Marshal.FreeHGlobal(num);
            currentSettings = appSetting.settingName;
            Fixer4Main.applayClick(null, null);

            HuionLog.printLog("FocusChanged", "Info: profile loaded");

            this.notifyIcon1.ShowBalloonTip(3, "Profile switched", appSetting.settingName, ToolTipIcon.Info);
            this.notifyIcon1.Text = ResourceCulture.GetString("FormHuionTabletNotifyIconText") + " (" +
                                    appSetting.settingName + ")";
            this.ResumeLayout(false);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);

        private Int32 GetWindowProcessID(IntPtr hwnd)
        {
            Int32 pid = 1;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        private void onDisplayChanged()
        {
            this.SetDisplayMonitors();
            if (this.mTabType != TabType.TabWorkArea)
                return;
            this.mTabType = TabType.TabInfo;
            this.buttonWorkArea_Click((object) null, (EventArgs) null);
        }

        private void MiddleModule_eventPost(object post, object msg)
        {
            this.Invoke((Delegate) new Boolean(this.setViewState), (object) HNStruct.globalInfo.bOpenedTablet);
        }

        private void HuionTablet_SizeChanged(object sender, EventArgs e)
        {
        }

        private void showBalloonTip()
        {
            this.clearBufferForms();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Clicks > 1)
                return;
            this.KeyboardUtils_showFormEvent();
        }

        private void HuionTalbet_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.ShowInTaskbar = true;
                KeyboardUtils.unlistenHotKey(this.Handle);
                this.Focus();
            }
            else
            {
                this.ShowInTaskbar = false;
                KeyboardUtils.listenHotKey(this.Handle, new Void(this.KeyboardUtils_showFormEvent),
                    SettingsUtil.ShowUIShortcut);
            }

            this.showBalloonTip();
        }

        private void KeyboardUtils_showFormEvent()
        {
            if (this.Visible)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void HuionTalbet_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            if (!this.tsmiExit.CheckOnClick)
                return;
            this.clearForms();
            this.clearBufferForms();
            e.Cancel = false;
        }

        private void tsmiItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem) sender;
            if (toolStripMenuItem == this.tsmiExit)
            {
                try
                {
                    this.Dispose(true);
                    this.Close();
                    this.ezUSB.RemoveUSBEventWatcher();
                    Process.GetCurrentProcess().Kill();
                }
                catch (Exception ex)
                {
                    HuionLog.saveLog("退出程序", ex.Message);
                }
            }
            else
            {
                if (toolStripMenuItem != this.tsmiSettings)
                    return;
                this.Show();
                FormSettings.showForm(this.Handle, true, 0);
            }
        }

        public override void onSettingsButtonClick(object sender, EventArgs e)
        {
            base.onSettingsButtonClick(sender, e);
            FormSettings.showForm(this.Handle, false, 0);
        }

        private void setTabPanelClicked(Panel v, TabType tab)
        {
            this.mTabType = tab;
            this.clearForms();
            TimerSession.userOperation();
            this.panelHotKey.Refresh();
            this.panelInfo.Refresh();
            this.panelTabletPen.Refresh();
            this.panelWorkArea.Refresh();
        }

        private void clearForms()
        {
            foreach (Control control in (ArrangedElementCollection) this.panelWindow.Controls)
            {
                if (control is IDestroy)
                    ((IDestroy) control).onDestroy();
                if (control is Form && !(control is HuionTalbet))
                    this.mForms.Add((Form) control);
            }

            this.panelWindow.Controls.Clear();
        }

        private void buttonHotKey_Click(object sender, EventArgs e)
        {
            if (this.mTabType == TabType.TabHotkey)
                return;
            this.setTabPanelClicked((Panel) this.panelHotKey, TabType.TabHotkey);
            FormHotKey formHotKey = new FormHotKey(PictureBoxSizeMode.StretchImage);
            formHotKey.FormBorderStyle = FormBorderStyle.None;
            formHotKey.TopLevel = false;
            formHotKey.Show();
            this.clearForms();
            this.panelWindow.Controls.Add((Control) formHotKey);
        }

        private void buttonTabletPen_Click(object sender, EventArgs e)
        {
            if (this.mTabType == TabType.TabTabletPen)
                return;
            this.setTabPanelClicked((Panel) this.panelTabletPen, TabType.TabTabletPen);
            FormTabletPen formTabletPen = new FormTabletPen();
            formTabletPen.FormBorderStyle = FormBorderStyle.None;
            formTabletPen.TopLevel = false;
            formTabletPen.Show();
            this.clearForms();
            this.panelWindow.Controls.Add((Control) formTabletPen);
        }

        private void buttonWorkArea_Click(object sender, EventArgs e)
        {
            if (this.mTabType == TabType.TabWorkArea)
                return;
            this.setTabPanelClicked((Panel) this.panelWorkArea, TabType.TabWorkArea);
            FormWorkArea formWorkArea = new FormWorkArea(ScreenHelper.ScreenNum, ScreenHelper.MonitorInfos);
            formWorkArea.FormBorderStyle = FormBorderStyle.None;
            formWorkArea.TopLevel = false;
            formWorkArea.Show();
            this.clearForms();
            this.panelWindow.Controls.Add((Control) formWorkArea);
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            if (this.mTabType == TabType.TabInfo)
                return;
            this.setTabPanelClicked((Panel) this.panelInfo, TabType.TabInfo);
            FormInfo formInfo = new FormInfo(HNStruct.OemType);
            formInfo.FormBorderStyle = FormBorderStyle.None;
            formInfo.TopLevel = false;
            formInfo.Show();
            this.clearForms();
            this.panelWindow.Controls.Add((Control) formInfo);
        }

        private void tabPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel) sender;
            TabType tag = (TabType) panel.Tag;
            Pen pen = new Pen(Color.Black, 2f);
            if (this.mTabType == tag)
            {
                pen = new Pen(Color.White, 2f);
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width, panel.Height);
            }
            else
                e.Graphics.DrawRectangle(pen, 0, panel.Height, panel.Width, panel.Height);

            pen.Dispose();
        }

        private void displayFormation()
        {
            this.labelTabletState.Text = HNStruct.tabletTextInfo;
            this.panelInfo.Enabled = HNStruct.globalInfo.bOpenedTablet;
            this.panelWorkArea.Enabled = HNStruct.globalInfo.bOpenedTablet;
            this.panelTabletPen.Enabled = HNStruct.globalInfo.bOpenedTablet;
            this.panelHotKey.Enabled = HNStruct.globalInfo.bOpenedTablet;
            this.btnOK.Enabled = HNStruct.globalInfo.bOpenedTablet;
            this.btnApplay.Enabled = HNStruct.globalInfo.bOpenedTablet;
            this.notifyIcon1.Icon = Fixer4Main.getNotifyIcon(HNStruct.globalInfo.bOpenedTablet);
            this.btnAdmin.Visible = !Utils.isAdmin();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 126:
                    break;
                case 512:
                    TimerSession.userOperation();
                    break;
                case 536:
                    switch (m.WParam.ToInt32())
                    {
                        case 4:
                            this.timer3.Stop();
                            ThreadPool.QueueUserWorkItem(new WaitCallback(TabletConfigUtils.closeDevice));
                            break;
                        case 7:
                            this.timer3.Start();
                            break;
                    }

                    MiddleModule.eventPost += new Post(this.MiddleModule_eventPost);
                    break;
                case 1044:
                    break;
                case 1054:
                    if (this.Visible)
                        break;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    break;
                case 1064:
                    if (this.Visible)
                        break;
                    KeyboardUtils.listenHotKey(this.Handle, new Void(this.KeyboardUtils_showFormEvent),
                        SettingsUtil.ShowUIShortcut);
                    break;
                default:
                    Fixer4Main.onDeviceChanged(ref m);
                    KeyboardUtils.onHotKey(ref m);
                    break;
            }
        }

        private void HuionTalbet_Load(object sender, EventArgs e)
        {
            Fixer4Main.MainForm = (Form) this;
            if (!SettingsUtil.CheckHotkey(this.Handle))
                HuionMessageBox.HotkeyConflict();
            this.tsmiSettings.Image = (Image) ImageHelper.getDllImage("settings.png");
            this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
            MiddleModule.initMainFormHandle((Form) this);
            MiddleModule.eventPost += new Post(this.MiddleModule_eventPost);
            this.timer3.Start();
            Fixer4Main.setDisplayChangedCallback(
                new SystemSessionService.SystemDisplayChangedCallback(this.onDisplayChanged));
            Fixer4Main.listenSystemStatus();
            DeviceStatusUtils.deviceConfigListener +=
                new DeviceStatusUtils.DeviceConfigChanged(this.onDeviceConfigChanged);
            this.btnApplay.Click += new EventHandler(Fixer4Main.applayClick);
            this.btnOK.Click += new EventHandler(Fixer4Main.okClick);
            this.btnClose.Click += new EventHandler(Fixer4Main.closeClick);
            this.btnAdmin.MouseDown += new MouseEventHandler(this.btnColor);
            this.btnApplay.MouseDown += new MouseEventHandler(this.btnColor);
            this.btnOK.MouseDown += new MouseEventHandler(this.btnColor);
            this.btnClose.MouseDown += new MouseEventHandler(this.btnColor);
            this.btnAdmin.MouseUp += new MouseEventHandler(this.btnColorDefault);
            this.btnApplay.MouseUp += new MouseEventHandler(this.btnColorDefault);
            this.btnOK.MouseUp += new MouseEventHandler(this.btnColorDefault);
            this.btnClose.MouseUp += new MouseEventHandler(this.btnColorDefault);
            this.FormClosed += new FormClosedEventHandler(Fixer4Main.FormClosed);
            this.KeyDown += new KeyEventHandler(KeyboardUtils.onKeyDown);
            this.displayFormation();
            this.KeyPreview = true;
            this.buttonInfo_Click(sender, (EventArgs) null);
            this.SetDisplayMonitors();
            this.panelHotKey.MouseMove += new MouseEventHandler(Fixer4Main.onMouseMove);
            this.panelInfo.MouseMove += new MouseEventHandler(Fixer4Main.onMouseMove);
            this.panelTabletPen.MouseMove += new MouseEventHandler(Fixer4Main.onMouseMove);
            this.panelWorkArea.MouseMove += new MouseEventHandler(Fixer4Main.onMouseMove);
            this.panel1.MouseMove += new MouseEventHandler(Fixer4Main.onMouseMove);
            this.btnAdmin.Click += new EventHandler(Fixer4Main.adminClick);
            this.panelHotKey.Tag = (object) TabType.TabHotkey;
            this.panelInfo.Tag = (object) TabType.TabInfo;
            this.panelTabletPen.Tag = (object) TabType.TabTabletPen;
            this.panelWorkArea.Tag = (object) TabType.TabWorkArea;
            TimerSession.UserLongtimeNoOperationListener +=
                new TimerSession.UserLongtimeNoOperationCallback(this.onUserLongtimeNoOperation);
            TimerSession.AutoOperationListener += new ElapsedEventHandler(Fixer4Main.T_Elapsed);
            new Thread(new ThreadStart(myThread)).Start();
            this.timer2.Start();
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.notifyIcon1.MouseClick += new MouseEventHandler(this.notifyIcon1_MouseClick);
        }

        private void onDeviceConfigChanged(int type)
        {
            switch (type)
            {
                case 2:
                    this.Invoke((Delegate) new Void(this.onDeviceConfigChanged));
                    break;
                case 16:
                    keyDisplay.keydisplayText = 1;
                    this.Invoke((Delegate) new Void(this.onMKeyIndexChanged));
                    break;
                case 17:
                    keyDisplay.keydisplayText = 2;
                    this.Invoke((Delegate) new Void(this.onMKeyIndexChanged));
                    break;
                case 18:
                    keyDisplay.keydisplayText = 3;
                    this.Invoke((Delegate) new Void(this.onMKeyIndexChanged));
                    break;
            }
        }

        private void onDeviceConfigChanged()
        {
            new TabletConfigUtils().readConfig();
            if (this.mTabType != TabType.TabWorkArea)
                return;
            this.mTabType = TabType.TabInfo;
            this.buttonWorkArea_Click((object) null, (EventArgs) null);
        }

        private void onMKeyIndexChanged()
        {
            if (keyDisplay.keydisplay == null)
            {
                keyDisplay.keydisplay = new keyDisplay();
                keyDisplay.keydisplay.Show();
            }
            else
                keyDisplay.keydisplay.Activate();
        }

        private void onUserLongtimeNoOperation()
        {
            this.Invoke((Delegate) new Void(this.setPanelWindow));
            this.Invoke((Delegate) new Void(this.clearBufferForms));
            MiddleModule.PostMessage((object) this, (object) 0);
        }

        private void clearBufferForms()
        {
            foreach (Form mForm in this.mForms)
            {
                mForm.Close();
                mForm.Dispose();
            }

            this.mForms.Clear();
            ResourceCulture.disposeResource();
        }

        private void SetDisplayMonitors()
        {
            ScreenHelper.getMonitorInfos();
        }

        private void setViewText8Locale()
        {
            this.panelHotKey.Text = ResourceCulture.GetString("FormHuionTablet_lbHotKeyText");
            this.panelTabletPen.Text = ResourceCulture.GetString("FormHuionTablet_lbTabletPenText");
            this.panelWorkArea.Text = ResourceCulture.GetString("FormHuionTablet_lbWorkAreaText");
            this.panelInfo.Text = ResourceCulture.GetString("FormHuionTablet_lbInfoText");
            this.btnOK.Text = ResourceCulture.GetString("FormHuionTablet_btOKText");
            this.btnClose.Text = ResourceCulture.GetString("closeForm");
            this.btnApplay.Text = ResourceCulture.GetString("FormHuionTablet_btAppText");
            this.Text = Fixer4Main.FormTitle();
            HNStruct.tabletTextInfo = Fixer4Main.getStatusText(false);
            this.labelTabletState.Enabled = false;
            this.labelTabletState.Text = HNStruct.tabletTextInfo;
            this.tsmiExit.Text = ResourceCulture.GetString("FormHuionTablet_exit");
            this.tsmiSettings.Text = ResourceCulture.GetString("SettingsTitle");
            this.notifyIcon1.Text = ResourceCulture.GetString("FormHuionTabletNotifyIconText");
            this.btnAdmin.Text = ResourceCulture.GetString("applyAdmin");
        }

        private void setViewState(bool enabled)
        {
            if (this.curEnabled != enabled)
            {
                this.labelTabletState.Text = Fixer4Main.getStatusText(enabled);
                this.labelTabletState.Enabled = enabled;
                this.panelHotKey.Enabled = enabled;
                this.panelTabletPen.Enabled = enabled;
                this.panelWorkArea.Enabled = enabled;
                this.panelInfo.Enabled = enabled;
                this.btnOK.Enabled = enabled;
                this.btnApplay.Enabled = enabled;
                this.notifyIcon1.Icon = Fixer4Main.getNotifyIcon(enabled);
                this.showBalloonTip();
                this.setPanelWindow();
            }

            this.curEnabled = enabled;
        }

        private void setPanelWindow()
        {
            this.mTabType = TabType.TabHotkey;
            this.buttonInfo_Click((object) null, (EventArgs) null);
        }

        private void panelWindow_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Gray, 1f);
            e.Graphics.DrawLine(pen, 0, this.panelWindow.Height - 1, this.panelWindow.Width,
                this.panelWindow.Height - 1);
            pen.Dispose();
        }

        private void panelWindow_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsUpdate || this.openNum >= 1)
                    return;
                Console.WriteLine(this.openNum.ToString());
                ++this.openNum;
                this.notifyIcon1.ShowBalloonTip(3, ResourceCulture.GetString("Update_Remind"),
                    ResourceCulture.GetString("Update_RemindVersion"), ToolTipIcon.Info);
                IsReminder = true;
            }
            catch (Exception ex)
            {
                HuionLog.saveLog("更新提示", ex.Message);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.isRunning = 0;
            HuionDriverDLL.SystemParametersInfo(114, 0, ref this.isRunning, 0);
            if (this.curIsRunning == this.isRunning)
                return;
            this.curIsRunning = this.isRunning;
            HuionDriverDLL.hnd_set_screenSaverRunning(this.isRunning);
        }

        private void btnColor(object sender, EventArgs e)
        {
            ((Control) sender).BackColor = Color.FromArgb(5, 0, 89, 128);
        }

        private void btnColorDefault(object sender, EventArgs e)
        {
            ((Control) sender).BackColor = Color.Transparent;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Fixer4Main.OpenDevice();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer) new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HuionTalbet));
            this.panelWindow = new Panel();
            this.btnOK = new Button();
            this.btnClose = new Button();
            this.btnApplay = new Button();
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.tsmiSettings = new ToolStripMenuItem();
            this.tsmiExit = new ToolStripMenuItem();
            this.timer1 = new Timer(this.components);
            this.btnAdmin = new Button();
            this.panelContent = new Panel();
            this.panelBottom = new Panel();
            this.panelTabControl = new Panel();
            this.panelInfo = new HNPanel();
            this.panelWorkArea = new HNPanel();
            this.panelTabletPen = new HNPanel();
            this.panelHotKey = new HNPanel();
            this.panel1 = new HNPanel();
            this.labelTabletState = new Label();
            this.notifyIcon2 = new NotifyIcon(this.components);
            this.timer2 = new Timer(this.components);
            this.timer3 = new Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTabControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.panelWindow.Dock = DockStyle.Fill;
            this.panelWindow.Location = new Point(148, 0);
            this.panelWindow.Margin = new Padding(2, 3, 2, 3);
            this.panelWindow.Name = "panelWindow";
            this.panelWindow.Padding = new Padding(0, 0, 0, 1);
            this.panelWindow.Size = new Size(838, 583);
            this.panelWindow.TabIndex = 5;
            this.panelWindow.SizeChanged += new EventHandler(this.panelWindow_SizeChanged);
            this.panelWindow.Paint += new PaintEventHandler(this.panelWindow_Paint);
            this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOK.AutoEllipsis = true;
            this.btnOK.BackColor = Color.Transparent;
            this.btnOK.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Location = new Point(434, 5);
            this.btnOK.Margin = new Padding(2, 3, 2, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(130, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.AutoEllipsis = true;
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Location = new Point(569, 5);
            this.btnClose.Margin = new Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(130, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnApplay.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnApplay.AutoEllipsis = true;
            this.btnApplay.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            this.btnApplay.FlatStyle = FlatStyle.Flat;
            this.btnApplay.Location = new Point(704, 5);
            this.btnApplay.Margin = new Padding(2, 3, 2, 3);
            this.btnApplay.Name = "btnApplay";
            this.btnApplay.Size = new Size(130, 25);
            this.btnApplay.TabIndex = 2;
            this.btnApplay.Text = "应用";
            this.btnApplay.UseVisualStyleBackColor = false;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
            {
                (ToolStripItem) this.tsmiSettings,
                (ToolStripItem) this.tsmiExit
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(123, 48);
            this.tsmiSettings.ImageAlign = ContentAlignment.MiddleLeft;
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new Size(122, 22);
            this.tsmiSettings.Text = "Settings";
            this.tsmiSettings.Click += new EventHandler(this.tsmiItemClick);
            this.tsmiExit.ImageAlign = ContentAlignment.BottomRight;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new Size(122, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new EventHandler(this.tsmiItemClick);
            this.btnAdmin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAdmin.AutoEllipsis = true;
            this.btnAdmin.BackColor = SystemColors.Control;
            this.btnAdmin.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            this.btnAdmin.FlatStyle = FlatStyle.Flat;
            this.btnAdmin.Location = new Point(133, 5);
            this.btnAdmin.Margin = new Padding(3, 4, 3, 4);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new Size(165, 25);
            this.btnAdmin.TabIndex = 10;
            this.btnAdmin.Text = "button1";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.panelContent.BackColor = Color.Transparent;
            this.panelContent.Controls.Add((Control) this.panelWindow);
            this.panelContent.Controls.Add((Control) this.panelBottom);
            this.panelContent.Controls.Add((Control) this.panelTabControl);
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.Location = new Point(1, 31);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new Size(986, 618);
            this.panelContent.TabIndex = 11;
            this.panelBottom.Controls.Add((Control) this.btnOK);
            this.panelBottom.Controls.Add((Control) this.btnAdmin);
            this.panelBottom.Controls.Add((Control) this.btnApplay);
            this.panelBottom.Controls.Add((Control) this.btnClose);
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Location = new Point(148, 583);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new Size(838, 35);
            this.panelBottom.TabIndex = 1;
            this.panelTabControl.Controls.Add((Control) this.panelInfo);
            this.panelTabControl.Controls.Add((Control) this.panelWorkArea);
            this.panelTabControl.Controls.Add((Control) this.panelTabletPen);
            this.panelTabControl.Controls.Add((Control) this.panelHotKey);
            this.panelTabControl.Controls.Add((Control) this.panel1);
            this.panelTabControl.Dock = DockStyle.Left;
            this.panelTabControl.Location = new Point(0, 0);
            this.panelTabControl.Name = "panelTabControl";
            this.panelTabControl.Size = new Size(148, 618);
            this.panelTabControl.TabIndex = 0;
            this.panelInfo.BackColor = Color.Transparent;
            this.panelInfo.BackgroundImage = (Image) Resources.icon3;
            this.panelInfo.BackgroundImageLayout = ImageLayout.Stretch;
            this.panelInfo.Dock = DockStyle.Top;
            this.panelInfo.Enabled = false;
            this.panelInfo.ForeColor = SystemColors.Window;
            this.panelInfo.Location = new Point(0, 240);
            this.panelInfo.Margin = new Padding(2, 3, 2, 3);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new Size(148, 80);
            this.panelInfo.TabIndex = 0;
            this.panelInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.panelInfo.TextColor = SystemColors.Window;
            this.panelInfo.TextDock = DockStyle.Right;
            this.panelInfo.TextHeight = 80;
            this.panelInfo.TextWidth = 94;
            this.panelInfo.Click += new EventHandler(this.buttonInfo_Click);
            this.panelInfo.Paint += new PaintEventHandler(this.tabPanel_Paint);
            this.panelWorkArea.BackColor = Color.Transparent;
            this.panelWorkArea.BackgroundImage = (Image) Resources.icon2;
            this.panelWorkArea.BackgroundImageLayout = ImageLayout.Stretch;
            this.panelWorkArea.Dock = DockStyle.Top;
            this.panelWorkArea.Enabled = false;
            this.panelWorkArea.ForeColor = SystemColors.Window;
            this.panelWorkArea.Location = new Point(0, 160);
            this.panelWorkArea.Margin = new Padding(2, 3, 2, 3);
            this.panelWorkArea.Name = "panelWorkArea";
            this.panelWorkArea.Size = new Size(148, 80);
            this.panelWorkArea.TabIndex = 0;
            this.panelWorkArea.TextAlign = ContentAlignment.MiddleLeft;
            this.panelWorkArea.TextColor = SystemColors.Window;
            this.panelWorkArea.TextDock = DockStyle.Right;
            this.panelWorkArea.TextHeight = 80;
            this.panelWorkArea.TextWidth = 94;
            this.panelWorkArea.Click += new EventHandler(this.buttonWorkArea_Click);
            this.panelWorkArea.Paint += new PaintEventHandler(this.tabPanel_Paint);
            this.panelTabletPen.BackColor = Color.Transparent;
            this.panelTabletPen.BackgroundImage = (Image) Resources.icon1;
            this.panelTabletPen.BackgroundImageLayout = ImageLayout.Stretch;
            this.panelTabletPen.Dock = DockStyle.Top;
            this.panelTabletPen.Enabled = false;
            this.panelTabletPen.ForeColor = SystemColors.Window;
            this.panelTabletPen.Location = new Point(0, 80);
            this.panelTabletPen.Margin = new Padding(2, 3, 2, 3);
            this.panelTabletPen.Name = "panelTabletPen";
            this.panelTabletPen.Size = new Size(148, 80);
            this.panelTabletPen.TabIndex = 0;
            this.panelTabletPen.TextAlign = ContentAlignment.MiddleLeft;
            this.panelTabletPen.TextColor = SystemColors.Window;
            this.panelTabletPen.TextDock = DockStyle.Right;
            this.panelTabletPen.TextHeight = 80;
            this.panelTabletPen.TextWidth = 94;
            this.panelTabletPen.Click += new EventHandler(this.buttonTabletPen_Click);
            this.panelTabletPen.Paint += new PaintEventHandler(this.tabPanel_Paint);
            this.panelHotKey.BackgroundImage = (Image) Resources.icon0;
            this.panelHotKey.BackgroundImageLayout = ImageLayout.Stretch;
            this.panelHotKey.Dock = DockStyle.Top;
            this.panelHotKey.Enabled = false;
            this.panelHotKey.ForeColor = SystemColors.Window;
            this.panelHotKey.Location = new Point(0, 0);
            this.panelHotKey.Margin = new Padding(2, 3, 2, 3);
            this.panelHotKey.Name = "panelHotKey";
            this.panelHotKey.Size = new Size(148, 80);
            this.panelHotKey.TabIndex = 0;
            this.panelHotKey.TextAlign = ContentAlignment.MiddleLeft;
            this.panelHotKey.TextColor = SystemColors.Window;
            this.panelHotKey.TextDock = DockStyle.Right;
            this.panelHotKey.TextHeight = 80;
            this.panelHotKey.TextWidth = 94;
            this.panelHotKey.Click += new EventHandler(this.buttonHotKey_Click);
            this.panelHotKey.Paint += new PaintEventHandler(this.tabPanel_Paint);
            this.panel1.BackColor = Color.Transparent;
            this.panel1.BackgroundImage = (Image) Resources.bg;
            this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.panel1.Controls.Add((Control) this.labelTabletState);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Margin = new Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(148, 618);
            this.panel1.TabIndex = 7;
            this.panel1.TextAlign = ContentAlignment.MiddleCenter;
            this.panel1.TextColor = SystemColors.ControlText;
            this.panel1.TextDock = DockStyle.Fill;
            this.panel1.TextHeight = 551;
            this.panel1.TextWidth = 148;
            this.labelTabletState.Dock = DockStyle.Bottom;
            this.labelTabletState.ForeColor = Color.White;
            this.labelTabletState.Location = new Point(0, 551);
            this.labelTabletState.Margin = new Padding(2, 0, 2, 0);
            this.labelTabletState.Name = "labelTabletState";
            this.labelTabletState.Size = new Size(148, 67);
            this.labelTabletState.TabIndex = 0;
            this.labelTabletState.Text = "设备已连接状态";
            this.labelTabletState.TextAlign = ContentAlignment.MiddleCenter;
            this.notifyIcon2.Text = "notifyIcon2";
            this.notifyIcon2.Visible = true;
            this.timer2.Enabled = true;
            this.timer2.Interval = 3000;
            this.timer2.Tick += new EventHandler(this.timer2_Tick);
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new EventHandler(this.timer3_Tick);
            this.AcceptButton = (IButtonControl) this.btnOK;
            this.AutoScaleDimensions = new SizeF(96f, 96f);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.CancelButton = (IButtonControl) this.btnClose;
            this.ClientSize = new Size(988, 650);
            this.Controls.Add((Control) this.panelContent);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
            this.Margin = new Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = nameof(HuionTalbet);
            this.ShowSettingsIcon = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Minimized;
            this.FormClosing += new FormClosingEventHandler(this.HuionTalbet_FormClosing);
            this.Load += new EventHandler(this.HuionTalbet_Load);
            this.SizeChanged += new EventHandler(this.HuionTablet_SizeChanged);
            this.VisibleChanged += new EventHandler(this.HuionTalbet_VisibleChanged);
            this.Controls.SetChildIndex((Control) this.panelContent, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelTabControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}