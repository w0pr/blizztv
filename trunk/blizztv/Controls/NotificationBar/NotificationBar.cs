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

/* Based on code of: http://www.codeproject.com/KB/miscctrl/InformationBar.aspx */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.ComponentModel;
using System.Threading;

namespace BlizzTV.Controls.NotificationBar
{
    public class NotificationBar : Control
    {
        System.Windows.Forms.Timer flashTimer = new System.Windows.Forms.Timer();
        ImageList smallImageList = null;
        int imageKey = 0;

        Size closeButtonSize = new Size(20, 20);
        int closeButtonPadding = 6;
        int textY = 5;

        bool playSoundOnVisible = true;
        bool mouseInBounds = false;
        bool controlHighlighted = false;
        bool inAnimation = false;

        int tickCount = 0;
        int flashCount = 0;
        int flashTo = 0;

        [Description("Image list containg images shown on the left side of the control"),
        Category("Appearance")]
        public ImageList SmallImageList
        {
            get
            {
                return smallImageList;
            }
            set
            {
                smallImageList = value;
            }
        }

        [DefaultValue(0)]
        [Description("The index of an image contained in the SmallImageList in which to display on the control"),
        Category("Appearance")]
        public int ImageIndex
        {
            get
            {
                return imageKey;
            }
            set
            {
                imageKey = value;
                this.Invalidate();
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        [DefaultValue(true)]
        [Description("Weather or not a sound should be played when the control is shown"),
        Category("Behavior")]
        public bool PlaySoundWhenShown
        {
            get
            {
                return playSoundOnVisible;
            }
            set
            {
                playSoundOnVisible = value;
            }
        }

        public NotificationBar()
        {
            this.BackColor = SystemColors.Info;

            flashTimer.Interval = 1000;
            flashTimer.Tick += new EventHandler(flashTimer_Tick);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void Flash(int interval, int times)
        {
            if (this.Visible)
            {
                flashTo = times;
                tickCount = 0;

                flashTimer.Interval = interval;
                flashTimer.Start();
            }
        }

        public void Flash(int numberOfTimes)
        {
            Flash(1000, numberOfTimes);
        }

        public void FlashOnce(int milliseconds)
        {
            Flash(milliseconds, 1);
        }

        public void Show(bool animate)
        {
            if (animate)
            {
                int origHeight = this.Height;
                this.Height = 1;
                this.Show();
                inAnimation = true;

                for (int height = this.Height; height < origHeight; height += 5)
                {
                    textY += 2;
                    this.Height = height;
                    this.Refresh();
                    Thread.Sleep(5);
                }

                inAnimation = false;
            }
            else
            {
                this.Show();
            }

            this.Refresh();
        }

        public void Hide(bool animate)
        {
            if (animate)
            {
                int origHeight = this.Height;
                inAnimation = true;

                for (int height = this.Height; height > 1; height -= 5)
                {
                    textY -= 2;
                    this.Height = height;
                    this.Refresh();
                    Thread.Sleep(5);
                }
                this.Hide();
                this.Height = origHeight;
                inAnimation = false;
            }
            else
            {
                this.Hide();
            }

            this.Refresh();
        }

        void flashTimer_Tick(object sender, EventArgs e)
        {
            if (controlHighlighted)
            {
                this.ForeColor = SystemColors.ControlText;
                this.BackColor = SystemColors.Info;
                controlHighlighted = false;
                flashCount++;

                if (flashCount == flashTo)
                {
                    flashTimer.Stop();
                    flashCount = 0;
                }
            }
            else
            {
                this.ForeColor = SystemColors.HighlightText;
                this.BackColor = SystemColors.Highlight;
                controlHighlighted = true;
            }

            tickCount++;
            this.Refresh();
        }

        #region Protected Methods
        protected void DrawText(PaintEventArgs e)
        {
            int leftPadding = 1;

            if (smallImageList != null && smallImageList.Images.Count > 0 && smallImageList.Images.Count > imageKey)
            {
                leftPadding = smallImageList.ImageSize.Width + 4;
                e.Graphics.DrawImage(smallImageList.Images[imageKey], new Point(2, 5));
            }

            Size textSize = TextRenderer.MeasureText(e.Graphics, this.Text, this.Font);
            int maxTextWidth = (this.Width - (closeButtonSize.Width + (closeButtonPadding * 2)));
            int lineHeight = textSize.Height + 2;
            int numLines = 1;

            if (textSize.Width > maxTextWidth)
            {
                numLines = textSize.Width / maxTextWidth + 1;
            }

            Rectangle textRect = new Rectangle();
            textRect.Width = this.Width - (closeButtonSize.Width + closeButtonPadding) - leftPadding;
            textRect.Height = (numLines * lineHeight);
            textRect.X = leftPadding;
            textRect.Y = 5;

            if (this.Height < (numLines * lineHeight) + 10)
            {
                if (!inAnimation)
                {
                    textRect.Y = 5;
                    this.Height = (numLines * lineHeight) + 10;
                }
                else
                {
                    textRect.Y = textY;
                }
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, textRect, this.ForeColor, TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.Top);
        }

        protected void DrawCloseButton(PaintEventArgs e)
        {
            Color closeButtonColor = Color.Black;

            if (mouseInBounds)
            {
                closeButtonColor = Color.White;
            }

            Pen linePen = new Pen(this.ForeColor, 2);
            Point line1Start = new Point((this.Width - (closeButtonSize.Width - closeButtonPadding)), closeButtonPadding);
            Point line1End = new Point((this.Width - closeButtonPadding), (closeButtonSize.Height - closeButtonPadding));
            Point line2Start = new Point((this.Width - closeButtonPadding), closeButtonPadding);
            Point line2End = new Point((this.Width - (closeButtonSize.Width - closeButtonPadding)), (closeButtonSize.Height - closeButtonPadding));

            e.Graphics.DrawLine(linePen, line1Start, line1End);
            e.Graphics.DrawLine(linePen, line2Start, line2End);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawText(e);
            DrawCloseButton(e);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.ForeColor = SystemColors.HighlightText;
            this.BackColor = SystemColors.Highlight;
            mouseInBounds = true;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (controlHighlighted)
            {
                this.ForeColor = SystemColors.HighlightText;
                this.BackColor = SystemColors.Highlight;
            }
            else
            {
                this.ForeColor = SystemColors.ControlText;
                this.BackColor = SystemColors.Info;
            }

            this.ForeColor = SystemColors.ControlText;
            mouseInBounds = false;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.X >= (this.Width - (closeButtonSize.Width + closeButtonPadding)) && e.Y <= 12)
            {
                this.Hide();
            }
            else
            {
                if (ContextMenuStrip != null)
                {
                    ContextMenuStrip.Show(this, e.Location);
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible && playSoundOnVisible)
            {
                SystemSounds.Beep.Play();
            }

            base.OnVisibleChanged(e);
        }
        #endregion
    }
}
