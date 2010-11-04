using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibBlizzTV;
using LibBlizzTV.Streams;

namespace BlizzTV
{
    public partial class frmMain : Form
    {
        Dictionary<string, Stream> Streams;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadStreams();
        }

        private void StageList_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selection = StageList.SelectedItems[0];
            StreamPlayer p = new StreamPlayer(LibBlizzTV.Streams.Streams.Instance.List[selection.Text]);
            p.Show();
        }

        private void LoadStreams()
        {
            foreach (KeyValuePair<string, Stream> pair in LibBlizzTV.Streams.Streams.Instance.List)
            {
                StageList.Items.Add(pair.Key);
            }
        }
    }
}
