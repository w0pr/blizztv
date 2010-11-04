using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV.Streams;

namespace BlizzTV
{
    public sealed class ListItem : ListViewItem
    {
        public enum ListItemType
        {
            Stream
        }

        private object _storage = null;
        private ListItemType _item_type;

        public ListItemType ItemType { get { return this._item_type; } }

        public ListItem(Stream stream)
        {
            this._item_type = ListItemType.Stream;
            this._storage = stream;
            this.SubItems.Add(new ListViewSubItem());
            this.SubItems.Add("Stream: " + stream.Name);
            this.SubItems.Add(stream.ViewerCount.ToString() + " viewers");            
        }

        public object GetObject()
        {
            return this._storage;
        }

        public void DoubleClick()
        {
            switch (this._item_type)
            {
                case ListItemType.Stream:
                    {
                        StreamPlayer p = new StreamPlayer((Stream)this._storage);
                        p.Show();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
