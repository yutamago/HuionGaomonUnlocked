// Decompiled with JetBrains decompiler
// Type: HuionTablet.BaseForm
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.Drawing;
using System.Windows.Forms;
using Huion;

namespace HuionTablet
{
    public class BaseForm : Form
    {
        private const int WS_CLIPCHILDREN = 33554432;
        private const int WS_MINIMIZEBOX = 131072;
        private const int WS_MAXIMIZEBOX = 65536;
        private const int WS_SYSMENU = 524288;
        private const int CS_DBLCLKS = 8;
        public const int CS_DROPSHADOW = 131072;
        public const int GCL_STYLE = -26;
        public static bool IsUpdate = false;
        public static TitleBar mTitleBar = new TitleBar();
        protected Color borderColor = Color.CornflowerBlue;

        public BaseForm()
        {
            this.Padding = new Padding(1);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mTitleBar = new TitleBar();
            mTitleBar.Height = 30;
            mTitleBar.Dock = DockStyle.Top;
            mTitleBar.BackColor = Color.White;
            mTitleBar.OnSettingsButtonClick += new EventHandler(this.onSettingsButtonClick);
            this.Controls.Add((Control) mTitleBar);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.Style |= 131072;
                return createParams;
            }
        }

        public new bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            set
            {
                base.MinimizeBox = value;
                mTitleBar.ShowMinimizeBox = value;
            }
        }

        public new FormBorderStyle FormBorderStyle
        {
            get { return FormBorderStyle.None; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        public bool ShowSettingsIcon
        {
            get { return mTitleBar.ShowSettingsIcon; }
            set { mTitleBar.ShowSettingsIcon = value; }
        }

        public new string Text
        {
            get
            {
                if (mTitleBar != null)
                    return mTitleBar.Text;
                return "";
            }
            set
            {
                base.Text = value;
                mTitleBar.Text = value;
            }
        }

        public new bool ControlBox
        {
            set { base.ControlBox = false; }
            get { return false; }
        }

        public new Icon Icon
        {
            get { return base.Icon; }
            set
            {
                base.Icon = value;
                if (value == null)
                    return;
                mTitleBar.Icon = value.ToBitmap();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            HuionDriverDLL.SetClassLong(this.Handle, -26, HuionDriverDLL.GetClassLong(this.Handle, -26) | 131072);
        }

        private static bool SettingIconChange()
        {
            try
            {
                return HuionMessageBox.compareVersion();
            }
            catch (Exception ex)
            {
                HuionLog.saveLog(nameof(SettingIconChange), ex.Message);
                return false;
            }
        }

        public static void myThread()
        {
            IsUpdate = SettingIconChange();
            if (!IsUpdate)
                return;
            mTitleBar.SettingsIcon = (Image) ImageHelper.getDllImage("settingnew--.png");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
            ControlPaint.DrawBorder(e.Graphics, bounds, this.borderColor, ButtonBorderStyle.Solid);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.borderColor = Color.CornflowerBlue;
            mTitleBar.IsActived = true;
            this.Invalidate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.borderColor = Color.LightGray;
            mTitleBar.IsActived = false;
            this.Invalidate();
        }

        public virtual void onSettingsButtonClick(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 261);
            this.Name = nameof(BaseForm);
            this.Load += new EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
        }
    }
}