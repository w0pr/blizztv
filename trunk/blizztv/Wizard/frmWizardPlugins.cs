using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlizzTV.Wizard
{
    public partial class frmWizardPlugins : Form , IWizardForm
    {
        public frmWizardPlugins()
        {
            InitializeComponent();
        }

        private void frmWizardPlugins_Load(object sender, EventArgs e) { }

        public void Finish()
        {
            if (CheckboxFeeds.Checked) Settings.Instance.Plugins.Enable("Feeds"); else Settings.Instance.Plugins.Disable("Feeds");
            if (CheckboxStreams.Checked) Settings.Instance.Plugins.Enable("Streams"); else Settings.Instance.Plugins.Disable("Streams");
            if (CheckboxVideos.Checked) Settings.Instance.Plugins.Enable("Videos"); else Settings.Instance.Plugins.Disable("Videos");
            if (CheckboxEvents.Checked) Settings.Instance.Plugins.Enable("Events"); else Settings.Instance.Plugins.Disable("Events");
            Settings.Instance.NeedInitialConfig = false;
            Settings.Instance.Save();
        }
    }
}
