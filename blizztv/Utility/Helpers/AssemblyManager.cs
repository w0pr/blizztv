﻿/*    
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
 * $Id: Program.cs 369 2011-02-10 21:23:43Z shalafiraistlin@gmail.com $
 */

using System;
using System.Reflection;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Log;

namespace BlizzTV.Utility.Helpers
{
    public sealed class AssemblyManager
    {
        #region Instance

        private static AssemblyManager _instance = new AssemblyManager();
        public static AssemblyManager Instance { get { return _instance; } }

        #endregion

        public Assembly Resolver(object sender, ResolveEventArgs args)
        {
            lock (this)
            {
                Assembly assembly;
                AssemblyName askedAssembly = new AssemblyName(args.Name);

                string[] fields = args.Name.Split(',');
                string name = fields[0];
                string culture = fields[2];
                // failing to ignore queries for satellite resource assemblies or using [assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.MainAssembly)] 
                // in AssemblyInfo.cs will crash the program on non en-US based system cultures. detailed discussion: http://connect.microsoft.com/VisualStudio/feedback/details/526836/wpf-appdomain-assemblyresolve-being-called-when-it-shouldnt
                if (name.EndsWith(".resources") && !culture.EndsWith("neutral")) return null;

                string resourceName = string.Format("BlizzTV.Assets.Assemblies.{0}.dll", askedAssembly.Name);
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        LogManager.Instance.Write(LogMessageTypes.Fatal, string.Format("Can not resolve asked assembly: {0}", askedAssembly));
                        MessageBox.Show(i18n.CanNotLoadRequiredAssembliesMessage, i18n.CanNotLoadRequiredAssembliesTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(-1);
                    }

                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    assembly = Assembly.Load(assemblyData);
                }

                LogManager.Instance.Write(LogMessageTypes.Trace, "Loaded embedded assembly: " + askedAssembly);

                return assembly;
            }
        }
    }
}
