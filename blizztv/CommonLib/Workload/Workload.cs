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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.UILib;

namespace BlizzTV.CommonLib.Workload
{
    public class Workload // contains information about current workload progress of plugins so we can animate our progressbar on status strip
    {
        private static Workload _intance = new Workload();
        private ToolStripProgressBar _progress_bar;
        private int _workload = 0;
        private int _maximum_workload = 0;

        public static Workload Instance { get { return _intance; } }

        private Workload() { }

        public void SetProgressBar(ToolStripProgressBar ProgressBar)
        {
            this._progress_bar = ProgressBar;
        }

        public void Add(object sender, int units)
        {
            this._progress_bar.Owner.AsyncInvokeHandler(() =>
            {
                this._maximum_workload += units;
                this._workload += units;
                this._progress_bar.Visible = true;
                this._progress_bar.Maximum = this._maximum_workload += units;
            });
        }

        public void Step(object sender)
        {
            this._progress_bar.Owner.AsyncInvokeHandler(() =>
            {
                this._workload -= 1;
                this._progress_bar.Value = (this._maximum_workload - this._workload);
                if (this._workload == 0)
                {
                    this._progress_bar.Visible = false;
                    this._progress_bar.Value = 0;
                    this._maximum_workload = 0;
                }
            });
        }
    }
}
