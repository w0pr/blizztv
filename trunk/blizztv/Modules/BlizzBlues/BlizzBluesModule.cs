using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.ModuleLib;
using BlizzTV.Modules.BlizzBlues.Game;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.BlizzBlues
{
    [ModuleAttributes("BlizzBlues", "Blizzard GM Blue post aggregator.", "blizzblues_16")]
    class BlizzBluesModule:Module
    {
        internal Dictionary<string, BlueParser> _parsers = new Dictionary<string, BlueParser>(); 

        public BlizzBluesModule()
        {
            this.RootListItem=new ListItem("BlizzBlues");
        }

        public override void Run()
        {
            this.UpdateBlues();
        }

        private void UpdateBlues()
        {
            if (this.Updating) return;


            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._parsers.Count > 0)
            {
                this._parsers.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.Style = ItemStyle.Regular;
            this.RootListItem.SetTitle("Updating BlizzBlues..");

            WOWBlues wow = new WOWBlues();
            this._parsers.Add("wow", wow);
            wow.OnStyleChange += ChildStyleChange;

            SCBlues sc = new SCBlues();
            this._parsers.Add("sc", sc);
            sc.OnStyleChange += ChildStyleChange;

            Workload.Instance.Add(this, this._parsers.Count);

            foreach (KeyValuePair<string, BlueParser> pair in this._parsers)
            {
                pair.Value.Update();
                this.RootListItem.Childs.Add(pair.Key, pair.Value);
                foreach (KeyValuePair<string, BlueStory> storyPair in pair.Value.Stories)
                {
                    pair.Value.Childs.Add(storyPair.Key, storyPair.Value);
                    if (storyPair.Value.More.Count > 0)
                    {
                        foreach (KeyValuePair<string, BlueStory> postPair in storyPair.Value.More)
                        {
                            storyPair.Value.Childs.Add(string.Format("{0}-{1}", postPair.Value.TopicId, postPair.Value.PostId),postPair.Value);
                        }
                    }
                }
                Workload.Instance.Step(this);
            }

            this.RootListItem.SetTitle("BlizzBlues");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        void ChildStyleChange(ItemStyle style)
        {
            throw new NotImplementedException();
        }
    }
}
