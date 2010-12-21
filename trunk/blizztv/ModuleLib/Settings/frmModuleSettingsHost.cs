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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlizzTV.ModuleLib.Settings
{
    public partial class frmModuleSettingsHost : Form
    {
        private Form _form;
        private ModuleAttributes _moduleAttributes;

        public frmModuleSettingsHost(ModuleAttributes moduleAttributes, Form form)
        {
            InitializeComponent();

            this._form = form;
            this._moduleAttributes = moduleAttributes;
        }

        private void frmModuleSettingsHost_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} Settings", this._moduleAttributes.Name);
            this.Icon = Icon.FromHandle(this._moduleAttributes.Icon.GetHicon());
            this._form.TopLevel = false;
            this._form.Dock = DockStyle.Fill;
            this._form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Panel.Controls.Add(this._form);
            this._form.Show();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            (this._form as IModuleSettingsForm).SaveSettings();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
