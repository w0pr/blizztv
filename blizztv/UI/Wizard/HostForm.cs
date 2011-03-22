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
using BlizzTV.InfraStructure.Modules;

namespace BlizzTV.UI.Wizard
{
    public partial class HostForm : Form
    {
        private readonly IWizardHostable[] _steps = { new WelcomeForm(), new ModulesForm(), new CompleteForm() }; // the wizard steps.
        private int _currentStep = -1; // current step.

        public HostForm() { InitializeComponent(); }

        private void frmWizardHost_Load(object sender, EventArgs e) { this.Step(); } // load the first form. 
        private void ButtonNext_Click(object sender, EventArgs e) { this.Step(); } // move on to next wizard-form.
        private void ButtonBack_Click(object sender, EventArgs e) { this.Step(false); } // move back to previous wizard-form.
        private void ButtonCancel_Click(object sender, EventArgs e) { this.Cancel(); }   // cancel button.

        private void Step(bool forward=true) // steps through forms, forward or backwards.
        {            
            if (this.Panel.Controls.Count > 0) // if we have an active form.
            {
                var onStage = (IWizardHostable)this.Panel.Controls[0]; // the active form.
                onStage.Finish(); // notify about it that we're stepping.
                this.Panel.Controls.Clear(); // remove it from stage.
            }

            if (forward) _currentStep++; else _currentStep--; // update the current step.

            if (_currentStep < this._steps.Length) // if we're not finished yet.
            {
                IWizardHostable f = this._steps[_currentStep]; // load the new step.
                ((Form) f).TopLevel = false; // set the steps settings.
                ((Form)f).FormBorderStyle = FormBorderStyle.None;
                ((Form)f).Dock = DockStyle.Fill;

                this.Text = ((Form)f).Text; // update the window's text.
                this.Panel.Controls.Add((Form)f); // add the new step.
                ((Form)f).Show(); // show the new step.

                this.ButtonNext.Text = this._steps.Length - 1 == _currentStep ? "Finish" : "Next >"; // set the button labels.
                this.ButtonBack.Enabled = _currentStep - 1 != -1; // enable the back-button only if it's valid.
            }
            else this.Close();
        }      
  
        private void Cancel() 
        {
            // If user cancels the module configuration, do just enable all available modules.

            foreach(KeyValuePair<string,ModuleController> pair in ModuleManager.Instance.AvailableModules)
            {
                Settings.Instance.Modules.Enable(pair.Value.Attributes.Name);
            }

            Settings.Instance.NeedInitialConfig = false; // no more run the initial configuration wizard.
            Settings.Instance.Save();
        }
    }
}
