# Huion/Gaomon Unlocked v14.7.4.2

This is a modification of the official driver for **ALL graphics tablets from Huion and Gaomon** and a few other Chinese manufacturers, unlocked from any limitations and improved by useful features and bugfixes.

It is based on Huion/Gaomon driver version 14.7.4 (released on 2018/08/24).
You can download it here: https://github.com/yutamago/HuionGaomonUnlocked/releases/tag/v14.7.4.2

## Features
- Removed all locks for Gaomon-devices
- New v14 UI for everyone
- New fancy pressure sensitivity curve
- Game Mode
- Mouse Mode
- **Per-App Profiles**: Export your favorite settings for each application! Whenever you switch, the profile switches automatically!
- **Disable Update Notifications**: You can choose if you like to be reminded to update or not. However: any official update will overwrite this patch. Don't update if you own a Gaomon device, the official driver will not work for you!

## Compatibility
- All Huion Graphic Tablets
- All Gaomon Graphic Tablets
- All Yineng, Youshang, Shijun and KJC Graphic Tablets

## How to install:

BEFORE YOU START: Uninstall previously installed drivers from Huion and Gaomon. Also delete the existing SettingsFile.xml in your driver folder.

1. Install the official Huion driver v14.7.4. You can find it included in my zip file.
2. Copy all files from the included **Patch 14.7.4.2** folder over the already existing files in your **HuionTablet** folder.
3. Restart the Huion Tablet Settings

## How to setup custom profiles
### Simple
1. Customize your settings for any application and export it to your profile directory. Save it as the same filename as your target application has. (i.e. photoshop.exe -> photoshop.xml, zbrush.exe -> zbrush.xml, osu!.exe -> osu!.xml)
If you haven't set a profile directory, you can find it in your **Huion Tablet** directory under **profiles**)
2. If you want, you can set a default.xml profile, that will be activated, whenever you leave your target applications.
3. When you've created new profiles (or deleted them) you will have to reload the registered profiles in your Huion Tablet control panel (or restart the control panel)!

### Advanced [Optional]
- You can manually assign profiles to each application by setting **manualMode to 1** in your SettingsFile.xml. You can even set multiple applications to use the same profile.
- You can also change the profile directory by changing **profileDir="profiles"** to any other folder on your computer. The path can be either absolute or relative.
- I've setup two sample SettingsFile.xml files, which you can refer to, if you feel lost.

## Screenshots
<img src="https://github.com/yutamago/HuionGaomonUnlocked/raw/master/about.png" alt="" width="600" />
<img src="https://github.com/yutamago/HuionGaomonUnlocked/raw/master/about_gaomon.png" alt="" width="600" />
<img src="https://github.com/yutamago/HuionGaomonUnlocked/raw/master/pen-settings.png" alt="" width="600" />

## Why is this compatible with other tablets than Huion's own?
Huion manufactures graphics tablets under multiple different company names. These companies include: Huion, Gaomon, Yineng, Youshang, Shijun and KJC, most of which you have probably not heard of.
Huions driver is compatible with all graphics tablets of all those manufacturers.

However, there is a runtime check in HNCommon.dll which determines the manufacturer of the device you own. If the driver software is not published by the manufacturer of your device, it will refuse to work. I simply overwrite the hardcoded driver publisher at runtime, into the manufacturer it detects.

If you own a Huion Device, it will show you the Huion Control Panel. If you own a Gaomon Device it will show you the Gaomon Control Panel. Simple.
