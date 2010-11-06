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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using LibBlizzTV;

namespace BlizzTV
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            DoubleBufferControl(this.List);
            bool exists = Storage.Instance.StorageExists(); // temp. here to force storage read the settings.

            if(Settings.Instance.EnableDebugConsole) DebugConsole.init();
        }

        public static void DoubleBufferControl(System.Windows.Forms.Control c)
        {
            // http://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form/77233#77233
            // Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            
            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;
            System.Reflection.PropertyInfo db_prop = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered",System.Reflection.BindingFlags.NonPublic |System.Reflection.BindingFlags.Instance);
            db_prop.SetValue(c, true, null);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.LoadPlugins();
        }

        private void LoadPlugins()
        {
            PluginManager pm = PluginManager.Instance;
            foreach (KeyValuePair<string, PluginSettings> pair in Settings.Instance.PluginSettings)
            {
                if (pair.Value.Enabled && pm.Plugins.ContainsKey(pair.Key)) // if the plugin is enabled
                {
                    PluginInfo pi = pm.Plugins[pair.Key];
                    Plugin Plugin = pi.CreateInstance();
                    ThreadStart plugin_thread = delegate { RunPlugin(Plugin); };
                    Thread t = new Thread(plugin_thread) { IsBackground = true };
                    t.Start();
                }
            }
        }

        private void RunPlugin(Plugin p)
        {            
            p.OnRegisterListGroup += RegisterListGroup;
            p.OnRegisterListItem += RegisterListItem;
            p.ApplyGlobalSettings(Settings.Instance.GlobalSettings);
            p.Load(Settings.Instance.PluginSettings[p.PluginInfo.AssemblyName]);
        }

        private void RegisterListGroup(object sender, ListGroup g)
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterListGroup(sender, g); }));
            else List.Groups.Add(new ListViewGroup(g.Key,g.Name));
        }

        private void RegisterListItem(object sender, ListItem item,ListGroup group)
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterListItem(sender, item, group); }));
            else
            {
                ListItemContainer c = new ListItemContainer((Plugin)sender,item);
                c.Group = List.Groups[group.Key];
                this.List.Items.Add(c);
            }
        }

        private void List_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListItemContainer item = (ListItemContainer)List.Items[e.ItemIndex];
            item.DrawSubItem(sender, e);
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            if (List.SelectedItems.Count > 0)
            {
                ListItemContainer selection = (ListItemContainer)List.SelectedItems[0];
                selection.DoubleClick(sender, e);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private void blizzTVcomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.blizztv.com", null);
        }

        private void bugReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/blizztv/issues/list", null);
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/blizztv/wiki/UserGuide", null);            
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences p = new frmPreferences();
            p.ShowDialog();
        }
    }
}
