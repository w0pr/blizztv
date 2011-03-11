using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.InfraStructure.Modules.Storage;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.InfraStructure.Modules
{
    public class ModuleNode : TreeNode
    {
        private NodeState _state = NodeState.Unknown; // the state of the item.

        public ModuleNode() { }

        public ModuleNode(string text) : base(text) { }

        /// <summary>
        /// The unique guid.
        /// </summary>
        public string Guid { get; protected set; }

        /// <summary>
        /// The icon of the item.
        /// </summary>
        public NamedImage Icon { get; protected set; }

        /// <summary>
        /// Bound context-menu's to item.
        /// </summary>
        public readonly Dictionary<string, ToolStripMenuItem> Menu = new Dictionary<string, ToolStripMenuItem>();

        /// <summary>
        /// The item state.
        /// </summary>
        public NodeState State
        {
            get
            {
                if (this._state == NodeState.Unknown)
                {
                    string key = string.Format("{0}.{1}", this.GetType(), this.Guid);

                    if (string.IsNullOrEmpty(this.Guid) || !StateStorage.Instance.Exists(key)) this.State = NodeState.Fresh; // if key does not exists in state-storage already, then it's just fresh.                    
                    else // if already exists on state-storage
                    {
                        this._state = (NodeState)StateStorage.Instance[key]; // get the last state from storage.
                        if (this._state == NodeState.Fresh) this.State = NodeState.Unread; // if last state was fresh, change it unread.
                        else { if (this.OnStateChange != null) this.OnStateChange(this, EventArgs.Empty); }
                    }
                }
                return this._state;
            }
            set
            {
                this._state = value;
                string key = string.Format("{0}.{1}", this.GetType(), this.Guid);
                if (!string.IsNullOrEmpty(this.Guid)) StateStorage.Instance[key] = (byte)this._state; // set the new state.
                if (this.OnStateChange != null) this.OnStateChange(this, EventArgs.Empty); // notify about the state change.
            }
        }

        /// <summary>
        /// Notifies about a state change on the item.
        /// </summary>
        public EventHandler OnStateChange;

        public delegate void ShowFormEventHandler(System.Windows.Forms.Form form, bool isModal);
        public event ShowFormEventHandler OnShowForm;

        /// <summary>
        /// Executes an open command on item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        public virtual void Open(object sender, EventArgs e) { }

        /// <summary>
        /// Notifies about a right-click on item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        public virtual void RightClicked(object sender, EventArgs e) { }

        /// <summary>
        /// Notifies about a notification click of the item.
        /// </summary>
        public virtual void NotificationClicked() { }
    }

    /// <summary>
    /// Item states.
    /// </summary>
    public enum NodeState
    {
        Unknown,
        Fresh,
        Unread,
        Read,
        Error
    }
}
