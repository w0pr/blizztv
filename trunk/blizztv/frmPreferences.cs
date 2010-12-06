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
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;
using BlizzTV.UILib;

namespace BlizzTV
{
    public partial class frmPreferences : Form
    {
        private List<TabPage> _plugin_tabs = new List<TabPage>();

        public frmPreferences()
        {
            InitializeComponent();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        public DialogResult ShowDialog(string tabPageName)
        {
            TabControl.SelectedTab = TabControl.TabPages[tabPageName];
            return this.ShowDialog();
        }

        private void LoadSettings()
        {

            if (GlobalSettings.Instance.UseInternalViewers) radioButtonUseInternalViewers.Checked = true;
            else radioButtonUseDefaultWebBrowser.Checked = true;

            checkBoxAllowAutomaticUpdateChecks.Checked = Settings.Instance.AllowAutomaticUpdateChecks;
            checkBoxAllowBetaVersionNotifications.Checked = Settings.Instance.AllowBetaVersionNotifications;

            txtVideoPlayerWidth.Text = GlobalSettings.Instance.VideoPlayerWidth.ToString();
            txtVideoPlayerHeight.Text = GlobalSettings.Instance.VideoPlayerHeight.ToString();
            checkBoxVideoAutoPlay.Checked = GlobalSettings.Instance.AutoPlayVideos;
            CheckBoxPlayerAlwaysOnTop.Checked = GlobalSettings.Instance.PlayerWindowsAlwaysOnTop;

            checkBoxMinimimizeToSystemTray.Checked = Settings.Instance.MinimizeToSystemTray;
            checkBoxEnableDebugLogging.Checked = Settings.Instance.EnableDebugLogging;
            checkBoxEnableDebugConsole.Checked = Settings.Instance.EnableDebugConsole;

            // plugin settings.
            foreach (KeyValuePair<string, ModuleInfo> pair in ModuleManager.Instance.AvailablePlugins)
            {
                ListviewModuleItem item=new ListviewModuleItem(pair.Value);
                this.listviewModules.SmallImageList.Images.Add(pair.Value.Attributes.Name, pair.Value.Attributes.Icon);
                this.listviewModules.Items.Add(item);
                if (Settings.Instance.Plugins.Enabled(pair.Value.Attributes.Name)) item.Checked = true;
            }

            // load plugins specific preferences tabs
            this.LoadPluginTabs();
        }

        private void LoadPluginTabs() // loads plugins specific preferences tabs
        {
            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Plugins.List)
            {
                if (pair.Value) // if plugin is enabled
                {
                    Form plugin_form = ModuleManager.Instance.InstantiatedPlugins[pair.Key].GetPreferencesForm(); // get plugin's preferences form.
                    if (plugin_form != null) // if plugin defined a preferences form in reality.
                    {
                        TabPage t = new TabPage(pair.Key); // create up a new tabpage for it.
                        plugin_form.TopLevel = false; // plugin form should not behave as top most.
                        plugin_form.Dock = DockStyle.Fill; // let it fill it's parent.
                        plugin_form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // it should not have borders too.
                        plugin_form.BackColor = Color.White;
                        plugin_form.Show(); // show the settings form.
                        t.ImageIndex = TabControl.ImageList.Images.IndexOfKey(pair.Key); // set the icon.
                        t.Controls.Add(plugin_form); // add the form to tabpage.                        
                        this._plugin_tabs.Add(t); // add tabpage to list so we can access it later.
                        this.TabControl.TabPages.Insert(this.TabControl.TabPages.Count - 1, t); // add tabpage to our tabcontrol -- insert it before the last tab, tabDebug.                        
                    }
                }
            }
        }

        private bool SaveSettings()
        {
            // save global settings
            if (radioButtonUseInternalViewers.Checked) GlobalSettings.Instance.UseInternalViewers = true;
            else GlobalSettings.Instance.UseInternalViewers = false;

            Settings.Instance.AllowAutomaticUpdateChecks = checkBoxAllowAutomaticUpdateChecks.Checked;
            Settings.Instance.AllowBetaVersionNotifications = checkBoxAllowBetaVersionNotifications.Checked;

            GlobalSettings.Instance.VideoPlayerWidth = Int32.Parse(txtVideoPlayerWidth.Text);
            GlobalSettings.Instance.VideoPlayerHeight = Int32.Parse(txtVideoPlayerHeight.Text);
            GlobalSettings.Instance.AutoPlayVideos = checkBoxVideoAutoPlay.Checked;
            GlobalSettings.Instance.PlayerWindowsAlwaysOnTop = CheckBoxPlayerAlwaysOnTop.Checked;

            Settings.Instance.MinimizeToSystemTray = checkBoxMinimimizeToSystemTray.Checked;
            Settings.Instance.EnableDebugLogging = checkBoxEnableDebugLogging.Checked;
            Settings.Instance.EnableDebugConsole = checkBoxEnableDebugConsole.Checked;

            // save plugin settings
            foreach (ListviewModuleItem item in listviewModules.Items)
            {
                if (item.Checked) Settings.Instance.Plugins.Enable(item.ModuleName);
                else
                {
                    if (ModuleManager.Instance.InstantiatedPlugins[item.ModuleName].Updating)
                    {
                        MessageBox.Show("You can not de-activate modules that are currently updating. Please wait them finish and re-try.", string.Format("Module {0} is updating", item.ModuleName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    Settings.Instance.Plugins.Disable(item.ModuleName);                    
                }
            }

            Settings.Instance.Save();
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.SaveSettings())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                foreach (TabPage t in this._plugin_tabs) { (t.Controls[0] as IModuleSettingsForm).SaveSettings(); } // also notify plugin settings forms to save their data also
                this.Close();
            }
        }
    }
}
