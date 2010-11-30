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
 * $Id: FlashPlayer.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Windows.Forms;

namespace BlizzTV.Module.Players
{
    // latest flash player activex can be downloaded from: http://fpdownload.adobe.com/get/flashplayer/current/install_flash_player_ax.exe

    /// <summary>
    /// Customized Flash Player.
    /// </summary>
    public class FlashPlayer : AxShockwaveFlashObjects.AxShockwaveFlash
    {
        private const int WM_RBUTTONDOWN = 0x0204;

        /// <summary>
        /// Initiates a new Flash Player object.
        /// </summary>
        public FlashPlayer()
            : base()
        {
            this.HandleCreated += new EventHandler(FlashPlayer_HandleCreated);
        }

        void FlashPlayer_HandleCreated(object sender, EventArgs e)
        {
            this.AllowFullScreen = "true";
            this.AllowNetworking = "all";
            this.AllowScriptAccess = "always";
        }

        /// <summary>
        /// Override's the WndProc and disables Flash activex's default right-click menu and if exists shows the attached ContextMenuStrip.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_RBUTTONDOWN)
            {
                if (this.ContextMenuStrip != null) this.ContextMenuStrip.Show(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
