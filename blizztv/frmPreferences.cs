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
            if (Settings.Instance.GlobalSettings.ContentViewer == ContentViewMethods.INTERNAL_VIEWERS) radioButtonUseInternalViewers.Checked = true;
            else radioButtonUseDefaultWebBrowser.Checked = true;

            txtVideoPlayerWidth.Text = Settings.Instance.GlobalSettings.VideoPlayerWidth.ToString();
            txtVideoPlayerHeight.Text = Settings.Instance.GlobalSettings.VideoPlayerHeight.ToString();
            checkBoxVideoAutoPlay.Checked = Settings.Instance.GlobalSettings.VideoAutoPlay;

            // load plugin settings
            foreach (KeyValuePair<string, PluginInfo> pair in PluginManager.Instance.Plugins)
            {
                ListviewPluginsItem item=new ListviewPluginsItem(pair.Value);
                this.ListviewPlugins.Items.Add(item);
                if (Settings.Instance.PluginSettings.ContainsKey(pair.Value.AssemblyName) && Settings.Instance.PluginSettings[pair.Value.AssemblyName].Enabled) item.Checked = true;
            }
            
            // load ui-specific settings
            checkBoxEnableDebugLogging.Checked = Settings.Instance.EnableDebugLogging;
            checkBoxEnableDebugConsole.Checked = Settings.Instance.EnableDebugConsole;
        }

        private void SaveSettings()
        {
            // save global settings
            if (radioButtonUseInternalViewers.Checked) Settings.Instance.GlobalSettings.ContentViewer = ContentViewMethods.INTERNAL_VIEWERS;
            else Settings.Instance.GlobalSettings.ContentViewer = ContentViewMethods.DEFAULT_WEB_BROWSER;

            Settings.Instance.GlobalSettings.VideoPlayerWidth = Int32.Parse(txtVideoPlayerWidth.Text);
            Settings.Instance.GlobalSettings.VideoPlayerHeight = Int32.Parse(txtVideoPlayerHeight.Text);
            Settings.Instance.GlobalSettings.VideoAutoPlay = checkBoxVideoAutoPlay.Checked;

            // save plugin settings
            foreach (ListviewPluginsItem item in ListviewPlugins.Items)
            {
                string plugin_name = item.PluginInfo.AssemblyName;
                if (!Settings.Instance.PluginSettings.ContainsKey(plugin_name)) Settings.Instance.PluginSettings.Add(plugin_name, new PluginSettings());
                Settings.Instance.PluginSettings[plugin_name].Enabled = item.Checked;                
            }

            Settings.Instance.EnableDebugLogging = checkBoxEnableDebugLogging.Checked;
            Settings.Instance.EnableDebugConsole = checkBoxEnableDebugConsole.Checked;

            Storage.Instance.SaveSettings();
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
