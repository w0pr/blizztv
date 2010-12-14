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
using System.Windows.Forms;
using BlizzTV.ModuleLib.Settings;

namespace BlizzTV.Modules.BlizzBlues
{
    public partial class frmSettings : Form, IModuleSettingsForm
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            numericUpDownUpdateBlizzBluesEveryXMinutes.Value = (decimal)Settings.Instance.UpdateEveryXMinutes;
            checkBoxTrackWorldofWarcraft.Checked = Settings.Instance.TrackWorldofWarcraft;
            checkBoxTrackStarcraft.Checked = Settings.Instance.TrackStarcraft;
        }

        public void SaveSettings()
        {
            Settings.Instance.UpdateEveryXMinutes = (int)numericUpDownUpdateBlizzBluesEveryXMinutes.Value;
            Settings.Instance.TrackWorldofWarcraft = checkBoxTrackWorldofWarcraft.Checked;
            Settings.Instance.TrackStarcraft = checkBoxTrackStarcraft.Checked;
            Settings.Instance.Save();
            BlizzBluesModule.Instance.OnSaveSettings();
        }
    }
}
