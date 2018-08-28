using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HuionTablet.Forms.profileSwitcher
{
    public delegate void SelectedAppChanged(AppUi app);

    public delegate void ProfileListAddedItem(Profile profile);

    public delegate void ProfileListRemovedItem(Profile profile);

    public delegate void AppListAddedItem(AppUi app);

    public delegate void AppListRemovedItem(AppUi app);

    public partial class FormProfileSwitcher : Form
    {
        private static readonly List<Profile> Profiles = new List<Profile>(new[] {new Profile("None", false),});
        private static readonly List<AppUi> Apps = new List<AppUi>(new[] {new AppUi("[All other]", "", false, null, Profiles[0]),});
        private AppUi _selectedApp;
        private readonly ImageList _appImages = new ImageList();

        private event SelectedAppChanged SelectedAppChanged;
        private event ProfileListAddedItem ProfileListAddedItem;
        private event ProfileListRemovedItem ProfileListRemovedItem;
        private event AppListAddedItem AppListAddedItem;
        private event AppListRemovedItem AppListRemovedItem;

        public FormProfileSwitcher()
        {
            InitializeComponent();
            SelectedAppChanged += OnSelectedAppChanged;
            ProfileListAddedItem += OnProfileListAddedItem;
            ProfileListRemovedItem += OnProfileListRemovedItem;
            AppListAddedItem += OnAppListAddedItem;
            AppListRemovedItem += OnAppListRemovedItem;

            FillProfileList();
            FillAppList();
        }

        private void FillAppList()
        {
            applicationsListView.Items.Clear();
            foreach (var app in Apps)
            {
                applicationsListView.Items.Add(app.processPath, app.name, app.processPath);
            }
        }

        private void FillProfileList()
        {
            profileComboBox.Items.Clear();
            foreach (var profile in Profiles)
            {
                profileComboBox.Items.Add(profile.Name);
            }
        }

        private void OnAppListRemovedItem(AppUi app)
        {
            _appImages.Images.RemoveByKey(app.processPath);
            applicationsListView.Items.RemoveByKey(app.processPath);
        }

        private void OnAppListAddedItem(AppUi app)
        {
            Bitmap image;
            if (File.Exists(app.processPath))
            {
                image = Icon
                    .ExtractAssociatedIcon(app.processPath)?
                    .ToBitmap();
            }
            else
            {
                image = null;
            }

            if (image != null) _appImages.Images.Add(app.processPath, image);

            ListViewItem item = new ListViewItem();
            item.Name = app.processPath;
            item.Text = app.name;
            item.ImageKey = app.processPath;
            applicationsListView.Items.Add(item);
        }

        private void OnProfileListRemovedItem(Profile profile)
        {
            profileComboBox.Items.Remove(profile.Name);
        }

        private void OnProfileListAddedItem(Profile profile)
        {
            profileComboBox.Items.Add(profile.Name);
        }

        private void OnSelectedAppChanged(AppUi app)
        {
            if (app == null)
            {
                processNameTextBox.Text = "";
                profileComboBox.SelectedIndex = 0;
                enableAppCheckbox.Checked = false;
            }
            else
            {
                processNameTextBox.Text = app.processPath;
                enableAppCheckbox.Checked = app.enabled;
                profileComboBox.SelectedIndex = profileComboBox.FindStringExact(app.profile);
            }
        }


        private void ApplicationsAddBtn_Click(object sender, EventArgs e)
        {
            AppUi newAppUi = new AppUi("New App", "", false, null, Profiles[0]);
            Apps.Add(newAppUi);
            AppListAddedItem?.Invoke(newAppUi);
        }

        private void ApplicationsRemoveBtn_Click(object sender, EventArgs e)
        {
            if (applicationsListView.SelectedIndices.Contains(0))
            {
                // Cannot remove rest
                return;
            }

            foreach (ListViewItem item in applicationsListView.SelectedItems)
            {
                AppUi remove = Apps.Find(app => app.processPath == item.Name);
                Apps.Remove(remove);
                AppListRemovedItem?.Invoke(remove);
            }
        }

        private void ApplicationsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAnythingSelected = applicationsListView.SelectedIndices.Count > 0;
            bool isRestSelected = applicationsListView.SelectedIndices.Contains(0);

            settingsGroup.Enabled = isAnythingSelected;
            processNameTextBox.Enabled = !isRestSelected;
            fileProcessBtn.Enabled = !isRestSelected;
            runningProcessBtn.Enabled = !isRestSelected;

            _selectedApp = isAnythingSelected ? Apps[applicationsListView.SelectedIndices[0]] : null;

            SelectedAppChanged?.Invoke(_selectedApp);

        }

        private void ProcessNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this._selectedApp != null)
            {
                AppUi selected = this._selectedApp;
                selected.processPath = this.processNameTextBox.Text;
            }
        }

        private void ProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._selectedApp != null)
            {
                AppUi selected = this._selectedApp;

                if (profileComboBox.SelectedIndex == 0)
                {
                    selected.profile = null;
                }
                else
                {
                    selected.profile = Profiles.Find(x => x.Name.Equals(profileComboBox.SelectedText)).Name;
                }
                
            }
        }

        private void EnableAppCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this._selectedApp != null)
            {
                AppUi selected = this._selectedApp;
                selected.enabled = this.enableAppCheckbox.Checked;
            }
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            bool enableGlobal = enableProfileSwitchingCheckBox.Checked;
            AppUi[] apps = Apps.ToArray();

            
            Dictionary<string, HNStruct.PerAppSetting> perAppSettingsDictionary = new Dictionary<string, HNStruct.PerAppSetting>();
            foreach (var app in apps)
            {
                if (app.profile != null)
                {
                    perAppSettingsDictionary.Add(app.processPath, new HNStruct.PerAppSetting(app.name, app.processPath, app.profile, app.enabled));
                }
            }

            SettingsUtil.appSettings = perAppSettingsDictionary;
            SettingsUtil.isPerAppSettingsEnabled = enableGlobal;
        }

        private void runningProcessBtn_Click(object sender, EventArgs e)
        {
            FormSelectRunningProcess formSelectRunningProcess = new FormSelectRunningProcess();
            DialogResult result = formSelectRunningProcess.ShowDialog();

            if (result == DialogResult.OK)
            {
                Process selected = formSelectRunningProcess.SelectedProcess;
                _selectedApp.name = selected.ProcessName;
                _selectedApp.processPath = Path.GetFullPath(selected.MainModule.FileName);
                formSelectRunningProcess.Dispose();
            }

        }
    }

    public class AppUi : HNStruct.PerAppSetting
    {
        public Bitmap Image;
        public Profile? ProfileRef;

        public AppUi(string name, string processPath, bool enabled, Bitmap image, Profile? profileRef) 
            : base(name, processPath, profileRef?.Name, enabled)
        {
            this.Image = image;
            this.ProfileRef = profileRef;
        }
    }

    public struct Profile
    {
        public string Name;
        public bool Enabled;

        public Profile(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;
        }
    }
}