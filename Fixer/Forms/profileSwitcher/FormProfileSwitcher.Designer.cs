namespace HuionTablet.Forms.profileSwitcher
{
    partial class FormProfileSwitcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("[All]", "(Keine)");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Osu!", "Osu!Logo_(2015).png");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Photoshop", "615px-Photoshop_CC_icon.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProfileSwitcher));
            this.applicationsListView = new System.Windows.Forms.ListView();
            this.appIcons16 = new System.Windows.Forms.ImageList(this.components);
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.fileProcessBtn = new System.Windows.Forms.Button();
            this.runningProcessBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.processNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.enableAppCheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.profileComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.applicationsRemoveBtn = new System.Windows.Forms.Button();
            this.applicationsAddBtn = new System.Windows.Forms.Button();
            this.enableProfileSwitchingCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.settingsUtilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.settingsGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsUtilBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // applicationsListView
            // 
            this.applicationsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            this.applicationsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.applicationsListView.LargeImageList = this.appIcons16;
            this.applicationsListView.Location = new System.Drawing.Point(6, 19);
            this.applicationsListView.MultiSelect = false;
            this.applicationsListView.Name = "applicationsListView";
            this.applicationsListView.Size = new System.Drawing.Size(152, 205);
            this.applicationsListView.SmallImageList = this.appIcons16;
            this.applicationsListView.TabIndex = 2;
            this.applicationsListView.UseCompatibleStateImageBehavior = false;
            this.applicationsListView.View = System.Windows.Forms.View.SmallIcon;
            this.applicationsListView.SelectedIndexChanged += new System.EventHandler(this.ApplicationsListView_SelectedIndexChanged);
            // 
            // appIcons16
            // 
            this.appIcons16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("appIcons16.ImageStream")));
            this.appIcons16.TransparentColor = System.Drawing.Color.Transparent;
            this.appIcons16.Images.SetKeyName(0, "Osu!Logo_(2015).png");
            this.appIcons16.Images.SetKeyName(1, "615px-Photoshop_CC_icon.png");
            // 
            // settingsGroup
            // 
            this.settingsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsGroup.Controls.Add(this.button3);
            this.settingsGroup.Controls.Add(this.button2);
            this.settingsGroup.Controls.Add(this.fileProcessBtn);
            this.settingsGroup.Controls.Add(this.runningProcessBtn);
            this.settingsGroup.Controls.Add(this.label3);
            this.settingsGroup.Controls.Add(this.processNameTextBox);
            this.settingsGroup.Controls.Add(this.label2);
            this.settingsGroup.Controls.Add(this.enableAppCheckbox);
            this.settingsGroup.Controls.Add(this.label1);
            this.settingsGroup.Controls.Add(this.profileComboBox);
            this.settingsGroup.Enabled = false;
            this.settingsGroup.Location = new System.Drawing.Point(182, 35);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(327, 259);
            this.settingsGroup.TabIndex = 6;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "App Settings";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(208, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(91, 163);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Apply";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // fileProcessBtn
            // 
            this.fileProcessBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileProcessBtn.Location = new System.Drawing.Point(91, 74);
            this.fileProcessBtn.Name = "fileProcessBtn";
            this.fileProcessBtn.Size = new System.Drawing.Size(230, 23);
            this.fileProcessBtn.TabIndex = 10;
            this.fileProcessBtn.Text = "Select process file";
            this.fileProcessBtn.UseVisualStyleBackColor = true;
            // 
            // runningProcessBtn
            // 
            this.runningProcessBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runningProcessBtn.Location = new System.Drawing.Point(91, 45);
            this.runningProcessBtn.Name = "runningProcessBtn";
            this.runningProcessBtn.Size = new System.Drawing.Size(230, 23);
            this.runningProcessBtn.TabIndex = 9;
            this.runningProcessBtn.Text = "Select running process";
            this.runningProcessBtn.UseVisualStyleBackColor = true;
            this.runningProcessBtn.Click += new System.EventHandler(this.runningProcessBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Enable:";
            // 
            // processNameTextBox
            // 
            this.processNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processNameTextBox.Location = new System.Drawing.Point(91, 19);
            this.processNameTextBox.Name = "processNameTextBox";
            this.processNameTextBox.Size = new System.Drawing.Size(230, 20);
            this.processNameTextBox.TabIndex = 4;
            this.processNameTextBox.TextChanged += new System.EventHandler(this.ProcessNameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Process Name:";
            // 
            // enableAppCheckbox
            // 
            this.enableAppCheckbox.AutoSize = true;
            this.enableAppCheckbox.Location = new System.Drawing.Point(91, 133);
            this.enableAppCheckbox.Name = "enableAppCheckbox";
            this.enableAppCheckbox.Size = new System.Drawing.Size(15, 14);
            this.enableAppCheckbox.TabIndex = 2;
            this.enableAppCheckbox.UseVisualStyleBackColor = true;
            this.enableAppCheckbox.CheckedChanged += new System.EventHandler(this.EnableAppCheckbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Profile:";
            // 
            // profileComboBox
            // 
            this.profileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileComboBox.Location = new System.Drawing.Point(91, 103);
            this.profileComboBox.Name = "profileComboBox";
            this.profileComboBox.Size = new System.Drawing.Size(230, 21);
            this.profileComboBox.TabIndex = 0;
            this.profileComboBox.SelectedIndexChanged += new System.EventHandler(this.ProfileComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.applicationsRemoveBtn);
            this.groupBox2.Controls.Add(this.applicationsAddBtn);
            this.groupBox2.Controls.Add(this.applicationsListView);
            this.groupBox2.Location = new System.Drawing.Point(12, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 259);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Apps";
            // 
            // applicationsRemoveBtn
            // 
            this.applicationsRemoveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applicationsRemoveBtn.Location = new System.Drawing.Point(83, 230);
            this.applicationsRemoveBtn.Name = "applicationsRemoveBtn";
            this.applicationsRemoveBtn.Size = new System.Drawing.Size(75, 23);
            this.applicationsRemoveBtn.TabIndex = 4;
            this.applicationsRemoveBtn.Text = "Remove";
            this.applicationsRemoveBtn.UseVisualStyleBackColor = true;
            this.applicationsRemoveBtn.Click += new System.EventHandler(this.ApplicationsRemoveBtn_Click);
            // 
            // applicationsAddBtn
            // 
            this.applicationsAddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applicationsAddBtn.Location = new System.Drawing.Point(6, 230);
            this.applicationsAddBtn.Name = "applicationsAddBtn";
            this.applicationsAddBtn.Size = new System.Drawing.Size(75, 23);
            this.applicationsAddBtn.TabIndex = 3;
            this.applicationsAddBtn.Text = "Add";
            this.applicationsAddBtn.UseVisualStyleBackColor = true;
            this.applicationsAddBtn.Click += new System.EventHandler(this.ApplicationsAddBtn_Click);
            // 
            // enableProfileSwitchingCheckBox
            // 
            this.enableProfileSwitchingCheckBox.AutoSize = true;
            this.enableProfileSwitchingCheckBox.Location = new System.Drawing.Point(18, 12);
            this.enableProfileSwitchingCheckBox.Name = "enableProfileSwitchingCheckBox";
            this.enableProfileSwitchingCheckBox.Size = new System.Drawing.Size(190, 17);
            this.enableProfileSwitchingCheckBox.TabIndex = 8;
            this.enableProfileSwitchingCheckBox.Text = "Enable Automatic Profile Switching";
            this.enableProfileSwitchingCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(409, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Save Settings.xml";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveSettings);
            // 
            // settingsUtilBindingSource
            // 
            this.settingsUtilBindingSource.DataSource = typeof(HuionTablet.SettingsUtil);
            // 
            // FormProfileSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 335);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.enableProfileSwitchingCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.settingsGroup);
            this.Name = "FormProfileSwitcher";
            this.Text = "FormProfileSwitcher";
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingsUtilBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView applicationsListView;
        private System.Windows.Forms.ImageList appIcons16;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox enableProfileSwitchingCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox profileComboBox;
        private System.Windows.Forms.CheckBox enableAppCheckbox;
        private System.Windows.Forms.Button fileProcessBtn;
        private System.Windows.Forms.Button runningProcessBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox processNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button applicationsRemoveBtn;
        private System.Windows.Forms.Button applicationsAddBtn;
        private System.Windows.Forms.BindingSource settingsUtilBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}