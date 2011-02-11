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

/* Xml Serialization FAQ: http://devolutions.net/articles/dot-net/Net-Serialization-FAQ.aspx#S21 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Windows.Forms;
using BlizzTV.Log;
using BlizzTV.Assets.i18n;
using BlizzTV.Utility.Helpers;

namespace BlizzTV.Modules.Subscriptions
{
    /// <summary>
    /// Provides a XML based storage for subscriptions.
    /// </summary>
    public sealed class SubscriptionsStorage
    {
        #region instance

        private static SubscriptionsStorage _instance = new SubscriptionsStorage();
        public static SubscriptionsStorage Instance { get { return _instance; } }

        #endregion

        private readonly string _subscriptonsFile = ApplicationHelper.GetResourcePath("subscriptions.db");
        private Type[] _knownTypes = new[] { typeof(Subscription) }; // known types that implements Subscription.
        private List<Subscription> _subscriptions = new List<Subscription>(); // the internal list of subscriptions.
                      
        public List<Subscription> Subscriptions { get { return this._subscriptions; } }

        private SubscriptionsStorage() 
        {
            LogManager.Instance.Write(LogMessageTypes.Info, "Loading subscriptions database..");
            this.RegisterKnownTypes();
            this.Load();
        }

        private void RegisterKnownTypes()  // loads & register known types that implements Subscriptions.
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof (Subscription))))
            {
                Array.Resize(ref this._knownTypes, this._knownTypes.Length + 1);
                this._knownTypes[this._knownTypes.Length - 1] = t;
            }
        }

        private void Load() // loads the subscriptions from xml storage.
        {
            try
            {
                if (!File.Exists(_subscriptonsFile)) this.CreateUsingDefaults(); // if subscriptions database does not exists, create one using the default database.
                using (FileStream fileStream = new FileStream(_subscriptonsFile, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Subscription>), new XmlAttributeOverrides(), this._knownTypes, new XmlRootAttribute("Subscriptions"), "");
                    this._subscriptions = (List<Subscription>)xs.Deserialize(fileStream);
                }
            }
            catch (Exception e) 
            { 
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("An exception occured while loading subscriptions database: {0}", e));
                DialogResult result = MessageBox.Show(i18n.SubscriptionsDatabaseCorruptedMessage, i18n.SubscriptionsDatabsaeCorruptedTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                
                if(result == DialogResult.Yes) 
                {
                    this.CreateUsingDefaults(); 
                    this.Load();
                    MessageBox.Show(i18n.ReplacedSubscriptionsDatabaseMessage, i18n.ReplacedSubscriptionsDatabaseTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Creates a subscriptions database based on default database.
        /// </summary>
        private void CreateUsingDefaults()
        {
            using (FileStream fileStream = new FileStream(_subscriptonsFile, FileMode.Create))
            {
                fileStream.Write(Encoding.UTF8.GetBytes(Assets.XML.Subscriptions.Default), 0, Assets.XML.Subscriptions.Default.Length);
            }
        }

        /// <summary>
        /// Saves the changes and writes to xml storage.
        /// </summary>
        public void Save()
        {
            using (FileStream fileStream = new FileStream(_subscriptonsFile, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Subscription>), new XmlAttributeOverrides(), this._knownTypes, new XmlRootAttribute("Subscriptions"), "");
                xs.Serialize(fileStream, this._subscriptions);
            }
        }

        /// <summary>
        /// Returns a dictionary of subscriptions based on supplied subscription type.
        /// </summary>
        /// <param name="type">The subscription type.</param>
        /// <returns>Dictionary of subscriptions based on provided subscription-type.</returns>
        public List<Subscription> GetSubscriptions(Type type)
        {
            return this._subscriptions.Where(subscription => subscription.GetType() == type).ToList();
        }
    }
}
