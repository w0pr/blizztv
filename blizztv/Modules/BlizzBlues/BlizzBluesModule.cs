using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.ModuleLib;
using BlizzTV.Modules.BlizzBlues.Game;

namespace BlizzTV.Modules.BlizzBlues
{
    [ModuleAttributes("BlizzBlues", "Blizzard GM Blue post aggregator.", "blizzblues_16")]
    class BlizzBluesModule:Module
    {
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
            if (!this.Updating)
            {
                this.Updating = true;
                this.NotifyUpdateStarted();

                if (this.RootListItem.Childs.Count > 0) this.RootListItem.Childs.Clear();

                this.RootListItem.Style = ItemStyle.REGULAR;
                this.RootListItem.SetTitle("Updating BlizzBlues..");
                                
                this.RootListItem.Childs.Add("WOW",new WOWBlues());
                this.RootListItem.Childs.Add("SC2",new SCBlues());

                this.RootListItem.SetTitle("BlizzBlues");
                this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
                this.Updating = false;
            }
        }
    }
}
