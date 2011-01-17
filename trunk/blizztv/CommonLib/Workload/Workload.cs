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
using BlizzTV.CommonLib.UI;
using BlizzTV.CommonLib.UI.LoadingCircle;

namespace BlizzTV.CommonLib.Workload
{
    public class Workload // contains information about current workload progress of plugins so we can animate our progressbar on status strip
    {
        #region Instance

        private static Workload _intance = new Workload();
        public static Workload Instance { get { return _intance; } }

        #endregion

        private int _currentWorkload = 0;
        private ToolStripProgressBar _progressBar;
        private LoadingCircle _progressIcon;

        private Workload() { }

        public void AttachControls(ToolStripProgressBar progressBar,LoadingCircle progressIcon)
        {
            this._progressBar = progressBar;
            this._progressIcon = progressIcon;
        }

        public void Add(object sender, int units)
        {
            this._progressBar.Owner.AsyncInvokeHandler(() =>
            {
                this._currentWorkload += units;
                this._progressBar.Maximum += units;
                if (!this._progressBar.Visible) this._progressBar.Visible = true;
                if (!this._progressIcon.Visible)
                {
                    this._progressIcon.Visible = true;
                    this._progressIcon.Active = true;
                }
            });
        }

        public void Step(object sender)
        {
            this._progressBar.Owner.AsyncInvokeHandler(() =>
            {
                this._currentWorkload -= 1;
                if (this._currentWorkload > 0) this._progressBar.Value = this._progressBar.Maximum - this._currentWorkload;
                else
                {
                    this._progressBar.Visible = false;
                    this._progressIcon.Visible = false;
                    this._progressIcon.Active = false;
                    this._progressBar.Value = 0;
                    this._progressBar.Maximum = 0;
                }
            });
        }
    }
}
