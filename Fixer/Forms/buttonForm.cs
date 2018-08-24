// Decompiled with JetBrains decompiler
// Type: HuionTablet.buttonForm
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
    public class buttonForm : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components;
        private Label label1;
        private Label label2;

        public buttonForm()
        {
            this.InitializeComponent();
            this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
            this.Text = Fixer4Main.FormTitle();
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        public static void closeform(object sender, EventArgs e)
        {
            ((Control) sender).FindForm().Close();
        }

        public static DialogResult importConfig()
        {
            buttonForm buttonForm = new buttonForm();
            buttonForm.label1.Text = ResourceCulture.GetString("FormInfo_remindText");
            buttonForm.label2.Text = ResourceCulture.GetString("FormInfo_RemindImportMessageText");
            buttonForm.button1.Text = ResourceCulture.GetString("FormEkey_btOKText");
            buttonForm.button2.Text = ResourceCulture.GetString("FormEkey_btCannelText");
            return buttonForm.ShowDialog();
        }

        public static DialogResult defaultConfig()
        {
            buttonForm buttonForm = new buttonForm();
            buttonForm.label1.Text = ResourceCulture.GetString("FormInfo_remindText");
            buttonForm.label2.Text = ResourceCulture.GetString("FormInfo_RemindMessageText");
            buttonForm.button1.Text = ResourceCulture.GetString("FormEkey_btOKText");
            buttonForm.button2.Text = ResourceCulture.GetString("FormEkey_btCannelText");
            return buttonForm.ShowDialog();
        }

        private void button()
        {
            if (new buttonForm().ShowDialog((IWin32Window) this) == DialogResult.Yes)
                Fixer4Info.importConfigClick((object) this, (EventArgs) null);
        }

        private void buttonForm_Load(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.SuspendLayout();
            this.button1.Location = new Point(154, 125);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(265, 125);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(21, 29);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(70, 71);
            this.label2.MaximumSize = new Size(250, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(379, 168);
            this.Controls.Add((Control) this.label2);
            this.Controls.Add((Control) this.label1);
            this.Controls.Add((Control) this.button2);
            this.Controls.Add((Control) this.button1);
            this.Name = nameof(buttonForm);
            this.Text = nameof(buttonForm);
            this.Load += new EventHandler(this.buttonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}