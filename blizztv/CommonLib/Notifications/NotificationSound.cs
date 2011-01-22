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

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using BlizzTV.CommonLib.Settings;
using BlizzTV.Audio;

namespace BlizzTV.CommonLib.Notifications
{
    public sealed class NotificationSound
    {
        #region instance

        private static NotificationSound _instance = new NotificationSound();
        public static NotificationSound Instance { get { return _instance; } }

        #endregion

        private List<string> _names = new List<string>();

        public List<string> Names { get { return this._names; } }

        private NotificationSound()
        {
            ResourceSet resourceset = Assets.Sounds.Notifications.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceset) { this._names.Add(entry.Key.ToString()); }
        }

        public void Play(string name = "")
        {
            if (name == "") name = GlobalSettings.Instance.NotificationSound;
            AudioManager.Instance.PlayFromMemory(name, (byte[])Assets.Sounds.Notifications.ResourceManager.GetObject(name));
        }
    }
}
