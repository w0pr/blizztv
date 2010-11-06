using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV
{
    [Serializable]
    public class GlobalSettings
    {
        public ContentViewMethods ContentViewer = ContentViewMethods.INTERNAL_VIEWERS;
        public int VideoPlayerWidth = 640;
        public int VideoPlayerHeight = 385;
        public bool VideoAutoPlay = true;
    }

    public enum ContentViewMethods
    {
        INTERNAL_VIEWERS,
        DEFAULT_WEB_BROWSER
    }
}
