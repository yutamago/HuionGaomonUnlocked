// Decompiled with JetBrains decompiler
// Type: HuionTablet.FormInsatll
// Assembly: Fixer, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: F573D0D8-B2B9-493C-AB71-EC374499E1DC
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet
{
    public class FormInsatll : Form
    {
        private static FormInsatll formInsatll = new FormInsatll();
        private Button button1;
        private IContainer components;
        private Label label1;

        public FormInsatll()
        {
            this.InitializeComponent();
            this.Icon = ImageHelper.getDllIcon("32.ico", HNStruct.OemType);
            this.Text = Fixer4Main.FormTitle();
            this.CenterToScreen();
        }

        public static void showForm()
        {
            showForm((string) null);
        }

        public static void showForm(string url)
        {
            if (formInsatll.IsDisposed)
                formInsatll = new FormInsatll();
            formInsatll.Show();
            formInsatll.Focus();
        }

        private void FormInsatll_Load(object sender, EventArgs e)
        {
            this.label1.Text = ResourceCulture.GetString("downloaded");
            this.button1.Text = ResourceCulture.GetString("openFile");
        }

        public static void openFile(string file)
        {
            Process.Start(new ProcessStartInfo("Explorer.exe")
            {
                Arguments = "/e,/select," + file
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFile("D:\\HUION_v14.3.1.180514_8192.7z");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.button1 = new Button();
            this.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(48, 45);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 0;
            this.button1.Location = new Point(212, 67);
            this.button1.Name = "button1";
            this.button1.Size = new Size(111, 29);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(352, 121);
            this.Controls.Add((Control) this.button1);
            this.Controls.Add((Control) this.label1);
            this.Name = nameof(FormInsatll);
            this.Text = nameof(FormInsatll);
            this.Load += new EventHandler(this.FormInsatll_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}