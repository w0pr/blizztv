/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using BlizzTV.Configuration;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Notifications;
using BlizzTV.Settings;
using BlizzTV.Updates;
using BlizzTV.Utility.Extensions;
using BlizzTV.Win32API;

namespace BlizzTV.UI
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<string, ModuleNode> _moduleNodes = new Dictionary<string, ModuleNode>();

        #region ctor & form handlers

        public MainForm()
        {
            InitializeComponent();

            this.TreeView.DoubleBuffer(); // double buffer the treeview as we may have excessive amount of treeview item flooding.
            Workload.WorkloadManager.Instance.AttachControls(this.ProgressBar, this.LoadingAnimation.LoadingAnimationControl); // init. workload-manager.
            NotificationManager.Instance.AttachControls(this, this.TrayIcon, this.NotificationIcon); // init. notification-manager.

            Module.UITreeView = this.TreeView;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load the last known size & location for the window.
            this.Size = new Size(Settings.Instance.MainWindowWidth, Settings.Instance.MainWindowHeight);
            this.DesktopLocation = new Point(Settings.Instance.MainWindowLocationX, Settings.Instance.MainWindowLocationY);

            if (Settings.Instance.NeedInitialConfig) { Wizard.frmWizardHost f = new Wizard.frmWizardHost(); f.ShowDialog(); } // if required run the configuration wizard.
            if (RuntimeConfiguration.Instance.StartedOnSystemStartup) this.MinimizeToSystemTray(); // if the app started on system startup, don't show the main form.
            Application.DoEvents(); // Process the UI-events before loading the plugins -- trying to not have any UI-blocking "as much as" possible.                     
            this.LoadModules(); // Load the enabled plugins.     
            this.AutomaticUpdateCheck(); // Automatically check for updates.
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            bool needsSaving = false;

            if (this.Size.Width != Settings.Instance.MainWindowWidth)
            {
                Settings.Instance.MainWindowWidth = this.Size.Width;
                needsSaving = true;
            }
            if (this.Size.Height != Settings.Instance.MainWindowHeight)
            {
                Settings.Instance.MainWindowHeight = this.Size.Height;
                needsSaving = true;
            }
            if (this.DesktopLocation.X != Settings.Instance.MainWindowLocationX)
            {
                Settings.Instance.MainWindowLocationX = this.DesktopLocation.X;
                needsSaving = true;
            }
            if (this.DesktopLocation.Y != Settings.Instance.MainWindowLocationY)
            {
                Settings.Instance.MainWindowLocationY = this.DesktopLocation.Y;
                needsSaving = true;
            }

            if (needsSaving) Settings.Instance.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && Settings.Instance.MinimizeToSystemTray) // only hook when user is closing the main form.
            {
                e.Cancel = true; // live in system-tray even if form is closed
                this.MinimizeToSystemTray();
            }
            else this.ExitApplication();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WindowMessaging.WM_BLIZZTV_SETFRONTMOST) this.SetFrontMostWindow();
            base.WndProc(ref m);
        }

        private void SetFrontMostWindow()
        {
            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            if (this.Visible == false) this.RestoreFromSystemTray();
            bool lastTopMostState = this.TopMost;
            this.TopMost = true;
            this.TopMost = lastTopMostState;
        }

        private void MinimizeToSystemTray()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow; // hide from alt-tab menu.
            this.Visible = false;
        }

        private void RestoreFromSystemTray()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable; // let it be shown in alt-tab menu.      
            this.Visible = true;
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

        #endregion

        #region modules handling code

        private void LoadModules() // Loads enabled plugins
        {
            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Modules.List) // loop through available modules.
            {
                if (pair.Value && ModuleManager.Instance.AvailableModules.ContainsKey(pair.Key)) this.LoadModule(pair.Key); // if module is enabled, run it.
            }          
        }

        private void LoadModule(string key) // Instantes & runs a module in a thread.
        {
            Module module = ModuleManager.Instance.Load(key); // get the module instance.
            ThreadStart threadStart = () => StartupModule(module); // create a new thread for the module.
            var moduleThread = new Thread(threadStart) { IsBackground = true };  // make the thread a background-one.
            moduleThread.Start();

            if (module.CanRenderMenus) this.AttachModuleMenus(module); // register's the module menus.

            if (!module.CanRenderTreeNodes) return;  // check if module can render tree-node's.

            ModuleNode moduleNode = module.GetModuleNode(); // get the module's root node.
            if (moduleNode == null) return; // check if we got a valid module-node returned.
            this.TreeView.Nodes.Add(moduleNode);
            this._moduleNodes.Add(key, moduleNode);
        }

        private void KillModule(string key) // Kill's an active module.
        {
            if (this._moduleNodes.ContainsKey(key)) // clean up the module.
            {
                ModuleNode rootNode = this._moduleNodes[key];
                rootNode.Nodes.Clear(); // remove the module root's childs.
                this.TreeView.Nodes.Remove(rootNode); // remove the module root from treeview.
                this._moduleNodes.Remove(key); // remove the module root from dictionary.
            }
            
            ModuleManager.Instance.Kill(key); // let the module-manager to kill it.
        }

        private void StartupModule(Module module) // Startup's a module.
        {
            module.Startup();            
        }

        private void AttachModuleMenus(Module p) // Register's modules main-menu item's.
        {
            Dictionary<string, ToolStripMenuItem> menus = p.GetMenus(); // request the module menus.
            if (menus == null) return; 
            if (menus.Count <= 0) return;

            this.AsyncInvokeHandler(() =>
            {
                var parent = new ToolStripMenuItem(p.Attributes.Name, p.Attributes.Icon); // create the parent module-menu.
                menuModules.DropDownItems.Add(parent); // add the parent-menu.

                foreach (KeyValuePair<string, ToolStripMenuItem> pair in menus) parent.DropDownItems.Add(pair.Value); // add requested sub-menu as a drop-down menu.
            });
        }

        #endregion

        #region automatic update check

        private void AutomaticUpdateCheck() // Checks for if an update is available
        {
            if (GlobalSettings.Instance.AllowAutomaticUpdateChecks) { UpdateManager.Check(); }
        }        

        #endregion

        #region treeview handlers

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) // Treeview node double-click handler.
        {
            var selection = (ModuleNode) this.TreeView.SelectedNode;
            if (selection == null) return;

            selection.Open(sender, e);
        }

        private void TreeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var selection = (ModuleNode)TreeView.SelectedNode; // get the selected node
                if (selection == null) return;
                    
                selection.Open(sender, e);  
                e.Handled = true; // don't let treeview beep.
            }
        }

        private void TreeView_MouseUp(object sender, MouseEventArgs e) 
        {
            if (e.Button != MouseButtons.Right) return;

            var pClick = new Point(e.X, e.Y); // the click-point.
            var selection = (ModuleNode)TreeView.GetNodeAt(pClick); // the clicked node.
            if (selection == null) return;

            TreeView.SelectedNode = selection;
            if (selection.Menu.Count == 0) return; // if the selected item has no attach menu's just return.

            selection.RightClicked(this, e);

            TreeviewContextMenu.Items.Clear();
            foreach(KeyValuePair<string,ToolStripMenuItem> pair in selection.Menu) { TreeviewContextMenu.Items.Add(pair.Value); } //add custom-context menu's.

            Point pClient = this.PointToClient(TreeView.PointToScreen(pClick)); // point to screen.
            var pShow = new Point(pClient.X + 5, pClient.Y - 20); // the actual cordinates.
            TreeviewContextMenu.Show(TreeView, pShow);
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text)) e.Effect = DragDropEffects.Copy;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            string link = (string)e.Data.GetData(DataFormats.Text);
            foreach (KeyValuePair<string, Module> pair in ModuleManager.Instance.LoadedModules)
            {
                if (pair.Value.AddSubscriptionFromUrl(link)) break;
            }
        }

        #endregion

        #region settings handlers

        private void MenuPreferences_Click(object sender, EventArgs e) // shows preferences form.
        {
            PreferencesForm p = new PreferencesForm();
            if (p.ShowDialog() == DialogResult.OK) ApplySettings();
        }

        private void ApplySettings() // Insantiates or kills plugins based on new applied plugin settings.
        {
            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Modules.List)
            {
                if (pair.Value && !ModuleManager.Instance.LoadedModules.ContainsKey(pair.Key)) this.LoadModule(pair.Key); // instantiate the plugin.
                else if (!pair.Value && ModuleManager.Instance.LoadedModules.ContainsKey(pair.Key)) this.KillModule(pair.Key); // kill the plugin.
            }
        }

        #endregion

        #region tray-icon & status-bar

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.RestoreFromSystemTray(); // if we're just living in system-tray, remake the main form visible again            
        }

        private void NotificationIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) NotificationManager.Instance.ClearArchivedNotifications();
        }

        #endregion

        #region menu-handlers

        private void MenuSleepMode_Click(object sender, EventArgs e) // puts the program in sleep mode or wakes it from it.
        {
            if (!RuntimeConfiguration.Instance.InSleepMode)
            {
                this.menuSleepMode.Checked = true;
                this.ContextMenuSleepMode.Checked = true;
                this.SleepIcon.Visible = true;
                this.TrayIcon.Icon = Assets.Images.Icons.Ico.sleep;
                this.TrayIcon.Text = "BlizzTV is in sleep mode.";
                RuntimeConfiguration.Instance.InSleepMode = true;
            }
            else
            {
                this.menuSleepMode.Checked = false;
                this.ContextMenuSleepMode.Checked = false;
                this.SleepIcon.Visible = false;
                this.TrayIcon.Icon = Assets.Images.Icons.Ico.blizztv;
                this.TrayIcon.Text = "BlizzTV";
                RuntimeConfiguration.Instance.InSleepMode = false;
            }
        }

        private void MenuCheckUpdates(object sender, EventArgs e) // manually checks for updates.
        {
            UpdateManager.Check(true); 
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog();
        }

        private void MenuExit_Click(object sender, EventArgs e) { this.ExitApplication(); }
        private void TrayIconMenuExit_Click(object sender, EventArgs e) { this.ExitApplication(); }
        private void MenuBlizzTVCom_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://www.blizztv.com", null); }
        private void MenuBugReports_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://code.google.com/p/blizztv/issues/list", null); }
        private void MenuUserGuide_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://www.blizztv.com/topic/96-user-guide/", null); }
        private void MenuFAQ_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://www.blizztv.com/topic/95-frequently-asked-questions/", null); }
        private void MenuDonate_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PQ3D5PMB85L34", null); }

        #endregion       
    }    
}