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

using System.Windows.Forms;
using BlizzTV.Controls.LoadingCircle;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.Workload
{
    public class WorkloadManager // tracks current workload.
    {
        #region Instance

        private static WorkloadManager _instance = new WorkloadManager();
        public static WorkloadManager Instance { get { return _instance; } }

        #endregion

        public int CurrentWorkload { get; private set; }

        private ToolStripProgressBar _progressBar; // the workload progress-bar.
        private LoadingCircle _progressAnimation; // the loading animation.

        private WorkloadManager()
        {
            this.CurrentWorkload = 0;
        }

        public void AttachControls(ToolStripProgressBar progressBar,LoadingCircle progressIcon) // Attaches controls to workload manager.
        {
            this._progressBar = progressBar;
            this._progressAnimation = progressIcon;
        }

        public void Add(int units) // adds given units of workload.
        {
            this._progressBar.Owner.AsyncInvokeHandler(() =>
            {
                this.CurrentWorkload += units; 
                this._progressBar.Maximum += units; 

                if (!this._progressBar.Visible) this._progressBar.Visible = true;

                if (!this._progressAnimation.Visible) // if the progress loading animation is not active yet, activate it.
                {
                    this._progressAnimation.Visible = true;
                    this._progressAnimation.Active = true;
                }
            });
        }

        public void Step() // steps a completed workload unit.
        {
            this._progressBar.Owner.AsyncInvokeHandler(() =>
            {
                this.CurrentWorkload -= 1;
                if (this.CurrentWorkload > 0) this._progressBar.Value = this._progressBar.Maximum - this.CurrentWorkload;
                else /* if workload queue is empty, clear the controls */
                {
                    this._progressBar.Visible = false;
                    this._progressBar.Value = 0;
                    this._progressBar.Maximum = 0;
                    this._progressAnimation.Visible = false;
                    this._progressAnimation.Active = false;
                }
            });
        }
    }
}
