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
using System.Drawing;
using System.Windows.Forms;
using BlizzTV.Settings;

namespace BlizzTV.Modules.Players
{
    /// <summary>
    /// Provides a base framework for player windows.
    /// </summary>
    public partial class BasePlayerForm : Form
    {
        private bool _borderless = false; // are we in borderless mode?
        private bool _dragging = false; // are we currently being dragged?
        private Point _dragMouseOffset;

        protected BasePlayerForm()
        {
            InitializeComponent();

            this.Size = new Size(GlobalSettings.Instance.VideoPlayerWidth, GlobalSettings.Instance.VideoPlayerHeight); // set the window size & location based on last-known values.
        }

        protected void LoadingStarted()
        {
            this.LoadingAnimation.Visible = true;
            this.LoadingAnimation.Active = true;
            this.LoadingAnimation.BringToFront();
        }

        protected void LoadingFinished()
        {
            if (this.LoadingAnimation == null) return;

            this.LoadingAnimation.SendToBack();
            this.LoadingAnimation.Active = false;           
            this.LoadingAnimation.Visible = false;
            this.Controls.Remove(this.LoadingAnimation);
            this.LoadingAnimation.Dispose();
            this.LoadingAnimation = null;
        }

        protected void SwitchBorderlessMode(object sender, MouseEventArgs e)
        {
            this.SwitchBorderlessMode();
        }

        protected void SwitchBorderlessMode(object sender, EventArgs e)
        {
            this.SwitchBorderlessMode();
        }

        private void SwitchBorderlessMode()
        {
            this._dragging = false;

            if (this._borderless)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this._borderless = false;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this._borderless = true;
            } 
        }

        protected void FormDragStart(object sender, MouseEventArgs e)
        {
            if (!this._borderless) return;

            this._dragMouseOffset = new Point(e.X - this.Location.X, e.Y - this.Location.Y); // remember the offset of the mouse when drag starts.
            this.Cursor = Cursors.SizeAll;
            this._dragging = true;
        }

        protected void FormDragEnd(object sender, MouseEventArgs e)
        {
            this._dragging = false;
            this.Cursor = Cursors.Default;
        }

        protected void FormDrag(object sender, MouseEventArgs e)
        {
            if (this._borderless && this._dragging) this.Location = new Point(e.X - this._dragMouseOffset.X, e.Y - this._dragMouseOffset.Y); // moves the dragged window with keeping the mouse offset in mind.
        }

        private void PlayerWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width == GlobalSettings.Instance.VideoPlayerWidth && this.Size.Height == GlobalSettings.Instance.VideoPlayerHeight) return; 

            GlobalSettings.Instance.VideoPlayerWidth = this.Size.Width;
            GlobalSettings.Instance.VideoPlayerHeight = this.Size.Height;
            GlobalSettings.Instance.Save();
        }
    }
}
