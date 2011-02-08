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

namespace BlizzTV.Modules.Subscriptions.Providers
{
    public class ProvidersHandler
    {
        private readonly Type _type; // the bound provider-type.

        /// <summary>
        /// Dictionary of known providers.
        /// </summary>
        public Dictionary<string, Provider> Dictionary { get { return ProvidersStorage.Instance.GetProviders(this._type); } } 

        protected ProvidersHandler(Type type)
        {
            this._type = type;
        }        
    }
}
