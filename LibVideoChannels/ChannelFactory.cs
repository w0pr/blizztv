using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibVideoChannels.Handlers;

namespace LibVideoChannels
{
    public static class ChannelFactory
    {
        public static Channel CreateChannel(string Name, string Slug, string Provider)
        {
            Channel _channel = null;
            switch (Provider.ToLower())
            {
                case "youtube":
                    _channel = new Youtube(Name, Slug, Provider);
                    break;
                case "bliptv":
                    _channel = new BlipTV(Name, Slug, Provider);
                    break;
                default:
                    break;
            }

            if (_channel != null) return _channel;
            else throw new NotImplementedException(string.Format("Video channel provider not implemented: '{0}'", Provider)); // throw an exception if video channel was not associated with a valid provider.
        }
    }
}
