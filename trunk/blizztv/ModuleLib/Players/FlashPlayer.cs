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
        private const int WM_RBUTTONDOWN = 0x0204;

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
            if (m.Msg == WM_RBUTTONDOWN)
            {
                if (this.ContextMenuStrip != null) this.ContextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
