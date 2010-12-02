using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.ModuleLib.Subscriptions;

namespace BlizzTV.Modules.Streams
{
    internal class StreamSubscription : ISubscription
    {
        private string _name;
        private string _slug;

        public string Name { get { return this._name; } }
        public string Slug { get { return this._slug; } }

        public StreamSubscription(string name, string slug):base(name)
        {
            this._name = name;
            this._slug = slug;
        }
    }
}
