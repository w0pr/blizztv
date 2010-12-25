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
using System.Diagnostics;
using System.Windows.Forms;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Downloads;

namespace BlizzTV.CommonLib.Dependencies
{
    public class Dependencies
    {
        #region Instance
        
        private static Dependencies _instance = new Dependencies();
        public static Dependencies Instance { get { return _instance; } }
        
        #endregion

        private Dependencies() { }

        public bool Satisfied()
        {
            if (CheckShockwaveFlash())
            {
                System.Windows.Forms.DialogResult result = MessageBox.Show("BlizzTV requires Abode Flash Player for best user-experience. Do you want to install latest Adobe Flash Player now?", "Missing component: Adobe Flash Player", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    frmDownload f = new frmDownload("Downloading latest Adobe Flash player");
                    f.StartDownload(new Download("http://fpdownload.adobe.com/get/flashplayer/current/install_flash_player_ax.exe", "install_flash_player_ax.exe"));
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        MessageBox.Show("The application will now exit to continue installation of Adobe Flash Player. Please restart BlizzTV after installation finishes.", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("install_flash_player_ax.exe");                        
                    }
                    else
                    {
                        MessageBox.Show("Adobe Flash Player download failed. Please install it manually.", "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Process.Start("IExplore.exe", "http://get.adobe.com/flashplayer/");
                    }
                    return false;
                }
            }
            return true;
        }

        private bool CheckShockwaveFlash()
        {
            bool satisfied = true;            
            try
            {
                Activator.CreateInstance(Type.GetTypeFromProgID("ShockwaveFlash.ShockwaveFlash"));
            }
            catch (Exception e)
            {
                satisfied = false;
                Log.Instance.Write(LogMessageTypes.Error, string.Format("CheckShockwaveFlash Exception: {0}", e));
            }
            return satisfied;
        }
    }       
}
