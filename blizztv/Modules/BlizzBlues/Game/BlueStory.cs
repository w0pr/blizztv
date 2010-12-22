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
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.CommonLib.Notifications;
using BlizzTV.CommonLib.Utils;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class BlueStory:ListItem
    {
        private Statutes _status = Statutes.Unknown;

        public Region Region { get; private set; }
        public string Link { get; private set; }
        public string TopicId { get; private set; }
        public string PostId { get; private set; }
        public Dictionary<string, BlueStory> More = new Dictionary<string, BlueStory>();

        public Statutes Status
        {
            get
            {
                if (this._status == Statutes.Unknown)
                {
                    if (!StatusStorage.Instance.Exists(string.Format("bluestory.{0}-{1}", this.TopicId,this.PostId))) this.Status = Statutes.Fresh;
                    else
                    {
                        this._status = (Statutes)StatusStorage.Instance[string.Format("bluestory.{0}-{1}", this.TopicId, this.PostId)];
                        if (this._status == Statutes.Fresh) this.Status = Statutes.Unread;
                        else if (this._status == Statutes.Unread) this.Style = ItemStyle.Bold;
                    }
                }
                else
                {
                    switch (this._status)
                    {
                        case Statutes.Fresh:
                        case Statutes.Unread:
                            if (this.Style != ItemStyle.Bold) this.Style = ItemStyle.Bold;
                            break;
                        case Statutes.Read:
                            if (this.Style != ItemStyle.Regular) this.Style = ItemStyle.Regular;
                            break;
                    }
                }
                return this._status;
            }
            set
            {
                this._status = value;
                StatusStorage.Instance[string.Format("bluestory.{0}-{1}", this.TopicId, this.PostId)] = (byte)this._status;
                switch (this._status)
                {
                    case Statutes.Fresh:
                    case Statutes.Unread:
                        this.Style = ItemStyle.Bold;
                        break;
                    case Statutes.Read:
                        this.Style = ItemStyle.Regular;
                        break;
                    default:
                        break;
                }
            }
        }

        public BlueStory(string title, Region region, string link, string topicId, string postId)
            : base(title)
        {
            this.Region = region;
            this.Link = link;
            this.TopicId = topicId;
            this.PostId = postId;

            // register context menus.
            this.ContextMenus.Add("markasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.                            

            if (this.Region == Game.Region.Eu)
                this.Icon = new NamedImage("eu_16", Assets.Images.Icons.Png._16.eu);
            else if (this.Region == Game.Region.Us)
                this.Icon = new NamedImage("us_16", Assets.Images.Icons.Png._16.us);
        }

        public void AddPost(BlueStory blueStory)
        {
            this.More.Add(blueStory.PostId,blueStory);
            blueStory.OnStyleChange+=ChildStyleChange;
        }

        void  ChildStyleChange(ItemStyle style)
        {
            if (this.Style == style) return;           
            int unread = this.More.Count(pair => pair.Value.Style == ItemStyle.Bold);
            this.Style = unread > 0 ? ItemStyle.Bold : ItemStyle.Regular;
        }

        public void CheckForNotifications()
        {
            if (this.Status == Statutes.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(string.Format("BlizzBlue: {0}",this.Title), "Click to read.", System.Windows.Forms.ToolTipIcon.Info));
        }

        public override void DoubleClicked(object sender, System.EventArgs e)
        {
            this.Navigate();
        }

        public override void NotificationClicked()
        {
            this.Navigate();
        }

        private void Navigate()
        {
            System.Diagnostics.Process.Start(this.Link, null);
            if (this.Status != Statutes.Read) this.Status = Statutes.Read;
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.Read; // set the story state as read.
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.Unread; // set the story state as unread.
        }

        public enum Statutes
        {
            Unknown,
            Fresh,
            Unread,
            Read
        }
    }
}
