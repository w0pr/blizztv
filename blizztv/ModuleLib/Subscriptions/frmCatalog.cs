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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.CommonLib.UI;

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
            foreach (CatalogEntry entry in this._entries)
            {
                this.listViewCatalog.Items.Add(new CatalogEntryItem(entry));
            }

            this.listViewCatalog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void frmCatalog_ResizeEnd(object sender, EventArgs e)
        {
            this.listViewCatalog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void linkLabelSuggest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.blizztv.com/topic/76-catalog-suggestions-feeds-streams-video-channels/");
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
