using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibVideos.Handlers
{
    public class BlipTVVideo:Video
    {
        public BlipTVVideo(string Title, string Guid, string Link, string Provider)
            : base(Title, Guid, Link, Provider) { }
    }
}
