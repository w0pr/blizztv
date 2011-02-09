using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.Controls.BlizzView
{
    public class BlizzView : TreeView
    {
        private const int WM_ERASEBKGND = 0x0014;

        private Image mImage;
        public Image Image
        {
            get { return mImage; }
            set { mImage = value; Invalidate(); }
        }

        public BlizzView()
        {
            this.mImage = Assets.Images.Images.blizztv.GrayScale();
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            if (mImage != null) Invalidate();
            base.OnAfterCollapse(e);
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            if (mImage != null) Invalidate();
            base.OnAfterExpand(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (mImage != null) Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_ERASEBKGND && mImage != null)
            {
                using (var gr = Graphics.FromHdc(m.WParam))
                {
                    gr.DrawImage(mImage, new Point(this.Width - (mImage.Width + 5), this.Height - (mImage.Height + 5)));
                }
            }

	    /* http://stackoverflow.com/questions/3377870/how-to-set-the-background-image-of-a-treeview-control-vs-2008-net-3-5-c-winfo */
        }
    }
}
