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
using System.Threading;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Settings;
using BlizzTV.ModuleLib.Notifications;
using BlizzTV.UILib;
using BlizzTV.Updates;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV
{
    public partial class frmMain : Form
    {
        #region members

        private Dictionary<string, TreeItem> _plugin_root_items = new Dictionary<string, TreeItem>();

        #endregion

        #region ctor

        public frmMain()
        {
            InitializeComponent();
            DoubleBufferControl(this.TreeView); // double buffer the treeview as we may have excessive amount of treeview item flooding.
            Workload.Instance.SetProgressBar(this.ProgressBar);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Settings.Instance.NeedInitialConfig) { Wizard.frmWizardHost f = new Wizard.frmWizardHost(); f.ShowDialog(); } // run the configuration wizard

            Application.DoEvents(); // Process the UI-events before loading the plugins -- trying to not have any UI-blocking "as much as" possible.            
            Notifications.Instance.OnNotificationRequest += OnNotificationRequest;            
            this.LoadPlugins(); // Load the enabled plugins.     
        }

        #endregion        

        #region Plugins-specific code & handlers

        private void OnNotificationRequest(object sender, string Title, string Text, System.Windows.Forms.ToolTipIcon Icon)
        {
            this.AsyncInvokeHandler(() =>
            {
                this.TrayIcon.Tag = sender;
                this.TrayIcon.ShowBalloonTip(10000, Title, Text, Icon);
            });
        }

        private void LoadPlugins() // Loads enabled plugins
        {
            ModuleManager pm = ModuleManager.Instance; // Let the plugin-manager run..

            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Plugins.List) // loop through available plugins.
            {
                if (pair.Value && pm.AvailablePlugins.ContainsKey(pair.Key)) this.InstantiatePlugin(pair.Key); // if the plugin is enabled then run it within it's own thread.
            }

            if (Settings.Instance.AllowAutomaticUpdateChecks)
            {
                UpdateManager.Instance.OnFoundNewAvailableUpdate += OnUpdateAutoCheckResult;
                UpdateManager.Instance.Check(); // Check for updates.
            }            
        }

        private void InstantiatePlugin(string key)
        {
            Module plugin = ModuleManager.Instance.Instantiate(key); // get the plugins instance.
            ThreadStart plugin_thread = delegate { RunPlugin(plugin); }; // define plugin's own thread.
            Thread t = new Thread(plugin_thread) { IsBackground = true, Name = string.Format("plugin-{0}-{1}", plugin.Attributes.Name, DateTime.Now.TimeOfDay.ToString()) };  // let the thread a background-one.
            t.Start(); // nuclear-launch detected :)
        }

        private void KillPlugin(string key)
        {
            if (this._plugin_root_items.ContainsKey(key))
            {
                this._plugin_root_items[key].Nodes.Clear();
                this.TreeView.Nodes.Remove(this._plugin_root_items[key]);
                this._plugin_root_items.Remove(key);
            }
            
            ModuleManager.Instance.Kill(key);
        }

        private void RunPlugin(Module p) // Applies plugin settings and run the plugin.
        {
            // register plugin communication events.     
            p.OnPluginUpdateStarted += PluginUpdateStarted;
            p.OnPluginUpdateComplete += PluginUpdateComplete;
            this.RegisterPluginMenus(p); // register plugin sub-menu's.
            p.Run(); // run the plugin & apply it's stored settings.
        }


        private void PluginUpdateStarted(object sender)
        {
            this.TreeView.InvokeHandler(() =>
                {
                    if (!this._plugin_root_items.ContainsKey((sender as Module).Attributes.Name))
                    {
                        TreeItem t = new TreeItem((Module)sender, (sender as Module).RootListItem);
                        TreeView.Nodes.Add(t);
                        this._plugin_root_items.Add((sender as Module).Attributes.Name, t);
                        t.Render();
                    }
                    else this._plugin_root_items[(sender as Module).Attributes.Name].Nodes.Clear();
                });
        }

        private void PluginUpdateComplete(object sender, PluginUpdateCompleteEventArgs e)
        {
            this.TreeView.InvokeHandler(() =>
                {
                    if (this._plugin_root_items.ContainsKey((sender as Module).Attributes.Name))
                    {
                        this.TreeView.BeginUpdate();
                        TreeItem _root_item = this._plugin_root_items[(sender as Module).Attributes.Name];
                        foreach (KeyValuePair<string, ListItem> pair in _root_item.Item.Childs) { this.LoadPluginListItems((Module)sender, pair.Value, _root_item); }
                        this.TreeView.EndUpdate();
                    }
                });
        }

        private void LoadPluginListItems(Module Plugin,ListItem Item,TreeItem Parent)
        {
            TreeItem t = new TreeItem(Plugin, Item);
            Parent.Nodes.Add(t);
            t.Render();

            if (Item.Childs.Count > 0) { foreach (KeyValuePair<string, ListItem> pair in Item.Childs) { this.LoadPluginListItems(Plugin, pair.Value, t); } }            
        }

        private void RegisterPluginMenus(Module p) // Registers plugin's sub-menus.
        {
            this.AsyncInvokeHandler(() =>
                {
                    if (p.Menus.Count > 0) // if plugin requests sub-menu's.
                    {
                        ToolStripMenuItem parent = new ToolStripMenuItem(p.Attributes.Name, p.Attributes.Icon); // create the parent plugin-menu first.
                        menuModules.DropDownItems.Add(parent); // add the parent-menu.

                        foreach (KeyValuePair<string, ToolStripMenuItem> pair in p.Menus) // loop through all plugin sub-menus.
                        {
                            parent.DropDownItems.Add(pair.Value); // add requested sub-menu as a drop-down menu.
                        }
                    }
                });
        }

        private void OnUpdateAutoCheckResult(bool FoundUpdate)
        {
            this.ProcessUpdateCheckResult(FoundUpdate, false);
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) // Treeview node double-click handler.
        {
            TreeItem selection = (TreeItem)TreeView.SelectedNode; // get the selected node
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

        private void TreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p_click = new Point(e.X, e.Y);
                TreeItem selection = (TreeItem)TreeView.GetNodeAt(p_click);
                if (selection != null)
                {
                    if (selection.Item.ContextMenus.Count > 0)
                    {
                        TreeviewContextMenu.Items.Clear();
                        foreach (KeyValuePair<string, ToolStripMenuItem> pair in selection.Item.ContextMenus)
                        {
                            TreeviewContextMenu.Items.Add(pair.Value);
                        }
                        Point p_client = this.PointToClient(TreeView.PointToScreen(p_click));
                        Point p_show = new Point(p_client.X + 5, p_client.Y - 20);
                        TreeviewContextMenu.Show(TreeView, p_show);
                    }
                }
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

        private void MenuFAQ_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/blizztv/wiki/FAQ", null);            
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences p = new frmPreferences();
            if (p.ShowDialog() == System.Windows.Forms.DialogResult.OK) ApplySettings();
        }

        private void MenuPlugins_Click(object sender, EventArgs e)
        {
            frmPreferences p = new frmPreferences();
            if (p.ShowDialog("tabModules") == System.Windows.Forms.DialogResult.OK) ApplySettings();
        }

        private void ApplySettings() // Insantiates or kills plugins based on new applied plugin settings.
        {
            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Plugins.List)
            {
                if (pair.Value && !ModuleManager.Instance.InstantiatedPlugins.ContainsKey(pair.Key)) this.InstantiatePlugin(pair.Key); // instantiate the plugin.
                else if (!pair.Value && ModuleManager.Instance.InstantiatedPlugins.ContainsKey(pair.Key)) this.KillPlugin(pair.Key); // kill the plugin.
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private void MenuDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PQ3D5PMB85L34", null);            
        }

        private void spreadTheWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://twitter.com/?status=I%20like%20BlizzTV%20(http://bit.ly/eVkpwz)%20cause;", null);            
        }

        private void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateManager.Instance.OnFoundNewAvailableUpdate += OnUpdateManualCheckResult;
            UpdateManager.Instance.Check(); // Check for updates.
        }

        private void OnUpdateManualCheckResult(bool UpdateFound)
        {
            this.ProcessUpdateCheckResult(UpdateFound, true);
        }

        private void ProcessUpdateCheckResult(bool FoundUpdate, bool AllowNoUpdatesFoundNotification)
        {
            if (FoundUpdate)
            {
                string update_question = "";
                string update_title = "";

                switch (UpdateManager.Instance.FoundUpdate.UpdateType)
                {
                    case UpdateTypes.STABLE:
                        update_question = "Found a new available update. Do you want to update now?";
                        update_title = "New Update Found!";
                        break;
                    case UpdateTypes.BETA:
                        update_question = "Found a new available BETA update. Do you want to update to this BETA version now?";
                        update_title = "New Beta Update Found!";
                        break;
                }

                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(update_question, update_title, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(UpdateManager.Instance.FoundUpdate.Link);
                }
            }
            else if (AllowNoUpdatesFoundNotification)
            {
                MessageBox.Show("You're already running the latest version.", "No available updates found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MenuSleepMode_Click(object sender, EventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode)
            {
                this.menuSleepMode.Checked = true;
                this.ContextMenuSleepMode.Checked = true;
                this.TrayIcon.Icon = Properties.Resources.ico_sleep_16;
                this.TrayIcon.Text = "BlizzTV is in sleep mode.";
                GlobalSettings.Instance.InSleepMode = true;
            }
            else
            {                
                this.menuSleepMode.Checked = false;
                this.ContextMenuSleepMode.Checked = false;
                this.TrayIcon.Icon = Properties.Resources.ico_blizztv_16;
                this.TrayIcon.Text = "BlizzTV";
                GlobalSettings.Instance.InSleepMode = false;
            }
        }

        #endregion

        #region Form-specific code

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Instance.MinimizeToSystemTray)
            {
                e.Cancel = true; // live in system-tray even if form is closed
                this.HideForm();
            }
            else this.ExitApplication();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) this.HideForm();
        }


        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.ShowInTaskbar == false) // if we're just living in system-tray, remake the main form visible again
            {
                this.ShowForm();
            }
        }

        private void HideForm()
        {            
            this.WindowState = FormWindowState.Minimized; // go minimized
            this.ShowInTaskbar = false; // hide ourself from taskbar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow; // hide from alt-tab.
        }

        private void ShowForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            (this.TrayIcon.Tag as ListItem).BalloonClicked(sender, e);
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

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text)) e.Effect = DragDropEffects.Copy;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            string link = (string)e.Data.GetData(DataFormats.Text);
            foreach(KeyValuePair<string,Module> pair in ModuleManager.Instance.InstantiatedPlugins)
            {
                if (pair.Value.TryDragDrop(link)) break;
            }            
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
}