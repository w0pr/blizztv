using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LibVideoChannels;

namespace LibVideoChannels.Handlers
{
    public class YoutubeVideo:Video
    {
        private static Regex _regex = new Regex(@"http://www\.youtube\.com/watch\?v\=(.*)\&", RegexOptions.Compiled); // compiled regex for reading video id's.

        public YoutubeVideo(string Title, string Guid, string Link, string Provider)
            : base(Title, Guid, Link, Provider)
        {
            Match m = _regex.Match(this.Link);
            if (m.Success) this.VideoID = m.Groups[1].Value;
        }
    }
}
