using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class SakwaUserControl : UserControl
    {
        public SakwaUserControl() : base()
        {
        }

        public void OnActivate()
        {
            DoActivate();
        }
        public void OnDeactivate()
        {
            DoDeactivate();
        }

        protected virtual void DoActivate()
        {

        }
        protected virtual void DoDeactivate()
        {

        }
        protected virtual void DefineConfiguration()
        {
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
        }
        protected Font SetFont(Font font, float delta)
        {
            return new Font(font.FontFamily, font.Size + delta, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        }

        public virtual void ReconnectConfig()
        {

        }

    }
}
