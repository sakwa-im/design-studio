using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using log4net;

namespace sakwa
{
    public partial class FloatingForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FloatingForm));
        private float _opacity;
        private GraphicsPath _path;
        private Form _floatOver = null;

        public FloatingForm()
        {
        }

        public FloatingForm(Form floatOver, float opacity, UserControl innerControl, GraphicsPath path = null)
        {
            InitializeComponent();
            _opacity = opacity;
            _path = path;
            _floatOver = floatOver;

            if (floatOver != null)
            {
               Icon = floatOver.Icon;
               Text = floatOver.Text;
               Size = floatOver.Size;
               BackgroundImageLayout = ImageLayout.Stretch;
            }

            DoubleBuffered = true;
            innerControl.Top = (Height - innerControl.Height) / 2;
            innerControl.Left = (Width - innerControl.Width) / 2;
            Controls.Add(innerControl);
            Refresh();
         } //public FloatingForm( ...

         public override void  Refresh()
         {
            base.Refresh();
            if (_floatOver != null)
            {
               Bitmap img = new Bitmap(_floatOver.Width, _floatOver.Height);
               _floatOver.DrawToBitmap(img, new Rectangle(0, 0, _floatOver.Width, _floatOver.Height));
               BackgroundImage = GraphicSupport.ToneDown(_opacity, img, _path);
            }
         }

        private void FloatingForm_Shown(object sender, EventArgs e)
        {
            if (_floatOver != null)
                Location = _floatOver.Location;

        }

        private void FloatingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //GC.Collect();
        }

     } //public partial class FloatingForm
}
