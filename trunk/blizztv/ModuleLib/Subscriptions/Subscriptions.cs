using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.CommonLib.Utils;

namespace BlizzTV.ModuleLib.Subscriptions
{
    public class Subscriptions
    {
        private string _module;

        public Subscriptions(string module)
        {
            this._module = module;
        }

        public bool Exists(ISubscription subscription)
        {
            return Storage.Instance.Dictionary.ContainsKey(string.Format("{0}.{1}", this._module, subscription.Key));
        }

        public bool Exists(string key)
        {
            return Storage.Instance.Dictionary.ContainsKey(string.Format("{0}.{1}", this._module, key));
        }

        public bool AddSubscription(ISubscription subscription)
        {
            if (this.Exists(subscription)) return false;
            else
            {
                //Storage.Instance.Dictionary[string.Format("{0}.{1}", this._module, subscription.Key)] = subscription;
                Storage.Instance.Dictionary.Flush();
                return true;
            }
        }

        public ISubscription GetSubscription(string key)
        {
            return null;
            if (!this.Exists(key)) return null;
            //else return Storage.Instance.Dictionary[string.Format("{0}.{1}", this._module, key)];
        }

        public bool Delete(ISubscription subscription)
        {
            if (this.Exists(subscription)) return false;
            else return Storage.Instance.Dictionary.Remove(string.Format("{0}.{1}", this._module, subscription.Key));
        }

        public void Dictionary()
        {
            foreach (KeyValuePair<string, string> pair in Storage.Instance.Dictionary)
            {

            }
        }
    }
}
