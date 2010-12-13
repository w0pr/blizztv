/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
 *  
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU General 
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
 * implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>. 
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BlizzTV.CommonLib.Settings;
using BlizzTV.CommonLib.UI;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;

namespace BlizzTV.UI
{
    public partial class frmPreferences : Form
    {
        private readonly List<TabPage> _pluginTabs = new List<TabPage>();

        public frmPreferences() { InitializeComponent(); }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            this.LoadSettings(); // load settings.
            this.LoadModuleTabs(); // load module setting-tabs.
        }

        public DialogResult ShowDialog(string tabPageName) // show dialog overrider that accepts a tab-name.
        {
            TabControl.SelectedTab = TabControl.TabPages[tabPageName];
            return this.ShowDialog();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.SaveSettings()) // try saving settings.
            {                
                foreach (TabPage t in this._pluginTabs) { ((IModuleSettingsForm) t.Controls[0]).SaveSettings(); } // also notify module's to let them save their settings.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadSettings()
        {
            // global settings.
            if (GlobalSettings.Instance.UseInternalViewers) radioButtonUseInternalViewers.Checked = true; else radioButtonUseDefaultWebBrowser.Checked = true;
            txtVideoPlayerWidth.Text = GlobalSettings.Instance.VideoPlayerWidth.ToString();
            txtVideoPlayerHeight.Text = GlobalSettings.Instance.VideoPlayerHeight.ToString();
            checkBoxVideoAutoPlay.Checked = GlobalSettings.Instance.AutoPlayVideos;
            CheckBoxPlayerAlwaysOnTop.Checked = GlobalSettings.Instance.PlayerWindowsAlwaysOnTop;

            // UI settings.
            checkBoxAllowAutomaticUpdateChecks.Checked = Settings.Instance.AllowAutomaticUpdateChecks;
            checkBoxAllowBetaVersionNotifications.Checked = Settings.Instance.AllowBetaVersionNotifications;
            checkBoxMinimimizeToSystemTray.Checked = Settings.Instance.MinimizeToSystemTray;
            checkBoxEnableDebugLogging.Checked = Settings.Instance.EnableDebugLogging;
            checkBoxEnableDebugConsole.Checked = Settings.Instance.EnableDebugConsole;

            // load module enabled-disabled status settings.
            foreach (KeyValuePair<string, ModuleInfo> pair in ModuleManager.Instance.AvailablePlugins)
            {
                ListviewModuleItem item = new ListviewModuleItem(pair.Value);
                this.listviewModules.SmallImageList.Images.Add(pair.Value.Attributes.Name, pair.Value.Attributes.Icon);
                this.listviewModules.Items.Add(item);
                if (Settings.Instance.Modules.Enabled(pair.Value.Attributes.Name)) item.Checked = true;
            }          
        }

        private void LoadModuleTabs() // loads module  specific preferences tabs
        {
            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Modules.List)
            {
                if (pair.Value) // if module is enabled
                {
                    Form moduleForm = ModuleManager.Instance.InstantiatedPlugins[pair.Key].GetPreferencesForm(); // get module's preferences form.

                    if (moduleForm != null) // if module supplies a preferenes form.
                    {
                        TabPage tab = new TabPage(pair.Key); // create up a new tabpage for it.
                        moduleForm.TopLevel = false; // module form should not behave as top most.
                        moduleForm.Dock = DockStyle.Fill; // let it fill it's parent.
                        moduleForm.FormBorderStyle = FormBorderStyle.None; // it should not have borders too.
                        moduleForm.BackColor = Color.White; 
                        moduleForm.Show(); // show the settings form.
                        tab.ImageIndex = TabControl.ImageList.Images.IndexOfKey(pair.Key); // set the icon.
                        tab.Controls.Add(moduleForm); // add the form to tabpage.                        
                        this._pluginTabs.Add(tab); // add tabpage to list so we can access it later.
                        this.TabControl.TabPages.Insert(this.TabControl.TabPages.Count - 1, tab); // add tabpage to our tabcontrol -- insert it before the last tab, tabDebug.                        
                    }
                }
            }
        }

        private bool SaveSettings() // sets the settings.
        {
            // globals ettings.
            if (radioButtonUseInternalViewers.Checked) GlobalSettings.Instance.UseInternalViewers = true; else GlobalSettings.Instance.UseInternalViewers = false;
            GlobalSettings.Instance.VideoPlayerWidth = Int32.Parse(txtVideoPlayerWidth.Text);
            GlobalSettings.Instance.VideoPlayerHeight = Int32.Parse(txtVideoPlayerHeight.Text);
            GlobalSettings.Instance.AutoPlayVideos = checkBoxVideoAutoPlay.Checked;
            GlobalSettings.Instance.PlayerWindowsAlwaysOnTop = CheckBoxPlayerAlwaysOnTop.Checked;

            // UI settings.
            Settings.Instance.AllowAutomaticUpdateChecks = checkBoxAllowAutomaticUpdateChecks.Checked;
            Settings.Instance.AllowBetaVersionNotifications = checkBoxAllowBetaVersionNotifications.Checked;
            Settings.Instance.MinimizeToSystemTray = checkBoxMinimimizeToSystemTray.Checked;
            Settings.Instance.EnableDebugLogging = checkBoxEnableDebugLogging.Checked;
            Settings.Instance.EnableDebugConsole = checkBoxEnableDebugConsole.Checked;

            // save module-status settings
            foreach (ListviewModuleItem item in listviewModules.Items)
            {
                if (item.Checked) Settings.Instance.Modules.Enable(item.ModuleName); // enable the module.
                else
                {
                    if (ModuleManager.Instance.InstantiatedPlugins.ContainsKey(item.ModuleName))
                    {
                        if (ModuleManager.Instance.InstantiatedPlugins[item.ModuleName].Updating) // don't allow disabling modules that are currently updating data.
                        {
                            MessageBox.Show("You can not de-activate modules that are currently updating. Please wait them finish and re-try.", string.Format("Module {0} is updating", item.ModuleName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    Settings.Instance.Modules.Disable(item.ModuleName); // disable the module.                   
                }
            }

            Settings.Instance.Save(); // save the settings.
            return true;
        }
    }
}
