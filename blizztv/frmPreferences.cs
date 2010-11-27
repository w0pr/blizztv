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
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV;

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

        public void ShowTabPage(string PageName)
        {
            TabControl.SelectedTab = TabControl.TabPages[PageName];
        }


        public delegate void ApplySettingsEventHandler();
        public event ApplySettingsEventHandler OnApplySettings; 

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
            foreach (KeyValuePair<string, PluginInfo> pair in PluginManager.Instance.AvailablePlugins)
            {
                ListviewPluginsItem item=new ListviewPluginsItem(pair.Value);
                this.ListviewPlugins.Items.Add(item);
                if (SettingsStorage.Instance.Settings.PluginSettings.ContainsKey(pair.Value.Attributes.Name) && SettingsStorage.Instance.Settings.PluginSettings[pair.Value.Attributes.Name].Enabled) item.Checked = true;
            }
            
            // load plugins specific preferences tabs
            this.LoadPluginTabs();
        }

        private void LoadPluginTabs() // loads plugins specific preferences tabs
        {
            foreach (KeyValuePair<string, PluginSettings> pair in SettingsStorage.Instance.Settings.PluginSettings) // loop through available plugins
            {
                if (pair.Value.Enabled) // if plugin is enabled
                {
                    Form plugin_form = PluginManager.Instance.InstantiatedPlugins[pair.Key].GetPreferencesForm(); // get plugin's preferences form.
                    if (plugin_form != null) // if plugin defined a preferences form in reality.
                    {
                        TabPage t = new TabPage(pair.Key); // create up a new tabpage for it.
                        plugin_form.TopLevel = false; // plugin form should not behave as top most.
                        plugin_form.Dock = DockStyle.Fill; // let it fill it's parent.
                        plugin_form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // it should not have borders too.
                        plugin_form.BackColor = Color.White; 
                        plugin_form.Show(); // show the settings form.
                        TabControl.ImageList.Images.Add(pair.Key, PluginManager.Instance.InstantiatedPlugins[pair.Key].Attributes.Icon); // get the plugin icon.
                        t.ImageIndex = TabControl.ImageList.Images.IndexOfKey(pair.Key); // set the icon.
                        t.Controls.Add(plugin_form); // add the form to tabpage.                        
                        this._plugin_tabs.Add(t); // add tabpage to list so we can access it later.
                        this.TabControl.TabPages.Insert(this.TabControl.TabPages.Count - 1, t); // add tabpage to our tabcontrol -- insert it before the last tab, tabDebug.                        
                    }
                }
            }
        }

        private void SaveSettings()
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
            foreach (ListviewPluginsItem item in ListviewPlugins.Items)
            {
                string plugin_name = item.PluginInfo.Attributes.Name;
                if (!SettingsStorage.Instance.Settings.PluginSettings.ContainsKey(plugin_name)) SettingsStorage.Instance.Settings.PluginSettings.Add(plugin_name, new PluginSettings());
                SettingsStorage.Instance.Settings.PluginSettings[plugin_name].Enabled = item.Checked;                
            }

            SettingsStorage.Instance.Save();
            Settings.Instance.Save();
            GlobalSettings.Instance.Save();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {            
            this.SaveSettings();  // save global settings
            foreach (TabPage t in this._plugin_tabs) { (t.Controls[0] as IPluginSettingsForm).SaveSettings(); } // also notify plugin settings forms to save their data also
            if (this.OnApplySettings != null) this.OnApplySettings(); // notify observers.
            this.Close();
        }
    }

    internal class ListviewPluginsItem : ListViewItem
    {
        private PluginInfo _plugin_info;
        public PluginInfo PluginInfo { get { return this._plugin_info; } }

        public ListviewPluginsItem(PluginInfo p)
        {
            _plugin_info = p;
            this.SubItems.Add(new ListViewSubItem());
            this.SubItems.Add(p.Attributes.Name);
            this.SubItems.Add(p.Attributes.Description);
        }
    }
}
