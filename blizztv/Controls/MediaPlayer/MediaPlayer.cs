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
using System.Windows.Forms;
using AxWMPLib;

namespace BlizzTV.Controls.MediaPlayer
{
    public class MediaPlayer: AxWindowsMediaPlayer
    {
        private const int WM_LBUTTONDBLCLK = 0x0203;
        private const int WM_RBUTTONDOWN = 0x0204;

        /// <summary>
        /// Fires on Double Click.
        /// </summary>
        public new event MouseEventHandler DoubleClick;

        public MediaPlayer() { }

        protected override void WndProc(ref Message m) // Override's the WndProc and disables wmplayer activex's default right-click menu and if one exists shows the attached ContextMenuStrip.
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDBLCLK:
                    if (this.DoubleClick != null) this.DoubleClick(this, new MouseEventArgs(MouseButtons.Left, 2, Cursor.Position.X, Cursor.Position.Y, 0));
                    m.Result = IntPtr.Zero; // don't allow actual wmplayer control process the message by flagging it as processed.
                    return;
                case WM_RBUTTONDOWN:
                    if (this.ContextMenuStrip != null) this.ContextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
                    m.Result = IntPtr.Zero; // don't allow actual wmplayer control process the message by flagging it as processed.
                    return;
            }

            base.WndProc(ref m); // when we're done with processing the events, let the message pass to others also too.
        }
    }
}
