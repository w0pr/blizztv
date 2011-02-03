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
using BlizzTV.Assets.i18n;
using BlizzTV.Audio;
using BlizzTV.Downloads;
using BlizzTV.Log;

namespace BlizzTV.Dependency
{
    public class DependencyManager
    {
        #region Instance
        
        private static DependencyManager _instance = new DependencyManager();
        public static DependencyManager Instance { get { return _instance; } }
        
        #endregion

        private DependencyManager() { }

        public bool Satisfied() // checks all depedency-rules for if they're satisfied.
        {
            if (!this.ShockwaveFlashInstalled()) // if adobe flash player is not installed, ask it for.
            {
                DialogResult result = MessageBox.Show(i18n.FlashPlayerRequiredMessage, i18n.FlashPlayerRequiredTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    frmDownload f = new frmDownload(i18n.DownloadingAdobeFlashPlayer);
                    f.StartDownload(new Download("http://fpdownload.adobe.com/get/flashplayer/current/install_flash_player_ax.exe", "install_flash_player_ax.exe"));
                    if (f.ShowDialog() == DialogResult.OK) // if download succeeed
                    {
                        MessageBox.Show(i18n.FlashPlayerWillBeInstalledMessage, i18n.DownloadComplete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("install_flash_player_ax.exe");                        
                    }
                    else // if download failed
                    {
                        MessageBox.Show(i18n.FlashPlayerDownloadFailedMessage, i18n.DownloadFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Process.Start("IExplore.exe", "http://get.adobe.com/flashplayer/");
                    }                    
                }
                return false; // rule is not satisfied.
            }
            if (!this.VisualCpp2010RuntimeInstalled()) // if visual c++ 2010 redistributable package is not installed, ask for it as it's required by audio-engine irrKlang.
            {
                DialogResult result = MessageBox.Show(i18n.VisualCPP2010RequiredMessage, i18n.VisualCPP2010RequiredTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    frmDownload f = new frmDownload(i18n.DownloadingVisualCPP2010);
                    f.StartDownload(new Download("http://download.microsoft.com/download/5/B/C/5BC5DBB3-652D-4DCE-B14A-475AB85EEF6E/vcredist_x86.exe", "vcredist_x86.exe"));
                    if (f.ShowDialog() == DialogResult.OK) // if download succeeded
                    {
                        MessageBox.Show(i18n.VisualCPP2010WillBeInstalledMessage, i18n.DownloadComplete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("vcredist_x86.exe");
                    }
                    else // if download failed
                    {
                        MessageBox.Show(i18n.VisualCPP2010DownloadFailedMessage, i18n.DownloadFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Process.Start("IExplore.exe", "http://www.microsoft.com/downloads/en/details.aspx?familyid=A7B7A05E-6DE6-4D3A-A423-37BF0912DB84&displaylang=en");
                    }                    
                }
                return false;
            }
            return true;
        }

        private bool VisualCpp2010RuntimeInstalled() // check Visual C++ 2010 runtime package
        {
            AudioManager player = AudioManager.Instance;
            if (player.EngineStatus == AudioManager.AudioEngineStatus.MissingDependency) // if AudioManager reports a missing dependency, let the rule fail.
            {
                LogManager.Instance.Write(LogMessageTypes.Error, "Dependency rule VisualCpp2010RuntimeInstalled failed. Visual C++ 2010 redistributable package is not installed.");
                return false;
            }
            return true;            
        }

        private bool ShockwaveFlashInstalled() // check Adobe Flash Player
        {            
            try
            {
                Activator.CreateInstance(Type.GetTypeFromProgID("ShockwaveFlash.ShockwaveFlash")); // try activating flash-player by type.
                return true;
            }
            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Depedency rule ShockwaveFlashInstalled() failed. Adobe Flash Player is not installed: {0}", e));
                return false;
            }
        }
    }       
}
