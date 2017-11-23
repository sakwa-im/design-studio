using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace sakwa
{
    public class NumericalScrollBar : Label
    {
        public NumericalScrollBar()
        {
            if (!DesignMode)
            {
                base.MouseClick += new MouseEventHandler(OnMouseClick);
                base.MouseMove += new MouseEventHandler(OnMouseMove);

            }

            base.FontChanged += new EventHandler(OnFontChanged);
            base.SizeChanged += new EventHandler(OnSizeChanged);
        }

        void OnSizeChanged(object sender, EventArgs e)
        {
            _Offsets = CalculateOffsets();
            Refresh();
        }

        void OnMouseClick(object sender, MouseEventArgs e)
        {
            int newValue = NewValue(e.Location);

            if (_Value != newValue)
            {
                Value = newValue;

            }
        }

        void OnFontChanged(object sender, EventArgs e)
        {
            _Offsets = CalculateOffsets();
            Refresh();

        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point p = PointToClient(e.Location);

        }

        private int NewValue(Point p)
        {
            if(_Value > _Minimum)
            {
                if (p.X < _Offsets[1])
                    return _Minimum;

                if(p.X < _Offsets[2])
                    return _Value - 1;

            } //if(_Value > _Minimum)

            if(_Value < _Maximum)
            {
                if(p.X > _Offsets[_Offsets.Length - 1])
                    return _Maximum;

                if(p.X > _Offsets[_Offsets.Length - 2])
                    return _Value + 1;

            }

            int index = 3;
            for(int i = _Minimum; i <= _Maximum; i++)
                if(p.X < _Offsets[index++])
                    return i;

            return _Value;

        }

        #region properties
        [Category("NumericalScrollBar"), RefreshProperties(RefreshProperties.All), Description("The value in the range")]
        public int Value { 
            get { return _Value; } 
            set
            {
                if (value < _Minimum)
                    value = _Minimum;

                if (value > _Maximum)
                    value = _Maximum;

                if (_Value != value)
                {
                    _Value = value;
                    if (ValueChanged != null)
                        ValueChanged(this, new EventArgs());

                    Refresh();

                } // if (_Value != value)
            } }
        private int _Value = 1;

        [Category("NumericalScrollBar"), RefreshProperties(RefreshProperties.All), Description("The lower limit of the scrollable range")]
        public int Minimum {
            get { return _Minimum; }
            set
            {
                _Minimum = value;
                Value = _Value;
                
                _Offsets = CalculateOffsets();

                Refresh();

            } }
        private int _Minimum = 1;

        [Category("NumericalScrollBar"), RefreshProperties(RefreshProperties.All), Description("The upper limit of the scrollable range")]
        public int Maximum {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
                Value = _Value;
                
                _Offsets = CalculateOffsets();

                Refresh();

            } }
        private int _Maximum = 1;

        [Category("NumericalScrollBar"), RefreshProperties(RefreshProperties.All), Description("The back color of the selected page indicator")]
        public Color SelectorBackColor { 
            get { return _SelectorBackColor; }
            set
            {
                _SelectorBackColor = value;
                Refresh();

            } }
        private Color _SelectorBackColor = SystemColors.ControlDark;

        [Category("NumericalScrollBar"), RefreshProperties(RefreshProperties.All), Description("The color of the selected page indicator")]
        public Color SelectorForeColor { 
            get { return _SelectorForeColor; }
            set
            {
                _SelectorForeColor = value;
                Refresh();

            } }
        private Color _SelectorForeColor = Color.White;
        #endregion

        private string _GotoFirst = "<<";
        private string _GoPrevious = "<";
        private string _GoNext = ">";
        private string _GotoLast = ">>";

        [Category("NumericalScrollBar")]
        [Description("Occurs when the value is changed")]
        public event EventHandler ValueChanged;

        private float[] _Offsets = null;

        private float[] CalculateOffsets()
        {
            float[] result = new float[(_Maximum - _Minimum) + 5];
            Graphics g = Graphics.FromHwnd(Handle);

            result[result.Length - 1] = Width - g.MeasureString(_GotoLast, Font).Width;
            result[result.Length - 2] = result[result.Length - 1] - g.MeasureString(_GoNext + " ", Font).Width;

            int index = result.Length - 3;

            for (int i = _Maximum; i >= _Minimum; i--)
            {
                result[index] = result[index + 1] - g.MeasureString(i.ToString() + " ", Font).Width;
                index--;

            }

            result[1] = result[2] - g.MeasureString(_GoPrevious + " ", Font).Width;
            result[0] = result[1] - g.MeasureString(_GotoFirst + " ", Font).Width;

            return result;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_Offsets == null)
                _Offsets = CalculateOffsets();

            Bitmap image = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(image);

            g.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));

            //float offset = 0;
            float spaceSize = g.MeasureString(" ", Font).Width;
            Brush b = new SolidBrush(ForeColor);
            if (_Value > _Minimum)
            {
                g.DrawString(_GotoFirst, Font, b, _Offsets[0], 0);
                //offset += g.MeasureString(_GotoFirst, Font).Width + spaceSize;

                g.DrawString(_GoPrevious, Font, b, _Offsets[1], 0);
                //g.DrawString(_GoPrevious, Font, b, offset, 0);
                //offset += g.MeasureString(_GoPrevious, Font).Width + spaceSize;

            }
            //else
            //    offset += g.MeasureString(_GotoFirst + _GoPrevious, Font).Width + 2 * spaceSize;

            int index = 2;
            for (int i = _Minimum; i <= _Maximum; i++)
            {
                if (i == _Value)
                {
                    SizeF dims = g.MeasureString(i.ToString() + " ", Font);
                    g.FillRectangle(new SolidBrush(_SelectorBackColor), _Offsets[index], 0, dims.Width, dims.Height);
                    g.DrawString(i.ToString(), Font, new SolidBrush(_SelectorForeColor), _Offsets[index++], 0);
                    //g.DrawString(i.ToString(), Font, new SolidBrush(_SelectorForeColor), offset, 0);
                    //offset += dims.Width;

                }
                else
                {
                    g.DrawString(i.ToString(), Font, b, _Offsets[index++], 0);
                    //g.DrawString(i.ToString(), Font, b, offset, 0);
                    //offset += g.MeasureString(i.ToString() + " ", Font).Width;

                }
            }

            if (_Value < _Maximum)
            {
                g.DrawString(_GoNext, Font, b, _Offsets[_Offsets.Length - 2], 0);
                //g.DrawString(_GoNext, Font, b, offset, 0);
                //offset += g.MeasureString(_GoNext, Font).Width + spaceSize;

                g.DrawString(_GotoLast, Font, b, _Offsets[_Offsets.Length - 1], 0);
                //g.DrawString(_GotoLast, Font, b, offset, 0);
            }

            e.Graphics.DrawImage(image, 0, 0);

        } //protected override void OnPaint(PaintEventArgs e)

    } //class NumericalScrollBar
}
