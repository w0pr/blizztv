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

using System.IO;
using System.Windows.Forms;

namespace BlizzTV.Utility.Helpers
{
    /// <summary>
    /// Provides helper functions for application related functionality.
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// Returns the full path for asked file or folder that resides in application directory.
        /// </summary>
        /// <param name="resourceName">Asked file or folder.</param>
        /// <returns>Returns full path of asked file or folder that resides in application directory</returns>
        public static string GetResourcePath(string resourceName)
        {
            return string.Format("{0}\\{1}", Path.GetDirectoryName(Application.ExecutablePath), resourceName);
        }
    }
}
