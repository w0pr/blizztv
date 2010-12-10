using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.ModuleLib;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class BlueStory:ListItem
    {
        public Region Region { get; private set; }
        public string Link { get; private set; }

        public BlueStory(string title, Region region, string link):base(title)
        {
            this.Region = region;
            this.Link = link;
        }

        public override void DoubleClicked(object sender, System.EventArgs e)
        {
            this.Navigate();
        }

        public override void NotificationClicked()
        {
            this.Navigate();
        }

        private void Navigate()
        {
            System.Diagnostics.Process.Start(this.Link, null);
        }
    }
}
