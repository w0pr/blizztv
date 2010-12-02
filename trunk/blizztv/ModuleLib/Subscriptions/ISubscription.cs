using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.ModuleLib.Subscriptions
{
    [Serializable]
    public class ISubscription
    {
        public string Key { get; private set; }

        public ISubscription(string key)
        {
            this.Key = key;
        }
    }
}
