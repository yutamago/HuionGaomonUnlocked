// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormSettings
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Huion;
using HuionTablet.Lib;
using HuionTablet.view;
using Void = HuionTablet.utils.Void;

namespace HuionTablet
{
    public class FormSettings : BaseForm
    {
        private static FormSettings mSettingsForm = new FormSettings();
        private HuionClickableButton btnAbout;
        private HuionClickableButton btnBrightness;
        private HuionClickableButton btnCommon;
        private HuionClickableButton btnShortcuts;
        private IContainer components;
        private bool isFromToolbar;
        private IntPtr mMainFormHandle;
        private Panel panel1;
        private Panel panel2;
        private Panel panelContent;
        private Panel panelMain;
        private Panel panelTab;
        private PictureBox pictureBox1;
        private int tabIndex;

        public FormSettings()
        {
            this.InitializeComponent();
            this.ControlBox = true;
            this.ShowSettingsIcon = false;
            this.MinimizeBox = false;
            this.AutoScroll = true;
            this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
            this.Text = ResourceCulture.GetString("SettingsTitle");
            this.btnCommon.Text = ResourceCulture.GetString("SetttingsCommon");
            this.btnShortcuts.Text = ResourceCulture.GetString("SettingsHotkey");
            this.btnAbout.Text = ResourceCulture.GetString("soft_update");
            this.btnBrightness.Text = "亮度设置";
            MiddleModule.eventPost += new Post(this.MiddleModule_eventSend);
        }

        public static void showForm(IntPtr mainFormHandle, bool fromToolbar, int tabIndex)
        {
            if (mSettingsForm.IsDisposed)
                mSettingsForm = new FormSettings();
            mSettingsForm.mMainFormHandle = mainFormHandle;
            mSettingsForm.isFromToolbar = fromToolbar;
            mSettingsForm.tabIndex = tabIndex;
            mSettingsForm.Show();
            mSettingsForm.Focus();
        }

        private void MiddleModule_eventSend(object sender, object msg)
        {
            if (Convert.ToBoolean(msg))
                return;
            this.Invoke((Delegate) new Void(((Form) this).Close));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            MiddleModule.eventPost -= new Post(this.MiddleModule_eventSend);
            this.Dispose();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            if (this.tabIndex == 1)
                this.btnShortcuts.PerformClick();
            else
                this.btnCommon.PerformClick();
            this.btnBrightness.Visible = false;
            if (this.isFromToolbar)
                this.CenterToScreen();
            else
                this.CenterToParent();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != 512)
                return;
            TimerSession.userOperation();
        }

        private void tabBtnOnClick(object sender, EventArgs e)
        {
            HuionClickableButton huionClickableButton = (HuionClickableButton) sender;
            this.btnCommon.Clickable = true;
            this.btnShortcuts.Clickable = true;
            this.btnAbout.Clickable = true;
            this.btnBrightness.Clickable = true;
            huionClickableButton.Clickable = false;
            this.panelTab.Invalidate();
            Form form = (Form) null;
            if (huionClickableButton == this.btnCommon)
                form = (Form) new FormCommonSettings();
            else if (huionClickableButton == this.btnShortcuts)
                form = (Form) new FormShortcut(this.mMainFormHandle);
            else if (huionClickableButton == this.btnAbout)
                form = (Form) new FormSettingAbout();
            else if (huionClickableButton == this.btnBrightness)
                form = (Form) new FormSetingBrightness();
            if (form == null)
                return;
            foreach (Form control in (ArrangedElementCollection) this.panel2.Controls)
            {
                control.Close();
                control.Dispose();
            }

            this.panel2.Controls.Clear();
            form.TopLevel = false;
            form.Width = this.panel2.Width;
            this.panel2.Controls.Add((Control) form);
            this.panel2.AutoSize = true;
            form.Show();
        }

        private void tabBtnOnPaint(object sender, PaintEventArgs e)
        {
            HuionClickableButton huionClickableButton = (HuionClickableButton) sender;
            if (!huionClickableButton.Clickable)
                huionClickableButton.ForeColor = HuionConst.HuionBlue4;
            else
                huionClickableButton.ForeColor = Color.Black;
        }

        private void panelTab_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel) sender;
            Graphics graphics = panel.CreateGraphics();
            Pen pen = new Pen(HuionConst.HuionGray);
            graphics.DrawLine(pen, panel.Width - 1, 0, panel.Width - 1, panel.Height);
            pen.Dispose();
            graphics.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormSettings));
            this.panelContent = new Panel();
            this.panel2 = new Panel();
            this.panelTab = new Panel();
            this.btnBrightness = new HuionClickableButton();
            this.panelMain = new Panel();
            this.pictureBox1 = new PictureBox();
            this.panel1 = new Panel();
            this.btnAbout = new HuionClickableButton();
            this.btnShortcuts = new HuionClickableButton();
            this.btnCommon = new HuionClickableButton();
            this.panelContent.SuspendLayout();
            this.panelTab.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.SuspendLayout();
            this.panelContent.BackColor = SystemColors.Control;
            this.panelContent.Controls.Add((Control) this.panel2);
            this.panelContent.Controls.Add((Control) this.panelTab);
            this.panelContent.Location = new Point(1, 35);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new Size(548, 428);
            this.panelContent.TabIndex = 2;
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(133, 0);
            this.panel2.Margin = new Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(415, 169);
            this.panel2.TabIndex = 2;
            this.panelTab.Controls.Add((Control) this.btnBrightness);
            this.panelTab.Controls.Add((Control) this.panelMain);
            this.panelTab.Controls.Add((Control) this.panel1);
            this.panelTab.Controls.Add((Control) this.btnAbout);
            this.panelTab.Controls.Add((Control) this.btnShortcuts);
            this.panelTab.Controls.Add((Control) this.btnCommon);
            this.panelTab.Dock = DockStyle.Left;
            this.panelTab.Location = new Point(0, 0);
            this.panelTab.Margin = new Padding(3, 4, 3, 4);
            this.panelTab.Name = "panelTab";
            this.panelTab.Padding = new Padding(0, 0, 1, 0);
            this.panelTab.Size = new Size(133, 428);
            this.panelTab.TabIndex = 0;
            this.panelTab.Paint += new PaintEventHandler(this.panelTab_Paint);
            this.btnBrightness.AutoEllipsis = true;
            this.btnBrightness.BackColor = Color.Transparent;
            this.btnBrightness.BackgroundImageLayout = ImageLayout.Stretch;
            this.btnBrightness.Clickable = true;
            this.btnBrightness.Dock = DockStyle.Top;
            this.btnBrightness.FlatAppearance.BorderSize = 0;
            this.btnBrightness.FlatStyle = FlatStyle.Flat;
            this.btnBrightness.ForeColor = SystemColors.InactiveCaptionText;
            this.btnBrightness.Location = new Point(0, 169);
            this.btnBrightness.Margin = new Padding(3, 4, 3, 4);
            this.btnBrightness.Name = "btnBrightness";
            this.btnBrightness.Size = new Size(132, 56);
            this.btnBrightness.TabIndex = 4;
            this.btnBrightness.Text = "OSD";
            this.btnBrightness.TextAlign = ContentAlignment.MiddleLeft;
            this.btnBrightness.UseVisualStyleBackColor = false;
            this.btnBrightness.Click += new EventHandler(this.tabBtnOnClick);
            this.btnBrightness.Paint += new PaintEventHandler(this.tabBtnOnPaint);
            this.panelMain.BackColor = SystemColors.Control;
            this.panelMain.BackgroundImage = (Image) componentResourceManager.GetObject("panelMain.BackgroundImage");
            this.panelMain.BackgroundImageLayout = ImageLayout.Stretch;
            this.panelMain.Controls.Add((Control) this.pictureBox1);
            this.panelMain.Location = new Point(133, 26);
            this.panelMain.Margin = new Padding(3, 4, 3, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(415, 448);
            this.panelMain.TabIndex = 1;
            this.pictureBox1.BackgroundImage =
                (Image) componentResourceManager.GetObject("pictureBox1.BackgroundImage");
            this.pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            this.pictureBox1.Location = new Point(146, 474);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(268, 10);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.panel1.Location = new Point(0, 227);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(132, 201);
            this.panel1.TabIndex = 3;
            this.btnAbout.AutoEllipsis = true;
            this.btnAbout.BackColor = Color.Transparent;
            this.btnAbout.Clickable = true;
            this.btnAbout.Dock = DockStyle.Top;
            this.btnAbout.FlatAppearance.BorderColor = SystemColors.Control;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = FlatStyle.Flat;
            this.btnAbout.ForeColor = SystemColors.InfoText;
            this.btnAbout.Location = new Point(0, 112);
            this.btnAbout.Margin = new Padding(3, 4, 3, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new Size(132, 57);
            this.btnAbout.TabIndex = 2;
            this.btnAbout.Text = "关于";
            this.btnAbout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new EventHandler(this.tabBtnOnClick);
            this.btnAbout.Paint += new PaintEventHandler(this.tabBtnOnPaint);
            this.btnShortcuts.AutoEllipsis = true;
            this.btnShortcuts.BackColor = Color.Transparent;
            this.btnShortcuts.BackgroundImageLayout = ImageLayout.Stretch;
            this.btnShortcuts.Clickable = true;
            this.btnShortcuts.Dock = DockStyle.Top;
            this.btnShortcuts.FlatAppearance.BorderSize = 0;
            this.btnShortcuts.FlatStyle = FlatStyle.Flat;
            this.btnShortcuts.ForeColor = SystemColors.Window;
            this.btnShortcuts.Location = new Point(0, 56);
            this.btnShortcuts.Margin = new Padding(3, 4, 3, 4);
            this.btnShortcuts.Name = "btnShortcuts";
            this.btnShortcuts.Size = new Size(132, 56);
            this.btnShortcuts.TabIndex = 1;
            this.btnShortcuts.Text = "快捷键";
            this.btnShortcuts.TextAlign = ContentAlignment.MiddleLeft;
            this.btnShortcuts.UseVisualStyleBackColor = false;
            this.btnShortcuts.Click += new EventHandler(this.tabBtnOnClick);
            this.btnShortcuts.Paint += new PaintEventHandler(this.tabBtnOnPaint);
            this.btnCommon.AutoEllipsis = true;
            this.btnCommon.BackColor = Color.Transparent;
            this.btnCommon.Clickable = true;
            this.btnCommon.Cursor = Cursors.Default;
            this.btnCommon.DialogResult = DialogResult.Cancel;
            this.btnCommon.Dock = DockStyle.Top;
            this.btnCommon.FlatAppearance.BorderSize = 0;
            this.btnCommon.FlatStyle = FlatStyle.Flat;
            this.btnCommon.ForeColor = SystemColors.Window;
            this.btnCommon.Location = new Point(0, 0);
            this.btnCommon.Margin = new Padding(3, 4, 3, 4);
            this.btnCommon.Name = "btnCommon";
            this.btnCommon.Size = new Size(132, 56);
            this.btnCommon.TabIndex = 0;
            this.btnCommon.Text = "通用";
            this.btnCommon.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCommon.UseVisualStyleBackColor = false;
            this.btnCommon.Click += new EventHandler(this.tabBtnOnClick);
            this.btnCommon.Paint += new PaintEventHandler(this.tabBtnOnPaint);
            this.AutoScaleDimensions = new SizeF(96f, 96f);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = SystemColors.Control;
            this.ClientSize = new Size(550, 457);
            this.Controls.Add((Control) this.panelContent);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.Margin = new Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = nameof(FormSettings);
            this.ShowSettingsIcon = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = nameof(FormSettings);
            this.Load += new EventHandler(this.FormSettings_Load);
            this.panelContent.ResumeLayout(false);
            this.panelTab.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.ResumeLayout(false);
        }
    }
}