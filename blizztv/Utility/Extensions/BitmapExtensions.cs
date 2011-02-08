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

using System.Drawing;
using System.Drawing.Imaging;

namespace BlizzTV.Utility.Extensions
{
    /// <summary>
    /// Provides bitmap extensions.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Converts a given bitmap to grayscale.
        /// </summary>
        /// <param name="bitmap">The input bitmap.</param>
        /// <returns>Returns the converted grayscale bitmap.</returns>
        public static Bitmap GrayScale(this Bitmap bitmap)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                // The colormatrix used for grayscale operation.
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                                              {
                                                                  new float[] {.3f, .3f, .3f, 0, 0},
                                                                  new float[] {.59f, .59f, .59f, 0, 0},
                                                                  new float[] {.11f, .11f, .11f, 0, 0},
                                                                  new float[] {0, 0, 0, 1, 0},
                                                                  new float[] {0, 0, 0, 0, 1}
                                                              });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);
            }

            return newBitmap;
        }
    }
}
