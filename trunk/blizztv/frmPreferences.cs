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
        public frmPreferences()
        {
            InitializeComponent();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            // load global settings
            if (SettingsStorage.Instance.Settings.GlobalSettings.ContentViewer == ContentViewMethods.INTERNAL_VIEWERS) radioButtonUseInternalViewers.Checked = true;
            else radioButtonUseDefaultWebBrowser.Checked = true;

            txtVideoPlayerWidth.Text = SettingsStorage.Instance.Settings.GlobalSettings.VideoPlayerWidth.ToString();
            txtVideoPlayerHeight.Text = SettingsStorage.Instance.Settings.GlobalSettings.VideoPlayerHeight.ToString();
            checkBoxVideoAutoPlay.Checked = SettingsStorage.Instance.Settings.GlobalSettings.VideoAutoPlay;

            // load plugin settings
            foreach (KeyValuePair<string, PluginInfo> pair in PluginManager.Instance.Plugins)
            {
                ListviewPluginsItem item=new ListviewPluginsItem(pair.Value);
                this.ListviewPlugins.Items.Add(item);
                if (SettingsStorage.Instance.Settings.PluginSettings.ContainsKey(pair.Value.AssemblyName) && SettingsStorage.Instance.Settings.PluginSettings[pair.Value.AssemblyName].Enabled) item.Checked = true;
            }
            
            // load ui-specific settings
            checkBoxEnableDebugLogging.Checked = SettingsStorage.Instance.Settings.EnableDebugLogging;
            checkBoxEnableDebugConsole.Checked = SettingsStorage.Instance.Settings.EnableDebugConsole;
        }

        private void SaveSettings()
        {
            // save global settings
            if (radioButtonUseInternalViewers.Checked) SettingsStorage.Instance.Settings.GlobalSettings.ContentViewer = ContentViewMethods.INTERNAL_VIEWERS;
            else SettingsStorage.Instance.Settings.GlobalSettings.ContentViewer = ContentViewMethods.DEFAULT_WEB_BROWSER;

            SettingsStorage.Instance.Settings.GlobalSettings.VideoPlayerWidth = Int32.Parse(txtVideoPlayerWidth.Text);
            SettingsStorage.Instance.Settings.GlobalSettings.VideoPlayerHeight = Int32.Parse(txtVideoPlayerHeight.Text);
            SettingsStorage.Instance.Settings.GlobalSettings.VideoAutoPlay = checkBoxVideoAutoPlay.Checked;

            // save plugin settings
            foreach (ListviewPluginsItem item in ListviewPlugins.Items)
            {
                string plugin_name = item.PluginInfo.AssemblyName;
                if (!SettingsStorage.Instance.Settings.PluginSettings.ContainsKey(plugin_name)) SettingsStorage.Instance.Settings.PluginSettings.Add(plugin_name, new PluginSettings());
                SettingsStorage.Instance.Settings.PluginSettings[plugin_name].Enabled = item.Checked;                
            }

            SettingsStorage.Instance.Settings.EnableDebugLogging = checkBoxEnableDebugLogging.Checked;
            SettingsStorage.Instance.Settings.EnableDebugConsole = checkBoxEnableDebugConsole.Checked;

            SettingsStorage.Instance.Save();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
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
