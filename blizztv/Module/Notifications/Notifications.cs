using System.Windows.Forms;

namespace BlizzTV.Module.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Notifications
    {
        private static Notifications _instance = new Notifications();

        /// <summary>
        /// 
        /// </summary>
        public static Notifications Instance { get { return _instance; } }

        /// <summary>
        /// 
        /// </summary>
        private Notifications() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Title"></param>
        /// <param name="Text"></param>
        /// <param name="Icon"></param>
        public delegate void ShowNotificationEventHandler(object sender,string Title,string Text,ToolTipIcon Icon);

        /// <summary>
        /// 
        /// </summary>
        public event ShowNotificationEventHandler OnNotificationRequest;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Title"></param>
        /// <param name="Text"></param>
        /// <param name="Icon"></param>
        public void Show(object sender, string Title, string Text, ToolTipIcon Icon)
        {
            if (OnNotificationRequest != null) OnNotificationRequest(sender, Title, Text, Icon);
        }
    }
}
