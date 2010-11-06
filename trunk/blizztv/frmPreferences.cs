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
            foreach (KeyValuePair<string, PluginInfo> pair in PluginManager.Instance.Plugins)
            {
                ListviewPluginsItem item=new ListviewPluginsItem(pair.Value);
                this.ListviewPlugins.Items.Add(item);
                if (Settings.Instance.PluginSettings.ContainsKey(pair.Value.AssemblyName) && Settings.Instance.PluginSettings[pair.Value.AssemblyName].Enabled) item.Checked = true;
            }
        }

        private void SaveSettings()
        {
            foreach (ListviewPluginsItem item in ListviewPlugins.Items)
            {
                string plugin_name = item.PluginInfo.AssemblyName;
                if (!Settings.Instance.PluginSettings.ContainsKey(plugin_name)) Settings.Instance.PluginSettings.Add(plugin_name, new PluginSettings());
                Settings.Instance.PluginSettings[plugin_name].Enabled = item.Checked;                
            }
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
