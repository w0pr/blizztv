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
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using BlizzTV.InfraStructure.Modules.Subscriptions.Providers;

namespace BlizzTV.EmbeddedModules.Streams
{
    public sealed class Providers : ProvidersHandler
    {
        #region instance

        private static Providers _instance = new Providers();
        public static Providers Instance { get { return _instance; } }

        #endregion

        private Providers() : base(typeof(StreamProvider)) { }
    }

    [Serializable]
    [XmlType("Stream")]
    public class StreamProvider : Provider
    {
        [XmlAttribute("Movie")]
        public string Movie { get; set; }

        [XmlAttribute("Player")]
        public string Player { get; set; }

        [XmlAttribute("ChatMovie")]
        public string ChatMovie { get; set; }

        [XmlAttribute("URLRegEx")]
        public string URLRegEx { get; set; }

        [XmlAttribute("URLHint")]
        public string URLHint { get; set; }

        [XmlIgnoreAttribute]
        public bool ChatAvailable { get { return ChatMovie != null; }
        }

        public bool LinkValid(string link)
        {
            Regex regex = new Regex(this.URLRegEx, RegexOptions.Compiled);
            Match m = regex.Match(link);
            return m.Success;
        }

        public string GetSlug(string link)
        {
            if (!this.LinkValid(link)) return null;
            Regex regex = new Regex(this.URLRegEx, RegexOptions.Compiled);
            Match m = regex.Match(link);
            return m.Groups["Slug"].Value;
        }
    }
}
