// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormHotKey
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Huion;
using HuionTablet.Entity;
using HuionTablet.Lib;
using HuionTablet.utils;

namespace HuionTablet
{
    public class FormHotKey : Form, IDestroy
    {
        public static int curEkeyIndex;
        private CheckBox chkHEkey;
        private CheckBox chkMEkey;
        private CheckBox chkSEkey;
        private Button clickBtn;
        private ComboBox comboBoxMekey;
        private IContainer components;
        public HNStruct.HNEkey ek;
        private PictureBox imgTablet;
        private HuionKeyLayout[] keyLayouts;
        private HuionKeyLayout mCheckedKeyLayout;
        private HuionKeyLayout mCurrentKeyLayout;
        private int mExcludeIndex1 = -1;
        public int mTouchIndex;

        public FormHotKey(PictureBoxSizeMode imageSizeMode)
        {
            this.InitializeComponent();
            this.imgTablet.SizeMode = imageSizeMode;
            this.imgTablet.Paint += new PaintEventHandler(this.imgTablet_Paint);
        }

        public void onDestroy()
        {
        }

        private void imgTablet_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.White, 0.0f);
            SolidBrush solidBrush1 = new SolidBrush(Color.White);
            Pen pen2;
            SolidBrush solidBrush2;
            if (HNStruct.OemType == OEMType.HUION)
            {
                pen2 = new Pen(HuionConst.HuionBlue1, 1f);
                solidBrush2 = new SolidBrush(HuionConst.HuionBlue2);
            }
            else
            {
                pen2 = new Pen(HuionConst.HuionBlue3, 1f);
                solidBrush2 = new SolidBrush(HuionConst.HuionBlue4);
            }

            if (this.mCurrentKeyLayout != null)
                e.Graphics.DrawPath(pen2, HuionRender.CreateRoundedRectanglePath(this.mCurrentKeyLayout.Rect, 2));
            if (this.mCheckedKeyLayout != null)
            {
                e.Graphics.FillPath((Brush) solidBrush2,
                    HuionRender.CreateRoundedRectanglePath(this.mCheckedKeyLayout.Rect, 2));
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(pen2, Utils.getViewCenter((Control) this.clickBtn, 16),
                    Utils.getRectCenter(this.mCheckedKeyLayout.Rect));
            }

            pen2.Dispose();
        }

        private void FormHotKey_Load(object sender, EventArgs e)
        {
            this.setViewText8Locale();
            this.clickBtn.Parent = (Control) this.imgTablet;
            this.clickBtn.UseMnemonic = false;
            this.comboBoxMekey.Parent = (Control) this.imgTablet;
            this.imgTablet.Image = ImageHelper.getDllScaleImage(HNStruct.devTypeString, (Control) this.imgTablet);
            this.imgTablet.Size = this.imgTablet.Image.Size;
            this.imgTablet.Location = new Point((this.Width - this.imgTablet.Width) / 2,
                (this.Height - this.imgTablet.Height) / 2 - 20);
            Point point = new Point(this.imgTablet.Width / 2, this.imgTablet.Height / 2);
            point.Y -= this.clickBtn.Height / 2;
            point.X -= this.clickBtn.Width / 2;
            this.clickBtn.Location = point;
            this.comboBoxMekey.Location = this.clickBtn.Location;
            curEkeyIndex = 0;
            this.comboBoxMekey.Visible = false;
            this.keyLayouts = new HuionKeyLayout[(int) HNStruct.globalInfo.layoutTablet.hbtnNum +
                                                 (int) HNStruct.globalInfo.layoutTablet.sbtnNum +
                                                 (int) HNStruct.globalInfo.layoutTablet.mekeyNum];
            for (int index = 0; (long) index < (long) HNStruct.globalInfo.layoutTablet.hbtnNum; ++index)
            {
                this.keyLayouts[index] = Utils.swapLayout(this.imgTablet.Size, HNStruct.globalInfo.layoutTablet.size,
                    HNStruct.globalInfo.hbtnLayouts[index]);
                this.keyLayouts[index].KeyType = HuionKeyType.HARDKEY;
                this.keyLayouts[index].Key = HNStruct.globalInfo.hbtns[index];
                this.keyLayouts[index].KeyIndex = index;
            }

            for (int index = 0; (long) index < (long) HNStruct.globalInfo.layoutTablet.sbtnNum; ++index)
            {
                HuionKeyLayout huionKeyLayout = Utils.swapLayout(this.imgTablet.Size,
                    HNStruct.globalInfo.layoutTablet.size, HNStruct.globalInfo.sbtnLayouts[index]);
                huionKeyLayout.KeyType = HuionKeyType.SOFTKEY;
                huionKeyLayout.Key = HNStruct.globalInfo.sbtns[index];
                huionKeyLayout.KeyIndex = index;
                this.keyLayouts[(long) HNStruct.globalInfo.layoutTablet.hbtnNum + (long) index] = huionKeyLayout;
            }

            for (int index = 0; (long) index < (long) HNStruct.globalInfo.layoutTablet.mekeyNum; ++index)
            {
                HuionKeyLayout huionKeyLayout = Utils.swapLayout(this.imgTablet.Size,
                    HNStruct.globalInfo.layoutTablet.size, HNStruct.globalInfo.mekeyLayouts[index]);
                huionKeyLayout.KeyType = HuionKeyType.MULTIKEY;
                huionKeyLayout.MutliKeys = HNStruct.globalInfo.mbtns;
                huionKeyLayout.KeyIndex = index;
                this.keyLayouts[
                    (long) (HNStruct.globalInfo.layoutTablet.hbtnNum + HNStruct.globalInfo.layoutTablet.sbtnNum) +
                    (long) index] = huionKeyLayout;
            }

            if (this.keyLayouts.Length != 0)
                this.mCheckedKeyLayout = this.keyLayouts[0];
            if (HNStruct.globalInfo.layoutTablet.hbtnNum > 0U)
                this.clickBtn.Text = HNStruct.globalInfo.hbtns[0].ToString();
            else
                this.clickBtn.Visible = false;
            this.SetTouchSinger(this.mTouchIndex);
            this.UpdateHnConfigToView();
        }

        private void setViewText8Locale()
        {
            this.chkHEkey.Text = ResourceCulture.GetString("FormHotKey_btnEnablePressKeysText");
            this.chkSEkey.Text = ResourceCulture.GetString("FormHotKey_btnEnableSoftKeysText");
            this.chkMEkey.Text = ResourceCulture.GetString("FormHotKey_btnEnableTouchText");
        }

        private void SetTouchSinger(int selectIndex)
        {
            this.comboBoxMekey.Items.Clear();
            if (HNStruct.globalInfo.layoutTablet.mekeyNum > 0U)
            {
                for (int index = 0; (long) index < (long) HNStruct.globalInfo.mekeyNames[0].num; ++index)
                {
                    string name = HNStruct.globalInfo.names[index];
                    string str = HNStruct.globalInfo.mbtns[index].ToString();
                    if ("双指顺时针旋转".Equals(name))
                        this.mExcludeIndex1 = index;
                    else if (!"双指逆时针旋转".Equals(name))
                        this.comboBoxMekey.Items.Add((object) (name + "    " + str));
                }

                if (HNStruct.globalInfo.names.Length == 0)
                    return;
                this.comboBoxMekey.Text = this.comboBoxMekey.Items[selectIndex].ToString();
            }
            else
                this.comboBoxMekey.Visible = false;
        }

        private void UpdateHnConfigToView()
        {
            bool boolean1 = Convert.ToBoolean(HNStruct.globalInfo.tabletInfo.hbtnNum);
            bool boolean2 = Convert.ToBoolean(HNStruct.globalInfo.tabletInfo.sbtnNum);
            bool boolean3 = Convert.ToBoolean(HNStruct.globalInfo.userConfig.mekeyNum);
            this.chkHEkey.Enabled = boolean1;
            this.chkSEkey.Enabled = boolean2;
            this.chkMEkey.Enabled = boolean3;
            bool boolean4 = Convert.ToBoolean(HNStruct.globalInfo.userConfig.bEnableHBtn);
            bool boolean5 = Convert.ToBoolean(HNStruct.globalInfo.userConfig.bEnableSBtn);
            bool boolean6 = Convert.ToBoolean(HNStruct.globalInfo.userConfig.bEnableMBtn);
            this.chkHEkey.Checked = boolean4;
            this.chkSEkey.Checked = boolean5;
            this.chkMEkey.Checked = boolean6;
        }

        private void imgTablet_MouseMove(object sender, MouseEventArgs e)
        {
            TimerSession.userOperation();
            if (this.mCurrentKeyLayout != null && this.mCurrentKeyLayout.mouseEnter(e.X, e.Y))
            {
                this.imgTablet.Cursor = Cursors.Hand;
            }
            else
            {
                if (this.keyLayouts != null)
                {
                    foreach (HuionKeyLayout keyLayout in this.keyLayouts)
                    {
                        if (keyLayout.mouseEnter(e.X, e.Y))
                        {
                            this.mCurrentKeyLayout = keyLayout;
                            this.imgTablet.Refresh();
                            this.imgTablet.Cursor = Cursors.Hand;
                            return;
                        }
                    }
                }

                this.imgTablet.Cursor = Cursors.Default;
            }
        }

        private void imgTablet_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.keyLayouts != null)
            {
                foreach (HuionKeyLayout keyLayout in this.keyLayouts)
                {
                    if (keyLayout.mouseEnter(e.X, e.Y))
                    {
                        this.mCheckedKeyLayout = keyLayout;
                        this.imgTablet.Refresh();
                        if (this.mCheckedKeyLayout.KeyType == HuionKeyType.MULTIKEY)
                        {
                            this.comboBoxMekey.Visible = true;
                            this.comboBoxMekey.Text = this.mCheckedKeyLayout.MutliKeys[this.mTouchIndex].ToString();
                            this.clickBtn.Visible = false;
                            break;
                        }

                        this.comboBoxMekey.Visible = false;
                        this.clickBtn.Visible = true;
                        this.clickBtn.Text = this.mCheckedKeyLayout.Key.ToString();
                        break;
                    }
                }
            }

            TimerSession.userOperation();
        }

        private void chkHEkey_CheckedChanged(object sender, EventArgs e)
        {
            int num = (int) Convert.ToByte(this.chkHEkey.Checked);
            HNStruct.globalInfo.userConfig.bEnableHBtn = Convert.ToByte(this.chkHEkey.Checked);
        }

        private void chkSEkey_CheckedChanged(object sender, EventArgs e)
        {
            int num = (int) Convert.ToByte(this.chkSEkey.Checked);
            HNStruct.globalInfo.userConfig.bEnableSBtn = Convert.ToByte(this.chkSEkey.Checked);
        }

        private void chkMEkey_CheckedChanged(object sender, EventArgs e)
        {
            int num = (int) Convert.ToByte(this.chkMEkey.Checked);
            HNStruct.globalInfo.userConfig.bEnableMBtn = Convert.ToByte(this.chkMEkey.Checked);
        }

        private void clickBtn_Click(object sender, EventArgs e)
        {
            if (DeployConfig.getOemType() == OEMType.HUION && DeployConfig.isNewUI)
            {
                FormEKey_New formEkeyNew =
                    new FormEKey_New((object) this.mCheckedKeyLayout, this.mCheckedKeyLayout.Key);
                formEkeyNew.callback += new EKeyCallback(this.form_TransfEvent);
                int num = (int) formEkeyNew.ShowDialog();
            }
            else
            {
                FormEKey formEkey = new FormEKey((object) this.mCheckedKeyLayout, this.mCheckedKeyLayout.Key);
                formEkey.callback += new EKeyCallback(this.form_TransfEvent);
                int num = (int) formEkey.ShowDialog();
            }
        }

        private void form_TransfEvent(object holder, HNStruct.HNEkey value)
        {
            HuionKeyLayout huionKeyLayout = (HuionKeyLayout) holder;
            if (huionKeyLayout.KeyType == HuionKeyType.HARDKEY)
            {
                this.clickBtn.Text = value.ToString();
                IntPtr ptr = HNStruct.globalInfo.userConfig.hbtns;
                huionKeyLayout.Key = value;
                HNStruct.globalInfo.hbtns[huionKeyLayout.KeyIndex] = value;
                switch (IntPtr.Size)
                {
                    case 4:
                        ptr = new IntPtr(ptr.ToInt32() +
                                         huionKeyLayout.KeyIndex * Marshal.SizeOf(typeof(HNStruct.HNEkey)));
                        break;
                    case 8:
                        ptr = new IntPtr(ptr.ToInt64() +
                                         (long) (huionKeyLayout.KeyIndex * Marshal.SizeOf(typeof(HNStruct.HNEkey))));
                        break;
                }

                Marshal.StructureToPtr((object) HNStruct.globalInfo.hbtns[huionKeyLayout.KeyIndex], ptr, false);
            }
            else if (huionKeyLayout.KeyType == HuionKeyType.SOFTKEY)
            {
                this.clickBtn.Text = value.ToString();
                IntPtr ptr = HNStruct.globalInfo.userConfig.sbtns;
                huionKeyLayout.Key = value;
                HNStruct.globalInfo.sbtns[huionKeyLayout.KeyIndex] = value;
                switch (IntPtr.Size)
                {
                    case 4:
                        ptr = new IntPtr(ptr.ToInt32() +
                                         huionKeyLayout.KeyIndex * Marshal.SizeOf(typeof(HNStruct.HNEkey)));
                        break;
                    case 8:
                        ptr = new IntPtr(ptr.ToInt64() +
                                         (long) (huionKeyLayout.KeyIndex * Marshal.SizeOf(typeof(HNStruct.HNEkey))));
                        break;
                }

                Marshal.StructureToPtr((object) HNStruct.globalInfo.sbtns[huionKeyLayout.KeyIndex], ptr, false);
            }
            else
            {
                if (huionKeyLayout.KeyType != HuionKeyType.MULTIKEY)
                    return;
                IntPtr ptr = HNStruct.globalInfo.meKeys[huionKeyLayout.KeyIndex].ekeys;
                int meIndex = this.getMeIndex();
                huionKeyLayout.MutliKeys[meIndex] = value;
                HNStruct.globalInfo.mbtns[meIndex] = value;
                this.SetTouchSinger(this.mTouchIndex);
                switch (IntPtr.Size)
                {
                    case 4:
                        ptr = new IntPtr(ptr.ToInt32() + meIndex * Marshal.SizeOf(typeof(HNStruct.HNEkey)));
                        break;
                    case 8:
                        ptr = new IntPtr(ptr.ToInt64() + (long) (meIndex * Marshal.SizeOf(typeof(HNStruct.HNEkey))));
                        break;
                }

                Marshal.StructureToPtr((object) HNStruct.globalInfo.mbtns[meIndex], ptr, false);
            }
        }

        private void comboBoxMekey_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mTouchIndex = this.comboBoxMekey.SelectedIndex;
            if (this.keyLayouts == null)
                return;
            HuionKeyLayout keyLayout = this.keyLayouts[this.keyLayouts.Length - 1];
            if (DeployConfig.getOemType() == OEMType.HUION && DeployConfig.isNewUI)
            {
                FormEKey_New formEkeyNew = new FormEKey_New((object) keyLayout, keyLayout.MutliKeys[this.getMeIndex()]);
                formEkeyNew.callback += new EKeyCallback(this.form_TransfEvent);
                int num = (int) formEkeyNew.ShowDialog();
            }
            else
            {
                FormEKey formEkey = new FormEKey((object) keyLayout, keyLayout.MutliKeys[this.getMeIndex()]);
                formEkey.callback += new EKeyCallback(this.form_TransfEvent);
                int num = (int) formEkey.ShowDialog();
            }
        }

        private int getMeIndex()
        {
            return this.mExcludeIndex1 == -1 || this.comboBoxMekey.SelectedIndex < this.mExcludeIndex1
                ? this.comboBoxMekey.SelectedIndex
                : this.comboBoxMekey.SelectedIndex + 2;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != 512)
                return;
            TimerSession.userOperation();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.imgTablet = new PictureBox();
            this.chkHEkey = new CheckBox();
            this.chkSEkey = new CheckBox();
            this.chkMEkey = new CheckBox();
            this.comboBoxMekey = new ComboBox();
            this.clickBtn = new Button();
            ((ISupportInitialize) this.imgTablet).BeginInit();
            this.SuspendLayout();
            this.imgTablet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.imgTablet.BackColor = Color.Transparent;
            this.imgTablet.Location = new Point(20, 5);
            this.imgTablet.Margin = new Padding(2, 3, 2, 3);
            this.imgTablet.Name = "imgTablet";
            this.imgTablet.Size = new Size(789, 518);
            this.imgTablet.SizeMode = PictureBoxSizeMode.AutoSize;
            this.imgTablet.TabIndex = 0;
            this.imgTablet.TabStop = false;
            this.imgTablet.Paint += new PaintEventHandler(this.imgTablet_Paint);
            this.imgTablet.MouseDown += new MouseEventHandler(this.imgTablet_MouseDown);
            this.imgTablet.MouseMove += new MouseEventHandler(this.imgTablet_MouseMove);
            this.chkHEkey.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.chkHEkey.AutoSize = true;
            this.chkHEkey.Location = new Point(34, 531);
            this.chkHEkey.Margin = new Padding(2, 3, 2, 3);
            this.chkHEkey.Name = "chkHEkey";
            this.chkHEkey.Size = new Size(99, 21);
            this.chkHEkey.TabIndex = 1;
            this.chkHEkey.Text = "启用硬快捷键";
            this.chkHEkey.UseVisualStyleBackColor = true;
            this.chkHEkey.CheckedChanged += new EventHandler(this.chkHEkey_CheckedChanged);
            this.chkSEkey.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.chkSEkey.AutoSize = true;
            this.chkSEkey.Location = new Point(262, 531);
            this.chkSEkey.Margin = new Padding(2, 3, 2, 3);
            this.chkSEkey.Name = "chkSEkey";
            this.chkSEkey.Size = new Size(99, 21);
            this.chkSEkey.TabIndex = 2;
            this.chkSEkey.Text = "启用软快捷键";
            this.chkSEkey.UseVisualStyleBackColor = true;
            this.chkSEkey.CheckedChanged += new EventHandler(this.chkSEkey_CheckedChanged);
            this.chkMEkey.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.chkMEkey.AutoSize = true;
            this.chkMEkey.Location = new Point(502, 531);
            this.chkMEkey.Margin = new Padding(2, 3, 2, 3);
            this.chkMEkey.Name = "chkMEkey";
            this.chkMEkey.Size = new Size(75, 21);
            this.chkMEkey.TabIndex = 3;
            this.chkMEkey.Text = "启用触摸";
            this.chkMEkey.UseVisualStyleBackColor = true;
            this.chkMEkey.CheckedChanged += new EventHandler(this.chkMEkey_CheckedChanged);
            this.comboBoxMekey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxMekey.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.comboBoxMekey.FormattingEnabled = true;
            this.comboBoxMekey.Location = new Point(313, 240);
            this.comboBoxMekey.Margin = new Padding(2, 3, 2, 3);
            this.comboBoxMekey.Name = "comboBoxMekey";
            this.comboBoxMekey.Size = new Size(379, 25);
            this.comboBoxMekey.TabIndex = 4;
            this.comboBoxMekey.SelectionChangeCommitted += new EventHandler(this.comboBoxMekey_SelectedIndexChanged);
            this.clickBtn.AutoEllipsis = true;
            this.clickBtn.BackColor = Color.Transparent;
            this.clickBtn.FlatAppearance.BorderColor = Color.DarkGray;
            this.clickBtn.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.clickBtn.Location = new Point(312, 240);
            this.clickBtn.Margin = new Padding(2, 3, 2, 3);
            this.clickBtn.Name = "clickBtn";
            this.clickBtn.Size = new Size(271, 25);
            this.clickBtn.TabIndex = 5;
            this.clickBtn.Text = "button1";
            this.clickBtn.UseVisualStyleBackColor = false;
            this.clickBtn.Click += new EventHandler(this.clickBtn_Click);
            this.AutoScaleDimensions = new SizeF(96f, 96f);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(840, 560);
            this.Controls.Add((Control) this.clickBtn);
            this.Controls.Add((Control) this.comboBoxMekey);
            this.Controls.Add((Control) this.chkMEkey);
            this.Controls.Add((Control) this.chkSEkey);
            this.Controls.Add((Control) this.chkHEkey);
            this.Controls.Add((Control) this.imgTablet);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Margin = new Padding(2, 3, 2, 3);
            this.Name = nameof(FormHotKey);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = nameof(FormHotKey);
            this.Load += new EventHandler(this.FormHotKey_Load);
            ((ISupportInitialize) this.imgTablet).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}