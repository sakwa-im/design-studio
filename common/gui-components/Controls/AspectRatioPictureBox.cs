using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace sakwa
{
    public class AspectRatioPictureBox : Panel
    {
        public AspectRatioPictureBox()
        {
        }

        public AspectRatioPictureBox(Image image)
        {
            _Image = image;
        }

        [Category("AspectRatioPictureBox"), RefreshProperties(RefreshProperties.All), Description("Image displayed in the AspectRatioPictureBox")]
        public Image Image
        { 
            get { return _Image; }
            set
            {
                _Image = value;
                Refresh();

            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_Image != null)
            {
               float horzFactor = Convert.ToSingle(this.Width) / Convert.ToSingle(_Image.Width);
               float vertFactor = Convert.ToSingle(this.Height) / Convert.ToSingle(_Image.Height);

                float factor = Math.Min(horzFactor, vertFactor);
                RectangleF rect = new RectangleF(0, 0, 0, 0);

                rect.Width = factor * _Image.Width;
                rect.Height = factor * _Image.Height;

                e.Graphics.DrawImage(_Image, rect);

            } //if (_Image != null)
        } //protected override void OnPaint(PaintEventArgs e)

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Refresh();
        }

        private Image _Image = null;

    } //public class AspectRatioPictureBox
}
