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
using System.Collections;
using System.Windows.Forms;

namespace BlizzTV.Utility.UI
{
    /// <summary>
    /// Provides item comparer for list-views.
    /// </summary>
    public class ListViewItemComparer : IComparer
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        public int SortColumn { get; set; } 

        /// <summary>
        /// Sort order to be used.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        public ListViewItemComparer()
        {
            SortColumn = 0;
            SortOrder = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            SortColumn = column;
            this.SortOrder = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = string.Compare(((ListViewItem)x).SubItems[SortColumn].Text, ((ListViewItem)y).SubItems[SortColumn].Text); // Determine whether the sort order is descending.                
            if (SortOrder == SortOrder.Descending) returnVal *= -1; // Invert the value returned by string.Compare.                    
            return returnVal;
        }
    }
}
