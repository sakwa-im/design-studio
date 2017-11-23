using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
//using log4net;

namespace sakwa
{
    public class RoundedPanel : Panel
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(RoundedPanel));

        public RoundedPanel()
        {
            ResizeRedraw = true;
        }

        [Category("RoundedPanel"), RefreshProperties(RefreshProperties.All), Description("The Radius of the rounded corners")]
        public int Radius { get { return _Radius; } set { _Radius = value; } }
        protected int _Radius = 5;

        [Category("RoundedPanel"), RefreshProperties(RefreshProperties.All), Description("The type of the rounded corners")]
        public RoundedTypes Corners { get { return _Corners; } set { _Corners = value; } }
        protected RoundedTypes _Corners = RoundedTypes.Full;

        [Category("RoundedPanel"), RefreshProperties(RefreshProperties.All), Description("The FillColor of the rounded panel")]
        public Color FillColor { get { return _FillColor; } set { _FillColor = value; } }
        protected Color _FillColor = SystemColors.Control;

        [Category("RoundedPanel"), RefreshProperties(RefreshProperties.All), Description("The Color of the border")]
        public Color BorderColor { get { return _BorderColor; } set { _BorderColor = value; } }
        protected Color _BorderColor = Color.Black;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle r = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            DrawRoundedBox(pevent.Graphics, r, _Corners, _Radius, _FillColor, _BorderColor);

            //log.Debug("RoundedPanel::Paint " + Convert.ToString(++cnt) + " " + ClientRectangle.ToString());
            if (lbl != null)
                lbl.Text = Convert.ToString(++cnt) + " " + ClientRectangle.ToString();

        }

        public enum RoundedTypes { Transparent, None, Left, Top, Right, Bottom, Full, TopLeft, TopRight, BottomRight, BottomLeft }
        private void DrawRoundedBox(Graphics g, Rectangle bounds, RoundedTypes type, int radius, Color fillColor, Color borderColor)
        {
            GraphicsPath path = new GraphicsPath();
            Pen pen = new Pen(borderColor);

            if (type != RoundedTypes.Transparent)
            {
                pen.EndCap = pen.StartCap = LineCap.Round;
                path.StartFigure();
                switch (type)
                {
                    case RoundedTypes.Left:
                        /*Left top*/
                        path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                        /*Right side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                        /*Left bottom*/
                        path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                        break;

                    case RoundedTypes.Top:
                        /*Left top*/
                        path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                        /*Right top*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                        /*Bottom side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                        break;

                    case RoundedTypes.Right:
                        /*Right top*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                        /*Right bottom*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                        /*Left side*/
                        path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
                        break;

                    case RoundedTypes.Bottom:
                        /*Top side*/
                        path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                        /*Right bottom*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                        /*Left bottom*/
                        path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                        break;

                    case RoundedTypes.Full:
                        /*Left top*/
                        path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                        /*Right top*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                        /*Right bottom*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                        /*Left bottom*/
                        path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                        break;

                    case RoundedTypes.None:
                        /*Top side*/
                        path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                        /*Right side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                        /*Bottom side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                        /*Left side*/
                        path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
                        break;

                    case RoundedTypes.TopLeft:
                        /*Left top*/
                        path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                        /*Right side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                        /*Bottom side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                        break;

                    case RoundedTypes.TopRight:
                        /*Right top*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                        /*Right side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y + radius, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                        /*Bottom side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                        break;

                    case RoundedTypes.BottomLeft:
                        /*Top side*/
                        path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                        /*Right side*/
                        path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                        /*Left bottom*/
                        path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                        break;

                    case RoundedTypes.BottomRight:
                        /*Left side*/
                        path.AddLine(bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height);
                        /*Bottom side*/
                        path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X + bounds.Width - radius, bounds.Y + bounds.Height);
                        /*Right bottom*/
                        path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                        break;

                }
                path.CloseAllFigures();

                g.SmoothingMode = SmoothingMode.HighQuality;

                g.FillPath(new SolidBrush(fillColor), path);
                g.DrawPath(pen, path);

            }
            else
                g.FillRectangle(new SolidBrush(fillColor), bounds);

        } //private void DrawRoundedBox( ...

        private int cnt = 0;
        public Label lbl = null;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RoundedPanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.ResumeLayout(false);

        }

    } //public class RoundedPanel
}
