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
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibBlizzTV;
using LibBlizzTV.Streams;

namespace BlizzTV
{
    public partial class frmMain : Form
    {
        Dictionary<string, Stream> Streams;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadStreams();
        }

        private void StageList_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selection = StageList.SelectedItems[0];
            StreamPlayer p = new StreamPlayer(LibBlizzTV.Streams.Streams.Instance.List[selection.Text]);
            p.Show();
        }

        private void LoadStreams()
        {
            foreach (KeyValuePair<string, Stream> pair in LibBlizzTV.Streams.Streams.Instance.List)
            {
                StageList.Items.Add(pair.Key);
            }
        }
    }
}
