/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
 *  
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU General 
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
 * implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>. 
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.UI;

namespace BlizzTV.Modules.Subscriptions.Catalog
{
    public partial class frmCatalog : Form
    {
        private readonly List<CatalogEntry> _entries; // the list of catalog entries.
        public bool AddedNewSubscriptions { get; private set; } // did user add a new subscription using the catalog?

        public frmCatalog(List<CatalogEntry> entries)
        {
            InitializeComponent();

            this.AddedNewSubscriptions = false;
            this._entries = entries;
            this.listViewCatalog.DoubleBuffer(); // prevent flickering while resizing and so.
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            this.ResizeColumnHeaders(); 
            this.LoadEntries();
        }

        private void LoadEntries(string filter="")
        {
            this.listViewCatalog.Items.Clear();
            filter = filter.ToLower();

            foreach (CatalogEntry entry in this._entries.Where(entry => 
                    filter.Trim() == "" || // if we have no filters set 
                    entry.Category.ToLower().IsLike(string.Format("*{0}*", filter)) || // category suits the filter
                    entry.Name.ToLower().IsLike(string.Format("*{0}*", filter)) || // name suits the filter
                    entry.Description.ToLower().IsLike(string.Format("*{0}*", filter)))) // description suits the filter
            {
                this.listViewCatalog.Items.Add(new CatalogEntryItem(entry));
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) txtFilter.Text = ""; // on escape-hit, reset the filter.
            this.LoadEntries(txtFilter.Text); // filter the entries live.
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            this.AddSubscription();
        }

        private void listViewCatalog_DoubleClick(object sender, EventArgs e)
        {
            this.AddSubscription();
        }

        private void AddSubscription()
        {
            if (this.listViewCatalog.SelectedItems.Count == 0) return;

            CatalogEntryItem selection = (CatalogEntryItem)this.listViewCatalog.SelectedItems[0];
            this.AddedNewSubscriptions = true;
            selection.ForeColor = Color.Gray;
            selection.Entry.AddAsSubscription();
        }

        private void frmCatalog_ResizeEnd(object sender, EventArgs e)
        {
            this.ResizeColumnHeaders();
        }

        private void ResizeColumnHeaders()
        {
            this.listViewCatalog.Columns[2].Width = this.listViewCatalog.Width - (this.listViewCatalog.Columns[0].Width + this.listViewCatalog.Columns[1].Width + 25);
        }

        private void linkLabelSuggest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.blizztv.com/topic/76-catalog-suggestions-feeds-streams-video-channels/");
        }

        private void listViewCatalog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listViewCatalog.ListViewItemSorter == null) this.listViewCatalog.ListViewItemSorter = new ListViewItemComparer(); // setup a new item comparer.
            ListViewItemComparer comparer = (ListViewItemComparer)this.listViewCatalog.ListViewItemSorter;
            
            if (e.Column != comparer.SortColumn) // if sort requested on a new column
            {
                comparer.SortColumn = e.Column; 
                comparer.SortOrder = SortOrder.Ascending;
            }
            else // if sort requested is on the same-last column
            {
                comparer.SortOrder = comparer.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending; // reverse the column ordering.
            }

            listViewCatalog.Sort(); 
        }

        private class CatalogEntryItem : ListViewItem
        {
            public CatalogEntry Entry { get; private set; }

            public CatalogEntryItem(CatalogEntry entry)
            {
                this.Entry = entry;
                this.Text = entry.Category;
                this.SubItems.Add(entry.Name);
                this.SubItems.Add(entry.Description);
            }
        }
    }
}
