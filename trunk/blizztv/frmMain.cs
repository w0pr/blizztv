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
        #region members

        private Workload _workload; 

        #endregion

        #region ctor

        public frmMain()
        {
            InitializeComponent();
            DoubleBufferControl(this.TreeView); // double buffer the treeview as we may have excessive amount of treeview item flooding.
            this._workload = new Workload(this.ProgressBar);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Application.DoEvents(); // Process the UI-events before loading the plugins -- trying to not have any UI-blocking "as much as" possible.
            this.LoadPlugins(); // Load the enabled plugins.           
        }

        #endregion        

        #region Plugins-specific code & handlers

        private void LoadPlugins() // Loads enabled plugins
        {
            PluginManager pm = PluginManager.Instance; // Let the plugin-manager run..

            foreach (KeyValuePair<string, PluginSettings> pair in SettingsStorage.Instance.Settings.PluginSettings) // loop through available plugins.
            {
                if (pair.Value.Enabled && pm.AvailablePlugins.ContainsKey(pair.Key)) // if the plugin is enabled then run it within it's own thread.
                {
                    Plugin plugin=pm.GetPlugin(pair.Key); // get the plugins instane.
                    ThreadStart plugin_thread = delegate { RunPlugin(plugin); }; // define plugin's own thread.
                    Thread t = new Thread(plugin_thread) { IsBackground = true };  // let the thread a background-one.
                    t.Start(); // nuclear-launch detected :)
                }
            }
        }

        private void RunPlugin(Plugin p) // Applies plugin settings and run the plugin.
        {
            // register plugin communication events.
            p.OnRegisterListItem += RegisterListItem;  // the treeview item handler.
            p.OnRegisterListItems += RegisterListItems; // the treeview item handler for more than one items.
            p.OnSavePluginSettings += SavePluginSettings;
            p.OnWorkloadAdd += this._workload.Add;
            p.OnWorkloadStep += this._workload.Step;
            this.RegisterPluginMenus(p); // register plugin sub-menu's.
            p.Run(); // run the plugin & apply it's stored settings.
        }

        private void RegisterListItem(object sender, ListItem item, ListItem parent) // Register's a treeview-item for plugins.
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterListItem(sender, item, parent); })); // switch to UI-thread.
            else
            {
                TreeItem t = new TreeItem((Plugin)sender, item); // Create a new treeitem wrapper.
                if (parent != null) (this.TreeView.Nodes.Find(parent.Key, true).GetValue(0) as TreeNode).Nodes.Add(t); // if we have a parent, add the item as sub-item.                        
                else TreeView.Nodes.Add(t); // oh, look we're the root!
                t.Render(); // let the treeview-item wrapper do it's own job.
            }
        }

        private void RegisterListItems(object sender, List<ListItem> items, ListItem parent) // Registers treeview items for plugins.
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterListItems(sender, items, parent); })); // switch to UI-thread.
            else
            {
                foreach (ListItem item in items)
                {
                    TreeItem t = new TreeItem((Plugin)sender, item); // Create a new treeitem wrapper.
                    if (parent != null) (this.TreeView.Nodes.Find(parent.Key, true).GetValue(0) as TreeNode).Nodes.Add(t); // if we have a parent, add the item as sub-item.                        
                    else TreeView.Nodes.Add(t); // oh, look we're the root!
                    t.Render(); // let the treeview-item wrapper do it's own job.
                } 
            }
        }

        private void SavePluginSettings(object sender, PluginSettings settings)
        {
            SettingsStorage.Instance.Settings.PluginSettings[(sender as Plugin).Attributes.Name] = settings;
            SettingsStorage.Instance.Save();
        }

        private void RegisterPluginMenus(Plugin p) // Registers plugin's sub-menus.
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterPluginMenus(p); })); // switch to UI-thread.
            else
            {
                if (p.Menus.Count > 0) // if plugin requests sub-menu's.
                {
                    ToolStripMenuItem parent = new ToolStripMenuItem(p.Attributes.Name, p.Attributes.Icon); // create the parent plugin-menu first.
                    MenuPlugins.DropDownItems.Add(parent); // add the parent-menu.

                    foreach(KeyValuePair<string,ToolStripMenuItem> pair in p.Menus) // loop through all plugin sub-menus.
                    {
                        parent.DropDownItems.Add(pair.Value); // add requested sub-menu as a drop-down menu.
                    }
                }
            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) // Treeview node double-click handler.
        {
            TreeItem selection = (TreeItem)e.Node; // get the selected node
            if (selection.Nodes.Count > 0) if (selection.IsExpanded) selection.Expand();  else selection.Collapse(); // if it's a parent node, let it expand() or collapse().
            else selection.DoubleClicked(sender, e);  // notify the item about the double-click event.
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) // Treeview node click handler
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) // if node is right clicked, that means context menu will be rendered for the node.
            {
                TreeItem selection = (TreeItem)e.Node; // get the selected node
                selection.RightClicked(sender, e); // notify the item about the right click, so it can manage it's context-menu's.
            }
        }

        #endregion

        #region Static menu-handlers

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

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private void MenuDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=TDWWEWYQ9CSU2", null);            
        }

        #endregion

        #region Form-specific code

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // live in system-tray even if form is closed
            this.WindowState = FormWindowState.Minimized; // go minimized
            this.ShowInTaskbar = false; // hide ourself from taskbar
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            (this.TrayIcon.Tag as ListItem).BalloonClicked(sender, e);
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.ShowInTaskbar == false) // if we're just living in system-tray, remake the main form visible again
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
        }

        private void ExitApplication() // Exits the application.
        {
            if (this.TrayIcon != null) // Destroy the notify-icon.
            {
                this.TrayIcon.Visible = false;
                this.TrayIcon.Dispose();
                this.TrayIcon = null;
            }
            Application.ExitThread(); // Exit the application.
        }

        private void MenuExit_Click(object sender, EventArgs e) { this.ExitApplication(); }

        private void TrayIconMenuExit_Click(object sender, EventArgs e) { this.ExitApplication(); }

        public static void DoubleBufferControl(System.Windows.Forms.Control c)
        {
            // http://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form/77233#77233
            // Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx

            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;
            System.Reflection.PropertyInfo db_prop = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            db_prop.SetValue(c, true, null);
        }

        #endregion 
    }

    #region workload processor

    public class Workload // contains information about current workload progress of plugins so we can animate our progressbar on status strip
    {
        private ToolStripProgressBar _progress_bar;
        private int _workload = 0;
        private int _maximum_workload = 0;

        public Workload(ToolStripProgressBar ProgressBar)
        {
            this._progress_bar = ProgressBar;
        }

        public void Add(object sender, int units)
        {
            if (_progress_bar.Owner.InvokeRequired) _progress_bar.Owner.BeginInvoke(new MethodInvoker(delegate() { Add(sender, units); })); // switch to UI-thread.
            else
            {
                this._maximum_workload += units;
                this._workload += units;
                this._progress_bar.Visible = true;
                this._progress_bar.Maximum = this._maximum_workload += units;
                LibBlizzTV.Utils.Log.Instance.Write(LibBlizzTV.Utils.LogMessageTypes.DEBUG, string.Format("{0}: Add workload progress: {1} - total workload: {2}",(sender as Plugin).Attributes.Name, units,this._workload));
            }
        }

        public void Step(object sender)
        {
            if (_progress_bar.Owner.InvokeRequired) _progress_bar.Owner.BeginInvoke(new MethodInvoker(delegate() { Step(sender); })); // switch to UI-thread.
            else
            {                
                this._workload -= 1;
                this._progress_bar.Value = (this._maximum_workload - this._workload);
                if (this._workload == 0)
                {
                    this._progress_bar.Visible = false;
                    this._progress_bar.Value = 0;
                    this._maximum_workload = 0;
                }
                LibBlizzTV.Utils.Log.Instance.Write(LibBlizzTV.Utils.LogMessageTypes.DEBUG, string.Format("{0}: --StepWorkload - total workload: {1}",(sender as Plugin).Attributes.Name, this._workload));
            }
        }
    }

    #endregion
}