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
    public partial class frmWizardHost : Form
    {
        private IWizardForm[] _steps = { new frmWizardWelcome(), new frmWizardPlugins() };
        private int current_step = -1;

        public frmWizardHost()
        {
            InitializeComponent();
        }

        private void frmWizardHost_Load(object sender, EventArgs e)
        {
            this.Step();
        }

        private void Step(bool Forward=true)
        {            
            if (this.Panel.Controls.Count > 0)
            {
                IWizardForm on_stage = (IWizardForm)this.Panel.Controls[0];
                on_stage.Finish();
                this.Panel.Controls.Clear();
            }

            if (Forward) current_step++; else current_step--;

            if (current_step < this._steps.Length)
            {
                IWizardForm f = this._steps[current_step];
                (f as Form).TopLevel = false;
                (f as Form).FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                (f as Form).Dock = DockStyle.Fill;

                this.Text = (f as Form).Text;
                this.Panel.Controls.Add((Form)f);
                (f as Form).Show();

                if (this._steps.Length - 1 == current_step) this.ButtonNext.Text = "Finish"; else this.ButtonNext.Text = "Next >";
                if (current_step - 1 == -1) this.ButtonBack.Enabled = false; else this.ButtonBack.Enabled = true;
            }
            else this.Close();
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            this.Step();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Step(false);
        }    

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}
