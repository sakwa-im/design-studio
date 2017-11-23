using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace sakwa
{
    public class ToolLedStrip : ToolStripStatusLabel
    {
        public ToolLedStrip()
        {
            base.AutoSize = false;
        }
           
        // Allowed statusses and assigned colors 
        public enum ToolLedIndicatorState
        {
            Clear,   // Gray 
            Warning, // Orange
            Cleared, // Green 
            Critical // Red
        };

        private bool _AutoSize = true;
        private string _Text = "";

        public new bool AutoSize
        {
            get { return _AutoSize; }
            set
            {
                if (_AutoSize != value)
                {
                    _AutoSize = value;
                }
            }
        }

        // Override Text to set the Width of the rendered text area, 
        // also taking in account right alignment and picture + margin size
        public new string Text
        {
            get { return _Text; }
            set
            {
                if (_AutoSize == false && Owner != null && Owner.Handle != null)
                {
                    Graphics g = Graphics.FromHwnd(Owner.Handle);
                    Size textSize = g.MeasureString(value, Font).ToSize();
                    //Width = textSize.Width + Height + Margin.Right;

                    _Text = value.Substring(0, value.Length * Width / Width);
                }

                _Text = value;
                ToolTipText = value;

                //Invalidate();

            }
        }

        // Set the default / initial status "Clear" 
        public ToolLedIndicatorState _currentToolLedIndicatorState = ToolLedIndicatorState.Clear;

        // Getter / Setter of the ImageList 
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ImageList ImageList { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            Rectangle rect = e.ClipRectangle;
            e.Graphics.DrawString(_Text, Font, new SolidBrush(ForeColor), rect, stringFormat);
            rect.X = rect.Width - rect.Height;
            rect.Width = rect.Height;
            if (ImageList != null && ImageList.Images.Count >= Convert.ToInt32(_currentToolLedIndicatorState))
            {
                e.Graphics.DrawImage(ImageList.Images[Convert.ToInt32(_currentToolLedIndicatorState)], rect);
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public ToolLedIndicatorState CurrentSeverity
        {
            get { return _currentToolLedIndicatorState; }
            set
            {
                if (_currentToolLedIndicatorState != value)
                {
                    _currentToolLedIndicatorState = value;
                    Invalidate();
                }
            }
        }
    }
}
