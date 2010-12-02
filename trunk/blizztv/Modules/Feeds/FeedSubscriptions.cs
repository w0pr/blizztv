using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.ModuleLib.Subscriptions;

namespace BlizzTV.Modules.Feeds
{
    public sealed class FeedSubscriptions:Subscriptions
    {
        private static FeedSubscriptions _instance = new FeedSubscriptions();
        public static FeedSubscriptions Instance { get { return _instance; } }

        private FeedSubscriptions():base("feeds")
        {
        }
    }

    [Serializable]
    public class FeedSubscription : ISubscription
    {
        private string _name;
        private string _url;

        public string Name { get { return this._name; } }
        public string Url { get { return this._url; } }

        public FeedSubscription(string name, string url)
            : base(name)
        {
            this._name = name;
            this._url = url;
        }
    }
}
