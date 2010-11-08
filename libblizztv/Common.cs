using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;

namespace LibBlizzTV
{
    public class MenuItemEventArgs:EventArgs
    {
        private string _name;
        private EventHandler _handler;
        private Image _icon;

        public string Name { get { return this._name; } }
        public EventHandler Handler { get { return this._handler; } }
        public Image Icon { get { return this._icon; } }

        public MenuItemEventArgs(string Name, EventHandler Handler, Image Icon = null)
        {
            this._name = Name;
            this._handler = Handler;
            this._icon = Icon;
        }
    }
}
