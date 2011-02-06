/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Windows.Forms;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.UI;

namespace BlizzTV.ModuleLib.Subscriptions
{
    public partial class frmCatalog : Form
    {
        private List<CatalogEntry> _entries;
        public bool AddedNewSubscriptions { get; private set; }

        public frmCatalog(List<CatalogEntry> entries)
        {
            InitializeComponent();            

            this.listViewCatalog.DoubleBuffer();            
            this.AddedNewSubscriptions = false;
            this._entries = entries;
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            this.ResizeColumnHeaders();
            this.LoadEntries();
        }

        private void LoadEntries(string filter="")
        {
            filter = filter.ToLower();
            this.listViewCatalog.Items.Clear();

            foreach (CatalogEntry entry in this._entries)
            {
                if (filter.Trim() == "" || entry.Category.ToLower().IsLike(string.Format("*{0}*", filter)) || entry.Name.ToLower().IsLike(string.Format("*{0}*", filter)) || entry.Description.ToLower().IsLike(string.Format("*{0}*", filter))) this.listViewCatalog.Items.Add(new CatalogEntryItem(entry));
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) txtFilter.Text = "";
            this.LoadEntries(txtFilter.Text);
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
            if (listViewCatalog.ListViewItemSorter == null) this.listViewCatalog.ListViewItemSorter = new ListViewItemComparer();
            ListViewItemComparer comparer = (ListViewItemComparer)this.listViewCatalog.ListViewItemSorter;
            
            if (e.Column != comparer.SortColumn) 
            {
                comparer.SortColumn = e.Column; 
                comparer.SortOrder = SortOrder.Ascending;
            }
            else
            {
                if (comparer.SortOrder == SortOrder.Ascending) comparer.SortOrder = SortOrder.Descending;
                else comparer.SortOrder = SortOrder.Ascending;
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
