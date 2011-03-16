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
using System.Drawing;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.Utility.Imaging
{
    /// <summary>
    /// Provides a key-bitmap structure.
    /// </summary>
    public class NodeIcon : IDisposable
    {
        private Bitmap _normalImage;
        private readonly string _normalKey;
        private Bitmap _grayscaledImage;
        private readonly string _grayscaledKey;
        private ImageMode _imageMode;
        private bool _disposed = false;

        public Bitmap Image { get { return this._imageMode == ImageMode.Normal ? this._normalImage : this._grayscaledImage; } }
        public string Key { get { return this._imageMode == ImageMode.Normal ? this._normalKey : this._grayscaledKey; } }
        public ImageMode Mode { get { return this._imageMode; } set { this._imageMode = value; } }

        public NodeIcon(string key, Bitmap image)
        {
            this._normalImage = image;
            this._normalKey = key;
            this._imageMode = ImageMode.Normal;
            this._grayscaledImage = image.GrayScale();
            this._grayscaledKey = string.Format("{0}-grayscaled", key);
        }

        public enum ImageMode
        {
            Normal,
            GrayScaled
        }

        #region de-ctor

        // IDisposable pattern: http://msdn.microsoft.com/en-us/library/fs2xkftw%28VS.80%29.aspx

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Take object out the finalization queue to prevent finalization code for it from executing a second time.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return; // if already disposed, just return

            if (disposing) // only dispose managed resources if we're called from directly or in-directly from user code.
            {
                this._normalImage.Dispose();
                this._grayscaledImage.Dispose();
                this._normalImage = null;
                this._grayscaledImage = null;
            }

            _disposed = true;
        }

        ~NodeIcon() { Dispose(false); } // finalizer called by the runtime. we should only dispose unmanaged objects and should NOT reference managed ones.
        
        #endregion
    }
}
