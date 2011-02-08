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
using System.Windows.Forms;
using BlizzTV.Modules;
using BlizzTV.Utility.UI;

namespace BlizzTV.UI.Wizard
{
    public partial class frmWizardPlugins : Form , IWizardForm
    {
        public frmWizardPlugins()
        {
            InitializeComponent();
        }

        private void frmWizardPlugins_Load(object sender, EventArgs e) 
        {
            foreach (KeyValuePair<string, ModuleInfo> pair in ModuleManager.Instance.AvailableModules) // load the available plugins list
            {
                ListviewModuleItem item = new ListviewModuleItem(pair.Value);
                this.listviewModules.SmallImageList.Images.Add(pair.Value.Attributes.Name, pair.Value.Attributes.Icon);
                this.listviewModules.Items.Add(item);
            }   
        }

        public void Finish()
        {
            foreach (ListviewModuleItem item in this.listviewModules.Items) // enable or disable the plugins based on selections.
            {
                if (item.Checked) Settings.Instance.Modules.Enable(item.ModuleName);
                else Settings.Instance.Modules.Disable(item.ModuleName);
            }

            Settings.Instance.NeedInitialConfig = false; // no more run the initial configuration wizard.
            Settings.Instance.Save();
        }
    }
}
