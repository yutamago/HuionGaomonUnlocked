// Decompiled with JetBrains decompiler
// Type: Huion.HuionTextView
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Huion
{
    public class HuionTextView : TextBox
    {
        public delegate bool IsRegisterCallback(object sender, HuionKeyEventArgs keyEvent);

        public delegate string KeyChangedListener(HuionKeyEventArgs keyEvent);

        public const int KeyEventMaxCount = 16;
        private bool isAltChecked;
        private bool isControlChecked;
        public IsRegisterCallback isRegisterCallback;
        private bool isShiftChecked;
        private bool isSingleKeys;
        private bool isWinChecked;
        public KeyChangedListener mKeyChangedListener;
        private List<HuionKeyEventArgs> mKeyEvents;

        public HuionTextView()
        {
            this.mKeyEvents = new List<HuionKeyEventArgs>(16);
            this.ImeMode = ImeMode.Disable;
        }

        public List<HuionKeyEventArgs> KeyEvents
        {
            get { return this.mKeyEvents; }
        }

        public bool IsControlChecked
        {
            get { return this.isControlChecked; }
            set
            {
                this.isControlChecked = value;
                this.updateText();
            }
        }

        public bool IsAltChecked
        {
            get { return this.isAltChecked; }
            set
            {
                this.isAltChecked = value;
                this.updateText();
            }
        }

        public bool IsShiftChecked
        {
            get { return this.isShiftChecked; }
            set
            {
                this.isShiftChecked = value;
                this.updateText();
            }
        }

        public bool IsWinChecked
        {
            get { return this.isWinChecked; }
            set
            {
                this.isWinChecked = value;
                this.updateText();
            }
        }

        public bool IsSingleKeys
        {
            get { return this.isSingleKeys; }
            set { this.isSingleKeys = value; }
        }

        public void addKeyEvent(HuionKeyEventArgs keyEvent)
        {
            this.onKeyDown((object) null, keyEvent);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (this.FindForm() == null)
                return;
            this.FindForm().InputLanguageChanged +=
                new InputLanguageChangedEventHandler(this.HuionTextView_InputLanguageChanged);
        }

        private void HuionTextView_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
        }

        private void onKeyDown(object sender, HuionKeyEventArgs e)
        {
            if (KeyCodeUtils.isLegalKey(e.KeyCode))
            {
                e.Handled = true;
                if (KeyCodeUtils.isControls(e.KeyCode))
                    return;
                if (this.isRegisterCallback != null)
                {
                    int num = this.isRegisterCallback((object) this, e) ? 1 : 0;
                }

                e.KeyText = this.mKeyChangedListener == null ? e.KeyCode.ToString() : this.mKeyChangedListener(e);
                this.addEvent2List(e);
                this.updateText();
            }
            else
                e.Handled = false;
        }

        private void onKeyUp(object sender, HuionKeyEventArgs e)
        {
        }

        private void updateText()
        {
            this.Text = "";
            if (this.isSingleKeys)
            {
                foreach (HuionKeyEventArgs mKeyEvent in this.mKeyEvents)
                {
                    if (mKeyEvent.Control)
                        this.Text += "Ctrl + ";
                    if (mKeyEvent.Alt)
                        this.Text += "Alt + ";
                    if (mKeyEvent.Shift)
                        this.Text += "Shift + ";
                    if (mKeyEvent.Window)
                        this.Text += "Win + ";
                    this.Text += mKeyEvent.KeyText;
                }
            }
            else
            {
                if (this.IsControlChecked)
                    this.Text += "Ctrl + ";
                if (this.IsAltChecked)
                    this.Text += "Alt + ";
                if (this.IsShiftChecked)
                    this.Text += "Shift + ";
                if (this.IsWinChecked)
                    this.Text += "Win + ";
                foreach (HuionKeyEventArgs mKeyEvent in this.mKeyEvents)
                {
                    this.Text += mKeyEvent.KeyText;
                    this.Text += " ";
                }
            }

            this.Select(this.Text.Length, 0);
        }

        public void clearKeyEvents()
        {
            this.mKeyEvents.Clear();
            this.updateText();
        }

        private bool addEvent2List(HuionKeyEventArgs keyEvent)
        {
            if (this.isSingleKeys)
            {
                this.mKeyEvents.Clear();
                this.mKeyEvents.Add(keyEvent);
            }
            else if (this.mKeyEvents.Count < 16)
            {
                this.mKeyEvents.Add(keyEvent);
                return true;
            }

            return false;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            KeyBoardHook.HookAll(new HuionKeyEventHandler(this.onKeyDown), new HuionKeyEventHandler(this.onKeyUp));
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            KeyBoardHook.StopHook();
        }
    }
}