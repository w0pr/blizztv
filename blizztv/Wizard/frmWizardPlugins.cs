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

namespace BlizzTV.Wizard
{
    public partial class frmWizardPlugins : Form , IWizardForm
    {
        public frmWizardPlugins()
        {
            InitializeComponent();
        }

        private void frmWizardPlugins_Load(object sender, EventArgs e) { }

        public void Finish()
        {
            if (CheckboxFeeds.Checked) Settings.Instance.Plugins.Enable("Feeds"); else Settings.Instance.Plugins.Disable("Feeds");
            if (CheckboxStreams.Checked) Settings.Instance.Plugins.Enable("Streams"); else Settings.Instance.Plugins.Disable("Streams");
            if (CheckboxVideos.Checked) Settings.Instance.Plugins.Enable("Videos"); else Settings.Instance.Plugins.Disable("Videos");
            if (CheckboxEvents.Checked) Settings.Instance.Plugins.Enable("Events"); else Settings.Instance.Plugins.Disable("Events");
            Settings.Instance.NeedInitialConfig = false;
            Settings.Instance.Save();
        }
    }
}
