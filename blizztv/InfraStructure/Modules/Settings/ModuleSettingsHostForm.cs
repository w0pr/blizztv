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
using System.Drawing;
using System.Windows.Forms;

namespace BlizzTV.InfraStructure.Modules.Settings
{
    /// <summary>
    /// Hosts a module-settings window.
    /// </summary>
    public partial class ModuleSettingsHostForm : Form
    {
        private readonly Form _hostedForm; // the hosted module-settings form.
        private readonly ModuleAttributes _moduleAttributes; // attributes of the hosted form's module.

        public ModuleSettingsHostForm(ModuleAttributes moduleAttributes, Form hostedForm)
        {
            InitializeComponent();

            this._hostedForm = hostedForm;
            this._moduleAttributes = moduleAttributes;
        }

        private void ModuleSettingsHostForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} Settings", this._moduleAttributes.Name);
            this.Icon = Icon.FromHandle(this._moduleAttributes.Icon.GetHicon()); // set the icon based on the module' provided icon.
            this._hostedForm.TopLevel = false; 
            this._hostedForm.Dock = DockStyle.Fill;
            this._hostedForm.FormBorderStyle = FormBorderStyle.None;
            this.Panel.Controls.Add(this._hostedForm);
            this._hostedForm.Show();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {            
            ((IModuleSettingsForm) this._hostedForm).SaveSettings();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
