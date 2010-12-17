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

// latest flash player activex can be downloaded from: http://fpdownload.adobe.com/get/flashplayer/current/install_flash_player_ax.exe

using System;
using System.Windows.Forms;

namespace BlizzTV.ModuleLib.Players
{    
    public class FlashPlayer : AxShockwaveFlashObjects.AxShockwaveFlash // Customized Flash Player.
    {
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONDBLCLK = 0x0203; 
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;                      
        
        public new event MouseEventHandler DoubleClick;
        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public new event MouseEventHandler MouseMove;

        public FlashPlayer():base()
        {
            this.HandleCreated += FlashPlayer_HandleCreated;
        }

        void FlashPlayer_HandleCreated(object sender, EventArgs e)
        {
            this.AllowFullScreen = "true";
            this.AllowNetworking = "all";
            this.AllowScriptAccess = "always";
        }

        protected override void WndProc(ref Message m) // Override's the WndProc and disables Flash activex's default right-click menu and if exists shows the attached ContextMenuStrip.
        {
            if (m.Msg == WM_LBUTTONDOWN)
            {
                if (this.MouseDown != null) this.MouseDown(this, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, Cursor.Position.X, Cursor.Position.Y, 0));
            }
            else if (m.Msg == WM_LBUTTONUP)
            {
                if (this.MouseUp != null) this.MouseUp(this, new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0));
            }
            else if (m.Msg == WM_MOUSEMOVE)
            {
                if (this.MouseMove != null) this.MouseMove(this, new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0));
            }
            else if (m.Msg == WM_RBUTTONDOWN)
            {
                if (this.ContextMenuStrip != null) this.ContextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
                m.Result = IntPtr.Zero;
                return;
            }
            else if (m.Msg == WM_LBUTTONDBLCLK)
            {
                if (this.DoubleClick != null) this.DoubleClick(this, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 2, Cursor.Position.X, Cursor.Position.Y, 0));
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
