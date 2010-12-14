using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using BlizzTV.CommonLib.Settings;
using BlizzTV.CommonLib.Workload;
using BlizzTV.ModuleLib;
using BlizzTV.Modules.BlizzBlues.Game;

namespace BlizzTV.Modules.BlizzBlues
{
    [ModuleAttributes("BlizzBlues", "Blizzard GM Blue post aggregator.", "blizzblues_16")]
    class BlizzBluesModule:Module
    {
        internal List<BlueParser> _parsers = new List<BlueParser>();
        private Timer _updateTimer;

        public static BlizzBluesModule Instance;

        public BlizzBluesModule()
        {
            BlizzBluesModule.Instance = this;
            this.RootListItem=new ListItem("BlizzBlues");

            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Feeds", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Read", null, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unread", null, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.            
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();            
        }

        public override void Run()
        {
            this.UpdateBlues();
            this.SetupUpdateTimer();
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

            if (Settings.Instance.TrackWorldofWarcraft)
            {
                WOWBlues wow = new WOWBlues();
                this._parsers.Add(wow);
                wow.OnStyleChange += ChildStyleChange;
            }

            if (Settings.Instance.TrackStarcraft)
            {
                SCBlues sc = new SCBlues();
                this._parsers.Add(sc);
                sc.OnStyleChange += ChildStyleChange;
            }

            if (this._parsers.Count > 0)
            {
                Workload.Instance.Add(this, this._parsers.Count);

                foreach (BlueParser parser in this._parsers)
                {
                    parser.Update();
                    this.RootListItem.Childs.Add(parser.Title, parser);
                    foreach (KeyValuePair<string, BlueStory> storyPair in parser.Stories)
                    {
                        parser.Childs.Add(storyPair.Key, storyPair.Value);
                        if (storyPair.Value.More.Count > 0)
                        {
                            foreach (KeyValuePair<string, BlueStory> postPair in storyPair.Value.More)
                            {
                                storyPair.Value.Childs.Add(string.Format("{0}-{1}", postPair.Value.TopicId, postPair.Value.PostId), postPair.Value);
                            }
                        }
                    }
                    Workload.Instance.Step(this);
                }
            }

            this.RootListItem.SetTitle("BlizzBlues");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        void ChildStyleChange(ItemStyle style)
        {
            if (this.RootListItem.Style == style) return;

            int unread = this._parsers.Count(parser => parser.Style == ItemStyle.Bold);
            this.RootListItem.Style = unread > 0 ? ItemStyle.Bold : ItemStyle.Regular;
        }


        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (BlueParser parser in this._parsers)
            {
                foreach (KeyValuePair<string, BlueStory> pair in parser.Stories)
                {
                    pair.Value.Status = BlueStory.Statutes.Read;
                }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (BlueParser parser in this._parsers)
            {
                foreach (KeyValuePair<string, BlueStory> pair in parser.Stories)
                {
                    pair.Value.Status = BlueStory.Statutes.Unread;
                }
            }
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(this.UpdateBlues) { IsBackground = true };
            thread.Start();
        }

        public void OnSaveSettings()
        {
            this.SetupUpdateTimer();
        }

        private void SetupUpdateTimer()
        {
            if (this._updateTimer != null)
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer = null;
            }

            _updateTimer = new Timer(Settings.Instance.UpdateEveryXMinutes * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode) this.UpdateBlues();
        }
    }
}
