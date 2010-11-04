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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LibBlizzTV;
using LibBlizzTV.Streams;
using LibBlizzTV.VideoChannels;

namespace BlizzTV
{
    public partial class frmMain : Form
    {
        LibBlizzTV.Streams.Streams Streams = new Streams();
        LibBlizzTV.VideoChannels.Channels VideoChannels = new Channels();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Streams.OnStreamsLoadCompleted += StreamsLoaded;
            Streams.Load();
            VideoChannels.Load();
            ChannelsLoaded();
        }

        private void ChannelsLoaded()
        {
            VideoChannels.Update();
            foreach (KeyValuePair<string, Channel> pair in VideoChannels.List)
            {
                ListItem item = new ListItem(pair.Value);
                item.ImageIndex = 0;
                List.Items.Add(item);
            }
        }

        private void StreamsLoaded(object sender, LoadStreamsCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Streams.OnStreamsUpdateCompleted += StreamsUpdated;
                Streams.OnStreamUpdateStep += StreamUpdateStep;
                Streams.Update();
                BeginInvoke(new MethodInvoker(delegate() { StreamsLoaded(sender, e); }));
            }
            else
            {
                LabelStatus.Text = "Updating streams..";
                ProgressStatus.Maximum = e.Count;
                ProgressStatus.Value = 0;
                ProgressStatus.Step = 1;
                ProgressStatus.Visible = true;
            }
        }

        private void StreamUpdateStep(object sender,NotifyStreamUpdateEventArgs e)
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { StreamUpdateStep(sender,e); }));
            else
            {
                ProgressStatus.PerformStep();
                ListItem item = new ListItem(e.Stream);
                item.ImageIndex = 0;
                List.Items.Add(item);
            }
        }

        private void StreamsUpdated(object sender, UpdateStreamsCompletedEventsArgs e)
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { StreamsUpdated(sender, e); }));
            else
            {
                LabelStatus.Text = "";
                ProgressStatus.Visible = false;
                ProgressStatus.Value = 0;
            }
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            ListItem selection = (ListItem)List.SelectedItems[0];
            selection.DoubleClick();
        }

        private void List_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {            
            switch(e.ColumnIndex)
            {
                case 0:
                    {
                        Rectangle Bounds = e.Bounds;
                        Bounds.Width = Bounds.Height = 16;
                        e.Graphics.DrawImage(this.GameIcons.Images[0], Bounds);
                        break;
                    }
                case 1:
                    {
                        Rectangle Bounds = e.Bounds;
                        Bounds.Width = Bounds.Height = 16;
                        e.Graphics.DrawImage(this.ListIcons.Images[0], Bounds);
                        break;
                    }
                case 2:
                case 3:
                    e.DrawDefault = true;
                    break;
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }
    }
}
