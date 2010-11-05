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
            PluginManager pm = PluginManager.Instance;
            foreach (KeyValuePair<string,PluginInfo> pair in pm.Plugins)
            {
                Plugin Plugin = pair.Value.CreateInstance();
                Plugin.Update();
            }


            /*List.Groups.Add("streams", "Streams");
            List.Groups.Add("channels", "Video Channels");
            List.Groups.Add("feeds","Feeds");

            Streams.OnStreamsLoadCompleted += StreamsLoaded;
            Streams.Load();

            VideoChannels.Load();
            VideoChannels.Update();
            foreach (KeyValuePair<string, Channel> pair in VideoChannels.List)
            {
                ListItem item = new ListItem(pair.Value);
                item.ImageIndex = 0;
                item.Group = List.Groups["channels"];
                List.Items.Add(item);
            }  

            Feeds.Load();
            Feeds.Update();
            foreach (KeyValuePair<string, Feed> pair in Feeds.List)
            {
                foreach (Story story in pair.Value.Stories)
                {
                    ListItem item = new ListItem(story);
                    item.ImageIndex = 0;
                    item.Group = List.Groups["feeds"];
                    List.Items.Add(item);
                }
            }*/                  
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
                item.Group = List.Groups["streams"];
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
                        ListItem item = (ListItem)this.List.Items[e.ItemIndex];
                        Image item_image = null;
                        switch (item.ItemType)
                        {
                            case ListItem.ListItemType.Stream:
                                item_image = this.ListIcons.Images[0];
                                break;
                            case ListItem.ListItemType.VideoChannel:
                                item_image = this.ListIcons.Images[1];
                                break;
                            case ListItem.ListItemType.Story:
                                item_image = this.ListIcons.Images[2];
                                break;
                            default:
                                break;
                        }
                        if (item_image != null)
                        {
                            Rectangle Bounds = e.Bounds;
                            Bounds.Width = Bounds.Height = 16;
                            e.Graphics.DrawImage(item_image, Bounds);
                        }
                        break;                        
                    }
                case 1:
                    e.DrawDefault = true;
                    break;
                case 2:
                    {
                        Rectangle Bounds = e.Bounds;
                        Bounds.Width = Bounds.Height = 16;
                        e.Graphics.DrawImage(this.GameIcons.Images[0], Bounds);
                        break;
                    }
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }
    }
}
