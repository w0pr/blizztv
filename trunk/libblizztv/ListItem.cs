using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV
{
    public class ListItem
    {
        public string _title;
        public string Title { get { return this._title; } set { this._title = value; } }

        public ListItem() { }

        public void DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
