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

/* Xml Serialization FAQ: http://devolutions.net/articles/dot-net/Net-Serialization-FAQ.aspx#S21 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.ModuleLib.Subscriptions
{
    public sealed class SubscriptionsStorage
    {
        #region instance

        private static SubscriptionsStorage _instance = new SubscriptionsStorage();
        public static SubscriptionsStorage Instance { get { return _instance; } }

        #endregion

        private const string SubscriptonsFile = "subscriptions.db";
        private Type[] _knownTypes = new[] { typeof(ISubscription) };
        
        private List<ISubscription> _subscriptions = new List<ISubscription>();                        
        public List<ISubscription> Subscriptions { get { return this._subscriptions; } }

        private SubscriptionsStorage() 
        {
            Log.Instance.Write(LogMessageTypes.Info, "Loading subscriptions database..");
            this.RegisterKnownTypes();
            this.Load();
        }

        private void RegisterKnownTypes()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(ISubscription)))
                {
                    Array.Resize(ref this._knownTypes, this._knownTypes.Length + 1);
                    this._knownTypes[this._knownTypes.Length - 1] = t;
                }
            }
        }

        private void Load()
        {
            try
            {
                if (!File.Exists(SubscriptonsFile)) this.LoadDefaults();
                using (FileStream fileStream = new FileStream(SubscriptonsFile, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<ISubscription>), new XmlAttributeOverrides(), this._knownTypes, new XmlRootAttribute("Subscriptions"), "");
                    this._subscriptions = (List<ISubscription>)xs.Deserialize(fileStream);
                }
            }
            catch (Exception e) 
            { 
                Log.Instance.Write(LogMessageTypes.Error, string.Format("An error occured while loading subscriptions.db: {0}", e.ToString()));
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Your subscriptions database is corrupted. Do you want it to be replaced with a default one?", "Subscriptions Database Corrupted", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);
                if(result== System.Windows.Forms.DialogResult.Yes) 
                {
                    this.LoadDefaults();
                    this.Load();
                    System.Windows.Forms.MessageBox.Show("Replaced your subscriptions database, it should be all working now.", "Subscriptions Database Replaced", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }

        private void LoadDefaults()
        {
            using (FileStream fileStream = new FileStream(SubscriptonsFile, FileMode.Create))
            {
                fileStream.Write(Encoding.UTF8.GetBytes(Properties.Resources.Subscriptions_Default), 0, Properties.Resources.Subscriptions_Default.Length);
            }
        }

        public void Save()
        {
            using (FileStream fileStream = new FileStream(SubscriptonsFile, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ISubscription>), new XmlAttributeOverrides(), this._knownTypes, new XmlRootAttribute("Subscriptions"), "");
                xs.Serialize(fileStream, this._subscriptions);
            }
        }

        public List<ISubscription> GetSubscriptions(Type type)
        {
            return this._subscriptions.Where(subscription => subscription.GetType() == type).ToList();
        }
    }
}
