using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibBlizzTV.Players
{
    // latest flash player activex can be downloaded from: http://fpdownload.adobe.com/get/flashplayer/current/install_flash_player_ax.exe

    /// <summary>
    /// Customized Flash Player.
    /// </summary>
    public class FlashPlayer : AxShockwaveFlashObjects.AxShockwaveFlash
    {
        private const int WM_RBUTTONDOWN = 0x0204;

        /// <summary>
        /// Initiates a new Flash Player object.
        /// </summary>
        public FlashPlayer()
            : base()
        {
            this.HandleCreated += new EventHandler(FlashPlayer_HandleCreated);
        }

        void FlashPlayer_HandleCreated(object sender, EventArgs e)
        {
            this.AllowFullScreen = "true";
            this.AllowNetworking = "all";
            this.AllowScriptAccess = "always";
        }

        /// <summary>
        /// Override's the WndProc and disables Flash activex's default right-click menu and if exists shows the attached ContextMenuStrip.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_RBUTTONDOWN)
            {
                if (this.ContextMenuStrip != null) this.ContextMenuStrip.Show(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
