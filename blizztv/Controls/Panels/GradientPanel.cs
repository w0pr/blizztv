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

/* Based on code of: http://www.openwinforms.com/creating_cool_gradient_panel_gdi.html */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BlizzTV.Controls.Panels
{
    [ToolboxBitmap(typeof(Panel))]
    public partial class GradientPanel : Panel
    {
        private int _borderWidth = 1;
        int _shadowOffSet = 0;
        int _cornerRadius = 0;
        Color _borderColor = Color.Gray;
        Color _gradientStartColor = Color.White;
        Color _gradientEndColor = Color.Gray;
     
        /// <summary>
        /// Gets or sets the border width of the panel.
        /// </summary>
        [Browsable(true), Category("GradientPanel")]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get { return this._borderWidth; }
            set { this._borderWidth = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets panels shadow offset.
        /// </summary>
        [Browsable(true), Category("GradientPanel")]
        [DefaultValue(1)]
        public int ShadowOffSet
        {
            get { return _shadowOffSet; }
            set { if (value == 0) value = 1; _shadowOffSet = Math.Abs(value); Invalidate(); }
        }

        /// <summary>
        /// Gets or sets panels corner radius.
        /// </summary>
        [Browsable(true), Category("GradientPanel")]
        [DefaultValue(1)]
        public int CornerRadius
        {
            get { return this._cornerRadius; }
            set { if (value == 0) value = 1; this._cornerRadius = Math.Abs(value); Invalidate(); }
        }

        /// <summary>
        /// Gets or sets border color.
        /// </summary>
        [Browsable(true), Category("GradientPanel")]
        [DefaultValue("Color.Gray")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets gradients start color.
        /// </summary>
        [Browsable(true), Category("GradientPanel")]
        [DefaultValue("Color.White")]
        public Color GradientStartColor
        {
            get { return _gradientStartColor; }
            set { _gradientStartColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets gradients end color.
        /// </summary>
        [Browsable(true), Category("GradientPanel")]
        [DefaultValue("Color.Gray")]
        public Color GradientEndColor
        {
            get { return _gradientEndColor; }
            set { _gradientEndColor = value; Invalidate(); }
        }

        public GradientPanel()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            int tmpShadowOffSet = Math.Min(Math.Min(_shadowOffSet, this.Width - 2), this.Height - 2);
            int tmpCornerRadius = Math.Min(Math.Min(_cornerRadius, this.Width - 2), this.Height - 2);

            if (this.Width <= 1 || this.Height <= 1) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle gradientRectangle = new Rectangle(0, 0, this.Width - tmpShadowOffSet - 1, this.Height - tmpShadowOffSet - 1);
            Rectangle shadowRectangle = new Rectangle(tmpShadowOffSet, tmpShadowOffSet, this.Width - tmpShadowOffSet - 1, this.Height - tmpShadowOffSet - 1);

            GraphicsPath shadowPath = GetRoundPath(shadowRectangle, tmpCornerRadius);
            GraphicsPath gradientPath = GetRoundPath(gradientRectangle, tmpCornerRadius);

            if (tmpCornerRadius > 0)
            {
                using (PathGradientBrush gBrush = new PathGradientBrush(shadowPath))
                {
                    gBrush.WrapMode = WrapMode.Clamp;
                    ColorBlend colorBlend = new ColorBlend(3)
                                                {
                                                    Colors = new Color[]
                                                                 {
                                                                     Color.Transparent,
                                                                     Color.FromArgb(180, Color.DimGray),
                                                                     Color.FromArgb(180, Color.DimGray)
                                                                 },
                                                    Positions = new float[] {0f, .1f, 1f}
                                                };

                    gBrush.InterpolationColors = colorBlend;
                    e.Graphics.FillPath(gBrush, shadowPath);
                }
            }

            // Draw backgroup
            LinearGradientBrush brush = new LinearGradientBrush(gradientRectangle, this._gradientStartColor, this._gradientEndColor, LinearGradientMode.BackwardDiagonal);
            e.Graphics.FillPath(brush, gradientPath);
            e.Graphics.DrawPath(new Pen(Color.FromArgb(180, this._borderColor), _borderWidth), gradientPath);
        }

        private static GraphicsPath GetRoundPath(Rectangle r, int depth)
        {
            GraphicsPath graphPath = new GraphicsPath();

            graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90);
            graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90);
            graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, r.Y + depth / 2);

            return graphPath;
        }
    }
}
